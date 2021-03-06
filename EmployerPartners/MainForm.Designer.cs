﻿namespace EmployerPartners
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.smiLists = new System.Windows.Forms.ToolStripMenuItem();
            this.smiOrganizationList = new System.Windows.Forms.ToolStripMenuItem();
            this.smiPersonList = new System.Windows.Forms.ToolStripMenuItem();
            this.smiRubric = new System.Windows.Forms.ToolStripMenuItem();
            this.smiPracticeMain = new System.Windows.Forms.ToolStripMenuItem();
            this.smiVKRblock = new System.Windows.Forms.ToolStripMenuItem();
            this.smiVKRAddEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.smiVKRThemesStudent = new System.Windows.Forms.ToolStripMenuItem();
            this.smiVKRThemesAspirant = new System.Windows.Forms.ToolStripMenuItem();
            this.smiVKRMain = new System.Windows.Forms.ToolStripMenuItem();
            this.smiGAK = new System.Windows.Forms.ToolStripMenuItem();
            this.smiGAKLists = new System.Windows.Forms.ToolStripMenuItem();
            this.smiGAKMembers = new System.Windows.Forms.ToolStripMenuItem();
            this.smiSOP = new System.Windows.Forms.ToolStripMenuItem();
            this.smiStatistics = new System.Windows.Forms.ToolStripMenuItem();
            this.smiOrgaanizationStatistics = new System.Windows.Forms.ToolStripMenuItem();
            this.smiGAKStatistics = new System.Windows.Forms.ToolStripMenuItem();
            this.smiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.smiEmailSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.smiWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.smiTables = new System.Windows.Forms.ToolStripMenuItem();
            this.smiDegree = new System.Windows.Forms.ToolStripMenuItem();
            this.smiRank = new System.Windows.Forms.ToolStripMenuItem();
            this.smiPosition = new System.Windows.Forms.ToolStripMenuItem();
            this.smiPositionSOP = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.smiActivityArea = new System.Windows.Forms.ToolStripMenuItem();
            this.smiOwnership = new System.Windows.Forms.ToolStripMenuItem();
            this.smiActivityGoal = new System.Windows.Forms.ToolStripMenuItem();
            this.smiNationalAffiliation = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.smiLPOP = new System.Windows.Forms.ToolStripMenuItem();
            this.smiStudent = new System.Windows.Forms.ToolStripMenuItem();
            this.smiAspirant = new System.Windows.Forms.ToolStripMenuItem();
            this.smiNPR = new System.Windows.Forms.ToolStripMenuItem();
            this.smiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.helpShowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smiNewOrgHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.smiNewOrgHelpLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.tmplToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.TimerSetLastUdateTime = new System.Windows.Forms.Timer(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.smiObrazProgramCharacteristics = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiLists,
            this.smiRubric,
            this.smiStatistics,
            this.smiSettings,
            this.smiWindow,
            this.smiTables,
            this.smiHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.smiWindow;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1182, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // smiLists
            // 
            this.smiLists.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiOrganizationList,
            this.smiPersonList});
            this.smiLists.Name = "smiLists";
            this.smiLists.Size = new System.Drawing.Size(60, 20);
            this.smiLists.Text = "Списки";
            // 
            // smiOrganizationList
            // 
            this.smiOrganizationList.Name = "smiOrganizationList";
            this.smiOrganizationList.Size = new System.Drawing.Size(207, 22);
            this.smiOrganizationList.Text = "Список организаций";
            this.smiOrganizationList.Click += new System.EventHandler(this.smiOrganizationList_Click);
            // 
            // smiPersonList
            // 
            this.smiPersonList.Name = "smiPersonList";
            this.smiPersonList.Size = new System.Drawing.Size(207, 22);
            this.smiPersonList.Text = "Список физических лиц";
            this.smiPersonList.Click += new System.EventHandler(this.smiPersonList_Click);
            // 
            // smiRubric
            // 
            this.smiRubric.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiPracticeMain,
            this.smiVKRblock,
            this.smiGAK,
            this.smiSOP,
            this.smiObrazProgramCharacteristics});
            this.smiRubric.Name = "smiRubric";
            this.smiRubric.Size = new System.Drawing.Size(66, 20);
            this.smiRubric.Text = "Рубрики";
            // 
            // smiPracticeMain
            // 
            this.smiPracticeMain.Enabled = false;
            this.smiPracticeMain.Name = "smiPracticeMain";
            this.smiPracticeMain.Size = new System.Drawing.Size(274, 22);
            this.smiPracticeMain.Text = "Практика (главное окно)";
            this.smiPracticeMain.Click += new System.EventHandler(this.smiPracticeMain_Click);
            // 
            // smiVKRblock
            // 
            this.smiVKRblock.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiVKRAddEdit,
            this.smiVKRThemesStudent,
            this.smiVKRThemesAspirant,
            this.smiVKRMain});
            this.smiVKRblock.Name = "smiVKRblock";
            this.smiVKRblock.Size = new System.Drawing.Size(274, 22);
            this.smiVKRblock.Text = "ВКР";
            // 
            // smiVKRAddEdit
            // 
            this.smiVKRAddEdit.Enabled = false;
            this.smiVKRAddEdit.Name = "smiVKRAddEdit";
            this.smiVKRAddEdit.Size = new System.Drawing.Size(282, 22);
            this.smiVKRAddEdit.Text = "Темы ВКР ";
            this.smiVKRAddEdit.Click += new System.EventHandler(this.smiVKRAddEdit_Click);
            // 
            // smiVKRThemesStudent
            // 
            this.smiVKRThemesStudent.Enabled = false;
            this.smiVKRThemesStudent.Name = "smiVKRThemesStudent";
            this.smiVKRThemesStudent.Size = new System.Drawing.Size(282, 22);
            this.smiVKRThemesStudent.Text = "Студенты - темы ВКР";
            this.smiVKRThemesStudent.Click += new System.EventHandler(this.smiVKRThemesStudent_Click);
            // 
            // smiVKRThemesAspirant
            // 
            this.smiVKRThemesAspirant.Enabled = false;
            this.smiVKRThemesAspirant.Name = "smiVKRThemesAspirant";
            this.smiVKRThemesAspirant.Size = new System.Drawing.Size(282, 22);
            this.smiVKRThemesAspirant.Text = "Аспиранты - темы ВКР";
            this.smiVKRThemesAspirant.Visible = false;
            this.smiVKRThemesAspirant.Click += new System.EventHandler(this.smiVKRThemesAspirant_Click);
            // 
            // smiVKRMain
            // 
            this.smiVKRMain.Enabled = false;
            this.smiVKRMain.Name = "smiVKRMain";
            this.smiVKRMain.Size = new System.Drawing.Size(282, 22);
            this.smiVKRMain.Text = "Темы ВКР - итоговый реестр (проект)";
            this.smiVKRMain.Visible = false;
            this.smiVKRMain.Click += new System.EventHandler(this.smiVKRMain_Click);
            // 
            // smiGAK
            // 
            this.smiGAK.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiGAKLists,
            this.smiGAKMembers});
            this.smiGAK.Name = "smiGAK";
            this.smiGAK.Size = new System.Drawing.Size(274, 22);
            this.smiGAK.Text = "Составы ГЭК";
            // 
            // smiGAKLists
            // 
            this.smiGAKLists.Enabled = false;
            this.smiGAKLists.Name = "smiGAKLists";
            this.smiGAKLists.Size = new System.Drawing.Size(247, 22);
            this.smiGAKLists.Text = "Составы ГЭК: председатели";
            this.smiGAKLists.Click += new System.EventHandler(this.smiGAKLists_Click);
            // 
            // smiGAKMembers
            // 
            this.smiGAKMembers.Enabled = false;
            this.smiGAKMembers.Name = "smiGAKMembers";
            this.smiGAKMembers.Size = new System.Drawing.Size(247, 22);
            this.smiGAKMembers.Text = "Составы ГЭК: список комиссий";
            this.smiGAKMembers.Visible = false;
            this.smiGAKMembers.Click += new System.EventHandler(this.smiGAKMembers_Click);
            // 
            // smiSOP
            // 
            this.smiSOP.Name = "smiSOP";
            this.smiSOP.Size = new System.Drawing.Size(274, 22);
            this.smiSOP.Text = "Советы образовательных программ";
            this.smiSOP.Click += new System.EventHandler(this.smiSOP_Click);
            // 
            // smiStatistics
            // 
            this.smiStatistics.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiOrgaanizationStatistics,
            this.smiGAKStatistics});
            this.smiStatistics.Name = "smiStatistics";
            this.smiStatistics.Size = new System.Drawing.Size(80, 20);
            this.smiStatistics.Text = "Статистика";
            // 
            // smiOrgaanizationStatistics
            // 
            this.smiOrgaanizationStatistics.Name = "smiOrgaanizationStatistics";
            this.smiOrgaanizationStatistics.Size = new System.Drawing.Size(234, 22);
            this.smiOrgaanizationStatistics.Text = "Статистика по организациям";
            this.smiOrgaanizationStatistics.Click += new System.EventHandler(this.smiOrgaanizationStatistics_Click);
            // 
            // smiGAKStatistics
            // 
            this.smiGAKStatistics.Name = "smiGAKStatistics";
            this.smiGAKStatistics.Size = new System.Drawing.Size(234, 22);
            this.smiGAKStatistics.Text = "Статистика по ГЭК";
            this.smiGAKStatistics.Click += new System.EventHandler(this.smiGAKStatistics_Click);
            // 
            // smiSettings
            // 
            this.smiSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiEmailSettings});
            this.smiSettings.Name = "smiSettings";
            this.smiSettings.Size = new System.Drawing.Size(79, 20);
            this.smiSettings.Text = "Настройки";
            this.smiSettings.Visible = false;
            // 
            // smiEmailSettings
            // 
            this.smiEmailSettings.Name = "smiEmailSettings";
            this.smiEmailSettings.Size = new System.Drawing.Size(209, 22);
            this.smiEmailSettings.Text = "Логин/Пароль для email";
            this.smiEmailSettings.Click += new System.EventHandler(this.smiEmailSettings_Click);
            // 
            // smiWindow
            // 
            this.smiWindow.Name = "smiWindow";
            this.smiWindow.Size = new System.Drawing.Size(48, 20);
            this.smiWindow.Text = "Окно";
            // 
            // smiTables
            // 
            this.smiTables.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiDegree,
            this.smiRank,
            this.smiPosition,
            this.smiPositionSOP,
            this.toolStripSeparator1,
            this.smiActivityArea,
            this.smiOwnership,
            this.smiActivityGoal,
            this.smiNationalAffiliation,
            this.toolStripSeparator2,
            this.smiLPOP,
            this.smiStudent,
            this.smiAspirant,
            this.smiNPR});
            this.smiTables.Name = "smiTables";
            this.smiTables.Size = new System.Drawing.Size(94, 20);
            this.smiTables.Text = "Справочники";
            // 
            // smiDegree
            // 
            this.smiDegree.Name = "smiDegree";
            this.smiDegree.Size = new System.Drawing.Size(320, 22);
            this.smiDegree.Text = "Ученая степень";
            this.smiDegree.Click += new System.EventHandler(this.smiDegree_Click);
            // 
            // smiRank
            // 
            this.smiRank.Name = "smiRank";
            this.smiRank.Size = new System.Drawing.Size(320, 22);
            this.smiRank.Text = "Ученое звание";
            this.smiRank.Click += new System.EventHandler(this.smiRank_Click);
            // 
            // smiPosition
            // 
            this.smiPosition.Name = "smiPosition";
            this.smiPosition.Size = new System.Drawing.Size(320, 22);
            this.smiPosition.Text = "Должности в организациях";
            this.smiPosition.Click += new System.EventHandler(this.smiPosition_Click);
            // 
            // smiPositionSOP
            // 
            this.smiPositionSOP.Name = "smiPositionSOP";
            this.smiPositionSOP.Size = new System.Drawing.Size(320, 22);
            this.smiPositionSOP.Text = "Должность в совете обр. программ";
            this.smiPositionSOP.Click += new System.EventHandler(this.smiPositionSOP_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(317, 6);
            // 
            // smiActivityArea
            // 
            this.smiActivityArea.Name = "smiActivityArea";
            this.smiActivityArea.Size = new System.Drawing.Size(320, 22);
            this.smiActivityArea.Text = "Ключевые слова";
            this.smiActivityArea.Click += new System.EventHandler(this.smiActivityArea_Click);
            // 
            // smiOwnership
            // 
            this.smiOwnership.Name = "smiOwnership";
            this.smiOwnership.Size = new System.Drawing.Size(320, 22);
            this.smiOwnership.Text = "Формы собственности";
            this.smiOwnership.Click += new System.EventHandler(this.smiOwnership_Click);
            // 
            // smiActivityGoal
            // 
            this.smiActivityGoal.Name = "smiActivityGoal";
            this.smiActivityGoal.Size = new System.Drawing.Size(320, 22);
            this.smiActivityGoal.Text = "Цель деятельности";
            this.smiActivityGoal.Click += new System.EventHandler(this.smiActivityGoal_Click);
            // 
            // smiNationalAffiliation
            // 
            this.smiNationalAffiliation.Name = "smiNationalAffiliation";
            this.smiNationalAffiliation.Size = new System.Drawing.Size(320, 22);
            this.smiNationalAffiliation.Text = "Национальные принадлежности";
            this.smiNationalAffiliation.Click += new System.EventHandler(this.ationalityAffiliation_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(317, 6);
            // 
            // smiLPOP
            // 
            this.smiLPOP.Name = "smiLPOP";
            this.smiLPOP.Size = new System.Drawing.Size(320, 22);
            this.smiLPOP.Text = "Направления, образовательные программы";
            this.smiLPOP.Click += new System.EventHandler(this.smiLPOP_Click);
            // 
            // smiStudent
            // 
            this.smiStudent.Name = "smiStudent";
            this.smiStudent.Size = new System.Drawing.Size(320, 22);
            this.smiStudent.Text = "Справочник студентов";
            this.smiStudent.Click += new System.EventHandler(this.smiStudent_Click);
            // 
            // smiAspirant
            // 
            this.smiAspirant.Name = "smiAspirant";
            this.smiAspirant.Size = new System.Drawing.Size(320, 22);
            this.smiAspirant.Text = "Справочник аспирантов";
            this.smiAspirant.Click += new System.EventHandler(this.smiAspirant_Click);
            // 
            // smiNPR
            // 
            this.smiNPR.Name = "smiNPR";
            this.smiNPR.Size = new System.Drawing.Size(320, 22);
            this.smiNPR.Text = "Научно-педагогические работники";
            this.smiNPR.Click += new System.EventHandler(this.smiNPR_Click);
            // 
            // smiHelp
            // 
            this.smiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpShowToolStripMenuItem,
            this.helpEditToolStripMenuItem,
            this.smiNewOrgHelp,
            this.smiNewOrgHelpLoad,
            this.tmplToolStripMenuItem});
            this.smiHelp.Name = "smiHelp";
            this.smiHelp.Size = new System.Drawing.Size(65, 20);
            this.smiHelp.Text = "Справка";
            // 
            // helpShowToolStripMenuItem
            // 
            this.helpShowToolStripMenuItem.Name = "helpShowToolStripMenuItem";
            this.helpShowToolStripMenuItem.Size = new System.Drawing.Size(430, 22);
            this.helpShowToolStripMenuItem.Text = "Справка по программе";
            this.helpShowToolStripMenuItem.Click += new System.EventHandler(this.helpShowToolStripMenuItem_Click);
            // 
            // helpEditToolStripMenuItem
            // 
            this.helpEditToolStripMenuItem.Name = "helpEditToolStripMenuItem";
            this.helpEditToolStripMenuItem.Size = new System.Drawing.Size(430, 22);
            this.helpEditToolStripMenuItem.Text = "Справка (загрузка в БД)";
            this.helpEditToolStripMenuItem.Visible = false;
            this.helpEditToolStripMenuItem.Click += new System.EventHandler(this.helpEditToolStripMenuItem_Click);
            // 
            // smiNewOrgHelp
            // 
            this.smiNewOrgHelp.Name = "smiNewOrgHelp";
            this.smiNewOrgHelp.Size = new System.Drawing.Size(430, 22);
            this.smiNewOrgHelp.Text = "Правила ввода новой организации в ИС Партнер";
            this.smiNewOrgHelp.Click += new System.EventHandler(this.smiNewOrgHelp_Click);
            // 
            // smiNewOrgHelpLoad
            // 
            this.smiNewOrgHelpLoad.Name = "smiNewOrgHelpLoad";
            this.smiNewOrgHelpLoad.Size = new System.Drawing.Size(430, 22);
            this.smiNewOrgHelpLoad.Text = "Правила ввода новой организации в ИС Партнер (загрузка в БД)";
            this.smiNewOrgHelpLoad.Visible = false;
            this.smiNewOrgHelpLoad.Click += new System.EventHandler(this.smiNewOrgHelpLoad_Click);
            // 
            // tmplToolStripMenuItem
            // 
            this.tmplToolStripMenuItem.Name = "tmplToolStripMenuItem";
            this.tmplToolStripMenuItem.Size = new System.Drawing.Size(430, 22);
            this.tmplToolStripMenuItem.Text = "Шаблоны (редактирование)";
            this.tmplToolStripMenuItem.Visible = false;
            this.tmplToolStripMenuItem.Click += new System.EventHandler(this.tmplToolStripMenuItem_Click);
            // 
            // MainTimer
            // 
            this.MainTimer.Interval = 1000;
            this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "11";
            // 
            // smiObrazProgramCharacteristics
            // 
            this.smiObrazProgramCharacteristics.Name = "smiObrazProgramCharacteristics";
            this.smiObrazProgramCharacteristics.Size = new System.Drawing.Size(274, 22);
            this.smiObrazProgramCharacteristics.Text = "Характеристика обр. программ";
            this.smiObrazProgramCharacteristics.Click += new System.EventHandler(this.smiObrazProgramCharacteristics_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 859);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem smiLists;
        private System.Windows.Forms.ToolStripMenuItem smiOrganizationList;
        private System.Windows.Forms.ToolStripMenuItem smiSettings;
        private System.Windows.Forms.ToolStripMenuItem smiEmailSettings;
        private System.Windows.Forms.ToolStripMenuItem smiRubric;
        private System.Windows.Forms.ToolStripMenuItem smiPersonList;
        private System.Windows.Forms.ToolStripMenuItem smiTables;
        private System.Windows.Forms.ToolStripMenuItem smiDegree;
        private System.Windows.Forms.ToolStripMenuItem smiRank;
        private System.Windows.Forms.ToolStripMenuItem smiActivityArea;
        private System.Windows.Forms.ToolStripMenuItem smiOwnership;
        private System.Windows.Forms.ToolStripMenuItem smiActivityGoal;
        private System.Windows.Forms.ToolStripMenuItem smiNationalAffiliation;
        private System.Windows.Forms.ToolStripMenuItem smiHelp;
        private System.Windows.Forms.ToolStripMenuItem helpShowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpEditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tmplToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smiPracticeMain;
        private System.Windows.Forms.ToolStripMenuItem smiLPOP;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem smiStatistics;
        private System.Windows.Forms.ToolStripMenuItem smiOrgaanizationStatistics;
        private System.Windows.Forms.ToolStripMenuItem smiWindow;
        private System.Windows.Forms.ToolStripMenuItem smiVKRMain;
        private System.Windows.Forms.ToolStripMenuItem smiNewOrgHelp;
        private System.Windows.Forms.ToolStripMenuItem smiNewOrgHelpLoad;
        private System.Windows.Forms.ToolStripMenuItem smiNPR;
        private System.Windows.Forms.Timer MainTimer;
        private System.Windows.Forms.ToolStripMenuItem smiVKRAddEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem smiPosition;
        private System.Windows.Forms.ToolStripMenuItem smiStudent;
        private System.Windows.Forms.ToolStripMenuItem smiGAKLists;
        private System.Windows.Forms.ToolStripMenuItem smiVKRThemesStudent;
        private System.Windows.Forms.ToolStripMenuItem smiGAKMembers;
        private System.Windows.Forms.ToolStripMenuItem smiGAKStatistics;
        private System.Windows.Forms.ToolStripMenuItem smiAspirant;
        private System.Windows.Forms.ToolStripMenuItem smiVKRThemesAspirant;
        private System.Windows.Forms.Timer TimerSetLastUdateTime;
        private System.Windows.Forms.ToolStripMenuItem smiSOP;
        private System.Windows.Forms.ToolStripMenuItem smiPositionSOP;
        private System.Windows.Forms.ToolStripMenuItem smiVKRblock;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem smiGAK;
        private System.Windows.Forms.ToolStripMenuItem smiObrazProgramCharacteristics;
    }
}