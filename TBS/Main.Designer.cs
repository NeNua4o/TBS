namespace TBS
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
            this.b_editArmies = new System.Windows.Forms.Button();
            this.pb_field = new System.Windows.Forms.PictureBox();
            this.b_reset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pb_field)).BeginInit();
            this.SuspendLayout();
            // 
            // b_editArmies
            // 
            this.b_editArmies.Location = new System.Drawing.Point(12, 12);
            this.b_editArmies.Name = "b_editArmies";
            this.b_editArmies.Size = new System.Drawing.Size(75, 23);
            this.b_editArmies.TabIndex = 0;
            this.b_editArmies.Text = "edit armies";
            this.b_editArmies.UseVisualStyleBackColor = true;
            this.b_editArmies.Click += new System.EventHandler(this.b_editArmies_Click);
            // 
            // pb_field
            // 
            this.pb_field.Location = new System.Drawing.Point(93, 41);
            this.pb_field.Name = "pb_field";
            this.pb_field.Size = new System.Drawing.Size(370, 312);
            this.pb_field.TabIndex = 1;
            this.pb_field.TabStop = false;
            // 
            // b_reset
            // 
            this.b_reset.Location = new System.Drawing.Point(93, 12);
            this.b_reset.Name = "b_reset";
            this.b_reset.Size = new System.Drawing.Size(75, 23);
            this.b_reset.TabIndex = 2;
            this.b_reset.Text = "start/reset";
            this.b_reset.UseVisualStyleBackColor = true;
            this.b_reset.Click += new System.EventHandler(this.b_reset_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 395);
            this.Controls.Add(this.b_reset);
            this.Controls.Add(this.pb_field);
            this.Controls.Add(this.b_editArmies);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            ((System.ComponentModel.ISupportInitialize)(this.pb_field)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_editArmies;
        private System.Windows.Forms.PictureBox pb_field;
        private System.Windows.Forms.Button b_reset;
    }
}

