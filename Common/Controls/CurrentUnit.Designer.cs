namespace Common.Controls
{
    partial class CurrentUnit
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
            this.pb_icon = new System.Windows.Forms.PictureBox();
            this.b_main = new System.Windows.Forms.Button();
            this.b_sec = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pb_icon)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_icon
            // 
            this.pb_icon.Location = new System.Drawing.Point(35, 37);
            this.pb_icon.Name = "pb_icon";
            this.pb_icon.Size = new System.Drawing.Size(80, 80);
            this.pb_icon.TabIndex = 0;
            this.pb_icon.TabStop = false;
            // 
            // b_main
            // 
            this.b_main.BackColor = System.Drawing.Color.Silver;
            this.b_main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.b_main.FlatAppearance.BorderSize = 0;
            this.b_main.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b_main.Location = new System.Drawing.Point(13, 0);
            this.b_main.Margin = new System.Windows.Forms.Padding(0);
            this.b_main.Name = "b_main";
            this.b_main.Size = new System.Drawing.Size(25, 25);
            this.b_main.TabIndex = 1;
            this.b_main.UseVisualStyleBackColor = false;
            // 
            // b_sec
            // 
            this.b_sec.BackColor = System.Drawing.Color.Silver;
            this.b_sec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.b_sec.FlatAppearance.BorderSize = 0;
            this.b_sec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b_sec.Location = new System.Drawing.Point(50, 0);
            this.b_sec.Margin = new System.Windows.Forms.Padding(0);
            this.b_sec.Name = "b_sec";
            this.b_sec.Size = new System.Drawing.Size(25, 25);
            this.b_sec.TabIndex = 2;
            this.b_sec.UseVisualStyleBackColor = false;
            // 
            // CurrentUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.b_sec);
            this.Controls.Add(this.b_main);
            this.Controls.Add(this.pb_icon);
            this.Name = "CurrentUnit";
            this.Size = new System.Drawing.Size(150, 120);
            ((System.ComponentModel.ISupportInitialize)(this.pb_icon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_icon;
        private System.Windows.Forms.Button b_main;
        private System.Windows.Forms.Button b_sec;
    }
}
