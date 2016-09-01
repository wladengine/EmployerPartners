namespace EmployerPartners
{
    partial class PracticeStudentCard
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
            this.lblStudent = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tbFIO = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbOrgDogovor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbOrg = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOrgCard = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOrgDogovorRefresh = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblStudent
            // 
            this.lblStudent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStudent.Location = new System.Drawing.Point(24, 30);
            this.lblStudent.Name = "lblStudent";
            this.lblStudent.Size = new System.Drawing.Size(208, 23);
            this.lblStudent.TabIndex = 0;
            this.lblStudent.Text = "Редактирование данных:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(560, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 24);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(688, 30);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 24);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tbFIO
            // 
            this.tbFIO.Location = new System.Drawing.Point(117, 84);
            this.tbFIO.Name = "tbFIO";
            this.tbFIO.Size = new System.Drawing.Size(382, 20);
            this.tbFIO.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "ФИО студента";
            // 
            // tbComment
            // 
            this.tbComment.Location = new System.Drawing.Point(117, 295);
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(665, 20);
            this.tbComment.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 297);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Комментарий";
            // 
            // cbOrgDogovor
            // 
            this.cbOrgDogovor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrgDogovor.FormattingEnabled = true;
            this.cbOrgDogovor.Location = new System.Drawing.Point(117, 189);
            this.cbOrgDogovor.Name = "cbOrgDogovor";
            this.cbOrgDogovor.Size = new System.Drawing.Size(665, 21);
            this.cbOrgDogovor.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Договор";
            // 
            // tbOrg
            // 
            this.tbOrg.Enabled = false;
            this.tbOrg.Location = new System.Drawing.Point(117, 123);
            this.tbOrg.Name = "tbOrg";
            this.tbOrg.Size = new System.Drawing.Size(665, 20);
            this.tbOrg.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Организация";
            // 
            // btnOrgCard
            // 
            this.btnOrgCard.Location = new System.Drawing.Point(117, 154);
            this.btnOrgCard.Name = "btnOrgCard";
            this.btnOrgCard.Size = new System.Drawing.Size(175, 24);
            this.btnOrgCard.TabIndex = 11;
            this.btnOrgCard.Text = "Карточка организации";
            this.btnOrgCard.UseVisualStyleBackColor = true;
            this.btnOrgCard.Click += new System.EventHandler(this.btnOrgCard_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(327, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(419, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "в карточке можно добавить новый договор или отредактировать существующий";
            // 
            // btnOrgDogovorRefresh
            // 
            this.btnOrgDogovorRefresh.Location = new System.Drawing.Point(117, 259);
            this.btnOrgDogovorRefresh.Name = "btnOrgDogovorRefresh";
            this.btnOrgDogovorRefresh.Size = new System.Drawing.Size(175, 24);
            this.btnOrgDogovorRefresh.TabIndex = 13;
            this.btnOrgDogovorRefresh.Text = "Обновить список договоров";
            this.btnOrgDogovorRefresh.UseVisualStyleBackColor = true;
            this.btnOrgDogovorRefresh.Click += new System.EventHandler(this.btnOrgDogovorRefresh_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(327, 265);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(387, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "обновление списка договоров после добавления  в карточке организации";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(114, 223);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(562, 26);
            this.label7.TabIndex = 15;
            this.label7.Text = "Здесь выбирается трудовой договор (соглашение) для данного студента.\r\nОбщий догов" +
    "ор организации с СПбГУ на проведение практики вводится в списке организаций на п" +
    "рактику.";
            // 
            // PracticeStudentCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 349);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnOrgDogovorRefresh);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnOrgCard);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbOrg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbOrgDogovor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbComment);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbFIO);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblStudent);
            this.Name = "PracticeStudentCard";
            this.Text = "Студент";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStudent;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox tbFIO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbOrgDogovor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbOrg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOrgCard;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOrgDogovorRefresh;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}