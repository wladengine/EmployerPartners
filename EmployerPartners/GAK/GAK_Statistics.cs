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

namespace EmployerPartners
{
    public partial class GAK_Statistics : Form
    {
        public GAK_Statistics()
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            FillStatistics();
        }
        private void FillStatistics()
        {
            FillGAK_OP();
            FillGAK_Member();
            FillGAK_MemberTotal();
            FillGAK_UNP();
            FillGAK_LP();
            FillGAK_UNP2();
        }
        private void FillGAK_OP()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    //var lst = (from x in context.GAK_Number
                    //           join examvkr in context.GAK_ExamVKR on x.ExamVKRId equals examvkr.Id
                    //           join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                    //           join lp in context.LicenseProgram on op.LicenseProgramId equals lp.Id
                    //           join stl in context.StudyLevel on lp.StudyLevelId equals stl.Id
                    //           where (x.GAKId == 2)
                    //           select new
                    //           {
                    //               x.Id,
                    //               ExamVKR = examvkr.Name,
                    //               x.ObrazProgramId,
                    //               //ObrazProgram = op.Name,
                    //               StudyLevel = stl.Name,
                    //           }).ToList();

                    //var gr = (from l in lst
                    //          group l by l.Id into l
                    //          orderby l.First().StudyLevel
                    //          select new
                    //          {
                    //              Уровень = l.First().StudyLevel,
                    //              Образовательных_программ = l.Count()
                    //          }).ToList();

                    var gak = (from x in context.GAK_StatOP_Itog
                               orderby x.StudyLevel
                               select new
                               {
                                   Уровень = x.StudyLevel,
                                   Образовательных_программ = x.OPquan,
                                   Всего_ГЭК = x.GAKquan,
                                   ГЭК_по_гос_экзаменам = x.GAKExamquan,
                                   ГЭК_по_защитам_ВКР = x.GAKVKRquan,
                                   ГЭК_по_присвоению_квалификации = x.GAKItogquan
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(gak);
                    dgvOP.DataSource = dt;
                    foreach (DataGridViewColumn col in dgvOP.Columns)
                    col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");
                }
            }
            catch (Exception)
            {
              
            }
        }
        private void FillGAK_LP()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var gak = (from x in context.GAK_StatLP_ItogExtPercent           //GAK_StatLP_ItogPercent
                               orderby x.StudyLevel, x.LicenseProgram
                               select new
                               {
                                   Уровень = x.StudyLevel,
                                   Код_направления = x.LPCode,
                                   Направление = x.LicenseProgram,
                                   Образовательных_программ = x.OPquan,
                                   Всего_ГЭК = x.GAKquan,
                                   ГЭК_по_гос_экзаменам = x.GAKExamquan,
                                   ГЭК_по_защитам_ВКР = x.GAKVKRquan,
                                   ГЭК_по_присвоению_квалификации = x.GAKItogquan,

                                   Всего_членов_ГЭК_из_работодателей = x.MemberPPPercent,
                                   Всего_членов_ГЭК_из_работников_СПбГУ = x.MemberSPbGUPercent,
                                   
                                   Всего_членов_ГЭК_работодателей = x.MemberPP,
                                   Всего_членов_ГЭК_СПбГУ = x.MemberSPbGU,
                                   Всего_председателей_ГЭК = x.MemberChairman
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(gak);
                    bindingSource1.DataSource = dt;
                    dgvLP.DataSource = bindingSource1;

                    foreach (DataGridViewColumn col in dgvLP.Columns)
                        col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");
                    try
                    {
                        dgvLP.Columns["Направление"].Width = 200;
                        dgvLP.Columns["Всего_членов_ГЭК_из_работодателей"].HeaderText = "Всего членов ГЭК из работодателей в %";
                        dgvLP.Columns["Всего_членов_ГЭК_из_работников_СПбГУ"].HeaderText = "Всего членов ГЭК из работников СПбГУ в %";
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
        private void FillGAK_UNP2()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var gak = (from x in context.GAK_StatLP_UNP_ItogExtPercent           //GAK_StatLP_ItogExtPercent           
                               orderby x.Faculty
                               select new
                               {
                                   УНП = x.Faculty,
                                   Образовательных_программ = x.OPquan,
                                   Всего_ГЭК = x.GAKquan,
                                   ГЭК_по_гос_экзаменам = x.GAKExamquan,
                                   ГЭК_по_защитам_ВКР = x.GAKVKRquan,
                                   ГЭК_по_присвоению_квалификации = x.GAKItogquan,

                                   Всего_членов_ГЭК_из_работодателей = x.MemberPPPercent,
                                   Всего_членов_ГЭК_из_работников_СПбГУ = x.MemberSPbGUPercent,

                                   Всего_членов_ГЭК_работодателей = x.MemberPP,
                                   Всего_членов_ГЭК_СПбГУ = x.MemberSPbGU,
                                   Всего_председателей_ГЭК = x.MemberChairman
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(gak);
                    bindingSource2.DataSource = dt;
                    dgvUNP2.DataSource = bindingSource2;

                    foreach (DataGridViewColumn col in dgvUNP2.Columns)
                        col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");
                    try
                    {
                        dgvUNP2.Columns["УНП"].Width = 200;
                        dgvUNP2.Columns["Всего_членов_ГЭК_из_работодателей"].HeaderText = "Всего членов ГЭК из работодателей в %";
                        dgvUNP2.Columns["Всего_членов_ГЭК_из_работников_СПбГУ"].HeaderText = "Всего членов ГЭК из работников СПбГУ в %";
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
        private void FillGAK_UNP()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var gak = (from x in context.GAK_StatLP_UNP_Itog
                               orderby x.Faculty
                               select new
                               {
                                   УНП = x.Faculty,
                                   Образовательных_программ = x.OPquan,
                                   Всего_ГЭК = x.GAKquan,
                                   ГЭК_по_гос_экзаменам = x.GAKExamquan,
                                   ГЭК_по_защитам_ВКР = x.GAKVKRquan,
                                   ГЭК_итоговых = x.GAKItogquan
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(gak);
                    dgvUNP.DataSource = dt;
                    foreach (DataGridViewColumn col in dgvUNP.Columns)
                        col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");
                    try
                    {
                        dgvUNP.Columns["УНП"].Frozen = true;
                        dgvUNP.Columns["УНП"].Width = 200;
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
        private void FillGAK_Member()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var gak = (from x in context.GAK_StatChairmanPPNPR_Itog
                               orderby x.StudyLevel
                               select new
                               {
                                   Уровень = x.StudyLevel,
                                   Всего_членов_ГЭК_по_гос_экзаменам_СПбГУ = x.MemberExamSPbGU,
                                   Всего_членов_ГЭК_по_гос_экзаменам_внешних = x.MemberExamPP,
                                   Всего_председателей_ГЭК_по_гос_экзаменам = x.MemberExamChairman,

                                   Всего_членов_ГЭК_по_защитам_ВКР_СПбГУ = x.MemberVKRSPbGU,
                                   Всего_членов_ГЭК_по_защитам_ВКР_внешних = x.MemberVKRPP,
                                   Всего_председателей_ГЭК_по_защитам_ВКР = x.MemberVKRChairman,

                                   Всего_членов_ГЭК_итоговых_СПбГУ = x.MemberItogSPbGU,
                                   Всего_членов_ГЭК_итоговых_внешних = x.MemberItogPP,
                                   Всего_председателей_ГЭК_итоговых = x.MemberItogChairman,
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(gak);
                    dgvMember.DataSource = dt;
                    foreach (DataGridViewColumn col in dgvMember.Columns)
                        col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");
                }
            }
            catch (Exception)
            {

            }
        }

        private void FillGAK_MemberTotal()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var gak = (from x in context.GAK_StatChairmanPPNPR_Itog2
                               orderby x.StudyLevel
                               select new
                               {
                                   Уровень = x.StudyLevel,

                                   Всего_членов_ГЭК_СПбГУ = x.MemberSPbGU,
                                   Всего_членов_ГЭК_внешних = x.MemberPP,
                                   Всего_председателей_ГЭК = x.MemberChairman,

                                   //Всего_членов_ГЭК_по_гос_экзаменам_СПбГУ = x.MemberExamSPbGU,
                                   //Всего_членов_ГЭК_по_гос_экзаменам_внешних = x.MemberExamPP,
                                   //Всего_председателей_ГЭК_по_гос_экзаменам = x.MemberExamChairman,

                                   //Всего_членов_ГЭК_по_защитам_ВКР_СПбГУ = x.MemberVKRSPbGU,
                                   //Всего_членов_ГЭК_по_защитам_ВКР_внешних = x.MemberVKRPP,
                                   //Всего_председателей_ГЭК_по_защитам_ВКР = x.MemberVKRChairman,

                                   //Всего_членов_ГЭК_итоговых_СПбГУ = x.MemberItogSPbGU,
                                   //Всего_членов_ГЭК_итоговых_внешних = x.MemberItogPP,
                                   //Всего_председателей_ГЭК_итоговых = x.MemberItogChairman,
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(gak);
                    dgvMemberTotal.DataSource = dt;
                    foreach (DataGridViewColumn col in dgvMemberTotal.Columns)
                        col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillStatistics();
        }
    }
}
