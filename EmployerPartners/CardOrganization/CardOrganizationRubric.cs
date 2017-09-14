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
    public partial class CardOrganizationRubric : CardRubrics
    {
        public CardOrganizationRubric()
            : base()
        {
        }
        public CardOrganizationRubric(int? Id, int objId, UpdateIntHandler h)
            : base(Id, objId, h)
        {
        }
        public override void FillCard()
        {
            string query = "dbo.Rubric where Id not in (select RubricId from dbo.OrganizationRubric where OrganizationId = " + ObjectId.ToString() +
                ((_id.HasValue) ? (" and Id!= " + _id.Value.ToString() + ")") : ")");
            if (!_id.HasValue)
            {
                FillControls(query, null);
                return;
            }
            else
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.OrganizationRubric
                               join p in context.Rubric
                               on x.RubricId equals p.Id
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
            var lst = (from x in context.OrganizationRubric
                       where x.OrganizationId == ObjectId
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
            OrganizationRubric org = new OrganizationRubric()
            {
                OrganizationId = ObjectId,
                RubricId = AreaId,
            };
            context.OrganizationRubric.Add(org);
            context.SaveChanges();
            _id = org.Id;
        }
        public override void UpdateRec(EmployerPartnersEntities context, int RubricId)
        {
            OrganizationRubric org = context.OrganizationRubric.Where(x => x.Id == _id.Value).First();
            org.RubricId = RubricId;
            context.SaveChanges();
        }
    }
}
