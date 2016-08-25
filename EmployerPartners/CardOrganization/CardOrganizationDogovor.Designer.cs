namespace EmployerPartners
{
    partial class CardOrganizationDogovor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardOrganizationDogovor));
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbRubric = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbdocumentType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDocument = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDocumentStart = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDocumentFinish = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxPermanent = new System.Windows.Forms.CheckBox();
            this.tbDocumentNumber = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbDocumentDate = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dgvFile = new System.Windows.Forms.DataGridView();
            this.lblFile = new System.Windows.Forms.Label();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.ColumnDiv1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDelFile = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnViewFile = new System.Windows.Forms.DataGridViewButtonColumn();
            this.checkBoxIsActual = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFile)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblHeader.Location = new System.Drawing.Point(20, 30);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(70, 16);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Договор";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(720, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(114, 24);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(860, 30);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(114, 24);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbRubric
            // 
            this.cbRubric.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRubric.FormattingEnabled = true;
            this.cbRubric.Location = new System.Drawing.Point(148, 67);
            this.cbRubric.Name = "cbRubric";
            this.cbRubric.Size = new System.Drawing.Size(224, 21);
            this.cbRubric.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Рубрика *";
            // 
            // cbdocumentType
            // 
            this.cbdocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbdocumentType.FormattingEnabled = true;
            this.cbdocumentType.Location = new System.Drawing.Point(148, 103);
            this.cbdocumentType.Name = "cbdocumentType";
            this.cbdocumentType.Size = new System.Drawing.Size(136, 21);
            this.cbdocumentType.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Тип документа *";
            // 
            // tbDocument
            // 
            this.tbDocument.Location = new System.Drawing.Point(148, 139);
            this.tbDocument.Name = "tbDocument";
            this.tbDocument.Size = new System.Drawing.Size(828, 20);
            this.tbDocument.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Договор *";
            // 
            // tbDocumentStart
            // 
            this.tbDocumentStart.Location = new System.Drawing.Point(148, 176);
            this.tbDocumentStart.Name = "tbDocumentStart";
            this.tbDocumentStart.Size = new System.Drawing.Size(138, 20);
            this.tbDocumentStart.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Дата начала";
            // 
            // tbDocumentFinish
            // 
            this.tbDocumentFinish.Location = new System.Drawing.Point(148, 212);
            this.tbDocumentFinish.Name = "tbDocumentFinish";
            this.tbDocumentFinish.Size = new System.Drawing.Size(136, 20);
            this.tbDocumentFinish.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 219);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Дата окончания";
            // 
            // checkBoxPermanent
            // 
            this.checkBoxPermanent.AutoSize = true;
            this.checkBoxPermanent.Location = new System.Drawing.Point(148, 247);
            this.checkBoxPermanent.Name = "checkBoxPermanent";
            this.checkBoxPermanent.Size = new System.Drawing.Size(87, 17);
            this.checkBoxPermanent.TabIndex = 13;
            this.checkBoxPermanent.Text = "бессрочный";
            this.checkBoxPermanent.UseVisualStyleBackColor = true;
            // 
            // tbDocumentNumber
            // 
            this.tbDocumentNumber.Location = new System.Drawing.Point(148, 279);
            this.tbDocumentNumber.Name = "tbDocumentNumber";
            this.tbDocumentNumber.Size = new System.Drawing.Size(224, 20);
            this.tbDocumentNumber.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 282);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "№ договора *";
            // 
            // tbDocumentDate
            // 
            this.tbDocumentDate.Location = new System.Drawing.Point(148, 312);
            this.tbDocumentDate.Name = "tbDocumentDate";
            this.tbDocumentDate.Size = new System.Drawing.Size(136, 20);
            this.tbDocumentDate.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 315);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Дата подписания *";
            // 
            // tbComment
            // 
            this.tbComment.Location = new System.Drawing.Point(148, 377);
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(826, 20);
            this.tbComment.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 380);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Примечание";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 451);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(512, 130);
            this.label11.TabIndex = 22;
            this.label11.Text = resources.GetString("label11.Text");
            // 
            // dgvFile
            // 
            this.dgvFile.AllowUserToAddRows = false;
            this.dgvFile.AllowUserToDeleteRows = false;
            this.dgvFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDiv1,
            this.ColumnDelFile,
            this.ColumnViewFile});
            this.dgvFile.Location = new System.Drawing.Point(436, 202);
            this.dgvFile.Name = "dgvFile";
            this.dgvFile.ReadOnly = true;
            this.dgvFile.Size = new System.Drawing.Size(535, 97);
            this.dgvFile.TabIndex = 23;
            this.dgvFile.Visible = false;
            this.dgvFile.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFile_CellClick);
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(434, 179);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(86, 13);
            this.lblFile.TabIndex = 24;
            this.lblFile.Text = "Файл договора";
            this.lblFile.Visible = false;
            // 
            // btnAddFile
            // 
            this.btnAddFile.Location = new System.Drawing.Point(436, 308);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(171, 23);
            this.btnAddFile.TabIndex = 25;
            this.btnAddFile.Text = "Загрузить файл договора";
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Visible = false;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // ColumnDiv1
            // 
            this.ColumnDiv1.Frozen = true;
            this.ColumnDiv1.HeaderText = "";
            this.ColumnDiv1.Name = "ColumnDiv1";
            this.ColumnDiv1.ReadOnly = true;
            this.ColumnDiv1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnDiv1.Width = 5;
            // 
            // ColumnDelFile
            // 
            this.ColumnDelFile.Frozen = true;
            this.ColumnDelFile.HeaderText = "Действие";
            this.ColumnDelFile.Name = "ColumnDelFile";
            this.ColumnDelFile.ReadOnly = true;
            this.ColumnDelFile.Text = "Удалить";
            this.ColumnDelFile.UseColumnTextForButtonValue = true;
            // 
            // ColumnViewFile
            // 
            this.ColumnViewFile.Frozen = true;
            this.ColumnViewFile.HeaderText = "Действие";
            this.ColumnViewFile.Name = "ColumnViewFile";
            this.ColumnViewFile.ReadOnly = true;
            this.ColumnViewFile.Text = "Просмотр";
            this.ColumnViewFile.UseColumnTextForButtonValue = true;
            // 
            // checkBoxIsActual
            // 
            this.checkBoxIsActual.AutoSize = true;
            this.checkBoxIsActual.Location = new System.Drawing.Point(148, 346);
            this.checkBoxIsActual.Name = "checkBoxIsActual";
            this.checkBoxIsActual.Size = new System.Drawing.Size(146, 17);
            this.checkBoxIsActual.TabIndex = 17;
            this.checkBoxIsActual.Text = "действующий документ";
            this.checkBoxIsActual.UseVisualStyleBackColor = true;
            // 
            // CardOrganizationDogovor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 596);
            this.Controls.Add(this.checkBoxIsActual);
            this.Controls.Add(this.btnAddFile);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.dgvFile);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbComment);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbDocumentDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbDocumentNumber);
            this.Controls.Add(this.checkBoxPermanent);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbDocumentFinish);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbDocumentStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbDocument);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbdocumentType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbRubric);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblHeader);
            this.MaximizeBox = false;
            this.Name = "CardOrganizationDogovor";
            this.Text = "Договор";
            ((System.ComponentModel.ISupportInitialize)(this.dgvFile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbRubric;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbdocumentType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDocument;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDocumentStart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDocumentFinish;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxPermanent;
        private System.Windows.Forms.TextBox tbDocumentNumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbDocumentDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dgvFile;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDiv1;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnDelFile;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnViewFile;
        private System.Windows.Forms.CheckBox checkBoxIsActual;
    }
}