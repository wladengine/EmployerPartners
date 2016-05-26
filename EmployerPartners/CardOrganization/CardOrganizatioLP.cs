using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployerPartners
{
    public partial class CardOrganizationLP : CardLP
    {
        public CardOrganizationLP()
            : base()
        {
        }
        public CardOrganizationLP(int? Id, int objId, UpdateVoidHandler h)
            : base(Id, objId, h)
        {
        }
        public override void FillCard()
        {
            List<string> AddString = new List<string>();
            if (StudyLevelId.HasValue) AddString.Add(" and StudyLevelId= " + StudyLevelId.Value.ToString());
            if (ProgramTypeId.HasValue) AddString.Add(" and ProgramTypeId= " + ProgramTypeId.Value.ToString());
            if (QualificationId.HasValue) AddString.Add(" and QualificationId= " + QualificationId.Value.ToString());


            string query = @"
select distinct  CONVERT(varchar(100), LicenseProgram.Id) AS Id, LicenseProgram.Code + ' ('+LicenseProgram.Name +')' as Name
from dbo.LicenseProgram where Id not in (select LicenseProgramId from dbo.OrganizationLP where OrganizationId = " + ObjectId.ToString() 
                + ((_id.HasValue) ? (" and Id!= " + _id.Value.ToString() +")") : ")");
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
                                   p.Id,
                               }).FirstOrDefault();
                    if (lst == null)
                        return;
                    FillControls(query, lst.Id);
                }
        }
        public override bool CheckExist(EmployerPartnersEntities context, int? AreaId)
        {
            var lst = (from x in context.OrganizationLP
                       where x.OrganizationId == ObjectId
                       && x.Id != _id
                       && x.LicenseProgramId == AreaId
                       select new
                       {
                           x.Id
                       }).ToList().Count();
            if (lst > 0)
            {
                MessageBox.Show("Такое направление уже была добавлено");
                return false;
            }
            return true;
        }
        public override void InsertRec(EmployerPartnersEntities context, int AreaId)
        {
            OrganizationLP org = new OrganizationLP()
            {
                OrganizationId = ObjectId,
                LicenseProgramId = AreaId,
            };
            context.OrganizationLP.Add(org);
            context.SaveChanges();
            _id = org.Id;
        }
        public override void UpdateRec(EmployerPartnersEntities context, int RubricId)
        {
            OrganizationLP org = context.OrganizationLP.Where(x => x.Id == _id.Value).First();
            org.LicenseProgramId = RubricId;
            context.SaveChanges();
        }
    }
}
