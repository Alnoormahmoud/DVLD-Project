using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataAccesses;


namespace BussenessAccessess
{
    public class clsBussenessPeopleManegement
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ID { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public int Gender { set; get; }
        public string NationalNo { set; get; }
        public DateTime DateOfBirth { set; get; }
        public string ImagePath { set; get; }
        public int CountryID { set; get; }
        public clsBuessenessCountriesManagement CountryInfo { set; get; }

          

        public clsBussenessPeopleManegement()
        {
            this.ID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.CountryID = -1;
            this.ImagePath = "";
            this.Gender = 0;
            this.NationalNo = "";
            Mode = enMode.AddNew;
        }

        public clsBussenessPeopleManegement(int ID, string FirstName, string Second, string Third, string LastName,
        string Email, string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath,string NationalNumber,int Gender)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.SecondName = Second;
            this.ThirdName = Third;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;
            this.Gender = Gender;
            this.NationalNo = NationalNumber;
            this.CountryInfo = clsBuessenessCountriesManagement.Find(CountryID);

            Mode = enMode.Update;
        }

        public static DataTable GetAllPeople()
        {
            
            return clsDataPeopleManegement.GetAllPeople();

        }
 
        public static clsBussenessPeopleManegement Find(int ID)
        {

            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "", NationalNo = "";
            DateTime DateOfBirth = DateTime.Now;
            int CountryID = -1, Gender = 0;

            if (clsDataPeopleManegement.GetPersonInfoByID(ID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, 
                ref Email, ref Phone, ref Address, ref Gender, ref DateOfBirth, ref CountryID, ref ImagePath, ref NationalNo))

                return new clsBussenessPeopleManegement(ID, FirstName,SecondName,ThirdName, LastName,
                           Email, Phone, Address, DateOfBirth, CountryID, ImagePath,NationalNo,Gender);
            else
                return null;

        }

        public  static bool DeletePerson(int id)
        {
           return clsDataPeopleManegement.DeletePerson(id);
        }

        private bool _AddNewPerson()
        {
            this.ID = clsDataPeopleManegement.AddNewPerson(this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.NationalNo,
                this.Gender, this.Address, this.Email, this.ImagePath, this.DateOfBirth, this.Phone, this.CountryID);

            return (this.ID != -1);
        }

        private bool _UpdatePerson()
        {
            //call DataAccess Layer 

            return clsDataPeopleManegement.UpdatePerson(this.ID, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.NationalNo,
                this.Gender,this.Address, this.Email, this.ImagePath,this.DateOfBirth,this.Phone, this.CountryID);
        }

        static public bool IsNationalNumberExsist(string nationalNo)
        {
             return clsDataPeopleManegement.IsPersonExistByNationalNo(nationalNo);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                  {  if (_AddNewPerson())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                case enMode.Update:
                    {
                        return _UpdatePerson();
                    }
            }
            return false;
        }
    }
}
