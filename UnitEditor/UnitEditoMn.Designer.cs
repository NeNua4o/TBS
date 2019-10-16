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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.unitsPage = new System.Windows.Forms.TabPage();
            this.unitEditor = new Common.Controls.UnitEditor();
            this.actionsPage = new System.Windows.Forms.TabPage();
            this.actionEditor = new Common.Controls.ActionEditor();
            this.effectsPage = new System.Windows.Forms.TabPage();
            this.effectEditor = new Common.Controls.EffectEditor();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.b_save = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.unitsPage.SuspendLayout();
            this.actionsPage.SuspendLayout();
            this.effectsPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.unitsPage);
            this.tabControl.Controls.Add(this.actionsPage);
            this.tabControl.Controls.Add(this.effectsPage);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(269, 465);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // unitsPage
            // 
            this.unitsPage.Controls.Add(this.unitEditor);
            this.unitsPage.Location = new System.Drawing.Point(4, 22);
            this.unitsPage.Name = "unitsPage";
            this.unitsPage.Padding = new System.Windows.Forms.Padding(3);
            this.unitsPage.Size = new System.Drawing.Size(261, 439);
            this.unitsPage.TabIndex = 0;
            this.unitsPage.Text = "Юниты";
            this.unitsPage.UseVisualStyleBackColor = true;
            // 
            // unitEditor
            // 
            this.unitEditor.BackColor = System.Drawing.SystemColors.Control;
            this.unitEditor.Location = new System.Drawing.Point(6, 6);
            this.unitEditor.Name = "unitEditor";
            this.unitEditor.Size = new System.Drawing.Size(250, 427);
            this.unitEditor.TabIndex = 2;
            // 
            // actionsPage
            // 
            this.actionsPage.Controls.Add(this.actionEditor);
            this.actionsPage.Location = new System.Drawing.Point(4, 22);
            this.actionsPage.Name = "actionsPage";
            this.actionsPage.Padding = new System.Windows.Forms.Padding(3);
            this.actionsPage.Size = new System.Drawing.Size(261, 439);
            this.actionsPage.TabIndex = 1;
            this.actionsPage.Text = "Действия";
            this.actionsPage.UseVisualStyleBackColor = true;
            // 
            // actionEditor
            // 
            this.actionEditor.Location = new System.Drawing.Point(5, 6);
            this.actionEditor.Name = "actionEditor";
            this.actionEditor.Size = new System.Drawing.Size(250, 397);
            this.actionEditor.TabIndex = 0;
            // 
            // effectsPage
            // 
            this.effectsPage.Controls.Add(this.effectEditor);
            this.effectsPage.Location = new System.Drawing.Point(4, 22);
            this.effectsPage.Name = "effectsPage";
            this.effectsPage.Padding = new System.Windows.Forms.Padding(3);
            this.effectsPage.Size = new System.Drawing.Size(261, 439);
            this.effectsPage.TabIndex = 2;
            this.effectsPage.Text = "Эффекты";
            this.effectsPage.UseVisualStyleBackColor = true;
            // 
            // effectEditor
            // 
            this.effectEditor.Location = new System.Drawing.Point(5, 6);
            this.effectEditor.Name = "effectEditor";
            this.effectEditor.Size = new System.Drawing.Size(250, 427);
            this.effectEditor.TabIndex = 0;
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
            // UnitEditoMn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 464);
            this.Controls.Add(this.b_save);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.propertyGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "UnitEditoMn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor";
            this.tabControl.ResumeLayout(false);
            this.unitsPage.ResumeLayout(false);
            this.actionsPage.ResumeLayout(false);
            this.effectsPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage unitsPage;
        private System.Windows.Forms.TabPage actionsPage;
        private System.Windows.Forms.TabPage effectsPage;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private Common.Controls.UnitEditor unitEditor;
        private System.Windows.Forms.Button b_save;
        private Common.Controls.ActionEditor actionEditor;
        private Common.Controls.EffectEditor effectEditor;
    }
}

