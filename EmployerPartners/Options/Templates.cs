using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployerPartners
{
    public partial class Templates : Form
    {
        public Templates()
        {
            InitializeComponent();
            FillGrid();
        }
        private void FillGrid()
        { 
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.Templates
                           select new
                           {
                               x.Id,
                               x.TemplateName,
                               x.FileName,
                               x.FileType,
                               x.DateLoad,
                               x.FileSizeKBytes
                           }).ToList();
                dgv.DataSource = lst;
                dgv.Columns["TemplateName"].Width = 300;
                dgv.Columns["FileName"].Width = 300;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tbNameToAdd.Text.Trim().Length == 0)
            {
                MessageBox.Show("Не введено название нового шаблона", "Напоминание");
                return;
            }
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
                //int dbFileID = 1;
                //Запись в БД
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    Templates template = new Templates();
                    template.TemplateName = tbNameToAdd.Text.Trim();
                    template.FileName = name;
                    template.FileType = type;
                    template.FileData = fileByteArray;
                    template.DateLoad = DateTime.Now;
                    template.FileSizeKBytes = kbSize;
                    context.Templates.Add(template);
                    context.SaveChanges();
                }
                MessageBox.Show("Файл успешно загружен в БД", "Сообщение");
            }
            catch (Exception ec)
            {

                if (Util.IsDBOwner())
                {
                    MessageBox.Show("Не удалось сохранить файл в БД...\r\n" + ec.Message , "Сообщение");
                }
                else
                {
                    MessageBox.Show("Не удалось сохранить файл в БД", "Сообщение");
                }
                return;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int dbFileID = 0;
                if (dgv.CurrentCell != null)
                    if (dgv.CurrentRow.Index >= 0)
                        dbFileID = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                    else
                        return;
                else
                    return;
                
                DialogResult msg = MessageBox.Show("Перезагрузить шаблон ? \r\n" + dgv.CurrentRow.Cells["FileName"].Value.ToString(), "Запрос на подтверждение", MessageBoxButtons.YesNo);
                if (msg != System.Windows.Forms.DialogResult.Yes)
                    return;

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
                
                //Запись в БД
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    //Templates template = new Templates();
                    Templates template = context.Templates.Where(x => x.Id == dbFileID).First();
                    template.FileName = name;
                    template.FileType = type;
                    template.FileData = fileByteArray;
                    template.DateLoad = DateTime.Now;
                    template.FileSizeKBytes = kbSize;
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

        private void btnShow_Click(object sender, EventArgs e)
        {
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            int dbFileID = 0;

            try
            {
                if (dgv.CurrentCell != null)
                    if (dgv.CurrentRow.Index >= 0)
                        dbFileID = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                    else
                        return;
                else
                    return;
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var template = (from x in context.Templates
                                where x.Id == dbFileID
                                select x).First();

                    fileByteArray = (byte[])template.FileData;
                    type = (string)template.FileType.Trim();
                    name = (string)template.FileName.Trim();
                    nameshort = name.Substring(0, name.Length - type.Length);
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
            
            string filePath = TempFilesFolder + name;
            string[] fileList = Directory.GetFiles(TempFilesFolder, nameshort +"*" + type);
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
                    filePath = TempFilesFolder + nameshort + " " + suffix + type;
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
    }
}
