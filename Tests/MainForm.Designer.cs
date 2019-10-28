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
            this.b_rtest = new System.Windows.Forms.Button();
            this.pb_rtest = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_rtest)).BeginInit();
            this.SuspendLayout();
            // 
            // b_rtest
            // 
            this.b_rtest.Location = new System.Drawing.Point(12, 12);
            this.b_rtest.Name = "b_rtest";
            this.b_rtest.Size = new System.Drawing.Size(75, 23);
            this.b_rtest.TabIndex = 0;
            this.b_rtest.Text = "rtest";
            this.b_rtest.UseVisualStyleBackColor = true;
            this.b_rtest.Click += new System.EventHandler(this.b_rtest_Click);
            // 
            // pb_rtest
            // 
            this.pb_rtest.Location = new System.Drawing.Point(12, 50);
            this.pb_rtest.Name = "pb_rtest";
            this.pb_rtest.Size = new System.Drawing.Size(300, 500);
            this.pb_rtest.TabIndex = 1;
            this.pb_rtest.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 361);
            this.Controls.Add(this.pb_rtest);
            this.Controls.Add(this.b_rtest);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pb_rtest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_rtest;
        private System.Windows.Forms.PictureBox pb_rtest;
    }
}

