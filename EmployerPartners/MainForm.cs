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
using EmployerPartners.EDMX;

namespace EmployerPartners
{
    public partial class MainForm : Form
    {
        private int LogId;
        public bool newAppFormIsOpend = false;

        public MainForm()
        {
            InitializeComponent();
            var x = Util.lstCountry;
            this.Text = "Организации партнеры: " + Util.GetUserName() + " ("+GetVersion()+")" ;
            Log();
            SetAccessRight();
        }
        private void SetAccessRight()
        {
            smiSettings.Visible = Util.IsAdministrator();
            smiPracticeMain.Visible = smiPracticeMain.Enabled = (Util.IsPractice() || Util.IsPracticeRead() || Util.IsPracticeWrite() || Util.IsReadOnlyAll());
            smiVKRAddEdit.Visible = smiVKRAddEdit.Enabled =  
            smiVKRThemesStudent.Visible = smiVKRThemesStudent.Enabled =  
            smiVKRThemesAspirant.Visible = smiVKRThemesAspirant.Enabled = (Util.IsVKRRead() || Util.IsVKRWrite() || Util.IsReadOnlyAll());
            smiGAKLists.Visible = smiGAKLists.Enabled = 
            smiGAKMembers.Visible = smiGAKMembers.Enabled =  
            smiSOP.Visible = smiSOP.Enabled = (Util.IsGAKRead() || Util.IsGAKWrite() || Util.IsReadOnlyAll());
            smiSOP.Visible = smiSOP.Enabled = Util.IsSOPReadWrite();

            if (Util.IsDBOwner() || Util.IsAdministrator())
            {
                helpEditToolStripMenuItem.Visible = true;
                tmplToolStripMenuItem.Visible = true;
                smiNewOrgHelpLoad.Visible = true;
                smiVKRAddEdit.Visible = smiVKRAddEdit.Enabled = true;
                smiVKRMain.Visible = smiVKRMain.Enabled = true;
                smiVKRThemesStudent.Visible = smiVKRThemesStudent.Enabled = true;
                smiVKRThemesAspirant.Visible = smiVKRThemesAspirant.Enabled = true;
                smiGAKLists.Visible = smiGAKLists.Enabled = true;
                smiSOP.Visible = smiSOP.Enabled = true;
            } 
            if (Util.IsSuperUser())
            {
                smiGAKMembers.Visible = smiGAKMembers.Enabled = true;
                smiSOP.Visible = smiSOP.Enabled = true;

            }
        }
        private void Log()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    TimerSetLastUdateTime = new Timer();
                    TimerSetLastUdateTime.Interval = 1000 * 60 * 5 ; // 5 минут
                    TimerSetLastUdateTime.Tick += TimerSetLastUdateTime_Tick;
                    TimerSetLastUdateTime.Start();

                    bool isnew = false;
                    string UserName = Util.GetUserName();
                    string Version = GetVersion();
                    C_AppUsersVersion vers = context.C_AppUsersVersion.Where(x => x.UserName == UserName).FirstOrDefault();
                    if (vers== null)
                    {
                        isnew = true;
                        vers = new C_AppUsersVersion();
                        vers.UserName = UserName;
                    }

                    vers.Version = Version;
                    vers.OpenTimestamp = DateTime.Now;
                    vers.LastUpdateTimestamp = DateTime.Now;
                    if (isnew)
                        context.C_AppUsersVersion.Add(vers);

                    EPLog log = new EPLog();
                    log.ActionName = "Открытие программы";
                    log.Version = Version;
                    log.ActionValue = UserName;
                    context.EPLog.Add(log);
                    context.SaveChanges();
                    LogId = log.Id;
                }
            }
            catch (Exception)
            {
            }
        }
        public static string GetVersion()
        {
            int ind = Application.StartupPath.LastIndexOf("\\") + 1;
            if (ind > 0 && ind < Application.StartupPath.ToString().Length)
            {
                return Application.StartupPath.ToString().Substring(ind);
            }
            return "";
        }
        private static void TimerSetLastUdateTime_Tick(object sender, EventArgs e)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    string UserName = Util.GetUserName();
                    C_AppUsersVersion vers = context.C_AppUsersVersion.Where(x => x.UserName == UserName).FirstOrDefault();
                    if (vers != null)
                    {
                        vers.LastUpdateTimestamp = DateTime.Now;
                        context.SaveChanges();
                    }
                    else
                    {
                        vers = new C_AppUsersVersion();
                        vers.UserName = UserName;
                        vers.Version = GetVersion();
                        vers.LastUpdateTimestamp = DateTime.Now;
                        vers.OpenTimestamp = DateTime.Now;
                        context.C_AppUsersVersion.Add(vers);
                        context.SaveChanges();
                    }
                }
            }
            catch { }
        }
        private void smiOrganizationList_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("ListOrganizations"))
                return;
            new ListOrganizations().Show();
        }

        private void smiEmailSettings_Click(object sender, EventArgs e)
        {
            new CardSettingsEmail().Show();
        }

        private void smiPersonList_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("ListPersons"))
                return;
            new ListPersons().Show();
        }

        private void smiDegree_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("CardDictionaryDegree"))
                return;
            new CardDictionaryDegree().Show();
        }

        private void smiRank_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("CardDictionaryRank"))
                return;
            new CardDictionaryRank().Show();
        }

        private void smiActivityArea_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("CardDictionaryActivityArea"))
                return;
            new CardDictionaryActivityArea().Show();
        }

        private void smiActivityGoal_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("CardDictionaryActivityGoal"))
                return;
            new CardDictionaryActivityGoal().Show();
        }

        private void ationalityAffiliation_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("CardDictionaryNatAffiliation"))
                return;
            new CardDictionaryNatAffiliation().Show();
        }

        private void smiOwnership_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("CardDictionaryOwnership"))
                return;
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
            TemplatesCard template = new TemplatesCard();
            template.MdiParent = this;
            template.Show();
        }

        private void smiPracticeMain_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("PracticeMain"))
                return;
            new PracticeMain().Show();
        }

        private void smiLPOP_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("UpdateFromSrv"))
                return;
            UpdateFromSrv updateform = new UpdateFromSrv();
            updateform.MdiParent = this;
            updateform.Show();
        }

        private void smiOrgaanizationStatistics_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("CardOrganizationStat"))
                return;
            new CardOrganizationStat().Show();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    string UserName = Util.GetUserName();
                    C_AppUsersVersion vers = context.C_AppUsersVersion.Where(x => x.UserName == UserName).FirstOrDefault();
                    if (vers != null)
                    {
                        vers.Version = null;
                        vers.OpenTimestamp = null;
                        vers.LastUpdateTimestamp = DateTime.Now;
                    }
                    var log = context.EPLog.Where(x => x.Id == LogId).First();
                    log.ActionName1 = "Закрытие программы";
                    log.ActionTime1 = DateTime.Now;
                    log.ActionValue1 = UserName;
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
        }

        private void smiVKRMain_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("VKRMain"))
                return;
            new VKRMain().Show();
        }

        private void smiNewOrgHelpLoad_Click(object sender, EventArgs e)
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
                int dbFileID = 7;
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

        private void smiNewOrgHelp_Click(object sender, EventArgs e)
        {
            byte[] fileByteArray;
            string type;
            int dbFileID = 7;

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
            string filePath = TempFilesFolder + "\\Правила занесения новой организации в ИС Партнер" + type;
            string[] fileList = Directory.GetFiles(TempFilesFolder, "Правила занесения новой организации в ИС Партнер*" + type);
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
                    filePath = TempFilesFolder + "\\Правила занесения новой организации в ИС Партнер " + suffix + type;
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

        private void smiNPR_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("SAP_NPS"))
                return;
            SAP_NPS sapnps = new SAP_NPS();
            sapnps.MdiParent = this;
            sapnps.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MainTimer.Enabled = true; 
            MainTimer.Start();
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Utilities.MainTimerStop)
                {
                    MainTimer.Stop();
                    MainTimer.Enabled = false;
                    return;
                }

                MainTimer.Interval = 600000;

                if (newAppFormIsOpend)
                {
                    return;
                }
                string curdir = Application.StartupPath;
                if (curdir.Contains("bin\\Debug") || curdir.Contains("bin\\Release"))
                {
                    MainTimer.Stop();
                    MainTimer.Enabled = false;
                    return;
                }
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    string targetdir = context.C_Settings.Where(x => (x.Key == "CurrentDir_EmployerPartners")).First().Value.ToString();
                    if (targetdir != curdir)
                    {
                        newAppFormIsOpend = true;
                        if (MessageBox.Show("Имеется новая версия приложения.\r\n" + 
                        "Запустить новую версию ?", "Запрос на подтверждение",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                        {
                            string newApp = targetdir + "\\" + "EmployerPartners.exe";
                            System.Diagnostics.Process.Start(newApp);
                            Application.Exit();
                            return;
                        }
                        else
                        {
                            newAppFormIsOpend = false;
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Сообщение", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void smiVKRAddEdit_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("VKRThemesEdit"))
                return;
            new VKRThemesEdit().Show();
        }

        private void smiPosition_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("Positions"))
                return;
            new Positions().Show();   
        }

        private void smiStudent_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("UpdateStudentFromSrv"))
                return;
            UpdateStudentFromSrv updateform = new UpdateStudentFromSrv();
            updateform.MdiParent = this;
            updateform.Show();
        }

        private void smiGAKLists_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("GAK_Lists"))
                return;
            new GAK_Lists().Show();
        }

        private void smiVKRThemesStudent_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("VKRThemesStudent"))
                return;
            new VKRThemesStudent().Show();
        }

        private void smiGAKMembers_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("GAK_Members"))
                return;
            new GAK_Members().Show();
        }

        private void smiGAKStatistics_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("GAK_Statistics"))
                return;
            new GAK_Statistics().Show();
        }

        private void smiAspirant_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("UpdateAspirantFromSrv"))
                return;
            UpdateAspirantFromSrv updateform = new UpdateAspirantFromSrv();
            updateform.MdiParent = this;
            updateform.Show();
        }

        private void smiVKRThemesAspirant_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("VKRThemesAspirant"))
                return;
            new VKRThemesAspirant().Show();
        }

        private void smiSOP_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("SOPList"))
                return;
            new SOPList().Show();
        }

        private void smiPositionSOP_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("CardDictionaryPositionSOP"))
                return;
            new CardDictionaryPositionSOP().Show();
        }

        private void smiObrazProgramCharacteristics_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("ObrazProgramCharacteristicList"))
                return;
            new ObrazProgramCharacteristicList().Show();
        }
        
    }
}
