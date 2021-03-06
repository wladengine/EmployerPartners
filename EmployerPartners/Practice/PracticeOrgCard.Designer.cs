﻿namespace EmployerPartners
{
    partial class PracticeOrgCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PracticeOrgCard));
            this.lblOrg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbOrgName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDateStart = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDateEnd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbOrgAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOrgCard = new System.Windows.Forms.Button();
            this.cbOrgDogovor = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOrgDogovorRefresh = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblOrg
            // 
            this.lblOrg.Location = new System.Drawing.Point(26, 19);
            this.lblOrg.Name = "lblOrg";
            this.lblOrg.Size = new System.Drawing.Size(462, 112);
            this.lblOrg.TabIndex = 0;
            this.lblOrg.Text = resources.GetString("lblOrg.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Организация (печать)";
            // 
            // tbOrgName
            // 
            this.tbOrgName.Location = new System.Drawing.Point(163, 147);
            this.tbOrgName.Name = "tbOrgName";
            this.tbOrgName.Size = new System.Drawing.Size(778, 20);
            this.tbOrgName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Начало практики";
            // 
            // tbDateStart
            // 
            this.tbDateStart.Location = new System.Drawing.Point(163, 191);
            this.tbDateStart.Name = "tbDateStart";
            this.tbDateStart.Size = new System.Drawing.Size(137, 20);
            this.tbDateStart.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 229);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Окончание практики";
            // 
            // tbDateEnd
            // 
            this.tbDateEnd.Location = new System.Drawing.Point(163, 229);
            this.tbDateEnd.Name = "tbDateEnd";
            this.tbDateEnd.Size = new System.Drawing.Size(137, 20);
            this.tbDateEnd.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 271);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Адрес";
            // 
            // tbOrgAddress
            // 
            this.tbOrgAddress.Location = new System.Drawing.Point(163, 268);
            this.tbOrgAddress.Name = "tbOrgAddress";
            this.tbOrgAddress.Size = new System.Drawing.Size(778, 20);
            this.tbOrgAddress.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 388);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Комментарий";
            // 
            // tbComment
            // 
            this.tbComment.Location = new System.Drawing.Point(163, 381);
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(778, 20);
            this.tbComment.TabIndex = 10;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(701, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 24);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(847, 19);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 24);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOrgCard
            // 
            this.btnOrgCard.Location = new System.Drawing.Point(494, 20);
            this.btnOrgCard.Name = "btnOrgCard";
            this.btnOrgCard.Size = new System.Drawing.Size(153, 24);
            this.btnOrgCard.TabIndex = 13;
            this.btnOrgCard.Text = "Карточка организации";
            this.btnOrgCard.UseVisualStyleBackColor = true;
            this.btnOrgCard.Click += new System.EventHandler(this.btnOrgCard_Click);
            // 
            // cbOrgDogovor
            // 
            this.cbOrgDogovor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrgDogovor.FormattingEnabled = true;
            this.cbOrgDogovor.Location = new System.Drawing.Point(163, 306);
            this.cbOrgDogovor.Name = "cbOrgDogovor";
            this.cbOrgDogovor.Size = new System.Drawing.Size(778, 21);
            this.cbOrgDogovor.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 309);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Договор";
            // 
            // btnOrgDogovorRefresh
            // 
            this.btnOrgDogovorRefresh.Location = new System.Drawing.Point(163, 339);
            this.btnOrgDogovorRefresh.Name = "btnOrgDogovorRefresh";
            this.btnOrgDogovorRefresh.Size = new System.Drawing.Size(175, 24);
            this.btnOrgDogovorRefresh.TabIndex = 9;
            this.btnOrgDogovorRefresh.Text = "Обновить список договоров";
            this.btnOrgDogovorRefresh.UseVisualStyleBackColor = true;
            this.btnOrgDogovorRefresh.Click += new System.EventHandler(this.btnOrgDogovorRefresh_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(408, 345);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(387, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "обновление списка договоров после добавления  в карточке организации";
            // 
            // PracticeOrgCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 445);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnOrgDogovorRefresh);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbOrgDogovor);
            this.Controls.Add(this.btnOrgCard);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbComment);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbOrgAddress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbDateEnd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbDateStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbOrgName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblOrg);
            this.Name = "PracticeOrgCard";
            this.Text = "Практика: организация";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOrg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbOrgName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDateStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDateEnd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbOrgAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOrgCard;
        private System.Windows.Forms.ComboBox cbOrgDogovor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOrgDogovorRefresh;
        private System.Windows.Forms.Label label7;
    }
}