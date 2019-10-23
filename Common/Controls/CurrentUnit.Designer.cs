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
            this.pb_curAct = new System.Windows.Forms.PictureBox();
            this.actionSelectorSpells = new Common.Controls.ActionSelector();
            this.actionSelectorSkills = new Common.Controls.ActionSelector();
            this.actionSelectorSec = new Common.Controls.ActionSelector();
            this.actionSelectorMain = new Common.Controls.ActionSelector();
            ((System.ComponentModel.ISupportInitialize)(this.pb_icon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_curAct)).BeginInit();
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
            // pb_curAct
            // 
            this.pb_curAct.Location = new System.Drawing.Point(0, 37);
            this.pb_curAct.Name = "pb_curAct";
            this.pb_curAct.Size = new System.Drawing.Size(30, 30);
            this.pb_curAct.TabIndex = 9;
            this.pb_curAct.TabStop = false;
            // 
            // actionSelectorSpells
            // 
            this.actionSelectorSpells.BackColor = System.Drawing.Color.LemonChiffon;
            this.actionSelectorSpells.Location = new System.Drawing.Point(115, 0);
            this.actionSelectorSpells.Margin = new System.Windows.Forms.Padding(0);
            this.actionSelectorSpells.Name = "actionSelectorSpells";
            this.actionSelectorSpells.Size = new System.Drawing.Size(25, 25);
            this.actionSelectorSpells.TabIndex = 11;
            this.actionSelectorSpells.Click += new System.EventHandler(this.actionSelectorSpells_Click);
            // 
            // actionSelectorSkills
            // 
            this.actionSelectorSkills.BackColor = System.Drawing.Color.LemonChiffon;
            this.actionSelectorSkills.Location = new System.Drawing.Point(80, 0);
            this.actionSelectorSkills.Margin = new System.Windows.Forms.Padding(0);
            this.actionSelectorSkills.Name = "actionSelectorSkills";
            this.actionSelectorSkills.Size = new System.Drawing.Size(25, 25);
            this.actionSelectorSkills.TabIndex = 10;
            this.actionSelectorSkills.Click += new System.EventHandler(this.ActionSelectorSkills_Click);
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
            // CurrentUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.actionSelectorSpells);
            this.Controls.Add(this.actionSelectorSkills);
            this.Controls.Add(this.pb_curAct);
            this.Controls.Add(this.actionSelectorSec);
            this.Controls.Add(this.actionSelectorMain);
            this.Controls.Add(this.pb_icon);
            this.Name = "CurrentUnit";
            this.Size = new System.Drawing.Size(150, 120);
            ((System.ComponentModel.ISupportInitialize)(this.pb_icon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_curAct)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_icon;
        private ActionSelector actionSelectorMain;
        private ActionSelector actionSelectorSec;
        private System.Windows.Forms.PictureBox pb_curAct;
        private ActionSelector actionSelectorSkills;
        private ActionSelector actionSelectorSpells;
    }
}
