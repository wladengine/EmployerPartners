using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployerPartners
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            var x = Util.lstCountry;
            this.Text = "Организации партнеры";
            Log();
            SetAccessRight();
        }
        private void SetAccessRight()
        {
            if (Util.IsAdministrator())
            {
                smiSettings.Visible = true;
            }
            else
            {
                smiSettings.Visible = false;
            }
            if (Util.IsDBOwner() || Util.IsAdministrator())
            {
                helpEditToolStripMenuItem.Visible = true;
                tmplToolStripMenuItem.Visible = true;
                smiLPOP.Visible = true;
            }
            if (Util.IsPractice())
            {
                smiPracticeMain.Visible = true;
                smiPracticeMain.Enabled = true;
                smiLPOP.Visible = true;
            }
            if (Util.IsPracticeRead())
            {
                smiPracticeMain.Visible = true;
                smiPracticeMain.Enabled = true;
                smiLPOP.Visible = true;
            }
            if (Util.IsPracticeWrite())
            {
                smiPracticeMain.Visible = true;
                smiPracticeMain.Enabled = true;
                smiLPOP.Visible = true;
            }
            if (Util.IsReadOnlyAll())
            {
                smiTables.Visible = false;
                helpEditToolStripMenuItem.Visible = false;
                tmplToolStripMenuItem.Visible = false;
                smiPracticeMain.Visible = true;
                smiPracticeMain.Enabled = true;
            }
        }
        private void Log()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    EPLog log = new EPLog();
                    log.ActionName = "Открытие программы";
                    context.EPLog.Add(log);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
        }
        private void smiOrganizationList_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form frm in Application.OpenForms)
                    if (frm is ListOrganizations)
                    {
                        frm.Activate();
                        return;
                    }
            }
            catch (Exception)
            {
            } 
            new ListOrganizations().Show();
        }

        private void smiEmailSettings_Click(object sender, EventArgs e)
        {
            new CardSettingsEmail().Show();
        }

        private void smiPersonList_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form frm in Application.OpenForms)
                    if (frm is ListPersons)
                    {
                        frm.Activate();
                        return;
                    }
            }
            catch (Exception)
            {
            } 
            new ListPersons().Show();
        }

        private void smiDegree_Click(object sender, EventArgs e)
        {
            new CardDictionaryDegree().Show();
        }

        private void smiRank_Click(object sender, EventArgs e)
        {
            new CardDictionaryRank().Show();
        }

        private void smiActivityArea_Click(object sender, EventArgs e)
        {
            new CardDictionaryActivityArea().Show();
        }

        private void smiActivityGoal_Click(object sender, EventArgs e)
        {
            new CardDictionaryActivityGoal().Show();

        }

        private void ationalityAffiliation_Click(object sender, EventArgs e)
        {
            new CardDictionaryNatAffiliation().Show();
        }

        private void smiOwnership_Click(object sender, EventArgs e)
        {
            new CardDictionaryOwnership().Show();
        }

        private void helpShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] fileByteArray;
            string type;
            int dbFileID = 1;

            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var help = (from x in context.HelpFiles
                                where x.Id == dbFileID
                                select x).First();

                    fileByteArray = (byte[])help.FileData;
                    type = (string)help.FileType;
                }
            }
            catch (Exception exc)
            {
                if (Util.IsDBOwner())
                {
                    MessageBox.Show("Не удалось получить данные...\r\n" + exc.Message, "Сообщение");
                }
                else
                {
                    MessageBox.Show("Не удалось получить данные...", "Сообщение");
                }
                return;
            }
            string TempFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\EmployerPartners_TempFiles\";
            try
            {
                if (!Directory.Exists(TempFilesFolder))
                    Directory.CreateDirectory(TempFilesFolder);
            }
            catch (Exception ex)
            {
                if (Util.IsDBOwner())
                {
                    MessageBox.Show("Не удалось создать директорию.\r\n" + ex.Message, "Сообщение");
                }
                else
                {
                    MessageBox.Show("Не удалось открыть файл...", "Сообщение");
                }
                return;
            }
            string filePath = TempFilesFolder + "\\Справка по программе" + type;
            string[] fileList = Directory.GetFiles(TempFilesFolder, "Справка по программе*" + type);
            int suffix;
            Random rnd = new Random();
            suffix = rnd.Next();
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    foreach (string f in fileList)
                    {
                        File.Delete(f);
                    }
                }
                catch (Exception)
                {
                    filePath = TempFilesFolder + "\\Справка по программе " + suffix + type;
                }
            }
            //Запись на диск. Используются классы BinaryWriter и FileStream
            try
            {
                FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter binWriter = new BinaryWriter(fileStream);
                binWriter.Write(fileByteArray);
                binWriter.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось открыть файл...", "Сообщение");
                return;
            }
            //Открыть файл
            System.Diagnostics.Process.Start(@filePath);

        }

        private void helpEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Чтение двоичного файла с диска
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Application.StartupPath;
                if (openFileDialog.ShowDialog() != DialogResult.OK) { return; }
                string filePath = openFileDialog.FileName;
                //Параметры файла
                string name = Path.GetFileName(filePath);
                string type = Path.GetExtension(filePath);
                byte[] fileByteArray = File.ReadAllBytes(filePath);
                double kbSize = Math.Round(Convert.ToDouble(fileByteArray.Length) / 1024, 2);
                int dbFileID = 1;
                //Запись в БД
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    HelpFiles help = context.HelpFiles.Where(x => x.Id == dbFileID).First();
                    help.FileName = name;
                    help.FileType = type;
                    help.FileData = fileByteArray;
                    help.DateLoad = DateTime.Now;
                    help.FileSizeKBytes = kbSize;
                    context.SaveChanges();
                }
                MessageBox.Show("Файл успешно загружен в БД", "Сообщение");
            }
            catch (Exception ec)
            {
                if (Util.IsDBOwner())
                {
                    MessageBox.Show("Не удалось сохранить файл в БД...\r\n" + ec.Message, "Сообщение");
                }
                else
                {
                    MessageBox.Show("Не удалось сохранить файл в БД", "Сообщение");
                }
                return;
            }
        }

        private void tmplToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Templates template = new Templates();
            template.MdiParent = this;
            template.Show();
        }

        private void smiPracticeMain_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form frm in Application.OpenForms)
                    if (frm is PracticeMain)
                    {
                        frm.Activate();
                        return;
                    }
            }
            catch (Exception)
            {
            }
            new PracticeMain().Show();
        }

        private void smiLPOP_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form frm in Application.OpenForms)
                    if (frm is UpdateFromSrv)
                    {
                        frm.Activate();
                        return;
                    }
            }
            catch (Exception)
            {
            }
            UpdateFromSrv updateform = new UpdateFromSrv();
            updateform.MdiParent = this;
            updateform.Show();
        }

        private void smiOrgaanizationStatistics_Click(object sender, EventArgs e)
        {
            new CardOrganizationStat().Show();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    EPLog log = new EPLog();
                    log.ActionName = "Закрытие программы";
                    context.EPLog.Add(log);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
