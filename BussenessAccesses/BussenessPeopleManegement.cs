using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussenessAccesses;
using DataAccesses;

namespace BussenessAccesses
{
    public class clsBussenessPeopleManegement
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int PersonID { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }

        }
        public string NationalNo { set; get; }
        public DateTime DateOfBirth { set; get; }
        public int Gendor { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public int NationalityCountryID { set; get; }

        public clsBuessenessCountriesManagement CountryInfo;

        private string _ImagePath;

        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }

        public clsBussenessPeopleManegement()

        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";

            Mode = enMode.AddNew;
        }

        public clsBussenessPeopleManegement(int PersonID, string FirstName, string SecondName, string ThirdName,
            string LastName, string NationalNo, DateTime DateOfBirth, int Gendor,
             string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.NationalNo = NationalNo;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            this.CountryInfo = clsBuessenessCountriesManagement.Find(NationalityCountryID);
            Mode = enMode.Update;
        }

        private bool _AddNewPerson()
        {
            //call DataAccess Layer 

            this.PersonID = clsDataPeopleManegement.AddNewPerson(
                this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.NationalNo, this.Gendor, this.Address, this.Email, this.ImagePath,
                this.DateOfBirth, this.Phone, this.NationalityCountryID);

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            //call DataAccess Layer 

            return clsDataPeopleManegement.UpdatePerson(
               this.PersonID, this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.NationalNo, this.Gendor, this.Address, this.Email, this.ImagePath,
                this.DateOfBirth, this.Phone, this.NationalityCountryID);
        }

        public static clsBussenessPeopleManegement Find(int PersonID)
        {

            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", NationalNo = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;
            int Gendor = 0;

            bool IsFound = clsDataPeopleManegement.GetPersonInfoByID
                                (
                                    PersonID, ref FirstName, ref SecondName,
                                    ref ThirdName, ref LastName, ref Email, ref Phone, ref Address,
                                    ref Gendor, ref DateOfBirth, ref NationalityCountryID, ref ImagePath, ref NationalNo
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new clsBussenessPeopleManegement(PersonID, FirstName, SecondName, ThirdName, LastName,
                          NationalNo, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            else
                return null;
        }

        public static clsBussenessPeopleManegement Find(string NationalNo)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int PersonID = -1, NationalityCountryID = -1;
            int Gendor = 0;

            bool IsFound = clsDataPeopleManegement.GetPersonInfoByNationalNo
                           (
                                  NationalNo  , ref FirstName, ref SecondName,
                                    ref ThirdName, ref LastName, ref Email, ref Phone, ref Address,
                                    ref Gendor, ref DateOfBirth, ref NationalityCountryID, ref ImagePath, ref PersonID
                                );

            if (IsFound)

                return new clsBussenessPeopleManegement(PersonID, FirstName, SecondName, ThirdName, LastName,
                          NationalNo, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePerson();

            }

            return false;
        }

        public static DataTable GetAllPeople()
        {
            return clsDataPeopleManegement.GetAllPeople();
        }

        public static bool DeletePerson(int ID)
        {
            return clsDataPeopleManegement.DeletePerson(ID);
        }

        public static bool isPersonExist(int ID)
        {
            return clsDataPeopleManegement.IsPersonExistById(ID);
        }

        public static bool isPersonExist(string NationlNo)
        {
            return clsDataPeopleManegement.IsPersonExistByNationalNo(NationlNo);
        }

    }
}
