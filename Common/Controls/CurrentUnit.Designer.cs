namespace Common.Controls
{
    partial class CurrentUnit
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
            this.actionMultiSelectorSkills = new Common.Controls.ActionMultiSelector();
            this.actionSelectorSec = new Common.Controls.ActionSelector();
            this.actionSelectorMain = new Common.Controls.ActionSelector();
            this.actionMultiSelectorSpells = new Common.Controls.ActionMultiSelector();
            ((System.ComponentModel.ISupportInitialize)(this.pb_icon)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_icon
            // 
            this.pb_icon.Location = new System.Drawing.Point(35, 37);
            this.pb_icon.Name = "pb_icon";
            this.pb_icon.Size = new System.Drawing.Size(80, 80);
            this.pb_icon.TabIndex = 0;
            this.pb_icon.TabStop = false;
            // 
            // actionMultiSelectorSkills
            // 
            this.actionMultiSelectorSkills.Location = new System.Drawing.Point(80, 0);
            this.actionMultiSelectorSkills.Margin = new System.Windows.Forms.Padding(0);
            this.actionMultiSelectorSkills.Name = "actionMultiSelectorSkills";
            this.actionMultiSelectorSkills.Size = new System.Drawing.Size(25, 25);
            this.actionMultiSelectorSkills.TabIndex = 7;
            // 
            // actionSelectorSec
            // 
            this.actionSelectorSec.BackColor = System.Drawing.Color.LemonChiffon;
            this.actionSelectorSec.Location = new System.Drawing.Point(45, 0);
            this.actionSelectorSec.Margin = new System.Windows.Forms.Padding(0);
            this.actionSelectorSec.Name = "actionSelectorSec";
            this.actionSelectorSec.Size = new System.Drawing.Size(25, 25);
            this.actionSelectorSec.TabIndex = 6;
            // 
            // actionSelectorMain
            // 
            this.actionSelectorMain.BackColor = System.Drawing.Color.LemonChiffon;
            this.actionSelectorMain.Location = new System.Drawing.Point(10, 0);
            this.actionSelectorMain.Margin = new System.Windows.Forms.Padding(0);
            this.actionSelectorMain.Name = "actionSelectorMain";
            this.actionSelectorMain.Size = new System.Drawing.Size(25, 25);
            this.actionSelectorMain.TabIndex = 5;
            // 
            // actionMultiSelectorSpells
            // 
            this.actionMultiSelectorSpells.Location = new System.Drawing.Point(115, 0);
            this.actionMultiSelectorSpells.Margin = new System.Windows.Forms.Padding(0);
            this.actionMultiSelectorSpells.Name = "actionMultiSelectorSpells";
            this.actionMultiSelectorSpells.Size = new System.Drawing.Size(25, 25);
            this.actionMultiSelectorSpells.TabIndex = 8;
            // 
            // CurrentUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.actionMultiSelectorSpells);
            this.Controls.Add(this.actionMultiSelectorSkills);
            this.Controls.Add(this.actionSelectorSec);
            this.Controls.Add(this.actionSelectorMain);
            this.Controls.Add(this.pb_icon);
            this.Name = "CurrentUnit";
            this.Size = new System.Drawing.Size(150, 120);
            ((System.ComponentModel.ISupportInitialize)(this.pb_icon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_icon;
        private ActionSelector actionSelectorMain;
        private ActionSelector actionSelectorSec;
        private ActionMultiSelector actionMultiSelectorSkills;
        private ActionMultiSelector actionMultiSelectorSpells;
    }
}
