namespace Common.Controls
{
    partial class TurnControl
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
            this.b_skip = new System.Windows.Forms.Button();
            this.b_def = new System.Windows.Forms.Button();
            this.b_wait = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // b_skip
            // 
            this.b_skip.BackColor = System.Drawing.Color.PapayaWhip;
            this.b_skip.FlatAppearance.BorderSize = 0;
            this.b_skip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b_skip.Location = new System.Drawing.Point(3, 3);
            this.b_skip.Name = "b_skip";
            this.b_skip.Size = new System.Drawing.Size(30, 30);
            this.b_skip.TabIndex = 0;
            this.b_skip.UseVisualStyleBackColor = false;
            this.b_skip.Click += new System.EventHandler(this.b_skip_Click);
            // 
            // b_def
            // 
            this.b_def.BackColor = System.Drawing.Color.PapayaWhip;
            this.b_def.FlatAppearance.BorderSize = 0;
            this.b_def.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b_def.Location = new System.Drawing.Point(61, 3);
            this.b_def.Name = "b_def";
            this.b_def.Size = new System.Drawing.Size(30, 30);
            this.b_def.TabIndex = 1;
            this.b_def.UseVisualStyleBackColor = false;
            // 
            // b_wait
            // 
            this.b_wait.BackColor = System.Drawing.Color.PapayaWhip;
            this.b_wait.FlatAppearance.BorderSize = 0;
            this.b_wait.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b_wait.Location = new System.Drawing.Point(117, 3);
            this.b_wait.Name = "b_wait";
            this.b_wait.Size = new System.Drawing.Size(30, 30);
            this.b_wait.TabIndex = 2;
            this.b_wait.UseVisualStyleBackColor = false;
            this.b_wait.Click += new System.EventHandler(this.b_wait_Click);
            // 
            // TurnControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Moccasin;
            this.Controls.Add(this.b_wait);
            this.Controls.Add(this.b_def);
            this.Controls.Add(this.b_skip);
            this.Name = "TurnControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_skip;
        private System.Windows.Forms.Button b_def;
        private System.Windows.Forms.Button b_wait;
    }
}
