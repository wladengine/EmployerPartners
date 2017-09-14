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
    public partial class VKRThemesStudentCard : Form
    {
        #region Common
        private string FIO
        {
            get { return tbStudentFIO.Text.Trim(); }
            set { tbStudentFIO.Text = value; }
        }
        private string Account
        {
            get { return tbAccount.Text.Trim(); }
            set { tbAccount.Text = value; }
        }
        private string OP
        {
            get { return tbOP.Text.Trim(); }
            set { tbOP.Text = value; }
        }
        private string OPCrypt
        {
            get { return tbOPCrypt.Text.Trim(); }
            set { tbOPCrypt.Text = value; }
        }
        private string StudyLevel
        {
            get { return tbStudyLevel.Text.Trim(); }
            set { tbStudyLevel.Text = value; }
        }
        //ВКР
        private string VKRName
        {
            get { return tbVKRName.Text.Trim(); }
            set { tbVKRName.Text = value; }
        }
        private string VKRNameEng
        {
            get { return tbVKRNameEng.Text.Trim(); }
            set { tbVKRNameEng.Text = value; }
        }
        private string VKRNameStudent
        {
            get { return tbVKRNameStudent.Text.Trim(); }
            set { tbVKRNameStudent.Text = value; }
        }
        private string VKRNameEngStudent
        {
            get { return tbVKRNameEngStudent.Text.Trim(); }
            set { tbVKRNameEngStudent.Text = value; }
        }
        private string VKRNameFinal
        {
            get { return tbVKRNameFinal.Text.Trim(); }
            set { tbVKRNameFinal.Text = value; }
        }
        private string VKRNameEngFinal
        {
            get { return tbVKRNameEngFinal.Text.Trim(); }
            set { tbVKRNameEngFinal.Text = value; }
        }
        private string Priority
        {
            get { return tbPriority.Text.Trim(); }
            set { tbPriority.Text = value; }
        }
        private bool VKRNameSet
        {
            get { return chbVKRNameSet.Checked; }
            set { chbVKRNameSet.Checked = value; }
        }
        private bool VKRNameEngSet
        {
            get { return chbVKRNameEngSet.Checked; }
            set { chbVKRNameEngSet.Checked = value; }
        }
        private bool VKRNameStudentSet
        {
            get { return chbVKRNameStudentSet.Checked; }
            set { chbVKRNameStudentSet.Checked = value; }
        }
        private bool VKRNameEngStudentSet
        {
            get { return chbVKRNameEngStudentSet.Checked; }
            set { chbVKRNameEngStudentSet.Checked = value; }
        }
        private bool VKRNameFinalSet
        {
            get { return chbVKRNameFinalSet.Checked; }
            set { chbVKRNameFinalSet.Checked = value; }
        }
        private bool VKRNameEngFinalSet
        {
            get { return chbVKRNameEngFinalSet.Checked; }
            set { chbVKRNameEngFinalSet.Checked = value; }
        }
        //Изменение темы ВКР
        private string VKRName_Changed
        {
            get { return tbVKRName_Changed.Text.Trim(); }
            set { tbVKRName_Changed.Text = value; }
        }
        private string VKRNameEng_Changed
        {
            get { return tbVKRNameEng_Changed.Text.Trim(); }
            set { tbVKRNameEng_Changed.Text = value; }
        }
        private string VKRName_ChangedDoc
        {
            get { return tbVKRName_ChangedDoc.Text.Trim(); }
            set { tbVKRName_ChangedDoc.Text = value; }
        }
        //2-е изменение темы ВКР после приказа о рецензентах
        private string VKRName_Changed2
        {
            get { return tbVKRName_Changed2.Text.Trim(); }
            set { tbVKRName_Changed2.Text = value; }
        }
        private string VKRNameEng_Changed2
        {
            get { return tbVKRNameEng_Changed2.Text.Trim(); }
            set { tbVKRNameEng_Changed2.Text = value; }
        }
        private string VKRName_ChangedDoc2
        {
            get { return tbVKRName_ChangedDoc2.Text.Trim(); }
            set { tbVKRName_ChangedDoc2.Text = value; }
        }
        //НПР
        private string NPRFIO
        {
            get { return tbNPRFIO.Text.Trim(); }
            set { tbNPRFIO.Text = value; }
        }
        private string NPRDegree
        {
            get { return tbNPRDegree.Text.Trim(); }
            set { tbNPRDegree.Text = value; }
        }
        private string NPRRank
        {
            get { return tbNPRRank.Text.Trim(); }
            set { tbNPRRank.Text = value; }
        }
        private string NPRPosition
        {
            get { return tbNPRPosition.Text.Trim(); }
            set { tbNPRPosition.Text = value; }
        }
        private string NPRUNP
        {
            get { return tbNPRUNP.Text.Trim(); }
            set { tbNPRUNP.Text = value; }
        }
        private string NPRChair
        {
            get { return tbNPRChair.Text.Trim(); }
            set { tbNPRChair.Text = value; }
        }
        private string NPRAccount
        {
            get { return tbNPRAccount.Text.Trim(); }
            set { tbNPRAccount.Text = value; }
        }
        private string NPRFIOStudent
        {
            get { return tbNPRFIOStudent.Text.Trim(); }
            set { tbNPRFIOStudent.Text = value; }
        }
        private string NPRDegreeStudent
        {
            get { return tbNPRDegreeStudent.Text.Trim(); }
            set { tbNPRDegreeStudent.Text = value; }
        }
        private string NPRRankStudent
        {
            get { return tbNPRRankStudent.Text.Trim(); }
            set { tbNPRRankStudent.Text = value; }
        }
        private string NPRPositionStudent
        {
            get { return tbNPRPositionStudent.Text.Trim(); }
            set { tbNPRPositionStudent.Text = value; }
        }
        private string NPRUNPStudent
        {
            get { return tbNPRUNPStudent.Text.Trim(); }
            set { tbNPRUNPStudent.Text = value; }
        }
        private string NPRChairStudent
        {
            get { return tbNPRChairStudent.Text.Trim(); }
            set { tbNPRChairStudent.Text = value; }
        }
        private string NPRAccountStudent
        {
            get { return tbNPRAccountStudent.Text.Trim(); }
            set { tbNPRAccountStudent.Text = value; }
        }
        private string NPRFIOFinal
        {
            get { return tbNPRFIOFinal.Text.Trim(); }
            set { tbNPRFIOFinal.Text = value; }
        }
        private string NPRDegreeFinal
        {
            get { return tbNPRDegreeFinal.Text.Trim(); }
            set { tbNPRDegreeFinal.Text = value; }
        }
        private string NPRRankFinal
        {
            get { return tbNPRRankFinal.Text.Trim(); }
            set { tbNPRRankFinal.Text = value; }
        }
        private string NPRPositionFinal
        {
            get { return tbNPRPositionFinal.Text.Trim(); }
            set { tbNPRPositionFinal.Text = value; }
        }
        private string NPRUNPFinal
        {
            get { return tbNPRUNPFinal.Text.Trim(); }
            set { tbNPRUNPFinal.Text = value; }
        }
        private string NPRChairFinal
        {
            get { return tbNPRChairFinal.Text.Trim(); }
            set { tbNPRChairFinal.Text = value; }
        }
        private string NPRAccountFinal
        {
            get { return tbNPRAccountFinal.Text.Trim(); }
            set { tbNPRAccountFinal.Text = value; }
        }
        private bool NPRSet
        {
            get { return chbNPRSet.Checked; }
            set { chbNPRSet.Checked = value; }
        }
        private bool NPRStudentSet
        {
            get { return chbNPRStudentSet.Checked; }
            set { chbNPRStudentSet.Checked = value; }
        }
        private bool NPRFinalSet
        {
            get { return chbNPRFinalSet.Checked; }
            set { chbNPRFinalSet.Checked = value; }
        }
        //НР измененный
        private string NPR_Changed_Persnum
        {
            get { return tbNPR_Changed_Persnum.Text.Trim(); }
            set { tbNPR_Changed_Persnum.Text = value; }
        }
        private string NPR_Changed_Tabnum
        {
            get { return tbNPR_Changed_Tabnum.Text.Trim(); }
            set { tbNPR_Changed_Tabnum.Text = value; }
        }
        private string NPR_FIO_Changed
        {
            get { return tbNPR_FIO_Changed.Text.Trim(); }
            set { tbNPR_FIO_Changed.Text = value; }
        }
        private string NPR_Degree_Changed
        {
            get { return tbNPR_Degree_Changed.Text.Trim(); }
            set { tbNPR_Degree_Changed.Text = value; }
        }
        private string NPR_Rank_Changed
        {
            get { return tbNPR_Rank_Changed.Text.Trim(); }
            set { tbNPR_Rank_Changed.Text = value; }
        }
        private string NPR_Position_Changed
        {
            get { return tbNPR_Position_Changed.Text.Trim(); }
            set { tbNPR_Position_Changed.Text = value; }
        }
        private string NPR_Faculty_Changed
        {
            get { return tbNPR_Faculty_Changed.Text.Trim(); }
            set { tbNPR_Faculty_Changed.Text = value; }
        }
        private string NPR_Chair_Changed
        {
            get { return tbNPR_Chair_Changed.Text.Trim(); }
            set { tbNPR_Chair_Changed.Text = value; }
        }
        private string NPR_Account_Changed
        {
            get { return tbNPR_Account_Changed.Text.Trim(); }
            set { tbNPR_Account_Changed.Text = value; }
        }
        //НР 2-е изменение
        private string NPR_Changed_Persnum2
        {
            get { return tbNPR_Changed_Persnum2.Text.Trim(); }
            set { tbNPR_Changed_Persnum2.Text = value; }
        }
        private string NPR_Changed_Tabnum2
        {
            get { return tbNPR_Changed_Tabnum2.Text.Trim(); }
            set { tbNPR_Changed_Tabnum2.Text = value; }
        }
        private string NPR_FIO_Changed2
        {
            get { return tbNPR_FIO_Changed2.Text.Trim(); }
            set { tbNPR_FIO_Changed2.Text = value; }
        }
        private string NPR_Degree_Changed2
        {
            get { return tbNPR_Degree_Changed2.Text.Trim(); }
            set { tbNPR_Degree_Changed2.Text = value; }
        }
        private string NPR_Rank_Changed2
        {
            get { return tbNPR_Rank_Changed2.Text.Trim(); }
            set { tbNPR_Rank_Changed2.Text = value; }
        }
        private string NPR_Position_Changed2
        {
            get { return tbNPR_Position_Changed2.Text.Trim(); }
            set { tbNPR_Position_Changed2.Text = value; }
        }
        private string NPR_Faculty_Changed2
        {
            get { return tbNPR_Faculty_Changed2.Text.Trim(); }
            set { tbNPR_Faculty_Changed2.Text = value; }
        }
        private string NPR_Chair_Changed2
        {
            get { return tbNPR_Chair_Changed2.Text.Trim(); }
            set { tbNPR_Chair_Changed2.Text = value; }
        }
        private string NPR_Account_Changed2
        {
            get { return tbNPR_Account_Changed2.Text.Trim(); }
            set { tbNPR_Account_Changed2.Text = value; }
        }
        //Организации
        private bool CloseOrg
        {
            get { return chbCloseOrg.Checked; }
            set { chbCloseOrg.Checked = value; }
        }
        public int? OrganizationId
        {
            get { return ComboServ.GetComboIdInt(cbOrganization); }
            set { ComboServ.SetComboId(cbOrganization, value); }
        }
        public int? OrganizationId2
        {
            get { return ComboServ.GetComboIdInt(cbOrganization2); }
            set { ComboServ.SetComboId(cbOrganization2, value); }
        }
        public int? OrganizationId3
        {
            get { return ComboServ.GetComboIdInt(cbOrganization3); }
            set { ComboServ.SetComboId(cbOrganization3, value); }
        }
        public string OrgName
        {
            get { return tbOrgName.Text.Trim(); }
            set { tbOrgName.Text = value; }
        }
        public string OrgName2
        {
            get { return tbOrgName2.Text.Trim(); }
            set { tbOrgName2.Text = value; }
        }
        public string OrgName3
        {
            get { return tbOrgName3.Text.Trim(); }
            set { tbOrgName3.Text = value; }
        }
        private string OrgDocument
        {
            get { return tbOrgDocument.Text.Trim(); }
            set { tbOrgDocument.Text = value; }
        }
        private string OrgDocument2
        {
            get { return tbOrgDocument2.Text.Trim(); }
            set { tbOrgDocument2.Text = value; }
        }
        private string OrgDocument3
        {
            get { return tbOrgDocument3.Text.Trim(); }
            set { tbOrgDocument3.Text = value; }
        }
        //Организации - изменение
        private int? OrganizationId_Changed2
        {
            get { return ComboServ.GetComboIdInt(cbOrganization_Changed2); }
            set { ComboServ.SetComboId(cbOrganization_Changed2, value); }
        }
        private string OrgName_Changed2
        {
            get { return tbOrgName_Changed2.Text.Trim(); }
            set { tbOrgName_Changed2.Text = value; }
        }
        private string OrgDocument_Changed2
        {
            get { return tbOrgDocument_Changed2.Text.Trim(); }
            set { tbOrgDocument_Changed2.Text = value; }
        }
        private bool OrganizationDop_Changed2
        {
            get { return chbOrganizationDop_Changed2.Checked; }
            set { chbOrganizationDop_Changed2.Checked = value; }
        }
        private bool OrganizationNotAgreed_Changed2
        {
            get { return chbOrganizationNotAgreed_Changed2.Checked; }
            set { chbOrganizationNotAgreed_Changed2.Checked = value; }
        }

        //Рецензенты СПбГУ
        private bool CloseReview
        {
            get { return chbCloseReview.Checked; }
            set { chbCloseReview.Checked = value; }
        }
        
        private string ReviewPersnumNPR
        {
            get { return tbReviewPersnumNPR.Text.Trim(); }
            set { tbReviewPersnumNPR.Text = value; }
        }
        private string ReviewTabnumNPR
        {
            get { return tbReviewTabnumNPR.Text.Trim(); }
            set { tbReviewTabnumNPR.Text = value; }
        }

        private string ReviewFIONPR
        {
            get { return tbReviewFIONPR.Text.Trim(); }
            set { tbReviewFIONPR.Text = value; }
        }
        private string ReviewDegreeNPR
        {
            get { return tbReviewDegreeNPR.Text.Trim(); }
            set { tbReviewDegreeNPR.Text = value; }
        }
        private string ReviewRankNPR
        {
            get { return tbReviewRankNPR.Text.Trim(); }
            set { tbReviewRankNPR.Text = value; }
        }
        private string ReviewPositionNPR
        {
            get { return tbReviewPositionNPR.Text.Trim(); }
            set { tbReviewPositionNPR.Text = value; }
        }
        private string ReviewUNPNPR
        {
            get { return tbReviewUNPNPR.Text.Trim(); }
            set { tbReviewUNPNPR.Text = value; }
        }
        private string ReviewChairNPR
        {
            get { return tbReviewChairNPR.Text.Trim(); }
            set { tbReviewChairNPR.Text = value; }
        }
        private string ReviewAccountNPR
        {
            get { return tbReviewAccountNPR.Text.Trim(); }
            set { tbReviewAccountNPR.Text = value; }
        }

        private string ReviewPersnumNPR2
        {
            get { return tbReviewPersnumNPR2.Text.Trim(); }
            set { tbReviewPersnumNPR2.Text = value; }
        }
        private string ReviewTabnumNPR2
        {
            get { return tbReviewTabnumNPR2.Text.Trim(); }
            set { tbReviewTabnumNPR2.Text = value; }
        }

        private string ReviewFIONPR2
        {
            get { return tbReviewFIONPR2.Text.Trim(); }
            set { tbReviewFIONPR2.Text = value; }
        }
        private string ReviewDegreeNPR2
        {
            get { return tbReviewDegreeNPR2.Text.Trim(); }
            set { tbReviewDegreeNPR2.Text = value; }
        }
        private string ReviewRankNPR2
        {
            get { return tbReviewRankNPR2.Text.Trim(); }
            set { tbReviewRankNPR2.Text = value; }
        }
        private string ReviewPositionNPR2
        {
            get { return tbReviewPositionNPR2.Text.Trim(); }
            set { tbReviewPositionNPR2.Text = value; }
        }
        private string ReviewUNPNPR2
        {
            get { return tbReviewUNPNPR2.Text.Trim(); }
            set { tbReviewUNPNPR2.Text = value; }
        }
        private string ReviewChairNPR2
        {
            get { return tbReviewChairNPR2.Text.Trim(); }
            set { tbReviewChairNPR2.Text = value; }
        }
        private string ReviewAccountNPR2
        {
            get { return tbReviewAccountNPR2.Text.Trim(); }
            set { tbReviewAccountNPR2.Text = value; }
        }

        //Рецензенты СПбГУ измененные
        private string ReviewPersnumNPR_Changed2
        {
            get { return tbReviewPersnumNPR_Changed2.Text.Trim(); }
            set { tbReviewPersnumNPR_Changed2.Text = value; }
        }
        private string ReviewTabnumNPR_Changed2
        {
            get { return tbReviewTabnumNPR_Changed2.Text.Trim(); }
            set { tbReviewTabnumNPR_Changed2.Text = value; }
        }

        private string ReviewFIONPR_Changed2
        {
            get { return tbReviewFIONPR_Changed2.Text.Trim(); }
            set { tbReviewFIONPR_Changed2.Text = value; }
        }
        private string ReviewDegreeNPR_Changed2
        {
            get { return tbReviewDegreeNPR_Changed2.Text.Trim(); }
            set { tbReviewDegreeNPR_Changed2.Text = value; }
        }
        private string ReviewRankNPR_Changed2
        {
            get { return tbReviewRankNPR_Changed2.Text.Trim(); }
            set { tbReviewRankNPR_Changed2.Text = value; }
        }
        private string ReviewPositionNPR_Changed2
        {
            get { return tbReviewPositionNPR_Changed2.Text.Trim(); }
            set { tbReviewPositionNPR_Changed2.Text = value; }
        }
        private string ReviewUNPNPR_Changed2
        {
            get { return tbReviewUNPNPR_Changed2.Text.Trim(); }
            set { tbReviewUNPNPR_Changed2.Text = value; }
        }
        private string ReviewChairNPR_Changed2
        {
            get { return tbReviewChairNPR_Changed2.Text.Trim(); }
            set { tbReviewChairNPR_Changed2.Text = value; }
        }
        private string ReviewAccountNPR_Changed2
        {
            get { return tbReviewAccountNPR_Changed2.Text.Trim(); }
            set { tbReviewAccountNPR_Changed2.Text = value; }
        }

        private string ReviewPersnumNPR2_Changed2
        {
            get { return tbReviewPersnumNPR2_Changed2.Text.Trim(); }
            set { tbReviewPersnumNPR2_Changed2.Text = value; }
        }
        private string ReviewTabnumNPR2_Changed2
        {
            get { return tbReviewTabnumNPR2_Changed2.Text.Trim(); }
            set { tbReviewTabnumNPR2_Changed2.Text = value; }
        }

        private string ReviewFIONPR2_Changed2
        {
            get { return tbReviewFIONPR2_Changed2.Text.Trim(); }
            set { tbReviewFIONPR2_Changed2.Text = value; }
        }
        private string ReviewDegreeNPR2_Changed2
        {
            get { return tbReviewDegreeNPR2_Changed2.Text.Trim(); }
            set { tbReviewDegreeNPR2_Changed2.Text = value; }
        }
        private string ReviewRankNPR2_Changed2
        {
            get { return tbReviewRankNPR2_Changed2.Text.Trim(); }
            set { tbReviewRankNPR2_Changed2.Text = value; }
        }
        private string ReviewPositionNPR2_Changed2
        {
            get { return tbReviewPositionNPR2_Changed2.Text.Trim(); }
            set { tbReviewPositionNPR2_Changed2.Text = value; }
        }
        private string ReviewUNPNPR2_Changed2
        {
            get { return tbReviewUNPNPR2_Changed2.Text.Trim(); }
            set { tbReviewUNPNPR2_Changed2.Text = value; }
        }
        private string ReviewChairNPR2_Changed2
        {
            get { return tbReviewChairNPR2_Changed2.Text.Trim(); }
            set { tbReviewChairNPR2_Changed2.Text = value; }
        }
        private string ReviewAccountNPR2_Changed2
        {
            get { return tbReviewAccountNPR2_Changed2.Text.Trim(); }
            set { tbReviewAccountNPR2_Changed2.Text = value; }
        }

        //Рецензенты Партнер
        public int? PersonId
        {
            get { return ComboServ.GetComboIdInt(cbPerson); }
            set { ComboServ.SetComboId(cbPerson, value); }
        }
        public int? PersonId2
        {
            get { return ComboServ.GetComboIdInt(cbPerson2); }
            set { ComboServ.SetComboId(cbPerson2, value); }
        }

        private string ReviewFIOPartner
        {
            get { return tbReviewFIOPartner.Text.Trim(); }
            set { tbReviewFIOPartner.Text = value; }
        }
        private string ReviewDegreePartner
        {
            get { return tbReviewDegreePartner.Text.Trim(); }
            set { tbReviewDegreePartner.Text = value; }
        }
        private string ReviewRankPartner
        {
            get { return tbReviewRankPartner.Text.Trim(); }
            set { tbReviewRankPartner.Text = value; }
        }
        private string ReviewPositionPartner
        {
            get { return tbReviewPositionPartner.Text.Trim(); }
            set { tbReviewPositionPartner.Text = value; }
        }
        private string ReviewOrgPartner
        {
            get { return tbReviewOrgPartner.Text.Trim(); }
            set { tbReviewOrgPartner.Text = value; }
        }
        private string ReviewSubdivisionPartner
        {
            get { return tbReviewSubdivisionPartner.Text.Trim(); }
            set { tbReviewSubdivisionPartner.Text = value; }
        }
        private string ReviewAccountPartner
        {
            get { return tbReviewAccountPartner.Text.Trim(); }
            set { tbReviewAccountPartner.Text = value; }
        }
        //поле объединенное OrgPosSubdiv
        private string ReviewOrgPosPartner
        {
            get { return tbReviewOrgPosPartner.Text.Trim(); }
            set { tbReviewOrgPosPartner.Text = value; }
        }

        private string ReviewFIOPartner2
        {
            get { return tbReviewFIOPartner2.Text.Trim(); }
            set { tbReviewFIOPartner2.Text = value; }
        }
        private string ReviewDegreePartner2
        {
            get { return tbReviewDegreePartner2.Text.Trim(); }
            set { tbReviewDegreePartner2.Text = value; }
        }
        private string ReviewRankPartner2
        {
            get { return tbReviewRankPartner2.Text.Trim(); }
            set { tbReviewRankPartner2.Text = value; }
        }
        private string ReviewPositionPartner2
        {
            get { return tbReviewPositionPartner2.Text.Trim(); }
            set { tbReviewPositionPartner2.Text = value; }
        }
        private string ReviewOrgPartner2
        {
            get { return tbReviewOrgPartner2.Text.Trim(); }
            set { tbReviewOrgPartner2.Text = value; }
        }
        private string ReviewSubdivisionPartner2
        {
            get { return tbReviewSubdivisionPartner2.Text.Trim(); }
            set { tbReviewSubdivisionPartner2.Text = value; }
        }
        private string ReviewAccountPartner2
        {
            get { return tbReviewAccountPartner2.Text.Trim(); }
            set { tbReviewAccountPartner2.Text = value; }
        }
        //поле объединенное OrgPosSubdiv
        private string ReviewOrgPosPartner2
        {
            get { return tbReviewOrgPosPartner2.Text.Trim(); }
            set { tbReviewOrgPosPartner2.Text = value; }
        }

        //Рецензенты Партнер измененные
        public int? PersonId_Changed2
        {
            get { return ComboServ.GetComboIdInt(cbPerson_Changed2); }
            set { ComboServ.SetComboId(cbPerson_Changed2, value); }
        }
        public int? PersonId2_Changed2
        {
            get { return ComboServ.GetComboIdInt(cbPerson2_Changed2); }
            set { ComboServ.SetComboId(cbPerson2_Changed2, value); }
        }

        private string ReviewFIOPartner_Changed2
        {
            get { return tbReviewFIOPartner_Changed2.Text.Trim(); }
            set { tbReviewFIOPartner_Changed2.Text = value; }
        }
        private string ReviewDegreePartner_Changed2
        {
            get { return tbReviewDegreePartner_Changed2.Text.Trim(); }
            set { tbReviewDegreePartner_Changed2.Text = value; }
        }
        private string ReviewRankPartner_Changed2
        {
            get { return tbReviewRankPartner_Changed2.Text.Trim(); }
            set { tbReviewRankPartner_Changed2.Text = value; }
        }
        private string ReviewPositionPartner_Changed2
        {
            get { return tbReviewPositionPartner_Changed2.Text.Trim(); }
            set { tbReviewPositionPartner_Changed2.Text = value; }
        }
        private string ReviewOrgPartner_Changed2
        {
            get { return tbReviewOrgPartner_Changed2.Text.Trim(); }
            set { tbReviewOrgPartner_Changed2.Text = value; }
        }
        private string ReviewSubdivisionPartner_Changed2
        {
            get { return tbReviewSubdivisionPartner_Changed2.Text.Trim(); }
            set { tbReviewSubdivisionPartner_Changed2.Text = value; }
        }
        private string ReviewAccountPartner_Changed2
        {
            get { return tbReviewAccountPartner_Changed2.Text.Trim(); }
            set { tbReviewAccountPartner_Changed2.Text = value; }
        }
        //поле объединенное OrgPosSubdiv
        private string ReviewOrgPosPartner_Changed2
        {
            get { return tbReviewOrgPosPartner_Changed2.Text.Trim(); }
            set { tbReviewOrgPosPartner_Changed2.Text = value; }
        }

        private string ReviewFIOPartner2_Changed2
        {
            get { return tbReviewFIOPartner2_Changed2.Text.Trim(); }
            set { tbReviewFIOPartner2_Changed2.Text = value; }
        }
        private string ReviewDegreePartner2_Changed2
        {
            get { return tbReviewDegreePartner2_Changed2.Text.Trim(); }
            set { tbReviewDegreePartner2_Changed2.Text = value; }
        }
        private string ReviewRankPartner2_Changed2
        {
            get { return tbReviewRankPartner2_Changed2.Text.Trim(); }
            set { tbReviewRankPartner2_Changed2.Text = value; }
        }
        private string ReviewPositionPartner2_Changed2
        {
            get { return tbReviewPositionPartner2_Changed2.Text.Trim(); }
            set { tbReviewPositionPartner2_Changed2.Text = value; }
        }
        private string ReviewOrgPartner2_Changed2
        {
            get { return tbReviewOrgPartner2_Changed2.Text.Trim(); }
            set { tbReviewOrgPartner2_Changed2.Text = value; }
        }
        private string ReviewSubdivisionPartner2_Changed2
        {
            get { return tbReviewSubdivisionPartner2_Changed2.Text.Trim(); }
            set { tbReviewSubdivisionPartner2_Changed2.Text = value; }
        }
        private string ReviewAccountPartner2_Changed2
        {
            get { return tbReviewAccountPartner2_Changed2.Text.Trim(); }
            set { tbReviewAccountPartner2_Changed2.Text = value; }
        }
        //поле объединенное OrgPosSubdiv
        private string ReviewOrgPosPartner2_Changed2
        {
            get { return tbReviewOrgPosPartner2_Changed2.Text.Trim(); }
            set { tbReviewOrgPosPartner2_Changed2.Text = value; }
        }
        //Отмена всех предыдущих рецензентов
        private bool Review_NotUsePrevious_Changed2
        {
            get { return chbReview_NotUsePrevious_Changed2.Checked; }
            set { chbReview_NotUsePrevious_Changed2.Checked = value; }
        }

        #endregion

        private string OrderNumber
        {
            get { return tbOrderNumber.Text.Trim(); }
            set { tbOrderNumber.Text = value; }
        }
        private string OrderDate
        {
            get { return tbOrderDate.Text.Trim(); }
            set { tbOrderDate.Text = value; }
        }
        private string OrderNumberReview
        {
            get { return tbOrderNumberReview.Text.Trim(); }
            set { tbOrderNumberReview.Text = value; }
        }
        private string OrderDateReview
        {
            get { return tbOrderDateReview.Text.Trim(); }
            set { tbOrderDateReview.Text = value; }
        }

        private int? _Id
        {
            get;
            set;
        }
        public int VKRThemesStudentCardId
        {
            get;
            set;
        }
        private bool VKRNameSetEdit = false;
        private bool VKRNameEngSetEdit = false;
        private bool VKRNameStudentSetEdit = false;
        private bool VKRNameEngStudentSetEdit = false;
        private bool NPRSetEdit = false;
        private bool NPRStudentSetEdit = false;
        
        UpdateIntHandler _hndl;

        public VKRThemesStudentCard(int id, UpdateIntHandler _hdl)
        {
            InitializeComponent();
            _Id = id;
            VKRThemesStudentCardId = id;
            _hndl = _hdl;
            SetAccessRight();
            this.MdiParent = Util.mainform;
            FillCombo();
            FillCard();
            FillData();
        }
        private void SetAccessRight()
        {
            if (Util.IsSuperUser())
            {
                btnChangeReviewOrderNum.Visible = true;
                btnSaveReviewOrderNum.Visible = true;
            }
        }
        private void FillCombo()
        {
            FillComboOrg();
            FillComboOrg2();
            FillComboOrg3();
            FillComboOrgChanged2();
            FillComboPerson();
            FillComboPerson2();
            FillComboPersonChanged2();
            FillComboPerson2Changed2();
        }
        private void FillComboOrg()
        {
            ComboServ.FillCombo(cbOrganization, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), Id) AS Id, 
                (CASE  WHEN [MiddleName] is NULL THEN Organization.[Name] WHEN [MiddleName] = '' THEN Organization.[Name] ELSE [MiddleName] END) AS Name
                from dbo.Organization order by Name"), true, false);
        }
        private void FillComboOrg2()
        {
            ComboServ.FillCombo(cbOrganization2, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), Id) AS Id, 
                (CASE  WHEN [MiddleName] is NULL THEN Organization.[Name] WHEN [MiddleName] = '' THEN Organization.[Name] ELSE [MiddleName] END) AS Name
                from dbo.Organization order by Name"), true, false);
        }
        private void FillComboOrg3()
        {
            ComboServ.FillCombo(cbOrganization3, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), Id) AS Id, 
                (CASE  WHEN [MiddleName] is NULL THEN Organization.[Name] WHEN [MiddleName] = '' THEN Organization.[Name] ELSE [MiddleName] END) AS Name
                from dbo.Organization order by Name"), true, false);
        }
        private void FillComboOrgChanged2()
        {
            ComboServ.FillCombo(cbOrganization_Changed2, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), Id) AS Id, 
                (CASE  WHEN [MiddleName] is NULL THEN Organization.[Name] WHEN [MiddleName] = '' THEN Organization.[Name] ELSE [MiddleName] END) AS Name
                from dbo.Organization order by Name"), true, false);
        }
        private void FillComboPerson()
        {
//            ComboServ.FillCombo(cbPerson, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), pp.Id) AS Id, 
//                (pp.Name + CASE WHEN d.Acronym is NULL THEN '' ELSE ',   ' + d.Acronym END  + CASE WHEN pop.OrgPosition is NULL THEN '' ELSE ',   ' + pop.OrgPosition END) as Name
//                from dbo.PartnerPerson pp left outer join dbo.Degree d on pp.DegreeId = d.Id left outer join dbo.PersonOrgPosition pop on pp.Id = pop.PartnerPersonId order by Name"), true, false);
            ComboServ.FillCombo(cbPerson, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), pp.Id) AS Id, 
                (pp.Name + CASE WHEN d.Acronym is NULL THEN '' ELSE ',   ' + d.Acronym END  + CASE WHEN pop.OrgName is NULL THEN '' ELSE ',   ' + pop.OrgName END) as Name
                from dbo.PartnerPerson pp left outer join dbo.Degree d on pp.DegreeId = d.Id left outer join dbo.ECD_OrganizationPersonOrgName pop on pp.Id = pop.PartnerPersonId order by Name"), true, false);
        }
        private void FillComboPerson2()
        {
//            ComboServ.FillCombo(cbPerson2, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), pp.Id) AS Id, 
//                (pp.Name + CASE WHEN d.Acronym is NULL THEN '' ELSE ',   ' + d.Acronym END  + CASE WHEN pop.OrgPosition is NULL THEN '' ELSE ',   ' + pop.OrgPosition END) as Name
//                from dbo.PartnerPerson pp left outer join dbo.Degree d on pp.DegreeId = d.Id left outer join dbo.PersonOrgPosition pop on pp.Id = pop.PartnerPersonId order by Name"), true, false);
            ComboServ.FillCombo(cbPerson2, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), pp.Id) AS Id, 
                (pp.Name + CASE WHEN d.Acronym is NULL THEN '' ELSE ',   ' + d.Acronym END  + CASE WHEN pop.OrgName is NULL THEN '' ELSE ',   ' + pop.OrgName END) as Name
                from dbo.PartnerPerson pp left outer join dbo.Degree d on pp.DegreeId = d.Id left outer join dbo.ECD_OrganizationPersonOrgName pop on pp.Id = pop.PartnerPersonId order by Name"), true, false);
        }
        private void FillComboPersonChanged2()
        {
            ComboServ.FillCombo(cbPerson_Changed2, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), pp.Id) AS Id, 
                (pp.Name + CASE WHEN d.Acronym is NULL THEN '' ELSE ',   ' + d.Acronym END  + CASE WHEN pop.OrgName is NULL THEN '' ELSE ',   ' + pop.OrgName END) as Name
                from dbo.PartnerPerson pp left outer join dbo.Degree d on pp.DegreeId = d.Id left outer join dbo.ECD_OrganizationPersonOrgName pop on pp.Id = pop.PartnerPersonId order by Name"), true, false);
        }
        private void FillComboPerson2Changed2()
        {
            ComboServ.FillCombo(cbPerson2_Changed2, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), pp.Id) AS Id, 
                (pp.Name + CASE WHEN d.Acronym is NULL THEN '' ELSE ',   ' + d.Acronym END  + CASE WHEN pop.OrgName is NULL THEN '' ELSE ',   ' + pop.OrgName END) as Name
                from dbo.PartnerPerson pp left outer join dbo.Degree d on pp.DegreeId = d.Id left outer join dbo.ECD_OrganizationPersonOrgName pop on pp.Id = pop.PartnerPersonId order by Name"), true, false);
        }

        private void FillCard()
        {
            try
            {
                if (_Id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var stud = (from x in context.VKR_ThemesStudentOrder
                                //join op in context.ObrazProgram on x.ObrazProgramId equals op.Id into _op
                                //from op in _op.DefaultIfEmpty()
                                //join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id into _lp
                                //from lp in _lp.DefaultIfEmpty() 
                                //join sl in context.StudyLevel on lp.StudyLevelId equals sl.Id
                                where x.Id == _Id
                                select x).First();
                    FIO = stud.FIO;
                    Account = stud.Accout;
                    OP = stud.ObrazProgram.Number + "  " + stud.ObrazProgram.Name;
                    OPCrypt = stud.ObrazProgramCrypt;
                    StudyLevel = stud.LicenseProgram.StudyLevel.Name;

                    VKRName = stud.VKRName;
                    VKRNameEng = stud.VKRNameEng;
                    VKRNameStudent = stud.VKRName_Student;
                    VKRNameEngStudent = stud.VKRNameEng_Student;
                    VKRNameFinal = stud.VKRName_Final;
                    VKRNameEngFinal = stud.VKRNameEng_Final;
                    Priority = stud.Priority;
                    VKRNameSet = (stud.VKRName_set.HasValue) ? (bool)stud.VKRName_set : false;
                    VKRNameEngSet = (stud.VKRNameEng_set.HasValue) ? (bool)stud.VKRNameEng_set : false;
                    VKRNameStudentSet = (stud.VKRName_Student_set.HasValue) ? (bool)stud.VKRName_Student_set : false;
                    VKRNameEngStudentSet = (stud.VKRNameEng_Student_set.HasValue) ? (bool)stud.VKRNameEng_Student_set : false;
                    VKRNameFinalSet = (stud.VKRName_Final_set.HasValue) ? (bool)stud.VKRName_Final_set : false ;
                    VKRNameEngFinalSet = (stud.VKRNameEng_Final_set.HasValue) ? (bool)stud.VKRNameEng_Final_set : false;

                    NPRFIO = stud.NPR_FIO;
                    NPRDegree = stud.NPR_Degree;
                    NPRRank = stud.NPR_Rank;
                    NPRPosition = stud.NPR_Position;
                    NPRUNP = stud.NPR_Faculty;
                    NPRChair = stud.NPR_Chair;
                    NPRAccount = stud.NPR_Account;
                    NPRSet = (stud.NPR_set.HasValue) ? (bool)stud.NPR_set : false;

                    NPRFIOStudent = stud.NPR_FIO_Student;
                    NPRDegreeStudent = stud.NPR_Degree_Student;
                    NPRRankStudent = stud.NPR_Rank_Student;
                    NPRPositionStudent = stud.NPR_Position_Student;
                    NPRUNPStudent = stud.NPR_Faculty_Student;
                    NPRChairStudent = stud.NPR_Chair_Student;
                    NPRAccountStudent = stud.NPR_Account_Student;
                    NPRStudentSet = (stud.NPR_Student_set.HasValue) ? (bool)stud.NPR_Student_set : false;

                    NPRFIOFinal = stud.NPR_FIO_Final;
                    NPRDegreeFinal = stud.NPR_Degree_Final;
                    NPRRankFinal = stud.NPR_Rank_Final;
                    NPRPositionFinal = stud.NPR_Position_Final;
                    NPRUNPFinal = stud.NPR_Faculty_Final;
                    NPRChairFinal = stud.NPR_Chair_Final;
                    NPRAccountFinal = stud.NPR_Account_Final;
                    NPRFinalSet = (stud.NPR_Final_set.HasValue) ? (bool)stud.NPR_Final_set : false;

                    OrderNumber = (!String.IsNullOrEmpty(stud.OrderNumber)) ? stud.OrderNumber : "";
                    OrderDate = (stud.OrderDate.HasValue) ? stud.OrderDate.Value.ToString("dd.MM.yyyy") : "";
                    OrderNumberReview = (!String.IsNullOrEmpty(stud.OrderNumberReview)) ? stud.OrderNumberReview : "";
                    OrderDateReview = (stud.OrderDateReview.HasValue) ? stud.OrderDateReview.Value.ToString("dd.MM.yyyy") : "";

                    VKRName_Changed = stud.VKRName_Changed;
                    VKRNameEng_Changed = stud.VKRNameEng_Changed;
                    VKRName_ChangedDoc = stud.VKRName_ChangedDoc;

                    VKRName_Changed2 = stud.VKRName_Changed2;
                    VKRNameEng_Changed2 = stud.VKRNameEng_Changed2;
                    VKRName_ChangedDoc2 = stud.VKRName_ChangedDoc2;

                    NPR_Changed_Persnum = stud.NPR_Changed_Persnum;
                    NPR_Changed_Tabnum = stud.NPR_Changed_Tabnum;
                    NPR_FIO_Changed = stud.NPR_FIO_Changed;
                    NPR_Degree_Changed = stud.NPR_Degree_Changed;
                    NPR_Rank_Changed = stud.NPR_Rank_Changed;
                    NPR_Position_Changed = stud.NPR_Position_Changed;
                    NPR_Faculty_Changed = stud.NPR_Faculty_Changed;
                    NPR_Chair_Changed = stud.NPR_Chair_Changed;
                    NPR_Account_Changed = stud.NPR_Account_Changed;

                    NPR_Changed_Persnum2 = stud.NPR_Changed_Persnum2;
                    NPR_Changed_Tabnum2 = stud.NPR_Changed_Tabnum2;
                    NPR_FIO_Changed2 = stud.NPR_FIO_Changed2;
                    NPR_Degree_Changed2 = stud.NPR_Degree_Changed2;
                    NPR_Rank_Changed2 = stud.NPR_Rank_Changed2;
                    NPR_Position_Changed2 = stud.NPR_Position_Changed2;
                    NPR_Faculty_Changed2 = stud.NPR_Faculty_Changed2;
                    NPR_Chair_Changed2 = stud.NPR_Chair_Changed2;
                    NPR_Account_Changed2 = stud.NPR_Account_Changed2;

                    OrganizationId = stud.OrganizationId;
                    OrgName = stud.OrganizationName;
                    OrgDocument = stud.OrganizationDocument;
                    OrganizationId2 = stud.OrganizationId2;
                    OrgName2 = stud.OrganizationName2;
                    OrgDocument2 = stud.OrganizationDocument2;
                    OrganizationId3 = stud.OrganizationId3;
                    OrgName3 = stud.OrganizationName3;
                    OrgDocument3 = stud.OrganizationDocument3;

                    OrganizationId_Changed2 = stud.OrganizationId_Changed2;
                    OrgName_Changed2 = stud.OrganizationName_Changed2;
                    OrgDocument_Changed2 = stud.OrganizationDocument_Changed2;
                    OrganizationDop_Changed2 = (stud.OrganizationDop_Changed2.HasValue) ? (bool)stud.OrganizationDop_Changed2 : false;
                    OrganizationNotAgreed_Changed2 = (stud.OrganizationNotAgreed_Changed2.HasValue) ? (bool)stud.OrganizationNotAgreed_Changed2 : false;

                    ReviewPersnumNPR = stud.Review_NPR_Persnum;
                    ReviewTabnumNPR = stud.Review_NPR_Tabnum;
                    ReviewFIONPR = stud.Review_NPR_FIO;
                    ReviewDegreeNPR = stud.Review_NPR_Degree;
                    ReviewRankNPR = stud.Review_NPR_Rank;
                    ReviewPositionNPR = stud.Review_NPR_Position;
                    ReviewUNPNPR = stud.Review_NPR_Faculty;
                    ReviewChairNPR = stud.Review_NPR_Chair;
                    ReviewAccountNPR = stud.Review_NPR_Account;

                    ReviewPersnumNPR2 = stud.Review_NPR_Persnum2;
                    ReviewTabnumNPR2 = stud.Review_NPR_Tabnum2;
                    ReviewFIONPR2 = stud.Review_NPR_FIO2;
                    ReviewDegreeNPR2 = stud.Review_NPR_Degree2;
                    ReviewRankNPR2 = stud.Review_NPR_Rank2;
                    ReviewPositionNPR2 = stud.Review_NPR_Position2;
                    ReviewUNPNPR2 = stud.Review_NPR_Faculty2;
                    ReviewChairNPR2 = stud.Review_NPR_Chair2;
                    ReviewAccountNPR2 = stud.Review_NPR_Account2;

                    PersonId = stud.Review_PartnerPersonId;
                    ReviewFIOPartner = stud.Review_PP_FIO;
                    ReviewDegreePartner = stud.Review_PP_Degree;
                    ReviewRankPartner = stud.Review_PP_Rank;
                    //ReviewPositionPartner = stud.Review_PP_Position;
                    //ReviewOrgPartner = stud.Review_PP_Organization;
                    //ReviewSubdivisionPartner = stud.Review_PP_Subdivision;
                    ReviewOrgPosPartner = stud.Review_PP_OrgPosition;
                    ReviewAccountPartner = stud.Review_PP_Account;

                    PersonId2 = stud.Review_PartnerPersonId2;
                    ReviewFIOPartner2 = stud.Review_PP_FIO2;
                    ReviewDegreePartner2 = stud.Review_PP_Degree2;
                    ReviewRankPartner2 = stud.Review_PP_Rank2;
                    //ReviewPositionPartner2 = stud.Review_PP_Position2;
                    //ReviewOrgPartner2 = stud.Review_PP_Organization2;
                    //ReviewSubdivisionPartner2 = stud.Review_PP_Subdivision2;
                    ReviewOrgPosPartner2 = stud.Review_PP_OrgPosition2;
                    ReviewAccountPartner2 = stud.Review_PP_Account2;

                    //измение рецензентов
                    ReviewPersnumNPR_Changed2 = stud.Review_NPR_Persnum_Changed2;
                    ReviewTabnumNPR_Changed2 = stud.Review_NPR_Tabnum_Changed2;
                    ReviewFIONPR_Changed2 = stud.Review_NPR_FIO_Changed2;
                    ReviewDegreeNPR_Changed2 = stud.Review_NPR_Degree_Changed2;
                    ReviewRankNPR_Changed2 = stud.Review_NPR_Rank_Changed2;
                    ReviewPositionNPR_Changed2 = stud.Review_NPR_Position_Changed2;
                    ReviewUNPNPR_Changed2 = stud.Review_NPR_Faculty_Changed2;
                    ReviewChairNPR_Changed2 = stud.Review_NPR_Chair_Changed2;
                    ReviewAccountNPR_Changed2 = stud.Review_NPR_Account_Changed2;

                    ReviewPersnumNPR2_Changed2 = stud.Review_NPR_Persnum2_Changed2;
                    ReviewTabnumNPR2_Changed2 = stud.Review_NPR_Tabnum2_Changed2;
                    ReviewFIONPR2_Changed2 = stud.Review_NPR_FIO2_Changed2;
                    ReviewDegreeNPR2_Changed2 = stud.Review_NPR_Degree2_Changed2;
                    ReviewRankNPR2_Changed2 = stud.Review_NPR_Rank2_Changed2;
                    ReviewPositionNPR2_Changed2 = stud.Review_NPR_Position2_Changed2;
                    ReviewUNPNPR2_Changed2 = stud.Review_NPR_Faculty2_Changed2;
                    ReviewChairNPR2_Changed2 = stud.Review_NPR_Chair2_Changed2;
                    ReviewAccountNPR2_Changed2 = stud.Review_NPR_Account2_Changed2;

                    PersonId_Changed2 = stud.Review_PartnerPersonId_Changed2;
                    ReviewFIOPartner_Changed2 = stud.Review_PP_FIO_Changed2;
                    ReviewDegreePartner_Changed2 = stud.Review_PP_Degree_Changed2;
                    ReviewRankPartner_Changed2 = stud.Review_PP_Rank_Changed2;
                    //ReviewPositionPartner_Changed2 = stud.Review_PP_Position_Changed2;
                    //ReviewOrgPartner_Changed2 = stud.Review_PP_Organization_Changed2;
                    //ReviewSubdivisionPartner_Changed2 = stud.Review_PP_Subdivision_Changed2;
                    ReviewOrgPosPartner_Changed2 = stud.Review_PP_OrgPosition_Changed2;
                    ReviewAccountPartner_Changed2 = stud.Review_PP_Account_Changed2;

                    PersonId2_Changed2 = stud.Review_PartnerPersonId2_Changed2;
                    ReviewFIOPartner2_Changed2 = stud.Review_PP_FIO2_Changed2;
                    ReviewDegreePartner2_Changed2 = stud.Review_PP_Degree2_Changed2;
                    ReviewRankPartner2_Changed2 = stud.Review_PP_Rank2_Changed2;
                    //ReviewPositionPartner2_Changed2 = stud.Review_PP_Position2_Changed2;
                    //ReviewOrgPartner2_Changed2 = stud.Review_PP_Organization2_Changed2;
                    //ReviewSubdivisionPartner2_Changed2 = stud.Review_PP_Subdivision2_Changed2;
                    ReviewOrgPosPartner2_Changed2 = stud.Review_PP_OrgPosition2_Changed2;
                    ReviewAccountPartner2_Changed2 = stud.Review_PP_Account2_Changed2;

                    Review_NotUsePrevious_Changed2 = (stud.Review_NotUsePrevious_Changed2.HasValue) ? (bool)stud.Review_NotUsePrevious_Changed2 : false;

                    //Comment = stud.Comment;
                    try
                    {
                        this.Text = "Карточка ВКР: " + FIO;
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        if (OrderNumber != "")  
                        {
                            //приказ зафиксирован
                            btnSave.Enabled = false;
                            lblFrozen.Visible = true;
                            if (Util.IsSuperUser())
                            {
                                btnSave.Enabled = true;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        if (OrderNumberReview != "")
                        {
                            //приказ по рецензентам зафиксирован
                            btnSave.Enabled = false;
                            lblFrozenReview.Visible = true;
                            btnSaveOrgReviewData.Enabled = false;
                            button1.Enabled = false;
                            if (Util.IsSuperUser())
                            {
                                button1.Enabled = true;
                            }
                            btnThemeNPRChangedSave.Enabled = false;

                            btnThemeNPRChangedSave2.Enabled = true;
                            lblUnFrozenReview.Visible = false;
                        }
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        if (!(bool)stud.IsActive)
                        {
                            lblNotInStudData.Visible = true;

                        }
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        if (OrganizationNotAgreed_Changed2)
                        {
                            //OrganizationId_Changed2 = null;
                            //OrgDocument_Changed2 = "";
                            cbOrganization_Changed2.Enabled = false;
                        }
                        else
                        {
                            cbOrganization_Changed2.Enabled = true;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                VKRNameSetEdit = true;
                VKRNameEngSetEdit = true;
                VKRNameStudentSetEdit = true;
                VKRNameEngStudentSetEdit = true;
                NPRSetEdit = true;
                NPRStudentSetEdit = true;
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void FillData()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudent
                               where x.Student_Account == Account
                               orderby x.Priority
                               select new
                               {
                                   Приоритет = x.Priority,
                                   Тема_ВКР = x.VKRName,
                                   Тема_ВКР_англ = x.VKRNameEng,
                                   Тема_ВКР_студент = x.VKRName_Student,
                                   Тема_ВКР_англ_студент = x.VKRNameEng_Student,

                                   Источник = x.VKRSourceKey,
                                   Документ = x.DocumentNumber,
                                   НПР_Фамилия = x.NPR_LastName,
                                   НПР_Имя = x.NPR_FirstName,
                                   НПР_Отчество = x.NPR_SecondName,
                                   НПР_должность = x.NPR_Position,
                                   НПР_степень = x.NPR_Degree,
                                   НПР_звание = x.NPR_Rank,
                                   НПР_аккаунт = x.NPR_Account,
                                   НПР_кафедра = x.NPR_Chair,
                                   //НПР_УНП = x.NPR_Faculty,
                                   НПР_ФИО_студент = x.NPR_FIO_Student,
                                   НПР_должность_студент = x.NPR_Rank_Student,
                                   НПР_аккаунт_студент = x.NPR_Account_Student,
                                   НПР_кафедра_студент = x.NPR_Chair_Student,
                                   
                                   Направление = x.LicenseProgramName,
                                   Уровень = x.Crypt,
                                   Номер_плана = x.StudyPlanNum,
                                   ООП_наименование = x.ObrazProgramName,
                                   Организация = x.OrganizationName,
                                   Студент_Аккаунт = x.Student_Account,
                                   Студент_Фамилия = x.Student_LastName,
                                   Студент_Имя = x.Student_FirstName,
                                   Студент_Отчество = x.Student_SecondName,
                                   Студент_направление = x.LicenseProgramName_Student,
                                   Студент_курс = x.Course_Student,
                                   Студент_номер_плана = x.StudyPlanNum_Student,
                                   Студент_ООП_наименование = x.ObrazProgramName_Student,
                                   Студент_статус = x.StatusName,
                                   Номер_приказа_по_темам_ВКР = x.OrderNumber,
                                   Дата_приказа_по_темам_ВКР = x.OrderDate,
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSource1.DataSource = dt;
                    dgvData.DataSource = bindingSource1;

                    foreach (DataGridViewColumn col in dgvData.Columns)
                        col.HeaderText = col.Name.Replace("_", " ");
                    try
                    {
                        dgvData.Columns["Тема_ВКР"].Width = 300;
                        dgvData.Columns["Тема_ВКР_англ"].Width = 300;
                        dgvData.Columns["Тема_ВКР_студент"].Width = 300;
                        dgvData.Columns["Тема_ВКР_англ_студент"].Width = 300;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
                return;
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var vkrst = context.VKR_ThemesStudentOrder.Where(x => x.Id == _Id).First();

                    vkrst.VKRName_Final = VKRNameFinal;
                    vkrst.VKRNameEng_Final = VKRNameEngFinal;

                    vkrst.VKRName_set = chbVKRNameSet.Checked ? true : false;
                    vkrst.VKRNameEng_set = chbVKRNameEngSet.Checked ? true : false;
                    vkrst.VKRName_Student_set = chbVKRNameStudentSet.Checked ? true : false;
                    vkrst.VKRNameEng_Student_set = chbVKRNameEngStudentSet.Checked ? true : false;
                    vkrst.VKRName_Final_set = chbVKRNameFinalSet.Checked ? true : false;
                    vkrst.VKRNameEng_Final_set = chbVKRNameEngFinalSet.Checked ? true : false;

                    vkrst.NPR_FIO_Final = NPRFIOFinal;
                    vkrst.NPR_Degree_Final = NPRDegreeFinal;
                    vkrst.NPR_Rank_Final = NPRRankFinal;
                    vkrst.NPR_Position_Final = NPRPositionFinal;
                    vkrst.NPR_Faculty_Final = NPRUNPFinal;
                    vkrst.NPR_Chair_Final = NPRChairFinal;
                    vkrst.NPR_Account_Final = NPRAccountFinal;

                    vkrst.NPR_set = chbNPRSet.Checked ? true : false;
                    vkrst.NPR_Student_set = chbNPRStudentSet.Checked ? true : false;
                    vkrst.NPR_Final_set = chbNPRFinalSet.Checked ? true : false;

                    //vkrst.OrganizationId = OrgId;
                    //vkrst.Comment = Comment;

                    context.SaveChanges();

                    //MessageBox.Show("Данные сохранены", "Сообщение");
                    if (_hndl != null)
                        _hndl(_Id);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить данные...\r\n" + ex.Message + "\r\n" + ex.InnerException.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chbVKRNameSet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (VKRNameSet)
                {
                    VKRNameStudentSet = false;
                    VKRNameFinalSet = false;
                    if (VKRNameSetEdit)
                    {
                        VKRNameFinal = VKRName;
                    }
                    else
                    {
                        VKRNameSetEdit = true;
                    }
                    
                }
            }
            catch (Exception)
            {
            }
        }

        private void chbVKRNameEngSet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (VKRNameEngSet)
                {
                    VKRNameEngStudentSet = false;
                    VKRNameEngFinalSet = false;
                    if (VKRNameEngSetEdit)
                    {
                        VKRNameEngFinal = VKRNameEng;
                    }
                    else
                    {
                        VKRNameEngSetEdit = true;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void chbVKRNameStudentSet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (VKRNameStudentSet)
                {
                    VKRNameSet = false;
                    VKRNameFinalSet = false;
                    if (VKRNameStudentSetEdit)
                    {
                        VKRNameFinal = VKRNameStudent;
                    }
                    else
                    {
                        VKRNameStudentSetEdit = true;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void chbVKRNameEngStudentSet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (VKRNameEngStudentSet)
                {
                    VKRNameEngSet = false;
                    VKRNameEngFinalSet = false;
                    if (VKRNameEngStudentSetEdit)
                    {
                        VKRNameEngFinal = VKRNameEngStudent;
                    }
                    else
                    {
                        VKRNameEngStudentSetEdit = true;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void chbVKRNameFinalSet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (VKRNameFinalSet)
                {
                    VKRNameSet = false;
                    VKRNameStudentSet = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void chbVKRNameEngFinalSet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (VKRNameEngFinalSet)
                {
                    VKRNameEngSet = false;
                    VKRNameEngStudentSet = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void chbNPRSet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (NPRSet)
                {
                    NPRStudentSet = false;
                    NPRFinalSet = false;
                    if (NPRSetEdit)
                    {
                        NPRFIOFinal = NPRFIO;
                        NPRDegreeFinal = NPRDegree;
                        NPRRankFinal = NPRRank;
                        NPRPositionFinal = NPRPosition;
                        NPRUNPFinal = NPRUNP;
                        NPRChairFinal = NPRChair;
                        NPRAccountFinal = NPRAccount;
                    }
                    else
                    {
                        NPRSetEdit = true;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void chbNPRStudentSet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (NPRStudentSet)
                {
                    NPRSet = false;
                    NPRFinalSet = false;
                    if (NPRStudentSetEdit)
                    {
                        NPRFIOFinal = NPRFIOStudent;
                        NPRDegreeFinal = NPRDegreeStudent;
                        NPRRankFinal = NPRRankStudent;
                        NPRPositionFinal = NPRPositionStudent;
                        NPRUNPFinal = NPRUNPStudent;
                        NPRChairFinal = NPRChairStudent;
                        NPRAccountFinal = NPRAccountStudent;
                    }
                    else
                    {
                        NPRStudentSetEdit = true;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void chbNPRFinalSet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (NPRFinalSet)
                {
                    NPRSet = false;
                    NPRStudentSet = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnNPRFinalClear_Click(object sender, EventArgs e)
        {
            try
            {
                NPRFIOFinal = "";
                NPRDegreeFinal = "";
                NPRRankFinal = "";
                NPRPositionFinal = "";
                NPRUNPFinal = "";
                NPRChairFinal = "";
                NPRAccountFinal = "";
            }
            catch (Exception)
            {
            }
        }

        private void cbOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!OrganizationId.HasValue)
                {
                    OrgName = "";
                    return;
                }
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = context.Organization.Where(x => x.Id == (int)OrganizationId).First();
                    //OrgName = lst.Name;
                    OrgName = (!String.IsNullOrEmpty(lst.Name)) ? lst.Name : "";
                }
            }
            catch (Exception)
            {
            }
        }

        private void cbOrganization2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!OrganizationId2.HasValue)
                {
                    OrgName2 = "";
                    return;
                }
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = context.Organization.Where(x => x.Id == (int)OrganizationId2).First();
                    //OrgName2 = lst.Name;
                    OrgName2 = (!String.IsNullOrEmpty(lst.Name)) ? lst.Name : "";
                }
            }
            catch (Exception)
            {
            }
        }

        private void cbOrganization3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!OrganizationId3.HasValue)
                {
                    OrgName3 = "";
                    return;
                }
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = context.Organization.Where(x => x.Id == (int)OrganizationId3).First();
                    //OrgName3 = lst.Name;
                    OrgName3 = (!String.IsNullOrEmpty(lst.Name)) ? lst.Name : "";
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnRefreshOrgList_Click(object sender, EventArgs e)
        {
            int? OrgId = OrganizationId;
            FillComboOrg();
            OrganizationId = OrgId;
        }

        private void btnRefreshOrgList2_Click(object sender, EventArgs e)
        {
            int? OrgId = OrganizationId2;
            FillComboOrg2();
            OrganizationId2 = OrgId;
        }

        private void btnRefreshOrgList3_Click(object sender, EventArgs e)
        {
            int? OrgId = OrganizationId3;
            FillComboOrg3();
            OrganizationId3 = OrgId;
        }

        private void OrgListSetToFound()
        {
            OrgListSetToFound(null);
        }
        private void OrgListSetToFound(int? id)
        {
            OrganizationId = id;
        }
        private void OrgList2SetToFound()
        {
            OrgList2SetToFound(null);
        }
        private void OrgList2SetToFound(int? id)
        {
            OrganizationId2 = id;
        }
        private void OrgList3SetToFound()
        {
            OrgList3SetToFound(null);
        }
        private void OrgList3SetToFound(int? id)
        {
            OrganizationId3 = id;
        }

        private void btnOrgHandBook_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("OrgListToFind"))
                Utilities.FormClose("OrgListToFind");

            new OrgListToFind(new UpdateIntHandler(OrgListSetToFound)).Show();
        }

        private void btnOrgHandBook2_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("OrgListToFind"))
                Utilities.FormClose("OrgListToFind");

            new OrgListToFind(new UpdateIntHandler(OrgList2SetToFound)).Show();
        }

        private void btnOrgHandBook3_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("OrgListToFind"))
                Utilities.FormClose("OrgListToFind");

            new OrgListToFind(new UpdateIntHandler(OrgList3SetToFound)).Show();
        }

        private void btnSaveOrgReviewData_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
                return;
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var vkrst = context.VKR_ThemesStudentOrder.Where(x => x.Id == _Id).First();

                    vkrst.OrganizationId = OrganizationId;
                    vkrst.OrganizationId2 = OrganizationId2;
                    vkrst.OrganizationId3 = OrganizationId3;
                    vkrst.OrganizationName = OrgName;
                    vkrst.OrganizationName2 = OrgName2;
                    vkrst.OrganizationName3 = OrgName3;
                    vkrst.OrganizationDocument = OrgDocument;
                    vkrst.OrganizationDocument2 = OrgDocument2;
                    vkrst.OrganizationDocument3 = OrgDocument3;

                    //vkrst.Comment = Comment;

                    context.SaveChanges();

                    MessageBox.Show("Данные сохранены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (_hndl != null)
                        _hndl(_Id);
                }
                //if (CloseOrg)
                //{
                //    this.Close();    
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить данные...\r\n" + ex.Message + "\r\n" + ex.InnerException.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
                return;
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var vkrst = context.VKR_ThemesStudentOrder.Where(x => x.Id == _Id).First();

                    vkrst.Review_NPR_Persnum = ReviewPersnumNPR;
                    vkrst.Review_NPR_Tabnum = ReviewTabnumNPR;
                    vkrst.Review_NPR_FIO = ReviewFIONPR;
                    vkrst.Review_NPR_Degree = ReviewDegreeNPR;
                    vkrst.Review_NPR_Rank = ReviewRankNPR;
                    vkrst.Review_NPR_Position = ReviewPositionNPR;
                    vkrst.Review_NPR_Faculty = ReviewUNPNPR;
                    vkrst.Review_NPR_Chair = ReviewChairNPR;
                    vkrst.Review_NPR_Account = ReviewAccountNPR;

                    vkrst.Review_NPR_Persnum2 = ReviewPersnumNPR2;
                    vkrst.Review_NPR_Tabnum2 = ReviewTabnumNPR2;
                    vkrst.Review_NPR_FIO2 = ReviewFIONPR2;
                    vkrst.Review_NPR_Degree2 = ReviewDegreeNPR2;
                    vkrst.Review_NPR_Rank2 = ReviewRankNPR2;
                    vkrst.Review_NPR_Position2 = ReviewPositionNPR2;
                    vkrst.Review_NPR_Faculty2 = ReviewUNPNPR2;
                    vkrst.Review_NPR_Chair2 = ReviewChairNPR2;
                    vkrst.Review_NPR_Account2 = ReviewAccountNPR2;

                    vkrst.Review_PartnerPersonId = PersonId;
                    vkrst.Review_PP_FIO = ReviewFIOPartner;
                    vkrst.Review_PP_Degree = ReviewDegreePartner;
                    vkrst.Review_PP_Rank = ReviewRankPartner;
                    //vkrst.Review_PP_Position = ReviewPositionPartner;
                    //vkrst.Review_PP_Organization = ReviewOrgPartner;
                    //vkrst.Review_PP_Subdivision = ReviewSubdivisionPartner;
                    vkrst.Review_PP_OrgPosition = ReviewOrgPosPartner;
                    vkrst.Review_PP_Account = ReviewAccountPartner;

                    vkrst.Review_PartnerPersonId2 = PersonId2;
                    vkrst.Review_PP_FIO2 = ReviewFIOPartner2;
                    vkrst.Review_PP_Degree2 = ReviewDegreePartner2;
                    vkrst.Review_PP_Rank2 = ReviewRankPartner2;
                    //vkrst.Review_PP_Position2 = ReviewPositionPartner2;
                    //vkrst.Review_PP_Organization2 = ReviewOrgPartner2;
                    //vkrst.Review_PP_Subdivision2 = ReviewSubdivisionPartner2;
                    vkrst.Review_PP_OrgPosition2 = ReviewOrgPosPartner2;
                    vkrst.Review_PP_Account2 = ReviewAccountPartner2;

                    //vkrst.Comment = Comment;

                    context.SaveChanges();

                    MessageBox.Show("Данные сохранены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (_hndl != null)
                        _hndl(_Id);
                }
                //if (CloseReview)
                //{
                //    this.Close();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить данные...\r\n" + ex.Message + "\r\n" + ex.InnerException.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ReviewNPRSetToFound()
        {
            ReviewNPRSetToFound(null, null);
        }
        private void ReviewNPRSetToFound(int? id, string persnum)
        {
            try
            {
                //string Persnum = persnum;
                //if (String.IsNullOrEmpty(Persnum))
                //        return;
                string Tabnum = persnum;
                if (String.IsNullOrEmpty(Tabnum))
                    return;

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    //var lst = context.SAP_NPR.Where(x => x.Persnum == Persnum).First();
                    var lst = context.SAP_NPR.Where(x => x.Tabnum == Tabnum).First();

                    ReviewPersnumNPR = ((!String.IsNullOrEmpty(lst.Persnum)) ? lst.Persnum : "");
                    ReviewTabnumNPR = ((!String.IsNullOrEmpty(lst.Tabnum)) ? lst.Tabnum : "");
                    ReviewFIONPR = ((!String.IsNullOrEmpty(lst.Lastname)) ? lst.Lastname : "") + " " + ((!String.IsNullOrEmpty(lst.Name)) ? lst.Name : "") + " " + ((!String.IsNullOrEmpty(lst.Surname)) ? lst.Surname : "");
                    ReviewDegreeNPR = ((!String.IsNullOrEmpty(lst.Degree)) ? lst.Degree : "");
                    ReviewRankNPR = ((!String.IsNullOrEmpty(lst.Titl2)) ? lst.Titl2 : "");
                    ReviewPositionNPR = ((!String.IsNullOrEmpty(lst.Position)) ? lst.Position : "");
                    ReviewUNPNPR = ((!String.IsNullOrEmpty(lst.Faculty)) ? lst.Faculty : "");
                    ReviewChairNPR = ((!String.IsNullOrEmpty(lst.FullName)) ? lst.FullName : "");
                    ReviewAccountNPR = ((!String.IsNullOrEmpty(lst.UsridAd)) ? lst.UsridAd : "");
                }
            }
            catch (Exception)
            {
            }
        }
        private void ReviewNPRSetToFound2()
        {
            ReviewNPRSetToFound2(null, null);
        }
        private void ReviewNPRSetToFound2(int? id, string persnum)
        {
            try
            {
                //string Persnum = persnum;
                //if (String.IsNullOrEmpty(Persnum))
                //    return;
                string Tabnum = persnum;
                if (String.IsNullOrEmpty(Tabnum))
                    return;

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    //var lst = context.SAP_NPR.Where(x => x.Persnum == Persnum).First();
                    var lst = context.SAP_NPR.Where(x => x.Tabnum == Tabnum).First();

                    ReviewPersnumNPR2 = ((!String.IsNullOrEmpty(lst.Persnum)) ? lst.Persnum : "");
                    ReviewTabnumNPR2 = ((!String.IsNullOrEmpty(lst.Tabnum)) ? lst.Tabnum : "");
                    ReviewFIONPR2 = ((!String.IsNullOrEmpty(lst.Lastname)) ? lst.Lastname : "") + " " + ((!String.IsNullOrEmpty(lst.Name)) ? lst.Name : "") + " " + ((!String.IsNullOrEmpty(lst.Surname)) ? lst.Surname : "");
                    ReviewDegreeNPR2 = ((!String.IsNullOrEmpty(lst.Degree)) ? lst.Degree : "");
                    ReviewRankNPR2 = ((!String.IsNullOrEmpty(lst.Titl2)) ? lst.Titl2 : "");
                    ReviewPositionNPR2 = ((!String.IsNullOrEmpty(lst.Position)) ? lst.Position : "");
                    ReviewUNPNPR2 = ((!String.IsNullOrEmpty(lst.Faculty)) ? lst.Faculty : "");
                    ReviewChairNPR2 = ((!String.IsNullOrEmpty(lst.FullName)) ? lst.FullName : "");
                    ReviewAccountNPR2 = ((!String.IsNullOrEmpty(lst.UsridAd)) ? lst.UsridAd : "");
                }
            }
            catch (Exception)
            {
            }
        }
        private void btnFindNPR_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("NPRListToFind"))
                Utilities.FormClose("NPRListToFind");

            new NPRListToFind(new UpdateStringHandler(ReviewNPRSetToFound)).Show();
        }

        private void btnFindNPR2_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("NPRListToFind"))
                Utilities.FormClose("NPRListToFind");

            new NPRListToFind(new UpdateStringHandler(ReviewNPRSetToFound2)).Show();
        }

        private void btnClearReviewNPR_Click(object sender, EventArgs e)
        {
            try
            {
                ReviewPersnumNPR = "";
                ReviewTabnumNPR = "";
                ReviewFIONPR = "";
                ReviewDegreeNPR = "";
                ReviewRankNPR = "";
                ReviewPositionNPR = "";
                ReviewUNPNPR = "";
                ReviewChairNPR = "";
                ReviewAccountNPR = "";
            }
            catch (Exception)
            {
            }
        }

        private void btnClearReviewNPR2_Click(object sender, EventArgs e)
        {
            try
            {
                ReviewPersnumNPR2 = "";
                ReviewTabnumNPR2 = "";
                ReviewFIONPR2 = "";
                ReviewDegreeNPR2 = "";
                ReviewRankNPR2 = "";
                ReviewPositionNPR2 = "";
                ReviewUNPNPR2 = "";
                ReviewChairNPR2 = "";
                ReviewAccountNPR2 = "";
            }
            catch (Exception)
            {
            }
        }

        private void btnReviewOrgCard_Click(object sender, EventArgs e)
        {
            //OrganizationId
            if (!OrganizationId.HasValue)
            {
                MessageBox.Show("Не выбрана организация!", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbOrganization.DroppedDown = true;
                return;
            }
            try
            {
                int Orgid = (int)OrganizationId;
                if (Utilities.OrgCardIsOpened(Orgid))
                    return;
                new CardOrganization(Orgid, null).Show();
            }
            catch
            {
            }
        }

        private void btnReviewOrgCard2_Click(object sender, EventArgs e)
        {
            //OrganizationId2
            if (!OrganizationId2.HasValue)
            {
                MessageBox.Show("Не выбрана организация!", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbOrganization2.DroppedDown = true;
                return;
            }
            try
            {
                int Orgid = (int)OrganizationId2;
                if (Utilities.OrgCardIsOpened(Orgid))
                    return;
                new CardOrganization(Orgid, null).Show();
            }
            catch
            {
            }
        }

        private void btnReviewOrgCard3_Click(object sender, EventArgs e)
        {
            //OrganizationId3
            if (!OrganizationId3.HasValue)
            {
                MessageBox.Show("Не выбрана организация!", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbOrganization3.DroppedDown = true;
                return;
            }
            try
            {
                int Orgid = (int)OrganizationId3;
                if (Utilities.OrgCardIsOpened(Orgid))
                    return;
                new CardOrganization(Orgid, null).Show();
            }
            catch
            {
            }
        }

        private void btnOrgClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCloseReview_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PersonListSetToFound()
        {
            PersonListSetToFound(null);
        }
        private void PersonListSetToFound(int? id)
        {
            if (PersonId == id)
            {
                PersonId = null;
                PersonId = id;
            }
            else
            {
                PersonId = id;
            }
        }
        private void PersonListSetToFound2()
        {
            PersonListSetToFound2(null);
        }
        private void PersonListSetToFound2(int? id)
        {
            if (PersonId2 == id)
            {
                PersonId2 = null;
                PersonId2 = id;
            }
            else
            {
                PersonId2 = id;
            }
        }

        private void BtnFindPartner_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("PersonListToFind"))
                Utilities.FormClose("PersonListToFind");

            new PersonListToFind(new UpdateIntHandler(PersonListSetToFound)).Show();
        }

        private void BtnFindPartner2_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("PersonListToFind"))
                Utilities.FormClose("PersonListToFind");

            new PersonListToFind(new UpdateIntHandler(PersonListSetToFound2)).Show();
        }
        
        private void cbPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!PersonId.HasValue)
                {
                    ReviewFIOPartner = "";
                    ReviewDegreePartner = "";
                    ReviewRankPartner = "";
                    ReviewOrgPosPartner = "";
                    ReviewAccountPartner = "";
                    return;
                }
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = context.PartnerPersonOrgPosition.Where(x => x.Id == (int)PersonId).First();

                    ReviewFIOPartner = (!String.IsNullOrEmpty(lst.Name)) ? lst.Name : "";
                    ReviewDegreePartner = (!String.IsNullOrEmpty(lst.DegreeName)) ? lst.DegreeName : "";
                    ReviewRankPartner = (!String.IsNullOrEmpty(lst.RankName)) ? lst.RankName : "";
                    ReviewOrgPosPartner = (!String.IsNullOrEmpty(lst.OrgPosition)) ? lst.OrgPosition : "";
                    ReviewAccountPartner = (!String.IsNullOrEmpty(lst.Account)) ? lst.Account : "";
                }
            }
            catch (Exception)
            {
            }
        }

        private void cbPerson2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!PersonId2.HasValue)
                {
                    ReviewFIOPartner2 = "";
                    ReviewDegreePartner2 = "";
                    ReviewRankPartner2 = "";
                    ReviewOrgPosPartner2 = "";
                    ReviewAccountPartner2 = "";
                    return;
                }
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = context.PartnerPersonOrgPosition.Where(x => x.Id == (int)PersonId2).First();

                    ReviewFIOPartner2 = (!String.IsNullOrEmpty(lst.Name)) ? lst.Name : "";
                    ReviewDegreePartner2 = (!String.IsNullOrEmpty(lst.DegreeName)) ? lst.DegreeName : "";
                    ReviewRankPartner2 = (!String.IsNullOrEmpty(lst.RankName)) ? lst.RankName : "";
                    ReviewOrgPosPartner2 = (!String.IsNullOrEmpty(lst.OrgPosition)) ? lst.OrgPosition : "";
                    ReviewAccountPartner2 = (!String.IsNullOrEmpty(lst.Account)) ? lst.Account : "";
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnClearReviewPartner_Click(object sender, EventArgs e)
        {
            try
            {
                PersonId = null;
                ReviewFIOPartner = "";
                ReviewDegreePartner = "";
                ReviewRankPartner = "";
                ReviewOrgPosPartner = "";
                ReviewAccountPartner = "";
            }
            catch
            {}
        }

        private void btnClearReviewPartner2_Click(object sender, EventArgs e)
        {
            try
            {
                PersonId2 = null;
                ReviewFIOPartner2 = "";
                ReviewDegreePartner2 = "";
                ReviewRankPartner2 = "";
                ReviewOrgPosPartner2 = "";
                ReviewAccountPartner2 = "";
            }
            catch
            { }
        }

        private void btnReviewPartnerCard_Click(object sender, EventArgs e)
        {
            if (!PersonId.HasValue)
            {
                MessageBox.Show("Не выбран рецензент!", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbPerson.DroppedDown = true;
                return;
            }
            try
            {
                int PersId = (int)PersonId;
                if (Utilities.PersonCardIsOpened(PersId))
                    return;
                new CardPerson(PersId, null).Show();
            }
            catch
            {
            }
        }

        private void btnReviewPartnerCard2_Click(object sender, EventArgs e)
        {
            if (!PersonId2.HasValue)
            {
                MessageBox.Show("Не выбран рецензент!", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbPerson2.DroppedDown = true;
                return;
            }
            try
            {
                int PersId = (int)PersonId2;
                if (Utilities.PersonCardIsOpened(PersId))
                    return;
                new CardPerson(PersId, null).Show();
            }
            catch
            {
            }
        }

        private void btnRefreshReviewList_Click(object sender, EventArgs e)
        {
            int? PersId = PersonId;
            FillComboPerson();
            PersonId = PersId;
        }

        private void btnRefreshReviewList2_Click(object sender, EventArgs e)
        {
            int? PersId = PersonId2;
            FillComboPerson2();
            PersonId2 = PersId;
        }

        private void btnThemeNPRChangedClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCleatNPRChanged_Click(object sender, EventArgs e)
        {
            try
            {
                NPR_Changed_Persnum = "";
                NPR_Changed_Tabnum = "";
                NPR_FIO_Changed = "";
                NPR_Degree_Changed = "";
                NPR_Rank_Changed = "";
                NPR_Position_Changed = "";
                NPR_Faculty_Changed = "";
                NPR_Chair_Changed = "";
                NPR_Account_Changed = "";
            }
            catch (Exception)
            {
            }
        }

        private void btnThemeNPRChangedSave_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
                return;
            if (OrderNumber == "")
            {
                MessageBox.Show("Тема ВКР И НР не утверждались приказом.\n\rДанные необходимо вводить на Вкладке 'Темы ВКР и НР'", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var vkrst = context.VKR_ThemesStudentOrder.Where(x => x.Id == _Id).First();

                    vkrst.VKRName_Changed = VKRName_Changed;
                    vkrst.VKRNameEng_Changed = VKRNameEng_Changed;
                    vkrst.VKRName_ChangedDoc = VKRName_ChangedDoc;

                    vkrst.NPR_Changed_Persnum = NPR_Changed_Persnum;
                    vkrst.NPR_Changed_Tabnum = NPR_Changed_Tabnum;
                    vkrst.NPR_FIO_Changed = NPR_FIO_Changed;
                    vkrst.NPR_Degree_Changed = NPR_Degree_Changed;
                    vkrst.NPR_Rank_Changed = NPR_Rank_Changed;
                    vkrst.NPR_Position_Changed = NPR_Position_Changed;
                    vkrst.NPR_Faculty_Changed = NPR_Faculty_Changed;
                    vkrst.NPR_Chair_Changed = NPR_Chair_Changed;
                    vkrst.NPR_Account_Changed = NPR_Account_Changed;

                    context.SaveChanges();

                    //MessageBox.Show("Данные сохранены", "Сообщение");
                    if (_hndl != null)
                        _hndl(_Id);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить данные...\r\n" + ex.Message + "\r\n" + ex.InnerException.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void NPRChangedSetToFound()
        {
            NPRChangedSetToFound(null, null);
        }
        private void NPRChangedSetToFound(int? id, string persnum)
        {
            try
            {
                //string Persnum = persnum;
                //if (String.IsNullOrEmpty(Persnum))
                //    return;
                string Tabnum = persnum;
                if (String.IsNullOrEmpty(Tabnum))
                    return;

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    //var lst = context.SAP_NPR.Where(x => x.Persnum == Persnum).First();
                    var lst = context.SAP_NPR.Where(x => x.Tabnum == Tabnum).First();

                    NPR_Changed_Persnum = ((!String.IsNullOrEmpty(lst.Persnum)) ? lst.Persnum : "");
                    NPR_Changed_Tabnum = ((!String.IsNullOrEmpty(lst.Tabnum)) ? lst.Tabnum : "");
                    NPR_FIO_Changed = ((!String.IsNullOrEmpty(lst.Lastname)) ? lst.Lastname : "") + " " + ((!String.IsNullOrEmpty(lst.Name)) ? lst.Name : "") + " " + ((!String.IsNullOrEmpty(lst.Surname)) ? lst.Surname : "");
                    NPR_Degree_Changed = ((!String.IsNullOrEmpty(lst.Degree)) ? lst.Degree : "");
                    NPR_Rank_Changed = ((!String.IsNullOrEmpty(lst.Titl2)) ? lst.Titl2 : "");
                    NPR_Position_Changed = ((!String.IsNullOrEmpty(lst.Position)) ? lst.Position : "");
                    NPR_Faculty_Changed = ((!String.IsNullOrEmpty(lst.Faculty)) ? lst.Faculty : "");
                    NPR_Chair_Changed  = ((!String.IsNullOrEmpty(lst.FullName)) ? lst.FullName : "");
                    NPR_Account_Changed = ((!String.IsNullOrEmpty(lst.UsridAd)) ? lst.UsridAd : "");
                }

            }
            catch (Exception)
            {
            }
        }
        private void btnFindNPRChanged_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("NPRListToFind"))
                Utilities.FormClose("NPRListToFind");

            new NPRListToFind(new UpdateStringHandler(NPRChangedSetToFound)).Show();
        }

        private void btnThemeNPRChangedClose2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefreshOrgList_Changed2_Click(object sender, EventArgs e)
        {
            int? OrgId = OrganizationId_Changed2;
            FillComboOrgChanged2();
            OrganizationId_Changed2 = OrgId;
        }

        private void btnOrgCard_Changed2_Click(object sender, EventArgs e)
        {
            //OrganizationId_Changed2
            if (!OrganizationId_Changed2.HasValue)
            {
                MessageBox.Show("Не выбрана организация!", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbOrganization_Changed2.DroppedDown = true;
                return;
            }
            try
            {
                int Orgid = (int)OrganizationId_Changed2;
                if (Utilities.OrgCardIsOpened(Orgid))
                    return;
                new CardOrganization(Orgid, null).Show();
            }
            catch
            {
            }
        }
        private void OrgListChanged2SetToFound()
        {
            OrgListChanged2SetToFound(null);
        }
        private void OrgListChanged2SetToFound(int? id)
        {
            OrganizationId_Changed2 = id;
        }
        private void btnOrgHandBook_Changed2_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("OrgListToFind"))
                Utilities.FormClose("OrgListToFind");

            new OrgListToFind(new UpdateIntHandler(OrgListChanged2SetToFound)).Show();
        }

        private void btnCleatNPRChanged2_Click(object sender, EventArgs e)
        {
            try
            {
                NPR_Changed_Persnum2 = "";
                NPR_Changed_Tabnum2 = "";
                NPR_FIO_Changed2 = "";
                NPR_Degree_Changed2 = "";
                NPR_Rank_Changed2 = "";
                NPR_Position_Changed2 = "";
                NPR_Faculty_Changed2 = "";
                NPR_Chair_Changed2 = "";
                NPR_Account_Changed2 = "";
            }
            catch (Exception)
            {
            }
        }
        private void NPRChanged2SetToFound()
        {
            NPRChanged2SetToFound(null, null);
        }
        private void NPRChanged2SetToFound(int? id, string persnum)
        {
            try
            {
                //string Persnum = persnum;
                //if (String.IsNullOrEmpty(Persnum))
                //    return;
                string Tabnum = persnum;
                if (String.IsNullOrEmpty(Tabnum))
                    return;

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    //var lst = context.SAP_NPR.Where(x => x.Persnum == Persnum).First();
                    var lst = context.SAP_NPR.Where(x => x.Tabnum == Tabnum).First();

                    NPR_Changed_Persnum2 = ((!String.IsNullOrEmpty(lst.Persnum)) ? lst.Persnum : "");
                    NPR_Changed_Tabnum2 = ((!String.IsNullOrEmpty(lst.Tabnum)) ? lst.Tabnum : "");
                    NPR_FIO_Changed2 = ((!String.IsNullOrEmpty(lst.Lastname)) ? lst.Lastname : "") + " " + ((!String.IsNullOrEmpty(lst.Name)) ? lst.Name : "") + " " + ((!String.IsNullOrEmpty(lst.Surname)) ? lst.Surname : "");
                    NPR_Degree_Changed2 = ((!String.IsNullOrEmpty(lst.Degree)) ? lst.Degree : "");
                    NPR_Rank_Changed2 = ((!String.IsNullOrEmpty(lst.Titl2)) ? lst.Titl2 : "");
                    NPR_Position_Changed2 = ((!String.IsNullOrEmpty(lst.Position)) ? lst.Position : "");
                    NPR_Faculty_Changed2 = ((!String.IsNullOrEmpty(lst.Faculty)) ? lst.Faculty : "");
                    NPR_Chair_Changed2 = ((!String.IsNullOrEmpty(lst.FullName)) ? lst.FullName : "");
                    NPR_Account_Changed2 = ((!String.IsNullOrEmpty(lst.UsridAd)) ? lst.UsridAd : "");
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnFindNPRChanged2_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("NPRListToFind"))
                Utilities.FormClose("NPRListToFind");

            new NPRListToFind(new UpdateStringHandler(NPRChanged2SetToFound)).Show();
        }

        private void cbOrganization_Changed2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!OrganizationId_Changed2.HasValue)
                {
                    OrgName_Changed2 = "";
                    return;
                }
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = context.Organization.Where(x => x.Id == (int)OrganizationId_Changed2).First();
                    //OrgName = lst.Name;
                    OrgName_Changed2 = (!String.IsNullOrEmpty(lst.Name)) ? lst.Name : "";
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnClearReviewNPR_Changed2_Click(object sender, EventArgs e)
        {
            try
            {
                ReviewPersnumNPR_Changed2 = "";
                ReviewTabnumNPR_Changed2 = "";
                ReviewFIONPR_Changed2 = "";
                ReviewDegreeNPR_Changed2 = "";
                ReviewRankNPR_Changed2 = "";
                ReviewPositionNPR_Changed2 = "";
                ReviewUNPNPR_Changed2 = "";
                ReviewChairNPR_Changed2 = "";
                ReviewAccountNPR_Changed2 = "";
            }
            catch (Exception)
            {
            }
        }

        private void btnClearReviewNPR2_Changed2_Click(object sender, EventArgs e)
        {
            try
            {
                ReviewPersnumNPR2_Changed2 = "";
                ReviewTabnumNPR2_Changed2 = "";
                ReviewFIONPR2_Changed2 = "";
                ReviewDegreeNPR2_Changed2 = "";
                ReviewRankNPR2_Changed2 = "";
                ReviewPositionNPR2_Changed2 = "";
                ReviewUNPNPR2_Changed2 = "";
                ReviewChairNPR2_Changed2 = "";
                ReviewAccountNPR2_Changed2 = "";
            }
            catch (Exception)
            {
            }
        }
        private void ReviewNPRChanged2SetToFound()
        {
            ReviewNPRChanged2SetToFound(null, null);
        }
        private void ReviewNPRChanged2SetToFound(int? id, string persnum)
        {
            try
            {
                //string Persnum = persnum;
                //if (String.IsNullOrEmpty(Persnum))
                //    return;
                string Tabnum = persnum;
                if (String.IsNullOrEmpty(Tabnum))
                    return;

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    //var lst = context.SAP_NPR.Where(x => x.Persnum == Persnum).First();
                    var lst = context.SAP_NPR.Where(x => x.Tabnum == Tabnum).First();

                    ReviewPersnumNPR_Changed2 = ((!String.IsNullOrEmpty(lst.Persnum)) ? lst.Persnum : "");
                    ReviewTabnumNPR_Changed2 = ((!String.IsNullOrEmpty(lst.Tabnum)) ? lst.Tabnum : "");
                    ReviewFIONPR_Changed2 = ((!String.IsNullOrEmpty(lst.Lastname)) ? lst.Lastname : "") + " " + ((!String.IsNullOrEmpty(lst.Name)) ? lst.Name : "") + " " + ((!String.IsNullOrEmpty(lst.Surname)) ? lst.Surname : "");
                    ReviewDegreeNPR_Changed2 = ((!String.IsNullOrEmpty(lst.Degree)) ? lst.Degree : "");
                    ReviewRankNPR_Changed2 = ((!String.IsNullOrEmpty(lst.Titl2)) ? lst.Titl2 : "");
                    ReviewPositionNPR_Changed2 = ((!String.IsNullOrEmpty(lst.Position)) ? lst.Position : "");
                    ReviewUNPNPR_Changed2 = ((!String.IsNullOrEmpty(lst.Faculty)) ? lst.Faculty : "");
                    ReviewChairNPR_Changed2 = ((!String.IsNullOrEmpty(lst.FullName)) ? lst.FullName : "");
                    ReviewAccountNPR_Changed2 = ((!String.IsNullOrEmpty(lst.UsridAd)) ? lst.UsridAd : "");
                }
            }
            catch (Exception)
            {
            }
        }
        private void ReviewNPRChanged2SetToFound2()
        {
            ReviewNPRChanged2SetToFound2(null, null);
        }
        private void ReviewNPRChanged2SetToFound2(int? id, string persnum)
        {
            try
            {
                //string Persnum = persnum;
                //if (String.IsNullOrEmpty(Persnum))
                //    return;
                string Tabnum = persnum;
                if (String.IsNullOrEmpty(Tabnum))
                    return;

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    //var lst = context.SAP_NPR.Where(x => x.Persnum == Persnum).First();
                    var lst = context.SAP_NPR.Where(x => x.Tabnum == Tabnum).First();

                    ReviewPersnumNPR2_Changed2 = ((!String.IsNullOrEmpty(lst.Persnum)) ? lst.Persnum : "");
                    ReviewTabnumNPR2_Changed2 = ((!String.IsNullOrEmpty(lst.Tabnum)) ? lst.Tabnum : "");
                    ReviewFIONPR2_Changed2 = ((!String.IsNullOrEmpty(lst.Lastname)) ? lst.Lastname : "") + " " + ((!String.IsNullOrEmpty(lst.Name)) ? lst.Name : "") + " " + ((!String.IsNullOrEmpty(lst.Surname)) ? lst.Surname : "");
                    ReviewDegreeNPR2_Changed2 = ((!String.IsNullOrEmpty(lst.Degree)) ? lst.Degree : "");
                    ReviewRankNPR2_Changed2 = ((!String.IsNullOrEmpty(lst.Titl2)) ? lst.Titl2 : "");
                    ReviewPositionNPR2_Changed2 = ((!String.IsNullOrEmpty(lst.Position)) ? lst.Position : "");
                    ReviewUNPNPR2_Changed2 = ((!String.IsNullOrEmpty(lst.Faculty)) ? lst.Faculty : "");
                    ReviewChairNPR2_Changed2 = ((!String.IsNullOrEmpty(lst.FullName)) ? lst.FullName : "");
                    ReviewAccountNPR2_Changed2 = ((!String.IsNullOrEmpty(lst.UsridAd)) ? lst.UsridAd : "");
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnFindNPR_Changed2_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("NPRListToFind"))
                Utilities.FormClose("NPRListToFind");

            new NPRListToFind(new UpdateStringHandler(ReviewNPRChanged2SetToFound)).Show();
        }

        private void btnFindNPR2_Changed2_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("NPRListToFind"))
                Utilities.FormClose("NPRListToFind");

            new NPRListToFind(new UpdateStringHandler(ReviewNPRChanged2SetToFound2)).Show();
        }

        private void btnRefreshReviewList_Changed2_Click(object sender, EventArgs e)
        {
            int? PersId = PersonId_Changed2;
            FillComboPersonChanged2();
            PersonId_Changed2 = PersId;
        }

        private void btnRefreshReviewList2_Changed2_Click(object sender, EventArgs e)
        {
            int? PersId = PersonId2_Changed2;
            FillComboPerson2Changed2();
            PersonId2_Changed2 = PersId;
        }

        private void cbPerson_Changed2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!PersonId_Changed2.HasValue)
                {
                    ReviewFIOPartner_Changed2 = "";
                    ReviewDegreePartner_Changed2 = "";
                    ReviewRankPartner_Changed2 = "";
                    ReviewOrgPosPartner_Changed2 = "";
                    ReviewAccountPartner_Changed2 = "";
                    return;
                }
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = context.PartnerPersonOrgPosition.Where(x => x.Id == (int)PersonId_Changed2).First();

                    ReviewFIOPartner_Changed2 = (!String.IsNullOrEmpty(lst.Name)) ? lst.Name : "";
                    ReviewDegreePartner_Changed2 = (!String.IsNullOrEmpty(lst.DegreeName)) ? lst.DegreeName : "";
                    ReviewRankPartner_Changed2 = (!String.IsNullOrEmpty(lst.RankName)) ? lst.RankName : "";
                    ReviewOrgPosPartner_Changed2 = (!String.IsNullOrEmpty(lst.OrgPosition)) ? lst.OrgPosition : "";
                    ReviewAccountPartner_Changed2 = (!String.IsNullOrEmpty(lst.Account)) ? lst.Account : "";
                }
            }
            catch (Exception)
            {
            }
        }

        private void cbPerson2_Changed2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!PersonId2_Changed2.HasValue)
                {
                    ReviewFIOPartner2_Changed2 = "";
                    ReviewDegreePartner2_Changed2 = "";
                    ReviewRankPartner2_Changed2 = "";
                    ReviewOrgPosPartner2_Changed2 = "";
                    ReviewAccountPartner2_Changed2 = "";
                    return;
                }
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = context.PartnerPersonOrgPosition.Where(x => x.Id == (int)PersonId2_Changed2).First();

                    ReviewFIOPartner2_Changed2 = (!String.IsNullOrEmpty(lst.Name)) ? lst.Name : "";
                    ReviewDegreePartner2_Changed2 = (!String.IsNullOrEmpty(lst.DegreeName)) ? lst.DegreeName : "";
                    ReviewRankPartner2_Changed2 = (!String.IsNullOrEmpty(lst.RankName)) ? lst.RankName : "";
                    ReviewOrgPosPartner2_Changed2 = (!String.IsNullOrEmpty(lst.OrgPosition)) ? lst.OrgPosition : "";
                    ReviewAccountPartner2_Changed2 = (!String.IsNullOrEmpty(lst.Account)) ? lst.Account : "";
                }
            }
            catch (Exception)
            {
            }
        }
        private void PersonListChanged2SetToFound()
        {
            PersonListChanged2SetToFound(null);
        }
        private void PersonListChanged2SetToFound(int? id)
        {
            if (PersonId_Changed2 == id)
            {
                PersonId_Changed2 = null;
                PersonId_Changed2 = id;
            }
            else
            {
                PersonId_Changed2 = id;
            }
        }
        private void PersonListChanged2SetToFound2()
        {
            PersonListChanged2SetToFound2(null);
        }
        private void PersonListChanged2SetToFound2(int? id)
        {
            if (PersonId2_Changed2 == id)
            {
                PersonId2_Changed2 = null;
                PersonId2_Changed2 = id;
            }
            else
            {
                PersonId2_Changed2 = id;
            }
        }

        private void BtnFindPartner_Changed2_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("PersonListToFind"))
                Utilities.FormClose("PersonListToFind");

            new PersonListToFind(new UpdateIntHandler(PersonListChanged2SetToFound)).Show();
        }

        private void BtnFindPartner2_Changed2_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("PersonListToFind"))
                Utilities.FormClose("PersonListToFind");

            new PersonListToFind(new UpdateIntHandler(PersonListChanged2SetToFound2)).Show();
        }

        private void btnReviewPartnerCard_Changed2_Click(object sender, EventArgs e)
        {
            if (!PersonId_Changed2.HasValue)
            {
                MessageBox.Show("Не выбран рецензент!", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbPerson_Changed2.DroppedDown = true;
                return;
            }
            try
            {
                int PersId = (int)PersonId_Changed2;
                if (Utilities.PersonCardIsOpened(PersId))
                    return;
                new CardPerson(PersId, null).Show();
            }
            catch
            {
            }
        }

        private void btnReviewPartnerCard2_Changed2_Click(object sender, EventArgs e)
        {
            if (!PersonId2_Changed2.HasValue)
            {
                MessageBox.Show("Не выбран рецензент!", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbPerson2_Changed2.DroppedDown = true;
                return;
            }
            try
            {
                int PersId = (int)PersonId2_Changed2;
                if (Utilities.PersonCardIsOpened(PersId))
                    return;
                new CardPerson(PersId, null).Show();
            }
            catch
            {
            }
        }

        private void btnClearReviewPartner_Changed2_Click(object sender, EventArgs e)
        {
            try
            {
                PersonId_Changed2 = null;
                ReviewFIOPartner_Changed2 = "";
                ReviewDegreePartner_Changed2 = "";
                ReviewRankPartner_Changed2 = "";
                ReviewOrgPosPartner_Changed2 = "";
                ReviewAccountPartner_Changed2 = "";
            }
            catch
            { }
        }

        private void btnClearReviewPartner2_Changed2_Click(object sender, EventArgs e)
        {
            try
            {
                PersonId2_Changed2 = null;
                ReviewFIOPartner2_Changed2 = "";
                ReviewDegreePartner2_Changed2 = "";
                ReviewRankPartner2_Changed2 = "";
                ReviewOrgPosPartner2_Changed2 = "";
                ReviewAccountPartner2_Changed2 = "";
            }
            catch
            { }
        }

        private void btnThemeNPRChangedSave2_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
                return;
            if (OrderNumberReview == "")
            {
                MessageBox.Show("Тема ВКР И НР не утверждались приказом о рецензентах.\n\rДанные необходимо вводить на предыдущих вкладках", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var vkrst = context.VKR_ThemesStudentOrder.Where(x => x.Id == _Id).First();
                    //тема
                    vkrst.VKRName_Changed2 = VKRName_Changed2;
                    vkrst.VKRNameEng_Changed2 = VKRNameEng_Changed2;
                    vkrst.VKRName_ChangedDoc2 = VKRName_ChangedDoc2;
                    //научный руководитель
                    vkrst.NPR_Changed_Persnum2 = NPR_Changed_Persnum2;
                    vkrst.NPR_Changed_Tabnum2 = NPR_Changed_Tabnum2;
                    vkrst.NPR_FIO_Changed2 = NPR_FIO_Changed2;
                    vkrst.NPR_Degree_Changed2 = NPR_Degree_Changed2;
                    vkrst.NPR_Rank_Changed2 = NPR_Rank_Changed2;
                    vkrst.NPR_Position_Changed2 = NPR_Position_Changed2;
                    vkrst.NPR_Faculty_Changed2 = NPR_Faculty_Changed2;
                    vkrst.NPR_Chair_Changed2 = NPR_Chair_Changed2;
                    vkrst.NPR_Account_Changed2 = NPR_Account_Changed2;

                    //организация
                    vkrst.OrganizationId_Changed2 = OrganizationId_Changed2;
                    vkrst.OrganizationName_Changed2 = OrgName_Changed2;
                    vkrst.OrganizationDocument_Changed2 = OrgDocument_Changed2;
                    vkrst.OrganizationDop_Changed2 = OrganizationDop_Changed2;
                    vkrst.OrganizationNotAgreed_Changed2 = OrganizationNotAgreed_Changed2;

                    //рецензенты
                    vkrst.Review_NPR_Persnum_Changed2 = ReviewPersnumNPR_Changed2;
                    vkrst.Review_NPR_Tabnum_Changed2 = ReviewTabnumNPR_Changed2;
                    vkrst.Review_NPR_FIO_Changed2 = ReviewFIONPR_Changed2;
                    vkrst.Review_NPR_Degree_Changed2 = ReviewDegreeNPR_Changed2;
                    vkrst.Review_NPR_Rank_Changed2 = ReviewRankNPR_Changed2;
                    vkrst.Review_NPR_Position_Changed2 = ReviewPositionNPR_Changed2;
                    vkrst.Review_NPR_Faculty_Changed2 = ReviewUNPNPR_Changed2;
                    vkrst.Review_NPR_Chair_Changed2 = ReviewChairNPR_Changed2;
                    vkrst.Review_NPR_Account_Changed2 = ReviewAccountNPR_Changed2;

                    vkrst.Review_NPR_Persnum2_Changed2 = ReviewPersnumNPR2_Changed2;
                    vkrst.Review_NPR_Tabnum2_Changed2 = ReviewTabnumNPR2_Changed2;
                    vkrst.Review_NPR_FIO2_Changed2 = ReviewFIONPR2_Changed2;
                    vkrst.Review_NPR_Degree2_Changed2 = ReviewDegreeNPR2_Changed2;
                    vkrst.Review_NPR_Rank2_Changed2 = ReviewRankNPR2_Changed2;
                    vkrst.Review_NPR_Position2_Changed2 = ReviewPositionNPR2_Changed2;
                    vkrst.Review_NPR_Faculty2_Changed2 = ReviewUNPNPR2_Changed2;
                    vkrst.Review_NPR_Chair2_Changed2 = ReviewChairNPR2_Changed2;
                    vkrst.Review_NPR_Account2_Changed2 = ReviewAccountNPR2_Changed2;

                    vkrst.Review_PartnerPersonId_Changed2 = PersonId_Changed2;
                    vkrst.Review_PP_FIO_Changed2 = ReviewFIOPartner_Changed2;
                    vkrst.Review_PP_Degree_Changed2 = ReviewDegreePartner_Changed2;
                    vkrst.Review_PP_Rank_Changed2 = ReviewRankPartner_Changed2;
                    //vkrst.Review_PP_Position_Changed2 = ReviewPositionPartner_Changed2;
                    //vkrst.Review_PP_Organization_Changed2 = ReviewOrgPartner_Changed2;
                    //vkrst.Review_PP_Subdivision_Changed2 = ReviewSubdivisionPartner_Changed2;
                    vkrst.Review_PP_OrgPosition_Changed2 = ReviewOrgPosPartner_Changed2;
                    vkrst.Review_PP_Account_Changed2 = ReviewAccountPartner_Changed2;

                    vkrst.Review_PartnerPersonId2_Changed2 = PersonId2_Changed2;
                    vkrst.Review_PP_FIO2_Changed2 = ReviewFIOPartner2_Changed2;
                    vkrst.Review_PP_Degree2_Changed2 = ReviewDegreePartner2_Changed2;
                    vkrst.Review_PP_Rank2_Changed2 = ReviewRankPartner2_Changed2;
                    //vkrst.Review_PP_Position2_Changed2 = ReviewPositionPartner2_Changed2;
                    //vkrst.Review_PP_Organization2_Changed2 = ReviewOrgPartner2_Changed2;
                    //vkrst.Review_PP_Subdivision2_Changed2 = ReviewSubdivisionPartner2_Changed2;
                    vkrst.Review_PP_OrgPosition2_Changed2 = ReviewOrgPosPartner2_Changed2;
                    vkrst.Review_PP_Account2_Changed2 = ReviewAccountPartner2_Changed2;

                    vkrst.Review_NotUsePrevious_Changed2 = Review_NotUsePrevious_Changed2;

                    context.SaveChanges();

                    MessageBox.Show("Данные сохранены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (_hndl != null)
                        _hndl(_Id);
                }
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить данные...\r\n" + ex.Message + "\r\n" + ex.InnerException.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSaveEngName_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
                return;
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var vkrst = context.VKR_ThemesStudentOrder.Where(x => x.Id == _Id).First();

                    //vkrst.VKRName_Final = VKRNameFinal;
                    vkrst.VKRNameEng_Final = VKRNameEngFinal;

                    //vkrst.VKRName_set = chbVKRNameSet.Checked ? true : false;
                    //vkrst.VKRNameEng_set = chbVKRNameEngSet.Checked ? true : false;
                    //vkrst.VKRName_Student_set = chbVKRNameStudentSet.Checked ? true : false;
                    //vkrst.VKRNameEng_Student_set = chbVKRNameEngStudentSet.Checked ? true : false;
                    //vkrst.VKRName_Final_set = chbVKRNameFinalSet.Checked ? true : false;
                    //vkrst.VKRNameEng_Final_set = chbVKRNameEngFinalSet.Checked ? true : false;

                    //vkrst.NPR_FIO_Final = NPRFIOFinal;
                    //vkrst.NPR_Degree_Final = NPRDegreeFinal;
                    //vkrst.NPR_Rank_Final = NPRRankFinal;
                    //vkrst.NPR_Position_Final = NPRPositionFinal;
                    //vkrst.NPR_Faculty_Final = NPRUNPFinal;
                    //vkrst.NPR_Chair_Final = NPRChairFinal;
                    //vkrst.NPR_Account_Final = NPRAccountFinal;

                    //vkrst.NPR_set = chbNPRSet.Checked ? true : false;
                    //vkrst.NPR_Student_set = chbNPRStudentSet.Checked ? true : false;
                    //vkrst.NPR_Final_set = chbNPRFinalSet.Checked ? true : false;

                    //vkrst.OrganizationId = OrgId;
                    //vkrst.Comment = Comment;

                    context.SaveChanges();

                    MessageBox.Show("Данные сохранены", "Сообщение");
                    if (_hndl != null)
                        _hndl(_Id);
                }
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить данные...\r\n" + ex.Message + "\r\n" + ex.InnerException.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSaveEngNameChange_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
                return;
            if (OrderNumber == "")
            {
                MessageBox.Show("Тема ВКР И НР не утверждались приказом.\n\rДанные необходимо вводить на Вкладке 'Темы ВКР и НР'", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var vkrst = context.VKR_ThemesStudentOrder.Where(x => x.Id == _Id).First();

                    //vkrst.VKRName_Changed = VKRName_Changed;
                    vkrst.VKRNameEng_Changed = VKRNameEng_Changed;
                    //vkrst.VKRName_ChangedDoc = VKRName_ChangedDoc;

                    //vkrst.NPR_Changed_Persnum = NPR_Changed_Persnum;
                    //vkrst.NPR_Changed_Tabnum = NPR_Changed_Tabnum;
                    //vkrst.NPR_FIO_Changed = NPR_FIO_Changed;
                    //vkrst.NPR_Degree_Changed = NPR_Degree_Changed;
                    //vkrst.NPR_Rank_Changed = NPR_Rank_Changed;
                    //vkrst.NPR_Position_Changed = NPR_Position_Changed;
                    //vkrst.NPR_Faculty_Changed = NPR_Faculty_Changed;
                    //vkrst.NPR_Chair_Changed = NPR_Chair_Changed;
                    //vkrst.NPR_Account_Changed = NPR_Account_Changed;

                    context.SaveChanges();

                    MessageBox.Show("Данные сохранены", "Сообщение");
                    if (_hndl != null)
                        _hndl(_Id);
                }
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить данные...\r\n" + ex.Message + "\r\n" + ex.InnerException.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chbOrganizationNotAgreed_Changed2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (OrganizationNotAgreed_Changed2)
                {
                    OrganizationId_Changed2 = null;
                    OrgDocument_Changed2 = "";
                    cbOrganization_Changed2.Enabled = false;
                }
                else
                {
                    cbOrganization_Changed2.Enabled = true;
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnChangeReviewOrderNum_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данная опция позволяет изменить № и дату приказа о рецензентах.\r\n" +
                "Необходимость в этом может возникнуть в случае нарушения\r\nцелостности данных в ходе операций фиксации и \"разморозки\" приказов.\r\n" +
                "Такая ситуация может возникнуть,\r\nкогда фиксация производится не сразу после подготовки приказа.\r\n" +
                "В результате чего в связи с изменениями за этот промежуток\r\nвремени статусов студентов может возникнуть раскхождение\r\n" +
                "между номерами студентов в реальном приложении к приказу и в базе данных.\r\n" +
                "Удаляя в необходимых случаях №№ приказов\r\nили, наоборот, проставляя их,\r\n" +
                "можно затем перенумеровать студентов согласно № приказа.\r\n" +
                "Для сохранения нового № и даты нажмите кнопку \"Сохранить №\".","Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            tbOrderNumberReview.ReadOnly = false;
            tbOrderDateReview.ReadOnly = false;
        }

        private void btnSaveReviewOrderNum_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
                return;
            //Проверка правильности даты
            if (!CheckFieldsReview())
                return;
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var vkrst = context.VKR_ThemesStudentOrder.Where(x => x.Id == _Id).First();

                    vkrst.OrderNumberReview = OrderNumberReview;
                    
                    if (!String.IsNullOrEmpty(OrderDateReview))
                    {
                        vkrst.OrderDateReview = DateTime.Parse(OrderDateReview);
                    }
                    else
                    {
                        vkrst.OrderDateReview = null;
                    }
                    if (String.IsNullOrEmpty(OrderNumberReview))
                    {
                        vkrst.StudentNumberInOrderReview = null;
                    }

                    context.SaveChanges();

                    MessageBox.Show("Данные сохранены", "Сообщение");
                    if (_hndl != null)
                        _hndl(_Id);
                }
                //this.Close();
                tbOrderNumberReview.ReadOnly = true;
                tbOrderDateReview.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить данные...\r\n" + ex.Message + "\r\n" + ex.InnerException.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private bool CheckFieldsReview()
        {
            DateTime res;
            if (!String.IsNullOrEmpty(OrderDateReview))
            {
                if (!DateTime.TryParse(OrderDateReview, out res))
                {
                    MessageBox.Show("Неправильный формат даты в поле 'Дата приказа' \r\n" + "Образец: 01.12.2017", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            return true;
        }
    }
}
