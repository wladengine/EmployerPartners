﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployerPartners
{
    public static class Utilities
    {
        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            System.ComponentModel.PropertyDescriptorCollection properties =
               System.ComponentModel.TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (System.ComponentModel.PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (System.ComponentModel.PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
        public static bool TestConnection()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.Organization
                               select x).First();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нет подключения к базе данных \r\n" + ex.Message + "\r\n" + ex.InnerException.Message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;   
            }
            finally
            {
            }
        }
        public static void SetReadMode(Control obj)
        {
            foreach (Control control in obj.Controls)
            {
                TextBox tb = control as TextBox;
                if (tb != null)
                {
                    tb.ReadOnly = true;
                }
                ComboBox cb = control as ComboBox;
                if (cb != null)
                {
                    cb.Enabled = false;
                }
            }
        }
        public static bool FormIsOpened(string frmName)
        {
            try
            {
                foreach (Form frm in Application.OpenForms)
                {
                    System.Type st = frm.GetType();
                    if  (st.Name == frmName)   //(frm.Name == frmName)
                    {
                        frm.Activate();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool OrgCardIsOpened(int orgid)
        {
            try
            {
                CardOrganization OrgCard;
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    if (Application.OpenForms[i].GetType() == typeof(CardOrganization))
                    {
                        OrgCard = Application.OpenForms[i] as CardOrganization;
                        if (OrgCard.OrgId == orgid)
                        {
                            OrgCard.Activate();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool PersonCardIsOpened(int personid)
        {
            try
            {
                CardPerson PersonCard;
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    if (Application.OpenForms[i].GetType() == typeof(CardPerson))
                    {
                        PersonCard = Application.OpenForms[i] as CardPerson;
                        if (PersonCard.PersonId == personid)
                        {
                            PersonCard.Activate();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool PracticeCardIsOpened(int practicecardid)
        {
            try
            {
                PracticeCard PCard;
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    if (Application.OpenForms[i].GetType() == typeof(PracticeCard))
                    {
                        PCard = Application.OpenForms[i] as PracticeCard;
                        if (PCard.PracticeCardId == practicecardid)
                        {
                            PCard.Activate();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool PracticeOrgCardIsOpened(int porgcardid)
        {
            try
            {
                PracticeOrgCard POrgCard;
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    if (Application.OpenForms[i].GetType() == typeof(PracticeOrgCard))
                    {
                        POrgCard = Application.OpenForms[i] as PracticeOrgCard;
                        if (POrgCard.POrgCardId == porgcardid)
                        {
                            POrgCard.Activate();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool PracticeStudentCardIsOpened(int pstudentcardid)
        {
            try
            {
                PracticeStudentCard PStudentCard;
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    if (Application.OpenForms[i].GetType() == typeof(PracticeStudentCard))
                    {
                        PStudentCard = Application.OpenForms[i] as PracticeStudentCard;
                        if (PStudentCard.PStudentCardId == pstudentcardid)
                        {
                            PStudentCard.Activate();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool VKRCardIsOpened(int vkrcardid)
        {
            try
            {
                VKRStudent VKRStCard;
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    if (Application.OpenForms[i].GetType() == typeof(VKRStudent))
                    {
                        VKRStCard = Application.OpenForms[i] as VKRStudent;
                        if (VKRStCard.VKRStCardId == vkrcardid)
                        {
                            VKRStCard.Activate();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool VKRStudentCardIsOpened(int vkrstudentcardid)
        {
            try
            {
                VKRStudentCard VKRStCard;
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    if (Application.OpenForms[i].GetType() == typeof(VKRStudentCard))
                    {
                        VKRStCard = Application.OpenForms[i] as VKRStudentCard;
                        if (VKRStCard.VKRStudentCardId == vkrstudentcardid)
                        {
                            VKRStCard.Activate();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool UpdateLP()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    ObjectParameter entId = new ObjectParameter("result", typeof(bool));
                    context.UpDateLicenseProgram(entId);
                    if (entId.Value is DBNull)
                    {
                        return Convert.ToBoolean(0);
                    }
                    else
                    {
                        return Convert.ToBoolean(entId.Value);
                    }
                    //return Convert.ToBoolean(entId.Value);
                }
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        public static bool UpdateOP()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    ObjectParameter entId = new ObjectParameter("result", typeof(bool));
                    context.UpDateObrazProgram(entId);
                    if (entId.Value is DBNull)
                    {
                        return Convert.ToBoolean(0);
                    }
                    else
                    {
                        return Convert.ToBoolean(entId.Value);
                    }
                    //return Convert.ToBoolean(entId.Value);
                }
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        public static bool UpdateStudentData()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    ObjectParameter entId = new ObjectParameter("result", typeof(bool));
                    context.UpDateStudentData(entId);
                    if (entId.Value is DBNull)
                    {
                        return Convert.ToBoolean(0);
                    }
                    else
                    {
                        return Convert.ToBoolean(entId.Value);
                    }
                    //return Convert.ToBoolean(entId.Value);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool UpdateStudentDVZ()
        {
            bool result = false;
            try
            {
                //Удаление записей из таблицы StudentDVZ
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    context.Database.ExecuteSqlCommand("DELETE FROM dbo.StudentDVZ");
                }

                //Добавление записей в таблицу StudentDVZ с сервера Development
                var SourceConnString = ConfigurationManager.ConnectionStrings["DevelopmentStudentDVZ"].ConnectionString;
                var DestinationConnString = ConfigurationManager.ConnectionStrings["EmployerPartnerSQL"].ConnectionString;

                using (SqlConnection sourceConnection = new SqlConnection(SourceConnString))
                {
                    sourceConnection.Open();

                    // Perform an initial count on the destination table.
                    //SqlCommand commandRowCount = new SqlCommand("SELECT COUNT(*) FROM dbo.StudentDVZ", sourceConnection);
                    //long countStart = System.Convert.ToInt32(commandRowCount.ExecuteScalar());

                    // Get data from the source table as a SqlDataReader.
                    SqlCommand commandSourceData = new SqlCommand("select * from [StudentDVZ].[dbo].[forBB]", sourceConnection);
                    SqlDataReader reader = commandSourceData.ExecuteReader();

                    using (SqlConnection destinationConnection = new SqlConnection(DestinationConnString))
                    {
                        destinationConnection.Open();
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection))
                        {
                            bulkCopy.DestinationTableName = "dbo.StudentDVZ";

                            #region Mapping

                            // Set up the column mappings by name.
                            SqlBulkCopyColumnMapping mapStudDataId = new SqlBulkCopyColumnMapping("StudDataId", "StudDataId");
                            bulkCopy.ColumnMappings.Add(mapStudDataId);
                            SqlBulkCopyColumnMapping mapFIO = new SqlBulkCopyColumnMapping("FIO", "FIO");
                            bulkCopy.ColumnMappings.Add(mapFIO);
                            SqlBulkCopyColumnMapping mapSurname = new SqlBulkCopyColumnMapping("Surname", "Surname");
                            bulkCopy.ColumnMappings.Add(mapSurname);
                            SqlBulkCopyColumnMapping mapFirstName = new SqlBulkCopyColumnMapping("FirstName", "FirstName");
                            bulkCopy.ColumnMappings.Add(mapFirstName);
                            SqlBulkCopyColumnMapping mapSecondName = new SqlBulkCopyColumnMapping("SecondName", "SecondName");
                            bulkCopy.ColumnMappings.Add(mapSecondName);
                            SqlBulkCopyColumnMapping mapCourse = new SqlBulkCopyColumnMapping("Course", "Course");
                            bulkCopy.ColumnMappings.Add(mapCourse);
                            SqlBulkCopyColumnMapping mapWorkPlan = new SqlBulkCopyColumnMapping("WorkPlan", "WorkPlan");
                            bulkCopy.ColumnMappings.Add(mapWorkPlan);
                            SqlBulkCopyColumnMapping mapRegNomWP = new SqlBulkCopyColumnMapping("RegNomWP", "RegNomWP");
                            bulkCopy.ColumnMappings.Add(mapRegNomWP);
                            SqlBulkCopyColumnMapping mapDepartment = new SqlBulkCopyColumnMapping("Department", "Department");
                            bulkCopy.ColumnMappings.Add(mapDepartment);
                            SqlBulkCopyColumnMapping mapStudyBasis = new SqlBulkCopyColumnMapping("StudyBasis", "StudyBasis");
                            bulkCopy.ColumnMappings.Add(mapStudyBasis);
                            SqlBulkCopyColumnMapping mapDegreeName = new SqlBulkCopyColumnMapping("DegreeName", "DegreeName");
                            bulkCopy.ColumnMappings.Add(mapDegreeName);
                            SqlBulkCopyColumnMapping mapAccout = new SqlBulkCopyColumnMapping("Accout", "Accout");
                            bulkCopy.ColumnMappings.Add(mapAccout);
                            SqlBulkCopyColumnMapping mapMailBox = new SqlBulkCopyColumnMapping("MailBox", "MailBox");
                            bulkCopy.ColumnMappings.Add(mapMailBox);
                            SqlBulkCopyColumnMapping mapFacultyName = new SqlBulkCopyColumnMapping("FacultyName", "FacultyName");
                            bulkCopy.ColumnMappings.Add(mapFacultyName);
                            SqlBulkCopyColumnMapping mapDR = new SqlBulkCopyColumnMapping("DR", "DR");
                            bulkCopy.ColumnMappings.Add(mapDR);
                            SqlBulkCopyColumnMapping mapPersNumber = new SqlBulkCopyColumnMapping("PersNumber", "PersNumber");
                            bulkCopy.ColumnMappings.Add(mapPersNumber);
                            SqlBulkCopyColumnMapping mapSpecNumber = new SqlBulkCopyColumnMapping("SpecNumber", "SpecNumber");
                            bulkCopy.ColumnMappings.Add(mapSpecNumber);
                            SqlBulkCopyColumnMapping mapSpecName = new SqlBulkCopyColumnMapping("SpecName", "SpecName");
                            bulkCopy.ColumnMappings.Add(mapSpecName);
                            SqlBulkCopyColumnMapping mapStatusName = new SqlBulkCopyColumnMapping("StatusName", "StatusName");
                            bulkCopy.ColumnMappings.Add(mapStatusName);
                            SqlBulkCopyColumnMapping mapStudyingName = new SqlBulkCopyColumnMapping("StudyingName", "StudyingName");
                            bulkCopy.ColumnMappings.Add(mapStudyingName);

                            #endregion

                            try
                            {
                                // Write from the source to the destination.
                                bulkCopy.WriteToServer(reader);
                                //Успешное завершение
                                result = true;
                            }
                            catch (Exception)
                            {
                                //MessageBox.Show("Не удалось обновить данные \r\n" + ex.Message);
                                result = false;
                            }
                            finally
                            {
                                // Close the SqlDataReader. The SqlBulkCopy object is automatically closed at the end of the using block.
                                reader.Close();
                            }
                        }
                        // Perform a final count on the destination table to see how many rows were added.
                        //long countEnd = System.Convert.ToInt32(commandRowCount.ExecuteScalar());
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
    }
    public class OP
    {
        //используется для типизации в UpdateFromSrv
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
    }
    public class OPInYear
    {
        //используется для типизации в UpdateFromSrv
        public int ObrazProgramId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string StudyLevelName { get; set; }
        public string Year { get; set; }
        public string ObrazProgramCrypt { get; set; }
    }
    public class OrgDogovor
    {
        //используюется для типизации в PracticeCard
        public int Id { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string ShortName { get; set; }
        public string NameEng { get; set; }
        public string ShortNameEng { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string WebSite { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Apartment { get; set; }
        public string Comment { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
    }
}
