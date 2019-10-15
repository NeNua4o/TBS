namespace Common.Controls
{
    partial class UnitEditor
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
            this.cb_race = new System.Windows.Forms.ComboBox();
            this.lb_units = new System.Windows.Forms.ListBox();
            this.b_add = new System.Windows.Forms.Button();
            this.b_del = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cb_race
            // 
            this.cb_race.FormattingEnabled = true;
            this.cb_race.Location = new System.Drawing.Point(3, 3);
            this.cb_race.Name = "cb_race";
            this.cb_race.Size = new System.Drawing.Size(126, 21);
            this.cb_race.TabIndex = 0;
            // 
            // lb_units
            // 
            this.lb_units.FormattingEnabled = true;
            this.lb_units.Location = new System.Drawing.Point(0, 30);
            this.lb_units.Name = "lb_units";
            this.lb_units.Size = new System.Drawing.Size(250, 368);
            this.lb_units.TabIndex = 1;
            this.lb_units.SelectedIndexChanged += new System.EventHandler(this.lb_units_SelectedIndexChanged);
            // 
            // b_add
            // 
            this.b_add.Location = new System.Drawing.Point(190, 404);
            this.b_add.Name = "b_add";
            this.b_add.Size = new System.Drawing.Size(60, 23);
            this.b_add.TabIndex = 2;
            this.b_add.Text = "add";
            this.b_add.UseVisualStyleBackColor = true;
            this.b_add.Click += new System.EventHandler(this.b_add_Click);
            // 
            // b_del
            // 
            this.b_del.Location = new System.Drawing.Point(0, 404);
            this.b_del.Name = "b_del";
            this.b_del.Size = new System.Drawing.Size(60, 23);
            this.b_del.TabIndex = 3;
            this.b_del.Text = "delete";
            this.b_del.UseVisualStyleBackColor = true;
            this.b_del.Click += new System.EventHandler(this.b_del_Click);
            // 
            // UnitEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.b_del);
            this.Controls.Add(this.b_add);
            this.Controls.Add(this.lb_units);
            this.Controls.Add(this.cb_race);
            this.Name = "UnitEditor";
            this.Size = new System.Drawing.Size(250, 427);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_race;
        private System.Windows.Forms.ListBox lb_units;
        private System.Windows.Forms.Button b_add;
        private System.Windows.Forms.Button b_del;
    }
}
