namespace EmployerPartners
{
    partial class ObrazProgramCharacteristicList
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOrgHandBook = new System.Windows.Forms.Button();
            this.btnRefreshOrgList = new System.Windows.Forms.Button();
            this.cbOrganization = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.cbObrazProgram = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbAggregateGroup = new System.Windows.Forms.ComboBox();
            this.cbLicenseProgram = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbStudyLevel = new System.Windows.Forms.ComboBox();
            this.ColRemove = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColRemove});
            this.dgv.Location = new System.Drawing.Point(12, 85);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv.Size = new System.Drawing.Size(1078, 446);
            this.dgv.TabIndex = 0;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnOrgHandBook);
            this.groupBox1.Controls.Add(this.btnRefreshOrgList);
            this.groupBox1.Controls.Add(this.cbOrganization);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Location = new System.Drawing.Point(12, 537);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1078, 107);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Добавление ";
            // 
            // btnOrgHandBook
            // 
            this.btnOrgHandBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOrgHandBook.Location = new System.Drawing.Point(255, 51);
            this.btnOrgHandBook.Name = "btnOrgHandBook";
            this.btnOrgHandBook.Size = new System.Drawing.Size(154, 24);
            this.btnOrgHandBook.TabIndex = 65;
            this.btnOrgHandBook.Text = "Найти организацию";
            this.btnOrgHandBook.UseVisualStyleBackColor = true;
            this.btnOrgHandBook.Click += new System.EventHandler(this.btnOrgHandBook_Click);
            // 
            // btnRefreshOrgList
            // 
            this.btnRefreshOrgList.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRefreshOrgList.Location = new System.Drawing.Point(92, 51);
            this.btnRefreshOrgList.Name = "btnRefreshOrgList";
            this.btnRefreshOrgList.Size = new System.Drawing.Size(154, 24);
            this.btnRefreshOrgList.TabIndex = 64;
            this.btnRefreshOrgList.Text = "Обновить список орг-ций";
            this.btnRefreshOrgList.UseVisualStyleBackColor = true;
            this.btnRefreshOrgList.Click += new System.EventHandler(this.btnRefreshOrgList_Click);
            // 
            // cbOrganization
            // 
            this.cbOrganization.FormattingEnabled = true;
            this.cbOrganization.Location = new System.Drawing.Point(92, 24);
            this.cbOrganization.Name = "cbOrganization";
            this.cbOrganization.Size = new System.Drawing.Size(971, 21);
            this.cbOrganization.TabIndex = 63;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 62;
            this.label2.Text = "Организация:";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(902, 51);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(161, 23);
            this.btnAdd.TabIndex = 61;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(536, 45);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(90, 13);
            this.label14.TabIndex = 60;
            this.label14.Text = "Обр. программа";
            // 
            // cbObrazProgram
            // 
            this.cbObrazProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbObrazProgram.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbObrazProgram.FormattingEnabled = true;
            this.cbObrazProgram.Location = new System.Drawing.Point(632, 42);
            this.cbObrazProgram.Name = "cbObrazProgram";
            this.cbObrazProgram.Size = new System.Drawing.Size(428, 21);
            this.cbObrazProgram.TabIndex = 59;
            this.cbObrazProgram.SelectedIndexChanged += new System.EventHandler(this.cbObrazProgram_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(71, 42);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 13);
            this.label13.TabIndex = 57;
            this.label13.Text = "Укр. группа";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(551, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 13);
            this.label12.TabIndex = 58;
            this.label12.Text = "Направление";
            // 
            // cbAggregateGroup
            // 
            this.cbAggregateGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAggregateGroup.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbAggregateGroup.FormattingEnabled = true;
            this.cbAggregateGroup.Location = new System.Drawing.Point(144, 39);
            this.cbAggregateGroup.Name = "cbAggregateGroup";
            this.cbAggregateGroup.Size = new System.Drawing.Size(371, 21);
            this.cbAggregateGroup.TabIndex = 55;
            this.cbAggregateGroup.SelectedIndexChanged += new System.EventHandler(this.cbAggregateGroup_SelectedIndexChanged);
            // 
            // cbLicenseProgram
            // 
            this.cbLicenseProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLicenseProgram.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbLicenseProgram.FormattingEnabled = true;
            this.cbLicenseProgram.Location = new System.Drawing.Point(632, 12);
            this.cbLicenseProgram.Name = "cbLicenseProgram";
            this.cbLicenseProgram.Size = new System.Drawing.Size(428, 21);
            this.cbLicenseProgram.TabIndex = 56;
            this.cbLicenseProgram.SelectedIndexChanged += new System.EventHandler(this.cbLicenseProgram_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(118, 13);
            this.label11.TabIndex = 54;
            this.label11.Text = "Уровень (СВ, BM, СМ)";
            // 
            // cbStudyLevel
            // 
            this.cbStudyLevel.AllowDrop = true;
            this.cbStudyLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyLevel.FormattingEnabled = true;
            this.cbStudyLevel.Location = new System.Drawing.Point(144, 12);
            this.cbStudyLevel.Name = "cbStudyLevel";
            this.cbStudyLevel.Size = new System.Drawing.Size(277, 21);
            this.cbStudyLevel.TabIndex = 53;
            this.cbStudyLevel.SelectedIndexChanged += new System.EventHandler(this.cbStudyLevel_SelectedIndexChanged);
            // 
            // ColRemove
            // 
            this.ColRemove.HeaderText = "Удалить";
            this.ColRemove.Name = "ColRemove";
            this.ColRemove.ReadOnly = true;
            this.ColRemove.Text = "Удалить";
            this.ColRemove.UseColumnTextForButtonValue = true;
            // 
            // ObrazProgramCharacteristicList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 656);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.cbStudyLevel);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbAggregateGroup);
            this.Controls.Add(this.cbObrazProgram);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cbLicenseProgram);
            this.Controls.Add(this.label12);
            this.Name = "ObrazProgramCharacteristicList";
            this.Text = "Характеристика образовательных программ";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbObrazProgram;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbAggregateGroup;
        private System.Windows.Forms.ComboBox cbLicenseProgram;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbStudyLevel;
        private System.Windows.Forms.Button btnOrgHandBook;
        private System.Windows.Forms.Button btnRefreshOrgList;
        public System.Windows.Forms.ComboBox cbOrganization;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewButtonColumn ColRemove;
    }
}