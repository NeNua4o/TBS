namespace Common.Controls
{
    partial class ItemSelector
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
            this.l_name = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_icon)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_icon
            // 
            this.pb_icon.Location = new System.Drawing.Point(0, 0);
            this.pb_icon.Margin = new System.Windows.Forms.Padding(0);
            this.pb_icon.Name = "pb_icon";
            this.pb_icon.Size = new System.Drawing.Size(20, 20);
            this.pb_icon.TabIndex = 0;
            this.pb_icon.TabStop = false;
            this.pb_icon.Click += new System.EventHandler(this.pb_icon_Click);
            // 
            // l_name
            // 
            this.l_name.AutoSize = true;
            this.l_name.Location = new System.Drawing.Point(23, 4);
            this.l_name.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.l_name.Name = "l_name";
            this.l_name.Size = new System.Drawing.Size(35, 13);
            this.l_name.TabIndex = 1;
            this.l_name.Text = "label1";
            this.l_name.Click += new System.EventHandler(this.l_name_Click);
            // 
            // ItemSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.l_name);
            this.Controls.Add(this.pb_icon);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "ItemSelector";
            this.Size = new System.Drawing.Size(200, 20);
            ((System.ComponentModel.ISupportInitialize)(this.pb_icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_icon;
        private System.Windows.Forms.Label l_name;
    }
}
