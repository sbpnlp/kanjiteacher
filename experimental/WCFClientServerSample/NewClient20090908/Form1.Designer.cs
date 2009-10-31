namespace NewClient20090908
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.calculate = new System.Windows.Forms.Button();
            this.first = new System.Windows.Forms.TextBox();
            this.second = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.answer = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.serviceAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.localIP = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // calculate
            // 
            this.calculate.Location = new System.Drawing.Point(135, 129);
            this.calculate.Name = "calculate";
            this.calculate.Size = new System.Drawing.Size(101, 21);
            this.calculate.TabIndex = 0;
            this.calculate.Text = "Calculate";
            this.calculate.Click += new System.EventHandler(this.calculate_Click);
            // 
            // first
            // 
            this.first.Location = new System.Drawing.Point(29, 102);
            this.first.Name = "first";
            this.first.Size = new System.Drawing.Size(100, 21);
            this.first.TabIndex = 1;
            this.first.Text = "1";
            // 
            // second
            // 
            this.second.Location = new System.Drawing.Point(29, 129);
            this.second.Name = "second";
            this.second.Size = new System.Drawing.Size(100, 21);
            this.second.TabIndex = 2;
            this.second.Text = "1";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(2, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 26);
            this.label3.Text = "+";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // answer
            // 
            this.answer.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.answer.Location = new System.Drawing.Point(29, 161);
            this.answer.Name = "answer";
            this.answer.Size = new System.Drawing.Size(100, 26);
            this.answer.Text = "0";
            this.answer.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(2, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 2);
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // serviceAddress
            // 
            this.serviceAddress.Location = new System.Drawing.Point(106, 3);
            this.serviceAddress.Name = "serviceAddress";
            this.serviceAddress.Size = new System.Drawing.Size(130, 21);
            this.serviceAddress.TabIndex = 7;
            this.serviceAddress.Text = "192.168.178.20";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 26);
            this.label1.Text = "Service IP:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 26);
            this.label2.Text = "My IP:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // localIP
            // 
            this.localIP.Location = new System.Drawing.Point(106, 29);
            this.localIP.Name = "localIP";
            this.localIP.Size = new System.Drawing.Size(130, 21);
            this.localIP.TabIndex = 12;
            this.localIP.Text = "0.0.0.0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.localIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.serviceAddress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.answer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.second);
            this.Controls.Add(this.first);
            this.Controls.Add(this.calculate);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "CalcSvc";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button calculate;
        private System.Windows.Forms.TextBox first;
        private System.Windows.Forms.TextBox second;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label answer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox serviceAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox localIP;
    }
}
