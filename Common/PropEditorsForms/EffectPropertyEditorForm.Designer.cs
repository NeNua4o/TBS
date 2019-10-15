namespace Common.PropEditorsForms
{
    partial class EffectPropertyEditorForm
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
            this.flp_items = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flp_items
            // 
            this.flp_items.AutoScroll = true;
            this.flp_items.AutoSize = true;
            this.flp_items.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flp_items.BackColor = System.Drawing.SystemColors.Control;
            this.flp_items.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_items.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flp_items.Location = new System.Drawing.Point(0, 0);
            this.flp_items.Name = "flp_items";
            this.flp_items.Size = new System.Drawing.Size(284, 261);
            this.flp_items.TabIndex = 0;
            // 
            // EffectPropertyEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.flp_items);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EffectPropertyEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EffectPropertyEditorForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flp_items;
    }
}