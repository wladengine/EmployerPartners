namespace EmployerPartners
{
    partial class CardPersonOrganizationLetter
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
            this.rbAllPersonsToOneFile = new System.Windows.Forms.RadioButton();
            this.rbOnePersonToOneFile = new System.Windows.Forms.RadioButton();
            this.rbOneOrganizationToOneFile = new System.Windows.Forms.RadioButton();
            this.btnPrint = new System.Windows.Forms.Button();
            this.pBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // rbAllPersonsToOneFile
            // 
            this.rbAllPersonsToOneFile.AutoSize = true;
            this.rbAllPersonsToOneFile.Location = new System.Drawing.Point(12, 12);
            this.rbAllPersonsToOneFile.Name = "rbAllPersonsToOneFile";
            this.rbAllPersonsToOneFile.Size = new System.Drawing.Size(85, 17);
            this.rbAllPersonsToOneFile.TabIndex = 0;
            this.rbAllPersonsToOneFile.Text = "radioButton1";
            this.rbAllPersonsToOneFile.UseVisualStyleBackColor = true;
            // 
            // rbOnePersonToOneFile
            // 
            this.rbOnePersonToOneFile.AutoSize = true;
            this.rbOnePersonToOneFile.Location = new System.Drawing.Point(12, 35);
            this.rbOnePersonToOneFile.Name = "rbOnePersonToOneFile";
            this.rbOnePersonToOneFile.Size = new System.Drawing.Size(85, 17);
            this.rbOnePersonToOneFile.TabIndex = 0;
            this.rbOnePersonToOneFile.Text = "radioButton1";
            this.rbOnePersonToOneFile.UseVisualStyleBackColor = true;
            // 
            // rbOneOrganizationToOneFile
            // 
            this.rbOneOrganizationToOneFile.AutoSize = true;
            this.rbOneOrganizationToOneFile.Checked = true;
            this.rbOneOrganizationToOneFile.Location = new System.Drawing.Point(12, 58);
            this.rbOneOrganizationToOneFile.Name = "rbOneOrganizationToOneFile";
            this.rbOneOrganizationToOneFile.Size = new System.Drawing.Size(85, 17);
            this.rbOneOrganizationToOneFile.TabIndex = 0;
            this.rbOneOrganizationToOneFile.TabStop = true;
            this.rbOneOrganizationToOneFile.Text = "radioButton1";
            this.rbOneOrganizationToOneFile.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(12, 101);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "Печать";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // pBar1
            // 
            this.pBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pBar1.Location = new System.Drawing.Point(12, 130);
            this.pBar1.Name = "pBar1";
            this.pBar1.Size = new System.Drawing.Size(116, 23);
            this.pBar1.TabIndex = 33;
            this.pBar1.Visible = false;
            // 
            // CardPersonOrganizationLetter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 180);
            this.Controls.Add(this.pBar1);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.rbOneOrganizationToOneFile);
            this.Controls.Add(this.rbOnePersonToOneFile);
            this.Controls.Add(this.rbAllPersonsToOneFile);
            this.Name = "CardPersonOrganizationLetter";
            this.Text = "Настройки печати инф.писем";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbAllPersonsToOneFile;
        private System.Windows.Forms.RadioButton rbOnePersonToOneFile;
        private System.Windows.Forms.RadioButton rbOneOrganizationToOneFile;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ProgressBar pBar1;
    }
}