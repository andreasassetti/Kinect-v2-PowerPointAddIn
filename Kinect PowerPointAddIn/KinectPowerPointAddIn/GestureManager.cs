using Microsoft.Kinect;
using Microsoft.Kinect.VisualGestureBuilder;
using Microsoft.Office.Interop.PowerPoint;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace KinectPowerPointAddIn
{
    internal class GestureManager
    {
        private readonly KinectSensor _kinectSensor;
        private readonly ConcurrentDictionary<ulong, GestureScan> _scans;
        private readonly List<Body> _bodies;
        private readonly VisualGestureBuilderDatabase _visualGestureBuilderDatabase;
        private readonly object _sync = new object();

        private BodyFrameReader _bodyFrameReader;
        private ulong _bodyWithControl;
        private SlideShowWindow _slideShowWindow;

        public GestureManager(KinectSensor kinectSensor, string visualGestureBuilderDatabaseFileName)
        {
            _kinectSensor = kinectSensor;
            _visualGestureBuilderDatabase = new VisualGestureBuilderDatabase(visualGestureBuilderDatabaseFileName);
            _scans = new ConcurrentDictionary<ulong, GestureScan>();
            _bodies = new List<Body>(_kinectSensor.BodyFrameSource.BodyCount);

            Delay = 4000;
        }

        public int Delay { get; set; }

        public void Start(SlideShowWindow slideShowWindow)
        {
            _slideShowWindow = slideShowWindow;

            _bodyFrameReader = _kinectSensor.BodyFrameSource.OpenReader();
            _bodyFrameReader.FrameArrived += OnBodyFrameArrived;

            _bodies.Clear();
            _scans.Clear();
        }

        public void Stop()
        {
            foreach (var gestureScan in _scans.Values)
            {
                gestureScan.Dispose();
            }
        }

        private void OnBodyFrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            using (var bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame == null) return;

                bodyFrame.GetAndRefreshBodyData(_bodies);

                foreach (var body in _bodies.Where(b => b.IsTracked && !_scans.ContainsKey(b.TrackingId)))
                {
                    var gestureScan = new GestureScan(_kinectSensor, body.TrackingId, _visualGestureBuilderDatabase);

                    gestureScan.Next += OnNext;
                    gestureScan.Previous += OnPrevious;
                    gestureScan.TakeControl += OnTakeControl;
                    gestureScan.TrackingIdLost += OnTrackingIdLost;
                    gestureScan.End += OnEnd;

                    _scans.TryAdd(body.TrackingId, gestureScan);
                }
            }
        }

        private void OnEnd(object sender, EventArgs e)
        {
            lock (_sync)
            {
                if (_bodyWithControl == ((GestureScan)sender).TrackingId)
                {
                    _slideShowWindow.View.EndNamedShow();
                }
            }
        }

        private void OnTrackingIdLost(object sender, EventArgs e)
        {
            GestureScan gestureScan;

            _scans.TryRemove(((GestureScan)sender).TrackingId, out gestureScan);
        }

        private void OnTakeControl(object sender, EventArgs e)
        {
            lock (_sync)
            {
                _bodyWithControl = ((GestureScan)sender).TrackingId;

                Sleep();
            }
        }

        private void OnPrevious(object sender, EventArgs e)
        {
            lock (_sync)
            {
                if (_bodyWithControl == ((GestureScan)sender).TrackingId)
                {
                    _slideShowWindow.View.Previous();

                    Sleep();
                }
            }
        }

        private void OnNext(object sender, EventArgs e)
        {
            lock (_sync)
            {
                if (_bodyWithControl == ((GestureScan)sender).TrackingId)
                {
                    _slideShowWindow.View.Next();

                    Sleep();
                }
            }
        }

        private void Sleep()
        {
            foreach (var gestureScan in _scans.Values)
            {
                gestureScan.Pause();
            }

            new System.Threading.Timer(OnResumeScan, null, Delay, -1);
        }

        private void OnResumeScan(object state)
        {
            foreach (var gestureScan in _scans.Values)
            {
                gestureScan.Resume();
            }
        }
    }
}