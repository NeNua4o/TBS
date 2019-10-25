namespace Common.Controls
{
    partial class TurnQueueItem
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
            this.l_init = new System.Windows.Forms.Label();
            this.l_hp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_icon)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_icon
            // 
            this.pb_icon.Location = new System.Drawing.Point(7, 16);
            this.pb_icon.Name = "pb_icon";
            this.pb_icon.Size = new System.Drawing.Size(50, 50);
            this.pb_icon.TabIndex = 0;
            this.pb_icon.TabStop = false;
            // 
            // l_init
            // 
            this.l_init.AutoSize = true;
            this.l_init.Location = new System.Drawing.Point(1, 1);
            this.l_init.Name = "l_init";
            this.l_init.Size = new System.Drawing.Size(63, 13);
            this.l_init.TabIndex = 1;
            this.l_init.Text = "<unknown>";
            this.l_init.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // l_hp
            // 
            this.l_hp.AutoSize = true;
            this.l_hp.Location = new System.Drawing.Point(3, 69);
            this.l_hp.Name = "l_hp";
            this.l_hp.Size = new System.Drawing.Size(60, 13);
            this.l_hp.TabIndex = 2;
            this.l_hp.Text = "9999/9999";
            this.l_hp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TurnQueueItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.Controls.Add(this.pb_icon);
            this.Controls.Add(this.l_init);
            this.Controls.Add(this.l_hp);
            this.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.Name = "TurnQueueItem";
            this.Size = new System.Drawing.Size(65, 85);
            ((System.ComponentModel.ISupportInitialize)(this.pb_icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_icon;
        private System.Windows.Forms.Label l_init;
        private System.Windows.Forms.Label l_hp;
    }
}
