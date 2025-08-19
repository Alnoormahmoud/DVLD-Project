using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussenessAccesses;
using System.Runtime.Remoting.Messaging;

namespace DVLD
{
    public partial class frmAddUpdatePerson : Form
    {

        // Declare a delegate
        public delegate void DataBackEventHandler(int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;
        public enum enMode { Add = 0, Update = 1 };
        private enMode Mode = enMode.Update;

        int PersonId;
        clsBussenessPeopleManegement _Person;
        public frmAddUpdatePerson(int Id)
        {
            InitializeComponent();

            PersonId = Id;

            if (PersonId == -1)
            {
                Mode = enMode.Add;
            }
            else
            {
                Mode = enMode.Update;
            }

        }

        private void _ResetDefultValues()
        {
            FillCountriesInCompoBox();
            txtFirst.Focus();
 

            if (Mode == enMode.Add)
            {
                lblText.Text = "Add New Person";
                cbCountries.Text = "Sudan";
                rbMale.Checked = true;
                _Person = new clsBussenessPeopleManegement();
                return;
            }

            _Person = clsBussenessPeopleManegement.Find(PersonId);

            if (_Person == null)
            {
                MessageBox.Show("This Form Will Close Because No Contact Was Found With Id " + PersonId.ToString());
                this.Close();
                return;
            }

            lblText.Text = "Edit Person's Info";
            lblId.Text = PersonId.ToString();
            txtFirst.Text = _Person.FirstName;
            txtSecond.Text = _Person.SecondName;
            txtTheird.Text = _Person.ThirdName;
            txtLast.Text = _Person.LastName;
            txtEmail.Text = _Person.Email;
            txtPhone.Text = _Person.Phone;
            txtAdderess.Text = _Person.Address;
            dtpDateOfBirth.Value = _Person.DateOfBirth;

            txtNationalNo.Text = _Person.NationalNo.ToString();

            int gender = Convert.ToInt32(_Person.Gendor);

            if (gender == 0)
                rbMale.Checked = true;
            else if (gender == 1)
                rbFemale.Checked = true;

            if (_Person.ImagePath != "")
            {
                pbPicture.ImageLocation = (_Person.ImagePath);
            }

            lblRemove.Visible = (_Person.ImagePath != "");

            cbCountries.SelectedIndex = cbCountries.FindString(clsBuessenessCountriesManagement.Find(_Person.NationalityCountryID).CountryName);

        }
 
        private void Form3_Load(object sender, EventArgs e)
        {
            _ResetDefultValues();
    
        }
 
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
 
        private void FillCountriesInCompoBox()
        {
            DataTable dataTable = clsBuessenessCountriesManagement.GetAllCountries();

            foreach (DataRow row in dataTable.Rows)
            {
                cbCountries.Items.Add(row["CountryName"]);
            }
        }
        private void lblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Filter to show only image files
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbPicture.Image = Image.FromFile(openFileDialog.FileName);
                pbPicture.ImageLocation = openFileDialog.FileName;
                lblRemove.Visible = true;
            }
        }

        private bool _HandlePersonImage()
        {

            //this procedure will handle the person image,
            //it will take care of deleting the old image from the folder
            //in case the image changed. and it will rename the new image with guid and 
            // place it in the images folder.


            //_Person.ImagePath contains the old Image, we check if it changed then we copy the new image
            if (_Person.ImagePath != pbPicture.ImageLocation)
            {

                if (_Person.ImagePath != "")
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                        // We could not delete the file.
                        //log it later   
                    }
                }



                if (pbPicture.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pbPicture.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPicture.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (!_HandlePersonImage())
            {
                return;
            }

            switch (Mode)
            {


                case enMode.Add:


                    int CountryId = clsBuessenessCountriesManagement.Find(cbCountries.Text).ID ;

                    _Person.FirstName = txtFirst.Text;
                    _Person.SecondName = txtSecond.Text;
                    _Person.ThirdName = txtTheird.Text;
                    _Person.LastName = txtLast.Text;
                    _Person.Email = txtEmail.Text;
                    _Person.Address = txtAdderess.Text;
                    _Person.Phone = txtPhone.Text;
                     dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
                     dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
                    _Person.DateOfBirth = dtpDateOfBirth.MaxDate;
                    _Person.NationalityCountryID = CountryId;
                    _Person.NationalNo = txtNationalNo.Text;
                    _Person.Gendor = rbMale.Checked ? 0 : 1;
                    if (pbPicture.Image == null)
                    {
                        _Person.ImagePath = "";
                    }
                    else
                    {
                        _Person.ImagePath = pbPicture.ImageLocation;
                    }

                    {
                        if (_Person.Save())
                        {
                            MessageBox.Show("New Person Added Successfully","Added",MessageBoxButtons.OK,MessageBoxIcon.Information );
                            lblId.Text = _Person.PersonID.ToString();
                            Mode = enMode.Update;

                            lblText.Text = "Edit Person's Info";
                            DataBack?.Invoke( _Person.PersonID);

                            return;
                         }
                        else
                        {
                            MessageBox.Show("Can't Add Person", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                case enMode.Update:
                    {
             
                        int CountrID = clsBuessenessCountriesManagement.Find(cbCountries.Text).ID;

                        int ID = int.Parse(lblId.Text);
                        int _Gender = rbMale.Checked ? 0 : 1;
                        clsBussenessPeopleManegement Edi = new clsBussenessPeopleManegement(ID, txtFirst.Text, txtSecond.Text, txtTheird.Text, txtLast.Text,
                          txtNationalNo.Text, dtpDateOfBirth.Value, _Gender, txtAdderess.Text, txtPhone.Text, txtEmail.Text, CountrID, pbPicture.ImageLocation);
                        if (Edi.Save())
                        {
                             MessageBox.Show("Person Updated Successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if(Edi.ImagePath == null)
                            {
                                _Person.ImagePath = "";
                                return;
                            }
                            _Person.ImagePath = Edi.ImagePath;
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Can't Update Person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                             return;
                        }
                    }
            }
        }

        private void lblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPicture .ImageLocation = null;


            if (rbMale.Checked)
                pbPicture.Image =Properties.Resources.Male_512;
            else
                pbPicture.Image = Properties.Resources.Female_512;

            lblRemove.Visible = false;
        }


        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            pbPicture.Image = Properties.Resources.Male_512;

        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            pbPicture.Image = Properties.Resources.Female_512;

        }
 
        private void txtFirst_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirst.Text) || txtFirst.Text.Any(char.IsDigit))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFirst, "Enter a valid name without numbers.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFirst, "");
            }
        }

        private void txtSecond_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSecond.Text) || txtSecond.Text.Any(char.IsDigit))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtSecond, "Enter a valid name without numbers.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtSecond, "");
            }
        }
 
        private void txtLast_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLast.Text) || txtLast.Text.Any(char.IsDigit))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLast, "Enter a valid name without numbers.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtLast, "");
            }
        }


        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National number is required.");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, "");
            }

            if ((txtNationalNo.Text.Trim() != _Person.NationalNo && clsBussenessPeopleManegement.isPersonExist(txtNationalNo.Text)))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number already Exsit.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, "");
            }
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
                return;
            string email = txtEmail.Text.Trim();
            string pattern = @"@.com";

            if (!Regex.IsMatch(email, pattern))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid email format.");
            }

            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, "");

            }
        }

        private void txtAdderess_Validating(object sender, CancelEventArgs e)
        {

        }
    }
}
