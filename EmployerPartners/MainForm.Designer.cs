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
            this.smiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.smiEmailSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.образовательныеСоветыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.кадровыеСоветыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.организацииToolStripMenuItem,
            this.smiSettings,
            this.образовательныеСоветыToolStripMenuItem,
            this.кадровыеСоветыToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1182, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // организацииToolStripMenuItem
            // 
            this.организацииToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiOrganizationList});
            this.организацииToolStripMenuItem.Name = "организацииToolStripMenuItem";
            this.организацииToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.организацииToolStripMenuItem.Text = "Организации";
            // 
            // smiOrganizationList
            // 
            this.smiOrganizationList.Name = "smiOrganizationList";
            this.smiOrganizationList.Size = new System.Drawing.Size(189, 22);
            this.smiOrganizationList.Text = "Список организаций";
            this.smiOrganizationList.Click += new System.EventHandler(this.smiOrganizationList_Click);
            // 
            // smiSettings
            // 
            this.smiSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiEmailSettings});
            this.smiSettings.Name = "smiSettings";
            this.smiSettings.Size = new System.Drawing.Size(79, 20);
            this.smiSettings.Text = "Настройки";
            // 
            // smiEmailSettings
            // 
            this.smiEmailSettings.Name = "smiEmailSettings";
            this.smiEmailSettings.Size = new System.Drawing.Size(209, 22);
            this.smiEmailSettings.Text = "Логин/Пароль для email";
            this.smiEmailSettings.Click += new System.EventHandler(this.smiEmailSettings_Click);
            // 
            // образовательныеСоветыToolStripMenuItem
            // 
            this.образовательныеСоветыToolStripMenuItem.Name = "образовательныеСоветыToolStripMenuItem";
            this.образовательныеСоветыToolStripMenuItem.Size = new System.Drawing.Size(160, 20);
            this.образовательныеСоветыToolStripMenuItem.Text = "Образовательные советы";
            // 
            // кадровыеСоветыToolStripMenuItem
            // 
            this.кадровыеСоветыToolStripMenuItem.Name = "кадровыеСоветыToolStripMenuItem";
            this.кадровыеСоветыToolStripMenuItem.Size = new System.Drawing.Size(115, 20);
            this.кадровыеСоветыToolStripMenuItem.Text = "Кадровые советы";
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
        private System.Windows.Forms.ToolStripMenuItem образовательныеСоветыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem кадровыеСоветыToolStripMenuItem;
    }
}