namespace Kanji.InputArea.WinFormGUI
{
    partial class WinFormInputForm
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
            this.SuspendLayout();
            // 
            // pnlDrawingArea
            // 
            this.pnlDrawingArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.InputArea_MouseMove);
            this.pnlDrawingArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InputArea_MouseDown);
            this.pnlDrawingArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.InputArea_MouseUp);
            // 
            // WinFormInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(322, 353);
            this.Name = "WinFormInputForm";
            this.ResumeLayout(false);

        }

        #endregion
    }
}
