namespace ClientV1Converter
{
    partial class Form1
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
            this.b_pickDir = new System.Windows.Forms.Button();
            this.pb_top = new System.Windows.Forms.PictureBox();
            this.tb_locName = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.vsc = new System.Windows.Forms.VScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.pb_top)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // b_pickDir
            // 
            this.b_pickDir.Location = new System.Drawing.Point(12, 38);
            this.b_pickDir.Name = "b_pickDir";
            this.b_pickDir.Size = new System.Drawing.Size(100, 23);
            this.b_pickDir.TabIndex = 0;
            this.b_pickDir.Text = "pick dir";
            this.b_pickDir.UseVisualStyleBackColor = true;
            this.b_pickDir.Click += new System.EventHandler(this.b_pickDir_Click);
            // 
            // pb_top
            // 
            this.pb_top.Location = new System.Drawing.Point(118, 12);
            this.pb_top.Name = "pb_top";
            this.pb_top.Size = new System.Drawing.Size(500, 500);
            this.pb_top.TabIndex = 1;
            this.pb_top.TabStop = false;
            // 
            // tb_locName
            // 
            this.tb_locName.Location = new System.Drawing.Point(12, 12);
            this.tb_locName.Name = "tb_locName";
            this.tb_locName.Size = new System.Drawing.Size(100, 20);
            this.tb_locName.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(664, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 500);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // vsc
            // 
            this.vsc.Location = new System.Drawing.Point(621, 12);
            this.vsc.Name = "vsc";
            this.vsc.Size = new System.Drawing.Size(17, 500);
            this.vsc.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 523);
            this.Controls.Add(this.vsc);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tb_locName);
            this.Controls.Add(this.pb_top);
            this.Controls.Add(this.b_pickDir);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pb_top)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button b_pickDir;
        private System.Windows.Forms.PictureBox pb_top;
        private System.Windows.Forms.TextBox tb_locName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.VScrollBar vsc;
    }
}

