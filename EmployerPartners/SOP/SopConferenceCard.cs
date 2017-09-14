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

namespace EmployerPartners
{
    public partial class SopConferenceCard : Form
    {
        int _Id;
        int? _QuestionId;
        List<SOPConferenseQuestionFile> Filelst;
        Color clrMemberIsAbsent = Color.LightGray;
        UpdateVoidHandler hndl;

        public SopConferenceCard(int id, UpdateVoidHandler h)
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            _Id = id;
            hndl = h;
            pnlMemberIsAbsent.BackColor = clrMemberIsAbsent;
        }
        public void FillCard()
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var sop_conf = (from x in context.SOP_Conference
                                           join sp in context.SOP on x.SOPId equals sp.Id
                                           where x.Id == _Id
                                           select new
                                           {
                                               x.Number,
                                               x.Theme,
                                               x.Date,
                                               sp.Name,
                                               x.AuthorCreate,
                                               x.AuthorUpdate,
                                               x.TimestampCreate,
                                               x.TimestampUpdate
                                           }).First();
                tbConferenceNumber.Text = sop_conf.Number;
                tbConferenceTheme.Text = sop_conf.Theme;
                lblSOPName.Text = sop_conf.Name;
                dtpConferenceDate.Value = sop_conf.Date ?? DateTime.Now;
                lblAuthorCreate.Text = string.Format("{0} ({1})", sop_conf.AuthorCreate, sop_conf.TimestampCreate);
                lblAuthorUpdate.Text = string.Format("{0} ({1})", sop_conf.AuthorUpdate, sop_conf.TimestampUpdate);

                FillQuestions(context);
                FillQuestionGroupBox();
                FillMembers(context);
                FillProtocolFile(context);
            }
        }
        private void SopConferenceCard_Shown(object sender, EventArgs e)
        {
           FillCard();
        }
        #region Question
        private void FillQuestions(EmployerPartnersEntities context)
        {
            var Questions = (from x in context.SOP_Conference_Questions
                             where x.SOPConferenceId == _Id
                             select new
                             {
                                 x.Id,
                                 x.Theme,
                             }).ToList().Select(x => new
                             {
                                 Id = x.Id,
                                 Вопрос = x.Theme,
                                 Файлов = context.SOP_Conference_Questions_Files.Where(t=>t.SOPConferenceQuestionId == x.Id).Count(),
                             }).ToList();
            dgvQuestions.DataSource = Questions;
            foreach (string s in new List<string>(){"Id"})
            {
                if (dgvQuestions.Columns.Contains(s))
                    dgvQuestions.Columns[s].Visible = false;
            }
        }
        private void btnQuestionAddFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            try
            {
                string newfilename = Util.TempFilesFolder + Guid.NewGuid().ToString().Substring(10);
                File.Copy(ofd.FileName, newfilename);
                
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    Filelst.Add(new SOPConferenseQuestionFile()
                    {
                        FileId = Guid.NewGuid(),
                        FileName = ofd.FileName.Substring(ofd.FileName.LastIndexOf("\\") + 1),
                        FileData = File.ReadAllBytes(newfilename)
                    });
                }
                File.Delete(newfilename); 
                FillQuestionAddFiles();
            }
            catch { }
        }
        private void FillQuestionAddFiles()
        {
            var lst = (from x in Filelst
                       select new
                       {
                           Id = x.FileId,
                           Название = x.FileName,
                       }).ToList();

            dgvQuestionAddFiles.DataSource = lst;
            foreach (string s in new List<string>() { "Id" })
            {
                if (dgvQuestionAddFiles.Columns.Contains(s))
                    dgvQuestionAddFiles.Columns[s].Visible = false;
            }          
        }
        private void btnQuestionShowGbAdd_Click(object sender, EventArgs e)
        {
            if (Filelst.Count()>0)
            {

            }
            _QuestionId = null;
            dgvQuestions.ClearSelection();
            FillQuestionGroupBox();
        }
        private void dgvQuestions_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgvQuestions.CurrentCell!=null)
            {
                if (dgvQuestions.CurrentCell.RowIndex > -1)
                    _QuestionId = (int)dgvQuestions.CurrentRow.Cells["Id"].Value;
                else
                    _QuestionId = null;
            }
            else
                _QuestionId = null; 

            FillQuestionGroupBox();
        }
        private void FillQuestionGroupBox()
        {
            grbQuestionAdd.Visible = !_QuestionId.HasValue;
            grbQuestionEdit.Visible = _QuestionId.HasValue;
            Filelst = new List<SOPConferenseQuestionFile>();
            if (_QuestionId.HasValue)
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var q = context.SOP_Conference_Questions.Where(x => x.Id == _QuestionId.Value && x.SOPConferenceId == _Id).FirstOrDefault();
                    if (q == null)
                    {
                        MessageBox.Show("Какая-то нелепая ошибка");
                        return;
                    }
                    tbQuestionEditText.Text = q.Theme;
                    FillQuestionEditFiles(context);
                }
            }
            else
            {
                tbQuestionAddText.Text = "";
                dgvQuestionAddFiles.DataSource = Filelst;
            }
        }
        private void btnSopConferenceCardUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить совещание?", "Подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    SOP_Conference conf = context.SOP_Conference.Where(x => x.Id == _Id).FirstOrDefault();
                    if (conf == null) return;

                    conf.Date = dtpConferenceDate.Value;
                    conf.Number = tbConferenceNumber.Text.Trim();
                    conf.Theme = tbConferenceTheme.Text.Trim();
                    context.SaveChanges();

                    if (hndl != null)
                        hndl();

                    this.Close();
                }
            }
        }
        private void btnQuestionAdd_Click(object sender, EventArgs e)
        {
            using (EmployerPartnersEntities context  = new EmployerPartnersEntities())
            {
                SOP_Conference_Questions ques = new SOP_Conference_Questions();
                ques.Theme = tbQuestionAddText.Text.Trim();
                ques.SOPConferenceId = _Id;
                context.SOP_Conference_Questions.Add(ques);
                context.SaveChanges();
                _QuestionId = ques.Id;

                SOP_Conference conf = context.SOP_Conference.Where(x => x.Id == _Id).FirstOrDefault();
                conf.AuthorUpdate = Util.GetUserName();
                conf.TimestampUpdate = DateTime.Now;
                context.SaveChanges();

                foreach (var File in Filelst)
                {
                    Guid FileId = Guid.NewGuid();
                    context.SOP_Conference_Questions_Files.Add (new SOP_Conference_Questions_Files(){

                        Id = FileId,
                        SOPConferenceQuestionId = _QuestionId.Value,
                        FileName = File.FileName,
                        Timestamp = DateTime.Now,
                        Author = Util.GetUserName(),
                    });
                    context.FileStorage.Add(new FileStorage() { 
                        Id = FileId,
                        FileData = File.FileData,
                    });
                    context.SaveChanges();
                }
                FillQuestions(context);
                dgvQuestions.ClearSelection();
                _QuestionId = null;
                FillQuestionGroupBox();
            }
        }
        private void btnQuestionUpdate_Click(object sender, EventArgs e)
        {
            if (!_QuestionId.HasValue)
                FillQuestionGroupBox();

            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var Ques = context.SOP_Conference_Questions.Where(x => x.Id == _QuestionId.Value && x.SOPConferenceId == _Id).FirstOrDefault();
                if (Ques == null)
                    FillQuestions(context);
                Ques.Theme = tbQuestionEditText.Text;
                Ques.Timestamp = DateTime.Now;
                Ques.Author = Util.GetUserName();
                context.SaveChanges();
                FillQuestions(context);
            }
        }
        private void FillQuestionEditFiles(EmployerPartnersEntities context)
        {
            var Files = context.SOP_Conference_Questions_Files.Where(x => x.SOPConferenceQuestionId == _QuestionId.Value).Select(x => new
            {
                x.Id,
                Название = x.FileName,
            }).ToList();
            dgvQuestionEditFiles.DataSource = Files;
            foreach (string s in new List<string>() { "Id" })
            {
                if (dgvQuestionEditFiles.Columns.Contains(s))
                    dgvQuestionEditFiles.Columns[s].Visible = false;
            } 
        }
        private void btnQuestionEditFileAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            try
            {
                string newfilename = Util.TempFilesFolder + Guid.NewGuid().ToString().Substring(10);
                File.Copy(ofd.FileName, newfilename);
                Guid FileId = Guid.NewGuid();
                string FileName = ofd.FileName.Substring(ofd.FileName.LastIndexOf("\\")+1);
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    context.SOP_Conference_Questions_Files.Add(new SOP_Conference_Questions_Files()
                        {
                            Id = FileId,
                            FileName = FileName,
                            Timestamp = DateTime.Now,
                            SOPConferenceQuestionId = _QuestionId.Value,
                        });
                    context.FileStorage.Add(new FileStorage()
                    {
                        Id = FileId,
                        FileData = File.ReadAllBytes(newfilename),
                    });
                    context.SaveChanges();
                    FillQuestionEditFiles(context);
                }
                File.Delete(newfilename);
            }
            catch { }
        }
        private void btnQuestionDelete_Click(object sender, EventArgs e)
        {
            if (dgvQuestions.CurrentRow == null || dgvQuestions.CurrentRow.Index < 0)
                return;
            if (MessageBox.Show("Удалить выбранный вопрос из совещания вместе с файлами?", "Подтверждение", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                return;

            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                int QuestId = (int)dgvQuestions.CurrentRow.Cells["Id"].Value;
                
                context.SOP_Conference_Questions.Remove(context.SOP_Conference_Questions.Where(x => x.Id == _QuestionId && x.SOPConferenceId == _Id).FirstOrDefault());
                context.SaveChanges();
                FillQuestions(context);
            }
        }
        private void dgvQuestionAddFiles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvQuestionAddFiles.CurrentCell == null) return;
            if (dgvQuestionAddFiles.CurrentCell.RowIndex < 0) return;

            if (dgvQuestionAddFiles.CurrentCell.ColumnIndex == dgvQuestionAddFiles.Columns["ColumnQuestionAddFileOpen"].Index)
            {
                Guid FileId = (Guid)dgvQuestionAddFiles.CurrentRow.Cells["Id"].Value;
                var File = Filelst.Where(x => x.FileId == FileId).FirstOrDefault();
                if (File == null)
                {
                    return;
                }
                OpenFile(File.FileData, File.FileName);
            }
            else if (dgvQuestionAddFiles.CurrentCell.ColumnIndex == dgvQuestionAddFiles.Columns["ColumnQuestionAddFileRemove"].Index)
            {
                if (MessageBox.Show("Удалить выбранный файл?", "Предупреждение", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                    return;
                Guid FileId = (Guid)dgvQuestionAddFiles.CurrentRow.Cells["Id"].Value;
                Filelst.Remove(Filelst.Where(x => x.FileId == FileId).FirstOrDefault());
                FillQuestionAddFiles();
            }
        }
        private void dgvQuestionEditFiles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvQuestionEditFiles.CurrentCell == null) return;
            if (dgvQuestionEditFiles.CurrentCell.RowIndex < 0) return;

            if (dgvQuestionEditFiles.CurrentCell.ColumnIndex == dgvQuestionEditFiles.Columns["ColumnQuestionEditFileOpen"].Index)
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    Guid FileId = (Guid)dgvQuestionEditFiles.CurrentRow.Cells["Id"].Value;
                    var File = (from x in context.SOP_Conference_Questions_Files
                                join fs in context.FileStorage on x.Id equals fs.Id
                                where x.Id == FileId
                                select new
                                {
                                    x.FileName,
                                    fs.FileData,
                                }).FirstOrDefault();
                    if (File == null)
                    {
                        return;
                    }
                    OpenFile(File.FileData, File.FileName);
                }
            }
            else if (dgvQuestionEditFiles.CurrentCell.ColumnIndex == dgvQuestionEditFiles.Columns["ColumnQuestionEditFileDelete"].Index)
            {
                if (MessageBox.Show("Удалить выбранный файл?", "Предупреждение", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                    return;
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    Guid FileId = (Guid)dgvQuestionEditFiles.CurrentRow.Cells["Id"].Value;
                    context.SOP_Conference_Questions_Files.Remove(context.SOP_Conference_Questions_Files.Where(x => x.Id == FileId).FirstOrDefault());
                    context.SaveChanges();
                    FillQuestionEditFiles(context);
                }
            }
        }
        private void OpenFile(byte[] FileData, string FileName)
        {
                string ResultFileName = Util.TempFilesFolder + FileName.Substring(FileName.LastIndexOf("\\") + 1);
                using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                {
                    saveFileDialog1.FileName = ResultFileName;
                    if (DialogResult.OK != saveFileDialog1.ShowDialog())
                        return;
                    File.WriteAllBytes(saveFileDialog1.FileName, FileData);
                }
                System.Diagnostics.Process.Start(ResultFileName);
        }
        #endregion
        #region Members
        private void FillMembers(EmployerPartnersEntities context)
        {
            int SopId = context.SOP_Conference.Where(x => x.Id == _Id).Select(x => x.SOPId).First();
            var lst = (from x in context.SOP_Members_NPR
                       join npr in context.SAP_NPR on x.Tabnum equals npr.Tabnum

                       join conf in context.SOP_ConferenceMembers_NPR on x.Tabnum equals conf.Tabnum into _conf
                       from conf in _conf.DefaultIfEmpty()

                       where x.SOPId == SopId
                       select new
                       {
                           Id = (conf==null)? null : (int?)conf.Id,
                           PartnerPersonId = (int?)null,
                           IsAbsent = (conf== null),
                           Tabnum = x.Tabnum,
                           ФИО = ((npr.Lastname + " ") ?? "") + ((npr.Name + " ") ?? "") + ((npr.Surname + " ") ?? ""),
                           isNPR = true,
                           Степень = npr.Degree,
                       }).ToList().Union((from x in context.SOP_Members_PartnerPerson
                                          join pp in context.PartnerPerson on x.PartnerPersonId equals pp.Id

                                          join conf in context.SOP_ConferenceMembers_PartnerPerson on x.PartnerPersonId equals conf.PartnerPersonId into _conf
                                          from conf in _conf.DefaultIfEmpty()

                                          where x.SOPId == SopId
                                          select new
                                          {
                                              Id = (conf==null)? null : (int?)conf.Id,
                                              PartnerPersonId = (int?)x.PartnerPersonId,
                                              IsAbsent = (conf == null),
                                              Tabnum = "",
                                              ФИО = pp.Name,
                                              isNPR = false,
                                              Степень = pp.Degree.Name,
                                          }).ToList()).ToList();
            dgvMembers.DataSource = lst;
            foreach (string s in new List<string>(){"Id", "IsAbsent", "isNPR", "Tabnum", "PartnerPersonId"})
            {
                if (dgvMembers.Columns.Contains(s))
                    dgvMembers.Columns[s].Visible = false;
            }

        }
        private void dgvMembers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMembers.CurrentCell == null) return;
            if (dgvMembers.CurrentCell.RowIndex <0 ) return;

            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                if (dgvMembers.CurrentCell.ColumnIndex == dgvMembers.Columns["ColumnMemberIsAbsent"].Index)
                {
                    int? Id = (int?)dgvMembers.CurrentRow.Cells["Id"].Value;
                    bool IsNPR = (bool)dgvMembers.CurrentRow.Cells["isNPR"].Value;
                    bool isAbsent = (bool)dgvMembers.CurrentRow.Cells["isAbsent"].Value;
                    if (IsNPR)
                    {
                        string Tabnum = dgvMembers.CurrentRow.Cells["Tabnum"].Value.ToString();
                        if (Id.HasValue)
                        {
                            context.SOP_ConferenceMembers_NPR.Remove(context.SOP_ConferenceMembers_NPR.Where(x => x.Id == Id.Value).First());
                        }
                        else
                        {
                            context.SOP_ConferenceMembers_NPR.Add(new SOP_ConferenceMembers_NPR()
                                {
                                    SOPConferenceId = _Id,
                                    Tabnum = Tabnum,
                                    Author = Util.GetUserName(),
                                    Timestamp = DateTime.Now
                                });
                        }
                    }
                    else
                    {
                        int PartnerPersonId = (int)dgvMembers.CurrentRow.Cells["PartnerPersonId"].Value;
                        if (Id.HasValue)
                        {
                            context.SOP_ConferenceMembers_PartnerPerson.Remove(context.SOP_ConferenceMembers_PartnerPerson.Where(x => x.Id == Id.Value).First());
                        }
                        else
                        {
                            context.SOP_ConferenceMembers_PartnerPerson.Add(new SOP_ConferenceMembers_PartnerPerson()
                            {
                                SOPConferenceId = _Id,
                                PartnerPersonId = PartnerPersonId,
                                Author = Util.GetUserName(),
                                Timestamp = DateTime.Now,
                            });
                        }
                    }
                    context.SaveChanges();
                    FillMembers(context);
                }
        }
        #endregion
        #region ProtocolFile
        private void FillProtocolFile( EmployerPartnersEntities context)
        {
            var File = context.SOP_Conference.Where(x => x.Id == _Id).Select(x => new { x.ProtocolFileId, x.ProtocolFileName }).First();
            lblAddProtofolFile.Visible = !File.ProtocolFileId.HasValue;
            lblProtocolFile.Visible = lblProtocolFileRemove.Visible = File.ProtocolFileId.HasValue;
            if (File.ProtocolFileId.HasValue)
                lblProtocolFile.Text = File.ProtocolFileName;
            lblProtocolFileRemove.Location = new Point(lblProtocolFile.Location.X + lblProtocolFile.Size.Width + 5, lblProtocolFile.Location.Y);
        }
        private void lblProtocolFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var File = context.SOP_Conference.Where(x => x.Id == _Id).Select(x => new { x.ProtocolFileId, x.ProtocolFileName }).First();
                if (File.ProtocolFileId.HasValue)
                {
                    byte[] filedata = context.FileStorage.Where(x => x.Id == File.ProtocolFileId).Select(x => x.FileData).FirstOrDefault();
                    OpenFile(filedata, File.ProtocolFileName);
                }
            }
        }
        private void lblAddProtofolFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            try
            {
                string newfilename = Util.TempFilesFolder + Guid.NewGuid().ToString().Substring(10);
                File.Copy(ofd.FileName, newfilename);
                Guid FileId = Guid.NewGuid();
                string FileName = ofd.FileName.Substring(ofd.FileName.LastIndexOf("\\") + 1);
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    
                    SOP_Conference conf = context.SOP_Conference.Where(x => x.Id == _Id).First();
                    conf.ProtocolFileId = FileId;
                    conf.ProtocolFileName = FileName;
                    conf.ProtocolFileAuthor = Util.GetUserName();
                    conf.ProtocolFileTimestamp = DateTime.Now;

                    context.FileStorage.Add(new FileStorage()
                    {
                        Id = FileId,
                        FileData = File.ReadAllBytes(newfilename),
                    });
                    context.SaveChanges();

                    FillProtocolFile(context);

                }
                File.Delete(newfilename);
            }
            catch { }
        }
        private void lblProtocolFileRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить файл протокола?", "Подтверждение", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                return;
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                SOP_Conference conf = context.SOP_Conference.Where(x => x.Id == _Id).First();
                conf.ProtocolFileId = null;
                conf.ProtocolFileName = null;
                conf.ProtocolFileTimestamp = null;
                conf.ProtocolFileAuthor = null;

                context.SaveChanges();
                FillProtocolFile(context);
            }


        }
        
        #endregion

        private void dgvMembers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && (bool)dgvMembers["IsAbsent", e.RowIndex].Value)
            {
                e.CellStyle.BackColor = clrMemberIsAbsent;
            }
        }
        private void btnConferenceDelete_Click(object sender, EventArgs e)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                SOP_Conference conf = context.SOP_Conference.Where(x => x.Id == _Id).FirstOrDefault();
                context.SOP_Conference.Remove(conf);
                context.SaveChanges();
                if (hndl != null)
                    hndl();
            }
        }
    }

    public class SOPConferenseQuestionFile
    {
        public Guid FileId;
        public string FileName;
        public byte[] FileData;
    }
}
