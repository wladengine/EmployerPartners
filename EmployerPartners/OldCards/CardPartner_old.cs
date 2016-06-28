using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using KLADR;

namespace EmployerPartners
{
    public partial class CardPartner_old : Form
    {
        PartnerContactPersonList lstContacts;

        public CardPartner_old(int? id, UpdateVoidHandler _hdl)
        {
            InitializeComponent();
            _hndl = _hdl;
            FillOrganizationName();
            _Id = id;
             
            FillCard();
            InitControls(this);
            this.MdiParent = Util.mainform;
        }

        public void InitControls(Control obj)
        {
            foreach (Control x in obj.Controls)
            {
                if (x.Controls.Count > 0)
                    InitControls(x);
                x.MouseClick += new MouseEventHandler(ChangeControlVisibility);
            }
        }

        private void ExtraInit(bool add)
        {
            if (add)
            {
                tbCountry.TextChanged += tbCountry_TextChanged;
                tbCity.TextChanged += tbCity_TextChanged;
                tbRegion.TextChanged += tbRegion_TextChanged;
                tbStreet.TextChanged += tbStreet_TextChanged;
                tbHouse.TextChanged += tbHouse_TextChanged;
            }
            else
            {
                tbCountry.TextChanged -= tbCountry_TextChanged;
                tbCity.TextChanged -= tbCity_TextChanged;
                tbRegion.TextChanged -= tbRegion_TextChanged;
                tbStreet.TextChanged -= tbStreet_TextChanged;
                tbHouse.TextChanged -= tbHouse_TextChanged;
            }
        }

        private void ChangeControlVisibility(object sender, EventArgs e)
        {
            if (sender != tbName && sender != lblName)
                if (_Id != null)
                {
                    OrgName = tbName.Text;
                    tbName.Visible = false;
                    lblName.Visible = true;
                }
            if (sender != tbCountry && sender != lbCountry)
            {
                lbCountry.Visible = false;
            }
            if (sender != tbRegion && sender != lbRegion)
            {
                lbRegion.Visible = false;
            }
            if (sender != tbCity && sender != lbCity)
            {
                lbCity.Visible = false;
            } 
            if (sender != tbStreet && sender != lbStreet)
            {
                lbStreet.Visible = false;
            }
            if (sender != tbCity && sender != lbCity)
            {
                lbCity.Visible = false;
            } 
            if (sender != tbStreet && sender != lbStreet)
            {
                lbStreet.Visible = false;
            }
            if (sender != tbHouse && sender != lbHouse)
            {
                lbHouse.Visible = false;
            }
            if (sender != lbFiles)
            {
                lbFiles.ResetWidthLocation();
            }
        }
        private void lblName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lblName.Visible = false;
            tbName.Visible = true;
            tbName.Focus();
        }
     
        #region FillCard
        public void FillCard()
        {
            FillOrganizationName();
            
            ExtraInit(false);
            FillPartnerInfo();
            FillPartnerContacts();
            FillPartnersInboxMessages();
            ExtraInit(true);
        }
        private void FillPartnerInfo()
        {
            lbCountry.Visible = false;
            FillCountry();
            if (_Id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var Partner = (from x in context.Organization
                                   where x.Id == _Id
                                   select x).First();
                    OrgName = Partner.Name;
                    NameEng = Partner.NameEng;

                    ShortName = Partner.ShortName;
                    ShortNameEng = Partner.ShortNameEng;

                    Contact = Partner.Contact;

                    INN = Partner.INN;
                    Email = Partner.Email;
                    WebSite = Partner.WebSite;
                    Fax = Partner.Fax;
                    Phone = Partner.Phone;
                    Mobiles = Partner.Mobiles;
                    CountryId = Partner.CountryId;
                    FillCountry(CountryId);
                    RegionId = Partner.RegionId;
                    City = Partner.City;
                    FillRegion(RegionId);
                    RegionCode = (RegionId.HasValue) ? Util.lstRegionCode.Where(x => x.Key == RegionId.Value).Select(x => x.Value).First() : "";
                    CodeKLADR = Partner.CodeKLADR;
                    Street = Partner.Street;
                    lbStreet.Visible = false;
                    House = Partner.House;
                    lbHouse.Visible = false;
                    Code = Partner.Code;
                    Comment = Partner.Comment;
                }
        }
        private void FillPartnerContacts()
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                lstContacts = new PartnerContactPersonList(this.dgvContacts);

                if (_Id.HasValue)
                    lstContacts.lst = (from x in context.OrganizationContacts
                                   where x.OrganizationId == _Id
                                   select new PartnerContactPerson()
                                   { 
                                       Id = x.Id,
                                       Name = x.Name,
                                       NameEng = x.NameEng,
                                       Position = x.Position, 
                                       Comment = x.Comment,
                                       Email = x.Email,
                                       Phone = x.Phone,
                                       Mobiles = x.Mobiles
                                   }).ToList();

                List<string> lstColumnsAll = new List<string>() { "GuidId", "Id", "ФИО", "Должность", "Комментарий" };
                foreach ( string s in lstColumnsAll)
                    dgvContacts.Columns.Add(s, s);

                foreach (var c in lstContacts.lst)
                    dgvContacts.Rows.Add(c.GuidId, c.Id, c.Name, c.Position, c.Comment);

                List<string> lstColumnsNotVisible = new List<string>() { "Id" , "GuidId" };
                foreach (string s in lstColumnsNotVisible)
                if (dgvContacts.Columns.Contains(s))
                    dgvContacts.Columns[s].Visible = false;

                if (dgvContacts.Columns.Contains("Комментарий"))
                    dgvContacts.Columns["Комментарий"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                CurrentContactRowIndex = (dgvContacts.CurrentCell != null) ? dgvContacts.CurrentCell.RowIndex : -1;
            }
        }
        private void FillPartnerContactsPerson()
        {
            if (ContactId.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var OrganizationContacts = (from x in context.OrganizationContacts
                                                where x.Id == ContactId.Value
                                                select x).First();
                    ContactName = OrganizationContacts.Name;
                    ContactNameEng = OrganizationContacts.NameEng;
                    ContactPosition = OrganizationContacts.Position;
                    ContactPhone = OrganizationContacts.Phone;
                    ContactMobiles = OrganizationContacts.Mobiles;
                    ContactEmail = OrganizationContacts.Email;
                    ContactComment = OrganizationContacts.Comment;
                }
            else
            {
                var c = lstContacts.Where(ContactGuidId);
                ContactName = c.Name;
                ContactNameEng = c.NameEng;
                ContactPosition = c.Position;
                ContactComment = c.Comment;
                ContactPhone = c.Phone;
                ContactMobiles = c.Mobiles;
                ContactEmail = c.Email;
            }
        }
        private void FillPartnersInboxMessages()
        {
            this.lbFiles.CanGrow = true;
            this.lbFiles.GrowLeft = true;
            this.lbFiles.GrowWidth = 90;

            if (_Id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from inb in context.InboxMessage 
                               where inb.OrganizationId == _Id
                               select new
                               {
                                   Id = inb.Id,
                                   Тема = inb.Theme,
                                   Автор = inb.Author,
                                   Дата_отправки = inb.Date,
                                   Текст = inb.Text,
                               }).OrderByDescending(x=>x.Дата_отправки).ToList();

                    dgvInboxMessages.DataSource = lst;
                    if (dgvInboxMessages.Columns.Contains("Id"))
                        dgvInboxMessages.Columns["Id"].Visible = false;
                }
        }
        private void FillInboxMessage()
        {
            if (InboxMessageId.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from inb in context.InboxMessage
                               where inb.Id == InboxMessageId
                               select new
                               {
                                   Тема = inb.Theme,
                                   Текст = inb.Text,
                                   Автор = inb.Author,
                                   Дата_отправки = inb.Date
                               }).FirstOrDefault();

                    MessageAuthor = lst.Автор;
                    MessageText = lst.Текст;
                    MessageTheme = lst.Тема;
                    MessageDate = lst.Дата_отправки.Value;

                    MessageFiles = new List<KeyValuePair<int, string>>();
                    var files = (from inb in context.InboxMessageFile
                                 where inb.InboxMessageId == InboxMessageId
                                 select new
                                 {
                                     inb.Id,
                                     inb.FileName,
                                 }).ToList();

                    MessageFiles = (from x in files
                                    select new KeyValuePair<int, string>(x.Id, x.FileName)).ToList();
                    lbFiles.DataSource = new BindingSource(MessageFiles, null);
                    lbFiles.DisplayMember = "Value";
                    lbFiles.ValueMember = "Key";
                    lbFiles.SelectedIndices.Clear();
                }
        } 
        private void FillOrganizationName()
        {
            lblName.Visible = (_Id != null);
            tbName.Visible = (_Id == null);
        }
        #endregion

        #region CheckSaveUpdate_CommonInformation
        private bool CheckFields()
        {
            if (String.IsNullOrEmpty(OrgName))
            {
                err.SetError(tbName, "не задано название");
                tabControl1.SelectedTab = tabPage1;
                return false;
            }
            else
                err.Clear();



            if (!CountryId.HasValue)
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var x = (from c in context.Country
                             where c.Name == tbCountry.Text.Trim()
                             select c.Id).ToList();
                    if (x.Count() == 1)
                    {
                        CountryId = x.First();
                        err.Clear();
                    }
                    else
                    {
                        err.SetError(tbCountry, "не выбрана страна");
                        tabControl1.SelectedTab = tabPage1;
                        return false;
                    }
                }
            }
            else err.Clear();

            if (!RegionId.HasValue && CountryId == Util.countryRussiaId)
            {
                err.SetError(tbRegion, "не выбран регион");
                tabControl1.SelectedTab = tabPage1;
                return false;
            }
            else err.Clear();

            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckFields())
                return;

            string str = "";
            if (_Id == null)
            {
                if (!OrganizationRecInsert())
                    return;
                str = "Сохранение успешно завершено";
            }
            else
            {
                if (!OrganizationRecUpdate())
                    return;
                str = "Обновление данных успешно завершено";
            }

            if (_hndl != null)
                _hndl(_Id);

            if (!SaveAllContacts())
                return;

            Timer t = new Timer();
            t.Interval = 5 * 1000; // секунды * 1000
            t.Tick += new EventHandler(t_Tick);
            SaveSuccess.SetError(btnSave, str);
            SaveSuccess.BlinkRate = 0;
            SaveSuccess.SetIconAlignment(btnSave, ErrorIconAlignment.MiddleLeft);
            t.Start();
        }
        private bool OrganizationRecInsert()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    Guid gId = Guid.NewGuid();
                    context.Organization.Add(new Organization()
                    {
                        rowguid = gId,
                        Name = OrgName,
                        NameEng = NameEng,
                        ShortName = ShortName,
                        ShortNameEng = ShortNameEng,
                        Contact = Contact,
                        INN = INN,
                        Email = Email,
                        WebSite = WebSite,
                        CountryId = CountryId,
                        RegionId = RegionId,
                        City = City,
                        Street = Street,
                        House = House,
                        Code = Code,
                        CodeKLADR = CodeKLADR,
                        Fax = Fax,
                        Phone = Phone,
                        Mobiles = Mobiles,
                        Comment = Comment,
                    });
                    context.SaveChanges();
                    _Id = context.Organization.Where(x => x.rowguid == gId).Select(x => x.Id).First();
                    return true;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ошибка при сохранении карточки\r\n" + exc.InnerException, "");
                return false;
            }
        }
        private bool OrganizationRecUpdate()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var Org = context.Organization.Where(x => x.Id == _Id).First();
                    Org.Name = OrgName;
                    Org.NameEng = NameEng;

                    Org.ShortName = ShortName;
                    Org.ShortNameEng = ShortNameEng;
                    Org.Contact = Contact;

                    Org.INN = INN;
                    Org.Email = Email;
                    Org.WebSite = WebSite;
                    Org.Fax = Fax;
                    Org.Phone = Phone;
                    Org.Mobiles = Mobiles;
                    Org.Comment = Comment;
                    Org.CountryId = CountryId;
                    Org.RegionId = RegionId;
                    Org.City = City;
                    Org.Street = Street;
                    Org.House = House;
                    Org.Code = Code;
                    Org.CodeKLADR = CodeKLADR;

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при сохранении карточки", "");
                return false;
            }
        }
        private void t_Tick(object sender, EventArgs e)
        {
            Timer t = (Timer)sender;
            t.Stop();
            SaveSuccess.Clear();
        }
        #endregion

        #region Contacts
        private void lbContactAdd_Click(object sender, EventArgs e)
        {
            UpdateGridAfterClick();
            ContactPersonSetFieldsEmpty();

            ContactGuidId = Guid.NewGuid();
            lstContacts.Add(ContactGuidId); 
            
            CurrentContactRowIndex = (dgvContacts.CurrentCell!=null) ? dgvContacts.CurrentCell.RowIndex : -1;
        }
        private void ContactPersonSetFieldsEmpty()
        {
            ContactName = ContactNameEng = ContactPosition = ContactPhone = 
            ContactMobiles = ContactEmail = ContactComment = "";
            ContactId = null;
            dgvContacts.ClearSelection();
        }
        private bool CheckContactFields()
        {
            if (String.IsNullOrEmpty(ContactName))
            {
                err.SetError(tbContactName, "отсутствует значение");
                tabControl1.SelectedTab = tabPage2;
                return false;
            }
            else
                err.Clear();

            return true;
        }
        private bool ContactRecInsert()
        {
            if (_Id.HasValue)
            {
                if (!CheckContactFields())
                    return false;

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var NewContact = new OrganizationContacts()
                        {
                            Name = ContactName,
                            NameEng = ContactNameEng,
                            Phone = ContactPhone,
                            Mobiles = ContactMobiles,
                            Email = ContactEmail,
                            Position = ContactPosition,
                            Comment = ContactComment,
                            OrganizationId = _Id.Value,
                        };
                    context.OrganizationContacts.Add(NewContact);
                    context.SaveChanges();
                    ContactId = NewContact.Id;

                    lstContacts.Update(ContactGuidId, ContactId, ContactName, ContactPosition, ContactComment, ContactNameEng, ContactEmail, ContactPhone, ContactMobiles);
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Сохраните карточку Организации-Партнера");
                return false;
            }
        }
        private void ContactRecUpdate()
        {
            if (ContactId.HasValue)
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var Person = context.OrganizationContacts.Where(x => x.Id == ContactId).First();
                    Person.Name = ContactName;
                    Person.NameEng = ContactNameEng;
                    Person.Phone = ContactPhone;
                    Person.Mobiles = ContactMobiles;
                    Person.Email = ContactEmail;
                    Person.Position = ContactPosition;
                    Person.Comment = ContactComment;
                    context.SaveChanges();
                }
            }
            lstContacts.Update(ContactGuidId, ContactId, ContactName, ContactPosition, ContactComment,  ContactNameEng, ContactEmail, ContactPhone, ContactMobiles);
        }
        private void dgvContacts_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvContacts.Rows)
            {
                if (row.Cells["GuidId"].Value.ToString() == ContactGuidId.ToString())
                {
                    dgvContacts.CurrentCell = row.Cells[2];
                    CurrentContactRowIndex = dgvContacts.CurrentCell.RowIndex;
                    break;
                }
            }
        }
        private void dgvContacts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateGridAfterClick();
        }
        private void UpdateGridAfterClick()
        {
            err.Clear();
            if (ContactGuidId != Guid.Empty)
                ContactRecUpdate();

            if (dgvContacts.CurrentCell != null)
            {
                if (CurrentContactRowIndex != dgvContacts.CurrentCell.RowIndex)
                {
                    if (!String.IsNullOrEmpty(dgvContacts.CurrentRow.Cells["Id"].Value.ToString()))
                        ContactId = int.Parse(dgvContacts.CurrentRow.Cells["Id"].Value.ToString());
                    else
                        ContactId = null;

                    ContactGuidId = Guid.Parse(dgvContacts.CurrentRow.Cells["GuidId"].Value.ToString());

                    CurrentContactRowIndex = dgvContacts.CurrentCell.RowIndex;
                    FillPartnerContactsPerson();
                }
                else
                { }
            }
            else
                ContactPersonSetFieldsEmpty();
        }
        private void dgvContacts_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            CurrentContactRowIndex = dgvContacts.CurrentRow.Index;
        }
        private void btnContactDelete_Click(object sender, EventArgs e)
        {
            if (dgvContacts.CurrentCell != null && ContactGuidId != Guid.Empty)
            {
                if (MessageBox.Show("Точно удалить?", "", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    if (ContactId.HasValue)
                    {
                        using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                        {
                            var t = context.OrganizationContacts.Where(x => x.Id == ContactId).First();
                            context.OrganizationContacts.Remove(t);
                            context.SaveChanges();
                        }
                    }
                    var c = lstContacts.Where(ContactGuidId);
                    ContactId = null;
                    ContactGuidId = Guid.Empty;
                    lstContacts.Remove(c);
                    CurrentContactRowIndex = -1; 
                    UpdateGridAfterClick();

                }
            }
            else
            { }
        }
        private bool SaveAllContacts()
        {
            bool res = true;
            if (_Id.HasValue)
                foreach (var l in lstContacts.lst)
                {
                    if (l.Id == null)
                    {
                        lstContacts.FindRow(l.GuidId);
                        UpdateGridAfterClick();
                        if (!ContactRecInsert())
                        {
                            return false ; 
                        }
                    }
                }
            return res;
        }
        #endregion

        #region Files_Messages
        private void btnFileDownoad_Click(object sender, EventArgs e)
        {
            List<int> lst = new List<int>();
            foreach (var lb in lbFiles.SelectedItems)
                if (lb is KeyValuePair<int, string>)
                {
                    lst.Add(((KeyValuePair<int, string>)lb).Key);
                }

            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lstFiles = (from x in context.InboxMessageFile
                                where lst.Contains(x.Id)
                                select new
                                {
                                    x.Id, x.FileName, x.FileData
                                }).ToList();
                string NewOrganizationFolder = Util.TempFilesFolder + OrgName;
                
                if (lstFiles.Count>0)
                    if (!Directory.Exists(NewOrganizationFolder))
                            Directory.CreateDirectory(NewOrganizationFolder);

                bool FileSaved = false;
                foreach (var file in lstFiles)
                {
                    if (file.FileData == null)
                        return;

                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.InitialDirectory = NewOrganizationFolder + @"\";
                    saveFileDialog1.FileName = file.FileName;

                    string filename = file.FileName.Substring(0, file.FileName.LastIndexOf('.'));
                    string extenion = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                    string FullName = file.FileName;
                    int ind = 1;
                    while (File.Exists(NewOrganizationFolder + @"\" + FullName))
                    {
                        FullName = filename + "_" + ind.ToString() + extenion;
                        ind++;
                    }


                    // If the file name is not an empty string open it for saving.
                    if (saveFileDialog1.FileName != "")
                    {
                        try
                        {
                            using (FileStream fsNew = new FileStream(NewOrganizationFolder + @"\" + FullName,
                                   FileMode.Create, FileAccess.Write))
                            {
                                fsNew.Write(file.FileData, 0, file.FileData.Length);
                                fsNew.Flush();
                                fsNew.Close();
                            }
                            FileSaved = true;
                        }
                        catch (FileNotFoundException ioEx)
                        {
                            Console.WriteLine(ioEx.Message);
                        }
                        catch (Exception xc)
                        {
                            MessageBox.Show("[Возможное решение: сохраните как новый файл]\n\nОшибка во время сохранения:\n" + (xc.InnerException ?? xc), "Ошибка");
                        }
                    }
                    else
                    {

                    }
                    
                }
                if (FileSaved)
                    System.Diagnostics.Process.Start(NewOrganizationFolder + @"\");
            }
        }
        private void dgvInboxMessages_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgvInboxMessages.CurrentCell != null)
                if (dgvInboxMessages.CurrentCell.RowIndex != CurrentFilesRowIndex)
                {
                    InboxMessageId = int.Parse(dgvInboxMessages.CurrentRow.Cells["Id"].Value.ToString());
                    FillInboxMessage();
                    CurrentFilesRowIndex = dgvInboxMessages.CurrentCell.RowIndex; 
                    lbFiles.Click -= new EventHandler(lbFiles_Click);

                    btnAddMessage.Visible = false;
                    lblDownloadFile.Visible = btnFileDownoad.Visible = true;
                }
        }
        private void lblAddMessage_Click(object sender, EventArgs e)
        {
            MessageDate = DateTime.Now;
            MessageText = "";
            MessageTheme = "";
            MessageFiles = new List<KeyValuePair<int, string>>();
            lbFiles.DataSource = null;
            MessageFileAdd = new List<InboxFile>();
            btnAddMessage.Visible = true;
            lblDownloadFile.Visible = btnFileDownoad.Visible = false;
            lbFiles.Click += new EventHandler(lbFiles_Click);
            dgvInboxMessages.ClearSelection();
        }
        private void lbFiles_Click(object sender, EventArgs e)
        {
            InboxFile file = new InboxFile();
            if (file.FileData != null)
            {
                MessageFileAdd.Add(file);
                List<string> lst = (from s in MessageFileAdd select s.FileName).ToList();
                lbFiles.DataSource = new BindingSource(lst, null);
                int kk = lbFiles.SelectedItems.Count;
            }
        }
        private void btnAddMessage_Click(object sender, EventArgs e)
        {
            if (_Id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    InboxMessage mes = new InboxMessage()
                    {
                        OrganizationId = _Id.Value,
                        Text = MessageText,
                        Theme = MessageTheme,
                        Author = "",
                        Date = DateTime.Now,
                    };
                    context.InboxMessage.Add(mes);
                    context.SaveChanges();

                    foreach (var x in MessageFileAdd)
                    {
                        context.InboxMessageFile.Add(new InboxMessageFile()
                        {
                              FileName = x.FileName,
                              FileData = x.FileData,
                              InboxMessageId=mes.Id,
                        });
                   
                    } 
                    context.SaveChanges();
                    FillPartnersInboxMessages();
                }
            else
                MessageBox.Show("Сохраните карточку Организации-Партнера");
        }
        private void dgvInboxMessages_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CurrentFilesRowIndex = -1;
        }
        #endregion
       
        #region Country
        private void FillCountry()
        {
            List<KeyValuePair<int, string>> klst = (from x in Util.lstCountry
                                                    where x.Value.ToLower().Contains(tbCountry.Text.ToLower())
                                                    select x).ToList();
            lbCountry.DataSource = klst;
            lbCountry.DisplayMember = "Value";
        }
        private void FillCountry(int? id)
        {
            tbCountry.TextChanged -= new EventHandler(tbCountry_TextChanged);
            if (id.HasValue)
            {
                tbCountry.Text = (from x in Util.lstCountry
                                  where x.Key == id.Value
                                  select x.Value).First();
            }
            tbCountry.TextChanged += new EventHandler(tbCountry_TextChanged);
        }
        private void tbCountry_TextChanged(object sender, EventArgs e)
        {
            lbCountry.Visible = true;
            CountryId = null;
            FillCountry();
        }
        private void lbCountry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lbCountry.SelectedValue != null)
            {
                sCountry = ((KeyValuePair<int, string>)lbCountry.SelectedItem).Value.ToString();
                CountryId = int.Parse(((KeyValuePair<int, string>)lbCountry.SelectedItem).Key.ToString());
                lbCountry.Visible = false;
                UpdateVisibleRegion();
                if (CountryId != Util.countryRussiaId)
                    CodeKLADR = "";
            }
        }
        private void lbCountry_MouseClick(object sender, MouseEventArgs e)
        {
            if (lbCountry.SelectedValue != null)
            {
                sCountry = ((KeyValuePair<int, string>)lbCountry.SelectedItem).Value.ToString();
                CountryId = int.Parse(((KeyValuePair<int, string>)lbCountry.SelectedItem).Key.ToString());
                lbCountry.Visible = false;
                UpdateVisibleRegion();
                if (CountryId != Util.countryRussiaId)
                    CodeKLADR = "";
            }
        }
        private void tbCountry_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lbCountry.Visible = true;
            if (CountryId == null)
            {
                CountryId = Util.countryRussiaId;
                FillCountry(CountryId);
                FillCountry();
                lbCountry.Visible = false;
                UpdateVisibleRegion();
            }
        }
        private void UpdateVisibleRegion()
        {
            if (CountryId != Util.countryRussiaId)
            {
                lbRegion.Visible = false;
                tbRegion.TextChanged -= tbRegion_TextChanged;
                tbRegion.Text = "";
                tbRegion.TextChanged += tbRegion_TextChanged;

                tbRegion.Enabled = false;
                RegionId = null;
                RegionCode = "";
            }
            else
                tbRegion.Enabled = true;
        }
        private void tbCountry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                lbCountry.Focus();
                if (lbCountry.Items.Count > lbCountry.SelectedIndex + 1)
                    lbCountry.SetSelected(lbCountry.SelectedIndex + 1, true);
            }
            else
                if (e.KeyCode == Keys.Enter)
                {
                    if (lbCountry.SelectedValue != null)
                    {
                        sCountry = ((KeyValuePair<int, string>)lbCountry.SelectedItem).Value.ToString();
                        CountryId = int.Parse(((KeyValuePair<int, string>)lbCountry.SelectedItem).Key.ToString());
                        lbCountry.Visible = false;
                        UpdateVisibleRegion();
                        if (CountryId != Util.countryRussiaId)
                            CodeKLADR = "";
                    }
                }
        }
        #endregion

        #region Region
        private void tbRegion_TextChanged(object sender, EventArgs e)
        {
            lbRegion.Visible = (CountryId  == Util.countryRussiaId);
            RegionId = null;
            RegionCode = "";
            FillRegion();
        }
        private void FillRegion()
        {
            List<KeyValuePair<int, string>> klst = (from x in Util.lstRegion
                                                    where x.Value.ToLower().Contains(sRegion.ToLower())
                                                    select x).ToList();
            lbRegion.DataSource = klst;
            lbRegion.DisplayMember = "Value";
        }
        private void FillRegion(int? id)
        {
            if (CountryId != Util.countryRussiaId)
            {
                lbRegion.Visible = false;
                tbRegion.Text = "";
                tbRegion.Enabled = false;
                RegionId = null;
                RegionCode = "";
            }
            else
            {
                tbRegion.Enabled = true;
                tbRegion.TextChanged -= new EventHandler(tbRegion_TextChanged);
                if (id.HasValue)
                {
                    tbRegion.Text = (from x in Util.lstRegion
                                     where x.Key == id.Value
                                     select x.Value).First();
                }
                tbRegion.TextChanged += new EventHandler(tbRegion_TextChanged);
            }
        }
        private void lbRegion_MouseClick(object sender, MouseEventArgs e)
        {
            if (lbRegion.SelectedValue != null)
            {
                sRegion = ((KeyValuePair<int, string>)lbRegion.SelectedItem).Value.ToString();
                RegionId = int.Parse(((KeyValuePair<int, string>)lbRegion.SelectedItem).Key.ToString());
                RegionCode = Util.lstRegionCode.Where(x => x.Key == RegionId).Select(x => x.Value).First();
                lbRegion.Visible = false;
            }
        }
        private void lbRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lbRegion.SelectedValue != null)
            {
                sRegion = ((KeyValuePair<int, string>)lbRegion.SelectedItem).Value.ToString();
                RegionId = int.Parse(((KeyValuePair<int, string>)lbRegion.SelectedItem).Key.ToString());
                RegionCode = Util.lstRegionCode.Where(x => x.Key == RegionId).Select(x => x.Value).First();
                lbRegion.Visible = false;
            }
        }
        private void tbRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (lbRegion.Visible)
                if (e.KeyCode == Keys.Down)
                {
                    lbRegion.Focus();
                    if (lbRegion.Items.Count > lbRegion.SelectedIndex +1)
                        lbRegion.SetSelected(lbRegion.SelectedIndex + 1, true);
                }
                else if (e.KeyCode== Keys.Enter)
                {
                    if (lbRegion.SelectedValue != null)
                    {
                        sRegion = ((KeyValuePair<int, string>)lbRegion.SelectedItem).Value.ToString();
                        RegionId = int.Parse(((KeyValuePair<int, string>)lbRegion.SelectedItem).Key.ToString());
                        RegionCode = Util.lstRegionCode.Where(x => x.Key == RegionId).Select(x => x.Value).First();
                        lbRegion.Visible = false;
                    }
                }
        }
        #endregion

        #region City
        private void lbCity_MouseClick(object sender, MouseEventArgs e)
        {
            if (lbCity.SelectedValue != null)
            {
                City = (string)lbCity.SelectedItem;
                GetCodeKLADR();
                lbCity.Visible = false;
            }
        }
        private void tbCity_TextChanged(object sender, EventArgs e)
        {
            lbCity.Visible = (CountryId == Util.countryRussiaId);
            FillCity();
        }
        private void FillCity()
        {
            if (!string.IsNullOrEmpty(RegionCode) && CountryId == Util.countryRussiaId)
            {
                List<string> lst = KLADR.KLADR.GetCitiesInRegion(RegionCode).Where(x => x.ToLower().Contains(City.ToLower())).ToList();
                lbCity.DataSource = lst;
            }
            else if (CountryId != Util.countryRussiaId)
            {
                lbCity.DataSource = Util.lstCountryCity.Where(x => x.Key == CountryId && x.Value.ToLower().Contains(City.ToLower())).Select(x=>x.Value).ToList();
                if (lbCity.Items.Count > 0)
                    lbCity.Visible = true;
            }
        }
        private void lbCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lbCity.SelectedValue != null)
            {
                City = (string)lbCity.SelectedItem;
                GetCodeKLADR();
                lbCity.Visible = false;
            }
        }
        private void tbCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (lbCity.Visible)
                if (e.KeyCode == Keys.Down)
                {
                    lbCity.Focus();
                    if (lbCity.Items.Count > lbCity.SelectedIndex + 1)
                        lbCity.SetSelected(lbCity.SelectedIndex + 1, true);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (lbCity.SelectedValue != null)
                    {
                        City = (string)lbCity.SelectedItem;
                        GetCodeKLADR();
                        lbCity.Visible = false;
                    }
                }
        }
        #endregion

        #region Street
        private void lbStreet_MouseClick(object sender, MouseEventArgs e)
        {
            if (lbStreet.SelectedValue != null)
            {
                Street = (string)lbStreet.SelectedItem;
                GetCodeKLADR();
                lbStreet.Visible = false;
            }
        }
        private void tbStreet_TextChanged(object sender, EventArgs e)
        {
            lbStreet.Visible = (CountryId == Util.countryRussiaId);
            FillStreet();
            GetCodeKLADR();
        }
        private void FillStreet()
        {
            if (!String.IsNullOrEmpty(RegionCode) && CountryId == Util.countryRussiaId && !String.IsNullOrEmpty(City))
            {
                List<string> lstStreet = KLADR.KLADR.GetStreetsInCity(RegionCode, City).Where(x => x.ToLower().Contains(Street.ToLower())).ToList() ;
                lbStreet.DataSource = lstStreet;
            }
            else if (CountryId != Util.countryRussiaId && CountryId != null)
            {
                lbStreet.DataSource = Util.lstCityStreet.Where(x => x.Key.ToLower() == City.ToLower() && x.Value.ToLower().Contains(Street.ToLower())).Select(x=>x.Value).ToList();
                if (lbStreet.Items.Count > 0)
                    lbStreet.Visible = true;
            }
        } 
        private void lbStreet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lbStreet.SelectedValue!=null)
            {
                Street = (string)lbStreet.SelectedItem;
                GetCodeKLADR();
                lbStreet.Visible = false;
            }
        }
        private void tbStreet_KeyDown(object sender, KeyEventArgs e)
        {
            if (lbStreet.Visible)
                if (e.KeyCode == Keys.Down)
                {
                    lbStreet.Focus();
                    if (lbStreet.Items.Count > lbStreet.SelectedIndex + 1)
                        lbStreet.SetSelected(lbStreet.SelectedIndex + 1, true);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (lbStreet.SelectedValue != null)
                    {
                        Street = (string)lbStreet.SelectedItem;
                        lbStreet.Visible = false;
                    }
                }
        }
        #endregion

        #region House
        private void lbHouse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lbStreet.SelectedValue != null)
            {
                House = (string)lbHouse.SelectedItem;
                GetCodeKLADR();
                lbHouse.Visible = false;
            }
        }
        private void lbHouse_MouseClick(object sender, MouseEventArgs e)
        {
            if (lbHouse.SelectedValue != null)
            {
                House = (string)lbHouse.SelectedItem;
                GetCodeKLADR();
                lbHouse.Visible = false;
            }
        }
        private void tbHouse_TextChanged(object sender, EventArgs e)
        {
            lbHouse.Visible = (CountryId == Util.countryRussiaId);
            FillHouses();
        }
        private void tbHouse_KeyDown(object sender, KeyEventArgs e)
        {
            if (CountryId  == Util.countryRussiaId)
                if (e.KeyCode == Keys.Down)
                {
                    lbHouse.Focus();
                    if (lbHouse.Items.Count > lbHouse.SelectedIndex + 1)
                        lbHouse.SetSelected(lbHouse.SelectedIndex + 1, true);
                }
                else
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (lbHouse.SelectedValue != null)
                        {
                            House = (string)lbHouse.SelectedItem;
                            GetCodeKLADR();
                            lbHouse.Visible = false;
                        }
                    }
        }
        private void FillHouses()
        {
            if (!String.IsNullOrEmpty(RegionCode) && CountryId == Util.countryRussiaId && !String.IsNullOrEmpty(City) && !String.IsNullOrEmpty(Street))
            {
                List<string> lstHouses = KLADR.KLADR.GetHouses(RegionCode, City, Street).Where(x => x.ToLower().Contains(House.ToLower())).ToList();
                lbHouse.DataSource = lstHouses;
            }
        }
        #endregion 

        #region PostCode & CodeKLADR
        private void GetCodeKLADR()
        {
            if (!string.IsNullOrEmpty(RegionCode) && CountryId == Util.countryRussiaId)
            {
                CodeKLADR =
                     (!String.IsNullOrEmpty(Street) && !String.IsNullOrEmpty(House)) ? KLADR.KLADR.GetKLADRCode(RegionCode, City, Street, House) :
                     (!String.IsNullOrEmpty(Street) ? KLADR.KLADR.GetKLADRCode(RegionCode, City, Street) :
                     KLADR.KLADR.GetKLADRCode(RegionCode, City))
                     ;

                Code =
                    (!String.IsNullOrEmpty(Street) && !String.IsNullOrEmpty(House)) ? KLADR.KLADR.GetPostIndex(RegionCode, City, Street, House) :
                    (!String.IsNullOrEmpty(Street) ? KLADR.KLADR.GetPostIndex(RegionCode, City, Street) :
                    KLADR.KLADR.GetPostIndex(RegionCode, City))
                    ;
            }
            else
            {
                CodeKLADR = Code = string.Empty;
            }
        }
        #endregion

    }

}
