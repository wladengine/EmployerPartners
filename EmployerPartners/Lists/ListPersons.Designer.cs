namespace EmployerPartners 
{
    partial class ListPersons
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListPersons));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnAddPartner = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cbRank = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbDegree = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbActivityArea = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbRegion = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbCountry = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnXLS = new System.Windows.Forms.Button();
            this.cbRubric = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbFaculty = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbRankState = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.cbRankHonorary = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.chbGAKChairman = new System.Windows.Forms.CheckBox();
            this.chbGAK = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.chbGAKChairman2016 = new System.Windows.Forms.CheckBox();
            this.chbGAK2016 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvGAK = new System.Windows.Forms.DataGridView();
            this.groupBoxGAK = new System.Windows.Forms.GroupBox();
            this.rbtn2017 = new System.Windows.Forms.RadioButton();
            this.rbtn2016 = new System.Windows.Forms.RadioButton();
            this.btnSendLetter = new System.Windows.Forms.Button();
            this.chbIsGAK2017MemberChairman = new System.Windows.Forms.CheckBox();
            this.chbIsGAK2016MemberChairman = new System.Windows.Forms.CheckBox();
            this.chbShowColumnLetter = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGAK)).BeginInit();
            this.groupBoxGAK.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(15, 164);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.Size = new System.Drawing.Size(1331, 400);
            this.dgv.TabIndex = 0;
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            this.dgv.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_CellMouseUp);
            this.dgv.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Location = new System.Drawing.Point(1229, 570);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(117, 24);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Открыть карточку";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnAddPartner
            // 
            this.btnAddPartner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPartner.Enabled = false;
            this.btnAddPartner.Location = new System.Drawing.Point(1227, 113);
            this.btnAddPartner.Name = "btnAddPartner";
            this.btnAddPartner.Size = new System.Drawing.Size(117, 30);
            this.btnAddPartner.TabIndex = 1;
            this.btnAddPartner.Text = "Добавить";
            this.btnAddPartner.UseVisualStyleBackColor = true;
            this.btnAddPartner.Click += new System.EventHandler(this.btnAddPartner_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(825, 123);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(117, 30);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cbRank
            // 
            this.cbRank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRank.FormattingEnabled = true;
            this.cbRank.Location = new System.Drawing.Point(168, 66);
            this.cbRank.Name = "cbRank";
            this.cbRank.Size = new System.Drawing.Size(354, 21);
            this.cbRank.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(76, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ученое звание:";
            // 
            // cbDegree
            // 
            this.cbDegree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDegree.FormattingEnabled = true;
            this.cbDegree.Location = new System.Drawing.Point(168, 39);
            this.cbDegree.Name = "cbDegree";
            this.cbDegree.Size = new System.Drawing.Size(354, 21);
            this.cbDegree.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Ученая степень:";
            // 
            // cbActivityArea
            // 
            this.cbActivityArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbActivityArea.FormattingEnabled = true;
            this.cbActivityArea.Location = new System.Drawing.Point(168, 12);
            this.cbActivityArea.Name = "cbActivityArea";
            this.cbActivityArea.Size = new System.Drawing.Size(354, 21);
            this.cbActivityArea.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Осн. сфера деятельности:";
            // 
            // cbRegion
            // 
            this.cbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRegion.FormattingEnabled = true;
            this.cbRegion.Location = new System.Drawing.Point(626, 39);
            this.cbRegion.Name = "cbRegion";
            this.cbRegion.Size = new System.Drawing.Size(179, 21);
            this.cbRegion.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(574, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Регион:";
            // 
            // cbCountry
            // 
            this.cbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCountry.FormattingEnabled = true;
            this.cbCountry.Location = new System.Drawing.Point(626, 12);
            this.cbCountry.Name = "cbCountry";
            this.cbCountry.Size = new System.Drawing.Size(179, 21);
            this.cbCountry.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(574, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Страна:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(676, 576);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(288, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "DoubleClick в любом поле - карточка физического лица";
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bindingNavigator1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bindingNavigator1.BindingSource = this.bindingSource1;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.CountItemFormat = "из {0}";
            this.bindingNavigator1.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.None;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.bindingNavigator1.Location = new System.Drawing.Point(15, 570);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(256, 25);
            this.bindingNavigator1.TabIndex = 18;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Enabled = false;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Добавить";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(36, 22);
            this.bindingNavigatorCountItem.Text = "из {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Общее число элементов";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Enabled = false;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Удалить";
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
            // btnXLS
            // 
            this.btnXLS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXLS.Location = new System.Drawing.Point(1227, 15);
            this.btnXLS.Name = "btnXLS";
            this.btnXLS.Size = new System.Drawing.Size(117, 30);
            this.btnXLS.TabIndex = 19;
            this.btnXLS.Text = "Отчет XLS";
            this.btnXLS.UseVisualStyleBackColor = true;
            this.btnXLS.Click += new System.EventHandler(this.btnXLS_Click);
            // 
            // cbRubric
            // 
            this.cbRubric.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRubric.FormattingEnabled = true;
            this.cbRubric.Location = new System.Drawing.Point(626, 66);
            this.cbRubric.Name = "cbRubric";
            this.cbRubric.Size = new System.Drawing.Size(179, 21);
            this.cbRubric.TabIndex = 10;
            this.cbRubric.SelectedIndexChanged += new System.EventHandler(this.cbRubric_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(568, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Рубрика:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // cbFaculty
            // 
            this.cbFaculty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFaculty.FormattingEnabled = true;
            this.cbFaculty.ItemHeight = 13;
            this.cbFaculty.Location = new System.Drawing.Point(626, 93);
            this.cbFaculty.Name = "cbFaculty";
            this.cbFaculty.Size = new System.Drawing.Size(179, 21);
            this.cbFaculty.TabIndex = 22;
            this.cbFaculty.SelectedIndexChanged += new System.EventHandler(this.cbFaculty_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(586, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "УНП:";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbSearch.Location = new System.Drawing.Point(540, 574);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(100, 20);
            this.tbSearch.TabIndex = 24;
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(334, 576);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(181, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Поиск (ФИО, ФИО англ., Рег. № )";
            // 
            // cbRankState
            // 
            this.cbRankState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRankState.FormattingEnabled = true;
            this.cbRankState.Location = new System.Drawing.Point(168, 122);
            this.cbRankState.Name = "cbRankState";
            this.cbRankState.Size = new System.Drawing.Size(354, 21);
            this.cbRankState.TabIndex = 7;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(43, 118);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(113, 26);
            this.label30.TabIndex = 48;
            this.label30.Text = "Гоударственное или \r\nвоенное  звание:";
            // 
            // cbRankHonorary
            // 
            this.cbRankHonorary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRankHonorary.FormattingEnabled = true;
            this.cbRankHonorary.Location = new System.Drawing.Point(168, 93);
            this.cbRankHonorary.Name = "cbRankHonorary";
            this.cbRankHonorary.Size = new System.Drawing.Size(354, 21);
            this.cbRankHonorary.TabIndex = 6;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(60, 96);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(97, 13);
            this.label25.TabIndex = 47;
            this.label25.Text = "Почетное звание:";
            // 
            // chbGAKChairman
            // 
            this.chbGAKChairman.AutoSize = true;
            this.chbGAKChairman.Location = new System.Drawing.Point(1007, 60);
            this.chbGAKChairman.Name = "chbGAKChairman";
            this.chbGAKChairman.Size = new System.Drawing.Size(147, 17);
            this.chbGAKChairman.TabIndex = 50;
            this.chbGAKChairman.Text = "председатели ГЭК 2017";
            this.chbGAKChairman.UseVisualStyleBackColor = true;
            this.chbGAKChairman.CheckedChanged += new System.EventHandler(this.chbGAKChairman_CheckedChanged);
            // 
            // chbGAK
            // 
            this.chbGAK.AutoSize = true;
            this.chbGAK.Location = new System.Drawing.Point(1007, 39);
            this.chbGAK.Name = "chbGAK";
            this.chbGAK.Size = new System.Drawing.Size(107, 17);
            this.chbGAK.TabIndex = 49;
            this.chbGAK.Text = "члены ГЭК 2017";
            this.chbGAK.UseVisualStyleBackColor = true;
            this.chbGAK.CheckedChanged += new System.EventHandler(this.chbGAK_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(165, 148);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(319, 12);
            this.label10.TabIndex = 51;
            this.label10.Text = "После установки критериев отбора воспользоваться кнопкой \"Обновить\"";
            // 
            // chbGAKChairman2016
            // 
            this.chbGAKChairman2016.AutoSize = true;
            this.chbGAKChairman2016.Location = new System.Drawing.Point(992, 83);
            this.chbGAKChairman2016.Name = "chbGAKChairman2016";
            this.chbGAKChairman2016.Size = new System.Drawing.Size(150, 17);
            this.chbGAKChairman2016.TabIndex = 53;
            this.chbGAKChairman2016.Text = "председатели ГЭК 2016 ";
            this.chbGAKChairman2016.UseVisualStyleBackColor = true;
            this.chbGAKChairman2016.Visible = false;
            this.chbGAKChairman2016.CheckedChanged += new System.EventHandler(this.chbGAKChairman2016_CheckedChanged);
            // 
            // chbGAK2016
            // 
            this.chbGAK2016.AutoSize = true;
            this.chbGAK2016.Location = new System.Drawing.Point(979, 83);
            this.chbGAK2016.Name = "chbGAK2016";
            this.chbGAK2016.Size = new System.Drawing.Size(119, 17);
            this.chbGAK2016.TabIndex = 52;
            this.chbGAK2016.Text = "составы ГЭК 2016";
            this.chbGAK2016.UseVisualStyleBackColor = true;
            this.chbGAK2016.Visible = false;
            this.chbGAK2016.CheckedChanged += new System.EventHandler(this.chbGAK2016_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 30);
            this.button1.TabIndex = 54;
            this.button1.Text = "XLS выгрузка ГЭК";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvGAK
            // 
            this.dgvGAK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGAK.Location = new System.Drawing.Point(979, 575);
            this.dgvGAK.Name = "dgvGAK";
            this.dgvGAK.Size = new System.Drawing.Size(72, 19);
            this.dgvGAK.TabIndex = 55;
            this.dgvGAK.Visible = false;
            // 
            // groupBoxGAK
            // 
            this.groupBoxGAK.Controls.Add(this.rbtn2017);
            this.groupBoxGAK.Controls.Add(this.rbtn2016);
            this.groupBoxGAK.Controls.Add(this.button1);
            this.groupBoxGAK.Location = new System.Drawing.Point(825, 12);
            this.groupBoxGAK.Name = "groupBoxGAK";
            this.groupBoxGAK.Size = new System.Drawing.Size(139, 75);
            this.groupBoxGAK.TabIndex = 56;
            this.groupBoxGAK.TabStop = false;
            this.groupBoxGAK.Text = "Составы ГЭК";
            this.groupBoxGAK.Visible = false;
            // 
            // rbtn2017
            // 
            this.rbtn2017.AutoSize = true;
            this.rbtn2017.Location = new System.Drawing.Point(82, 48);
            this.rbtn2017.Name = "rbtn2017";
            this.rbtn2017.Size = new System.Drawing.Size(49, 17);
            this.rbtn2017.TabIndex = 56;
            this.rbtn2017.Text = "2017";
            this.rbtn2017.UseVisualStyleBackColor = true;
            // 
            // rbtn2016
            // 
            this.rbtn2016.AutoSize = true;
            this.rbtn2016.Checked = true;
            this.rbtn2016.Location = new System.Drawing.Point(20, 48);
            this.rbtn2016.Name = "rbtn2016";
            this.rbtn2016.Size = new System.Drawing.Size(49, 17);
            this.rbtn2016.TabIndex = 55;
            this.rbtn2016.TabStop = true;
            this.rbtn2016.Text = "2016";
            this.rbtn2016.UseVisualStyleBackColor = true;
            // 
            // btnSendLetter
            // 
            this.btnSendLetter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendLetter.Location = new System.Drawing.Point(1227, 48);
            this.btnSendLetter.Name = "btnSendLetter";
            this.btnSendLetter.Size = new System.Drawing.Size(117, 39);
            this.btnSendLetter.TabIndex = 19;
            this.btnSendLetter.Text = "Благодарственное письмо";
            this.btnSendLetter.UseVisualStyleBackColor = true;
            this.btnSendLetter.Click += new System.EventHandler(this.btnSendLetter_Click);
            // 
            // chbIsGAK2017MemberChairman
            // 
            this.chbIsGAK2017MemberChairman.AutoSize = true;
            this.chbIsGAK2017MemberChairman.Location = new System.Drawing.Point(979, 16);
            this.chbIsGAK2017MemberChairman.Name = "chbIsGAK2017MemberChairman";
            this.chbIsGAK2017MemberChairman.Size = new System.Drawing.Size(158, 17);
            this.chbIsGAK2017MemberChairman.TabIndex = 57;
            this.chbIsGAK2017MemberChairman.Text = "Входит в состав ГЭК 2017";
            this.chbIsGAK2017MemberChairman.UseVisualStyleBackColor = true;
            this.chbIsGAK2017MemberChairman.CheckedChanged += new System.EventHandler(this.chbIsGAK2017MemberChairman_CheckedChanged);
            // 
            // chbIsGAK2016MemberChairman
            // 
            this.chbIsGAK2016MemberChairman.AutoSize = true;
            this.chbIsGAK2016MemberChairman.Location = new System.Drawing.Point(979, 83);
            this.chbIsGAK2016MemberChairman.Name = "chbIsGAK2016MemberChairman";
            this.chbIsGAK2016MemberChairman.Size = new System.Drawing.Size(158, 17);
            this.chbIsGAK2016MemberChairman.TabIndex = 57;
            this.chbIsGAK2016MemberChairman.Text = "Входит в состав ГЭК 2016";
            this.chbIsGAK2016MemberChairman.UseVisualStyleBackColor = true;
            this.chbIsGAK2016MemberChairman.Visible = false;
            this.chbIsGAK2016MemberChairman.CheckedChanged += new System.EventHandler(this.chbIsGAK2016MemberChairman_CheckedChanged);
            // 
            // chbShowColumnLetter
            // 
            this.chbShowColumnLetter.Location = new System.Drawing.Point(979, 117);
            this.chbShowColumnLetter.Name = "chbShowColumnLetter";
            this.chbShowColumnLetter.Size = new System.Drawing.Size(210, 41);
            this.chbShowColumnLetter.TabIndex = 58;
            this.chbShowColumnLetter.Text = "Выводить столбцы с отметкой отправки письма";
            this.chbShowColumnLetter.UseVisualStyleBackColor = true;
            // 
            // ListPersons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1356, 602);
            this.Controls.Add(this.chbShowColumnLetter);
            this.Controls.Add(this.chbIsGAK2016MemberChairman);
            this.Controls.Add(this.chbIsGAK2017MemberChairman);
            this.Controls.Add(this.groupBoxGAK);
            this.Controls.Add(this.dgvGAK);
            this.Controls.Add(this.chbGAKChairman2016);
            this.Controls.Add(this.chbGAK2016);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.chbGAKChairman);
            this.Controls.Add(this.chbGAK);
            this.Controls.Add(this.cbRankState);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.cbRankHonorary);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbFaculty);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbRubric);
            this.Controls.Add(this.btnSendLetter);
            this.Controls.Add(this.btnXLS);
            this.Controls.Add(this.bindingNavigator1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbRegion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbCountry);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbRank);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbDegree);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbActivityArea);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddPartner);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.dgv);
            this.Name = "ListPersons";
            this.Text = "ListPersons";
            this.Load += new System.EventHandler(this.ListPersons_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGAK)).EndInit();
            this.groupBoxGAK.ResumeLayout(false);
            this.groupBoxGAK.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnAddPartner;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cbRank;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbDegree;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbActivityArea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbRegion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbCountry;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.Button btnXLS;
        private System.Windows.Forms.ComboBox cbRubric;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbFaculty;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbRankState;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ComboBox cbRankHonorary;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.CheckBox chbGAKChairman;
        private System.Windows.Forms.CheckBox chbGAK;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chbGAKChairman2016;
        private System.Windows.Forms.CheckBox chbGAK2016;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgvGAK;
        private System.Windows.Forms.GroupBox groupBoxGAK;
        private System.Windows.Forms.RadioButton rbtn2017;
        private System.Windows.Forms.RadioButton rbtn2016;
        private System.Windows.Forms.Button btnSendLetter;
        private System.Windows.Forms.CheckBox chbIsGAK2017MemberChairman;
        private System.Windows.Forms.CheckBox chbIsGAK2016MemberChairman;
        private System.Windows.Forms.CheckBox chbShowColumnLetter;
    }
}