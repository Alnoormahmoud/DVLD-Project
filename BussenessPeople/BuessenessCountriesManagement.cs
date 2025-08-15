using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccesses;

namespace BussenessAccessess
{
    public class clsBuessenessCountriesManagement
    {

        public int ID { set; get; }
        public string CountryName { set; get; }


        public clsBuessenessCountriesManagement()

        {
            this.ID = -1;
            this.CountryName = "";
        }

        private clsBuessenessCountriesManagement(int ID, string CountryName)

        {
            this.ID = ID;
            this.CountryName = CountryName;

        }
        public static DataTable GetAllCountries()
        {
            return clsDataCountriesManagement.GetAllCountries();
        }

        public static clsBuessenessCountriesManagement Find(string CountryName)
        {
            int ID = -1;

            if (clsDataCountriesManagement.GetCountryInfoByName(CountryName, ref ID))

                return new clsBuessenessCountriesManagement(ID, CountryName);
            else
                return null;

        }

        public static clsBuessenessCountriesManagement Find(int ID)
        {
            string CountryName = "";

            int CountryID = -1;

            if (clsDataCountriesManagement.GetCountryInfoByID(ID, ref CountryName))

                return new clsBuessenessCountriesManagement(ID, CountryName);
            else
                return null;

        }

    }
}
