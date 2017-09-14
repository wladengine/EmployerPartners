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
using EmployerPartners.EDMX;

namespace EmployerPartners
{
    public partial class CardOrganizationDogovor : Form
    {
        #region Fields
        public int? RubricId
        {
            get { return ComboServ.GetComboIdInt(cbRubric); }
            set { ComboServ.SetComboId(cbRubric, value); }
        }
        public int? DocumentTypeId
        {
            get { return ComboServ.GetComboIdInt(cbdocumentType); }
            set { ComboServ.SetComboId(cbdocumentType, value); }
        }
        public string Document
        {
            get { return tbDocument.Text.Trim(); }
            set { tbDocument.Text = value; }
        }
        public string DocumentStart
        {
            get { return tbDocumentStart.Text.Trim(); }
            set { tbDocumentStart.Text = value; }
        }
        public string DocumentFinish
        {
            get { return tbDocumentFinish.Text.Trim(); }
            set { tbDocumentFinish.Text = value; }
        }
        public bool Permanent
        {
            get { return checkBoxPermanent.Checked; }
            set { checkBoxPermanent.Checked = value; }
        }
        public bool FromDocumentDate
        {
            get { return checkBoxFromDocumentDate.Checked; }
            set { checkBoxFromDocumentDate.Checked = value; }
        }
        public string DocumentNumber
        {
            get { return tbDocumentNumber.Text.Trim(); }
            set { tbDocumentNumber.Text = value; }
        }
        public string DocumentDate
        {
            get { return tbDocumentDate.Text.Trim(); }
            set { tbDocumentDate.Text = value; }
        }
        public string Address
        {
            get { return tbAddress.Text.Trim(); }
            set { tbAddress.Text = value; }
        }
        public string Comment
        {
            get { return tbComment.Text.Trim(); }
            set { tbComment.Text = value; }
        }
        public bool IsActual
        {
            get { return checkBoxIsActual.Checked; }
            set { checkBoxIsActual.Checked = value; }
        }
        #endregion

        private int _OrgId
        {
            get;
            set;
        }

        private int? _Id
        {
            get;
            set;
        }

        UpdateIntHandler _hndl;

        public CardOrganizationDogovor(int? Id, int orgId, UpdateIntHandler _hdl)
        {
            InitializeComponent();
            _hndl = _hdl;
            _Id = Id;
            _OrgId = orgId;
            this.MdiParent = Util.mainform;
            FillCard();
            FillFileList();
            SetAccessRight();
        }
        private void SetAccessRight()
        {
            if (Util.IsOrgPersonWrite())
            {
                btnSave.Enabled = true;
                btnAddFile.Enabled = true;
                dgvFile.Columns[1].Visible = true;
            }
        }
        private void FillCard()
        {
            ComboServ.FillCombo(cbRubric, HelpClass.GetComboListByTable("dbo.Rubric"), false, true);
            //ComboServ.FillCombo(cbdocumentType, HelpClass.GetComboListByTable("dbo.DocumentType"), false, false);
            ComboServ.FillCombo(cbdocumentType, HelpClass.GetComboListByQuery(@" SELECT CONVERT(varchar(100), Id) AS Id, Name 
                FROM DocumentType ORDER BY Id "), false, false);
            DocumentTypeId = 2;
            IsActual = true;

            if (_Id.HasValue)
            {
                lblHeader.Text = "Редактирование договора";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var dogovor = (from x in context.OrganizationDogovor
                                   where x.Id == _Id
                                   select x).First();
                    RubricId = dogovor.RubricId;
                    DocumentTypeId = dogovor.DocumentTypeId;
                    Document = dogovor.Document;
                    DocumentStart = (dogovor.DocumentStart.HasValue) ? dogovor.DocumentStart.Value.Date.ToString("dd.MM.yyyy") : "";
                    DocumentFinish = (dogovor.DocumentFinish.HasValue) ? dogovor.DocumentFinish.Value.Date.ToString("dd.MM.yyyy") : "";
                    //Permanent = (bool)dogovor.Permanent;    //checkBoxPermanent.Checked = dogovor.Permanent.HasValue ? (bool)dogovor.Permanent : false;
                    Permanent = dogovor.Permanent.HasValue ? (bool)dogovor.Permanent : false;
                    FromDocumentDate = dogovor.FromDocumentDate.HasValue ? (bool)dogovor.FromDocumentDate : false;
                    DocumentNumber = dogovor.DocumentNumber;
                    DocumentDate = (dogovor.DocumentDate.HasValue) ? dogovor.DocumentDate.Value.Date.ToString("dd.MM.yyyy") : "";
                    Address = dogovor.Address;
                    Comment = dogovor.Comment;
                    IsActual = (bool)dogovor.IsActual;

                    try
                    {
                        var org = (from x in context.OrganizationDogovor
                                   join o in context.Organization on x.OrganizationId equals o.Id
                                   where x.Id == _Id
                                   select o).First();
                        this.Text = "Договор: " + org.Name;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            else
            {
                lblHeader.Text = "Новый договор";
                try
                {
                    using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                    {
                        var org = (from x in context.Organization
                                   where x.Id == _OrgId
                                   select x).First();
                        this.Text = "Новый договор: " + org.Name;
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        private void FillFileList()
        {
            if (!_Id.HasValue)
                return;
            lblFile.Visible = true;
            dgvFile.Visible = true;
            btnAddFile.Visible = true;
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.OrganizationDogovorFile
                               where x.OrganizationDogovorId == _Id
                               select new 
                               {
                                   x.Id,
                                   x.OrganizationDogovorId,
                                   Файл = x.FileName,
                                   Дата_загрузки = x.DateLoad,
                               }).ToList();
                    dgvFile.DataSource = lst;

                    List<string> Cols = new List<string>() { "Id", "OrganizationDogovorId" };

                    foreach (string s in Cols)
                        if (dgvFile.Columns.Contains(s))
                            dgvFile.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvFile.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnDelFile")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnViewFile")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnDiv1")
                        {
                            col.HeaderText = "";
                        }
                    }
                    try
                    {
                        dgvFile.Columns["ColumnDiv1"].Width = 6;
                        dgvFile.Columns["ColumnDelFile"].Width = 70;
                        dgvFile.Columns["ColumnViewFile"].Width = 70;
                        dgvFile.Columns["Файл"].Frozen = true;
                        dgvFile.Columns["Файл"].Width = 200;
                        dgvFile.Columns["Дата_загрузки"].Width = 120;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private bool CheckFields()
        {
            //проверка рубрики
            if (!RubricId.HasValue)
            {
                MessageBox.Show("Не выбрана рубрика!", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            //проверка типа документа
            if (!DocumentTypeId.HasValue)
            {
                MessageBox.Show("Не выбран тип документа!", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            //проверка других полей
            if (String.IsNullOrEmpty(Document))
            {
                MessageBox.Show("Не введены данные в поле 'Договор'!", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (String.IsNullOrEmpty(DocumentNumber))
            {
                MessageBox.Show("Не введены данные в поле '№ договора'!", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (String.IsNullOrEmpty(DocumentDate))
            {
                MessageBox.Show("Не введены данные в поле 'Дата договора'!", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            //проверка дат
            DateTime res;
            if (!String.IsNullOrEmpty(DocumentStart))
            {
                if (!DateTime.TryParse(DocumentStart, out res))
                {
                    MessageBox.Show("Неправильный формат даты в поле 'Дата начала' \r\n" + "Образец: 01.12.2016", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            if (!String.IsNullOrEmpty(DocumentFinish))
            {
                if (!DateTime.TryParse(DocumentFinish, out res))
                {
                    MessageBox.Show("Неправильный формат даты в поле 'Дата окончания'\r\n" + "Образец: 01.12.2016", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            if (!String.IsNullOrEmpty(DocumentDate))
            {
                if (!DateTime.TryParse(DocumentDate, out res))
                {
                    MessageBox.Show("Неправильный формат даты в поле 'Дата подписания' \r\n" + "Образец: 01.12.2016", "Инфо");
                    return false;
                }
            }
            return true;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckFields())
            {
                return;
            }
            try
            {
                if (FromDocumentDate)
                {
                    DocumentStart = DocumentDate;
                }
            }
            catch (Exception)
            {
            }
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    OrganizationDogovor dogovor;
                    if (_Id.HasValue)
                    {
                        dogovor = context.OrganizationDogovor.Where(x => x.Id == _Id).First();
                    }
                    else
                    {
                        dogovor = new OrganizationDogovor();
                    }
                    
                    dogovor.OrganizationId = _OrgId;
                    dogovor.RubricId = (int)RubricId;
                    dogovor.DocumentTypeId = (int)DocumentTypeId;
                    dogovor.Document = Document;
                    dogovor.Permanent = Permanent;      //checkBoxPermanent.Checked ? true : false;
                    dogovor.FromDocumentDate = FromDocumentDate;    //checkBoxFromDocumentDate.Checked ? true : false;
                    dogovor.DocumentNumber = DocumentNumber;
                    dogovor.Address = Address;
                    dogovor.Comment = Comment;
                    dogovor.IsActual = IsActual;        //checkBoxIsActual.Checked ? true : false;

                    if (!String.IsNullOrEmpty(DocumentStart))
                    {
                        dogovor.DocumentStart = DateTime.Parse(DocumentStart);
                    }
                    else
                    {
                        dogovor.DocumentStart = null;
                    }
                    if (!String.IsNullOrEmpty(DocumentFinish))
                    {
                        dogovor.DocumentFinish = DateTime.Parse(DocumentFinish);
                    }
                    else
                    {
                        dogovor.DocumentFinish = null;
                    }
                    if (!String.IsNullOrEmpty(DocumentDate))
                    {
                        dogovor.DocumentDate = DateTime.Parse(DocumentDate);
                    }
                    else
                    {
                        dogovor.DocumentDate = null;
                    }

                    if (!_Id.HasValue)
                    {
                        context.OrganizationDogovor.Add(dogovor);
                    }
                    
                    context.SaveChanges();

                    MessageBox.Show("Данные сохранены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (_hndl != null)
                        _hndl(_Id);

                    //this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить данные...\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private bool CheckFileLoaded()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.OrganizationDogovorFile
                               where x.OrganizationDogovorId == _Id
                               select new
                               {
                                   x.Id,
                                   Файл = x.FileName,
                                   Дата_загрузки = x.DateLoad,
                               }).ToList();
                    if (lst.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void btnAddFile_Click(object sender, EventArgs e)
        {
            if (CheckFileLoaded())
            {
                if (MessageBox.Show("В БД уже есть загруженный файл договора.\r\n" + "Если это новая версия файла, то рекомендуется \r\n" +
                    "предыдущую версию удалить (кнопка 'Удалить')\r\n" + "Если это файл в другом формате, можно продолжать.\r\n" +
                    "Выполнить загрузку документа?", "Запрос на подтверждение",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                { return; }
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
                    OrganizationDogovorFile docfile = new OrganizationDogovorFile();
                    docfile.OrganizationDogovorId = (int)_Id;
                    docfile.FileName = name;
                    docfile.FileType = type;
                    docfile.FileData = fileByteArray;
                    docfile.DateLoad = DateTime.Now;
                    docfile.FileSizeKBytes = kbSize;
                    context.OrganizationDogovorFile.Add(docfile);
                    context.SaveChanges();
                }
                MessageBox.Show("Файл успешно загружен в БД", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FillFileList();
            }
            catch (Exception ec)
            {
                MessageBox.Show("Не удалось сохранить файл в БД...\r\n" + ec.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void dgvFile_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFile.CurrentCell != null)
                if (dgvFile.CurrentRow.Index >= 0)
                {
                    if (dgvFile.CurrentCell.ColumnIndex == 1)
                    {
                        try
                        {
                            dgvFile.CurrentCell = dgvFile.CurrentRow.Cells["Файл"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        int id;
                        try
                        {
                            id = int.Parse(dgvFile.CurrentRow.Cells["Id"].Value.ToString());
                        }
                        catch (Exception)
                        {
                            return;
                        }
                        string FileName = "";
                        try
                        {
                            FileName = dgvFile.CurrentRow.Cells["Файл"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        if (MessageBox.Show("Удалить выбранный файл из БД? \r\n" + FileName, "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            try
                            {
                                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                                {
                                    context.OrganizationDogovorFile.RemoveRange(context.OrganizationDogovorFile.Where(x => x.Id == id));
                                    context.SaveChanges();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Не удалось удалить запись...\r\n" + ex.Message, "Сообщение",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            FillFileList();
                            return;
                        }
                        else
                            return;
                    }
                    if (dgvFile.CurrentCell.ColumnIndex == 2)
                    {
                        try
                        {
                            dgvFile.CurrentCell = dgvFile.CurrentRow.Cells["Файл"];
                        }
                        catch (Exception)
                        {
                        }
                        //////
                        int id;
                        try
                        {
                            id = int.Parse(dgvFile.CurrentRow.Cells["Id"].Value.ToString());
                        }
                        catch (Exception)
                        {
                            return;
                        }
                        //извлечение файла из БД
                        byte[] fileByteArray;
                        string type;
                        string name;
                        string nameshort;

                        try
                        {
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                var dogfile =(from x in context.OrganizationDogovorFile
                                              where x.Id == id
                                              select x).First();

                                fileByteArray = (byte[])dogfile.FileData;
                                type = (string)dogfile.FileType.Trim();
                                name = (string)dogfile.FileName.Trim();
                                nameshort = name.Substring(0, name.Length - type.Length);
                            }
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show("Не удалось получить данные...\r\n" + exc.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                                MessageBox.Show("Не удалось создать директорию.\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                MessageBox.Show("Не удалось открыть файл...", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            return;
                        }

                        string filePath = TempFilesFolder + name;
                        string[] fileList = Directory.GetFiles(TempFilesFolder, nameshort + "*" + type);
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
                        //Запись на диск
                        try
                        {
                            FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                            BinaryWriter binWriter = new BinaryWriter(fileStream);
                            binWriter.Write(fileByteArray);
                            binWriter.Close();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Не удалось открыть файл...", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        //Открыть файл
                        System.Diagnostics.Process.Start(@filePath);
                        //////
                    }
                }
        }

        private void checkBoxFromDocumentDate_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (FromDocumentDate)
            //    {
            //        DocumentStart = DocumentDate;   
            //    }
            //}
            //catch 
            //{
            //}
        }
    }
}
