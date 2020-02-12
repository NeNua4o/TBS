namespace Tests
{
    partial class MainForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.nud_ptCount = new System.Windows.Forms.NumericUpDown();
            this.nud_width = new System.Windows.Forms.NumericUpDown();
            this.nud_height = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.nud_Lloyd = new System.Windows.Forms.NumericUpDown();
            this.pb1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ptCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Lloyd)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(360, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 20);
            this.button1.TabIndex = 0;
            this.button1.Text = "grid+poly";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 300);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // nud_ptCount
            // 
            this.nud_ptCount.Location = new System.Drawing.Point(186, 12);
            this.nud_ptCount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nud_ptCount.Name = "nud_ptCount";
            this.nud_ptCount.Size = new System.Drawing.Size(81, 20);
            this.nud_ptCount.TabIndex = 2;
            this.nud_ptCount.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // nud_width
            // 
            this.nud_width.Location = new System.Drawing.Point(12, 12);
            this.nud_width.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nud_width.Name = "nud_width";
            this.nud_width.Size = new System.Drawing.Size(81, 20);
            this.nud_width.TabIndex = 3;
            this.nud_width.Value = new decimal(new int[] {
            1400,
            0,
            0,
            0});
            // 
            // nud_height
            // 
            this.nud_height.Location = new System.Drawing.Point(99, 12);
            this.nud_height.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nud_height.Name = "nud_height";
            this.nud_height.Size = new System.Drawing.Size(81, 20);
            this.nud_height.TabIndex = 4;
            this.nud_height.Value = new decimal(new int[] {
            700,
            0,
            0,
            0});
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(433, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(54, 20);
            this.button2.TabIndex = 5;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(567, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(61, 20);
            this.button3.TabIndex = 6;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // nud_Lloyd
            // 
            this.nud_Lloyd.Location = new System.Drawing.Point(273, 12);
            this.nud_Lloyd.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nud_Lloyd.Name = "nud_Lloyd";
            this.nud_Lloyd.Size = new System.Drawing.Size(81, 20);
            this.nud_Lloyd.TabIndex = 7;
            this.nud_Lloyd.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // pb1
            // 
            this.pb1.Location = new System.Drawing.Point(634, 12);
            this.pb1.Maximum = 1;
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(20, 20);
            this.pb1.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(768, 415);
            this.Controls.Add(this.pb1);
            this.Controls.Add(this.nud_Lloyd);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.nud_height);
            this.Controls.Add(this.nud_width);
            this.Controls.Add(this.nud_ptCount);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ptCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Lloyd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown nud_ptCount;
        private System.Windows.Forms.NumericUpDown nud_width;
        private System.Windows.Forms.NumericUpDown nud_height;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.NumericUpDown nud_Lloyd;
        private System.Windows.Forms.ProgressBar pb1;
    }
}

