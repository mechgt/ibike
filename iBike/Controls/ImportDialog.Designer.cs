namespace iBike.Controls
{
    partial class ImportDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bnrTitle = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.chkElevation = new System.Windows.Forms.CheckBox();
            this.chkDistance = new System.Windows.Forms.CheckBox();
            this.chkPower = new System.Windows.Forms.CheckBox();
            this.chkWind = new System.Windows.Forms.CheckBox();
            this.chkCadence = new System.Windows.Forms.CheckBox();
            this.chkTilt = new System.Windows.Forms.CheckBox();
            this.grpUpdate = new System.Windows.Forms.GroupBox();
            this.chkCalories = new System.Windows.Forms.CheckBox();
            this.lblCalories = new System.Windows.Forms.Label();
            this.chkName = new System.Windows.Forms.CheckBox();
            this.lblName = new System.Windows.Forms.Label();
            this.chkTemp = new System.Windows.Forms.CheckBox();
            this.lblTemp = new System.Windows.Forms.Label();
            this.lblGPS = new System.Windows.Forms.Label();
            this.lblTilt = new System.Windows.Forms.Label();
            this.lblWind = new System.Windows.Forms.Label();
            this.lblCadence = new System.Windows.Forms.Label();
            this.lblDistance = new System.Windows.Forms.Label();
            this.lblHR = new System.Windows.Forms.Label();
            this.lblElevation = new System.Windows.Forms.Label();
            this.lblPower = new System.Windows.Forms.Label();
            this.chkGPS = new System.Windows.Forms.CheckBox();
            this.chkHR = new System.Windows.Forms.CheckBox();
            this.lblDistTime = new System.Windows.Forms.Label();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.btnCancel = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnFinish = new ZoneFiveSoftware.Common.Visuals.Button();
            this.lblRecording = new System.Windows.Forms.Label();
            this.lblPoints = new System.Windows.Forms.Label();
            this.lblAlign = new System.Windows.Forms.Label();
            this.grpUpdate.SuspendLayout();
            this.SuspendLayout();
            // 
            // bnrTitle
            // 
            this.bnrTitle.BackColor = System.Drawing.Color.Transparent;
            this.bnrTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.bnrTitle.HasMenuButton = false;
            this.bnrTitle.Location = new System.Drawing.Point(0, 0);
            this.bnrTitle.Name = "bnrTitle";
            this.bnrTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bnrTitle.Size = new System.Drawing.Size(386, 27);
            this.bnrTitle.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header2;
            this.bnrTitle.TabIndex = 6;
            this.bnrTitle.UseStyleFont = true;
            // 
            // chkElevation
            // 
            this.chkElevation.AutoSize = true;
            this.chkElevation.Location = new System.Drawing.Point(96, 41);
            this.chkElevation.Name = "chkElevation";
            this.chkElevation.Size = new System.Drawing.Size(70, 17);
            this.chkElevation.TabIndex = 0;
            this.chkElevation.Text = "Elevation";
            this.chkElevation.UseVisualStyleBackColor = true;
            // 
            // chkDistance
            // 
            this.chkDistance.AutoSize = true;
            this.chkDistance.Location = new System.Drawing.Point(96, 65);
            this.chkDistance.Name = "chkDistance";
            this.chkDistance.Size = new System.Drawing.Size(111, 17);
            this.chkDistance.TabIndex = 1;
            this.chkDistance.Text = "Speed + Distance";
            this.chkDistance.UseVisualStyleBackColor = true;
            // 
            // chkPower
            // 
            this.chkPower.AutoSize = true;
            this.chkPower.Location = new System.Drawing.Point(96, 19);
            this.chkPower.Name = "chkPower";
            this.chkPower.Size = new System.Drawing.Size(56, 17);
            this.chkPower.TabIndex = 1;
            this.chkPower.Text = "Power";
            this.chkPower.UseVisualStyleBackColor = true;
            // 
            // chkWind
            // 
            this.chkWind.AutoSize = true;
            this.chkWind.Location = new System.Drawing.Point(96, 88);
            this.chkWind.Name = "chkWind";
            this.chkWind.Size = new System.Drawing.Size(51, 17);
            this.chkWind.TabIndex = 1;
            this.chkWind.Text = "Wind";
            this.chkWind.UseVisualStyleBackColor = true;
            // 
            // chkCadence
            // 
            this.chkCadence.AutoSize = true;
            this.chkCadence.Location = new System.Drawing.Point(282, 42);
            this.chkCadence.Name = "chkCadence";
            this.chkCadence.Size = new System.Drawing.Size(69, 17);
            this.chkCadence.TabIndex = 1;
            this.chkCadence.Text = "Cadence";
            this.chkCadence.UseVisualStyleBackColor = true;
            // 
            // chkTilt
            // 
            this.chkTilt.AutoSize = true;
            this.chkTilt.Location = new System.Drawing.Point(282, 65);
            this.chkTilt.Name = "chkTilt";
            this.chkTilt.Size = new System.Drawing.Size(40, 17);
            this.chkTilt.TabIndex = 1;
            this.chkTilt.Text = "Tilt";
            this.chkTilt.UseVisualStyleBackColor = true;
            // 
            // grpUpdate
            // 
            this.grpUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpUpdate.Controls.Add(this.chkElevation);
            this.grpUpdate.Controls.Add(this.chkDistance);
            this.grpUpdate.Controls.Add(this.chkPower);
            this.grpUpdate.Controls.Add(this.chkCalories);
            this.grpUpdate.Controls.Add(this.lblCalories);
            this.grpUpdate.Controls.Add(this.chkName);
            this.grpUpdate.Controls.Add(this.lblName);
            this.grpUpdate.Controls.Add(this.chkTemp);
            this.grpUpdate.Controls.Add(this.lblTemp);
            this.grpUpdate.Controls.Add(this.lblGPS);
            this.grpUpdate.Controls.Add(this.lblTilt);
            this.grpUpdate.Controls.Add(this.lblWind);
            this.grpUpdate.Controls.Add(this.lblCadence);
            this.grpUpdate.Controls.Add(this.lblDistance);
            this.grpUpdate.Controls.Add(this.lblHR);
            this.grpUpdate.Controls.Add(this.lblElevation);
            this.grpUpdate.Controls.Add(this.lblPower);
            this.grpUpdate.Controls.Add(this.chkGPS);
            this.grpUpdate.Controls.Add(this.chkTilt);
            this.grpUpdate.Controls.Add(this.chkWind);
            this.grpUpdate.Controls.Add(this.chkCadence);
            this.grpUpdate.Controls.Add(this.chkHR);
            this.grpUpdate.Location = new System.Drawing.Point(0, 80);
            this.grpUpdate.Name = "grpUpdate";
            this.grpUpdate.Size = new System.Drawing.Size(387, 159);
            this.grpUpdate.TabIndex = 9;
            this.grpUpdate.TabStop = false;
            this.grpUpdate.Text = "Add/update data:";
            // 
            // chkCalories
            // 
            this.chkCalories.AutoSize = true;
            this.chkCalories.Location = new System.Drawing.Point(96, 134);
            this.chkCalories.Name = "chkCalories";
            this.chkCalories.Size = new System.Drawing.Size(63, 17);
            this.chkCalories.TabIndex = 1;
            this.chkCalories.Text = "Calories";
            this.chkCalories.UseVisualStyleBackColor = true;
            // 
            // lblCalories
            // 
            this.lblCalories.AutoEllipsis = true;
            this.lblCalories.Location = new System.Drawing.Point(10, 135);
            this.lblCalories.Name = "lblCalories";
            this.lblCalories.Size = new System.Drawing.Size(78, 16);
            this.lblCalories.TabIndex = 7;
            this.lblCalories.Text = "742 calories";
            this.lblCalories.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkName
            // 
            this.chkName.AutoSize = true;
            this.chkName.Location = new System.Drawing.Point(282, 111);
            this.chkName.Name = "chkName";
            this.chkName.Size = new System.Drawing.Size(54, 17);
            this.chkName.TabIndex = 1;
            this.chkName.Text = "Name";
            this.chkName.UseVisualStyleBackColor = true;
            // 
            // lblName
            // 
            this.lblName.AutoEllipsis = true;
            this.lblName.Location = new System.Drawing.Point(166, 112);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(110, 16);
            this.lblName.TabIndex = 7;
            this.lblName.Text = "Dans Macabre";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkTemp
            // 
            this.chkTemp.AutoSize = true;
            this.chkTemp.Location = new System.Drawing.Point(282, 88);
            this.chkTemp.Name = "chkTemp";
            this.chkTemp.Size = new System.Drawing.Size(86, 17);
            this.chkTemp.TabIndex = 1;
            this.chkTemp.Text = "Temperature";
            this.chkTemp.UseVisualStyleBackColor = true;
            // 
            // lblTemp
            // 
            this.lblTemp.Location = new System.Drawing.Point(209, 89);
            this.lblTemp.Name = "lblTemp";
            this.lblTemp.Size = new System.Drawing.Size(67, 13);
            this.lblTemp.TabIndex = 7;
            this.lblTemp.Text = "79 °F";
            this.lblTemp.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblGPS
            // 
            this.lblGPS.Location = new System.Drawing.Point(10, 112);
            this.lblGPS.Name = "lblGPS";
            this.lblGPS.Size = new System.Drawing.Size(78, 13);
            this.lblGPS.TabIndex = 7;
            this.lblGPS.Text = "51.33 miles";
            this.lblGPS.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTilt
            // 
            this.lblTilt.Location = new System.Drawing.Point(209, 66);
            this.lblTilt.Name = "lblTilt";
            this.lblTilt.Size = new System.Drawing.Size(67, 13);
            this.lblTilt.TabIndex = 7;
            this.lblTilt.Text = "0.2 %";
            this.lblTilt.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblWind
            // 
            this.lblWind.Location = new System.Drawing.Point(10, 89);
            this.lblWind.Name = "lblWind";
            this.lblWind.Size = new System.Drawing.Size(78, 13);
            this.lblWind.TabIndex = 7;
            this.lblWind.Text = "18 mph";
            this.lblWind.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCadence
            // 
            this.lblCadence.Location = new System.Drawing.Point(209, 43);
            this.lblCadence.Name = "lblCadence";
            this.lblCadence.Size = new System.Drawing.Size(67, 13);
            this.lblCadence.TabIndex = 7;
            this.lblCadence.Text = "74 rpm";
            this.lblCadence.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblDistance
            // 
            this.lblDistance.Location = new System.Drawing.Point(10, 66);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(78, 13);
            this.lblDistance.TabIndex = 7;
            this.lblDistance.Text = "21 mile";
            this.lblDistance.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblHR
            // 
            this.lblHR.Location = new System.Drawing.Point(209, 20);
            this.lblHR.Name = "lblHR";
            this.lblHR.Size = new System.Drawing.Size(67, 13);
            this.lblHR.TabIndex = 7;
            this.lblHR.Text = "175 bpm";
            this.lblHR.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblElevation
            // 
            this.lblElevation.Location = new System.Drawing.Point(10, 43);
            this.lblElevation.Name = "lblElevation";
            this.lblElevation.Size = new System.Drawing.Size(78, 13);
            this.lblElevation.TabIndex = 7;
            this.lblElevation.Text = "3489 m";
            this.lblElevation.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPower
            // 
            this.lblPower.Location = new System.Drawing.Point(10, 20);
            this.lblPower.Name = "lblPower";
            this.lblPower.Size = new System.Drawing.Size(78, 13);
            this.lblPower.TabIndex = 7;
            this.lblPower.Text = "245 watts";
            this.lblPower.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkGPS
            // 
            this.chkGPS.AutoSize = true;
            this.chkGPS.Location = new System.Drawing.Point(96, 111);
            this.chkGPS.Name = "chkGPS";
            this.chkGPS.Size = new System.Drawing.Size(48, 17);
            this.chkGPS.TabIndex = 1;
            this.chkGPS.Text = "GPS";
            this.chkGPS.UseVisualStyleBackColor = true;
            // 
            // chkHR
            // 
            this.chkHR.AutoSize = true;
            this.chkHR.Location = new System.Drawing.Point(282, 19);
            this.chkHR.Name = "chkHR";
            this.chkHR.Size = new System.Drawing.Size(78, 17);
            this.chkHR.TabIndex = 1;
            this.chkHR.Text = "Heart Rate";
            this.chkHR.UseVisualStyleBackColor = true;
            // 
            // lblDistTime
            // 
            this.lblDistTime.AutoSize = true;
            this.lblDistTime.Location = new System.Drawing.Point(38, 58);
            this.lblDistTime.Name = "lblDistTime";
            this.lblDistTime.Size = new System.Drawing.Size(97, 13);
            this.lblDistTime.TabIndex = 7;
            this.lblDistTime.Text = "51.33 mi. / 2:58:20";
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartTime.Location = new System.Drawing.Point(38, 39);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(136, 13);
            this.lblStartTime.TabIndex = 8;
            this.lblStartTime.Text = "Sun 7/11/2010 - 10:04 AM";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnCancel.CenterImage = null;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.HyperlinkStyle = false;
            this.btnCancel.ImageMargin = 2;
            this.btnCancel.LeftImage = null;
            this.btnCancel.Location = new System.Drawing.Point(295, 245);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PushStyle = true;
            this.btnCancel.RightImage = null;
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnCancel.TextLeftMargin = 2;
            this.btnCancel.TextRightMargin = 2;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinish.BackColor = System.Drawing.Color.Transparent;
            this.btnFinish.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnFinish.CenterImage = null;
            this.btnFinish.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnFinish.HyperlinkStyle = false;
            this.btnFinish.ImageMargin = 2;
            this.btnFinish.LeftImage = null;
            this.btnFinish.Location = new System.Drawing.Point(214, 245);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.PushStyle = true;
            this.btnFinish.RightImage = null;
            this.btnFinish.Size = new System.Drawing.Size(75, 23);
            this.btnFinish.TabIndex = 10;
            this.btnFinish.Text = "Finish";
            this.btnFinish.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnFinish.TextLeftMargin = 2;
            this.btnFinish.TextRightMargin = 2;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // lblRecording
            // 
            this.lblRecording.AutoSize = true;
            this.lblRecording.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecording.Location = new System.Drawing.Point(232, 39);
            this.lblRecording.Name = "lblRecording";
            this.lblRecording.Size = new System.Drawing.Size(80, 13);
            this.lblRecording.TabIndex = 8;
            this.lblRecording.Text = "1 sec recording";
            // 
            // lblPoints
            // 
            this.lblPoints.AutoSize = true;
            this.lblPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoints.Location = new System.Drawing.Point(232, 58);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(91, 13);
            this.lblPoints.TabIndex = 8;
            this.lblPoints.Text = "4027 points/track";
            // 
            // lblAlign
            // 
            this.lblAlign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAlign.AutoSize = true;
            this.lblAlign.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlign.Location = new System.Drawing.Point(10, 249);
            this.lblAlign.Name = "lblAlign";
            this.lblAlign.Size = new System.Drawing.Size(138, 13);
            this.lblAlign.TabIndex = 8;
            this.lblAlign.Text = "Aligned to: Speed/Distance";
            // 
            // ImportDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 275);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.bnrTitle);
            this.Controls.Add(this.grpUpdate);
            this.Controls.Add(this.lblDistTime);
            this.Controls.Add(this.lblAlign);
            this.Controls.Add(this.lblPoints);
            this.Controls.Add(this.lblRecording);
            this.Controls.Add(this.lblStartTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ImportDialog";
            this.grpUpdate.ResumeLayout(false);
            this.grpUpdate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZoneFiveSoftware.Common.Visuals.ActionBanner bnrTitle;
        private System.Windows.Forms.GroupBox grpUpdate;
        private System.Windows.Forms.Label lblDistTime;
        private System.Windows.Forms.Label lblStartTime;
        private ZoneFiveSoftware.Common.Visuals.Button btnCancel;
        private ZoneFiveSoftware.Common.Visuals.Button btnFinish;
        internal System.Windows.Forms.CheckBox chkElevation;
        internal System.Windows.Forms.CheckBox chkDistance;
        internal System.Windows.Forms.CheckBox chkPower;
        internal System.Windows.Forms.CheckBox chkWind;
        internal System.Windows.Forms.CheckBox chkCadence;
        internal System.Windows.Forms.CheckBox chkTilt;
        internal System.Windows.Forms.CheckBox chkHR;
        internal System.Windows.Forms.CheckBox chkTemp;
        private System.Windows.Forms.Label lblPower;
        private System.Windows.Forms.Label lblWind;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.Label lblElevation;
        private System.Windows.Forms.Label lblTemp;
        private System.Windows.Forms.Label lblTilt;
        private System.Windows.Forms.Label lblCadence;
        private System.Windows.Forms.Label lblHR;
        private System.Windows.Forms.Label lblRecording;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.Label lblAlign;
        internal System.Windows.Forms.CheckBox chkName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblGPS;
        internal System.Windows.Forms.CheckBox chkGPS;
        internal System.Windows.Forms.CheckBox chkCalories;
        private System.Windows.Forms.Label lblCalories;
    }
}