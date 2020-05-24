namespace iBike.Controls
{
    partial class SelectChartsForm
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
            this.AvailableChartsList = new System.Windows.Forms.ListBox();
            this.DisplayedChartsList = new System.Windows.Forms.ListBox();
            this.AddChartButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.MoveUpButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.MoveDownButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.RemoveChartButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.Cancel_Button = new ZoneFiveSoftware.Common.Visuals.Button();
            this.OKButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.MainPanel = new ZoneFiveSoftware.Common.Visuals.Panel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.SelectedChartLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.AvailableChartLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.MainPanel.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AvailableChartsList
            // 
            this.AvailableChartsList.FormattingEnabled = true;
            this.AvailableChartsList.Location = new System.Drawing.Point(3, 16);
            this.AvailableChartsList.Name = "AvailableChartsList";
            this.AvailableChartsList.Size = new System.Drawing.Size(246, 251);
            this.AvailableChartsList.TabIndex = 0;
            this.AvailableChartsList.SelectedIndexChanged += new System.EventHandler(this.AvailableChartsList_SelectedIndexChanged);
            this.AvailableChartsList.DoubleClick += new System.EventHandler(this.AvailableChartsList_DoubleClick);
            // 
            // DisplayedChartsList
            // 
            this.DisplayedChartsList.FormattingEnabled = true;
            this.DisplayedChartsList.Location = new System.Drawing.Point(3, 16);
            this.DisplayedChartsList.Name = "DisplayedChartsList";
            this.DisplayedChartsList.Size = new System.Drawing.Size(246, 251);
            this.DisplayedChartsList.TabIndex = 0;
            this.DisplayedChartsList.SelectedIndexChanged += new System.EventHandler(this.DisplayedChartsList_SelectedIndexChanged);
            this.DisplayedChartsList.DoubleClick += new System.EventHandler(this.DisplayedChartsList_DoubleClick);
            // 
            // AddChartButton
            // 
            this.AddChartButton.BackColor = System.Drawing.Color.Transparent;
            this.AddChartButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.AddChartButton.CenterImage = null;
            this.AddChartButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.AddChartButton.Enabled = false;
            this.AddChartButton.HyperlinkStyle = false;
            this.AddChartButton.ImageMargin = 2;
            this.AddChartButton.LeftImage = null;
            this.AddChartButton.Location = new System.Drawing.Point(3, 3);
            this.AddChartButton.MinimumSize = new System.Drawing.Size(75, 23);
            this.AddChartButton.Name = "AddChartButton";
            this.AddChartButton.PushStyle = true;
            this.AddChartButton.RightImage = null;
            this.AddChartButton.Size = new System.Drawing.Size(75, 23);
            this.AddChartButton.TabIndex = 1;
            this.AddChartButton.Text = "Add chart";
            this.AddChartButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.AddChartButton.TextLeftMargin = 2;
            this.AddChartButton.TextRightMargin = 2;
            this.AddChartButton.Click += new System.EventHandler(this.AddChartButton_Click);
            // 
            // MoveUpButton
            // 
            this.MoveUpButton.BackColor = System.Drawing.Color.Transparent;
            this.MoveUpButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.MoveUpButton.CenterImage = null;
            this.MoveUpButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.MoveUpButton.Enabled = false;
            this.MoveUpButton.HyperlinkStyle = false;
            this.MoveUpButton.ImageMargin = 2;
            this.MoveUpButton.LeftImage = null;
            this.MoveUpButton.Location = new System.Drawing.Point(3, 3);
            this.MoveUpButton.Name = "MoveUpButton";
            this.MoveUpButton.PushStyle = true;
            this.MoveUpButton.RightImage = null;
            this.MoveUpButton.Size = new System.Drawing.Size(23, 23);
            this.MoveUpButton.TabIndex = 2;
            this.MoveUpButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.MoveUpButton.TextLeftMargin = 2;
            this.MoveUpButton.TextRightMargin = 2;
            this.MoveUpButton.Click += new System.EventHandler(this.MoveUpButton_Click);
            // 
            // MoveDownButton
            // 
            this.MoveDownButton.BackColor = System.Drawing.Color.Transparent;
            this.MoveDownButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.MoveDownButton.CenterImage = null;
            this.MoveDownButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.MoveDownButton.Enabled = false;
            this.MoveDownButton.HyperlinkStyle = false;
            this.MoveDownButton.ImageMargin = 2;
            this.MoveDownButton.LeftImage = null;
            this.MoveDownButton.Location = new System.Drawing.Point(32, 3);
            this.MoveDownButton.Name = "MoveDownButton";
            this.MoveDownButton.PushStyle = true;
            this.MoveDownButton.RightImage = null;
            this.MoveDownButton.Size = new System.Drawing.Size(23, 23);
            this.MoveDownButton.TabIndex = 2;
            this.MoveDownButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.MoveDownButton.TextLeftMargin = 2;
            this.MoveDownButton.TextRightMargin = 2;
            this.MoveDownButton.Click += new System.EventHandler(this.MoveDownButton_Click);
            // 
            // RemoveChartButton
            // 
            this.RemoveChartButton.BackColor = System.Drawing.Color.Transparent;
            this.RemoveChartButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.RemoveChartButton.CenterImage = null;
            this.RemoveChartButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.RemoveChartButton.Enabled = false;
            this.RemoveChartButton.HyperlinkStyle = false;
            this.RemoveChartButton.ImageMargin = 2;
            this.RemoveChartButton.LeftImage = null;
            this.RemoveChartButton.Location = new System.Drawing.Point(61, 3);
            this.RemoveChartButton.MinimumSize = new System.Drawing.Size(75, 23);
            this.RemoveChartButton.Name = "RemoveChartButton";
            this.RemoveChartButton.PushStyle = true;
            this.RemoveChartButton.RightImage = null;
            this.RemoveChartButton.Size = new System.Drawing.Size(119, 23);
            this.RemoveChartButton.TabIndex = 1;
            this.RemoveChartButton.Text = "Remove chart";
            this.RemoveChartButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.RemoveChartButton.TextLeftMargin = 2;
            this.RemoveChartButton.TextRightMargin = 2;
            this.RemoveChartButton.Click += new System.EventHandler(this.RemoveChartButton_Click);
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel_Button.BackColor = System.Drawing.Color.Transparent;
            this.Cancel_Button.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.Cancel_Button.CenterImage = null;
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_Button.HyperlinkStyle = false;
            this.Cancel_Button.ImageMargin = 2;
            this.Cancel_Button.LeftImage = null;
            this.Cancel_Button.Location = new System.Drawing.Point(445, 327);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.PushStyle = true;
            this.Cancel_Button.RightImage = null;
            this.Cancel_Button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_Button.TabIndex = 3;
            this.Cancel_Button.Text = "Cancel";
            this.Cancel_Button.TextAlign = System.Drawing.StringAlignment.Center;
            this.Cancel_Button.TextLeftMargin = 2;
            this.Cancel_Button.TextRightMargin = 2;
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.BackColor = System.Drawing.Color.Transparent;
            this.OKButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.OKButton.CenterImage = null;
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.HyperlinkStyle = false;
            this.OKButton.ImageMargin = 2;
            this.OKButton.LeftImage = null;
            this.OKButton.Location = new System.Drawing.Point(364, 327);
            this.OKButton.Name = "OKButton";
            this.OKButton.PushStyle = true;
            this.OKButton.RightImage = null;
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "OK";
            this.OKButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.OKButton.TextLeftMargin = 2;
            this.OKButton.TextRightMargin = 2;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.BackColor = System.Drawing.Color.Transparent;
            this.MainPanel.BorderColor = System.Drawing.Color.Gray;
            this.MainPanel.Controls.Add(this.flowLayoutPanel4);
            this.MainPanel.Controls.Add(this.flowLayoutPanel3);
            this.MainPanel.HeadingBackColor = System.Drawing.Color.LightBlue;
            this.MainPanel.HeadingFont = null;
            this.MainPanel.HeadingLeftMargin = 0;
            this.MainPanel.HeadingText = null;
            this.MainPanel.HeadingTextColor = System.Drawing.Color.Black;
            this.MainPanel.HeadingTopMargin = 3;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(7);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(532, 319);
            this.MainPanel.TabIndex = 4;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.SelectedChartLabel);
            this.flowLayoutPanel4.Controls.Add(this.DisplayedChartsList);
            this.flowLayoutPanel4.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(268, 7);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(252, 311);
            this.flowLayoutPanel4.TabIndex = 7;
            // 
            // SelectedChartLabel
            // 
            this.SelectedChartLabel.AutoSize = true;
            this.SelectedChartLabel.Location = new System.Drawing.Point(3, 0);
            this.SelectedChartLabel.Name = "SelectedChartLabel";
            this.SelectedChartLabel.Size = new System.Drawing.Size(87, 13);
            this.SelectedChartLabel.TabIndex = 3;
            this.SelectedChartLabel.Text = "Selected charts :";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.Controls.Add(this.MoveUpButton);
            this.flowLayoutPanel2.Controls.Add(this.MoveDownButton);
            this.flowLayoutPanel2.Controls.Add(this.RemoveChartButton);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 273);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(246, 29);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.AvailableChartLabel);
            this.flowLayoutPanel3.Controls.Add(this.AvailableChartsList);
            this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel1);
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(9, 7);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(252, 311);
            this.flowLayoutPanel3.TabIndex = 6;
            // 
            // AvailableChartLabel
            // 
            this.AvailableChartLabel.AutoSize = true;
            this.AvailableChartLabel.Location = new System.Drawing.Point(3, 0);
            this.AvailableChartLabel.Name = "AvailableChartLabel";
            this.AvailableChartLabel.Size = new System.Drawing.Size(56, 13);
            this.AvailableChartLabel.TabIndex = 3;
            this.AvailableChartLabel.Text = "Available :";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.AddChartButton);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 273);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(246, 29);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // SelectChartsForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_Button;
            this.ClientSize = new System.Drawing.Size(532, 354);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.Cancel_Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectChartsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chart Details";
            this.MainPanel.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox AvailableChartsList;
        private System.Windows.Forms.ListBox DisplayedChartsList;
        private ZoneFiveSoftware.Common.Visuals.Button AddChartButton;
        private ZoneFiveSoftware.Common.Visuals.Button MoveUpButton;
        private ZoneFiveSoftware.Common.Visuals.Button MoveDownButton;
        private ZoneFiveSoftware.Common.Visuals.Button RemoveChartButton;
        private ZoneFiveSoftware.Common.Visuals.Button Cancel_Button;
        private ZoneFiveSoftware.Common.Visuals.Button OKButton;
        private ZoneFiveSoftware.Common.Visuals.Panel MainPanel;
        private System.Windows.Forms.Label SelectedChartLabel;
        private System.Windows.Forms.Label AvailableChartLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
    }
}