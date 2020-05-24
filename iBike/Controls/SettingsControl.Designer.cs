namespace iBike
{
    partial class SettingsControl
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
            this.chkElevation = new System.Windows.Forms.CheckBox();
            this.chkDistance = new System.Windows.Forms.CheckBox();
            this.chkPower = new System.Windows.Forms.CheckBox();
            this.chkCadence = new System.Windows.Forms.CheckBox();
            this.chkHR = new System.Windows.Forms.CheckBox();
            this.bnrTitle = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.lblDistTime = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkWind = new System.Windows.Forms.CheckBox();
            this.chkTilt = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkElevation
            // 
            this.chkElevation.AutoSize = true;
            this.chkElevation.Location = new System.Drawing.Point(6, 41);
            this.chkElevation.Name = "chkElevation";
            this.chkElevation.Size = new System.Drawing.Size(70, 17);
            this.chkElevation.TabIndex = 0;
            this.chkElevation.Text = "Elevation";
            this.chkElevation.UseVisualStyleBackColor = true;
            // 
            // chkDistance
            // 
            this.chkDistance.AutoSize = true;
            this.chkDistance.Location = new System.Drawing.Point(6, 65);
            this.chkDistance.Name = "chkDistance";
            this.chkDistance.Size = new System.Drawing.Size(111, 17);
            this.chkDistance.TabIndex = 1;
            this.chkDistance.Text = "Speed + Distance";
            this.chkDistance.UseVisualStyleBackColor = true;
            // 
            // chkPower
            // 
            this.chkPower.AutoSize = true;
            this.chkPower.Location = new System.Drawing.Point(6, 19);
            this.chkPower.Name = "chkPower";
            this.chkPower.Size = new System.Drawing.Size(56, 17);
            this.chkPower.TabIndex = 1;
            this.chkPower.Text = "Power";
            this.chkPower.UseVisualStyleBackColor = true;
            // 
            // chkCadence
            // 
            this.chkCadence.AutoSize = true;
            this.chkCadence.Location = new System.Drawing.Point(146, 42);
            this.chkCadence.Name = "chkCadence";
            this.chkCadence.Size = new System.Drawing.Size(69, 17);
            this.chkCadence.TabIndex = 1;
            this.chkCadence.Text = "Cadence";
            this.chkCadence.UseVisualStyleBackColor = true;
            // 
            // chkHR
            // 
            this.chkHR.AutoSize = true;
            this.chkHR.Location = new System.Drawing.Point(146, 19);
            this.chkHR.Name = "chkHR";
            this.chkHR.Size = new System.Drawing.Size(78, 17);
            this.chkHR.TabIndex = 1;
            this.chkHR.Text = "Heart Rate";
            this.chkHR.UseVisualStyleBackColor = true;
            // 
            // bnrTitle
            // 
            this.bnrTitle.BackColor = System.Drawing.Color.Transparent;
            this.bnrTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.bnrTitle.HasMenuButton = false;
            this.bnrTitle.Location = new System.Drawing.Point(0, 0);
            this.bnrTitle.Name = "bnrTitle";
            this.bnrTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bnrTitle.Size = new System.Drawing.Size(274, 27);
            this.bnrTitle.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header2;
            this.bnrTitle.TabIndex = 2;
            this.bnrTitle.UseStyleFont = true;
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartTime.Location = new System.Drawing.Point(38, 39);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(136, 13);
            this.lblStartTime.TabIndex = 4;
            this.lblStartTime.Text = "Sun 7/11/2010 - 10:04 AM";
            // 
            // lblDistTime
            // 
            this.lblDistTime.AutoSize = true;
            this.lblDistTime.Location = new System.Drawing.Point(38, 58);
            this.lblDistTime.Name = "lblDistTime";
            this.lblDistTime.Size = new System.Drawing.Size(97, 13);
            this.lblDistTime.TabIndex = 4;
            this.lblDistTime.Text = "51.33 mi. / 2:58:20";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkElevation);
            this.groupBox1.Controls.Add(this.chkDistance);
            this.groupBox1.Controls.Add(this.chkPower);
            this.groupBox1.Controls.Add(this.chkTilt);
            this.groupBox1.Controls.Add(this.chkWind);
            this.groupBox1.Controls.Add(this.chkCadence);
            this.groupBox1.Controls.Add(this.chkHR);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 116);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add/update data:";
            // 
            // chkWind
            // 
            this.chkWind.AutoSize = true;
            this.chkWind.Location = new System.Drawing.Point(6, 88);
            this.chkWind.Name = "chkWind";
            this.chkWind.Size = new System.Drawing.Size(51, 17);
            this.chkWind.TabIndex = 1;
            this.chkWind.Text = "Wind";
            this.chkWind.UseVisualStyleBackColor = true;
            // 
            // chkTilt
            // 
            this.chkTilt.AutoSize = true;
            this.chkTilt.Location = new System.Drawing.Point(146, 88);
            this.chkTilt.Name = "chkTilt";
            this.chkTilt.Size = new System.Drawing.Size(40, 17);
            this.chkTilt.TabIndex = 1;
            this.chkTilt.Text = "Tilt";
            this.chkTilt.UseVisualStyleBackColor = true;
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblDistTime);
            this.Controls.Add(this.lblStartTime);
            this.Controls.Add(this.bnrTitle);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(274, 196);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkElevation;
        private System.Windows.Forms.CheckBox chkDistance;
        private System.Windows.Forms.CheckBox chkPower;
        private System.Windows.Forms.CheckBox chkCadence;
        private System.Windows.Forms.CheckBox chkHR;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner bnrTitle;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.Label lblDistTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkWind;
        private System.Windows.Forms.CheckBox chkTilt;
    }
}
