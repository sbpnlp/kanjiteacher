﻿namespace Kanji.FirstGraphicsTest
{
    partial class Form1
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
            this.lblAbzisse = new System.Windows.Forms.Label();
            this.lblYWert = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblAbzisse
            // 
            this.lblAbzisse.AutoSize = true;
            this.lblAbzisse.Location = new System.Drawing.Point(18, 28);
            this.lblAbzisse.Name = "lblAbzisse";
            this.lblAbzisse.Size = new System.Drawing.Size(0, 13);
            this.lblAbzisse.TabIndex = 0;
            // 
            // lblYWert
            // 
            this.lblYWert.AutoSize = true;
            this.lblYWert.Location = new System.Drawing.Point(21, 65);
            this.lblYWert.Name = "lblYWert";
            this.lblYWert.Size = new System.Drawing.Size(0, 13);
            this.lblYWert.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.lblYWert);
            this.Controls.Add(this.lblAbzisse);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseLeave += new System.EventHandler(this.Form1_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAbzisse;
        private System.Windows.Forms.Label lblYWert;
    }
}

