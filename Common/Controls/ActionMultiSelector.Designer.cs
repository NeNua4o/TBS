namespace Common.Controls
{
    partial class ActionMultiSelector
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
            this.b_action = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // b_action
            // 
            this.b_action.BackColor = System.Drawing.Color.DarkGray;
            this.b_action.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.b_action.FlatAppearance.BorderSize = 0;
            this.b_action.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b_action.Location = new System.Drawing.Point(0, 0);
            this.b_action.Margin = new System.Windows.Forms.Padding(0);
            this.b_action.Name = "b_action";
            this.b_action.Size = new System.Drawing.Size(25, 25);
            this.b_action.TabIndex = 1;
            this.b_action.UseVisualStyleBackColor = false;
            // 
            // ActionMultiSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.b_action);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ActionMultiSelector";
            this.Size = new System.Drawing.Size(25, 25);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_action;
    }
}
