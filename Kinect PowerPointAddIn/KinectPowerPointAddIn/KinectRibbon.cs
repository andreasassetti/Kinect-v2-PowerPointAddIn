using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace KinectPowerPointAddIn
{
    public partial class KinectRibbon
    {
        public event EventHandler KinectControlEnabedChanged;
        public event EventHandler StartBeginning;

        public bool KinectControlEnabled
        {
            get
            {
                return kinectControlSpltBtn.Checked;
            }

            set
            {
                kinectControlSpltBtn.Checked = value;
            }
        }

        private void OnKinectRibbonLoad(object sender, RibbonUIEventArgs e)
        {

        }

        private void OnStartFromBeginningClick(object sender, RibbonControlEventArgs e)
        {
            var internalStartBeginning = StartBeginning;

            if (internalStartBeginning != null)
                internalStartBeginning(this, EventArgs.Empty);
        }

        private void OnKinectControlClick(object sender, RibbonControlEventArgs e)
        {
            var internalKinectControlEnabedChanged = KinectControlEnabedChanged;

            if (internalKinectControlEnabedChanged != null)
                internalKinectControlEnabedChanged(this, EventArgs.Empty);
        }

        private void OnDebugViewClick(object sender, RibbonControlEventArgs e)
        {
            //if (_debugViewWindow.IsVisible)
            //    _debugViewWindow.Close();
            //else
            //{
            //    _debugViewWindow = new DebugWindow();
            //    _debugViewWindow.Show();
            //}
        }
    }
}
