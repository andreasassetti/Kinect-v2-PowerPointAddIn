namespace KinectPowerPointAddIn
{
    partial class KinectRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        //private DebugWindow _debugViewWindow;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public KinectRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();

           // _debugViewWindow = new DebugWindow();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainTb = this.Factory.CreateRibbonTab();
            this.mainGrp = this.Factory.CreateRibbonGroup();
            this.startBeginningBtn = this.Factory.CreateRibbonButton();
            this.kinectControlSpltBtn = this.Factory.CreateRibbonSplitButton();
            this.tglBtnDebugView = this.Factory.CreateRibbonToggleButton();
            this.mainTb.SuspendLayout();
            this.mainGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTb
            // 
            this.mainTb.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.mainTb.Groups.Add(this.mainGrp);
            this.mainTb.Label = "Kinect Slide Show control";
            this.mainTb.Name = "mainTb";
            // 
            // mainGrp
            // 
            this.mainGrp.Items.Add(this.kinectControlSpltBtn);
            this.mainGrp.Items.Add(this.startBeginningBtn);
            this.mainGrp.Name = "mainGrp";
            // 
            // startBeginningBtn
            // 
            this.startBeginningBtn.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.startBeginningBtn.Image = global::KinectPowerPointAddIn.Properties.Resources.KinectStart;
            this.startBeginningBtn.Label = "From Beginning";
            this.startBeginningBtn.Name = "startBeginningBtn";
            this.startBeginningBtn.ScreenTip = "Start from Beginning";
            this.startBeginningBtn.ShowImage = true;
            this.startBeginningBtn.SuperTip = "Start the show form the first slide with Kinect control.";
            this.startBeginningBtn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnStartFromBeginningClick);
            // 
            // kinectControlSpltBtn
            // 
            this.kinectControlSpltBtn.ButtonType = Microsoft.Office.Tools.Ribbon.RibbonButtonType.ToggleButton;
            this.kinectControlSpltBtn.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.kinectControlSpltBtn.Image = global::KinectPowerPointAddIn.Properties.Resources.KinectLogo;
            this.kinectControlSpltBtn.Items.Add(this.tglBtnDebugView);
            this.kinectControlSpltBtn.Label = "Kinect control";
            this.kinectControlSpltBtn.Name = "kinectControlSpltBtn";
            this.kinectControlSpltBtn.ScreenTip = "Kinect control";
            this.kinectControlSpltBtn.SuperTip = "Kinect control";
            this.kinectControlSpltBtn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnKinectControlClick);
            // 
            // tglBtnDebugView
            // 
            this.tglBtnDebugView.Image = global::KinectPowerPointAddIn.Properties.Resources.Test;
            this.tglBtnDebugView.Label = "Camera output";
            this.tglBtnDebugView.Name = "tglBtnDebugView";
            this.tglBtnDebugView.ShowImage = true;
            this.tglBtnDebugView.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnDebugViewClick);
            // 
            // KinectRibbon
            // 
            this.Name = "KinectRibbon";
            this.RibbonType = "Microsoft.PowerPoint.Presentation";
            this.Tabs.Add(this.mainTb);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.OnKinectRibbonLoad);
            this.mainTb.ResumeLayout(false);
            this.mainTb.PerformLayout();
            this.mainGrp.ResumeLayout(false);
            this.mainGrp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab mainTb;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup mainGrp;
        internal Microsoft.Office.Tools.Ribbon.RibbonSplitButton kinectControlSpltBtn;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton startBeginningBtn;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton tglBtnDebugView;
    }

    partial class ThisRibbonCollection
    {
        internal KinectRibbon KinectRibbon
        {
            get { return this.GetRibbon<KinectRibbon>(); }
        }
    }
}