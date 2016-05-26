﻿using System;
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
    public partial class CardOrganizationPerson: Form
    {
        public int? _id;
        public int PersId;
        UpdateVoidHandler _hdl;

        public CardOrganizationPerson()
        {
            InitializeComponent();
        }
        public CardOrganizationPerson(int? Id, int persId, UpdateVoidHandler h)
        {
            InitializeComponent();
            _id = Id;
            PersId = persId;
            _hdl = h;
            this.MdiParent = Util.mainform;
            FillCard();
        }
        public void FillCard()
        {
            btnAdd.Text = (_id.HasValue) ? "Обновить" : "Добавить";
            ComboServ.FillCombo(cbPerson, HelpClass.GetComboListByTable("dbo.PartnerPerson"), false, false);

            if (!_id.HasValue) return;
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.OrganizationPerson
                           join p in context.PartnerPerson
                           on x.PartnerPersonId equals p.Id
                           where x.Id == _id.Value
                           && x.OrganizationId == PersId
                           select new
                           {
                               p.Id,
                               p.Name,
                               p.Title,
                               p.IsSPbGUGraduate,
                               p.AlumniAssociation,
                               ActivityAreaName = p.ActivityArea.Name,
                               p.Email,
                               x.Position,
                               x.Comment,
                           }).FirstOrDefault();
                if (lst == null)
                    return;
                lblTitle.Text = lst.Title;
                lblActivityArea.Text = lst.ActivityAreaName;
                lblAlumni.Text = (lst.AlumniAssociation ?? false) ? "да" : "нет";
                lblEmail.Text = lst.Email;
                lblIsGreduateSPbGU.Text = (lst.IsSPbGUGraduate ?? false) ? "да" : "нет";

                tbposition.Text = lst.Position;
                tbComment.Text = lst.Comment;
                ComboServ.SetComboId(cbPerson, lst.Id);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int? PersonId = ComboServ.GetComboIdInt(cbPerson);
            if (!PersonId.HasValue)
            {
                MessageBox.Show("Контактное лицо не выбрано");
                return;
            }
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.OrganizationPerson
                           where x.OrganizationId == PersId
                           && x.Id != _id
                           && x.PartnerPersonId == PersonId.Value
                           select new
                           {
                               x.Id
                           }).ToList().Count();
                if (lst > 0)
                {
                    MessageBox.Show("Такое контактное лицо уже было добавлено");
                    return;
                }
                else if (!_id.HasValue)
                {
                    OrganizationPerson org = new OrganizationPerson()
                    {
                        OrganizationId = PersId,
                        PartnerPersonId = PersonId.Value,
                        Position = tbposition.Text.Trim(),
                        Comment = tbComment.Text.Trim(),
                    };
                    context.OrganizationPerson.Add(org);
                    context.SaveChanges();
                    _id = org.Id;
                }
                else if (_id.HasValue)
                {
                    OrganizationPerson org = context.OrganizationPerson.Where(x => x.Id == _id.Value).First();
                    org.PartnerPersonId = PersonId.Value;
                    org.Position = tbposition.Text.Trim();
                    org.Comment = tbComment.Text.Trim();
                    context.SaveChanges();
                }
                if (_hdl != null && _id.HasValue)
                    _hdl(_id);
            }
            this.Close();
        }

        private void cbPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? PersonId = ComboServ.GetComboIdInt(cbPerson);
            if (!PersonId.HasValue)
                return;

            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from p in context.PartnerPerson
                           where p.Id == PersonId
                           select new
                           {
                               p.Id,
                               p.Name,
                               p.Title,
                               p.IsSPbGUGraduate,
                               p.AlumniAssociation,
                               ActivityAreaName = p.ActivityArea.Name,
                               p.Email,
                           }).FirstOrDefault();
                if (lst == null)
                    return;
                lblTitle.Text = lst.Title;
                lblActivityArea.Text = lst.ActivityAreaName;
                lblAlumni.Text = (lst.AlumniAssociation ?? false) ? "да" : "нет";
                lblEmail.Text = lst.Email;
                lblIsGreduateSPbGU.Text = (lst.IsSPbGUGraduate ?? false) ? "да" : "нет";
            }
        }
    }
}