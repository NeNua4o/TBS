namespace TBS
{
    partial class ArmyEditor
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
            this.teamsTabControl = new System.Windows.Forms.TabControl();
            this.team1tab = new System.Windows.Forms.TabPage();
            this.team2tab = new System.Windows.Forms.TabPage();
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lv_units = new System.Windows.Forms.ListView();
            this.b_apply = new System.Windows.Forms.Button();
            this.armyBrowser1 = new Common.Controls.ArmyBrowser();
            this.armyBrowser2 = new Common.Controls.ArmyBrowser();
            this.teamsTabControl.SuspendLayout();
            this.team1tab.SuspendLayout();
            this.team2tab.SuspendLayout();
            this.tableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // teamsTabControl
            // 
            this.teamsTabControl.Controls.Add(this.team1tab);
            this.teamsTabControl.Controls.Add(this.team2tab);
            this.teamsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.teamsTabControl.Location = new System.Drawing.Point(304, 3);
            this.teamsTabControl.Name = "teamsTabControl";
            this.teamsTabControl.SelectedIndex = 0;
            this.teamsTabControl.Size = new System.Drawing.Size(295, 359);
            this.teamsTabControl.TabIndex = 0;
            // 
            // team1tab
            // 
            this.team1tab.Controls.Add(this.armyBrowser1);
            this.team1tab.Location = new System.Drawing.Point(4, 22);
            this.team1tab.Name = "team1tab";
            this.team1tab.Padding = new System.Windows.Forms.Padding(3);
            this.team1tab.Size = new System.Drawing.Size(383, 333);
            this.team1tab.TabIndex = 0;
            this.team1tab.Text = "Команда слева";
            this.team1tab.UseVisualStyleBackColor = true;
            // 
            // team2tab
            // 
            this.team2tab.Controls.Add(this.armyBrowser2);
            this.team2tab.Location = new System.Drawing.Point(4, 22);
            this.team2tab.Name = "team2tab";
            this.team2tab.Padding = new System.Windows.Forms.Padding(3);
            this.team2tab.Size = new System.Drawing.Size(287, 333);
            this.team2tab.TabIndex = 1;
            this.team2tab.Text = "Команда справа";
            this.team2tab.UseVisualStyleBackColor = true;
            // 
            // tableLayout
            // 
            this.tableLayout.ColumnCount = 2;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayout.Controls.Add(this.teamsTabControl, 1, 0);
            this.tableLayout.Controls.Add(this.lv_units, 0, 0);
            this.tableLayout.Controls.Add(this.b_apply, 1, 1);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(0, 0);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 2;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayout.Size = new System.Drawing.Size(602, 388);
            this.tableLayout.TabIndex = 1;
            // 
            // lv_units
            // 
            this.lv_units.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_units.Location = new System.Drawing.Point(3, 3);
            this.lv_units.Name = "lv_units";
            this.lv_units.Size = new System.Drawing.Size(295, 359);
            this.lv_units.TabIndex = 1;
            this.lv_units.UseCompatibleStateImageBehavior = false;
            // 
            // b_apply
            // 
            this.b_apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.b_apply.Location = new System.Drawing.Point(527, 365);
            this.b_apply.Margin = new System.Windows.Forms.Padding(0);
            this.b_apply.Name = "b_apply";
            this.b_apply.Size = new System.Drawing.Size(75, 23);
            this.b_apply.TabIndex = 2;
            this.b_apply.Text = "Применить";
            this.b_apply.UseVisualStyleBackColor = true;
            // 
            // armyBrowser1
            // 
            this.armyBrowser1.Location = new System.Drawing.Point(6, 6);
            this.armyBrowser1.Name = "armyBrowser1";
            this.armyBrowser1.Size = new System.Drawing.Size(275, 163);
            this.armyBrowser1.TabIndex = 0;
            // 
            // armyBrowser2
            // 
            this.armyBrowser2.Location = new System.Drawing.Point(6, 6);
            this.armyBrowser2.Name = "armyBrowser2";
            this.armyBrowser2.Size = new System.Drawing.Size(275, 163);
            this.armyBrowser2.TabIndex = 0;
            // 
            // ArmyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 388);
            this.Controls.Add(this.tableLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ArmyEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование армий";
            this.teamsTabControl.ResumeLayout(false);
            this.team1tab.ResumeLayout(false);
            this.team2tab.ResumeLayout(false);
            this.tableLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl teamsTabControl;
        private System.Windows.Forms.TabPage team1tab;
        private System.Windows.Forms.TabPage team2tab;
        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.ListView lv_units;
        private System.Windows.Forms.Button b_apply;
        private Common.Controls.ArmyBrowser armyBrowser1;
        private Common.Controls.ArmyBrowser armyBrowser2;
    }
}