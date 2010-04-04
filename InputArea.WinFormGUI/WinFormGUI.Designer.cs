namespace Kanji.InputArea.WinFormGUI
{
    partial class InputArea
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
            this.btnReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.BackColor = System.Drawing.Color.DimGray;
            this.btnReset.Location = new System.Drawing.Point(255, 288);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // InputArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(342, 323);
            this.Controls.Add(this.btnReset);
            this.Name = "WinFormGUI";
            this.Text = "KanjiInput - 漢字インプト";
            this.Load += new System.EventHandler(this.InputArea_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.InputArea_MouseUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.InputArea_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InputArea_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.InputArea_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReset;
    }
}