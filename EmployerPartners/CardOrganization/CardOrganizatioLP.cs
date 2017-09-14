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
    public partial class CardOrganizationLP : CardLP
    {
        public CardOrganizationLP()
            : base()
        {
        }
        public CardOrganizationLP(int? Id, int objId, UpdateIntHandler h)
            : base(Id, objId, h)
        {
        }
        public override void FillCard()
        {
            base.FillCard();
            if (_id.HasValue)
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.OrganizationLP
                               join p in context.LicenseProgram
                               on x.LicenseProgramId equals p.Id
                               where x.Id == _id.Value
                               && x.OrganizationId == ObjectId
                               select new
                               {
                                   x.RubricId,
                                   LPId = p.Id,
                                   p.StudyLevelId,
                                   p.ProgramTypeId,
                                   p.QualificationId,
                               }).FirstOrDefault();
                    if (lst == null)
                        return;

                    RubricId = lst.RubricId;
                    StudyLevelId = lst.StudyLevelId;
                    ProgramTypeId = lst.ProgramTypeId;
                    QualificationId = lst.QualificationId;
                }
            }
        }
        public override void FillLP()
        {
            List<string> AddString = new List<string>();
            //if (StudyLevelId.HasValue) AddString.Add(" and StudyLevelId= " + StudyLevelId.Value.ToString());
            //if (ProgramTypeId.HasValue) AddString.Add(" and ProgramTypeId= " + ProgramTypeId.Value.ToString());
            //if (QualificationId.HasValue) AddString.Add(" and QualificationId= " + QualificationId.Value.ToString());

            bool sqlWhere = false;
            if (StudyLevelId.HasValue)
            {
                sqlWhere = true;
                AddString.Add(" where StudyLevelId = " + StudyLevelId.Value.ToString());
            }
            if (ProgramTypeId.HasValue)
            {
                if (sqlWhere)
                {
                    AddString.Add(" and ProgramTypeId = " + ProgramTypeId.Value.ToString());
                }
                else
                {
                    sqlWhere = true;
                    AddString.Add(" where ProgramTypeId = " + ProgramTypeId.Value.ToString());
                }
            }
            if (QualificationId.HasValue)
            {
                if (sqlWhere)
                {
                    AddString.Add(" and QualificationId = " + QualificationId.Value.ToString());
                }
                else
                {
                    sqlWhere = true;
                    AddString.Add(" where QualificationId = " + QualificationId.Value.ToString());
                }
            }
            if (LPLicenseId.HasValue)
            {
                if (sqlWhere)
                {
                    AddString.Add(" and LicenseId = " + LPLicenseId.Value.ToString());
                }
                else
                {
                    sqlWhere = true;
                    AddString.Add(" where LicenseId = " + LPLicenseId.Value.ToString());
                }
            }

            string query = @"
select distinct  CONVERT(varchar(100), LicenseProgram.Id) AS Id, LicenseProgram.Code + ' ('+LicenseProgram.Name +')' as Name
from dbo.LicenseProgram ";
            foreach (string s in AddString)
                query += s;
            query += " order by 2";
            if (!_id.HasValue)
            {
                FillControls(query, null);
                return;
            }
            else
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.OrganizationLP
                               join p in context.LicenseProgram
                               on x.LicenseProgramId equals p.Id
                               where x.Id == _id.Value
                               && x.OrganizationId == ObjectId
                               select new
                               {
                                   x.RubricId,
                                   LPId = p.Id,
                               }).FirstOrDefault();
                    if (lst == null)
                        return;
                    FillControls(query, lst.LPId);
                }
        }
        public override bool CheckExist(EmployerPartnersEntities context, int? LPId)
        {
            var lst = (from x in context.OrganizationLP
                       where x.OrganizationId == ObjectId
                       && x.Id != _id
                       && x.LicenseProgramId == LPId
                       && x.RubricId == RubricId
                       select new
                       {
                           LPId = x.Id,
                       }).ToList().Count();
            if (lst > 0)
            {
                MessageBox.Show("Такое направление уже было добавлено");
                return false;
            }
            return true;
        }
        public override void InsertRec(EmployerPartnersEntities context, int ObjId)
        {
            OrganizationLP org = new OrganizationLP()
            {
                OrganizationId = ObjectId,
                LicenseProgramId = ObjId,
                RubricId = RubricId,
            };
            context.OrganizationLP.Add(org);
            context.SaveChanges();
            _id = org.Id;
        }
        public override void UpdateRec(EmployerPartnersEntities context, int ObjId)
        {
            OrganizationLP org = context.OrganizationLP.Where(x => x.Id == _id.Value).First();
            org.LicenseProgramId = ObjId;
            org.RubricId = RubricId;
            context.SaveChanges();
        }
    }
}
