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
    public partial class CardOrganizationArea : CardActivityArea
    {
        public CardOrganizationArea()
            : base()
        {
        }
        public CardOrganizationArea(int? Id, int orgId, UpdateVoidHandler h)
            : base(Id, orgId, h)
        {
        }
        public override void FillCard()
        {
            string query = "dbo.ActivityArea where Id not in (select ActivityAreaId from dbo.OrganizationActivityArea where OrganizationId = " + ObjectId.ToString() + 
                ((_id.HasValue)?(" and Id!= "+_id.Value.ToString()+")"):")");
            if (!_id.HasValue) 
            { 
                FillControls(query, null); 
                return; 
            } 
            else
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.OrganizationActivityArea
                               join p in context.ActivityArea
                               on x.ActivityAreaId equals p.Id
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
            var lst = (from x in context.OrganizationActivityArea
                       where x.OrganizationId == ObjectId
                       && x.Id != _id
                       && x.ActivityAreaId == AreaId
                       select new
                       {
                           x.Id
                       }).ToList().Count();
            if (lst > 0)
            {
                MessageBox.Show("Такая сфера деятельности уже была добавлена");
                return false;
            }
            return true;
        }
        public override void InsertRec(EmployerPartnersEntities context, int AreaId)
        {
            OrganizationActivityArea org = new OrganizationActivityArea()
            {
                OrganizationId = ObjectId,
                ActivityAreaId = AreaId,
            };
            context.OrganizationActivityArea.Add(org);
            context.SaveChanges();
            _id = org.Id;
        }
        public override void UpdateRec(EmployerPartnersEntities context, int AreaId)
        {
            OrganizationActivityArea org = context.OrganizationActivityArea.Where(x => x.Id == _id.Value).First();
            org.ActivityAreaId = AreaId;
            context.SaveChanges();
        }
    }
}
