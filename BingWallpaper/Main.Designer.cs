namespace BingWallpaper
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.imagePreview = new System.Windows.Forms.PictureBox();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.FitBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CCBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FreqText = new System.Windows.Forms.TextBox();
            this.FreqTrack = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.ResetTaskButton = new System.Windows.Forms.Button();
            this.AboutButton = new System.Windows.Forms.Button();
            this.InfoLabel = new System.Windows.Forms.TextBox();
            this.ApplyOnce = new System.Windows.Forms.Button();
            this.Loading = new System.Windows.Forms.Label();
            this.MMK = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imagePreview)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FreqTrack)).BeginInit();
            this.SuspendLayout();
            // 
            // imagePreview
            // 
            this.imagePreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imagePreview.Location = new System.Drawing.Point(0, 0);
            this.imagePreview.Name = "imagePreview";
            this.imagePreview.Size = new System.Drawing.Size(719, 406);
            this.imagePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imagePreview.TabIndex = 0;
            this.imagePreview.TabStop = false;
            // 
            // ApplyButton
            // 
            this.ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplyButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApplyButton.Location = new System.Drawing.Point(725, 219);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(97, 23);
            this.ApplyButton.TabIndex = 1;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose a fit";
            // 
            // FitBox
            // 
            this.FitBox.AutoCompleteCustomSource.AddRange(new string[] {
            "Fill",
            "Fit",
            "Stretch",
            "Tile",
            "Center",
            "Span"});
            this.FitBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.FitBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.FitBox.BackColor = System.Drawing.Color.White;
            this.FitBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FitBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FitBox.FormattingEnabled = true;
            this.FitBox.Items.AddRange(new object[] {
            "Fill",
            "Fit",
            "Stretch",
            "Tile",
            "Center",
            "Span"});
            this.FitBox.Location = new System.Drawing.Point(12, 32);
            this.FitBox.Name = "FitBox";
            this.FitBox.Size = new System.Drawing.Size(182, 21);
            this.FitBox.TabIndex = 1;
            this.FitBox.Text = "Stretch";
            this.FitBox.TextChanged += new System.EventHandler(this.FitBox_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.CCBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.FitBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(725, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 108);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Style";
            // 
            // CCBox
            // 
            this.CCBox.BackColor = System.Drawing.Color.White;
            this.CCBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CCBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CCBox.FormattingEnabled = true;
            this.CCBox.Items.AddRange(new object[] {
            "Default"});
            this.CCBox.Location = new System.Drawing.Point(12, 75);
            this.CCBox.Name = "CCBox";
            this.CCBox.Size = new System.Drawing.Size(182, 21);
            this.CCBox.TabIndex = 3;
            this.CCBox.Text = "Default";
            this.CCBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CCBox_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Country Code";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.FreqText);
            this.groupBox2.Controls.Add(this.FreqTrack);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(725, 126);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 87);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Task settings";
            // 
            // FreqText
            // 
            this.FreqText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FreqText.Location = new System.Drawing.Point(130, 13);
            this.FreqText.Name = "FreqText";
            this.FreqText.Size = new System.Drawing.Size(64, 22);
            this.FreqText.TabIndex = 4;
            this.FreqText.TextChanged += new System.EventHandler(this.FreqText_TextChanged);
            // 
            // FreqTrack
            // 
            this.FreqTrack.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.FreqTrack.Location = new System.Drawing.Point(9, 38);
            this.FreqTrack.Maximum = 30;
            this.FreqTrack.Minimum = 1;
            this.FreqTrack.Name = "FreqTrack";
            this.FreqTrack.Size = new System.Drawing.Size(185, 45);
            this.FreqTrack.TabIndex = 3;
            this.FreqTrack.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.FreqTrack.Value = 1;
            this.FreqTrack.Scroll += new System.EventHandler(this.FreqTrack_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Frequency (in minutes):";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshButton.Location = new System.Drawing.Point(828, 248);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(97, 23);
            this.RefreshButton.TabIndex = 4;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // ResetTaskButton
            // 
            this.ResetTaskButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetTaskButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResetTaskButton.Location = new System.Drawing.Point(725, 248);
            this.ResetTaskButton.Name = "ResetTaskButton";
            this.ResetTaskButton.Size = new System.Drawing.Size(97, 23);
            this.ResetTaskButton.TabIndex = 5;
            this.ResetTaskButton.Text = "Reset task";
            this.ResetTaskButton.UseVisualStyleBackColor = true;
            this.ResetTaskButton.Click += new System.EventHandler(this.ResetTaskButton_Click);
            // 
            // AboutButton
            // 
            this.AboutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AboutButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AboutButton.Location = new System.Drawing.Point(725, 277);
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(200, 23);
            this.AboutButton.TabIndex = 6;
            this.AboutButton.Text = "About Bing Wallpaper";
            this.AboutButton.UseVisualStyleBackColor = true;
            this.AboutButton.Click += new System.EventHandler(this.AboutButton_Click);
            // 
            // InfoLabel
            // 
            this.InfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(247)))));
            this.InfoLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InfoLabel.Cursor = System.Windows.Forms.Cursors.Help;
            this.InfoLabel.Enabled = false;
            this.InfoLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoLabel.ForeColor = System.Drawing.SystemColors.WindowText;
            this.InfoLabel.Location = new System.Drawing.Point(725, 306);
            this.InfoLabel.Multiline = true;
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(200, 81);
            this.InfoLabel.TabIndex = 7;
            // 
            // ApplyOnce
            // 
            this.ApplyOnce.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplyOnce.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApplyOnce.Location = new System.Drawing.Point(828, 219);
            this.ApplyOnce.Name = "ApplyOnce";
            this.ApplyOnce.Size = new System.Drawing.Size(97, 23);
            this.ApplyOnce.TabIndex = 8;
            this.ApplyOnce.Text = "Apply once";
            this.ApplyOnce.UseVisualStyleBackColor = true;
            this.ApplyOnce.Click += new System.EventHandler(this.ApplyOnce_Click);
            // 
            // Loading
            // 
            this.Loading.AutoSize = true;
            this.Loading.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Loading.Location = new System.Drawing.Point(722, 373);
            this.Loading.Name = "Loading";
            this.Loading.Size = new System.Drawing.Size(102, 30);
            this.Loading.TabIndex = 9;
            this.Loading.Text = "Loading...";
            // 
            // MMK
            // 
            this.MMK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MMK.AutoSize = true;
            this.MMK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MMK.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MMK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(160)))), ((int)(((byte)(179)))));
            this.MMK.Location = new System.Drawing.Point(828, 389);
            this.MMK.Name = "MMK";
            this.MMK.Size = new System.Drawing.Size(104, 15);
            this.MMK.TabIndex = 12;
            this.MMK.Text = "{ } with ♥ by MMK";
            this.MMK.Click += new System.EventHandler(this.MMK_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(934, 406);
            this.Controls.Add(this.MMK);
            this.Controls.Add(this.Loading);
            this.Controls.Add(this.ApplyOnce);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.AboutButton);
            this.Controls.Add(this.ResetTaskButton);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.imagePreview);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(950, 445);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bing Wallpaper";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imagePreview)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FreqTrack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imagePreview;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox FitBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox CCBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox FreqText;
        private System.Windows.Forms.TrackBar FreqTrack;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Button ResetTaskButton;
        private System.Windows.Forms.Button AboutButton;
        private System.Windows.Forms.TextBox InfoLabel;
        private System.Windows.Forms.Button ApplyOnce;
        private System.Windows.Forms.Label Loading;
        private System.Windows.Forms.Label MMK;
    }
}

