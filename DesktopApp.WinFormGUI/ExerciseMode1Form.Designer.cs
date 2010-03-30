namespace Kanji.DesktopApp.WinFormGUI
{
    partial class ExerciseMode1Form
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenLesson = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCloseLesson = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUser = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLearningProgress = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSwitchUser = new System.Windows.Forms.ToolStripMenuItem();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.grpMode = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnRepeat = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCharacter = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.grpInfo.SuspendLayout();
            this.grpMode.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuUser});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(710, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpenLesson,
            this.mnuCloseLesson,
            this.toolStripSeparator1,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(35, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuOpenLesson
            // 
            this.mnuOpenLesson.Name = "mnuOpenLesson";
            this.mnuOpenLesson.Size = new System.Drawing.Size(136, 22);
            this.mnuOpenLesson.Text = "Open Lesson";
            // 
            // mnuCloseLesson
            // 
            this.mnuCloseLesson.Name = "mnuCloseLesson";
            this.mnuCloseLesson.Size = new System.Drawing.Size(136, 22);
            this.mnuCloseLesson.Text = "Close Lesson";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(136, 22);
            this.mnuExit.Text = "Exit";
            // 
            // mnuUser
            // 
            this.mnuUser.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLearningProgress,
            this.mnuSwitchUser});
            this.mnuUser.Name = "mnuUser";
            this.mnuUser.Size = new System.Drawing.Size(41, 20);
            this.mnuUser.Text = "User";
            // 
            // mnuLearningProgress
            // 
            this.mnuLearningProgress.Name = "mnuLearningProgress";
            this.mnuLearningProgress.Size = new System.Drawing.Size(160, 22);
            this.mnuLearningProgress.Text = "Learning Progress";
            // 
            // mnuSwitchUser
            // 
            this.mnuSwitchUser.Name = "mnuSwitchUser";
            this.mnuSwitchUser.Size = new System.Drawing.Size(160, 22);
            this.mnuSwitchUser.Text = "Switch User";
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.textBox1);
            this.grpInfo.Location = new System.Drawing.Point(341, 27);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(371, 381);
            this.grpInfo.TabIndex = 1;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Character Info";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(6, 19);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(359, 356);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "\r\nPlease write the character that means \'root\'.\r\n\r\n\r\nGood effort!\r\n\r\nThere\'s only" +
                " one stroke missing.\r\n\r\nThe correct character looks like this:\r\n\r\n本";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 409);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(710, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // grpMode
            // 
            this.grpMode.BackColor = System.Drawing.SystemColors.Control;
            this.grpMode.Controls.Add(this.btnReset);
            this.grpMode.Controls.Add(this.btnRepeat);
            this.grpMode.Controls.Add(this.panel1);
            this.grpMode.Controls.Add(this.btnNext);
            this.grpMode.Location = new System.Drawing.Point(0, 27);
            this.grpMode.Name = "grpMode";
            this.grpMode.Size = new System.Drawing.Size(335, 381);
            this.grpMode.TabIndex = 3;
            this.grpMode.TabStop = false;
            this.grpMode.Text = "Character";
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.SystemColors.Control;
            this.btnReset.Location = new System.Drawing.Point(92, 352);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            // 
            // btnRepeat
            // 
            this.btnRepeat.BackColor = System.Drawing.SystemColors.Control;
            this.btnRepeat.Location = new System.Drawing.Point(173, 352);
            this.btnRepeat.Name = "btnRepeat";
            this.btnRepeat.Size = new System.Drawing.Size(75, 23);
            this.btnRepeat.TabIndex = 3;
            this.btnRepeat.Text = "Repeat";
            this.btnRepeat.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblCharacter);
            this.panel1.ForeColor = System.Drawing.Color.LightGray;
            this.panel1.Location = new System.Drawing.Point(6, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(323, 327);
            this.panel1.TabIndex = 2;
            // 
            // lblCharacter
            // 
            this.lblCharacter.AutoSize = true;
            this.lblCharacter.BackColor = System.Drawing.Color.White;
            this.lblCharacter.Font = new System.Drawing.Font("MingLiU", 144F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharacter.ForeColor = System.Drawing.Color.Maroon;
            this.lblCharacter.Location = new System.Drawing.Point(25, 67);
            this.lblCharacter.Name = "lblCharacter";
            this.lblCharacter.Size = new System.Drawing.Size(272, 192);
            this.lblCharacter.TabIndex = 3;
            this.lblCharacter.Text = "本";
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.SystemColors.Control;
            this.btnNext.Location = new System.Drawing.Point(254, 352);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = false;
            // 
            // ExcerciseMode1Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 431);
            this.Controls.Add(this.grpMode);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grpInfo);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ExcerciseMode1Form";
            this.Text = "Kanji Teacher - Lesson 1 - Exercise Mode 1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            this.grpMode.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenLesson;
        private System.Windows.Forms.ToolStripMenuItem mnuCloseLesson;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuUser;
        private System.Windows.Forms.ToolStripMenuItem mnuLearningProgress;
        private System.Windows.Forms.ToolStripMenuItem mnuSwitchUser;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox grpMode;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnRepeat;
        private System.Windows.Forms.Label lblCharacter;
    }
}