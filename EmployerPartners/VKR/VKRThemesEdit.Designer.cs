namespace EmployerPartners
{
    partial class VKRThemesEdit
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VKRThemesEdit));
            this.btnRefreshGrid = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cbLicenseProgram = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbStudyLevel = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnWord = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbCrypt = new System.Windows.Forms.ComboBox();
            this.btnRemoveFilter = new System.Windows.Forms.Button();
            this.btnShowSearchResult = new System.Windows.Forms.Button();
            this.btnSearchPrevious = new System.Windows.Forms.Button();
            this.btnSearchNext = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bindingNavigatorAll = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingSourceAll = new System.Windows.Forms.BindingSource(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.dgvAll = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.cbVKRSourceSelection = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbObrazProgram = new System.Windows.Forms.ComboBox();
            this.btnExcel = new System.Windows.Forms.Button();
            this.cbOrder = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnWord2 = new System.Windows.Forms.Button();
            this.cbAggregateGroup = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.cbYear = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorAll)).BeginInit();
            this.bindingNavigatorAll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAll)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRefreshGrid
            // 
            this.btnRefreshGrid.Location = new System.Drawing.Point(653, 125);
            this.btnRefreshGrid.Name = "btnRefreshGrid";
            this.btnRefreshGrid.Size = new System.Drawing.Size(100, 24);
            this.btnRefreshGrid.TabIndex = 48;
            this.btnRefreshGrid.Text = "Обновить";
            this.btnRefreshGrid.UseVisualStyleBackColor = true;
            this.btnRefreshGrid.Click += new System.EventHandler(this.btnRefreshGrid_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(53, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 47;
            this.label7.Text = "Направление";
            // 
            // cbLicenseProgram
            // 
            this.cbLicenseProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLicenseProgram.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbLicenseProgram.FormattingEnabled = true;
            this.cbLicenseProgram.Location = new System.Drawing.Point(134, 98);
            this.cbLicenseProgram.Name = "cbLicenseProgram";
            this.cbLicenseProgram.Size = new System.Drawing.Size(513, 21);
            this.cbLicenseProgram.TabIndex = 46;
            this.cbLicenseProgram.SelectedIndexChanged += new System.EventHandler(this.cbLicenseProgram_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(314, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 13);
            this.label6.TabIndex = 45;
            this.label6.Text = "Уровень (СВ, BM, СМ)";
            // 
            // cbStudyLevel
            // 
            this.cbStudyLevel.AllowDrop = true;
            this.cbStudyLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudyLevel.FormattingEnabled = true;
            this.cbStudyLevel.Location = new System.Drawing.Point(438, 35);
            this.cbStudyLevel.Name = "cbStudyLevel";
            this.cbStudyLevel.Size = new System.Drawing.Size(209, 21);
            this.cbStudyLevel.TabIndex = 44;
            this.cbStudyLevel.SelectedIndexChanged += new System.EventHandler(this.cbCryptSelection_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnWord);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbCrypt);
            this.groupBox1.Location = new System.Drawing.Point(764, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(363, 60);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Приложение к новому приказу";
            // 
            // btnWord
            // 
            this.btnWord.Location = new System.Drawing.Point(242, 21);
            this.btnWord.Name = "btnWord";
            this.btnWord.Size = new System.Drawing.Size(100, 28);
            this.btnWord.TabIndex = 2;
            this.btnWord.Text = "Word";
            this.btnWord.UseVisualStyleBackColor = true;
            this.btnWord.Click += new System.EventHandler(this.btnWord_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Уровень";
            // 
            // cbCrypt
            // 
            this.cbCrypt.FormattingEnabled = true;
            this.cbCrypt.Location = new System.Drawing.Point(74, 24);
            this.cbCrypt.Name = "cbCrypt";
            this.cbCrypt.Size = new System.Drawing.Size(147, 21);
            this.cbCrypt.TabIndex = 0;
            // 
            // btnRemoveFilter
            // 
            this.btnRemoveFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveFilter.Location = new System.Drawing.Point(795, 625);
            this.btnRemoveFilter.Name = "btnRemoveFilter";
            this.btnRemoveFilter.Size = new System.Drawing.Size(126, 23);
            this.btnRemoveFilter.TabIndex = 41;
            this.btnRemoveFilter.Text = "Убрать фильтр";
            this.btnRemoveFilter.UseVisualStyleBackColor = true;
            this.btnRemoveFilter.Click += new System.EventHandler(this.btnRemoveFilter_Click);
            // 
            // btnShowSearchResult
            // 
            this.btnShowSearchResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShowSearchResult.Location = new System.Drawing.Point(591, 625);
            this.btnShowSearchResult.Name = "btnShowSearchResult";
            this.btnShowSearchResult.Size = new System.Drawing.Size(198, 23);
            this.btnShowSearchResult.TabIndex = 40;
            this.btnShowSearchResult.Text = "Отобразить только найденное";
            this.btnShowSearchResult.UseVisualStyleBackColor = true;
            this.btnShowSearchResult.Click += new System.EventHandler(this.btnShowSearchResult_Click);
            // 
            // btnSearchPrevious
            // 
            this.btnSearchPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearchPrevious.Location = new System.Drawing.Point(693, 600);
            this.btnSearchPrevious.Name = "btnSearchPrevious";
            this.btnSearchPrevious.Size = new System.Drawing.Size(96, 24);
            this.btnSearchPrevious.TabIndex = 39;
            this.btnSearchPrevious.Text = "Найти пред.";
            this.btnSearchPrevious.UseVisualStyleBackColor = true;
            this.btnSearchPrevious.Click += new System.EventHandler(this.btnSearchPrevious_Click);
            // 
            // btnSearchNext
            // 
            this.btnSearchNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearchNext.Location = new System.Drawing.Point(591, 600);
            this.btnSearchNext.Name = "btnSearchNext";
            this.btnSearchNext.Size = new System.Drawing.Size(96, 24);
            this.btnSearchNext.TabIndex = 38;
            this.btnSearchNext.Text = "Найти далее";
            this.btnSearchNext.UseVisualStyleBackColor = true;
            this.btnSearchNext.Click += new System.EventHandler(this.btnSearchNext_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(295, 606);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "Поиск по всем полям";
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbSearch.Location = new System.Drawing.Point(431, 603);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(147, 20);
            this.tbSearch.TabIndex = 36;
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(30, 631);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 12);
            this.label2.TabIndex = 35;
            this.label2.Text = "Click по заголоаку столбца - сортировка";
            // 
            // bindingNavigatorAll
            // 
            this.bindingNavigatorAll.AddNewItem = null;
            this.bindingNavigatorAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bindingNavigatorAll.BindingSource = this.bindingSourceAll;
            this.bindingNavigatorAll.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigatorAll.CountItemFormat = "из {0}";
            this.bindingNavigatorAll.DeleteItem = null;
            this.bindingNavigatorAll.Dock = System.Windows.Forms.DockStyle.None;
            this.bindingNavigatorAll.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.bindingNavigatorAll.Location = new System.Drawing.Point(27, 846);
            this.bindingNavigatorAll.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigatorAll.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigatorAll.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigatorAll.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigatorAll.Name = "bindingNavigatorAll";
            this.bindingNavigatorAll.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigatorAll.Size = new System.Drawing.Size(210, 25);
            this.bindingNavigatorAll.TabIndex = 34;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(36, 22);
            this.bindingNavigatorCountItem.Text = "из {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Общее число элементов";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Переместить в начало";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Переместить назад";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Положение";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Текущее положение";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Переместить вперед";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Переместить в конец";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // dgvAll
            // 
            this.dgvAll.AllowUserToAddRows = false;
            this.dgvAll.AllowUserToDeleteRows = false;
            this.dgvAll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAll.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAll.Location = new System.Drawing.Point(20, 155);
            this.dgvAll.Name = "dgvAll";
            this.dgvAll.ReadOnly = true;
            this.dgvAll.Size = new System.Drawing.Size(1107, 435);
            this.dgvAll.TabIndex = 33;
            this.dgvAll.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAll_CellDoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 50;
            this.label5.Text = "Тема предложена";
            // 
            // cbVKRSourceSelection
            // 
            this.cbVKRSourceSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVKRSourceSelection.FormattingEnabled = true;
            this.cbVKRSourceSelection.Location = new System.Drawing.Point(134, 35);
            this.cbVKRSourceSelection.Name = "cbVKRSourceSelection";
            this.cbVKRSourceSelection.Size = new System.Drawing.Size(151, 21);
            this.cbVKRSourceSelection.TabIndex = 49;
            this.cbVKRSourceSelection.SelectedIndexChanged += new System.EventHandler(this.cbVKRSourceSelection_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Обр. программа";
            // 
            // cbObrazProgram
            // 
            this.cbObrazProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbObrazProgram.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbObrazProgram.FormattingEnabled = true;
            this.cbObrazProgram.Location = new System.Drawing.Point(134, 128);
            this.cbObrazProgram.Name = "cbObrazProgram";
            this.cbObrazProgram.Size = new System.Drawing.Size(513, 21);
            this.cbObrazProgram.TabIndex = 51;
            this.cbObrazProgram.SelectedIndexChanged += new System.EventHandler(this.cbOPSelection_SelectedIndexChanged);
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(653, 95);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(100, 24);
            this.btnExcel.TabIndex = 54;
            this.btnExcel.Text = "Экспорт в Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // cbOrder
            // 
            this.cbOrder.FormattingEnabled = true;
            this.cbOrder.Location = new System.Drawing.Point(74, 24);
            this.cbOrder.Name = "cbOrder";
            this.cbOrder.Size = new System.Drawing.Size(147, 21);
            this.cbOrder.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Приказ\r\n";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnWord2);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cbOrder);
            this.groupBox2.Location = new System.Drawing.Point(764, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(363, 60);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Приложение к существующему приказу";
            // 
            // btnWord2
            // 
            this.btnWord2.Location = new System.Drawing.Point(242, 21);
            this.btnWord2.Name = "btnWord2";
            this.btnWord2.Size = new System.Drawing.Size(100, 28);
            this.btnWord2.TabIndex = 2;
            this.btnWord2.Text = "Word";
            this.btnWord2.UseVisualStyleBackColor = true;
            this.btnWord2.Click += new System.EventHandler(this.btnWord2_Click);
            // 
            // cbAggregateGroup
            // 
            this.cbAggregateGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAggregateGroup.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbAggregateGroup.FormattingEnabled = true;
            this.cbAggregateGroup.Location = new System.Drawing.Point(134, 71);
            this.cbAggregateGroup.Name = "cbAggregateGroup";
            this.cbAggregateGroup.Size = new System.Drawing.Size(513, 21);
            this.cbAggregateGroup.TabIndex = 46;
            this.cbAggregateGroup.SelectedIndexChanged += new System.EventHandler(this.cbAggregateGroup_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(61, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 47;
            this.label8.Text = "Укр. группа";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 600);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(99, 13);
            this.label10.TabIndex = 55;
            this.label10.Text = "Найдено записей:";
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(122, 600);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(13, 13);
            this.lblCount.TabIndex = 55;
            this.lblCount.Text = "0";
            // 
            // cbYear
            // 
            this.cbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYear.FormattingEnabled = true;
            this.cbYear.Location = new System.Drawing.Point(134, 8);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(151, 21);
            this.cbYear.TabIndex = 49;
            this.cbYear.SelectedIndexChanged += new System.EventHandler(this.cbVKRSourceSelection_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(103, 12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 13);
            this.label11.TabIndex = 50;
            this.label11.Text = "Год";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Location = new System.Drawing.Point(927, 625);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 23);
            this.btnAdd.TabIndex = 56;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // VKRThemesEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 661);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbObrazProgram);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbYear);
            this.Controls.Add(this.cbVKRSourceSelection);
            this.Controls.Add(this.btnRefreshGrid);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbAggregateGroup);
            this.Controls.Add(this.cbLicenseProgram);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbStudyLevel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRemoveFilter);
            this.Controls.Add(this.btnShowSearchResult);
            this.Controls.Add(this.btnSearchPrevious);
            this.Controls.Add(this.btnSearchNext);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bindingNavigatorAll);
            this.Controls.Add(this.dgvAll);
            this.Name = "VKRThemesEdit";
            this.Text = "Темы ВКР";
            this.Load += new System.EventHandler(this.VKRThemesEdit_Load);
            this.Shown += new System.EventHandler(this.VKRThemesEdit_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorAll)).EndInit();
            this.bindingNavigatorAll.ResumeLayout(false);
            this.bindingNavigatorAll.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAll)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRefreshGrid;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbLicenseProgram;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbStudyLevel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnWord;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbCrypt;
        private System.Windows.Forms.Button btnRemoveFilter;
        private System.Windows.Forms.Button btnShowSearchResult;
        private System.Windows.Forms.Button btnSearchPrevious;
        private System.Windows.Forms.Button btnSearchNext;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingNavigator bindingNavigatorAll;
        private System.Windows.Forms.BindingSource bindingSourceAll;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.DataGridView dgvAll;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbVKRSourceSelection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbObrazProgram;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbOrder;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnWord2;
        private System.Windows.Forms.ComboBox cbAggregateGroup;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.ComboBox cbYear;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnAdd;
    }
}