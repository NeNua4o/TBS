﻿namespace TBS
{
    partial class Main
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
            this.b_editArmies = new System.Windows.Forms.Button();
            this.pb_field = new System.Windows.Forms.PictureBox();
            this.b_reset = new System.Windows.Forms.Button();
            this.pb_debug = new System.Windows.Forms.PictureBox();
            this.b_log = new System.Windows.Forms.Button();
            this.lb_log = new System.Windows.Forms.ListBox();
            this.b_debugDraw = new System.Windows.Forms.Button();
            this.turnControl = new Common.Controls.TurnControl();
            this.currentUnit = new Common.Controls.CurrentUnit();
            this.turnQueue = new Common.Controls.TurnQueue();
            ((System.ComponentModel.ISupportInitialize)(this.pb_field)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_debug)).BeginInit();
            this.SuspendLayout();
            // 
            // b_editArmies
            // 
            this.b_editArmies.Location = new System.Drawing.Point(12, 12);
            this.b_editArmies.Name = "b_editArmies";
            this.b_editArmies.Size = new System.Drawing.Size(75, 23);
            this.b_editArmies.TabIndex = 0;
            this.b_editArmies.Text = "edit armies";
            this.b_editArmies.UseVisualStyleBackColor = true;
            this.b_editArmies.Click += new System.EventHandler(this.b_editArmies_Click);
            // 
            // pb_field
            // 
            this.pb_field.Location = new System.Drawing.Point(170, 41);
            this.pb_field.Name = "pb_field";
            this.pb_field.Size = new System.Drawing.Size(370, 312);
            this.pb_field.TabIndex = 1;
            this.pb_field.TabStop = false;
            this.pb_field.Click += new System.EventHandler(this.pb_field_Click);
            this.pb_field.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb_field_MouseMove);
            // 
            // b_reset
            // 
            this.b_reset.Location = new System.Drawing.Point(93, 12);
            this.b_reset.Name = "b_reset";
            this.b_reset.Size = new System.Drawing.Size(75, 23);
            this.b_reset.TabIndex = 2;
            this.b_reset.Text = "start/reset";
            this.b_reset.UseVisualStyleBackColor = true;
            this.b_reset.Click += new System.EventHandler(this.b_reset_Click);
            // 
            // pb_debug
            // 
            this.pb_debug.BackColor = System.Drawing.Color.White;
            this.pb_debug.Location = new System.Drawing.Point(359, 3);
            this.pb_debug.Name = "pb_debug";
            this.pb_debug.Size = new System.Drawing.Size(2, 312);
            this.pb_debug.TabIndex = 6;
            this.pb_debug.TabStop = false;
            // 
            // b_log
            // 
            this.b_log.Location = new System.Drawing.Point(12, 41);
            this.b_log.Name = "b_log";
            this.b_log.Size = new System.Drawing.Size(75, 23);
            this.b_log.TabIndex = 7;
            this.b_log.Text = "showLog";
            this.b_log.UseVisualStyleBackColor = true;
            this.b_log.Click += new System.EventHandler(this.b_log_Click);
            // 
            // lb_log
            // 
            this.lb_log.FormattingEnabled = true;
            this.lb_log.Location = new System.Drawing.Point(405, 3);
            this.lb_log.Name = "lb_log";
            this.lb_log.Size = new System.Drawing.Size(1, 394);
            this.lb_log.TabIndex = 8;
            // 
            // b_debugDraw
            // 
            this.b_debugDraw.Location = new System.Drawing.Point(12, 70);
            this.b_debugDraw.Name = "b_debugDraw";
            this.b_debugDraw.Size = new System.Drawing.Size(75, 23);
            this.b_debugDraw.TabIndex = 9;
            this.b_debugDraw.Text = "debugDraw";
            this.b_debugDraw.UseVisualStyleBackColor = true;
            this.b_debugDraw.Click += new System.EventHandler(this.b_debugDraw_Click);
            // 
            // turnControl
            // 
            this.turnControl.BackColor = System.Drawing.Color.Moccasin;
            this.turnControl.Location = new System.Drawing.Point(556, 150);
            this.turnControl.Name = "turnControl";
            this.turnControl.Size = new System.Drawing.Size(150, 155);
            this.turnControl.TabIndex = 5;
            // 
            // currentUnit
            // 
            this.currentUnit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.currentUnit.Location = new System.Drawing.Point(10, 185);
            this.currentUnit.Name = "currentUnit";
            this.currentUnit.Size = new System.Drawing.Size(150, 120);
            this.currentUnit.TabIndex = 4;
            // 
            // turnQueue
            // 
            this.turnQueue.AutoSize = true;
            this.turnQueue.BackColor = System.Drawing.Color.Gainsboro;
            this.turnQueue.Location = new System.Drawing.Point(0, 359);
            this.turnQueue.Name = "turnQueue";
            this.turnQueue.Size = new System.Drawing.Size(1005, 85);
            this.turnQueue.TabIndex = 3;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(757, 468);
            this.Controls.Add(this.b_debugDraw);
            this.Controls.Add(this.lb_log);
            this.Controls.Add(this.b_log);
            this.Controls.Add(this.pb_debug);
            this.Controls.Add(this.turnControl);
            this.Controls.Add(this.currentUnit);
            this.Controls.Add(this.turnQueue);
            this.Controls.Add(this.b_reset);
            this.Controls.Add(this.pb_field);
            this.Controls.Add(this.b_editArmies);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            ((System.ComponentModel.ISupportInitialize)(this.pb_field)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_debug)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button b_editArmies;
        private System.Windows.Forms.PictureBox pb_field;
        private System.Windows.Forms.Button b_reset;
        private Common.Controls.TurnQueue turnQueue;
        private Common.Controls.CurrentUnit currentUnit;
        private Common.Controls.TurnControl turnControl;
        private System.Windows.Forms.PictureBox pb_debug;
        private System.Windows.Forms.Button b_log;
        private System.Windows.Forms.ListBox lb_log;
        private System.Windows.Forms.Button b_debugDraw;
    }
}

