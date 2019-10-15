namespace Common.Controls
{
    partial class EffectEditor
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
            this.b_del = new System.Windows.Forms.Button();
            this.b_add = new System.Windows.Forms.Button();
            this.lb_effects = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // b_del
            // 
            this.b_del.Location = new System.Drawing.Point(0, 402);
            this.b_del.Name = "b_del";
            this.b_del.Size = new System.Drawing.Size(60, 23);
            this.b_del.TabIndex = 6;
            this.b_del.Text = "delete";
            this.b_del.UseVisualStyleBackColor = true;
            this.b_del.Click += new System.EventHandler(this.b_del_Click);
            // 
            // b_add
            // 
            this.b_add.Location = new System.Drawing.Point(190, 402);
            this.b_add.Name = "b_add";
            this.b_add.Size = new System.Drawing.Size(60, 23);
            this.b_add.TabIndex = 5;
            this.b_add.Text = "add";
            this.b_add.UseVisualStyleBackColor = true;
            this.b_add.Click += new System.EventHandler(this.b_add_Click);
            // 
            // lb_effects
            // 
            this.lb_effects.FormattingEnabled = true;
            this.lb_effects.Location = new System.Drawing.Point(0, 28);
            this.lb_effects.Name = "lb_effects";
            this.lb_effects.Size = new System.Drawing.Size(250, 368);
            this.lb_effects.TabIndex = 4;
            this.lb_effects.SelectedIndexChanged += new System.EventHandler(this.lb_units_SelectedIndexChanged);
            // 
            // EffectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.b_del);
            this.Controls.Add(this.b_add);
            this.Controls.Add(this.lb_effects);
            this.Name = "EffectEditor";
            this.Size = new System.Drawing.Size(250, 427);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_del;
        private System.Windows.Forms.Button b_add;
        private System.Windows.Forms.ListBox lb_effects;
    }
}
