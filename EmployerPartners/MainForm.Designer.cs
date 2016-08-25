namespace EmployerPartners
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.организацииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smiOrganizationList = new System.Windows.Forms.ToolStripMenuItem();
            this.smiPersonList = new System.Windows.Forms.ToolStripMenuItem();
            this.smiRubric = new System.Windows.Forms.ToolStripMenuItem();
            this.smiPracticeMain = new System.Windows.Forms.ToolStripMenuItem();
            this.smiStatistics = new System.Windows.Forms.ToolStripMenuItem();
            this.smiOrgaanizationStatistics = new System.Windows.Forms.ToolStripMenuItem();
            this.smiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.smiEmailSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.smiTables = new System.Windows.Forms.ToolStripMenuItem();
            this.smiDegree = new System.Windows.Forms.ToolStripMenuItem();
            this.smiRank = new System.Windows.Forms.ToolStripMenuItem();
            this.smiActivityArea = new System.Windows.Forms.ToolStripMenuItem();
            this.smiOwnership = new System.Windows.Forms.ToolStripMenuItem();
            this.smiActivityGoal = new System.Windows.Forms.ToolStripMenuItem();
            this.smiNationalAffiliation = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.smiLPOP = new System.Windows.Forms.ToolStripMenuItem();
            this.smiWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpShowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmplToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.организацииToolStripMenuItem,
            this.smiRubric,
            this.smiStatistics,
            this.smiSettings,
            this.smiTables,
            this.smiWindow,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.smiWindow;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1182, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // организацииToolStripMenuItem
            // 
            this.организацииToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiOrganizationList,
            this.smiPersonList});
            this.организацииToolStripMenuItem.Name = "организацииToolStripMenuItem";
            this.организацииToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.организацииToolStripMenuItem.Text = "Списки";
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
            this.smiPracticeMain});
            this.smiRubric.Name = "smiRubric";
            this.smiRubric.Size = new System.Drawing.Size(66, 20);
            this.smiRubric.Text = "Рубрики";
            // 
            // smiPracticeMain
            // 
            this.smiPracticeMain.Name = "smiPracticeMain";
            this.smiPracticeMain.Size = new System.Drawing.Size(211, 22);
            this.smiPracticeMain.Text = "Практика (главное окно)";
            this.smiPracticeMain.Visible = false;
            this.smiPracticeMain.Click += new System.EventHandler(this.smiPracticeMain_Click);
            // 
            // smiStatistics
            // 
            this.smiStatistics.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiOrgaanizationStatistics});
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
            // smiTables
            // 
            this.smiTables.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiDegree,
            this.smiRank,
            this.smiActivityArea,
            this.smiOwnership,
            this.smiActivityGoal,
            this.smiNationalAffiliation,
            this.toolStripSeparator1,
            this.smiLPOP});
            this.smiTables.Name = "smiTables";
            this.smiTables.Size = new System.Drawing.Size(94, 20);
            this.smiTables.Text = "Справочники";
            // 
            // smiDegree
            // 
            this.smiDegree.Name = "smiDegree";
            this.smiDegree.Size = new System.Drawing.Size(255, 22);
            this.smiDegree.Text = "Ученая степень";
            this.smiDegree.Click += new System.EventHandler(this.smiDegree_Click);
            // 
            // smiRank
            // 
            this.smiRank.Name = "smiRank";
            this.smiRank.Size = new System.Drawing.Size(255, 22);
            this.smiRank.Text = "Ученое звание";
            this.smiRank.Click += new System.EventHandler(this.smiRank_Click);
            // 
            // smiActivityArea
            // 
            this.smiActivityArea.Name = "smiActivityArea";
            this.smiActivityArea.Size = new System.Drawing.Size(255, 22);
            this.smiActivityArea.Text = "Сферы деятельности";
            this.smiActivityArea.Click += new System.EventHandler(this.smiActivityArea_Click);
            // 
            // smiOwnership
            // 
            this.smiOwnership.Name = "smiOwnership";
            this.smiOwnership.Size = new System.Drawing.Size(255, 22);
            this.smiOwnership.Text = "Формы собственности";
            this.smiOwnership.Click += new System.EventHandler(this.smiOwnership_Click);
            // 
            // smiActivityGoal
            // 
            this.smiActivityGoal.Name = "smiActivityGoal";
            this.smiActivityGoal.Size = new System.Drawing.Size(255, 22);
            this.smiActivityGoal.Text = "Цель деятельности";
            this.smiActivityGoal.Click += new System.EventHandler(this.smiActivityGoal_Click);
            // 
            // smiNationalAffiliation
            // 
            this.smiNationalAffiliation.Name = "smiNationalAffiliation";
            this.smiNationalAffiliation.Size = new System.Drawing.Size(255, 22);
            this.smiNationalAffiliation.Text = "Национальные принадлежности";
            this.smiNationalAffiliation.Click += new System.EventHandler(this.ationalityAffiliation_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(252, 6);
            // 
            // smiLPOP
            // 
            this.smiLPOP.Name = "smiLPOP";
            this.smiLPOP.Size = new System.Drawing.Size(255, 22);
            this.smiLPOP.Text = "Направления, ОП, Студенты";
            this.smiLPOP.Visible = false;
            this.smiLPOP.Click += new System.EventHandler(this.smiLPOP_Click);
            // 
            // smiWindow
            // 
            this.smiWindow.Name = "smiWindow";
            this.smiWindow.Size = new System.Drawing.Size(48, 20);
            this.smiWindow.Text = "Окно";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpShowToolStripMenuItem,
            this.helpEditToolStripMenuItem,
            this.tmplToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.helpToolStripMenuItem.Text = "Справка";
            // 
            // helpShowToolStripMenuItem
            // 
            this.helpShowToolStripMenuItem.Name = "helpShowToolStripMenuItem";
            this.helpShowToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.helpShowToolStripMenuItem.Text = "Справка по программе";
            this.helpShowToolStripMenuItem.Click += new System.EventHandler(this.helpShowToolStripMenuItem_Click);
            // 
            // helpEditToolStripMenuItem
            // 
            this.helpEditToolStripMenuItem.Name = "helpEditToolStripMenuItem";
            this.helpEditToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.helpEditToolStripMenuItem.Text = "Справка (загрузка в БД)";
            this.helpEditToolStripMenuItem.Visible = false;
            this.helpEditToolStripMenuItem.Click += new System.EventHandler(this.helpEditToolStripMenuItem_Click);
            // 
            // tmplToolStripMenuItem
            // 
            this.tmplToolStripMenuItem.Name = "tmplToolStripMenuItem";
            this.tmplToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.tmplToolStripMenuItem.Text = "Шаблоны (редактирование)";
            this.tmplToolStripMenuItem.Visible = false;
            this.tmplToolStripMenuItem.Click += new System.EventHandler(this.tmplToolStripMenuItem_Click);
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
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem организацииToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpShowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpEditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tmplToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smiPracticeMain;
        private System.Windows.Forms.ToolStripMenuItem smiLPOP;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem smiStatistics;
        private System.Windows.Forms.ToolStripMenuItem smiOrgaanizationStatistics;
        private System.Windows.Forms.ToolStripMenuItem smiWindow;
    }
}