namespace Tests
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.b_test = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pb_grad = new System.Windows.Forms.PictureBox();
            this.b_makeGrad = new System.Windows.Forms.Button();
            this.hsR = new System.Windows.Forms.HScrollBar();
            this.hsG = new System.Windows.Forms.HScrollBar();
            this.hsB = new System.Windows.Forms.HScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.pb_grad)).BeginInit();
            this.SuspendLayout();
            // 
            // b_test
            // 
            this.b_test.Location = new System.Drawing.Point(107, 12);
            this.b_test.Name = "b_test";
            this.b_test.Size = new System.Drawing.Size(75, 23);
            this.b_test.TabIndex = 2;
            this.b_test.Text = "click";
            this.b_test.UseVisualStyleBackColor = true;
            this.b_test.Click += new System.EventHandler(this.b_test_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 63);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // pb_grad
            // 
            this.pb_grad.Location = new System.Drawing.Point(7, 66);
            this.pb_grad.Name = "pb_grad";
            this.pb_grad.Size = new System.Drawing.Size(500, 30);
            this.pb_grad.TabIndex = 4;
            this.pb_grad.TabStop = false;
            // 
            // b_makeGrad
            // 
            this.b_makeGrad.Location = new System.Drawing.Point(7, 153);
            this.b_makeGrad.Name = "b_makeGrad";
            this.b_makeGrad.Size = new System.Drawing.Size(75, 23);
            this.b_makeGrad.TabIndex = 5;
            this.b_makeGrad.Text = "button1";
            this.b_makeGrad.UseVisualStyleBackColor = true;
            this.b_makeGrad.Click += new System.EventHandler(this.b_makeGrad_Click);
            // 
            // hsR
            // 
            this.hsR.Location = new System.Drawing.Point(7, 99);
            this.hsR.Maximum = 255;
            this.hsR.Name = "hsR";
            this.hsR.Size = new System.Drawing.Size(500, 17);
            this.hsR.TabIndex = 6;
            // 
            // hsG
            // 
            this.hsG.Location = new System.Drawing.Point(7, 116);
            this.hsG.Maximum = 255;
            this.hsG.Name = "hsG";
            this.hsG.Size = new System.Drawing.Size(500, 17);
            this.hsG.TabIndex = 7;
            // 
            // hsB
            // 
            this.hsB.Location = new System.Drawing.Point(7, 133);
            this.hsB.Maximum = 255;
            this.hsB.Name = "hsB";
            this.hsB.Size = new System.Drawing.Size(500, 17);
            this.hsB.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(519, 415);
            this.Controls.Add(this.hsB);
            this.Controls.Add(this.hsG);
            this.Controls.Add(this.hsR);
            this.Controls.Add(this.b_makeGrad);
            this.Controls.Add(this.pb_grad);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.b_test);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pb_grad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button b_test;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pb_grad;
        private System.Windows.Forms.Button b_makeGrad;
        private System.Windows.Forms.HScrollBar hsR;
        private System.Windows.Forms.HScrollBar hsG;
        private System.Windows.Forms.HScrollBar hsB;
    }
}

