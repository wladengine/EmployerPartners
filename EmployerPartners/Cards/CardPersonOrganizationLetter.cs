using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmployerPartners.EDMX;
using System.IO;
using Novacode;
using System.Diagnostics;
using WordOut;

namespace EmployerPartners 
{
    public partial class CardPersonOrganizationLetter : Form
    {
        private LetterList lstLetter; 
        public CardPersonOrganizationLetter(List<int> ppIds)
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            lstLetter = new LetterList(ppIds);
            pBar1.Visible = false;
            pBar1.Minimum = 0;
            GetPersonOrgList();
            FillCard();
        }
        private void GetPersonOrgList()
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var ppInfo = (from x in context.PartnerPerson
                              join o in context.OrganizationPerson on x.Id equals o.PartnerPersonId
                              join org in context.Organization on o.OrganizationId equals org.Id
                              where lstLetter.Ids.Contains(x.Id)
                              orderby o.Sorting
                              select new
                              {
                                  OrgId = org.Id,
                                  PartnerId = x.Id,
                                  o.Sorting,
                              }).ToList();

                var lst = (from x in ppInfo
                           group x by x.PartnerId into _x
                           select new
                           {
                               PartnerId = _x.Key,
                               lst = _x.Select(t => new { t.OrgId, t.Sorting }).ToList(),
                           }).ToList();

                foreach (var x in lst)
                {
                    if (x.lst.Where(t => t.Sorting > 0).Count() != 0)
                    {
                        while (x.lst.Where(t => t.Sorting > 0).Count() != 1)
                        {
                            x.lst.Remove(x.lst.Where(t => t.Sorting != x.lst.Select(p => p.Sorting).Min()).First());
                        }
                    }
                }
                foreach (var x in lst)
                {
                    Letter let = lstLetter.lst.Where(t => t.PartnerPersonId == x.PartnerId).First();
                    foreach (var orgs in x.lst)
                        let.OrgSorting.Add(orgs.OrgId);
                }
            }
        }

        private void FillCard()
        {
            rbAllPersonsToOneFile.Text = "(1 файл) Разместить все письма в одном файле";
            rbOnePersonToOneFile.Text = String.Format("({0} файл(ов)) Разместить все письма к одному партнеру в одном файле", lstLetter.lst.Count());
            rbOneOrganizationToOneFile.Text = string.Format("({0} файл(ов)) Разместить каждое письмо в новом файле", lstLetter.lst.SelectMany(x => x.OrgSorting).Count());

            pBar1.Maximum = lstLetter.lst.SelectMany(x => x.OrgSorting).Count();
        }

        /*private void GetLetter()
        {
            pBar1.Visible = true;
            
            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = "Письмо";
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var template = (from x in context.Templates
                                    where x.TemplateName == templatename
                                    select x).First();

                    fileByteArray = (byte[])template.FileData;
                    type = (string)template.FileType.Trim();
                    name = (string)template.FileName.Trim();
                    nameshort = name.Substring(0, name.Length - type.Length);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Не удалось получить данные...\r\n" + exc.Message, "Сообщение");
                return;
            }
            string TempFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\EmployerPartners_TempFiles\"+DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString().Replace(":","-")+@"\" ;    //@"\Приказы по составам ГЭК\";
            try
            {
                if (!Directory.Exists(TempFilesFolder))
                    Directory.CreateDirectory(TempFilesFolder);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось создать директорию.\r\n" + ex.Message, "Сообщение");
                return;
            }

            string TemplatePath = TempFilesFolder + nameshort + " template " + Guid.NewGuid().ToString() + type; 
            //Запись на диск
            try
            {
                FileStream fileStream = new FileStream(TemplatePath, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter binWriter = new BinaryWriter(fileStream);
                binWriter.Write(fileByteArray);
                binWriter.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось открыть файл...", "Сообщение");
                return;
            }
            string filename = String.Format("{0}{1} {2}{3}{4}{5}", TempFilesFolder, nameshort, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Millisecond, type);
                    
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    using (FileStream fs = new FileStream(filename, FileMode.CreateNew, FileAccess.ReadWrite))
                    using (DocX doc = DocX.Create(fs))
                    {
                        foreach (var letter in lstLetter.lst)
                        {
                            var ppinfo = (from x in context.PartnerPerson
                                          join o in context.OrganizationPerson on x.Id equals o.PartnerPersonId
                                          join org in context.Organization on o.OrganizationId equals org.Id
                                          join orgsub in context.OrganizationSubdivision on o.OrganizationSubdivisionId equals orgsub.Id into _orgsub
                                          from orgsub in _orgsub.DefaultIfEmpty()
                                          where x.Id == letter.PartnerPersonId && letter.OrgSorting.Contains(o.OrganizationId)
                                          orderby o.Sorting
                                          select new
                                          {
                                              FullName = x.FirstName + ((" " + x.SecondName) ?? ""),
                                              x.SexMale,
                                              Surname = x.LastName,
                                              Name = x.FirstName,
                                              SecondName = x.SecondName,
                                              OrganixationName = org.Name,
                                              Subdivision = (orgsub != null) ? orgsub.Name : "",
                                              org.Street,
                                              org.House,
                                              org.City,
                                              org.Code,
                                              OrgId = org.Id,
                                              o.Sorting,
                                          }).ToList();

                            int i = 1;
                            foreach (var PPInfo in ppinfo)
                            {
                                pBar1.Value++;
                                pBar1.Invalidate();

                                using (FileStream fsP = new FileStream(TemplatePath, FileMode.Open, FileAccess.Read))
                                using (DocX docP = DocX.Load(fsP))
                                {
                                    string City = PPInfo.City;
                                    if (PPInfo.City.StartsWith("г. "))
                                        City = "г." + PPInfo.City.Substring(3);
                                    docP.ReplaceText("&&Организация", PPInfo.OrganixationName);
                                    docP.ReplaceText("&&Подразделение", string.IsNullOrEmpty(PPInfo.Subdivision) ? "" : (PPInfo.Subdivision + "\r\n"));
                                    string Name = ((String.IsNullOrEmpty(PPInfo.Name) ? "" : (PPInfo.Name[0] + ".")) +
                                        (String.IsNullOrEmpty(PPInfo.SecondName) ? "" : (PPInfo.SecondName[0] + ".")) + " " + PPInfo.Surname).Trim();

                                    docP.ReplaceText("&&ИОФамилия", Name);
                                    docP.ReplaceText("&&Улица", PPInfo.Street);
                                    docP.ReplaceText("&&Дом", "д. " + PPInfo.House);
                                    docP.ReplaceText("&&Город", City);
                                    docP.ReplaceText("&&Индекс", PPInfo.Code);
                                    docP.ReplaceText("&&ФИО", (!PPInfo.SexMale.HasValue ? "ый(ая) " : (PPInfo.SexMale.Value ? "ый " : "ая ")) + PPInfo.FullName);
                                    docP.ReplaceText("&&КонвертУлица", PPInfo.Street);
                                    docP.ReplaceText("&&КонвертДом", "д. " + PPInfo.House);
                                    docP.ReplaceText("&&КонвертГород", City);
                                    docP.ReplaceText("&&КонвертИндекс", PPInfo.Code);
                                    docP.ReplaceText("&&КонвертОрганизация", PPInfo.OrganixationName);
                                    docP.ReplaceText("&&КонвертПодразделение", string.IsNullOrEmpty(PPInfo.Subdivision) ? "" : (PPInfo.Subdivision + "\r\n"));
                                    docP.ReplaceText("&&КонвертИОФамилия", Name);

                                    //удалять ли созданные записи?
                                    context.PartnerPersonOrganizationLetter.Add(new PartnerPersonOrganizationLetter()
                                    {
                                        PartnerPersonId = letter.PartnerPersonId,
                                        OrganizationId = PPInfo.OrgId,
                                        Author = Util.GetUserName(),
                                        Timestamp = DateTime.Now,
                                        IsSend = false,
                                    });
                                    context.SaveChanges(); 
                                    
                                    doc.InsertDocument(docP);

                                    if (rbOneOrganizationToOneFile.Checked)
                                        docP.SaveAs(string.Format("{0}{1} {2}{3}", TempFilesFolder, PPInfo.Surname, i, type));
                                    i++;
                                }
                            }
                            //if (rbOnePersonToOneFile.Checked)
                            //    doc.SaveAs(string.Format("{0}{1} {2}{3}", TempFilesFolder, nameshort, "", type));
                        }
                        //doc.Paragraphs.ForEach(x => x.Font(new System.Drawing.FontFamily("Times New Roman")));
                        string file1 = string.Format("{0}{1} {2}{3}{4}{5}", TempFilesFolder, nameshort, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Millisecond, type);
                        if (rbAllPersonsToOneFile.Checked)
                        {
                            doc.SaveAs(file1);
                            //Process.Start(file1);
                        }
                       
                        Process.Start(TempFilesFolder);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сформировать документ\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            File.Delete(filename);
            File.Delete(TemplatePath);
        }*/
        private void GetLetterWordOut()
        {
            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = "ПисьмоОрганизации";
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var template = (from x in context.Templates
                                    where x.TemplateName == templatename
                                    select x).First();

                    fileByteArray = (byte[])template.FileData;
                    type = (string)template.FileType.Trim();
                    name = (string)template.FileName.Trim();
                    nameshort = name.Substring(0, name.Length - type.Length);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Не удалось получить данные...\r\n" + exc.Message, "Сообщение");
                return;
            }
            string TempFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\EmployerPartners_TempFiles\" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString().Replace(":", "-") + @"\";    //@"\Приказы по составам ГЭК\";
            try
            {
                if (!Directory.Exists(TempFilesFolder))
                    Directory.CreateDirectory(TempFilesFolder);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось создать директорию.\r\n" + ex.Message, "Сообщение");
                return;
            }

            string TemplatePath = TempFilesFolder + nameshort + " template " + Guid.NewGuid().ToString() + type;
            //Запись на диск
            try
            {
                FileStream fileStream = new FileStream(TemplatePath, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter binWriter = new BinaryWriter(fileStream);
                binWriter.Write(fileByteArray);
                binWriter.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось открыть файл...", "Сообщение");
                return;
            }

            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    WordDoc wd = new WordDoc(TemplatePath);
                    bool isCreated = true;
                    if (rbAllPersonsToOneFile.Checked)
                    {
                        wd = new WordDoc(TemplatePath);
                    }
                    {
                        foreach (var letter in lstLetter.lst)
                        {
                            var ppinfo = (from x in context.PartnerPerson
                                          join o in context.OrganizationPerson on x.Id equals o.PartnerPersonId
                                          join org in context.Organization on o.OrganizationId equals org.Id
                                          join orgsub in context.OrganizationSubdivision on o.OrganizationSubdivisionId equals orgsub.Id into _orgsub
                                          from orgsub in _orgsub.DefaultIfEmpty()
                                          where x.Id == letter.PartnerPersonId && letter.OrgSorting.Contains(o.OrganizationId)
                                          orderby o.Sorting
                                          select new
                                          {
                                              FullName = x.FirstName + ((" " + x.SecondName) ?? ""),
                                              x.SexMale,
                                              Surname = x.LastName,
                                              Name = x.FirstName,
                                              SecondName = x.SecondName,
                                              OrganixationName = org.Name,
                                              Subdivision = (orgsub != null) ? orgsub.Name : "",
                                              org.Street,
                                              org.House,
                                              org.City,
                                              org.Code,
                                              OrgId = org.Id,
                                              o.Sorting,
                                              org.Apartment
                                          }).ToList();

                            int i = 1;
                            if (rbOnePersonToOneFile.Checked && !isCreated)
                            {
                                wd = new WordDoc(TemplatePath);
                            }

                            foreach (var PPInfo in ppinfo)
                            {
                                pBar1.Value++;
                                pBar1.Invalidate();
                                if (rbOneOrganizationToOneFile.Checked && !isCreated)
                                {
                                    //wd.Close();
                                    wd = new WordDoc(TemplatePath);
                                    isCreated = false;
                                }

                                wd.InsertAutoTextInEnd("Письмо", true);
                                wd.GetLastFields(9);
                                {
                                    string City = PPInfo.City;
                                    if (PPInfo.City.StartsWith("г. "))
                                        City = "г." + PPInfo.City.Substring(3);
                                    wd.SetFields("Организация", PPInfo.OrganixationName);
                                    wd.SetFields("Подразделение", string.IsNullOrEmpty(PPInfo.Subdivision) ? "" : (PPInfo.Subdivision + "\r\n"));
                                    string Name = ((String.IsNullOrEmpty(PPInfo.Name) ? "" : (PPInfo.Name[0] + ".")) +
                                        (String.IsNullOrEmpty(PPInfo.SecondName) ? "" : (PPInfo.SecondName[0] + ".")) + " " + PPInfo.Surname).Trim();

                                    wd.SetFields("ИОФамилия", Name);
                                    string Address =
                                        (String.IsNullOrEmpty(PPInfo.Street) ? "" : (PPInfo.Street + ", ")) +
                                        (String.IsNullOrEmpty(PPInfo.House) ? "" : (PPInfo.House + ", ")) +
                                        (String.IsNullOrEmpty(PPInfo.Apartment) ? "" : (PPInfo.Apartment + ", ")) +
                                        (String.IsNullOrEmpty(City) ? "" : (City + ", ")) +
                                        (String.IsNullOrEmpty(PPInfo.Code) ? "" : (PPInfo.Code));
                                    wd.SetFields("Адрес", Address);
                                    wd.SetFields("ФИО", (!PPInfo.SexMale.HasValue ? "ый(ая) " : (PPInfo.SexMale.Value ? "ый " : "ая ")) + PPInfo.FullName);
                                    wd.SetFields("КонвертАдрес", Address);

                                    wd.SetFields("КонвертОрганизация", PPInfo.OrganixationName);
                                    wd.SetFields("КонвертПодразделение", string.IsNullOrEmpty(PPInfo.Subdivision) ? "" : (PPInfo.Subdivision + "\r\n"));
                                    wd.SetFields("КонвертИОФамилия", Name);

                                    //удалять ли созданные записи?
                                    context.PartnerPersonOrganizationLetter.Add(new PartnerPersonOrganizationLetter()
                                    {
                                        PartnerPersonId = letter.PartnerPersonId,
                                        OrganizationId = PPInfo.OrgId,
                                        Author = Util.GetUserName(),
                                        Timestamp = DateTime.Now,
                                        IsSend = false,
                                    });
                                    context.SaveChanges();

                                    if (rbOneOrganizationToOneFile.Checked)
                                    {
                                        wd.Save(string.Format("{0}{1} {2}{3}{4}", TempFilesFolder, PPInfo.Surname, i, letter.PartnerPersonId, type));
                                        wd.Close();
                                        isCreated = false;
                                    }
                                    i++;
                                }
                            }
                            if (rbOnePersonToOneFile.Checked)
                            {
                                wd.Save(string.Format("{0}{1} {2}{3}{4}", TempFilesFolder, ppinfo.First().Surname, i, letter.PartnerPersonId, type));
                                wd.Close();
                                isCreated = false;
                            }
                        }
                        //doc.Paragraphs.ForEach(x => x.Font(new System.Drawing.FontFamily("Times New Roman")));
                        if (rbAllPersonsToOneFile.Checked)
                        {
                            wd.Save(string.Format("{0}{1} {2}", TempFilesFolder, nameshort, type));
                            wd.Close();
                            isCreated = false;
                        }

                        Process.Start(TempFilesFolder);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сформировать документ\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            try
            {
                File.Delete(TemplatePath);
            }
            catch { }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            pBar1.Visible = true;
            pBar1.Value = 0;
            GetLetterWordOut();
            pBar1.Visible = false;
        }
    }

    public class LetterList 
    {
        public List<Letter> lst;
        public List<int> Ids;
        public LetterList(List<int> lstId)
        {
            Ids = lstId;
            lst = new List<Letter>();
            foreach (var id in lstId)
                lst.Add(new Letter(id));
        }
    }
    public class Letter
    {
        public int PartnerPersonId;
        public List<int> OrgSorting;

        public Letter(int PPid)
        {
            PartnerPersonId = PPid;
            OrgSorting = new List<int>();
        }
    }
}
