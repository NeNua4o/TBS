namespace Common.Controls
{
    partial class ActionEditor
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
            this.lb_actions = new System.Windows.Forms.ListBox();
            this.b_del = new System.Windows.Forms.Button();
            this.b_add = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_actions
            // 
            this.lb_actions.FormattingEnabled = true;
            this.lb_actions.Location = new System.Drawing.Point(0, 0);
            this.lb_actions.Name = "lb_actions";
            this.lb_actions.Size = new System.Drawing.Size(250, 368);
            this.lb_actions.TabIndex = 2;
            this.lb_actions.SelectedIndexChanged += new System.EventHandler(this.lb_actions_SelectedIndexChanged);
            // 
            // b_del
            // 
            this.b_del.Location = new System.Drawing.Point(0, 374);
            this.b_del.Name = "b_del";
            this.b_del.Size = new System.Drawing.Size(60, 23);
            this.b_del.TabIndex = 5;
            this.b_del.Text = "delete";
            this.b_del.UseVisualStyleBackColor = true;
            this.b_del.Click += new System.EventHandler(this.b_del_Click);
            // 
            // b_add
            // 
            this.b_add.Location = new System.Drawing.Point(190, 374);
            this.b_add.Name = "b_add";
            this.b_add.Size = new System.Drawing.Size(60, 23);
            this.b_add.TabIndex = 4;
            this.b_add.Text = "add";
            this.b_add.UseVisualStyleBackColor = true;
            this.b_add.Click += new System.EventHandler(this.b_add_Click);
            // 
            // ActionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.b_del);
            this.Controls.Add(this.b_add);
            this.Controls.Add(this.lb_actions);
            this.Name = "ActionEditor";
            this.Size = new System.Drawing.Size(250, 397);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lb_actions;
        private System.Windows.Forms.Button b_del;
        private System.Windows.Forms.Button b_add;
    }
}
