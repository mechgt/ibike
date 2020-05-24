namespace iBike.DetailPage
{
    partial class iBikeDetail
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            this.panelTop = new System.Windows.Forms.Panel();
            this.txtWindScale = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtAtm = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtTemp = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtWindSpeed = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtRecord = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtClimbing = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtEnergy = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.lblFilename = new System.Windows.Forms.Label();
            this.txtFilename = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.lblWindScale = new System.Windows.Forms.Label();
            this.lblPres = new System.Windows.Forms.Label();
            this.lblTemp = new System.Windows.Forms.Label();
            this.lblWindSpeed = new System.Windows.Forms.Label();
            this.lblRecInt = new System.Windows.Forms.Label();
            this.lblClimbing = new System.Windows.Forms.Label();
            this.lblEnergy = new System.Windows.Forms.Label();
            this.panelMain = new ZoneFiveSoftware.Common.Visuals.Panel();
            this.zedChart = new ZoneFiveSoftware.Common.Visuals.Chart.ZedGraphControl();
            this.ChartBanner = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.ButtonPanel = new ZoneFiveSoftware.Common.Visuals.Panel();
            this.ZoomInButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.ZoomOutButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.ZoomChartButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnDecode = new ZoneFiveSoftware.Common.Visuals.Button();
            this.LeftButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.RightButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.ExtraChartsButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.SaveImageButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.windSpeedDistance = new System.Windows.Forms.ToolStripMenuItem();
            this.windSpeedTime = new System.Windows.Forms.ToolStripMenuItem();
            this.tiltDistance = new System.Windows.Forms.ToolStripMenuItem();
            this.tiltTime = new System.Windows.Forms.ToolStripMenuItem();
            this.importAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelTop.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.ButtonPanel.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.txtWindScale);
            this.panelTop.Controls.Add(this.txtAtm);
            this.panelTop.Controls.Add(this.txtTemp);
            this.panelTop.Controls.Add(this.txtWindSpeed);
            this.panelTop.Controls.Add(this.txtRecord);
            this.panelTop.Controls.Add(this.txtClimbing);
            this.panelTop.Controls.Add(this.txtEnergy);
            this.panelTop.Controls.Add(this.lblFilename);
            this.panelTop.Controls.Add(this.txtFilename);
            this.panelTop.Controls.Add(this.lblWindScale);
            this.panelTop.Controls.Add(this.lblPres);
            this.panelTop.Controls.Add(this.lblTemp);
            this.panelTop.Controls.Add(this.lblWindSpeed);
            this.panelTop.Controls.Add(this.lblRecInt);
            this.panelTop.Controls.Add(this.lblClimbing);
            this.panelTop.Controls.Add(this.lblEnergy);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.MinimumSize = new System.Drawing.Size(0, 110);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(400, 137);
            this.panelTop.TabIndex = 11;
            // 
            // txtWindScale
            // 
            this.txtWindScale.AcceptsReturn = false;
            this.txtWindScale.AcceptsTab = false;
            this.txtWindScale.BackColor = System.Drawing.Color.White;
            this.txtWindScale.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtWindScale.ButtonImage = null;
            this.txtWindScale.Enabled = false;
            this.txtWindScale.Location = new System.Drawing.Point(293, 50);
            this.txtWindScale.MaxLength = 32767;
            this.txtWindScale.Multiline = false;
            this.txtWindScale.Name = "txtWindScale";
            this.txtWindScale.ReadOnly = false;
            this.txtWindScale.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtWindScale.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtWindScale.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtWindScale.Size = new System.Drawing.Size(79, 19);
            this.txtWindScale.TabIndex = 0;
            this.txtWindScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtAtm
            // 
            this.txtAtm.AcceptsReturn = false;
            this.txtAtm.AcceptsTab = false;
            this.txtAtm.BackColor = System.Drawing.Color.White;
            this.txtAtm.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtAtm.ButtonImage = null;
            this.txtAtm.Enabled = false;
            this.txtAtm.Location = new System.Drawing.Point(293, 25);
            this.txtAtm.MaxLength = 32767;
            this.txtAtm.Multiline = false;
            this.txtAtm.Name = "txtAtm";
            this.txtAtm.ReadOnly = false;
            this.txtAtm.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtAtm.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtAtm.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtAtm.Size = new System.Drawing.Size(79, 19);
            this.txtAtm.TabIndex = 0;
            this.txtAtm.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtTemp
            // 
            this.txtTemp.AcceptsReturn = false;
            this.txtTemp.AcceptsTab = false;
            this.txtTemp.BackColor = System.Drawing.Color.White;
            this.txtTemp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtTemp.ButtonImage = null;
            this.txtTemp.Enabled = false;
            this.txtTemp.Location = new System.Drawing.Point(293, 75);
            this.txtTemp.MaxLength = 32767;
            this.txtTemp.Multiline = false;
            this.txtTemp.Name = "txtTemp";
            this.txtTemp.ReadOnly = false;
            this.txtTemp.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtTemp.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtTemp.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTemp.Size = new System.Drawing.Size(79, 19);
            this.txtTemp.TabIndex = 0;
            this.txtTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtWindSpeed
            // 
            this.txtWindSpeed.AcceptsReturn = false;
            this.txtWindSpeed.AcceptsTab = false;
            this.txtWindSpeed.BackColor = System.Drawing.Color.White;
            this.txtWindSpeed.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtWindSpeed.ButtonImage = null;
            this.txtWindSpeed.Enabled = false;
            this.txtWindSpeed.Location = new System.Drawing.Point(92, 100);
            this.txtWindSpeed.MaxLength = 32767;
            this.txtWindSpeed.Multiline = false;
            this.txtWindSpeed.Name = "txtWindSpeed";
            this.txtWindSpeed.ReadOnly = false;
            this.txtWindSpeed.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtWindSpeed.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtWindSpeed.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtWindSpeed.Size = new System.Drawing.Size(79, 19);
            this.txtWindSpeed.TabIndex = 0;
            this.txtWindSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtRecord
            // 
            this.txtRecord.AcceptsReturn = false;
            this.txtRecord.AcceptsTab = false;
            this.txtRecord.BackColor = System.Drawing.Color.White;
            this.txtRecord.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtRecord.ButtonImage = null;
            this.txtRecord.Enabled = false;
            this.txtRecord.Location = new System.Drawing.Point(92, 75);
            this.txtRecord.MaxLength = 32767;
            this.txtRecord.Multiline = false;
            this.txtRecord.Name = "txtRecord";
            this.txtRecord.ReadOnly = false;
            this.txtRecord.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtRecord.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtRecord.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtRecord.Size = new System.Drawing.Size(79, 19);
            this.txtRecord.TabIndex = 0;
            this.txtRecord.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtClimbing
            // 
            this.txtClimbing.AcceptsReturn = false;
            this.txtClimbing.AcceptsTab = false;
            this.txtClimbing.BackColor = System.Drawing.Color.White;
            this.txtClimbing.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtClimbing.ButtonImage = null;
            this.txtClimbing.Enabled = false;
            this.txtClimbing.Location = new System.Drawing.Point(92, 50);
            this.txtClimbing.MaxLength = 32767;
            this.txtClimbing.Multiline = false;
            this.txtClimbing.Name = "txtClimbing";
            this.txtClimbing.ReadOnly = false;
            this.txtClimbing.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtClimbing.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtClimbing.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtClimbing.Size = new System.Drawing.Size(79, 19);
            this.txtClimbing.TabIndex = 0;
            this.txtClimbing.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtEnergy
            // 
            this.txtEnergy.AcceptsReturn = false;
            this.txtEnergy.AcceptsTab = false;
            this.txtEnergy.BackColor = System.Drawing.Color.White;
            this.txtEnergy.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtEnergy.ButtonImage = null;
            this.txtEnergy.Enabled = false;
            this.txtEnergy.Location = new System.Drawing.Point(92, 25);
            this.txtEnergy.MaxLength = 32767;
            this.txtEnergy.Multiline = false;
            this.txtEnergy.Name = "txtEnergy";
            this.txtEnergy.ReadOnly = false;
            this.txtEnergy.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtEnergy.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtEnergy.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEnergy.Size = new System.Drawing.Size(79, 19);
            this.txtEnergy.TabIndex = 0;
            this.txtEnergy.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // lblFilename
            // 
            this.lblFilename.AutoSize = true;
            this.lblFilename.Location = new System.Drawing.Point(0, 3);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(52, 13);
            this.lblFilename.TabIndex = 1;
            this.lblFilename.Text = "Filename:";
            // 
            // txtFilename
            // 
            this.txtFilename.AcceptsReturn = false;
            this.txtFilename.AcceptsTab = false;
            this.txtFilename.BackColor = System.Drawing.Color.White;
            this.txtFilename.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtFilename.ButtonImage = null;
            this.txtFilename.Enabled = false;
            this.txtFilename.Location = new System.Drawing.Point(92, 0);
            this.txtFilename.MaxLength = 32767;
            this.txtFilename.Multiline = false;
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.ReadOnly = false;
            this.txtFilename.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtFilename.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtFilename.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFilename.Size = new System.Drawing.Size(280, 19);
            this.txtFilename.TabIndex = 0;
            this.txtFilename.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // lblWindScale
            // 
            this.lblWindScale.AutoSize = true;
            this.lblWindScale.Location = new System.Drawing.Point(201, 53);
            this.lblWindScale.Name = "lblWindScale";
            this.lblWindScale.Size = new System.Drawing.Size(73, 13);
            this.lblWindScale.TabIndex = 1;
            this.lblWindScale.Text = "Wind Scaling:";
            // 
            // lblPres
            // 
            this.lblPres.AutoSize = true;
            this.lblPres.Location = new System.Drawing.Point(201, 28);
            this.lblPres.Name = "lblPres";
            this.lblPres.Size = new System.Drawing.Size(87, 13);
            this.lblPres.TabIndex = 1;
            this.lblPres.Text = "Atm Pres. (mbar):";
            // 
            // lblTemp
            // 
            this.lblTemp.AutoSize = true;
            this.lblTemp.Location = new System.Drawing.Point(201, 78);
            this.lblTemp.Name = "lblTemp";
            this.lblTemp.Size = new System.Drawing.Size(89, 13);
            this.lblTemp.TabIndex = 1;
            this.lblTemp.Text = "Temperature (°F):";
            // 
            // lblWindSpeed
            // 
            this.lblWindSpeed.AutoSize = true;
            this.lblWindSpeed.Location = new System.Drawing.Point(0, 103);
            this.lblWindSpeed.Name = "lblWindSpeed";
            this.lblWindSpeed.Size = new System.Drawing.Size(89, 13);
            this.lblWindSpeed.TabIndex = 1;
            this.lblWindSpeed.Text = "Avg. Wind (mph):";
            // 
            // lblRecInt
            // 
            this.lblRecInt.AutoSize = true;
            this.lblRecInt.Location = new System.Drawing.Point(0, 78);
            this.lblRecInt.Name = "lblRecInt";
            this.lblRecInt.Size = new System.Drawing.Size(83, 13);
            this.lblRecInt.TabIndex = 1;
            this.lblRecInt.Text = "Record Interval:";
            // 
            // lblClimbing
            // 
            this.lblClimbing.AutoSize = true;
            this.lblClimbing.Location = new System.Drawing.Point(0, 53);
            this.lblClimbing.Name = "lblClimbing";
            this.lblClimbing.Size = new System.Drawing.Size(49, 13);
            this.lblClimbing.TabIndex = 1;
            this.lblClimbing.Text = "Climbing:";
            // 
            // lblEnergy
            // 
            this.lblEnergy.AutoSize = true;
            this.lblEnergy.Location = new System.Drawing.Point(0, 28);
            this.lblEnergy.Name = "lblEnergy";
            this.lblEnergy.Size = new System.Drawing.Size(63, 13);
            this.lblEnergy.TabIndex = 1;
            this.lblEnergy.Text = "Energy (kJ):";
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.Transparent;
            this.panelMain.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.Round;
            this.panelMain.BorderColor = System.Drawing.Color.Gray;
            this.panelMain.Controls.Add(this.zedChart);
            this.panelMain.Controls.Add(this.ChartBanner);
            this.panelMain.Controls.Add(this.ButtonPanel);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.HeadingBackColor = System.Drawing.Color.LightBlue;
            this.panelMain.HeadingFont = null;
            this.panelMain.HeadingLeftMargin = 0;
            this.panelMain.HeadingText = null;
            this.panelMain.HeadingTextColor = System.Drawing.Color.Black;
            this.panelMain.HeadingTopMargin = 0;
            this.panelMain.Location = new System.Drawing.Point(0, 137);
            this.panelMain.Margin = new System.Windows.Forms.Padding(0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(400, 263);
            this.panelMain.TabIndex = 12;
            // 
            // zedChart
            // 
            this.zedChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedChart.FitToSelection = true;
            this.zedChart.IsShowPointValues = true;
            this.zedChart.Location = new System.Drawing.Point(0, 24);
            this.zedChart.Name = "zedChart";
            this.zedChart.PointValueFormat = "#.##";
            this.zedChart.ScrollGrace = 0D;
            this.zedChart.ScrollMaxX = 0D;
            this.zedChart.ScrollMaxY = 0D;
            this.zedChart.ScrollMaxY2 = 0D;
            this.zedChart.ScrollMinX = 0D;
            this.zedChart.ScrollMinY = 0D;
            this.zedChart.ScrollMinY2 = 0D;
            this.zedChart.Size = new System.Drawing.Size(400, 239);
            this.zedChart.TabIndex = 8;
            // 
            // ChartBanner
            // 
            this.ChartBanner.BackColor = System.Drawing.Color.Transparent;
            this.ChartBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.ChartBanner.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ChartBanner.HasMenuButton = true;
            this.ChartBanner.Location = new System.Drawing.Point(0, 0);
            this.ChartBanner.Name = "ChartBanner";
            this.ChartBanner.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ChartBanner.Size = new System.Drawing.Size(400, 24);
            this.ChartBanner.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header2;
            this.ChartBanner.TabIndex = 7;
            this.ChartBanner.Text = "Detail Pane Chart";
            this.ChartBanner.UseStyleFont = true;
            this.ChartBanner.MenuClicked += new System.EventHandler(this.ChartBanner_MenuClicked);
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonPanel.BackColor = System.Drawing.Color.Transparent;
            this.ButtonPanel.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.Square;
            this.ButtonPanel.BorderColor = System.Drawing.Color.Gray;
            this.ButtonPanel.Controls.Add(this.ZoomInButton);
            this.ButtonPanel.Controls.Add(this.ZoomOutButton);
            this.ButtonPanel.Controls.Add(this.ZoomChartButton);
            this.ButtonPanel.Controls.Add(this.btnDecode);
            this.ButtonPanel.Controls.Add(this.LeftButton);
            this.ButtonPanel.Controls.Add(this.RightButton);
            this.ButtonPanel.Controls.Add(this.ExtraChartsButton);
            this.ButtonPanel.Controls.Add(this.SaveImageButton);
            this.ButtonPanel.HeadingBackColor = System.Drawing.Color.LightBlue;
            this.ButtonPanel.HeadingFont = null;
            this.ButtonPanel.HeadingLeftMargin = 0;
            this.ButtonPanel.HeadingText = null;
            this.ButtonPanel.HeadingTextColor = System.Drawing.Color.Black;
            this.ButtonPanel.HeadingTopMargin = 0;
            this.ButtonPanel.Location = new System.Drawing.Point(0, 23);
            this.ButtonPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(400, 24);
            this.ButtonPanel.TabIndex = 6;
            // 
            // ZoomInButton
            // 
            this.ZoomInButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ZoomInButton.BackColor = System.Drawing.Color.Transparent;
            this.ZoomInButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.ZoomInButton.CenterImage = global::iBike.Resources.Images.ZoomIn;
            this.ZoomInButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ZoomInButton.HyperlinkStyle = false;
            this.ZoomInButton.ImageMargin = 2;
            this.ZoomInButton.LeftImage = null;
            this.ZoomInButton.Location = new System.Drawing.Point(374, 0);
            this.ZoomInButton.Margin = new System.Windows.Forms.Padding(0);
            this.ZoomInButton.Name = "ZoomInButton";
            this.ZoomInButton.PushStyle = true;
            this.ZoomInButton.RightImage = null;
            this.ZoomInButton.Size = new System.Drawing.Size(24, 24);
            this.ZoomInButton.TabIndex = 0;
            this.ZoomInButton.Tag = "In";
            this.ZoomInButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.ZoomInButton.TextLeftMargin = 2;
            this.ZoomInButton.TextRightMargin = 2;
            this.ZoomInButton.Click += new System.EventHandler(this.ZoomButton_Click);
            // 
            // ZoomOutButton
            // 
            this.ZoomOutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ZoomOutButton.BackColor = System.Drawing.Color.Transparent;
            this.ZoomOutButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.ZoomOutButton.CenterImage = global::iBike.Resources.Images.ZoomOut;
            this.ZoomOutButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ZoomOutButton.HyperlinkStyle = false;
            this.ZoomOutButton.ImageMargin = 2;
            this.ZoomOutButton.LeftImage = null;
            this.ZoomOutButton.Location = new System.Drawing.Point(350, 0);
            this.ZoomOutButton.Margin = new System.Windows.Forms.Padding(0);
            this.ZoomOutButton.Name = "ZoomOutButton";
            this.ZoomOutButton.PushStyle = true;
            this.ZoomOutButton.RightImage = null;
            this.ZoomOutButton.Size = new System.Drawing.Size(24, 24);
            this.ZoomOutButton.TabIndex = 0;
            this.ZoomOutButton.Tag = "Out";
            this.ZoomOutButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.ZoomOutButton.TextLeftMargin = 2;
            this.ZoomOutButton.TextRightMargin = 2;
            this.ZoomOutButton.Click += new System.EventHandler(this.ZoomButton_Click);
            // 
            // ZoomChartButton
            // 
            this.ZoomChartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ZoomChartButton.BackColor = System.Drawing.Color.Transparent;
            this.ZoomChartButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.ZoomChartButton.CenterImage = global::iBike.Resources.Images.ZoomFit;
            this.ZoomChartButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ZoomChartButton.HyperlinkStyle = false;
            this.ZoomChartButton.ImageMargin = 2;
            this.ZoomChartButton.LeftImage = null;
            this.ZoomChartButton.Location = new System.Drawing.Point(326, 0);
            this.ZoomChartButton.Margin = new System.Windows.Forms.Padding(0);
            this.ZoomChartButton.Name = "ZoomChartButton";
            this.ZoomChartButton.PushStyle = true;
            this.ZoomChartButton.RightImage = null;
            this.ZoomChartButton.Size = new System.Drawing.Size(24, 24);
            this.ZoomChartButton.TabIndex = 0;
            this.ZoomChartButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.ZoomChartButton.TextLeftMargin = 2;
            this.ZoomChartButton.TextRightMargin = 2;
            this.ZoomChartButton.Click += new System.EventHandler(this.ZoomChartButton_Click);
            // 
            // btnDecode
            // 
            this.btnDecode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDecode.BackColor = System.Drawing.Color.Transparent;
            this.btnDecode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnDecode.CenterImage = null;
            this.btnDecode.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDecode.HyperlinkStyle = false;
            this.btnDecode.ImageMargin = 2;
            this.btnDecode.LeftImage = null;
            this.btnDecode.Location = new System.Drawing.Point(206, 0);
            this.btnDecode.Margin = new System.Windows.Forms.Padding(0);
            this.btnDecode.Name = "btnDecode";
            this.btnDecode.PushStyle = true;
            this.btnDecode.RightImage = null;
            this.btnDecode.Size = new System.Drawing.Size(24, 24);
            this.btnDecode.TabIndex = 0;
            this.btnDecode.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnDecode.TextLeftMargin = 2;
            this.btnDecode.TextRightMargin = 2;
            // 
            // LeftButton
            // 
            this.LeftButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LeftButton.BackColor = System.Drawing.Color.Transparent;
            this.LeftButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.LeftButton.CenterImage = null;
            this.LeftButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.LeftButton.HyperlinkStyle = false;
            this.LeftButton.ImageMargin = 2;
            this.LeftButton.LeftImage = null;
            this.LeftButton.Location = new System.Drawing.Point(230, 0);
            this.LeftButton.Margin = new System.Windows.Forms.Padding(0);
            this.LeftButton.Name = "LeftButton";
            this.LeftButton.PushStyle = true;
            this.LeftButton.RightImage = null;
            this.LeftButton.Size = new System.Drawing.Size(24, 24);
            this.LeftButton.TabIndex = 0;
            this.LeftButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.LeftButton.TextLeftMargin = 2;
            this.LeftButton.TextRightMargin = 2;
            this.LeftButton.Click += new System.EventHandler(this.LeftButton_Click);
            // 
            // RightButton
            // 
            this.RightButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RightButton.BackColor = System.Drawing.Color.Transparent;
            this.RightButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.RightButton.CenterImage = null;
            this.RightButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.RightButton.HyperlinkStyle = false;
            this.RightButton.ImageMargin = 2;
            this.RightButton.LeftImage = null;
            this.RightButton.Location = new System.Drawing.Point(254, 0);
            this.RightButton.Margin = new System.Windows.Forms.Padding(0);
            this.RightButton.Name = "RightButton";
            this.RightButton.PushStyle = true;
            this.RightButton.RightImage = null;
            this.RightButton.Size = new System.Drawing.Size(24, 24);
            this.RightButton.TabIndex = 0;
            this.RightButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.RightButton.TextLeftMargin = 2;
            this.RightButton.TextRightMargin = 2;
            this.RightButton.Click += new System.EventHandler(this.RightButton_Click);
            // 
            // ExtraChartsButton
            // 
            this.ExtraChartsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExtraChartsButton.BackColor = System.Drawing.Color.Transparent;
            this.ExtraChartsButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.ExtraChartsButton.CenterImage = null;
            this.ExtraChartsButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ExtraChartsButton.HyperlinkStyle = false;
            this.ExtraChartsButton.ImageMargin = 2;
            this.ExtraChartsButton.LeftImage = null;
            this.ExtraChartsButton.Location = new System.Drawing.Point(278, 0);
            this.ExtraChartsButton.Margin = new System.Windows.Forms.Padding(0);
            this.ExtraChartsButton.Name = "ExtraChartsButton";
            this.ExtraChartsButton.PushStyle = true;
            this.ExtraChartsButton.RightImage = null;
            this.ExtraChartsButton.Size = new System.Drawing.Size(24, 24);
            this.ExtraChartsButton.TabIndex = 0;
            this.ExtraChartsButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.ExtraChartsButton.TextLeftMargin = 2;
            this.ExtraChartsButton.TextRightMargin = 2;
            this.ExtraChartsButton.Visible = false;
            this.ExtraChartsButton.Click += new System.EventHandler(this.ExtraChartsButton_Click);
            // 
            // SaveImageButton
            // 
            this.SaveImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveImageButton.BackColor = System.Drawing.Color.Transparent;
            this.SaveImageButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.SaveImageButton.CenterImage = null;
            this.SaveImageButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.SaveImageButton.Enabled = false;
            this.SaveImageButton.HyperlinkStyle = false;
            this.SaveImageButton.ImageMargin = 2;
            this.SaveImageButton.LeftImage = null;
            this.SaveImageButton.Location = new System.Drawing.Point(302, 0);
            this.SaveImageButton.Margin = new System.Windows.Forms.Padding(0);
            this.SaveImageButton.Name = "SaveImageButton";
            this.SaveImageButton.PushStyle = true;
            this.SaveImageButton.RightImage = null;
            this.SaveImageButton.Size = new System.Drawing.Size(24, 24);
            this.SaveImageButton.TabIndex = 0;
            this.SaveImageButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.SaveImageButton.TextLeftMargin = 2;
            this.SaveImageButton.TextRightMargin = 2;
            this.SaveImageButton.Click += new System.EventHandler(this.SaveImageButton_Click);
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windSpeedDistance,
            this.windSpeedTime,
            this.tiltDistance,
            this.tiltTime,
            this.importAnalysisToolStripMenuItem});
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(194, 114);
            // 
            // windSpeedDistance
            // 
            this.windSpeedDistance.Name = "windSpeedDistance";
            this.windSpeedDistance.Size = new System.Drawing.Size(193, 22);
            this.windSpeedDistance.Text = "Wind Speed / Distance";
            this.windSpeedDistance.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // windSpeedTime
            // 
            this.windSpeedTime.Checked = true;
            this.windSpeedTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.windSpeedTime.Name = "windSpeedTime";
            this.windSpeedTime.Size = new System.Drawing.Size(193, 22);
            this.windSpeedTime.Text = "Wind Speed / Time";
            this.windSpeedTime.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // tiltDistance
            // 
            this.tiltDistance.Name = "tiltDistance";
            this.tiltDistance.Size = new System.Drawing.Size(193, 22);
            this.tiltDistance.Text = "Tilt / Distance";
            this.tiltDistance.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // tiltTime
            // 
            this.tiltTime.Name = "tiltTime";
            this.tiltTime.Size = new System.Drawing.Size(193, 22);
            this.tiltTime.Text = "Tilt / Time";
            this.tiltTime.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // importAnalysisToolStripMenuItem
            // 
            this.importAnalysisToolStripMenuItem.Name = "importAnalysisToolStripMenuItem";
            this.importAnalysisToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.importAnalysisToolStripMenuItem.Text = "Import Analysis";
            this.importAnalysisToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // iBikeDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelTop);
            this.MinimumSize = new System.Drawing.Size(400, 0);
            this.Name = "iBikeDetail";
            this.Size = new System.Drawing.Size(400, 400);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.ButtonPanel.ResumeLayout(false);
            this.MenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblClimbing;
        private System.Windows.Forms.Label lblEnergy;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtClimbing;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtEnergy;
        private System.Windows.Forms.Label lblFilename;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtFilename;
        private ZoneFiveSoftware.Common.Visuals.Panel panelMain;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner ChartBanner;
        private ZoneFiveSoftware.Common.Visuals.Panel ButtonPanel;
        private ZoneFiveSoftware.Common.Visuals.Button ZoomInButton;
        private ZoneFiveSoftware.Common.Visuals.Button ZoomOutButton;
        private ZoneFiveSoftware.Common.Visuals.Button ZoomChartButton;
        private ZoneFiveSoftware.Common.Visuals.Button ExtraChartsButton;
        private ZoneFiveSoftware.Common.Visuals.Button SaveImageButton;
        private System.Windows.Forms.ContextMenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem windSpeedTime;
        private System.Windows.Forms.ToolStripMenuItem importAnalysisToolStripMenuItem;
        private ZoneFiveSoftware.Common.Visuals.Button LeftButton;
        private ZoneFiveSoftware.Common.Visuals.Button RightButton;
        private System.Windows.Forms.Label lblWindScale;
        private System.Windows.Forms.Label lblPres;
        private System.Windows.Forms.Label lblTemp;
        private System.Windows.Forms.Label lblRecInt;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtWindScale;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtAtm;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtTemp;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtRecord;
        private System.Windows.Forms.Label lblWindSpeed;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtWindSpeed;
        private System.Windows.Forms.ToolStripMenuItem tiltTime;
        private System.Windows.Forms.ToolStripMenuItem windSpeedDistance;
        private System.Windows.Forms.ToolStripMenuItem tiltDistance;
        private ZoneFiveSoftware.Common.Visuals.Button btnDecode;
        private ZoneFiveSoftware.Common.Visuals.Chart.ZedGraphControl zedChart;

    }
}
