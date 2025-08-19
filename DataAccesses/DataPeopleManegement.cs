using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccesses
{
    public class clsDataPeopleManegement
    {

        public static DataTable GetAllPeople()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataSettingd.ConnectionString);

            string query = @"SELECT People.PersonID, People.NationalNo,
              People.FirstName, People.SecondName, People.ThirdName, People.LastName,
			  People.DateOfBirth, People.Gendor,  
				  CASE
                  WHEN People.Gendor = 0 THEN 'Male'

                  ELSE 'Female'

                  END as GendorCaption ,
			  People.Address, People.Phone, People.Email, 
              People.NationalityCountryID, Countries.CountryName, People.ImagePath
              FROM            People INNER JOIN
                         Countries ON People.NationalityCountryID = Countries.CountryID
                ORDER BY People.FirstName";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }


        public static int AddNewPerson(string First, string Second, string Therir, string Last, string NationalNumber, int Gender, string Adderes,
        string Email, string PhotoPath, DateTime DateOfBirth, string Phone, int Country)
        {
            //this function will return the new contact id if succeeded and -1 if not.
            int CountryID = -1;

            SqlConnection connection = new SqlConnection(clsDataSettingd.ConnectionString);

            string query = @"INSERT INTO People (NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth,
Gendor,Address,Phone,Email,NationalityCountryID,ImagePath)
                             VALUES (@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName,@DateOfBirth,@Gendor,@Address,@Phone,@Email,
@NationalityCountryID,@ImagePath);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNumber);
            command.Parameters.AddWithValue("@FirstName", First);
            command.Parameters.AddWithValue("@SecondName", Second);
            if (Therir != "" && Therir != null)
                command.Parameters.AddWithValue("@ThirdName", Therir);
            else
                GetParameters(command).AddWithValue("@ThirdName", DBNull.Value);


            command.Parameters.AddWithValue("@LastName", Last);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gender);
            command.Parameters.AddWithValue("@Address", Adderes);
            command.Parameters.AddWithValue("@Phone", Phone);
            if (Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);
            command.Parameters.AddWithValue("@NationalityCountryID", Country);

            if (PhotoPath != "" && PhotoPath != null)
                command.Parameters.AddWithValue("@ImagePath", PhotoPath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);




            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    CountryID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return CountryID;
        }

        private static SqlParameterCollection GetParameters(SqlCommand command)
        {
            return command.Parameters;
        }

        public static bool UpdatePerson(int PersonId, string First, string Second, string Third, string Last, string NationalNumber, int Gender, string Adderes,
        string Email, string PhotoPath, DateTime DateOfBirth, string Phone, int Country)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataSettingd.ConnectionString);

            string query = @"Update  People  
                            set NationalNo = @NationalNo,
                                FirstName = @FirstName, 
                                SecondName = @SecondName, 
                                ThirdName = @ThirdName, 
                                LastName = @LastName, 
                                Email = @Email, 
                                Gendor = @Gender, 
                                Phone = @Phone, 
                                Address = @Address, 
                                DateOfBirth = @DateOfBirth,
                                NationalityCountryID = @NationalityCountryID,
                                ImagePath =@ImagePath
                                where PersonID = @PersonId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonId", PersonId);
            command.Parameters.AddWithValue("@NationalNo", NationalNumber);
            command.Parameters.AddWithValue("@FirstName", First);
            command.Parameters.AddWithValue("@SecondName", Second);
            command.Parameters.AddWithValue("@ThirdName", Third);
            command.Parameters.AddWithValue("@LastName", Last);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Adderes);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@NationalityCountryID", Country);

            if (PhotoPath != "" && PhotoPath != null)
                command.Parameters.AddWithValue("@ImagePath", PhotoPath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool DeletePerson(int PersonId)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataSettingd.ConnectionString);

            string query = @"Delete People 
                                where PersonID = @PersonId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonId", PersonId);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }

        public static bool GetPersonInfoByID(int ID, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
          ref string Email, ref string Phone, ref string Address, ref int Gender,
          ref DateTime DateOfBirth, ref int CountryID, ref string ImagePath, ref string NationalNumber)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataSettingd.ConnectionString);

            string query = "SELECT * FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";

                    }
                    LastName = (string)reader["LastName"];
                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";

                    }
                    Phone = (string)reader["Phone"];
                    Gender = (byte)reader["Gendor"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    CountryID = (int)reader["NationalityCountryID"];
                    NationalNumber = (string)reader["NationalNo"];

                    //ImagePath: allows null in database so we should handle null
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetPersonInfoByNationalNo(string National, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
          ref string Email, ref string Phone, ref string Address, ref int Gender,
          ref DateTime DateOfBirth, ref int CountryID, ref string ImagePath, ref int Id)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataSettingd.ConnectionString);

            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", National);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";

                    }
                    LastName = (string)reader["LastName"];
                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";

                    }
                    Phone = (string)reader["Phone"];
                    Gender = (byte)reader["Gendor"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    CountryID = (int)reader["NationalityCountryID"];
                    Id = (int)reader["PersonID"];

                    //ImagePath: allows null in database so we should handle null
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool IsPersonExistByNationalNo(string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataSettingd.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static bool IsPersonExistById(int Id)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataSettingd.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE PersonId = @PersonId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonId", Id);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

    }
}
