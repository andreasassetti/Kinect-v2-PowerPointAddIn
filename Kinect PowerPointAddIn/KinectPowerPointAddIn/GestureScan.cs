using Microsoft.Kinect;
using Microsoft.Kinect.VisualGestureBuilder;

using System;
using System.Linq;

namespace KinectPowerPointAddIn
{
    internal class GestureScan : IDisposable
    {
        private readonly VisualGestureBuilderFrameSource _vgbFrameSource;
        private VisualGestureBuilderFrameReader _vgbFrameReader;

        public GestureScan(KinectSensor kinectSensor, ulong trackingId, VisualGestureBuilderDatabase visualGestureBuilderDatabase)
        {
            TrackingId = trackingId;

            _vgbFrameSource = new VisualGestureBuilderFrameSource(kinectSensor, 0);

            _vgbFrameSource.TrackingIdLost += OnTrackingIdLost;

            _vgbFrameReader = _vgbFrameSource.OpenReader();
            _vgbFrameReader.IsPaused = true;
            _vgbFrameReader.FrameArrived += OnFrameArrived;

            foreach (var gesture in visualGestureBuilderDatabase.AvailableGestures)
            {
                _vgbFrameSource.AddGesture(gesture);
            }

            _vgbFrameSource.TrackingId = trackingId;
            _vgbFrameReader.IsPaused = false;
        }

        public event EventHandler TrackingIdLost;
        public event EventHandler TakeControl;
        public event EventHandler Next;
        public event EventHandler Previous;
        public event EventHandler End;

        public ulong TrackingId { get; private set; }

        public void Pause()
        {
            _vgbFrameReader.IsPaused = true;
        }

        public void Resume()
        {
            _vgbFrameReader.IsPaused = false;
        }

        public void Dispose()
        {
            _vgbFrameReader.IsPaused = true;
            _vgbFrameReader.Dispose();
            _vgbFrameReader = null;

            _vgbFrameSource.Dispose();
            _vgbFrameSource.Dispose();
        }

        private void OnFrameArrived(object sender, VisualGestureBuilderFrameArrivedEventArgs e)
        {
            using (var frame = e.FrameReference.AcquireFrame())
            {
                if (frame == null)
                {
                    return;
                }

                if (frame.DiscreteGestureResults == null)
                {
                    return;
                }

                foreach (var gestureKey in frame.DiscreteGestureResults.Keys.Where(gestureKey => frame.DiscreteGestureResults[gestureKey].Detected))
                {
                    switch (gestureKey.Name)
                    {
                        case "TakeControl":
                            if (TakeControl != null)
                                TakeControl(this, EventArgs.Empty);

                            break;

                        case "Next":
                            if (Next != null)
                                Next(this, EventArgs.Empty);

                            break;

                        case "Previous":
                            if (Previous != null)
                                Previous(this, EventArgs.Empty);

                            break;

                        case "End":
                            if (End != null)
                                End(this, EventArgs.Empty);

                            break;
                    }
                }
            }
        }

        private void OnTrackingIdLost(object sender, TrackingIdLostEventArgs e)
        {
            _vgbFrameReader.IsPaused = true;
            _vgbFrameReader.Dispose();
            _vgbFrameReader = null;

            _vgbFrameSource.Dispose();
            _vgbFrameSource.Dispose();

            var tmpTrackingIdLost = TrackingIdLost;

            if (tmpTrackingIdLost != null)
                tmpTrackingIdLost(this, EventArgs.Empty);
        }
    }
}
