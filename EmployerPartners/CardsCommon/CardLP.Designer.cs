namespace EmployerPartners
{
    partial class CardLP
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.cbName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.lblStudyLevel = new System.Windows.Forms.Label();
            this.lblProgramType = new System.Windows.Forms.Label();
            this.lblQualification = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbLevel = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbProgramType = new System.Windows.Forms.ComboBox();
            this.cbQulification = new System.Windows.Forms.ComboBox();
            this.cbRubric = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(394, 101);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cbName
            // 
            this.cbName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbName.FormattingEnabled = true;
            this.cbName.Location = new System.Drawing.Point(92, 18);
            this.cbName.Name = "cbName";
            this.cbName.Size = new System.Drawing.Size(377, 21);
            this.cbName.TabIndex = 5;
            this.cbName.SelectedIndexChanged += new System.EventHandler(this.cbName_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Направление:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Код:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Уровень:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(89, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Тип программы:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(89, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Квалификация:";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(124, 50);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(29, 13);
            this.lblCode.TabIndex = 7;
            this.lblCode.Text = "Код:";
            // 
            // lblStudyLevel
            // 
            this.lblStudyLevel.AutoSize = true;
            this.lblStudyLevel.Location = new System.Drawing.Point(149, 63);
            this.lblStudyLevel.Name = "lblStudyLevel";
            this.lblStudyLevel.Size = new System.Drawing.Size(54, 13);
            this.lblStudyLevel.TabIndex = 7;
            this.lblStudyLevel.Text = "Уровень:";
            // 
            // lblProgramType
            // 
            this.lblProgramType.AutoSize = true;
            this.lblProgramType.Location = new System.Drawing.Point(186, 76);
            this.lblProgramType.Name = "lblProgramType";
            this.lblProgramType.Size = new System.Drawing.Size(91, 13);
            this.lblProgramType.TabIndex = 7;
            this.lblProgramType.Text = "Тип программы:";
            // 
            // lblQualification
            // 
            this.lblQualification.AutoSize = true;
            this.lblQualification.Location = new System.Drawing.Point(180, 89);
            this.lblQualification.Name = "lblQualification";
            this.lblQualification.Size = new System.Drawing.Size(85, 13);
            this.lblQualification.TabIndex = 7;
            this.lblQualification.Text = "Квалификация:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblQualification);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblProgramType);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblStudyLevel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 164);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 130);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Направление";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Уровень:";
            // 
            // cbLevel
            // 
            this.cbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLevel.FormattingEnabled = true;
            this.cbLevel.Location = new System.Drawing.Point(104, 51);
            this.cbLevel.Name = "cbLevel";
            this.cbLevel.Size = new System.Drawing.Size(383, 21);
            this.cbLevel.TabIndex = 5;
            this.cbLevel.SelectedIndexChanged += new System.EventHandler(this.cbLevel_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Тип программы:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 108);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Квалификация:";
            // 
            // cbProgramType
            // 
            this.cbProgramType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProgramType.FormattingEnabled = true;
            this.cbProgramType.Location = new System.Drawing.Point(104, 78);
            this.cbProgramType.Name = "cbProgramType";
            this.cbProgramType.Size = new System.Drawing.Size(383, 21);
            this.cbProgramType.TabIndex = 5;
            this.cbProgramType.SelectedIndexChanged += new System.EventHandler(this.cbProgramType_SelectedIndexChanged);
            // 
            // cbQulification
            // 
            this.cbQulification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQulification.FormattingEnabled = true;
            this.cbQulification.Location = new System.Drawing.Point(104, 105);
            this.cbQulification.Name = "cbQulification";
            this.cbQulification.Size = new System.Drawing.Size(383, 21);
            this.cbQulification.TabIndex = 5;
            this.cbQulification.SelectedIndexChanged += new System.EventHandler(this.cbQulification_SelectedIndexChanged);
            // 
            // cbRubric
            // 
            this.cbRubric.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRubric.FormattingEnabled = true;
            this.cbRubric.Location = new System.Drawing.Point(104, 12);
            this.cbRubric.MaximumSize = new System.Drawing.Size(340, 0);
            this.cbRubric.Name = "cbRubric";
            this.cbRubric.Size = new System.Drawing.Size(340, 21);
            this.cbRubric.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(46, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Рубрика:";
            // 
            // CardLP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 302);
            this.Controls.Add(this.cbRubric);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbQulification);
            this.Controls.Add(this.cbProgramType);
            this.Controls.Add(this.cbLevel);
            this.Controls.Add(this.label7);
            this.MinimumSize = new System.Drawing.Size(460, 132);
            this.Name = "CardLP";
            this.Text = "Добавить направление";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblStudyLevel;
        private System.Windows.Forms.Label lblProgramType;
        private System.Windows.Forms.Label lblQualification;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbLevel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbProgramType;
        private System.Windows.Forms.ComboBox cbQulification;
        private System.Windows.Forms.ComboBox cbRubric;
        private System.Windows.Forms.Label label9;
    }
}