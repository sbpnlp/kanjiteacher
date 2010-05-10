namespace Kanji.InputArea.WinFormGUI
{
    partial class WinFormInputArea
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
            this.pnlDrawingArea = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.BackColor = System.Drawing.Color.DimGray;
            this.btnReset.Location = new System.Drawing.Point(235, 318);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // pnlDrawingArea
            // 
            this.pnlDrawingArea.BackColor = System.Drawing.Color.White;
            this.pnlDrawingArea.Location = new System.Drawing.Point(10, 10);
            this.pnlDrawingArea.Name = "pnlDrawingArea";
            this.pnlDrawingArea.Size = new System.Drawing.Size(300, 300);
            this.pnlDrawingArea.TabIndex = 3;
            this.pnlDrawingArea.Paint += new System.Windows.Forms.PaintEventHandler(this.InputArea_Paint);
            this.pnlDrawingArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.InputArea_MouseMove);
            this.pnlDrawingArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InputArea_MouseDown);
            this.pnlDrawingArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.InputArea_MouseUp);
            // 
            // WinFormInputArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(322, 353);
            this.Controls.Add(this.pnlDrawingArea);
            this.Controls.Add(this.btnReset);
            this.Name = "WinFormInputArea";
            this.Text = "KanjiInput - 漢字インプト";
            this.Load += new System.EventHandler(this.InputArea_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReset;
        protected System.Windows.Forms.Panel pnlDrawingArea;
    }
}