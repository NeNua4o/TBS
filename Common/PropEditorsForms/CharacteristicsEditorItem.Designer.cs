namespace Common.PropEditorsForms
{
    partial class CharacteristicsEditorItem
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
            this.cb_charType = new System.Windows.Forms.ComboBox();
            this.tb_val = new System.Windows.Forms.TextBox();
            this.b_ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cb_charType
            // 
            this.cb_charType.FormattingEnabled = true;
            this.cb_charType.Location = new System.Drawing.Point(12, 12);
            this.cb_charType.Name = "cb_charType";
            this.cb_charType.Size = new System.Drawing.Size(121, 21);
            this.cb_charType.TabIndex = 0;
            // 
            // tb_val
            // 
            this.tb_val.Location = new System.Drawing.Point(139, 12);
            this.tb_val.Name = "tb_val";
            this.tb_val.Size = new System.Drawing.Size(100, 20);
            this.tb_val.TabIndex = 1;
            // 
            // b_ok
            // 
            this.b_ok.Location = new System.Drawing.Point(164, 38);
            this.b_ok.Name = "b_ok";
            this.b_ok.Size = new System.Drawing.Size(75, 23);
            this.b_ok.TabIndex = 2;
            this.b_ok.Text = "Добавить";
            this.b_ok.UseVisualStyleBackColor = true;
            this.b_ok.Click += new System.EventHandler(this.b_ok_Click);
            // 
            // CharacteristicsEditorItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 69);
            this.Controls.Add(this.b_ok);
            this.Controls.Add(this.tb_val);
            this.Controls.Add(this.cb_charType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CharacteristicsEditorItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Характеристика";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_charType;
        private System.Windows.Forms.TextBox tb_val;
        private System.Windows.Forms.Button b_ok;
    }
}