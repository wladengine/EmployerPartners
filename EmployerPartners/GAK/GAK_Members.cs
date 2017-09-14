using OfficeOpenXml;
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
using WordOut;
using EmployerPartners.EDMX;

namespace EmployerPartners
{
    public partial class GAK_Members : Form
    {
        private int? GAKId
        {
            get { return ComboServ.GetComboIdInt(cbGAK); }
            set { ComboServ.SetComboId(cbGAK, value); }
        }
        private int? FacultyId
        {
            get { return ComboServ.GetComboIdInt(cbFaculty); }
            set { ComboServ.SetComboId(cbFaculty, value); }
        }
        private int? StudyLevelId
        {
            get { return ComboServ.GetComboIdInt(cbStudyLevel); }
            set { ComboServ.SetComboId(cbStudyLevel, value); }
        }
        private int? LicenseProgramId
        {
            get { return ComboServ.GetComboIdInt(cbLicenseProgram); }
            set { ComboServ.SetComboId(cbLicenseProgram, value); }
        }
        private int? ObrazProgramId
        {
            get { return ComboServ.GetComboIdInt(cbObrazProgram); }
            set { ComboServ.SetComboId(cbObrazProgram, value); }
        }
        private int? ExamVKRId
        {
            get { return ComboServ.GetComboIdInt(cbExamVKR); }
            set { ComboServ.SetComboId(cbExamVKR, value); }
        }
        private string GAKNumber
        {
            get { return tbGAKNumber.Text.Trim(); }
            set { tbGAKNumber.Text = value; }
        }
        private bool ShowGAKNumberOld
        {
            get { return chbShowGAKNumberOld.Checked; }
            set { chbShowGAKNumberOld.Checked = value; }
        }
        private string OrderNumber
        {
            get { return tbOrderNumber.Text.Trim(); }
            set { tbOrderNumber.Text = value; }
        }
        private string OrderDate
        {
            get { return tbOrderDate.Text.Trim(); }
            set { tbOrderDate.Text = value; }
        }

        public GAK_Members()
        {
            InitializeComponent();
            FillGAK();
            FillCombo();
            //FillGAKList();
            this.MdiParent = Util.mainform;
            SetAccessRight();
        }
        private void SetAccessRight()
        {
            if (Util.IsGAKWrite() || Util.IsSuperUser() || Util.IsDBOwner() || Util.IsAdministrator())
            {
                btnCreateGAK.Enabled = true;
                //btnRemakeGAKNumber.Enabled = true;
            }
            if (Util.IsGAKWrite())
            {
                btnRemakeGAKNumber.Enabled = true;
                btnRestoreGAKNumer.Enabled = true;
            }
            if (Util.IsGAKWrite() || Util.IsSuperUser())
            {
               // btnArchive.Visible = true;
                btnOrderChangeDoc.Visible = true;
            }
            if (Util.IsSuperUser())
            {
                btnXLS.Visible = true;
            }
        }
        private void FillGAK()
        {
            ComboServ.FillCombo(cbGAK, HelpClass.GetComboListByQuery(@" SELECT DISTINCT CONVERT(varchar(100), Id) AS Id, GAKYear AS Name 
                FROM GAK ORDER BY Name DESC "), false, false);
        }
        private void FillExamVKR()
        {
            ComboServ.FillCombo(cbExamVKR, HelpClass.GetComboListByQuery(@" SELECT DISTINCT CONVERT(varchar(100), Id) AS Id, Name 
                FROM dbo.GAK_ExamVKR ORDER BY Name"), false, true);
        }
        private void FillCombo()
        {
            FillFacultyList();
            FillStudyLevelList();
            FillLicenseProgramList();
            FillObrazProgramList();
            FillExamVKR();
        }
        private void FillFacultyList()
        {
            ComboServ.FillCombo(cbFaculty, HelpClass.GetComboListByQuery(@" select CONVERT(varchar(100), [Id]) AS Id, Name  
                                    from dbo.Faculty where Id in (select FacultyId from dbo.VKR_ThemesStudentOrder union select FacultyId from dbo.VKR_ThemesAspirantOrder ) order by dbo.Faculty.Name"), false, true);
        }
        private void FillStudyLevelList()
        {
            ComboServ.FillCombo(cbStudyLevel, HelpClass.GetComboListByQuery(@" 
select distinct CONVERT(varchar(100), StudyLevel.Id) AS Id, StudyLevel.Name  
from dbo.StudyLevel  
join dbo.LicenseProgram on LicenseProgram.StudyLevelId = StudyLevel.Id
where LicenseProgram.Id in 
(select LicenseProgramId from dbo.VKR_ThemesStudentOrder
union select LicenseProgramId from dbo.VKR_ThemesAspirantOrder
)
order by id
"), false, true);
        }
        private void FillLicenseProgramList()
        {
            string faculty = "";
            if (FacultyId.HasValue)
            {
                faculty = " where FacultyId = " + FacultyId.ToString() + " ";
            } 
            string sStudyLevelId = "";
            if (StudyLevelId.HasValue)
            {
                sStudyLevelId = " and StudyLevelId = " + StudyLevelId.ToString() + " ";
            }
            ComboServ.FillCombo(cbLicenseProgram, HelpClass.GetComboListByQuery(String.Format(@" select CONVERT(varchar(100), dbo.LicenseProgram.[Id]) AS Id, 
                        (dbo.LicenseProgram.[Code] + case Len(dbo.LicenseProgram.[Code]) when 6 then '    ' else '  ' end + 
                        dbo.StudyLevel.Name + case Len(dbo.StudyLevel.Name) when 11 then '    ' else '  ' end + dbo.LicenseProgram.Name) as Name  
                                    from dbo.LicenseProgram inner join dbo.StudyLevel on dbo.LicenseProgram.StudyLevelId = dbo.StudyLevel.Id  
                                    where dbo.LicenseProgram.Id in 
                    (select LicenseProgramId from dbo.VKR_ThemesStudentOrder {0}
union all select LicenseProgramId from dbo.VKR_ThemesAspirantOrder {1}
) {2} order by dbo.LicenseProgram.Name", faculty, faculty, sStudyLevelId)), false, true);
        }
        private void FillObrazProgramList()
        {
            string licenseprogram = "";
            if (LicenseProgramId.HasValue)
            {
                licenseprogram = " where LicenseProgramId = " + LicenseProgramId.ToString() + " ";
                if (FacultyId.HasValue)
                {
                    licenseprogram += " and FacultyId = " + FacultyId.ToString() + " ";
                }
            }
            else
            {
                if (FacultyId.HasValue)
                {
                    licenseprogram = " where FacultyId = " + FacultyId.ToString() + " ";
                }
            }
            ComboServ.FillCombo(cbObrazProgram, HelpClass.GetComboListByQuery(String.Format(@" select CONVERT(varchar(100), [Id]) AS Id, ([Number] + '  ' + Name) as Name  
                                    from dbo.ObrazProgram where Id in (select ObrazProgramId from dbo.VKR_ThemesStudentOrder {0} 
union all select ObrazProgramId from dbo.VKR_ThemesAspirantOrder {1}) order by dbo.ObrazProgram.Name", licenseprogram, licenseprogram)), false, true);
        }
        private void cbGAK_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillFacultyList();
        }
        private void cbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLicenseProgramList();
        }
        private void cbLicenseProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillObrazProgramList();
        }
         private void cbObrazProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            GAKNumber = "";
            FillGAKList();
        }     
         private void cbExamVKR_SelectedIndexChanged(object sender, EventArgs e)
        {
            GAKNumber = "";
            FillGAKList();
        }

        private void FillGAKList()
        {
            FillGAKList(null);
        }
        private void FillGAKList(int? id)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_Number
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               join fac in context.Faculty on op.FacultyId equals fac.Id
                               join lp in context.LicenseProgram on op.LicenseProgramId equals lp.Id
                               join examvkr in context.GAK_ExamVKR on x.ExamVKRId equals examvkr.Id
                               join person in context.PartnerPerson on x.PartnerPersonId equals person.Id into _person
                               from person in _person.DefaultIfEmpty()
                               where (x.GAKId == GAKId) && (ExamVKRId.HasValue ? x.ExamVKRId == ExamVKRId : true) && 
                                     (FacultyId.HasValue ? fac.Id == FacultyId : true) &&
                                     (LicenseProgramId.HasValue ? lp.Id == LicenseProgramId : true) &&
                                     (ObrazProgramId.HasValue ? x.ObrazProgramId == ObrazProgramId : true)
                               orderby fac.Name, lp.Name, op.Name, x.ExamVKRId, x.GAKNumber
                               select new
                               {
                                   Образовательная_программа = op.Number + " " + op.Name,
                                   ГОС_ВКР = examvkr.Acronym,
                                   Номер_комиссии = x.GAKNumber,
                                   Номер_комиссии_исходный = x.GAKNumberOld,
                                   Председатель = person.Name,
                                   Ученая_степень = person.Degree.Name,
                                   Ученое_звание = person.Rank.Name,
                                   Координатор = x.Coordinator,
                                   Дата_время_заседания = x.DateTimeMeeting,
                                   Адрес = x.Address,
                                   Аудитория = x.Auditorium,
                                   Примечание = x.Comment,
                                   Номер_приказа = x.OrderNumber,
                                   Дата_приказа = x.OrderDate,
                                   Дополнение_к_приказу = x.OrderDop,
                                   x.Id,
                                   x.GAKId,
                                   x.ObrazProgramId,
                                   x.PartnerPersonId
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSource1.DataSource = dt;
                    dgv.DataSource = bindingSource1;

                    List<string> Cols = new List<string>() { "Id", "GAKId", "ObrazProgramId", "PartnerPersonId", "Номер_комиссии_исходный" };

                    foreach (string s in Cols)
                        if (dgv.Columns.Contains(s))
                        {
                            dgv.Columns[s].Visible = false;
                        }
                    if (ShowGAKNumberOld)
                    {
                        dgv.Columns["Номер_комиссии_исходный"].Visible = true;
                    }
                    else
                    {
                        dgv.Columns["Номер_комиссии_исходный"].Visible = false;
                    }
                    foreach (DataGridViewColumn col in dgv.Columns)
                        col.HeaderText = col.Name.Replace("_", " ");
                    try
                    {
                        dgv.Columns["Образовательная_программа"].Frozen = true;
                        dgv.Columns["ГОС_ВКР"].Frozen = true;
                        dgv.Columns["Номер_комиссии"].Frozen = true;
                        dgv.Columns["Образовательная_программа"].Width = 200;
                        dgv.Columns["Председатель"].Width = 200;
                        dgv.Columns["Адрес"].Width = 200;
                        dgv.Columns["Примечание"].Width = 200;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Инфо"); 
            }
        }

        private void GAK_Members_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Parent.Width > this.Width + 50 + this.Left)
                {
                    this.Width = this.Parent.Width - 50 - this.Left;
                }
                if (this.Parent.Height > this.Height + 50 + this.Top)
                {
                    this.Height = this.Parent.Height - 50 - this.Top;
                }
            }
            catch (Exception)
            {
            }
        }
        
        private void btnCreateGAK_Click(object sender, EventArgs e)
        {
            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GAKNumber = "";
                cbObrazProgram.DroppedDown = true;
                return;
            }
            if (!ExamVKRId.HasValue)
            {
                MessageBox.Show("Не выбран тип (назначение) комиссии", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GAKNumber = "";
                cbExamVKR.DroppedDown = true;
                return;
            }
            if (String.IsNullOrEmpty(GAKNumber))
            {
                MessageBox.Show("Не указан номер комисии", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //проверка наличия комиссии с введенным номером
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.GAK_Number
                           //join gak in context.GAK on x.GAKId equals gak.Id
                           where (x.GAKId == GAKId) && (x.GAKNumber == GAKNumber) //&& (x.ObrazProgramId == ObrazProgramId) 
                           select new
                           {
                               x.Id,
                           }).ToList().Count();
                if (lst > 0)
                {
                    MessageBox.Show("Комиссия с номером " + GAKNumber + " для выбранного года " + cbGAK.Text + "\r\n" + "уже существует", "Инфо",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            //собственно добавление
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    GAK_Number gak = new GAK_Number();

                    gak.GAKId = (int)GAKId;
                    gak.ObrazProgramId = (int)ObrazProgramId;
                    gak.GAKNumber = GAKNumber;
                    gak.ExamVKRId = (int)ExamVKRId;

                    context.GAK_Number.Add(gak);
                    context.SaveChanges();

                    GAKNumber = "";
                    FillGAKList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btnMakeGAKNumber_Click(object sender, EventArgs e)
        {
            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GAKNumber = "";
                cbObrazProgram.DroppedDown = true;
                return;
            }
            if (!ExamVKRId.HasValue)
            {
                MessageBox.Show("Не выбран тип (назначение) комиссии", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GAKNumber = "";
                cbExamVKR.DroppedDown = true;
                return;
            }
            //генерация номера комиссии
            try
            {
                string gaknumber = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var op = context.ObrazProgram.Where(x => x.Id == ObrazProgramId).First();
                    gaknumber = op.Number + "-";

                    var lst = (from x in context.GAK_Number
                               where (x.GAKId == GAKId) && (x.ObrazProgramId == ObrazProgramId) && (x.ExamVKRId == ExamVKRId)
                               select new
                               {
                                   x.Id,
                               }).ToList().Count();
                    lst += 1;
                    switch (ExamVKRId)
                    {
                        case 1:
                            lst += 50;  //Гос. экз
                            break;
                        case 2:
                            break;
                        case 3:
                            lst += 100; //итоговая
                            break;
                        default:
                            break;
                    }

                    if (lst < 10)
                    {
                        gaknumber += "0" + lst.ToString();
                    }
                    else
                    {
                        gaknumber += lst.ToString();
                    }
                }
                GAKNumber = gaknumber;
            }
            catch (Exception)
            {
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                int opid = int.Parse(dgv.CurrentRow.Cells["ObrazProgramid"].Value.ToString());
                int gakid = int.Parse(dgv.CurrentRow.Cells["GAKId"].Value.ToString());
                //if (Utilities.VKRThemesStudentCardIsOpened(id))
                //    return;
                new GAK_MembersCard(id, opid, gakid, new UpdateIntHandler(FillGAKList)).Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSHowAll_Click(object sender, EventArgs e)
        {
            FacultyId = null;
            cbFaculty.Text = "все";
            if (ExamVKRId.HasValue)
            {
                ExamVKRId = null;
                cbExamVKR.Text = "все";
            }
        }
        private bool CheckOrderFreeze()
        {
            //Проверка что уже зафиксировано
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_Number
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               where (x.GAKId == GAKId) && (x.ObrazProgramId == ObrazProgramId) &&
                               (x.OrderNumber != null) && (x.OrderNumber != "") //&& 
                               //((x.OrderDop == null) || (x.OrderDop == false))
                               orderby x.OrderDate
                               select new
                               {
                                   x.Id,
                                   //x.Freeze,
                                   x.OrderNumber,
                                   x.OrderDate,
                                   ObPr = ((!String.IsNullOrEmpty(op.Number)) ? op.Number : "") + "  " + ((!String.IsNullOrEmpty(op.Name)) ? op.Name : "")
                               }).ToList();
                    if (lst.Count != 0)
                    {
                        string op = lst.First().ObPr;
                        string ordernumber = lst.First().OrderNumber;
                        string orderdate = lst.First().OrderDate.Value.Date.ToString("dd.MM.yyyy");
                        MessageBox.Show("Приказ по выбранной образовательной программе\r\n" + op + "\r\n" + "уже зафиксирован\r\n" +
                            "№ приказа: " + ordernumber + "\r\n" + "Дата приказа: " + orderdate, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    return false;
                }
            }
            catch
            { }
            return false;
        }

        private void btnRemakeGAKNumber_Click(object sender, EventArgs e)
        {
            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GAKNumber = "";
                cbObrazProgram.DroppedDown = true;
                return;
            }
            if (CheckOrderFreeze()) //приказ зафиксирован
                return;
            if (!ExamVKRId.HasValue)
            {
                MessageBox.Show("Не выбран тип (назначение) комиссии", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GAKNumber = "";
                cbExamVKR.DroppedDown = true;
                return;
            }
            //ExamVKRId = null;
            //cbExamVKR.Text = "все";

            //проверка наличия данных
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_Number
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               where (x.GAKId == GAKId) && (ExamVKRId.HasValue ? x.ExamVKRId == ExamVKRId : true) &&
                                     (ObrazProgramId.HasValue ? x.ObrazProgramId == ObrazProgramId : true)
                               orderby ExamVKRId, x.DateTimeMeeting //x.GAKNumber
                               select new
                               {
                                   x.Id,
                                   x.ExamVKRId,
                                   x.DateTimeMeeting,
                                   //ObPr = ((!String.IsNullOrEmpty(op.Number)) ? op.Number : "") + "  " + ((!String.IsNullOrEmpty(op.Name)) ? op.Name : "")
                               }).ToList();
                    if (lst.Count() == 0)
                    {
                        //string op = lst.First().ObPr;
                        MessageBox.Show("Нет данных для выбранной образовательной программы\r\n", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    var lst2 = (from x in context.GAK_Number
                                where (x.GAKId == GAKId) && (ExamVKRId.HasValue ? x.ExamVKRId == ExamVKRId : true) &&
                                     (ObrazProgramId.HasValue ? x.ObrazProgramId == ObrazProgramId : true) && !x.DateTimeMeeting.HasValue
                                select new
                               {
                                   x.Id
                               }).ToList();
                    if (lst2.Count() != 0)
                    {
                        MessageBox.Show("Не для всех комиссий установлены дата и время заседания.\r\n" + 
                            "В такой ситуации перенумерация не производится.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    //Запрос на подтверждение
                    if (MessageBox.Show("Произвести перенумерацию комиссий\r\nдля выбранной образовательной програмы?", "Запрос на подтверждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                        return;

                    ShowGAKNumberOld = true;

                    //Собственно перенумеровывание
                    dgvWork.DataSource = lst;
                    
                    int id;
                    int i = 50;  //Гос. экз
                    int j = 0;  //Защита ВКР
                    int examvkrid;
                    string gaknum = "";
                    var obrazprog = context.ObrazProgram.Where(x => x.Id == ObrazProgramId).First();
                    gaknum = obrazprog.Number + "-";
                    string gaknumber = "";

                    foreach (DataGridViewRow rw in dgvWork.Rows)
                    {
                        gaknumber = gaknum;
                        id = int.Parse(rw.Cells["Id"].Value.ToString());
                        examvkrid = int.Parse(rw.Cells["ExamVKRId"].Value.ToString());
                        switch (examvkrid)
                        {
                            case 1:         //Гос. экз
                                i += 1;
                                if (i < 10)
                                {
                                    gaknumber += "0" + i.ToString();
                                }
                                else
                                {
                                    gaknumber += i.ToString();
                                }
                                break;
                            case 2:       //Защита ВКР
                                j += 1;
                                if (j < 10)
                                {
                                    gaknumber += "0" + j.ToString();
                                }
                                else
                                {
                                    gaknumber += j.ToString();
                                }
                                break;
                            default:
                                break;
                        }
                        
                        var gak = context.GAK_Number.Where(x => x.Id == id).First();
                        if (String.IsNullOrEmpty(gak.GAKNumberOld))
                        {
                            gak.GAKNumberOld = gak.GAKNumber;   //сохранение исходных номеров
                        }
                        gak.GAKNumber = gaknumber;
                        
                        context.SaveChanges();
                    }
                    FillGAKList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chbShowGAKNumberOld_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowGAKNumberOld)
            {
                dgv.Columns["Номер_комиссии_исходный"].Visible = true;
            }
            else
            {
                dgv.Columns["Номер_комиссии_исходный"].Visible = false;
            }
        }

        private void btnRestoreGAKNumer_Click(object sender, EventArgs e)
        {
            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GAKNumber = "";
                cbObrazProgram.DroppedDown = true;
                return;
            }
            if (CheckOrderFreeze()) //приказ зафиксирован
                return;
            if (!ExamVKRId.HasValue)
            {
                MessageBox.Show("Не выбран тип (назначение) комиссии", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GAKNumber = "";
                cbExamVKR.DroppedDown = true;
                return;
            }
            //ExamVKRId = null;
            //cbExamVKR.Text = "все";

            //проверка наличия данных
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_Number
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               where (x.GAKId == GAKId) && (ExamVKRId.HasValue ? x.ExamVKRId == ExamVKRId : true) &&
                                     (ObrazProgramId.HasValue ? x.ObrazProgramId == ObrazProgramId : true)
                               orderby ExamVKRId, x.DateTimeMeeting //x.GAKNumber
                               select new
                               {
                                   x.Id,
                                   x.ExamVKRId,
                                   x.DateTimeMeeting,
                                   //ObPr = ((!String.IsNullOrEmpty(op.Number)) ? op.Number : "") + "  " + ((!String.IsNullOrEmpty(op.Name)) ? op.Name : "")
                               }).ToList();
                    if (lst.Count() == 0)
                    {
                        //string op = lst.First().ObPr;
                        MessageBox.Show("Нет данных для выбранной образовательной программы\r\n", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    var lst2 = (from x in context.GAK_Number
                                where (x.GAKId == GAKId) && (ExamVKRId.HasValue ? x.ExamVKRId == ExamVKRId : true) &&
                                     (ObrazProgramId.HasValue ? x.ObrazProgramId == ObrazProgramId : true) && (!String.IsNullOrEmpty(x.GAKNumberOld))
                                select new
                               {
                                   x.Id
                               }).ToList();
                    if (lst2.Count() == 0)
                    {
                        MessageBox.Show("Перенумерация еще не производилась.\r\n" +
                            "Нет данных для восстановления.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowGAKNumberOld = true;
                        return;
                    }
                    //Запрос на подтверждение
                    if (MessageBox.Show("Произвести восстановление исходных номеров комиссий\r\nдля выбранной образовательной програмы?", "Запрос на подтверждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                        return;

                    //восстановление номеров
                    ShowGAKNumberOld = true;
                    dgvWork.DataSource = lst;
                    int id;
                    foreach (DataGridViewRow rw in dgvWork.Rows)
                    {
                        id = int.Parse(rw.Cells["Id"].Value.ToString());
                        var gak = context.GAK_Number.Where(x => x.Id == id).First();
                        if (!String.IsNullOrEmpty(gak.GAKNumberOld))
                        {
                            gak.GAKNumber = gak.GAKNumberOld; 
                        }
                        context.SaveChanges();
                    }
                    FillGAKList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnXLS_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Экспорт в Excel составов ГЭК 2017г\r\n" + "Это может занять некоторое время..\r\n" +
                        "Продолжить ?", "Запрос на подтверждение",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;

            ToExcel();
        }

        private void ToExcel()
        {
            try
            {
                string filenameDate = "Выгрузка составов ГЭК";
                string filename = Util.TempFilesFolder + filenameDate + ".xlsx";
                string[] fileList = Directory.GetFiles(Util.TempFilesFolder, "Выгрузка составов ГЭК*" + ".xlsx");

                try
                {
                    File.Delete(filename);
                    foreach (string f in fileList)
                    {
                        File.Delete(f);
                    }
                }
                catch (Exception)
                {
                }

                int fileindex = 1;
                while (File.Exists(filename))
                {
                    filename = Util.TempFilesFolder + filenameDate + " (" + fileindex + ")" + ".xlsx";
                    fileindex++;
                }
                System.IO.FileInfo newFile = new System.IO.FileInfo(filename);
                if (newFile.Exists)
                {
                    newFile.Delete();  // ensures we create a new workbook
                    newFile = new System.IO.FileInfo(filename);
                }

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.ext_GAK_2017
                               orderby x.ExamVKR, x.GAKNumber, x.External, x.Chairman descending, x.Name
                               select new
                               {
                                   Гос_зкзамен_защита_ВКР = x.ExamVKR,
                                   Номер_комиссии = x.GAKNumber,
                                   Дата_проведения =x.DateTimeMeeting,
                                   Номер_ОП = x.ObrazProgramNumber,
                                   Образовательная_программа = x.ObrazProgram,
                                   УНП = x.UNP,
                                   Внешний_внутренний =x.External,
                                   Председатель = x.Chairman,
                                   ФИО = x.Name,
                                   ФИО_Инициалы = x.NameInitials,
                                   ФИО_англ = x.NameEng,
                                   ФИО_Инициалы_англ = x.NameInitialsEng,
                                   Ученая_степень = x.Degree,
                                   Ученая_стнепень_англ = x.DegreeEng,
                                   Ученое_звание = x.Rank,
                                   Ученое_звание_англ = x.RankEng,
                                   Почетное_звание = x.RankHonorary,
                                   Почетное_звание_англ = x.RankHonoraryEng,
                                   Государственное_звание = x.RankState,
                                   Государственное_звание_англ = x.RankStateEng,
                                   Страна = x.Country,
                                   Должность_организация = x.OrgPosition,
                                   Должность_организация_англ = x.OrgPositionEng,
                                   Аккаунт = x.Account
                               }).ToList();

                    dgvWork.DataSource = lst;

                    foreach (DataGridViewColumn col in dgvWork.Columns)
                        col.HeaderText = col.Name.Replace("_", " ");
                }
                

                using (ExcelPackage doc = new ExcelPackage(newFile))
                {
                    ExcelWorksheet ws = doc.Workbook.Worksheets.Add("Составы ГЭК");
                    Color lightGray = Color.FromName("LightGray");
                    Color darkGray = Color.FromName("DarkGray");

                    int colind = 0;

                    foreach (DataGridViewColumn cl in dgvWork.Columns)
                    {
                        ws.Cells[1, ++colind].Value = cl.HeaderText.ToString();
                        ws.Cells[1, colind].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                        ws.Cells[1, colind].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        ws.Cells[1, colind].Style.Fill.BackgroundColor.SetColor(lightGray);
                    }

                    for (int rwInd = 0; rwInd < dgvWork.Rows.Count; rwInd++)
                    {
                        DataGridViewRow rw = dgvWork.Rows[rwInd];
                        int colInd = 0;
                        foreach (DataGridViewCell cell in rw.Cells)
                        {
                            if (cell.Value == null)
                            {
                                ws.Cells[rwInd + 2, colInd + 1].Value = "";
                            }
                            else
                            {
                                ws.Cells[rwInd + 2, colInd + 1].Value = cell.Value; //.ToString(); 
                            }
                            ws.Cells[rwInd + 2, colInd + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                            colInd++;
                        }
                    }

                    //форматирование
                    //int clmnInd = 0;
                    //foreach (DataGridViewColumn clmn in dgv.Columns)
                    //{
                    //    clmnInd++;
                    //    if (clmn.Name == "ФИО")
                    //    {
                    //        ws.Column(clmnInd).AutoFit();
                    //    }
                    //    //if (clmn.Name == "ФИО")
                    //    //    //ws.Column(++clmnInd).Width = 100;
                    //    //    ws.Column(++clmnInd).AutoFit();
                    //    //else if (clmn.Name == "Id")
                    //    //    ws.Column(++clmnInd).Width = 0;
                    //    //else
                    //    //    ws.Column(++clmnInd).AutoFit();
                    //}

                    doc.Save();
                }
                System.Diagnostics.Process.Start(filename);
            }
            catch
            {
            }            
        }

        private void btnOrderDoc_Click(object sender, EventArgs e)
        {
            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GAKNumber = "";
                cbObrazProgram.DroppedDown = true;
                return;
            }

            OrderToDoc(ObrazProgramId, null);
        }
        private void OrderToDoc(int? opid, int? itog)
        {
            if (!opid.HasValue)
                return;

            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = "";
            templatename = "Приказ_ составы_ГЭК";
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
            string TempFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Приказы по составам ГЭК\"; //@"\EmployerPartners_TempFiles\";
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
                MessageBox.Show("Не удалось открыть файл...", "Сообщение");
                return;
            }

            WordDoc wd = new WordDoc(string.Format(filePath), true);

            //заполнение шаблона
            try
            {
                string ObrazProgram = "";
                string ObrazProgramHeader = "";
                string LicenseProgram = "";
                //string UNKUNP = "";
                string StudyLevel = "";
                string Stlevel = "";
                string OPNumber = "";
                string OPName = "";
                string LPCode = "";
                string LPName = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.ObrazProgram
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               where x.Id == opid
                               select x).First();

                    Stlevel = lst.LicenseProgram.StudyLevel.Name;
                    OPNumber = lst.Number;
                    OPName = lst.Name;
                    LPCode = lst.LicenseProgram.Code;
                    LPName = lst.LicenseProgram.Name;
                }
                switch (Stlevel)
                {
                    case "бакалавриат":
                        //ObrazProgram = "бакалавриата CB." + OPNumber + ".*";
                        //ObrazProgramHeader = "бакалавриата (шифр CB." + OPNumber + ".*)";
                        ObrazProgram = "CB." + OPNumber + ".*";
                        ObrazProgramHeader = "(шифр CB." + OPNumber + ".*)";
                        LicenseProgram = "по направлению подготовки ";
                        StudyLevel = "бакалавриат";
                        break;
                    case "магистратура":
                        //ObrazProgram = "магистратуры BM." + OPNumber + ".*";
                        //ObrazProgramHeader = "магистратуры (шифр BM." + OPNumber + ".*)";
                        ObrazProgram = "BM." + OPNumber + ".*";
                        ObrazProgramHeader = "(шифр BM." + OPNumber + ".*)";
                        LicenseProgram = "по направлению подготовки ";
                        StudyLevel = "магистратура";
                        break;
                    case "специалитет":
                        //ObrazProgram = "специалитета CM." + OPNumber + ".*";
                        //ObrazProgramHeader = "специалитета (шифр CM." + OPNumber + ".*)";
                        ObrazProgram = "CM." + OPNumber + ".*";
                        ObrazProgramHeader = "(шифр CM." + OPNumber + ".*)";
                        LicenseProgram = "по специальности  ";
                        StudyLevel = "специалитет";
                        break;
                    default:
                        break;
                }
                ObrazProgram += " " + "«" + OPName + "»";
                LicenseProgram += LPCode + " " + "«" + LPName + "»";

                wd.SetFields("ObrazProgramHeader", ObrazProgramHeader);
                wd.SetFields("ObrazProgram", ObrazProgram);
                wd.SetFields("LicenseProgram", LicenseProgram);
                wd.SetFields("StudyLevel", StudyLevel);

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_Number
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               //join lp in context.LicenseProgram on op.LicenseProgramId equals lp.Id

                               join person in context.PartnerPerson on x.PartnerPersonId equals person.Id

                               join degree in context.Degree on person.DegreeId equals degree.Id into _degree
                               from degree in _degree.DefaultIfEmpty()
                               join degree2 in context.Degree on person.Degree2Id equals degree2.Id into _degree2
                               from degree2 in _degree2.DefaultIfEmpty()

                               join rank in context.Rank on person.RankId equals rank.Id into _rank
                               from rank in _rank.DefaultIfEmpty()
                               join rank2 in context.Rank on person.Rank2Id equals rank2.Id into _rank2
                               from rank2 in _rank2.DefaultIfEmpty()

                               join rankhon in context.RankHonorary on person.RankHonoraryId equals rankhon.Id into _rankhon
                               from rankhon in _rankhon.DefaultIfEmpty()
                               join rankhon2 in context.RankHonorary on person.RankHonorary2Id equals rankhon2.Id into _rankhon2
                               from rankhon2 in _rankhon2.DefaultIfEmpty()

                               join rankstate in context.RankState on person.RankStateId equals rankstate.Id into _rankstate
                               from rankstate in _rankstate.DefaultIfEmpty()
                               join rankstate2 in context.RankState on person.RankState2Id equals rankstate2.Id into _rankstate2
                               from rankstate2 in _rankstate2.DefaultIfEmpty()

                               join orgpos in context.PersonOrgPosition on x.PartnerPersonId equals orgpos.PartnerPersonId into _orgpos
                               from orgpos in _orgpos.DefaultIfEmpty()

                               where (x.GAKId == GAKId) && (x.ObrazProgramId == opid) && ((itog.HasValue) ? (x.ExamVKRId == 3) : (x.ExamVKRId == 1 || x.ExamVKRId == 2))
                               orderby x.ExamVKRId, x.GAKNumber     //level.Id, (rbtnAlphabetOrder.Checked ? lp.Name : lp.Code), person.Name
                               select new
                               {
                                   x.GAKNumber,
                                   x.ExamVKRId,
                                   FIO = person.Name,
                                   orgpos.OrgPosition,
                                   Degree = (person.DegreeId.HasValue) ? ((person.Degree2Id.HasValue) ? (degree.Name + ", " + degree2.Name) : degree.Name) : ((person.Degree2Id.HasValue) ? (degree2.Name) : ""),
                                   Rank = (person.RankId.HasValue) ? ((person.Rank2Id.HasValue) ? (rank.Name + ", " + rank2.Name) : rank.Name) : ((person.Rank2Id.HasValue) ? (rank2.Name) : ""),
                                   RankHonorary = (person.RankHonoraryId.HasValue) ? ((person.RankHonorary2Id.HasValue) ? (rankhon.Name + ", " + rankhon2.Name) : rankhon.Name) : ((person.RankHonorary2Id.HasValue) ? (rankhon2.Name) : ""),
                                   RankState = (person.RankStateId.HasValue) ? ((person.RankState2Id.HasValue) ? (rankstate.Name + ", " + rankstate2.Name) : rankstate.Name) : ((person.RankState2Id.HasValue) ? (rankstate.Name) : ""),
                                   //Ученая_степень = person.Degree.Name,
                                   //Ученое_звание = person.Rank.Name,
                                   //Почетное_звание = person.RankHonorary.Name,
                                   //Государственное_или_военное_звание = person.RankState.Name,
                                   TitleDop = person.Title,
                                   x.Id,
                                   x.PartnerPersonId,
                                   op.LicenseProgramId
                               }).ToList();

                    int i = 0;
                    int j = 1; //члены комиссии идут начиная со 2-го после председателя
                    int k = 0;
                    string GAKList = "";
                    string Title = "";
                    string TitleNext = "";

                    foreach (var item in lst)
                    {
                        //замена в последней строке списка состава ГЭК символа ";" на "."
                        try
                        {
                            if (GAKList != "")
                            {
                                int ind = 0;
                                ind = GAKList.Length - 3;
                                if (GAKList.Substring(ind, 1) == ";")
                                {
                                    //GAKList = GAKList.Remove(GAKList.Length - 3, 1).Insert(GAKList.Length - 3, ".");
                                    GAKList = GAKList.Remove(ind, 1).Insert(ind, ".");
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                        //

                        i++;
                        switch (item.ExamVKRId)
                        {
                            case 1:
                                GAKList += "1." + i.ToString() + ". " + "Государственная экзаменационная комиссия по приему государственного экзамена " + item.GAKNumber + ":" + "\r\n";
                                break;
                            case 2 :
                                GAKList += "1." + i.ToString() + ". " + "Государственная экзаменационная комиссия по защите выпускных квалификационных работ " + item.GAKNumber + ":" + "\r\n";
                                break;
                            case 3:
                                GAKList += "1." + i.ToString() + ". " + "Государственная экзаменационная комиссия " + item.GAKNumber + ":" + "\r\n";
                                break;
                            default:
                                break;
                        }
                        //председатель
                        Title = "";
                        TitleNext = "";
                        Title = (!String.IsNullOrEmpty(item.Degree)) ? item.Degree : "";
                        TitleNext = (!String.IsNullOrEmpty(item.Rank)) ? item.Rank : "";
                        Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                        TitleNext = item.OrgPosition;
                        Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                        TitleNext = (!String.IsNullOrEmpty(item.RankHonorary)) ? item.RankHonorary : "";
                        Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                        TitleNext = (!String.IsNullOrEmpty(item.RankState)) ? item.RankState : "";
                        Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                        TitleNext = (!String.IsNullOrEmpty(item.TitleDop)) ? item.TitleDop : "";
                        Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;

                        GAKList += "1." + i.ToString() + ".1. " + "Председатель Государственной экзаменационной комиссии: " +
                            item.FIO + ((!string.IsNullOrEmpty(Title)) ? (", " + Title) : "") +
                            ", утвержден приказом первого проректора по учебной, внеучебной и учебно-методической работе от 30.12.2016 №10681/1"; //+ ";" + "\r\n";
                        //Дополнение к приказу
                        //var gakperson = context.GAK_LP_Person.Where(x => (x.GAKId == GAKId) && (x.LicenseProgramId == item.LicenseProgramId) && (x.PartnerPersonId == item.PartnerPersonId) && (x.OrderDop == true)).First(); 
                        try
                        {
                            var gakperson = (from x in context.GAK_LP_Person
                                             where (x.GAKId == GAKId) && (x.LicenseProgramId == item.LicenseProgramId) && (x.PartnerPersonId == item.PartnerPersonId) && (x.OrderDop == true)
                                             select new
                                             {
                                                 x.OrderNumber,
                                                 OrderDate = x.OrderDate,
                                             }).ToList();
                            if (gakperson.Count() != 0)
                            {
                                var order = gakperson.First();
                                GAKList += " (с изменениями и дополнениями от " + ((order.OrderDate.HasValue) ? order.OrderDate.Value.Date.ToString("dd.MM.yyyy") : "") + " №" + order.OrderNumber + ")";
                            }
                        }
                        catch (Exception)
                        {
                        }
                        GAKList += ";" + "\r\n";

                        //члены комиссии
                        try
                        {
                            //партнеры
                            var memberPP = (from x in context.GAK_MemberPP
                                            join person in context.PartnerPerson on x.PartnerPersonId equals person.Id

                                            join degree in context.Degree on person.DegreeId equals degree.Id into _degree
                                            from degree in _degree.DefaultIfEmpty()
                                            join degree2 in context.Degree on person.Degree2Id equals degree2.Id into _degree2
                                            from degree2 in _degree2.DefaultIfEmpty()

                                            join rank in context.Rank on person.RankId equals rank.Id into _rank
                                            from rank in _rank.DefaultIfEmpty()
                                            join rank2 in context.Rank on person.Rank2Id equals rank2.Id into _rank2
                                            from rank2 in _rank2.DefaultIfEmpty()

                                            join rankhon in context.RankHonorary on person.RankHonoraryId equals rankhon.Id into _rankhon
                                            from rankhon in _rankhon.DefaultIfEmpty()
                                            join rankhon2 in context.RankHonorary on person.RankHonorary2Id equals rankhon2.Id into _rankhon2
                                            from rankhon2 in _rankhon2.DefaultIfEmpty()

                                            join rankstate in context.RankState on person.RankStateId equals rankstate.Id into _rankstate
                                            from rankstate in _rankstate.DefaultIfEmpty()
                                            join rankstate2 in context.RankState on person.RankState2Id equals rankstate2.Id into _rankstate2
                                            from rankstate2 in _rankstate2.DefaultIfEmpty()

                                            join orgpos in context.PersonOrgPosition on x.PartnerPersonId equals orgpos.PartnerPersonId into _orgpos
                                            from orgpos in _orgpos.DefaultIfEmpty()

                                            where x.GAK_NumberId == item.Id
                                            orderby person.Name
                                            select new
                                            {
                                                FIO = person.Name,
                                                orgpos.OrgPosition,
                                                Degree = (person.DegreeId.HasValue) ? ((person.Degree2Id.HasValue) ? (degree.Name + ", " + degree2.Name) : degree.Name) : ((person.Degree2Id.HasValue) ? (degree2.Name) : ""),
                                                Rank = (person.RankId.HasValue) ? ((person.Rank2Id.HasValue) ? (rank.Name + ", " + rank2.Name) : rank.Name) : ((person.Rank2Id.HasValue) ? (rank2.Name) : ""),
                                                RankHonorary = (person.RankHonoraryId.HasValue) ? ((person.RankHonorary2Id.HasValue) ? (rankhon.Name + ", " + rankhon2.Name) : rankhon.Name) : ((person.RankHonorary2Id.HasValue) ? (rankhon2.Name) : ""),
                                                RankState = (person.RankStateId.HasValue) ? ((person.RankState2Id.HasValue) ? (rankstate.Name + ", " + rankstate2.Name) : rankstate.Name) : ((person.RankState2Id.HasValue) ? (rankstate.Name) : ""),
                                                TitleDop = person.Title,
                                            }).ToList();

                            foreach (var itemPP in memberPP)
                            {
                                j++;
                                //регалии члена комиссии
                                Title = "";
                                TitleNext = "";
                                Title = (!String.IsNullOrEmpty(itemPP.Degree)) ? itemPP.Degree : "";
                                TitleNext = (!String.IsNullOrEmpty(itemPP.Rank)) ? itemPP.Rank : "";
                                Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                                TitleNext = itemPP.OrgPosition;
                                Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                                TitleNext = (!String.IsNullOrEmpty(itemPP.RankHonorary)) ? itemPP.RankHonorary : "";
                                Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                                TitleNext = (!String.IsNullOrEmpty(itemPP.RankState)) ? itemPP.RankState : "";
                                Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                                TitleNext = (!String.IsNullOrEmpty(itemPP.TitleDop)) ? itemPP.TitleDop : "";
                                Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;

                                GAKList += "1." + i.ToString() + "." + j.ToString() + ". " +
                                itemPP.FIO + ((!string.IsNullOrEmpty(Title)) ? (", " + Title) : "") + ";" + "\r\n";
                            }

                            //НПР
                            var memberNPR = (from x in context.GAK_MemberNPR
                                             join sap in context.SAP_NPR on x.NPR_Tabnum equals sap.Tabnum
                                             where x.GAK_NumberId == item.Id
                                             orderby sap.Lastname, sap.Name, sap.Surname
                                             select new
                                            {
                                                FIO = ((!String.IsNullOrEmpty(sap.Lastname)) ? sap.Lastname : "") + " " + ((!String.IsNullOrEmpty(sap.Name)) ? sap.Name : "") + " " + ((!String.IsNullOrEmpty(sap.Surname)) ? sap.Surname : ""),
                                                Degree = sap.Degree,
                                                Rank = sap.Titl2,
                                                Position = sap.Position,
                                                Chair = sap.FullName,
                                                UNP = sap.Faculty,
                                                sap.Persnum,
                                            }).ToList();

                            foreach (var itemNPR in memberNPR)
                            {
                                j++;
                                //регалии члена комиссии
                                Title = "";
                                TitleNext = "";
                                Title = (!String.IsNullOrEmpty(itemNPR.Degree)) ? itemNPR.Degree : "";
                                TitleNext = (!String.IsNullOrEmpty(itemNPR.Rank)) ? itemNPR.Rank : "";
                                Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                                TitleNext = (!String.IsNullOrEmpty(itemNPR.Position)) ? itemNPR.Position : "";
                                Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                                TitleNext = (!String.IsNullOrEmpty(itemNPR.Chair)) ? itemNPR.Chair : "";
                                Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;

                                GAKList += "1." + i.ToString() + "." + j.ToString() + ". " +
                                itemNPR.FIO + ((!string.IsNullOrEmpty(Title)) ? (", " + Title) : "") + ";" + "\r\n";
                            }
                        }
                        catch (Exception)
                        {
                        }
                        j = 1;
                    } //end foreach item
                    //замена в последеней строке списка состава ГЭК символа ";" на "."
                    try
                    {
                        if (GAKList != "")
                        {
                            int ind = 0;
                            ind = GAKList.Length - 3;
                            if (GAKList.Substring(ind, 1) == ";")
                            {
                                //GAKList = GAKList.Remove(GAKList.Length - 3, 1).Insert(GAKList.Length - 3, ".");
                                GAKList = GAKList.Remove(ind, 1).Insert(ind, ".");
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                    //
                    wd.SetFields("GAKList", GAKList);
                }   //end using context

                //Основания
                int FacId;
                int StLevelId;
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.ObrazProgram
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               //join fac in context.Faculty on x.FacultyId equals fac.Id
                               where x.Id == opid
                               select x).First();

                    FacId = lst.FacultyId;
                    StLevelId = lst.LicenseProgram.StudyLevel.Id;
                }

                //рассылка
                string basis = "";
                string chairman = "";
                string send = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var quan = (from x in context.VKR_OrderSource
                                where (x.FacultyId == FacId) && (x.StudyLevelId == StLevelId)
                                select x).Count();
                    var lst = (from x in context.VKR_OrderSource
                               where (x.FacultyId == FacId) && ((quan > 0) ? x.StudyLevelId == StLevelId : x.StudyLevelId == null)
                               select x).First();
                    basis = lst.Basis;
                    chairman = lst.ChairmanForOrder;
                    send = ((!String.IsNullOrEmpty(lst.ChairmanForSend)) ? lst.ChairmanForSend : "") +
                            ((!String.IsNullOrEmpty(lst.UOPForSend)) ? "\r\n" + lst.UOPForSend : "") +
                            ((!String.IsNullOrEmpty(lst.UUForSend)) ? "\r\n" + lst.UUForSend : "");
                }
                //wd.SetFields("Chairman", chairman);
                //wd.SetFields("Basis", basis);
                wd.SetFields("Send", send);

                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch
                    { }
                }
                string fpath = filePath;
                try
                {
                    filePath = TempFilesFolder + nameshort + " " + OPNumber + type;
                }
                catch (Exception)
                {
                    filePath = fpath;
                }

                wd.Save(filePath);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сформировать документ\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnFreezeOrder_Click(object sender, EventArgs e)
        {
            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GAKNumber = "";
                cbObrazProgram.DroppedDown = true;
                return;
            }

            //Проверка что уже зафиксировано
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_Number
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               where (x.GAKId == GAKId) && (x.ObrazProgramId == ObrazProgramId) &&
                               (x.OrderNumber != null) && (x.OrderNumber != "") && (x.ExamVKRId == 1 || x.ExamVKRId == 2) //&& 
                               //((x.OrderDop == null) || (x.OrderDop == false))
                               orderby x.OrderDate 
                               select new
                               {
                                   x.Id,
                                   //x.Freeze,
                                   x.OrderNumber,
                                   x.OrderDate,
                                   ObPr = ((!String.IsNullOrEmpty(op.Number)) ? op.Number : "") + "  " + ((!String.IsNullOrEmpty(op.Name)) ? op.Name : "")
                               }).ToList();
                    if (lst.Count != 0)
                    {
                        string op = lst.First().ObPr;
                        string ordernumber = lst.First().OrderNumber;
                        string orderdate = lst.First().OrderDate.Value.Date.ToString("dd.MM.yyyy");
                        MessageBox.Show("Приказ по выбранной образовательной программе\r\n" + op + "\r\n" + "уже зафиксирован\r\n" +
                            "№ приказа: " + ordernumber + "\r\n" + "Дата приказа: " + orderdate, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            catch
            { }

            if (String.IsNullOrEmpty(OrderNumber) || String.IsNullOrEmpty(OrderDate))
            {
                MessageBox.Show("Для фиксации (\"заморозки\") данных\r\n" + "необходимо указать № и дату приказа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!CheckFields())
                return;

            if (MessageBox.Show("Фиксация приказа по составам ГЭК в базе данных\r\n" + cbObrazProgram.Text + "\r\nВыполнить?", "Запрос на подтверждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                return;

            FreezeOrderData(ObrazProgramId);
        }
        private bool CheckFields()
        {
            DateTime res;
            if (!String.IsNullOrEmpty(OrderDate))
            {
                if (!DateTime.TryParse(OrderDate, out res))
                {
                    MessageBox.Show("Неправильный формат даты в поле 'Дата приказа' \r\n" + "Образец: 01.12.2017", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            return true;
        }
        private void FreezeOrderData(int? opid)
        {
            if (!opid.HasValue)
                return;
            if (!CheckFields())
                return;
            try
            {
                string Log = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_Number
                               where (x.GAKId == GAKId) && (x.ObrazProgramId == opid) && (x.ExamVKRId == 1 || x.ExamVKRId == 2)
                               orderby x.GAKNumber
                               select new
                               {
                                   x.Id,
                                   //x.Freeze,
                                   //x.OrderNumber,
                                   //x.OrderDate,
                                   //x.StudentNumberInOrder
                               }).ToList();

                    //DataTable dt = new DataTable();
                    //dt = Utilities.ConvertToDataTable(lst);

                    dgvWork.DataSource = lst;

                    int id;
                    int i = 0;
                    foreach (DataGridViewRow rw in dgvWork.Rows)
                    {
                        i++;
                        id = int.Parse(rw.Cells["Id"].Value.ToString());
                        string action = "";
                        var gak = context.GAK_Number.Where(x => x.Id == id).First();
                        if (!String.IsNullOrEmpty(OrderNumber))
                        {
                            gak.Freeze = true;
                            gak.InOrder = true;
                            gak.OrderNumber = OrderNumber;
                            gak.OrderDate = DateTime.Parse(OrderDate);
                            gak.OrderDop = false;
                            action = "Фиксация приказа";
                        }
                        else
                        {
                            gak.Freeze = null;
                            gak.InOrder = null;
                            gak.OrderNumber = null;
                            gak.OrderDate = null;
                            gak.OrderDop = null;
                            action = "Разморозка приказа";
                        }
                        context.SaveChanges();
                        //Логирование
                        if (Utilities.GAKLog(id, action))
                        {
                            ;//MessageBox.Show("Логирование произведено", "Успешное завершение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            ;// MessageBox.Show("Не удалось произвести логирование... ", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    FillGAKList();
                    OrderNumber = "";
                    OrderDate = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось зафиксировать данные...\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUnFreezeOrder_Click(object sender, EventArgs e)
        {
            OrderNumber = "";
            OrderDate = "";

            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbObrazProgram.DroppedDown = true;
                return;
            }

            //проверка наличия доп приказов
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_Number
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               where (x.GAKId == GAKId) && (x.ObrazProgramId == ObrazProgramId) &&
                               (x.OrderNumber != null) && (x.OrderNumber != "") && (x.OrderDop == true)
                               && (x.ExamVKRId == 1 || x.ExamVKRId == 2)
                               select new
                               {
                                   x.Id,
                                   //x.Freeze,
                                   //x.OrderNumber,
                                   //x.OrderDate,
                                   //ObPr = ((!String.IsNullOrEmpty(op.Number)) ? op.Number : "") + "  " + ((!String.IsNullOrEmpty(op.Name)) ? op.Name : "")
                                   //x.StudentNumberInOrder
                               }).ToList();
                    if (lst.Count != 0)
                    {
                        //string op = lst.First().ObPr;
                        //string ordernumber = lst.First().OrderNumber;
                        //string orderdate = lst.First().OrderDate.Value.Date.ToString("dd.MM.yyyy");
                        MessageBox.Show("Не \"разморожены\" дополнения к приказу.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось проверить наличие дополнений к приказу.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("\"Разморозка\" приказа по составам ГЭК в базе данных\r\n" + cbObrazProgram.Text + "\r\nВыполнить?", "Запрос на подтверждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                return;

            FreezeOrderData(ObrazProgramId);
        }
        private bool MakeGAKLog(int opid)
        {
            // добавление в GAK_Number_log
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_Number
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               where (x.GAKId == GAKId) && 
                                     (ObrazProgramId.HasValue ? x.ObrazProgramId == opid : true)
                               select new
                               {
                                   x.Id,
                                  x.GAKId,
                                  x.ObrazProgramId,
                                  x.GAKNumber,
                                  x.GAKNumberOld,
                                  x.ExamVKRId,
                                  x.GAK_LP_PersonId,
                                  x.PartnerPersonId,
                                  x.Coordinator,
                                  x.Address,
                                  x.Auditorium,
                                  x.DateTimeMeeting,
                                  x.Comment,
                                  x.Freeze,
                                  x.ToOrder,
                                  x.InOrder,
                                  x.OrderNumber,
                                  x.OrderDate,
                                  x.OrderDop,
                                  x.AuthorUpdated,
                                  x.DateUpdated,
                                  x.Author,
                                  x.DateCreated
                               }).ToList();
                    if (lst.Count() == 0)
                        return false;
                    dgvWork.DataSource = lst;

                    foreach (var item in lst)
                    {

                    }

                    int id;

                    foreach (DataGridViewRow rw in dgvWork.Rows)
                    {
                        id = int.Parse(rw.Cells["Id"].Value.ToString());
                    }
                    //GAK_Number_log gak = new GAK_Number_log();

                    //gak.Action = "Фиксация приказа";
                    //gak.GAK_NumberId = 
                    //gak.GAKId = (int)GAKId;
                    //gak.ObrazProgramId = (int)ObrazProgramId;
                    //gak.GAKNumber = GAKNumber;
                    //gak.ExamVKRId = (int)ExamVKRId;

                    //context.GAK_Number_log.Add(gak);
                    //context.SaveChanges();

                    //GAKNumber = "";
                    //FillGAKList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return false;
        }

        private void btnOrderItogDoc_Click(object sender, EventArgs e)
        {
            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GAKNumber = "";
                cbObrazProgram.DroppedDown = true;
                return;
            }

            OrderToDoc(ObrazProgramId, 3);
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Архивирование составов ГЭК.\r\n" + "Продолжить ?", "Запрос на подтверждение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                return;

            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_Number
                               where (x.GAKId == GAKId)
                               orderby x.ExamVKRId, x.GAKNumber
                               select new
                               {
                                   x.Id
                               }).ToList();
                    string action = "архивирование";
                    foreach (var item in lst)
                    {
                        int id = int.Parse(item.Id.ToString());
                        if (item.Id == 20 || item.Id == 21)
                        {
                            if (Utilities.GAKArchive(id, action))
                            {
                                MessageBox.Show("Архивирование произведено", "Успешное завершение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Не удалось произвести архивирование... ", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось провести архивацию данных\r\n" + ex.Message, "Инфо");
            }
        }

        private void btnOrderChangeDoc_Click(object sender, EventArgs e)
        {
            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GAKNumber = "";
                cbObrazProgram.DroppedDown = true;
                return;
            }

            OrderNewEditionDoc(ObrazProgramId, null);
        }

        private void OrderNewEditionDoc(int? opid, int? itog)
        {
            if (!opid.HasValue)
                return;

            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = "";
            templatename = "Приказ_составы_ГЭК_ новая редакция";
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
            string TempFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Приказы по составам ГЭК\"; //@"\EmployerPartners_TempFiles\";
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
                MessageBox.Show("Не удалось открыть файл...", "Сообщение");
                return;
            }

            WordDoc wd = new WordDoc(string.Format(filePath), true);

            //заполнение шаблона
            try
            {
                string ObrazProgram = "";
                string ObrazProgramHeader = "";
                string LicenseProgram = "";
                //string UNKUNP = "";
                string StudyLevel = "";
                string Stlevel = "";
                string OPNumber = "";
                string OPName = "";
                string LPCode = "";
                string LPName = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.ObrazProgram
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               where x.Id == opid
                               select x).First();

                    Stlevel = lst.LicenseProgram.StudyLevel.Name;
                    OPNumber = lst.Number;
                    OPName = lst.Name;
                    LPCode = lst.LicenseProgram.Code;
                    LPName = lst.LicenseProgram.Name;
                }
                switch (Stlevel)
                {
                    case "бакалавриат":
                        //ObrazProgram = "бакалавриата CB." + OPNumber + ".*";
                        //ObrazProgramHeader = "бакалавриата (шифр CB." + OPNumber + ".*)";
                        ObrazProgram = "CB." + OPNumber + ".*";
                        ObrazProgramHeader = "(шифр CB." + OPNumber + ".*)";
                        LicenseProgram = "по направлению подготовки ";
                        StudyLevel = "бакалавриат";
                        break;
                    case "магистратура":
                        //ObrazProgram = "магистратуры BM." + OPNumber + ".*";
                        //ObrazProgramHeader = "магистратуры (шифр BM." + OPNumber + ".*)";
                        ObrazProgram = "BM." + OPNumber + ".*";
                        ObrazProgramHeader = "(шифр BM." + OPNumber + ".*)";
                        LicenseProgram = "по направлению подготовки ";
                        StudyLevel = "магистратура";
                        break;
                    case "специалитет":
                        //ObrazProgram = "специалитета CM." + OPNumber + ".*";
                        //ObrazProgramHeader = "специалитета (шифр CM." + OPNumber + ".*)";
                        ObrazProgram = "CM." + OPNumber + ".*";
                        ObrazProgramHeader = "(шифр CM." + OPNumber + ".*)";
                        LicenseProgram = "по специальности  ";
                        StudyLevel = "специалитет";
                        break;
                    default:
                        break;
                }
                ObrazProgram += " " + "«" + OPName + "»";
                LicenseProgram += LPCode + " " + "«" + LPName + "»";

                wd.SetFields("ObrazProgramHeader", ObrazProgramHeader);
                wd.SetFields("ObrazProgramHeader2", ObrazProgramHeader);
                wd.SetFields("ObrazProgram", ObrazProgram);
                wd.SetFields("LicenseProgram", LicenseProgram);
                wd.SetFields("StudyLevel", StudyLevel);
                
                //////
                string Order = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_Number
                               where (x.GAKId == GAKId) && (x.ObrazProgramId == opid) && ((itog.HasValue) ? (x.ExamVKRId == 3) : (x.ExamVKRId == 1 || x.ExamVKRId == 2))
                               && (x.OrderNumber != null) && (x.OrderNumber != "")
                               select x).ToList();
                    if (lst.Count != 0)
                    {
                        var lst2 = (from x in context.GAK_Number
                                   where (x.GAKId == GAKId) && (x.ObrazProgramId == opid) && ((itog.HasValue) ? (x.ExamVKRId == 3) : (x.ExamVKRId == 1 || x.ExamVKRId == 2))
                                   && (x.OrderNumber != null) && (x.OrderNumber != "")
                                   select x).First();
                        Order = "от " + ((lst2.OrderDate.HasValue) ? lst2.OrderDate.Value.ToString("dd.MM.yyyy") : "") + " № " + lst2.OrderNumber;
                    }
                    else
                    {
                        var arch = (from x in context.GAK_Number_archive
                                   where (x.GAKId == GAKId) && (x.ObrazProgramId == opid) && ((itog.HasValue) ? (x.ExamVKRId == 3) : (x.ExamVKRId == 1 || x.ExamVKRId == 2))
                                   && (x.OrderNumber != null) && (x.OrderNumber != "")
                                   select x).ToList();
                        if (arch.Count != 0)
                        {
                            var arch2 = (from x in context.GAK_Number_archive
                                        where (x.GAKId == GAKId) && (x.ObrazProgramId == opid) && ((itog.HasValue) ? (x.ExamVKRId == 3) : (x.ExamVKRId == 1 || x.ExamVKRId == 2))
                                        && (x.OrderNumber != null) && (x.OrderNumber != "")
                                        select x).First();
                            Order = "от " + ((arch2.OrderDate.HasValue) ? arch2.OrderDate.Value.ToString("dd.MM.yyyy") : "") + " № " + arch2.OrderNumber;
                        }
                        else
                        {
                            Order = "от " + " № ";
                        }
                    }
                }

                wd.SetFields("OrderNumber", Order);
                wd.SetFields("OrderNumber2", Order);
                //////

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_Number
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               //join lp in context.LicenseProgram on op.LicenseProgramId equals lp.Id

                               join person in context.PartnerPerson on x.PartnerPersonId equals person.Id

                               join degree in context.Degree on person.DegreeId equals degree.Id into _degree
                               from degree in _degree.DefaultIfEmpty()
                               join degree2 in context.Degree on person.Degree2Id equals degree2.Id into _degree2
                               from degree2 in _degree2.DefaultIfEmpty()

                               join rank in context.Rank on person.RankId equals rank.Id into _rank
                               from rank in _rank.DefaultIfEmpty()
                               join rank2 in context.Rank on person.Rank2Id equals rank2.Id into _rank2
                               from rank2 in _rank2.DefaultIfEmpty()

                               join rankhon in context.RankHonorary on person.RankHonoraryId equals rankhon.Id into _rankhon
                               from rankhon in _rankhon.DefaultIfEmpty()
                               join rankhon2 in context.RankHonorary on person.RankHonorary2Id equals rankhon2.Id into _rankhon2
                               from rankhon2 in _rankhon2.DefaultIfEmpty()

                               join rankstate in context.RankState on person.RankStateId equals rankstate.Id into _rankstate
                               from rankstate in _rankstate.DefaultIfEmpty()
                               join rankstate2 in context.RankState on person.RankState2Id equals rankstate2.Id into _rankstate2
                               from rankstate2 in _rankstate2.DefaultIfEmpty()

                               join orgpos in context.PersonOrgPosition on x.PartnerPersonId equals orgpos.PartnerPersonId into _orgpos
                               from orgpos in _orgpos.DefaultIfEmpty()

                               where (x.GAKId == GAKId) && (x.ObrazProgramId == opid) && ((itog.HasValue) ? (x.ExamVKRId == 3) : (x.ExamVKRId == 1 || x.ExamVKRId == 2))
                               orderby x.ExamVKRId, x.GAKNumber     //level.Id, (rbtnAlphabetOrder.Checked ? lp.Name : lp.Code), person.Name
                               select new
                               {
                                   x.GAKNumber,
                                   x.ExamVKRId,
                                   FIO = person.Name,
                                   orgpos.OrgPosition,
                                   Degree = (person.DegreeId.HasValue) ? ((person.Degree2Id.HasValue) ? (degree.Name + ", " + degree2.Name) : degree.Name) : ((person.Degree2Id.HasValue) ? (degree2.Name) : ""),
                                   Rank = (person.RankId.HasValue) ? ((person.Rank2Id.HasValue) ? (rank.Name + ", " + rank2.Name) : rank.Name) : ((person.Rank2Id.HasValue) ? (rank2.Name) : ""),
                                   RankHonorary = (person.RankHonoraryId.HasValue) ? ((person.RankHonorary2Id.HasValue) ? (rankhon.Name + ", " + rankhon2.Name) : rankhon.Name) : ((person.RankHonorary2Id.HasValue) ? (rankhon2.Name) : ""),
                                   RankState = (person.RankStateId.HasValue) ? ((person.RankState2Id.HasValue) ? (rankstate.Name + ", " + rankstate2.Name) : rankstate.Name) : ((person.RankState2Id.HasValue) ? (rankstate.Name) : ""),
                                   //Ученая_степень = person.Degree.Name,
                                   //Ученое_звание = person.Rank.Name,
                                   //Почетное_звание = person.RankHonorary.Name,
                                   //Государственное_или_военное_звание = person.RankState.Name,
                                   TitleDop = person.Title,
                                   x.Id,
                                   x.PartnerPersonId,
                                   op.LicenseProgramId,
                                   x.OrderNumber,
                                   x.OrderDate
                               }).ToList();

                    int i = 0;
                    int j = 1; //члены комиссии идут начиная со 2-го после председателя
                    int k = 0;
                    string GAKList = "";
                    string Title = "";
                    string TitleNext = "";

                    foreach (var item in lst)
                    {
                        //if (item.)
                        //{
                            
                        //}
                        //замена в последней строке списка состава ГЭК символа ";" на "."
                        try
                        {
                            if (GAKList != "")
                            {
                                int ind = 0;
                                ind = GAKList.Length - 3;
                                if (GAKList.Substring(ind, 1) == ";")
                                {
                                    //GAKList = GAKList.Remove(GAKList.Length - 3, 1).Insert(GAKList.Length - 3, ".");
                                    GAKList = GAKList.Remove(ind, 1).Insert(ind, ".");
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                        //

                        i++;
                        switch (item.ExamVKRId)
                        {
                            case 1:
                                GAKList += "1." + i.ToString() + ". " + "Государственная экзаменационная комиссия по приему государственного экзамена " + item.GAKNumber + ":" + "\r\n";
                                break;
                            case 2:
                                GAKList += "1." + i.ToString() + ". " + "Государственная экзаменационная комиссия по защите выпускных квалификационных работ " + item.GAKNumber + ":" + "\r\n";
                                break;
                            case 3:
                                GAKList += "1." + i.ToString() + ". " + "Государственная экзаменационная комиссия " + item.GAKNumber + ":" + "\r\n";
                                break;
                            default:
                                break;
                        }
                        //председатель
                        Title = "";
                        TitleNext = "";
                        Title = (!String.IsNullOrEmpty(item.Degree)) ? item.Degree : "";
                        TitleNext = (!String.IsNullOrEmpty(item.Rank)) ? item.Rank : "";
                        Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                        TitleNext = item.OrgPosition;
                        Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                        TitleNext = (!String.IsNullOrEmpty(item.RankHonorary)) ? item.RankHonorary : "";
                        Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                        TitleNext = (!String.IsNullOrEmpty(item.RankState)) ? item.RankState : "";
                        Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                        TitleNext = (!String.IsNullOrEmpty(item.TitleDop)) ? item.TitleDop : "";
                        Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;

                        GAKList += "1." + i.ToString() + ".1. " + "Председатель Государственной экзаменационной комиссии: " +
                            item.FIO + ((!string.IsNullOrEmpty(Title)) ? (", " + Title) : "") +
                            ", утвержден приказом первого проректора по учебной, внеучебной и учебно-методической работе от 30.12.2016 №10681/1"; //+ ";" + "\r\n";
                        //Дополнение к приказу
                        //var gakperson = context.GAK_LP_Person.Where(x => (x.GAKId == GAKId) && (x.LicenseProgramId == item.LicenseProgramId) && (x.PartnerPersonId == item.PartnerPersonId) && (x.OrderDop == true)).First(); 
                        try
                        {
                            var gakperson = (from x in context.GAK_LP_Person
                                             where (x.GAKId == GAKId) && (x.LicenseProgramId == item.LicenseProgramId) && (x.PartnerPersonId == item.PartnerPersonId) && (x.OrderDop == true)
                                             select new
                                             {
                                                 x.OrderNumber,
                                                 OrderDate = x.OrderDate,
                                             }).ToList();
                            if (gakperson.Count() != 0)
                            {
                                var order = gakperson.First();
                                GAKList += " (с изменениями и дополнениями от " + ((order.OrderDate.HasValue) ? order.OrderDate.Value.Date.ToString("dd.MM.yyyy") : "") + " №" + order.OrderNumber + ")";
                            }
                        }
                        catch (Exception)
                        {
                        }
                        GAKList += ";" + "\r\n";

                        //члены комиссии
                        try
                        {
                            //партнеры
                            var memberPP = (from x in context.GAK_MemberPP
                                            join person in context.PartnerPerson on x.PartnerPersonId equals person.Id

                                            join degree in context.Degree on person.DegreeId equals degree.Id into _degree
                                            from degree in _degree.DefaultIfEmpty()
                                            join degree2 in context.Degree on person.Degree2Id equals degree2.Id into _degree2
                                            from degree2 in _degree2.DefaultIfEmpty()

                                            join rank in context.Rank on person.RankId equals rank.Id into _rank
                                            from rank in _rank.DefaultIfEmpty()
                                            join rank2 in context.Rank on person.Rank2Id equals rank2.Id into _rank2
                                            from rank2 in _rank2.DefaultIfEmpty()

                                            join rankhon in context.RankHonorary on person.RankHonoraryId equals rankhon.Id into _rankhon
                                            from rankhon in _rankhon.DefaultIfEmpty()
                                            join rankhon2 in context.RankHonorary on person.RankHonorary2Id equals rankhon2.Id into _rankhon2
                                            from rankhon2 in _rankhon2.DefaultIfEmpty()

                                            join rankstate in context.RankState on person.RankStateId equals rankstate.Id into _rankstate
                                            from rankstate in _rankstate.DefaultIfEmpty()
                                            join rankstate2 in context.RankState on person.RankState2Id equals rankstate2.Id into _rankstate2
                                            from rankstate2 in _rankstate2.DefaultIfEmpty()

                                            join orgpos in context.PersonOrgPosition on x.PartnerPersonId equals orgpos.PartnerPersonId into _orgpos
                                            from orgpos in _orgpos.DefaultIfEmpty()

                                            where x.GAK_NumberId == item.Id
                                            orderby person.Name
                                            select new
                                            {
                                                FIO = person.Name,
                                                orgpos.OrgPosition,
                                                Degree = (person.DegreeId.HasValue) ? ((person.Degree2Id.HasValue) ? (degree.Name + ", " + degree2.Name) : degree.Name) : ((person.Degree2Id.HasValue) ? (degree2.Name) : ""),
                                                Rank = (person.RankId.HasValue) ? ((person.Rank2Id.HasValue) ? (rank.Name + ", " + rank2.Name) : rank.Name) : ((person.Rank2Id.HasValue) ? (rank2.Name) : ""),
                                                RankHonorary = (person.RankHonoraryId.HasValue) ? ((person.RankHonorary2Id.HasValue) ? (rankhon.Name + ", " + rankhon2.Name) : rankhon.Name) : ((person.RankHonorary2Id.HasValue) ? (rankhon2.Name) : ""),
                                                RankState = (person.RankStateId.HasValue) ? ((person.RankState2Id.HasValue) ? (rankstate.Name + ", " + rankstate2.Name) : rankstate.Name) : ((person.RankState2Id.HasValue) ? (rankstate.Name) : ""),
                                                TitleDop = person.Title,
                                            }).ToList();

                            foreach (var itemPP in memberPP)
                            {
                                j++;
                                //регалии члена комиссии
                                Title = "";
                                TitleNext = "";
                                Title = (!String.IsNullOrEmpty(itemPP.Degree)) ? itemPP.Degree : "";
                                TitleNext = (!String.IsNullOrEmpty(itemPP.Rank)) ? itemPP.Rank : "";
                                Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                                TitleNext = itemPP.OrgPosition;
                                Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                                TitleNext = (!String.IsNullOrEmpty(itemPP.RankHonorary)) ? itemPP.RankHonorary : "";
                                Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                                TitleNext = (!String.IsNullOrEmpty(itemPP.RankState)) ? itemPP.RankState : "";
                                Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                                TitleNext = (!String.IsNullOrEmpty(itemPP.TitleDop)) ? itemPP.TitleDop : "";
                                Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;

                                GAKList += "1." + i.ToString() + "." + j.ToString() + ". " +
                                itemPP.FIO + ((!string.IsNullOrEmpty(Title)) ? (", " + Title) : "") + ";" + "\r\n";
                            }

                            //НПР
                            var memberNPR = (from x in context.GAK_MemberNPR
                                             join sap in context.SAP_NPR on x.NPR_Tabnum equals sap.Tabnum
                                             where x.GAK_NumberId == item.Id
                                             orderby sap.Lastname, sap.Name, sap.Surname
                                             select new
                                             {
                                                 FIO = ((!String.IsNullOrEmpty(sap.Lastname)) ? sap.Lastname : "") + " " + ((!String.IsNullOrEmpty(sap.Name)) ? sap.Name : "") + " " + ((!String.IsNullOrEmpty(sap.Surname)) ? sap.Surname : ""),
                                                 Degree = sap.Degree,
                                                 Rank = sap.Titl2,
                                                 Position = sap.Position,
                                                 Chair = sap.FullName,
                                                 UNP = sap.Faculty,
                                                 sap.Persnum,
                                             }).ToList();

                            foreach (var itemNPR in memberNPR)
                            {
                                j++;
                                //регалии члена комиссии
                                Title = "";
                                TitleNext = "";
                                Title = (!String.IsNullOrEmpty(itemNPR.Degree)) ? itemNPR.Degree : "";
                                TitleNext = (!String.IsNullOrEmpty(itemNPR.Rank)) ? itemNPR.Rank : "";
                                Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                                TitleNext = (!String.IsNullOrEmpty(itemNPR.Position)) ? itemNPR.Position : "";
                                Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                                TitleNext = (!String.IsNullOrEmpty(itemNPR.Chair)) ? itemNPR.Chair : "";
                                Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;

                                GAKList += "1." + i.ToString() + "." + j.ToString() + ". " +
                                itemNPR.FIO + ((!string.IsNullOrEmpty(Title)) ? (", " + Title) : "") + ";" + "\r\n";
                            }
                        }
                        catch (Exception)
                        {
                        }
                        j = 1;
                    } //end foreach item
                    //замена в последеней строке списка состава ГЭК символа ";" на "."
                    try
                    {
                        if (GAKList != "")
                        {
                            int ind = 0;
                            ind = GAKList.Length - 3;
                            if (GAKList.Substring(ind, 1) == ";")
                            {
                                //GAKList = GAKList.Remove(GAKList.Length - 3, 1).Insert(GAKList.Length - 3, ".");
                                GAKList = GAKList.Remove(ind, 1).Insert(ind, ".");
                                GAKList = GAKList.Insert(ind + 1, "».");
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                    //
                    wd.SetFields("GAKList", GAKList);
                }   //end using context

                //Основания
                int FacId;
                int StLevelId;
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.ObrazProgram
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               //join fac in context.Faculty on x.FacultyId equals fac.Id
                               where x.Id == opid
                               select x).First();

                    FacId = lst.FacultyId;
                    StLevelId = lst.LicenseProgram.StudyLevel.Id;
                }

                //рассылка
                string basis = "";
                string chairman = "";
                string send = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var quan = (from x in context.VKR_OrderSource
                                where (x.FacultyId == FacId) && (x.StudyLevelId == StLevelId)
                                select x).Count();
                    var lst = (from x in context.VKR_OrderSource
                               where (x.FacultyId == FacId) && ((quan > 0) ? x.StudyLevelId == StLevelId : x.StudyLevelId == null)
                               select x).First();
                    basis = lst.Basis;
                    chairman = lst.ChairmanForOrder;
                    send = ((!String.IsNullOrEmpty(lst.ChairmanForSend)) ? lst.ChairmanForSend : "") +
                            ((!String.IsNullOrEmpty(lst.UOPForSend)) ? "\r\n" + lst.UOPForSend : "") +
                            ((!String.IsNullOrEmpty(lst.UUForSend)) ? "\r\n" + lst.UUForSend : "");
                }
                //wd.SetFields("Chairman", chairman);
                //wd.SetFields("Basis", basis);
                wd.SetFields("Send", send);

                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch
                    { }
                }
                string fpath = filePath;
                try
                {
                    filePath = TempFilesFolder + nameshort + " " + OPNumber + type;
                }
                catch (Exception)
                {
                    filePath = fpath;
                }

                wd.Save(filePath);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сформировать документ\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
