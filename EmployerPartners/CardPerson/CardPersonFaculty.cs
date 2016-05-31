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
    public partial class CardPersonFaculty : CardFaculty
    {
        public CardPersonFaculty()
            : base()
        {
        }
        public CardPersonFaculty(int? Id, int objId, UpdateVoidHandler h)
            : base(Id, objId, h)
        {
        }
        public override void FillCard()
        {
            string query = "dbo.Faculty ";
            //string query = "dbo.Faculty where Id not in (select FacultyId from dbo.PartnerPersonFaculty where PartnerPersonId = " + ObjectId.ToString() +
            //    ((_id.HasValue) ? (" and Id!= " + _id.Value.ToString() + ")") : ")");
            if (!_id.HasValue)
            {
                FillControls(query, null, null);
                return;
            }
            else
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.PartnerPersonFaculty
                               join p in context.Faculty
                               on x.FacultyId equals p.Id
                               where x.Id == _id.Value
                               && x.PartnerPersonId == ObjectId
                               select new
                               {
                                   x.RubricId,
                                  FacultyId = p.Id,
                                   p.Name,
                               }).FirstOrDefault();
                    if (lst == null)
                        return;
                    FillControls(query, lst.FacultyId, lst.RubricId);
                }
        }
        public override bool CheckExist(EmployerPartnersEntities context, int? AreaId)
        {
            var lst = (from x in context.PartnerPersonFaculty
                       where x.PartnerPersonId == ObjectId
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
        public override void InsertRec(EmployerPartnersEntities context, int fId, int? RubricId)
        {
            PartnerPersonFaculty org = new PartnerPersonFaculty()
            {
                PartnerPersonId = ObjectId,
                FacultyId = fId,
                RubricId = RubricId,
            };
            context.PartnerPersonFaculty.Add(org);
            context.SaveChanges();
            _id = org.Id;
        }
        public override void UpdateRec(EmployerPartnersEntities context, int fId, int? RubricId)
        {
            PartnerPersonFaculty org = context.PartnerPersonFaculty.Where(x => x.Id == _id.Value).First();
            org.FacultyId = fId;
            org.RubricId = RubricId;
            context.SaveChanges();
        }
    }
}
