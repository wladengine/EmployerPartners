using System;
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
        public static bool UpdateLP()
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
        public static bool UpdateOP()
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
        public static bool UpdateStudentData()
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
                            catch (Exception ex)
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
}
