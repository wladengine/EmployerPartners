namespace EmployerPartners
{
    partial class CardPersonOrganization
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbOrganization = new System.Windows.Forms.ComboBox();
            this.tbposition = new System.Windows.Forms.TextBox();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tbpositionEng = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.btnOrgHandBook = new System.Windows.Forms.Button();
            this.btnRefreshOrgList = new System.Windows.Forms.Button();
            this.btnPosHandBook = new System.Windows.Forms.Button();
            this.btnRefreshPosList = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cbPosition = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnPosEdit = new System.Windows.Forms.Button();
            this.btnPosEngEdit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbSubdivision = new System.Windows.Forms.ComboBox();
            this.btnRefreshSubdivList = new System.Windows.Forms.Button();
            this.btnRefreshSubdivList2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cbSubdivision2 = new System.Windows.Forms.ComboBox();
            this.btnPosEngEdit2 = new System.Windows.Forms.Button();
            this.btnPosEdit2 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnPosHandBook2 = new System.Windows.Forms.Button();
            this.btnRefreshPosList2 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.cbPosition2 = new System.Windows.Forms.ComboBox();
            this.tbpositionEng2 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbposition2 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.chbNotUseInDocs = new System.Windows.Forms.CheckBox();
            this.cbSorting = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Должность в организации:\r\n(ручной ввод)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Организация:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 513);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Комментарий:";
            // 
            // cbOrganization
            // 
            this.cbOrganization.FormattingEnabled = true;
            this.cbOrganization.Location = new System.Drawing.Point(177, 23);
            this.cbOrganization.Name = "cbOrganization";
            this.cbOrganization.Size = new System.Drawing.Size(720, 21);
            this.cbOrganization.TabIndex = 1;
            this.cbOrganization.SelectedIndexChanged += new System.EventHandler(this.cbOrganization_SelectedIndexChanged);
            // 
            // tbposition
            // 
            this.tbposition.Enabled = false;
            this.tbposition.Location = new System.Drawing.Point(177, 208);
            this.tbposition.Multiline = true;
            this.tbposition.Name = "tbposition";
            this.tbposition.Size = new System.Drawing.Size(606, 30);
            this.tbposition.TabIndex = 9;
            // 
            // tbComment
            // 
            this.tbComment.Location = new System.Drawing.Point(177, 513);
            this.tbComment.Multiline = true;
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(720, 30);
            this.tbComment.TabIndex = 14;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(792, 565);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(105, 26);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tbpositionEng
            // 
            this.tbpositionEng.Enabled = false;
            this.tbpositionEng.Location = new System.Drawing.Point(177, 252);
            this.tbpositionEng.Multiline = true;
            this.tbpositionEng.Name = "tbpositionEng";
            this.tbpositionEng.Size = new System.Drawing.Size(606, 30);
            this.tbpositionEng.TabIndex = 11;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.Color.Red;
            this.label26.Location = new System.Drawing.Point(-5, 306);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(11, 13);
            this.label26.TabIndex = 88;
            this.label26.Text = "*";
            // 
            // btnOrgHandBook
            // 
            this.btnOrgHandBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOrgHandBook.Location = new System.Drawing.Point(349, 50);
            this.btnOrgHandBook.Name = "btnOrgHandBook";
            this.btnOrgHandBook.Size = new System.Drawing.Size(154, 24);
            this.btnOrgHandBook.TabIndex = 3;
            this.btnOrgHandBook.Text = "Найти организацию";
            this.btnOrgHandBook.UseVisualStyleBackColor = true;
            this.btnOrgHandBook.Click += new System.EventHandler(this.btnOrgHandBook_Click);
            // 
            // btnRefreshOrgList
            // 
            this.btnRefreshOrgList.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRefreshOrgList.Location = new System.Drawing.Point(177, 50);
            this.btnRefreshOrgList.Name = "btnRefreshOrgList";
            this.btnRefreshOrgList.Size = new System.Drawing.Size(154, 24);
            this.btnRefreshOrgList.TabIndex = 2;
            this.btnRefreshOrgList.Text = "Обновить список орг-ций";
            this.btnRefreshOrgList.UseVisualStyleBackColor = true;
            this.btnRefreshOrgList.Click += new System.EventHandler(this.btnRefreshOrgList_Click);
            // 
            // btnPosHandBook
            // 
            this.btnPosHandBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPosHandBook.Location = new System.Drawing.Point(350, 112);
            this.btnPosHandBook.Name = "btnPosHandBook";
            this.btnPosHandBook.Size = new System.Drawing.Size(154, 24);
            this.btnPosHandBook.TabIndex = 6;
            this.btnPosHandBook.Text = "Найти должность";
            this.btnPosHandBook.UseVisualStyleBackColor = true;
            this.btnPosHandBook.Click += new System.EventHandler(this.btnPosHandBook_Click);
            // 
            // btnRefreshPosList
            // 
            this.btnRefreshPosList.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRefreshPosList.Location = new System.Drawing.Point(177, 112);
            this.btnRefreshPosList.Name = "btnRefreshPosList";
            this.btnRefreshPosList.Size = new System.Drawing.Size(155, 24);
            this.btnRefreshPosList.TabIndex = 5;
            this.btnRefreshPosList.Text = "Обновить список должностей";
            this.btnRefreshPosList.UseVisualStyleBackColor = true;
            this.btnRefreshPosList.Click += new System.EventHandler(this.btnRefreshPosList_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(25, 85);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(145, 26);
            this.label9.TabIndex = 86;
            this.label9.Text = "Должность в организации:\r\n(по справочнику)";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbPosition
            // 
            this.cbPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbPosition.FormattingEnabled = true;
            this.cbPosition.Location = new System.Drawing.Point(177, 85);
            this.cbPosition.Name = "cbPosition";
            this.cbPosition.Size = new System.Drawing.Size(720, 21);
            this.cbPosition.TabIndex = 4;
            this.cbPosition.SelectedIndexChanged += new System.EventHandler(this.cbPosition_SelectedIndexChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label27.Location = new System.Drawing.Point(12, 275);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(146, 12);
            this.label27.TabIndex = 89;
            this.label27.Text = "(используется для детализации)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(20, 234);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 12);
            this.label5.TabIndex = 89;
            this.label5.Text = "(используется для детализации)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-2, 250);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(177, 26);
            this.label6.TabIndex = 4;
            this.label6.Text = "Должность в организации (англ):\r\n(ручной ввод)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnPosEdit
            // 
            this.btnPosEdit.Location = new System.Drawing.Point(792, 208);
            this.btnPosEdit.Name = "btnPosEdit";
            this.btnPosEdit.Size = new System.Drawing.Size(105, 26);
            this.btnPosEdit.TabIndex = 10;
            this.btnPosEdit.Text = "Редактировать";
            this.btnPosEdit.UseVisualStyleBackColor = true;
            this.btnPosEdit.Click += new System.EventHandler(this.btnPosEdit_Click);
            // 
            // btnPosEngEdit
            // 
            this.btnPosEngEdit.Location = new System.Drawing.Point(792, 252);
            this.btnPosEngEdit.Name = "btnPosEngEdit";
            this.btnPosEngEdit.Size = new System.Drawing.Size(105, 26);
            this.btnPosEngEdit.TabIndex = 12;
            this.btnPosEngEdit.Text = "Редактировать";
            this.btnPosEngEdit.UseVisualStyleBackColor = true;
            this.btnPosEngEdit.Click += new System.EventHandler(this.btnPosEngEdit_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(518, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(273, 24);
            this.label4.TabIndex = 92;
            this.label4.Text = "При отсутствии оргганизации в списке добавить орг-цию через \r\nГлавное меню => Спи" +
    "ски = > Список организаций";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(3, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(167, 13);
            this.label7.TabIndex = 94;
            this.label7.Text = "Подразделение в организации:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbSubdivision
            // 
            this.cbSubdivision.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbSubdivision.FormattingEnabled = true;
            this.cbSubdivision.Location = new System.Drawing.Point(177, 147);
            this.cbSubdivision.Name = "cbSubdivision";
            this.cbSubdivision.Size = new System.Drawing.Size(720, 21);
            this.cbSubdivision.TabIndex = 7;
            // 
            // btnRefreshSubdivList
            // 
            this.btnRefreshSubdivList.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRefreshSubdivList.Location = new System.Drawing.Point(177, 176);
            this.btnRefreshSubdivList.Name = "btnRefreshSubdivList";
            this.btnRefreshSubdivList.Size = new System.Drawing.Size(155, 24);
            this.btnRefreshSubdivList.TabIndex = 8;
            this.btnRefreshSubdivList.Text = "Обновить список подразделений";
            this.btnRefreshSubdivList.UseVisualStyleBackColor = true;
            this.btnRefreshSubdivList.Click += new System.EventHandler(this.btnRefreshSubdivList_Click);
            // 
            // btnRefreshSubdivList2
            // 
            this.btnRefreshSubdivList2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRefreshSubdivList2.ForeColor = System.Drawing.Color.Teal;
            this.btnRefreshSubdivList2.Location = new System.Drawing.Point(177, 389);
            this.btnRefreshSubdivList2.Name = "btnRefreshSubdivList2";
            this.btnRefreshSubdivList2.Size = new System.Drawing.Size(155, 24);
            this.btnRefreshSubdivList2.TabIndex = 101;
            this.btnRefreshSubdivList2.Text = "Обновить список подразделений";
            this.btnRefreshSubdivList2.UseVisualStyleBackColor = true;
            this.btnRefreshSubdivList2.Click += new System.EventHandler(this.btnRefreshSubdivList2_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.Color.Teal;
            this.label8.Location = new System.Drawing.Point(3, 363);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(167, 13);
            this.label8.TabIndex = 109;
            this.label8.Text = "Подразделение в организации:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbSubdivision2
            // 
            this.cbSubdivision2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbSubdivision2.FormattingEnabled = true;
            this.cbSubdivision2.Location = new System.Drawing.Point(177, 360);
            this.cbSubdivision2.Name = "cbSubdivision2";
            this.cbSubdivision2.Size = new System.Drawing.Size(720, 21);
            this.cbSubdivision2.TabIndex = 100;
            // 
            // btnPosEngEdit2
            // 
            this.btnPosEngEdit2.ForeColor = System.Drawing.Color.Teal;
            this.btnPosEngEdit2.Location = new System.Drawing.Point(792, 465);
            this.btnPosEngEdit2.Name = "btnPosEngEdit2";
            this.btnPosEngEdit2.Size = new System.Drawing.Size(105, 26);
            this.btnPosEngEdit2.TabIndex = 105;
            this.btnPosEngEdit2.Text = "Редактировать";
            this.btnPosEngEdit2.UseVisualStyleBackColor = true;
            this.btnPosEngEdit2.Click += new System.EventHandler(this.btnPosEngEdit2_Click);
            // 
            // btnPosEdit2
            // 
            this.btnPosEdit2.ForeColor = System.Drawing.Color.Teal;
            this.btnPosEdit2.Location = new System.Drawing.Point(792, 421);
            this.btnPosEdit2.Name = "btnPosEdit2";
            this.btnPosEdit2.Size = new System.Drawing.Size(105, 26);
            this.btnPosEdit2.TabIndex = 103;
            this.btnPosEdit2.Text = "Редактировать";
            this.btnPosEdit2.UseVisualStyleBackColor = true;
            this.btnPosEdit2.Click += new System.EventHandler(this.btnPosEdit2_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.ForeColor = System.Drawing.Color.Teal;
            this.label10.Location = new System.Drawing.Point(20, 447);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(146, 12);
            this.label10.TabIndex = 107;
            this.label10.Text = "(используется для детализации)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.ForeColor = System.Drawing.Color.Teal;
            this.label11.Location = new System.Drawing.Point(12, 488);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(146, 12);
            this.label11.TabIndex = 108;
            this.label11.Text = "(используется для детализации)";
            // 
            // btnPosHandBook2
            // 
            this.btnPosHandBook2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPosHandBook2.ForeColor = System.Drawing.Color.Teal;
            this.btnPosHandBook2.Location = new System.Drawing.Point(350, 325);
            this.btnPosHandBook2.Name = "btnPosHandBook2";
            this.btnPosHandBook2.Size = new System.Drawing.Size(154, 24);
            this.btnPosHandBook2.TabIndex = 99;
            this.btnPosHandBook2.Text = "Найти должность";
            this.btnPosHandBook2.UseVisualStyleBackColor = true;
            this.btnPosHandBook2.Click += new System.EventHandler(this.btnPosHandBook2_Click);
            // 
            // btnRefreshPosList2
            // 
            this.btnRefreshPosList2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRefreshPosList2.ForeColor = System.Drawing.Color.Teal;
            this.btnRefreshPosList2.Location = new System.Drawing.Point(177, 325);
            this.btnRefreshPosList2.Name = "btnRefreshPosList2";
            this.btnRefreshPosList2.Size = new System.Drawing.Size(155, 24);
            this.btnRefreshPosList2.TabIndex = 98;
            this.btnRefreshPosList2.Text = "Обновить список должностей";
            this.btnRefreshPosList2.UseVisualStyleBackColor = true;
            this.btnRefreshPosList2.Click += new System.EventHandler(this.btnRefreshPosList2_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.ForeColor = System.Drawing.Color.Teal;
            this.label12.Location = new System.Drawing.Point(7, 298);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(160, 26);
            this.label12.TabIndex = 106;
            this.label12.Text = "2-я должность в организации:\r\n(по справочнику)";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbPosition2
            // 
            this.cbPosition2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbPosition2.FormattingEnabled = true;
            this.cbPosition2.Location = new System.Drawing.Point(177, 298);
            this.cbPosition2.Name = "cbPosition2";
            this.cbPosition2.Size = new System.Drawing.Size(720, 21);
            this.cbPosition2.TabIndex = 96;
            this.cbPosition2.SelectedIndexChanged += new System.EventHandler(this.cbPosition2_SelectedIndexChanged);
            // 
            // tbpositionEng2
            // 
            this.tbpositionEng2.Enabled = false;
            this.tbpositionEng2.Location = new System.Drawing.Point(177, 465);
            this.tbpositionEng2.Multiline = true;
            this.tbpositionEng2.Name = "tbpositionEng2";
            this.tbpositionEng2.Size = new System.Drawing.Size(606, 30);
            this.tbpositionEng2.TabIndex = 104;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.ForeColor = System.Drawing.Color.Teal;
            this.label13.Location = new System.Drawing.Point(-2, 463);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(160, 24);
            this.label13.TabIndex = 97;
            this.label13.Text = "2-я должность в организации (англ):\r\n(ручной ввод)";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tbposition2
            // 
            this.tbposition2.Enabled = false;
            this.tbposition2.Location = new System.Drawing.Point(177, 421);
            this.tbposition2.Multiline = true;
            this.tbposition2.Name = "tbposition2";
            this.tbposition2.Size = new System.Drawing.Size(606, 30);
            this.tbposition2.TabIndex = 102;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Teal;
            this.label14.Location = new System.Drawing.Point(6, 421);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(160, 26);
            this.label14.TabIndex = 95;
            this.label14.Text = "2-я должность в организации:\r\n(ручной ввод)";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chbNotUseInDocs
            // 
            this.chbNotUseInDocs.AutoSize = true;
            this.chbNotUseInDocs.Location = new System.Drawing.Point(177, 571);
            this.chbNotUseInDocs.Name = "chbNotUseInDocs";
            this.chbNotUseInDocs.Size = new System.Drawing.Size(185, 17);
            this.chbNotUseInDocs.TabIndex = 110;
            this.chbNotUseInDocs.Text = "Не использовать в документах";
            this.chbNotUseInDocs.UseVisualStyleBackColor = true;
            // 
            // cbSorting
            // 
            this.cbSorting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSorting.FormattingEnabled = true;
            this.cbSorting.Location = new System.Drawing.Point(499, 569);
            this.cbSorting.Name = "cbSorting";
            this.cbSorting.Size = new System.Drawing.Size(63, 21);
            this.cbSorting.TabIndex = 111;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(421, 572);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(67, 13);
            this.label15.TabIndex = 112;
            this.label15.Text = "Сортировка";
            // 
            // CardPersonOrganization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 614);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cbSorting);
            this.Controls.Add(this.chbNotUseInDocs);
            this.Controls.Add(this.btnRefreshSubdivList2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbSubdivision2);
            this.Controls.Add(this.btnPosEngEdit2);
            this.Controls.Add(this.btnPosEdit2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnPosHandBook2);
            this.Controls.Add(this.btnRefreshPosList2);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cbPosition2);
            this.Controls.Add(this.tbpositionEng2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tbposition2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.btnRefreshSubdivList);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbSubdivision);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnPosEngEdit);
            this.Controls.Add(this.btnPosEdit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.btnOrgHandBook);
            this.Controls.Add(this.btnRefreshOrgList);
            this.Controls.Add(this.btnPosHandBook);
            this.Controls.Add(this.btnRefreshPosList);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbPosition);
            this.Controls.Add(this.tbpositionEng);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.tbComment);
            this.Controls.Add(this.tbposition);
            this.Controls.Add(this.cbOrganization);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "CardPersonOrganization";
            this.Text = "Организация - контакты";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cbOrganization;
        public System.Windows.Forms.TextBox tbposition;
        public System.Windows.Forms.TextBox tbComment;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.TextBox tbpositionEng;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button btnOrgHandBook;
        private System.Windows.Forms.Button btnRefreshOrgList;
        private System.Windows.Forms.Button btnPosHandBook;
        private System.Windows.Forms.Button btnRefreshPosList;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbPosition;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Button btnPosEdit;
        public System.Windows.Forms.Button btnPosEngEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbSubdivision;
        private System.Windows.Forms.Button btnRefreshSubdivList;
        private System.Windows.Forms.Button btnRefreshSubdivList2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbSubdivision2;
        public System.Windows.Forms.Button btnPosEngEdit2;
        public System.Windows.Forms.Button btnPosEdit2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnPosHandBook2;
        private System.Windows.Forms.Button btnRefreshPosList2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbPosition2;
        public System.Windows.Forms.TextBox tbpositionEng2;
        public System.Windows.Forms.Label label13;
        public System.Windows.Forms.TextBox tbposition2;
        public System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox chbNotUseInDocs;
        private System.Windows.Forms.ComboBox cbSorting;
        private System.Windows.Forms.Label label15;
    }
}