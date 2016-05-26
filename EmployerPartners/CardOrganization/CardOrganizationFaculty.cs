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
    public partial class CardOrganizationFaculty : CardFaculty
    {
        public CardOrganizationFaculty()
            : base()
        {
        }
        public CardOrganizationFaculty(int? Id, int objId, UpdateVoidHandler h)
            : base(Id, objId, h)
        {
        }
        public override void FillCard()
        {
            string query = "dbo.Faculty where Id not in (select FacultyId from dbo.OrganizationFaculty where OrganizationId = " + ObjectId.ToString() +
                ((_id.HasValue) ? (" and Id!= " + _id.Value.ToString() + ")") : ")");
            if (!_id.HasValue)
            {
                FillControls(query, null);
                return;
            }
            else
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.OrganizationFaculty
                               join p in context.Faculty
                               on x.FacultyId equals p.Id
                               where x.Id == _id.Value
                               && x.OrganizationId == ObjectId
                               select new
                               {
                                   p.Id,
                                   p.Name,
                               }).FirstOrDefault();
                    if (lst == null)
                        return;
                    FillControls(query, lst.Id);
                }
        }
        public override bool CheckExist(EmployerPartnersEntities context, int? AreaId)
        {
            var lst = (from x in context.OrganizationFaculty
                       where x.OrganizationId == ObjectId
                       && x.Id != _id
                       && x.FacultyId == AreaId
                       select new
                       {
                           x.Id
                       }).ToList().Count();
            if (lst > 0)
            {
                MessageBox.Show("Такая рубрика уже была добавлена");
                return false;
            }
            return true;
        }
        public override void InsertRec(EmployerPartnersEntities context, int fId)
        {
            OrganizationFaculty org = new OrganizationFaculty()
            {
                OrganizationId = ObjectId,
                FacultyId = fId,
            };
            context.OrganizationFaculty.Add(org);
            context.SaveChanges();
            _id = org.Id;
        }
        public override void UpdateRec(EmployerPartnersEntities context, int fId)
        {
            OrganizationFaculty org = context.OrganizationFaculty.Where(x => x.Id == _id.Value).First();
            org.FacultyId = fId;
            context.SaveChanges();
        }
    }
}
