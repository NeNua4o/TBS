namespace UnitEditor
{
    partial class UnitEditoMn
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.b_save = new System.Windows.Forms.Button();
            this.unitEditor = new Common.Controls.UnitEditor();
            this.actionEditor = new Common.Controls.ActionEditor();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(269, 465);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.unitEditor);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(261, 439);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Юниты";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.actionEditor);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(261, 439);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Действия";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(261, 439);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Эффекты";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Location = new System.Drawing.Point(271, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGrid.Size = new System.Drawing.Size(527, 425);
            this.propertyGrid.TabIndex = 1;
            this.propertyGrid.UseCompatibleTextRendering = true;
            // 
            // b_save
            // 
            this.b_save.Location = new System.Drawing.Point(714, 431);
            this.b_save.Name = "b_save";
            this.b_save.Size = new System.Drawing.Size(84, 23);
            this.b_save.TabIndex = 2;
            this.b_save.Text = "Сохранить";
            this.b_save.UseVisualStyleBackColor = true;
            this.b_save.Click += new System.EventHandler(this.b_save_Click);
            // 
            // unitEditor
            // 
            this.unitEditor.BackColor = System.Drawing.SystemColors.Control;
            this.unitEditor.Location = new System.Drawing.Point(6, 6);
            this.unitEditor.Name = "unitEditor";
            this.unitEditor.Size = new System.Drawing.Size(250, 427);
            this.unitEditor.TabIndex = 2;
            // 
            // actionEditor
            // 
            this.actionEditor.Location = new System.Drawing.Point(8, 6);
            this.actionEditor.Name = "actionEditor";
            this.actionEditor.Size = new System.Drawing.Size(250, 397);
            this.actionEditor.TabIndex = 0;
            // 
            // UnitEditoMn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 464);
            this.Controls.Add(this.b_save);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.propertyGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "UnitEditoMn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private Common.Controls.UnitEditor unitEditor;
        private System.Windows.Forms.Button b_save;
        private Common.Controls.ActionEditor actionEditor;
    }
}

