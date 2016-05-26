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
    public partial class CardPersonRubric : CardRubrics
    {
        public CardPersonRubric()
            : base()
        {
        }
        public CardPersonRubric(int? Id, int objId, UpdateVoidHandler h)
            : base(Id, objId, h)
        {
        }
        public override void FillCard()
        {
            string query = "dbo.Rubric where Id not in (select RubricId from dbo.PartnerPersonRubric where PartnerPersonId = " + ObjectId.ToString() +
                ((_id.HasValue) ? (" and Id!= " + _id.Value.ToString() + ")") : ")");
            if (!_id.HasValue)
            {
                FillControls(query, null);
                return;
            }
            else
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.PartnerPersonRubric
                               join p in context.Rubric
                               on x.RubricId equals p.Id
                               where x.Id == _id.Value
                               && x.PartnerPersonId == ObjectId
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
            var lst = (from x in context.PartnerPersonRubric
                       where x.PartnerPersonId == ObjectId
                       && x.Id != _id
                       && x.RubricId == AreaId
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
        public override void InsertRec(EmployerPartnersEntities context, int AreaId)
        {
            PartnerPersonRubric org = new PartnerPersonRubric()
            {
                PartnerPersonId = ObjectId,
                RubricId = AreaId,
            };
            context.PartnerPersonRubric.Add(org);
            context.SaveChanges();
            _id = org.Id;
        }
        public override void UpdateRec(EmployerPartnersEntities context, int RubricId)
        {
            PartnerPersonRubric org = context.PartnerPersonRubric.Where(x => x.Id == _id.Value).First();
            org.RubricId = RubricId;
            context.SaveChanges();
        }
    }
}
