namespace Common.Controls
{
    partial class ItemMultiSelector
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
            this.cb_selected = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_icon)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_icon
            // 
            this.pb_icon.Location = new System.Drawing.Point(0, 0);
            this.pb_icon.Margin = new System.Windows.Forms.Padding(0);
            this.pb_icon.Name = "pb_icon";
            this.pb_icon.Size = new System.Drawing.Size(20, 20);
            this.pb_icon.TabIndex = 2;
            this.pb_icon.TabStop = false;
            // 
            // cb_selected
            // 
            this.cb_selected.AutoSize = true;
            this.cb_selected.Location = new System.Drawing.Point(23, 3);
            this.cb_selected.Name = "cb_selected";
            this.cb_selected.Size = new System.Drawing.Size(80, 17);
            this.cb_selected.TabIndex = 3;
            this.cb_selected.Text = "checkBox1";
            this.cb_selected.UseVisualStyleBackColor = true;
            // 
            // ItemMultiSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.cb_selected);
            this.Controls.Add(this.pb_icon);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "ItemMultiSelector";
            this.Size = new System.Drawing.Size(200, 20);
            ((System.ComponentModel.ISupportInitialize)(this.pb_icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pb_icon;
        private System.Windows.Forms.CheckBox cb_selected;
    }
}
