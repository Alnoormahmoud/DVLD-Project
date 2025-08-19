using BussenessAccesses;
using DVLD.Properties;
using System.IO;
using System;
using DVLD;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class UCInfo : UserControl
    {
        public int ID = -1;
        private clsBussenessPeopleManegement _Person;

        public UCInfo()
        {
            InitializeComponent();
        }

        public int PersonId
        {
            get { return ID; }
        }

        public clsBussenessPeopleManegement Person
            { get { return _Person; } }

        private void _LoadPersonImage()
        {
            if (_Person.Gendor == 0)
                pictureBox1.Image = Resources.Male_512;
            else
                pictureBox1.Image = Resources.Female_512;

            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pictureBox1.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public void LoadPersonInfo(int Id)
        {
            lblEditPerson.Enabled = false;
            ResetPersonInfo();
            _Person = clsBussenessPeopleManegement.Find(Id);
            
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("Can't Find Person With Id "+Id.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

                return;
            }

            _FillPersonInfo();

        }

        public void LoadPersonInfo(string NationalNumber)
        {
            _Person = clsBussenessPeopleManegement.Find(NationalNumber);
            
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("Can't Find Person With National Number "+NationalNumber,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

                return;
            }

            _FillPersonInfo();

        }

        private void _FillPersonInfo()
        {
            lblEditPerson.Enabled = true;
            ID = _Person.PersonID;
            lblPersonId.Text = _Person.PersonID.ToString();
            lblName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
            lblEmail.Text = _Person.Email;
            lblPhone.Text = _Person.Phone;
            lblAdderes.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString();
            lblNationalNumber.Text = _Person.NationalNo.ToString();

            int gender = _Person.Gendor;

            if (gender == 0)
                lblGender.Text = "Male";
            else if (gender == 1)
                lblGender.Text = "FeMale"; 

            lblCountry.Text = _Person.CountryInfo.CountryName.ToString();// (clsBuessenessCountriesManagement.Find(_Person.CountryID).CountryName);
            _LoadPersonImage();
        }

        public void ResetPersonInfo()
        {
            ID = -1;
            lblPersonId.Text = "[????]";
            lblNationalNumber.Text = "[????]";
            lblName.Text = "[????]";
            pictureBox1.Image = Resources.Man_32;
            lblGender.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAdderes.Text = "[????]";
            pictureBox1.Image = Resources.Male_512;

        } 
 
        private void llEditPerson_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(ID);
            frm.ShowDialog();
            LoadPersonInfo(ID);

        }

   
    }
}
