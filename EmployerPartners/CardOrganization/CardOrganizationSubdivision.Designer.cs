namespace EmployerPartners
{
    partial class CardOrganizationSubdivision
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNameEng = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblHeader.Location = new System.Drawing.Point(25, 29);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(119, 16);
            this.lblHeader.TabIndex = 11;
            this.lblHeader.Text = "Редактирование";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(623, 171);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 24);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Подразделение (англ.)";
            // 
            // tbNameEng
            // 
            this.tbNameEng.Location = new System.Drawing.Point(135, 121);
            this.tbNameEng.Name = "tbNameEng";
            this.tbNameEng.Size = new System.Drawing.Size(585, 20);
            this.tbNameEng.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Подразделение";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(135, 69);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(585, 20);
            this.tbName.TabIndex = 6;
            // 
            // CardOrganizationSubdivision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 233);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbNameEng);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbName);
            this.Name = "CardOrganizationSubdivision";
            this.Text = "Организация - подразделение";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbNameEng;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbName;
    }
}