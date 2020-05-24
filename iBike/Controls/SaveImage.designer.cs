// <copyright file="ImageSaveDialog.Designer.cs" company="N/A">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>mechgt</author>
// <email>mechgt@gmail.com</email>
// <date>2008-12-23</date>
namespace iBike.Controls
{
    partial class SaveImage
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
            this.bottomPanel = new ZoneFiveSoftware.Common.Visuals.Panel();
            this.btnOK = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnCancel = new ZoneFiveSoftware.Common.Visuals.Button();
            this.lblSaveIn = new System.Windows.Forms.Label();
            this.lblFilename = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.txtDirectory = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtFilename = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.backPanel = new ZoneFiveSoftware.Common.Visuals.Panel();
            this.btnTypeOpen = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnSizeOpen = new ZoneFiveSoftware.Common.Visuals.Button();
            this.txtType = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtSize = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.btnFolderUp = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnDirTree = new ZoneFiveSoftware.Common.Visuals.Button();
            this.bottomPanel.SuspendLayout();
            this.backPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // bottomPanel
            // 
            this.bottomPanel.BackColor = System.Drawing.Color.Transparent;
            this.bottomPanel.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.Square;
            this.bottomPanel.BorderColor = System.Drawing.Color.Gray;
            this.bottomPanel.Controls.Add(this.btnOK);
            this.bottomPanel.Controls.Add(this.btnCancel);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.HeadingBackColor = System.Drawing.Color.LightBlue;
            this.bottomPanel.HeadingFont = null;
            this.bottomPanel.HeadingLeftMargin = 0;
            this.bottomPanel.HeadingText = null;
            this.bottomPanel.HeadingTextColor = System.Drawing.Color.Black;
            this.bottomPanel.HeadingTopMargin = 3;
            this.bottomPanel.Location = new System.Drawing.Point(0, 168);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.RightGradientColor = System.Drawing.Color.Black;
            this.bottomPanel.RightGradientPercent = 0.25;
            this.bottomPanel.Size = new System.Drawing.Size(508, 35);
            this.bottomPanel.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnOK.CenterImage = null;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOK.HyperlinkStyle = false;
            this.btnOK.ImageMargin = 2;
            this.btnOK.LeftImage = null;
            this.btnOK.Location = new System.Drawing.Point(340, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.PushStyle = true;
            this.btnOK.RightImage = null;
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "OK";
            this.btnOK.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnOK.TextLeftMargin = 2;
            this.btnOK.TextRightMargin = 2;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
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
            this.btnCancel.Location = new System.Drawing.Point(421, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PushStyle = true;
            this.btnCancel.RightImage = null;
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnCancel.TextLeftMargin = 2;
            this.btnCancel.TextRightMargin = 2;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblSaveIn
            // 
            this.lblSaveIn.AutoSize = true;
            this.lblSaveIn.Location = new System.Drawing.Point(12, 16);
            this.lblSaveIn.Name = "lblSaveIn";
            this.lblSaveIn.Size = new System.Drawing.Size(47, 13);
            this.lblSaveIn.TabIndex = 2;
            this.lblSaveIn.Text = "Save In:";
            // 
            // lblFilename
            // 
            this.lblFilename.AutoSize = true;
            this.lblFilename.Location = new System.Drawing.Point(12, 40);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(55, 13);
            this.lblFilename.TabIndex = 2;
            this.lblFilename.Text = "File name:";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(12, 67);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(30, 13);
            this.lblSize.TabIndex = 2;
            this.lblSize.Text = "Size:";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(12, 92);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 13);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "Type:";
            // 
            // txtDirectory
            // 
            this.txtDirectory.AcceptsReturn = false;
            this.txtDirectory.AcceptsTab = false;
            this.txtDirectory.BackColor = System.Drawing.Color.White;
            this.txtDirectory.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtDirectory.ButtonImage = null;
            this.txtDirectory.Location = new System.Drawing.Point(109, 16);
            this.txtDirectory.Multiline = false;
            this.txtDirectory.Name = "txtDirectory";
            this.txtDirectory.ReadOnly = false;
            this.txtDirectory.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtDirectory.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtDirectory.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDirectory.Size = new System.Drawing.Size(363, 19);
            this.txtDirectory.TabIndex = 3;
            this.txtDirectory.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtFilename
            // 
            this.txtFilename.AcceptsReturn = false;
            this.txtFilename.AcceptsTab = false;
            this.txtFilename.BackColor = System.Drawing.Color.White;
            this.txtFilename.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtFilename.ButtonImage = null;
            this.txtFilename.Location = new System.Drawing.Point(109, 40);
            this.txtFilename.Multiline = false;
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.ReadOnly = false;
            this.txtFilename.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtFilename.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtFilename.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFilename.Size = new System.Drawing.Size(363, 19);
            this.txtFilename.TabIndex = 3;
            this.txtFilename.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // backPanel
            // 
            this.backPanel.BackColor = System.Drawing.Color.Transparent;
            this.backPanel.BorderColor = System.Drawing.Color.Gray;
            this.backPanel.Controls.Add(this.btnTypeOpen);
            this.backPanel.Controls.Add(this.btnSizeOpen);
            this.backPanel.Controls.Add(this.txtType);
            this.backPanel.Controls.Add(this.txtSize);
            this.backPanel.Controls.Add(this.txtFilename);
            this.backPanel.Controls.Add(this.btnFolderUp);
            this.backPanel.Controls.Add(this.btnDirTree);
            this.backPanel.Controls.Add(this.lblFilename);
            this.backPanel.Controls.Add(this.lblSize);
            this.backPanel.Controls.Add(this.lblType);
            this.backPanel.Controls.Add(this.txtDirectory);
            this.backPanel.Controls.Add(this.lblSaveIn);
            this.backPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backPanel.HeadingBackColor = System.Drawing.Color.LightBlue;
            this.backPanel.HeadingFont = null;
            this.backPanel.HeadingLeftMargin = 0;
            this.backPanel.HeadingText = null;
            this.backPanel.HeadingTextColor = System.Drawing.Color.Black;
            this.backPanel.HeadingTopMargin = 3;
            this.backPanel.Location = new System.Drawing.Point(0, 0);
            this.backPanel.Name = "backPanel";
            this.backPanel.Size = new System.Drawing.Size(508, 203);
            this.backPanel.TabIndex = 5;
            // 
            // btnTypeOpen
            // 
            this.btnTypeOpen.BackColor = System.Drawing.Color.Transparent;
            this.btnTypeOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnTypeOpen.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnTypeOpen.CenterImage = null;
            this.btnTypeOpen.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnTypeOpen.HyperlinkStyle = false;
            this.btnTypeOpen.ImageMargin = 2;
            this.btnTypeOpen.LeftImage = null;
            this.btnTypeOpen.Location = new System.Drawing.Point(306, 91);
            this.btnTypeOpen.Name = "btnTypeOpen";
            this.btnTypeOpen.PushStyle = true;
            this.btnTypeOpen.RightImage = null;
            this.btnTypeOpen.Size = new System.Drawing.Size(17, 17);
            this.btnTypeOpen.TabIndex = 9;
            this.btnTypeOpen.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnTypeOpen.TextLeftMargin = 2;
            this.btnTypeOpen.TextRightMargin = 2;
            this.btnTypeOpen.Click += new System.EventHandler(this.btnTypeOpen_Click);
            // 
            // btnSizeOpen
            // 
            this.btnSizeOpen.BackColor = System.Drawing.Color.Transparent;
            this.btnSizeOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSizeOpen.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnSizeOpen.CenterImage = null;
            this.btnSizeOpen.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSizeOpen.HyperlinkStyle = false;
            this.btnSizeOpen.ImageMargin = 2;
            this.btnSizeOpen.LeftImage = null;
            this.btnSizeOpen.Location = new System.Drawing.Point(306, 66);
            this.btnSizeOpen.Name = "btnSizeOpen";
            this.btnSizeOpen.PushStyle = true;
            this.btnSizeOpen.RightImage = null;
            this.btnSizeOpen.Size = new System.Drawing.Size(17, 17);
            this.btnSizeOpen.TabIndex = 8;
            this.btnSizeOpen.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnSizeOpen.TextLeftMargin = 2;
            this.btnSizeOpen.TextRightMargin = 2;
            this.btnSizeOpen.Click += new System.EventHandler(this.btnSizeOpen_Click);
            // 
            // txtType
            // 
            this.txtType.AcceptsReturn = false;
            this.txtType.AcceptsTab = false;
            this.txtType.BackColor = System.Drawing.Color.White;
            this.txtType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtType.ButtonImage = null;
            this.txtType.Location = new System.Drawing.Point(109, 90);
            this.txtType.Multiline = false;
            this.txtType.Name = "txtType";
            this.txtType.ReadOnly = false;
            this.txtType.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtType.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtType.Size = new System.Drawing.Size(215, 19);
            this.txtType.TabIndex = 7;
            this.txtType.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtSize
            // 
            this.txtSize.AcceptsReturn = false;
            this.txtSize.AcceptsTab = false;
            this.txtSize.BackColor = System.Drawing.Color.White;
            this.txtSize.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtSize.ButtonImage = null;
            this.txtSize.Location = new System.Drawing.Point(109, 65);
            this.txtSize.Multiline = false;
            this.txtSize.Name = "txtSize";
            this.txtSize.ReadOnly = false;
            this.txtSize.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtSize.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSize.Size = new System.Drawing.Size(215, 19);
            this.txtSize.TabIndex = 7;
            this.txtSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnFolderUp
            // 
            this.btnFolderUp.BackColor = System.Drawing.Color.Transparent;
            this.btnFolderUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnFolderUp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnFolderUp.CenterImage = null;
            this.btnFolderUp.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnFolderUp.HyperlinkStyle = false;
            this.btnFolderUp.ImageMargin = 2;
            this.btnFolderUp.LeftImage = null;
            this.btnFolderUp.Location = new System.Drawing.Point(476, 17);
            this.btnFolderUp.Name = "btnFolderUp";
            this.btnFolderUp.PushStyle = true;
            this.btnFolderUp.RightImage = null;
            this.btnFolderUp.Size = new System.Drawing.Size(17, 17);
            this.btnFolderUp.TabIndex = 6;
            this.btnFolderUp.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnFolderUp.TextLeftMargin = 2;
            this.btnFolderUp.TextRightMargin = 2;
            this.btnFolderUp.Click += new System.EventHandler(this.btnFolderUp_Click);
            // 
            // btnDirTree
            // 
            this.btnDirTree.BackColor = System.Drawing.Color.Transparent;
            this.btnDirTree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDirTree.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnDirTree.CenterImage = null;
            this.btnDirTree.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDirTree.HyperlinkStyle = false;
            this.btnDirTree.ImageMargin = 2;
            this.btnDirTree.LeftImage = null;
            this.btnDirTree.Location = new System.Drawing.Point(454, 17);
            this.btnDirTree.Name = "btnDirTree";
            this.btnDirTree.PushStyle = true;
            this.btnDirTree.RightImage = null;
            this.btnDirTree.Size = new System.Drawing.Size(17, 17);
            this.btnDirTree.TabIndex = 6;
            this.btnDirTree.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnDirTree.TextLeftMargin = 2;
            this.btnDirTree.TextRightMargin = 2;
            this.btnDirTree.Click += new System.EventHandler(this.btnDirTree_Click);
            // 
            // ImageSaveDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 203);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.backPanel);
            this.Name = "ImageSaveDialog";
            this.Text = "Save Image";
            this.bottomPanel.ResumeLayout(false);
            this.backPanel.ResumeLayout(false);
            this.backPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ZoneFiveSoftware.Common.Visuals.Panel bottomPanel;
        private ZoneFiveSoftware.Common.Visuals.Button btnOK;
        private ZoneFiveSoftware.Common.Visuals.Button btnCancel;
        private System.Windows.Forms.Label lblSaveIn;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblType;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtDirectory;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtFilename;
        private ZoneFiveSoftware.Common.Visuals.Panel backPanel;
        private ZoneFiveSoftware.Common.Visuals.Button btnDirTree;
        private ZoneFiveSoftware.Common.Visuals.Button btnFolderUp;
        private ZoneFiveSoftware.Common.Visuals.Button btnTypeOpen;
        private ZoneFiveSoftware.Common.Visuals.Button btnSizeOpen;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtType;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtSize;
    }
}