using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.DirectoryServices.AccountManagement;
using System.Data.Entity.Core.Objects;

namespace EmployerPartners
{
    static class  Util
    {
        public static Form mainform;

        public static int countryRussiaId = 172;
        public static List<KeyValuePair<int, string>> lstCountry;
        public static List<KeyValuePair<int, string>> lstRegion;
        public static string TempFilesFolder;
        public static string TemplatesFolder;
        public static List<KeyValuePair<int, string>> lstRegionCode;

        public static List<KeyValuePair<int, string>> lstCountryCity;
        public static List<KeyValuePair<string, string>> lstCityStreet;


        static Util()
        {
            Init();

            TemplatesFolder = string.Format(@"{0}\Templates", Application.StartupPath);
            TempFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\EmployerPartners_TempFiles\";
            try
            {
                if (!Directory.Exists(TempFilesFolder))
                    Directory.CreateDirectory(TempFilesFolder);
            }
            catch (Exception e)
            {
                MessageBox.Show("Не удалось создать директорию.\r\n" + e.Message + ": " + e.InnerException);
            }
        }

        private static void Init ()
        {
            lstCountry = new List<KeyValuePair<int, string>>();
            lstRegion = new List<KeyValuePair<int, string>>();
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst_country = context.Country.Select(x => new { x.Id, x.Name }).ToList();
                lstCountry = (from l in lst_country select new KeyValuePair<int, string>(l.Id, l.Name)).ToList();
                
                var lst_reg = context.Region.Where(x=>!String.IsNullOrEmpty(x.RegionNumber) && x.RegionNumber != "00").Select(x => new { x.Id, x.Name, x.RegionNumber }).Distinct().ToList();
                lstRegion = (from l in lst_reg select new KeyValuePair<int, string>(l.Id, l.Name)).ToList();
                lstRegionCode = (from l in lst_reg select new KeyValuePair<int, string>(l.Id, l.RegionNumber)).ToList();

                var lst_country_city = context.Organization
                    .Where(x => x.CountryId != countryRussiaId && x.CountryId.HasValue)
                    .Select(x => new { key = x.CountryId.Value, value =  x.City}).Distinct().ToList();
                lstCountryCity = (from lst in lst_country_city
                                  select new KeyValuePair<int, string>(lst.key, lst.value)).ToList();

                var lst_city_street = context.Organization
                    .Where(x => x.CountryId != countryRussiaId && x.CountryId.HasValue)
                    .Select(x => new { key = x.City, value = x.Street }).Distinct().ToList();
                lstCityStreet = (from lst in lst_city_street
                                 select new KeyValuePair<string, string>(lst.key, lst.value)).ToList();
            }
        }

        public static string GetUserName()
        {
            return GetADUserName(System.Environment.UserName);
        }
        public static string GetADUserName(string userName)
        {
            try
            {
                var ADPrincipal = new PrincipalContext(ContextType.Domain);
                UserPrincipal user = UserPrincipal.FindByIdentity(ADPrincipal, userName);

                if (user != null)
                    return user.DisplayName + " (" + userName + ")";
            }
            catch { }

            return userName;
        }

        public static bool IsAdministrator()
        {
            return IsRoleMember("db_administrator") || IsDeveloper();
        }

        public static bool IsDBOwner()
        {
            return IsRoleMember("db_owner") || IsRoleMember("db_administrator");
        }
		
        public static bool IsRoleMember(string roleName)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                ObjectParameter entId = new ObjectParameter("result", typeof(bool));
                context.RoleMember(roleName, entId);
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
        public static bool IsDeveloper()
        {
            string userName = System.Environment.UserName;
            if (userName == "v.chikhira")
                return true;

            return false;
        }
    }
   
    public delegate void UpdateVoidHandler(int? id);
    public delegate void UpdateStringHandler(int? id, string Name);


}
