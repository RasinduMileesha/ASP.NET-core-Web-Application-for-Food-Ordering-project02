using CTAProject_ClassLibrary.BusinessObjects;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace CTAProject_ClassLibrary.DAOs
{
    public class Manager_DAO
    {
        // Creates variables for Connect to the database,return messages and ect
        private readonly byte _DataBase_Error_Code;
        private string _DataBase_Result_Message;
        private ConnectionStringSettings _aConnectionString;
        private SqlConnection _aSqlConnection;
        private SqlCommand _aSqlCommand;
        private SqlDataReader _aSqlReader;
        public string DataSource;
        private string LocalDataSource;

        /***** Define Properties *******/
        public byte DataBase_Error_Code

        {
            get
            {
                return _DataBase_Error_Code;
            }
        } // end of DataBase_Error_Code
        public string DataBase_Result_Mesaage
        {
            get
            {
                return _DataBase_Result_Message;
            }
        } // end of DataBase_Result_Mesaage
        public ConnectionStringSettings aConnectionString
        {
            set
            {
                _aConnectionString = value;
            }
            get
            {
                return _aConnectionString;
            }
        } // end of aConnectionString
        public SqlConnection aSqlConnection
        {
            set
            {
                _aSqlConnection = value;
            }

            get
            {
                return _aSqlConnection;
            }
        } // end of aOleDbConnection
        public SqlCommand aSqlCommand
        {
            set
            {
                _aSqlCommand = value;
            }
            get
            {
                return _aSqlCommand;
            }
        } // end of aOleDbCommand
        public SqlDataReader aSqlDataReader
        {
            set
            {
                _aSqlReader = value;
            }
            get
            {
                return _aSqlReader;
            }

        } // end of aOleDbReader
        public Manager_DAO()
        {
            _DataBase_Error_Code = 255;
            _DataBase_Result_Message = "";
            getComputerName();

            DataSource = "Data Source=yourserver,1433;Initial Catalog=db;Persist Security Info=True;User ID=username;Password=pass;";

            //DataSource = "Data Source = " + LocalDataSource + "; Initial Catalog = TruckDB; Integrated Security = True";


        }//End of Defult Constructor 

        public void getComputerName()
        {
            LocalDataSource = System.Environment.MachineName;
        }//End of getComputerName() method    




        //****************  All the Data Read Methods from the Database Using Parameters ************************//
       



        public CeylonMiniAdaptor[] GetSessionInformation()
        {
            // Just read the details of due payments
            try
            {
                ArrayList ListOfItemsForPurchase = new ArrayList();

                // Create new Databse Connection
                aSqlConnection = new SqlConnection(DataSource);

                // Open the Database Connection
                aSqlConnection.Open();

                aSqlCommand = new SqlCommand("GetCurrentSessionCounter", aSqlConnection);


                aSqlCommand.CommandType = CommandType.StoredProcedure;
                aSqlDataReader = aSqlCommand.ExecuteReader();

                while (aSqlDataReader.Read())
                {
                    CeylonMiniAdaptor aObjectForReports = new CeylonMiniAdaptor();

                    aObjectForReports.FieldD1 = aSqlDataReader.GetDecimal(0);
                    aObjectForReports.FieldS1 = aSqlDataReader.GetString(1);
                    ListOfItemsForPurchase.Add(aObjectForReports);
                }

                _DataBase_Result_Message = "Database Read Successfully";
                return ((CeylonMiniAdaptor[])ListOfItemsForPurchase.ToArray(typeof(CeylonMiniAdaptor)));

            }
            catch (Exception Ex)
            {
                _DataBase_Result_Message = "Database Error:-" + Ex.Message;
                return (null);
            }
            finally
            {
                // If connection is unclosed, connection will be closed in here
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }

        }//End of 
        public bool UpdateSessionID(CeylonMiniAdaptor aObject)
        {
            // Just read the details of due payments
            try
            {
                // Create new Databse Connection
                aSqlConnection = new SqlConnection(DataSource);

                // Open the Database Connection
                aSqlConnection.Open();

                // Create command to get the all centers into the arraylist

                aSqlCommand = new SqlCommand("UpdateSessionIDCounter", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                aSqlCommand.Parameters.Add("@Test1", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@Details", SqlDbType.VarChar);
                aSqlCommand.Parameters["@Test1"].Value = aObject.FieldD1;
                aSqlCommand.Parameters["@Details"].Value = aObject.FieldS1;

                aSqlCommand.ExecuteNonQuery();
                return (true);
            }
            catch (Exception Ex)
            {
                _DataBase_Result_Message = "Database Error:-" + Ex.Message;
                return (false);
            }
            finally
            {
                // If connection is unclosed, connection will be closed in here
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }

        }//End 
        public bool InsertToLogTable(CeylonAdaptor NewPay)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(DataSource);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("InsertToLogTable", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;


                aSqlCommand.Parameters.Add("@FK_UserID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@Action", SqlDbType.NVarChar);
                aSqlCommand.Parameters.Add("@Register_Date", SqlDbType.DateTime);

                aSqlCommand.Parameters["@FK_UserID"].Value = NewPay.FieldI1;
                aSqlCommand.Parameters["@Action"].Value = NewPay.FieldS1;
                aSqlCommand.Parameters["@Register_Date"].Value = NewPay.FieldDate1;


                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "Customer Added to the Database";
                return (true);
            }
            catch (Exception aException)
            {
                _DataBase_Result_Message = "Database Error : " + aException.Message;
                return (false);
            }
            finally
            {
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }
        }//End 
        public bool RegisterSChools(CeylonAdaptor NewPay)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(DataSource);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("RegisterSChools", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                aSqlCommand.Parameters.Add("@Image", SqlDbType.VarBinary);
                aSqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar);
                aSqlCommand.Parameters.Add("@Address", SqlDbType.NVarChar);
                aSqlCommand.Parameters.Add("@Tele", SqlDbType.NVarChar);
                aSqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar);
                aSqlCommand.Parameters.Add("@Website", SqlDbType.NVarChar);
                aSqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar);
                aSqlCommand.Parameters.Add("@FK_UserID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@Register_Date", SqlDbType.DateTime);
                aSqlCommand.Parameters.Add("@URL", SqlDbType.NVarChar);

                aSqlCommand.Parameters["@Image"].Value = NewPay.FieldByte1;
                aSqlCommand.Parameters["@Name"].Value = NewPay.FieldS1;
                aSqlCommand.Parameters["@Address"].Value = NewPay.FieldS2;
                aSqlCommand.Parameters["@Tele"].Value = NewPay.FieldS3;
                aSqlCommand.Parameters["@Email"].Value = NewPay.FieldS4;
                aSqlCommand.Parameters["@Website"].Value = NewPay.FieldS5;
                aSqlCommand.Parameters["@Description"].Value = NewPay.FieldS6;
                aSqlCommand.Parameters["@FK_UserID"].Value = NewPay.FieldI1;
                aSqlCommand.Parameters["@Register_Date"].Value = NewPay.FieldDate1;
                aSqlCommand.Parameters["@URL"].Value = NewPay.FieldS7;

                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "Customer Added to the Database";
                return (true);
            }
            catch (Exception aException)
            {
                _DataBase_Result_Message = "Database Error : " + aException.Message;
                return (false);
            }
            finally
            {
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }
        }//End 
        public bool UpdateSChools(CeylonAdaptor NewPay)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(DataSource);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("UpdateSChools", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                aSqlCommand.Parameters.Add("@School_ID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@Image", SqlDbType.VarBinary);
                aSqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar);
                aSqlCommand.Parameters.Add("@Address", SqlDbType.NVarChar);
                aSqlCommand.Parameters.Add("@Tele", SqlDbType.NVarChar);
                aSqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar);
                aSqlCommand.Parameters.Add("@Website", SqlDbType.NVarChar);
                aSqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar);
                aSqlCommand.Parameters.Add("@FK_UserID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@URL", SqlDbType.NVarChar);

                aSqlCommand.Parameters["@School_ID"].Value = NewPay.FieldI1;
                aSqlCommand.Parameters["@Image"].Value = NewPay.FieldByte1;
                aSqlCommand.Parameters["@Name"].Value = NewPay.FieldS1;
                aSqlCommand.Parameters["@Address"].Value = NewPay.FieldS2;
                aSqlCommand.Parameters["@Tele"].Value = NewPay.FieldS3;
                aSqlCommand.Parameters["@Email"].Value = NewPay.FieldS4;
                aSqlCommand.Parameters["@Website"].Value = NewPay.FieldS5;
                aSqlCommand.Parameters["@Description"].Value = NewPay.FieldS6;
                aSqlCommand.Parameters["@FK_UserID"].Value = NewPay.FieldI2;
                aSqlCommand.Parameters["@URL"].Value = NewPay.FieldS7;


                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "Customer Added to the Database";
                return (true);
            }
            catch (Exception aException)
            {
                _DataBase_Result_Message = "Database Error : " + aException.Message;
                return (false);
            }
            finally
            {
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }
        }//End 
        public CeylonAdaptor[] SchoolLogoByID(string SearchParameter)
        {

            try
            {
                ArrayList ListOFCustomer = new ArrayList();

                // Create new Databse Connection
                aSqlConnection = new SqlConnection(DataSource);

                // Open the Database Connection
                aSqlConnection.Open();


                // Check the Parametter is int or string and select the appropriate statement into the arraylist
                aSqlCommand = new SqlCommand("SchoolLogoByID", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter aSqlParameter = new SqlParameter();
                aSqlParameter.ParameterName = "@SearchParameter";
                aSqlParameter.SqlDbType = SqlDbType.VarChar;
                aSqlParameter.Direction = ParameterDirection.Input;
                aSqlParameter.Value = SearchParameter;
                aSqlCommand.Parameters.Add(aSqlParameter);
                aSqlDataReader = aSqlCommand.ExecuteReader();

                while (aSqlDataReader.Read())
                {
                    CeylonAdaptor aCustomerTable = new CeylonAdaptor();


                    aCustomerTable.FieldByte1 = (byte[])aSqlDataReader.GetSqlBinary(0); // image 1

                    ListOFCustomer.Add(aCustomerTable);
                }

                _DataBase_Result_Message = "Database Read Successfully";
                return ((CeylonAdaptor[])ListOFCustomer.ToArray(typeof(CeylonAdaptor)));

            }
            catch (Exception Ex)
            {
                _DataBase_Result_Message = "Database Error:-" + Ex.Message;
                return (null);
            }
            finally
            {
                // If connection is unclosed, connection will be closed in here
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }

        }//End
        public CeylonAdaptor[] GetAllSchools()
        {
            // Just read the details of due payments
            try
            {
                ArrayList ListOfCategory = new ArrayList();

                // Create new Databse Connection
                aSqlConnection = new SqlConnection(DataSource);

                // Open the Database Connection
                aSqlConnection.Open();

                aSqlCommand = new SqlCommand("GetAllSchools", aSqlConnection);


                aSqlCommand.CommandType = CommandType.StoredProcedure;
                aSqlDataReader = aSqlCommand.ExecuteReader();

                while (aSqlDataReader.Read())
                {
                    CeylonAdaptor Agents = new CeylonAdaptor();


                    Agents.FieldI1 = aSqlDataReader.GetInt32(0);// id
                    Agents.FieldS1 = aSqlDataReader.GetString(1);// Name
                    Agents.FieldS2 = aSqlDataReader.GetString(2);// address
                    Agents.FieldS3 = aSqlDataReader.GetString(3);// tele
                    Agents.FieldS4 = aSqlDataReader.GetString(4);// email
                    Agents.FieldS5 = aSqlDataReader.GetString(5);// web
                    Agents.FieldS6 = aSqlDataReader.GetString(6);// descrip
                    Agents.FieldI2 = aSqlDataReader.GetInt32(7);// user id
                    Agents.FieldDate1 = aSqlDataReader.GetDateTime(8);// date
                    Agents.FieldS7 = aSqlDataReader.GetString(9);// logo URL



                    ListOfCategory.Add(Agents);
                }

                _DataBase_Result_Message = "Database Read Successfully";
                return ((CeylonAdaptor[])ListOfCategory.ToArray(typeof(CeylonAdaptor)));

            }
            catch (Exception Ex)
            {
                _DataBase_Result_Message = "Database Error:-" + Ex.Message;
                return (null);
            }
            finally
            {
                // If connection is unclosed, connection will be closed in here
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }

        }
        public CeylonMiniAdaptor[] XUserLogin(string Username, string Passcode)
        {
            // Just read the details of due payments
            try
            {
                ArrayList ListOfCategory = new ArrayList();

                // Create new Databse Connection
                aSqlConnection = new SqlConnection(DataSource);

                // Open the Database Connection
                aSqlConnection.Open();
                aSqlCommand = new SqlCommand("XUserLogin", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter aSqlParameter = new SqlParameter();
                aSqlParameter.ParameterName = "@Username";
                aSqlParameter.SqlDbType = SqlDbType.VarChar;
                aSqlParameter.Direction = ParameterDirection.Input;
                aSqlParameter.Value = Username;
                aSqlCommand.Parameters.Add(aSqlParameter);
                SqlParameter aSqlParameter1 = new SqlParameter();
                aSqlParameter1.ParameterName = "@Password";
                aSqlParameter1.SqlDbType = SqlDbType.VarChar;
                aSqlParameter1.Direction = ParameterDirection.Input;
                aSqlParameter1.Value = Passcode;
                aSqlCommand.Parameters.Add(aSqlParameter1);
                aSqlDataReader = aSqlCommand.ExecuteReader();

                while (aSqlDataReader.Read())
                {
                    CeylonMiniAdaptor aObjectForReports = new CeylonMiniAdaptor();

                    aObjectForReports.FieldI1 = aSqlDataReader.GetInt32(0);//user id
                    aObjectForReports.FieldI2 = aSqlDataReader.GetInt32(1);//Staff ID
                    aObjectForReports.FieldS1 = aSqlDataReader.GetString(2);//user type
                    aObjectForReports.FieldS2 = aSqlDataReader.GetString(3);//staff name


                    ListOfCategory.Add(aObjectForReports);
                }

                _DataBase_Result_Message = "Database Read Successfully";
                return ((CeylonMiniAdaptor[])ListOfCategory.ToArray(typeof(CeylonMiniAdaptor)));

            }
            catch (Exception Ex)
            {
                _DataBase_Result_Message = "Database Error:-" + Ex.Message;
                return (null);
            }
            finally
            {
                // If connection is unclosed, connection will be closed in here
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }

        }//End of  Method
        public CeylonAdaptor[] GetLastSchoolDetals()
        {
            // Just read the details of due payments
            try
            {
                ArrayList ListOfCategory = new ArrayList();

                // Create new Databse Connection
                aSqlConnection = new SqlConnection(DataSource);

                // Open the Database Connection
                aSqlConnection.Open();

                aSqlCommand = new SqlCommand("GetLastSchoolDetals", aSqlConnection);


                aSqlCommand.CommandType = CommandType.StoredProcedure;
                aSqlDataReader = aSqlCommand.ExecuteReader();

                while (aSqlDataReader.Read())
                {
                    CeylonAdaptor Agents = new CeylonAdaptor();



                    Agents.FieldI1 = aSqlDataReader.GetInt32(0);//school id
                    Agents.FieldByte1 = (byte[])aSqlDataReader.GetSqlBinary(1); // image 1              
                    Agents.FieldS1 = aSqlDataReader.GetString(2);// Name
                    Agents.FieldS2 = aSqlDataReader.GetString(3);// address
                    Agents.FieldS3 = aSqlDataReader.GetString(4);// tele
                    Agents.FieldS4 = aSqlDataReader.GetString(5);// email
                    Agents.FieldS5 = aSqlDataReader.GetString(6);// website
                    Agents.FieldS6 = aSqlDataReader.GetString(7);// description
                    Agents.FieldI2 = aSqlDataReader.GetInt32(8);// user id
                    Agents.FieldDate1 = aSqlDataReader.GetDateTime(9);// date
                    Agents.FieldS7 = aSqlDataReader.GetString(10);// img link



                    ListOfCategory.Add(Agents);
                }

                _DataBase_Result_Message = "Database Read Successfully";
                return ((CeylonAdaptor[])ListOfCategory.ToArray(typeof(CeylonAdaptor)));

            }
            catch (Exception Ex)
            {
                _DataBase_Result_Message = "Database Error:-" + Ex.Message;
                return (null);
            }
            finally
            {
                // If connection is unclosed, connection will be closed in here
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }

        }
        public CeylonAdaptor[] XSearchLogByDateRange(string SearchParameter1, string SearchParameter2)
        {

            try
            {
                ArrayList ListOFCustomer = new ArrayList();

                // Create new Databse Connection
                aSqlConnection = new SqlConnection(DataSource);

                // Open the Database Connection
                aSqlConnection.Open();


                // Check the Parametter is int or string and select the appropriate statement into the arraylist
                aSqlCommand = new SqlCommand("XSearchLogByDateRange", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;



                SqlParameter aSqlParamete1 = new SqlParameter();
                aSqlParamete1.ParameterName = "@FromDate";
                aSqlParamete1.SqlDbType = SqlDbType.VarChar;
                aSqlParamete1.Direction = ParameterDirection.Input;
                aSqlParamete1.Value = SearchParameter1;
                aSqlCommand.Parameters.Add(aSqlParamete1);

                SqlParameter aSqlParameter2 = new SqlParameter();
                aSqlParameter2.ParameterName = "@ToDate";
                aSqlParameter2.SqlDbType = SqlDbType.VarChar;
                aSqlParameter2.Direction = ParameterDirection.Input;
                aSqlParameter2.Value = SearchParameter2;
                aSqlCommand.Parameters.Add(aSqlParameter2);
                aSqlDataReader = aSqlCommand.ExecuteReader();

                while (aSqlDataReader.Read())
                {
                    CeylonAdaptor aCustomerTable = new CeylonAdaptor();

                    aCustomerTable.FieldI1 = aSqlDataReader.GetInt32(0);// Log id                 
                    aCustomerTable.FieldS1 = aSqlDataReader.GetString(1);// name
                    aCustomerTable.FieldS2 = aSqlDataReader.GetString(2);// action
                    aCustomerTable.FieldDate1 = aSqlDataReader.GetDateTime(3);// E date


                    ListOFCustomer.Add(aCustomerTable);
                }

                _DataBase_Result_Message = "Database Read Successfully";
                return ((CeylonAdaptor[])ListOFCustomer.ToArray(typeof(CeylonAdaptor)));

            }
            catch (Exception Ex)
            {
                _DataBase_Result_Message = "Database Error:-" + Ex.Message;
                return (null);
            }
            finally
            {
                // If connection is unclosed, connection will be closed in here
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }

        }//End

        public CeylonAdaptor[] ZXGetAllPastryCategoryForMainMenu()
        {
            // Just read the details of due payments
            try
            {
                ArrayList ListOfCategory = new ArrayList();

                // Create new Databse Connection
                aSqlConnection = new SqlConnection(DataSource);

                // Open the Database Connection
                aSqlConnection.Open();

                aSqlCommand = new SqlCommand("ZXGetAllPastryCategoryForMainMenu", aSqlConnection);


                aSqlCommand.CommandType = CommandType.StoredProcedure;
                aSqlDataReader = aSqlCommand.ExecuteReader();

                while (aSqlDataReader.Read())
                {
                    CeylonAdaptor aObjectForReports = new CeylonAdaptor();

                    aObjectForReports.FieldI1 = aSqlDataReader.GetInt32(0);//id
                    aObjectForReports.FieldS1 = aSqlDataReader.GetString(1);//name
                    aObjectForReports.FieldDate1 = aSqlDataReader.GetDateTime(2);
                    aObjectForReports.FieldS2 = aSqlDataReader.GetString(3);
                    aObjectForReports.FieldS3 = aSqlDataReader.GetString(4);
                    ListOfCategory.Add(aObjectForReports);
                }

                _DataBase_Result_Message = "Database Read Successfully";
                return ((CeylonAdaptor[])ListOfCategory.ToArray(typeof(CeylonAdaptor)));

            }
            catch (Exception Ex)
            {
                _DataBase_Result_Message = "Database Error:-" + Ex.Message;
                return (null);
            }
            finally
            {
                // If connection is unclosed, connection will be closed in here
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }

        }//End of  CeylonMiniAdaptor[] GetAllCategory(string DataSource) Method
        public CeylonAdaptor[] ZXGetAllMenuImagesByCategoryName(string SearchParameter)
        {
            // Just read the details of due payments
            try
            {
                ArrayList ListOfCategory = new ArrayList();

                // Create new Databse Connection
                aSqlConnection = new SqlConnection(DataSource);

                // Open the Database Connection
                aSqlConnection.Open();
                aSqlCommand = new SqlCommand("ZXGetAllMenuImagesByCategoryName", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter aSqlParameter = new SqlParameter();
                aSqlParameter.ParameterName = "@SearchParameter";
                aSqlParameter.SqlDbType = SqlDbType.VarChar;
                aSqlParameter.Direction = ParameterDirection.Input;
                aSqlParameter.Value = SearchParameter;
                aSqlCommand.Parameters.Add(aSqlParameter);

                aSqlDataReader = aSqlCommand.ExecuteReader();

                while (aSqlDataReader.Read())
                {
                    CeylonAdaptor aObjectForReports = new CeylonAdaptor();


                    aObjectForReports.FieldByte1 = (byte[])aSqlDataReader.GetSqlBinary(3); // image 1  

                    ListOfCategory.Add(aObjectForReports);
                }

                _DataBase_Result_Message = "Database Read Successfully";
                return ((CeylonAdaptor[])ListOfCategory.ToArray(typeof(CeylonAdaptor)));

            }
            catch (Exception Ex)
            {
                _DataBase_Result_Message = "Database Error:-" + Ex.Message;
                return (null);
            }
            finally
            {
                // If connection is unclosed, connection will be closed in here
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }

        }//End of  Method
        public CeylonMiniAdaptor[] GetAllPastryForSalesByParaCategory(string SearchParameter)
        {
            // Just read the details of due payments
            try
            {
                ArrayList ListOfUsers = new ArrayList();

                // Create new Databse Connection
                aSqlConnection = new SqlConnection(DataSource);

                // Open the Database Connection
                aSqlConnection.Open();



                aSqlCommand = new SqlCommand("GetAllItemForPastrySalesByParaCategory", aSqlConnection);



                // Create command to get the all centers into the arraylist

                aSqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter aSqlParameter = new SqlParameter();
                aSqlParameter.ParameterName = "@SearchParameter";
                aSqlParameter.SqlDbType = SqlDbType.VarChar;
                aSqlParameter.Direction = ParameterDirection.Input;
                aSqlParameter.Value = SearchParameter;
                aSqlCommand.Parameters.Add(aSqlParameter);
                aSqlDataReader = aSqlCommand.ExecuteReader();

                while (aSqlDataReader.Read())
                {
                    CeylonMiniAdaptor PastryItems = new CeylonMiniAdaptor();
                    PastryItems.FieldI1 = aSqlDataReader.GetInt32(0);//item id
                    PastryItems.FieldS1 = aSqlDataReader.GetString(1);//name
                    PastryItems.FieldS2 = aSqlDataReader.GetString(2);//PCategoryName
                    PastryItems.FieldS3 = aSqlDataReader.GetString(3);//PSubCatName
                    PastryItems.FieldS4 = aSqlDataReader.GetString(4);//PFoodName
                    PastryItems.FieldD1 = aSqlDataReader.GetDecimal(5);//RetailPrice
                    PastryItems.FieldD2 = aSqlDataReader.GetDecimal(6);//0.00
                    PastryItems.FieldD3 = aSqlDataReader.GetDecimal(7);//Price1
                    PastryItems.FieldS5 = aSqlDataReader.GetString(8);//Details2
                    PastryItems.FieldS6 = aSqlDataReader.GetString(9);//Details1
                    ListOfUsers.Add(PastryItems);
                }

                _DataBase_Result_Message = "Database Read Successfully";
                return ((CeylonMiniAdaptor[])ListOfUsers.ToArray(typeof(CeylonMiniAdaptor)));

            }
            catch (Exception Ex)
            {
                _DataBase_Result_Message = "Database Error:-" + Ex.Message;
                return (null);
            }
            finally
            {
                // If connection is unclosed, connection will be closed in here
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }

        }//End of CeylonAdaptor[] GetAllUsersByPara(string SearchParameter, string DataSource) Method
        public CeylonAdaptor[] GetAllPastryImagesByID(int SearchParameter)
        {
            // Just read the details of due payments
            try
            {
                ArrayList ListOfPastryItems = new ArrayList();

                // Create new Databse Connection
                aSqlConnection = new SqlConnection(DataSource);

                // Open the Database Connection
                aSqlConnection.Open();

                aSqlCommand = new SqlCommand("GetAllPastryItemImagesByID", aSqlConnection);

                aSqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter aSqlParameter = new SqlParameter();
                aSqlParameter.ParameterName = "@SearchParameter";
                aSqlParameter.SqlDbType = SqlDbType.VarChar;
                aSqlParameter.Direction = ParameterDirection.Input;
                aSqlParameter.Value = SearchParameter;
                aSqlCommand.Parameters.Add(aSqlParameter);
                aSqlDataReader = aSqlCommand.ExecuteReader();

                while (aSqlDataReader.Read())
                {
                    CeylonAdaptor aObjectForReports = new CeylonAdaptor();

                    aObjectForReports.FieldI1 = aSqlDataReader.GetInt32(0);
                    aObjectForReports.FieldS1 = aSqlDataReader.GetString(1);
                    aObjectForReports.FieldI2 = aSqlDataReader.GetInt32(2);
                    //aObjectForReports.FieldByte1 = (byte[])aSqlDataReader.GetSqlBinary(3);
                    aObjectForReports.FieldByte2 = (byte[])aSqlDataReader.GetSqlBinary(3);
                    //aObjectForReports.FieldByte3 = (byte[])aSqlDataReader.GetSqlBinary(5);
                    aObjectForReports.FieldS2 = aSqlDataReader.GetString(4);
                    aObjectForReports.FieldS3 = aSqlDataReader.GetString(5);
                    ListOfPastryItems.Add(aObjectForReports);
                }

                _DataBase_Result_Message = "Database Read Successfully";
                return ((CeylonAdaptor[])ListOfPastryItems.ToArray(typeof(CeylonAdaptor)));

            }
            catch (Exception Ex)
            {
                _DataBase_Result_Message = "Database Error:-" + Ex.Message;
                return (null);
            }
            finally
            {
                // If connection is unclosed, connection will be closed in here
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }

        }//End of Method
        public CeylonAdaptor[] ZGetAllSelectrosFromCategoryTable()
        {
            // Just read the details of due payments
            try
            {
                ArrayList ListOfCategory = new ArrayList();

                // Create new Databse Connection
                aSqlConnection = new SqlConnection(DataSource);

                // Open the Database Connection
                aSqlConnection.Open();

                aSqlCommand = new SqlCommand("ZGetAllSelectrosFromCategoryTable", aSqlConnection);


                aSqlCommand.CommandType = CommandType.StoredProcedure;
                aSqlDataReader = aSqlCommand.ExecuteReader();

                while (aSqlDataReader.Read())
                {
                    CeylonAdaptor aObjectForReports = new CeylonAdaptor();

                    aObjectForReports.FieldI1 = aSqlDataReader.GetInt32(0);//id
                    aObjectForReports.FieldS1 = aSqlDataReader.GetString(1);//catname
                    //aObjectForReports.FieldDate1 = aSqlDataReader.GetDateTime(2);
                    //aObjectForReports.FieldS2 = aSqlDataReader.GetString(3);
                    //aObjectForReports.FieldS3 = aSqlDataReader.GetString(4);
                    ListOfCategory.Add(aObjectForReports);
                }

                _DataBase_Result_Message = "Database Read Successfully";
                return ((CeylonAdaptor[])ListOfCategory.ToArray(typeof(CeylonAdaptor)));

            }
            catch (Exception Ex)
            {
                _DataBase_Result_Message = "Database Error:-" + Ex.Message;
                return (null);
            }
            finally
            {
                // If connection is unclosed, connection will be closed in here
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }

        }//End of  CeylonMiniAdaptor[] GetAllCategory(string DataSource) Method
       
        public int ZInsertToPendingOrderTable(CeylonAdaptor NewPay)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(DataSource);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("ZInsertToPendingOrderTable", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;
                int new_MEM_BASIC_ID = 0;
                SqlParameter aParameter = new SqlParameter("@CID", SqlDbType.Int);
                aParameter.Direction = ParameterDirection.Output;
                aSqlCommand.Parameters.Add(aParameter);

                aSqlCommand.Parameters.Add("@TOKENID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@Register_Date", SqlDbType.DateTime);


                aSqlCommand.Parameters["@TOKENID"].Value = NewPay.FieldI1;
                aSqlCommand.Parameters["@Register_Date"].Value = NewPay.FieldDate1;

                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "Customer Added to the Database";
                if (aParameter.Value != DBNull.Value) new_MEM_BASIC_ID = Convert.ToInt32(aParameter.Value);
                return new_MEM_BASIC_ID;
            }
            catch (Exception aException)
            {
                _DataBase_Result_Message = "Database Error : " + aException.Message;
                return (0);
            }
            finally
            {
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }
        }//End 
        public bool ZInsertToPendingOrderTransTable(CeylonAdaptor NewPay)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(DataSource);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("ZInsertToPendingOrderTransTable", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;





                aSqlCommand.Parameters.Add("@FK_PO_ID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@FK_Item_ID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@Price", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@QTY", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@Total", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@Register_Date", SqlDbType.DateTime);
                aSqlCommand.Parameters.Add("@SK", SqlDbType.NVarChar);


                aSqlCommand.Parameters["@FK_PO_ID"].Value = NewPay.FieldI1;
                aSqlCommand.Parameters["@FK_Item_ID"].Value = NewPay.FieldI2;
                aSqlCommand.Parameters["@Price"].Value = NewPay.FieldD1;
                aSqlCommand.Parameters["@QTY"].Value = NewPay.FieldD2;
                aSqlCommand.Parameters["@Total"].Value = NewPay.FieldD3;
                aSqlCommand.Parameters["@Register_Date"].Value = NewPay.FieldDate1;
                aSqlCommand.Parameters["@SK"].Value = NewPay.FieldS1;


                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "Customer Added to the Database";
                return (true);
            }
            catch (Exception aException)
            {
                _DataBase_Result_Message = "Database Error : " + aException.Message;
                return (false);
            }
            finally
            {
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }
        }//End 
       
        public CeylonMiniAdaptor[] XMealGetAllItemForPastrySalesByPara1(string SearchParameter, int SearchParameter1)// CatName and 
        {
            // Just read the details of due payments
            try
            {
                ArrayList ListOfUsers = new ArrayList();

                // Create new Databse Connection
                aSqlConnection = new SqlConnection(DataSource);

                // Open the Database Connection
                aSqlConnection.Open();

                aSqlCommand = new SqlCommand("XMealGetAllItemForPastrySalesByPara1", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter aSqlParameter = new SqlParameter();
                aSqlParameter.ParameterName = "@SearchParameter";
                aSqlParameter.SqlDbType = SqlDbType.VarChar;
                aSqlParameter.Direction = ParameterDirection.Input;
                aSqlParameter.Value = SearchParameter;

                SqlParameter aSqlParameter1 = new SqlParameter();
                aSqlParameter1.ParameterName = "@SearchParameter1";
                aSqlParameter1.SqlDbType = SqlDbType.Int;
                aSqlParameter1.Direction = ParameterDirection.Input;
                aSqlParameter1.Value = SearchParameter1;
                aSqlCommand.Parameters.Add(aSqlParameter);
                aSqlCommand.Parameters.Add(aSqlParameter1);

                aSqlDataReader = aSqlCommand.ExecuteReader();
                while (aSqlDataReader.Read())
                {
                    CeylonMiniAdaptor PastryItems = new CeylonMiniAdaptor();
                    PastryItems.FieldI1 = aSqlDataReader.GetInt32(0);
                    PastryItems.FieldS1 = aSqlDataReader.GetString(1);
                    PastryItems.FieldD1 = aSqlDataReader.GetDecimal(2);
                    PastryItems.FieldD2 = aSqlDataReader.GetDecimal(3);
                    PastryItems.FieldD3 = aSqlDataReader.GetDecimal(4);
                    PastryItems.FieldS3 = aSqlDataReader.GetString(5);
                    ListOfUsers.Add(PastryItems);
                }

                _DataBase_Result_Message = "Database Read Successfully";
                return ((CeylonMiniAdaptor[])ListOfUsers.ToArray(typeof(CeylonMiniAdaptor)));

            }
            catch (Exception Ex)
            {
                _DataBase_Result_Message = "Database Error:-" + Ex.Message;
                return (null);
            }
            finally
            {
                // If connection is unclosed, connection will be closed in here
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }

        }//End of Method
         //public CeylonMiniAdaptor[] GetAllPastryForSalesByParaCategory(string SearchParameter)
         //{
         //    // Just read the details of due payments
         //    try
         //    {
         //        ArrayList ListOfUsers = new ArrayList();

        //        // Create new Databse Connection
        //        aSqlConnection = new SqlConnection(DataSource);

        //        // Open the Database Connection
        //        aSqlConnection.Open();



        //        aSqlCommand = new SqlCommand("GetAllItemForPastrySalesByParaCategory", aSqlConnection);



        //        // Create command to get the all centers into the arraylist

        //        aSqlCommand.CommandType = CommandType.StoredProcedure;

        //        SqlParameter aSqlParameter = new SqlParameter();
        //        aSqlParameter.ParameterName = "@SearchParameter";
        //        aSqlParameter.SqlDbType = SqlDbType.VarChar;
        //        aSqlParameter.Direction = ParameterDirection.Input;
        //        aSqlParameter.Value = SearchParameter;
        //        aSqlCommand.Parameters.Add(aSqlParameter);
        //        aSqlDataReader = aSqlCommand.ExecuteReader();

        //        while (aSqlDataReader.Read())
        //        {
        //            CeylonMiniAdaptor PastryItems = new CeylonMiniAdaptor();
        //            PastryItems.FieldI1 = aSqlDataReader.GetInt32(0);
        //            PastryItems.FieldS1 = aSqlDataReader.GetString(1);
        //            PastryItems.FieldS2 = aSqlDataReader.GetString(2);
        //            PastryItems.FieldS3 = aSqlDataReader.GetString(3);
        //            PastryItems.FieldS4 = aSqlDataReader.GetString(4);
        //            PastryItems.FieldD1 = aSqlDataReader.GetDecimal(5);
        //            PastryItems.FieldD2 = aSqlDataReader.GetDecimal(6);
        //            PastryItems.FieldD3 = aSqlDataReader.GetDecimal(7);
        //            PastryItems.FieldS5 = aSqlDataReader.GetString(8);
        //            PastryItems.FieldS6 = aSqlDataReader.GetString(9);
        //            ListOfUsers.Add(PastryItems);
        //        }

        //        _DataBase_Result_Message = "Database Read Successfully";
        //        return ((CeylonMiniAdaptor[])ListOfUsers.ToArray(typeof(CeylonMiniAdaptor)));

        //    }
        //    catch (Exception Ex)
        //    {
        //        _DataBase_Result_Message = "Database Error:-" + Ex.Message;
        //        return (null);
        //    }
        //    finally
        //    {
        //        // If connection is unclosed, connection will be closed in here
        //        if (aSqlConnection != null)
        //        {
        //            aSqlConnection.Close();
        //        }
        //    }

        //}//End of CeylonAdaptor[] GetAllUsersByPara(string SearchParameter, string DataSource) Method
        public bool ZInsertToPendingOrderCustomizeTable(CeylonAdaptor NewPay)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(DataSource);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("ZInsertToPendingOrderCustomizeTable", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;





                aSqlCommand.Parameters.Add("@FK_PO_ID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@FK_SK", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@FK_Item_ID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@Price", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@QTY", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@Total", SqlDbType.Decimal);
               


                aSqlCommand.Parameters["@FK_PO_ID"].Value = NewPay.FieldI1;
                aSqlCommand.Parameters["@FK_SK"].Value = NewPay.FieldI2;
                aSqlCommand.Parameters["@FK_Item_ID"].Value = NewPay.FieldI3;
                aSqlCommand.Parameters["@Price"].Value = NewPay.FieldD1;
                aSqlCommand.Parameters["@QTY"].Value = NewPay.FieldD2;
                aSqlCommand.Parameters["@Total"].Value = NewPay.FieldD3;
               


                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "Customer Added to the Database";
                return (true);
            }
            catch (Exception aException)
            {
                _DataBase_Result_Message = "Database Error : " + aException.Message;
                return (false);
            }
            finally
            {
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }
        }//End 
        public CeylonAdaptor[] ZSearchSideAndDrinkCountByItemID(string SearchParameter)
        {
            // Just read the details of due payments
            try
            {
                ArrayList ListOfPastryItems = new ArrayList();

                // Create new Databse Connection
                aSqlConnection = new SqlConnection(DataSource);

                // Open the Database Connection
                aSqlConnection.Open();

                aSqlCommand = new SqlCommand("ZSearchSideAndDrinkCountByItemID", aSqlConnection);

                aSqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter aSqlParameter = new SqlParameter();
                aSqlParameter.ParameterName = "@SearchParameter";
                aSqlParameter.SqlDbType = SqlDbType.VarChar;
                aSqlParameter.Direction = ParameterDirection.Input;
                aSqlParameter.Value = SearchParameter;
                aSqlCommand.Parameters.Add(aSqlParameter);
                aSqlDataReader = aSqlCommand.ExecuteReader();

                while (aSqlDataReader.Read())
                {
                    CeylonAdaptor aObjectForReports = new CeylonAdaptor();

                    aObjectForReports.FieldI1 = aSqlDataReader.GetInt32(0); // sides                
                    aObjectForReports.FieldI2 = aSqlDataReader.GetInt32(1);//drinks
                   
                    ListOfPastryItems.Add(aObjectForReports);
                }

                _DataBase_Result_Message = "Database Read Successfully";
                return ((CeylonAdaptor[])ListOfPastryItems.ToArray(typeof(CeylonAdaptor)));

            }
            catch (Exception Ex)
            {
                _DataBase_Result_Message = "Database Error:-" + Ex.Message;
                return (null);
            }
            finally
            {
                // If connection is unclosed, connection will be closed in here
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }

        }//End of Method
        public int AddAPastrySaleTable(CeylonMiniAdaptor NewSaleTable)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(DataSource);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("InsertAPastrySale", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;
                int new_MEM_BASIC_ID = 0;
                SqlParameter aParameter = new SqlParameter("@NewBankID", SqlDbType.Int);
                aParameter.Direction = ParameterDirection.Output;
                aSqlCommand.Parameters.Add(aParameter);

                aSqlCommand.Parameters.Add("@PSaleID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@SaleDate", SqlDbType.DateTime);
                aSqlCommand.Parameters.Add("@CustomerType", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@FK_CustomerId", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@CustomerName", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@CustomerAddress", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@CustomerPhone", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@CustomerEmail", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@TotalDueAmount", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@TotalPaidAmount", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@Balance", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@BalancePayDate", SqlDbType.DateTime);
                aSqlCommand.Parameters.Add("@SaleStatus", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@SaleType", SqlDbType.VarChar);


                aSqlCommand.Parameters["@PSaleID"].Value = NewSaleTable.FieldI1;
                aSqlCommand.Parameters["@SaleDate"].Value = NewSaleTable.FieldDate1;
                aSqlCommand.Parameters["@CustomerType"].Value = NewSaleTable.FieldS1;
                aSqlCommand.Parameters["@FK_CustomerId"].Value = NewSaleTable.FieldI2;
                aSqlCommand.Parameters["@CustomerName"].Value = NewSaleTable.FieldS2;
                aSqlCommand.Parameters["@CustomerAddress"].Value = NewSaleTable.FieldI3.ToString();//Table ID
                aSqlCommand.Parameters["@CustomerPhone"].Value = NewSaleTable.FieldD1.ToString();//Expected DeliveryTime
                aSqlCommand.Parameters["@CustomerEmail"].Value = NewSaleTable.FieldS3;//Special Requests
                aSqlCommand.Parameters["@TotalDueAmount"].Value = NewSaleTable.FieldD1;//Expected Time
                aSqlCommand.Parameters["@TotalPaidAmount"].Value = 0;
                aSqlCommand.Parameters["@Balance"].Value = 0;
                aSqlCommand.Parameters["@BalancePayDate"].Value = NewSaleTable.FieldDate1;
                aSqlCommand.Parameters["@SaleStatus"].Value = NewSaleTable.FieldS4;//Order Type
                aSqlCommand.Parameters["@SaleType"].Value = NewSaleTable.FieldS5;//Print Status


                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "A New Pastry Sale Transaction Added to the Database";
                if (aParameter.Value != DBNull.Value) new_MEM_BASIC_ID = Convert.ToInt32(aParameter.Value);
                return new_MEM_BASIC_ID;
            }
            catch (Exception aException)
            {
                _DataBase_Result_Message = "Database Error : " + aException.Message;
                return (0);
            }
            finally
            {
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }
        }//End of AddASaleTable(SaleTable NewSaleTable, string DataSource) Method 


        public int AddAPastrySaleTransaction(CeylonAdaptor NewSaleTransaction)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(DataSource);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("InsertAPastrySaleTransaction", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                int new_MEM_BASIC_ID = 0;
                SqlParameter aParameter = new SqlParameter("@NewBankID", SqlDbType.Int);
                aParameter.Direction = ParameterDirection.Output;
                aSqlCommand.Parameters.Add(aParameter);

                aSqlCommand.Parameters.Add("@SaleDateDate", SqlDbType.DateTime);
                aSqlCommand.Parameters.Add("@FK_SaleID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@FK_ItemID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@RetailPrice", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@UnitPrice", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@SellingQuantity", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@FreeQuantity", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@SubTotal", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@Discount", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@TotalWithoutTax", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@ProfitWithoutTax", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@VAT", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@NBT", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@STax1", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@STax2", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@ServiceCharge", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@TotalWithTax", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@ProfitWithTax", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@Details1", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Details2", SqlDbType.VarChar);

                aSqlCommand.Parameters["@SaleDateDate"].Value = NewSaleTransaction.FieldDate1;
                aSqlCommand.Parameters["@FK_SaleID"].Value = NewSaleTransaction.FieldI1;
                aSqlCommand.Parameters["@FK_ItemID"].Value = NewSaleTransaction.FieldI2;
                aSqlCommand.Parameters["@RetailPrice"].Value = NewSaleTransaction.FieldD1;
                aSqlCommand.Parameters["@UnitPrice"].Value = NewSaleTransaction.FieldD2;
                aSqlCommand.Parameters["@SellingQuantity"].Value = NewSaleTransaction.FieldD3;
                aSqlCommand.Parameters["@FreeQuantity"].Value = NewSaleTransaction.FieldD4;
                aSqlCommand.Parameters["@Discount"].Value = NewSaleTransaction.FieldD5;
                aSqlCommand.Parameters["@SubTotal"].Value = NewSaleTransaction.FieldD6;
                aSqlCommand.Parameters["@TotalWithoutTax"].Value = NewSaleTransaction.FieldD7;
                aSqlCommand.Parameters["@ProfitWithoutTax"].Value = NewSaleTransaction.FieldD8;
                aSqlCommand.Parameters["@VAT"].Value = NewSaleTransaction.FieldD9;
                aSqlCommand.Parameters["@NBT"].Value = NewSaleTransaction.FieldD10;
                aSqlCommand.Parameters["@STax1"].Value = NewSaleTransaction.FieldD11;
                aSqlCommand.Parameters["@STax2"].Value = NewSaleTransaction.FieldD12;
                aSqlCommand.Parameters["@ServiceCharge"].Value = NewSaleTransaction.FieldD13;
                aSqlCommand.Parameters["@TotalWithTax"].Value = NewSaleTransaction.FieldD14;
                aSqlCommand.Parameters["@ProfitWithTax"].Value = NewSaleTransaction.FieldD15;
                aSqlCommand.Parameters["@Details1"].Value = NewSaleTransaction.FieldS1;
                aSqlCommand.Parameters["@Details2"].Value = NewSaleTransaction.FieldS2;

                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "A New Pastry Sale Transaction Added to the Database";
                if (aParameter.Value != DBNull.Value) new_MEM_BASIC_ID = Convert.ToInt32(aParameter.Value);
                return new_MEM_BASIC_ID;
            }
            catch (Exception aException)
            {
                _DataBase_Result_Message = "Database Error : " + aException.Message;
                return (0);
            }
            finally
            {
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }
        }//End of AddAPastrySaleTransaction(CeylonAdaptor NewSaleTransaction, string DataSource) Method 
        public bool AddAPastrySalePaymentsTable(CeylonAdaptor NewSalePaymentsTable)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(DataSource);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("InsertAPastrySalePayment", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                aSqlCommand.Parameters.Add("@SPayDate", SqlDbType.DateTime);
                aSqlCommand.Parameters.Add("@FK_SaleID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@FK_CustomerID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@FK_CPID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@PayingAmount", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@PaymentType", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@ChequeNumber", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@ChequeDate", SqlDbType.DateTime);
                aSqlCommand.Parameters.Add("@RealiseDate", SqlDbType.DateTime);
                aSqlCommand.Parameters.Add("@RealiseAmount", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@Status", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Details1", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Details2", SqlDbType.VarChar);


                aSqlCommand.Parameters["@SPayDate"].Value = NewSalePaymentsTable.FieldDate1;
                aSqlCommand.Parameters["@FK_SaleID"].Value = NewSalePaymentsTable.FieldI3;
                aSqlCommand.Parameters["@FK_CustomerID"].Value = NewSalePaymentsTable.FieldI2;
                aSqlCommand.Parameters["@FK_CPID"].Value = NewSalePaymentsTable.FieldI1;
                aSqlCommand.Parameters["@PayingAmount"].Value = NewSalePaymentsTable.FieldD1;
                aSqlCommand.Parameters["@PaymentType"].Value = NewSalePaymentsTable.FieldS1;
                aSqlCommand.Parameters["@ChequeNumber"].Value = NewSalePaymentsTable.FieldS2;
                aSqlCommand.Parameters["@ChequeDate"].Value = NewSalePaymentsTable.FieldDate2;
                aSqlCommand.Parameters["@RealiseDate"].Value = NewSalePaymentsTable.FieldDate3;
                aSqlCommand.Parameters["@RealiseAmount"].Value = NewSalePaymentsTable.FieldD2;
                aSqlCommand.Parameters["@Status"].Value = NewSalePaymentsTable.FieldS3;
                aSqlCommand.Parameters["@Details1"].Value = NewSalePaymentsTable.FieldS4;
                aSqlCommand.Parameters["@Details2"].Value = NewSalePaymentsTable.FieldS5;


                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "A New Pastry Sale Payment Transaction Added to the Database";
                return (true);
            }
            catch (Exception aException)
            {
                _DataBase_Result_Message = "Sale Payment Error : " + aException.Message;
                return (false);
            }
            finally
            {
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }
        }//End of AddASalePaymentsTable(CeylonAdaptor NewSalePaymentsTable, string DataSource) Method 

        public int AddAPastryPendingSaleOrder(CeylonAdaptor NewSaleTable)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(DataSource);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("InsertAPastryPendingSaleOrder", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;
                int new_MEM_BASIC_ID = 0;
                SqlParameter aParameter = new SqlParameter("@NewBankID", SqlDbType.Int);
                aParameter.Direction = ParameterDirection.Output;
                aSqlCommand.Parameters.Add(aParameter);

                aSqlCommand.Parameters.Add("@FK_PRandBSaleID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@OrderDateTime", SqlDbType.DateTime);
                aSqlCommand.Parameters.Add("@PrintStatus", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Details1", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Details2", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@KOT1", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@KOT2", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@KOT3", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@BOT", SqlDbType.Int);

                aSqlCommand.Parameters["@FK_PRandBSaleID"].Value = NewSaleTable.FieldI1;
                aSqlCommand.Parameters["@OrderDateTime"].Value = NewSaleTable.FieldDate1;
                aSqlCommand.Parameters["@PrintStatus"].Value = NewSaleTable.FieldS1;
                aSqlCommand.Parameters["@Details1"].Value = NewSaleTable.FieldS2;
                aSqlCommand.Parameters["@Details2"].Value = NewSaleTable.FieldS3;
                aSqlCommand.Parameters["@KOT1"].Value = NewSaleTable.FieldI2;
                aSqlCommand.Parameters["@KOT2"].Value = NewSaleTable.FieldI3;
                aSqlCommand.Parameters["@KOT3"].Value = NewSaleTable.FieldI4;
                aSqlCommand.Parameters["@BOT"].Value = NewSaleTable.FieldI5;

                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "A New Pending R and B Sale Order Added to the Database";
                if (aParameter.Value != DBNull.Value) new_MEM_BASIC_ID = Convert.ToInt32(aParameter.Value);
                return new_MEM_BASIC_ID;
            }
            catch (Exception aException)
            {
                _DataBase_Result_Message = "Database Error : " + aException.Message;
                return (0);
            }
            finally
            {
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }
        }//End of method





       

        public bool AddStaffDailyTransactionFinalPastry(CeylonMiniAdaptor NewSaleTable)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(DataSource);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("InsertStaffDailyTransactionFinalPastry", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                aSqlCommand.Parameters.Add("@FK_StaffID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@FK_SaleID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@Status", SqlDbType.VarChar);

                aSqlCommand.Parameters["@FK_StaffID"].Value = NewSaleTable.FieldI1;
                aSqlCommand.Parameters["@FK_SaleID"].Value = NewSaleTable.FieldI2;
                aSqlCommand.Parameters["@Status"].Value = NewSaleTable.FieldS1;



                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "A New Table Allocated to the Staff";
                return (true);
            }
            catch (Exception aException)
            {
                _DataBase_Result_Message = "Database Error : " + aException.Message;
                return (false);
            }
            finally
            {
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }
        }//End of method

        public bool AddAPastryStockTransaction(CeylonMiniAdaptor NewPurchaseTransactionTable)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(DataSource);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("InsertAPastryStockTransaction", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                aSqlCommand.Parameters.Add("@FK_StockID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@StockAdjustedDate", SqlDbType.DateTime);
                aSqlCommand.Parameters.Add("@AdjustmentType", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@AdjustmentID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@Debit", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@Credit", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@Details1", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Details2", SqlDbType.VarChar);

                aSqlCommand.Parameters["@FK_StockID"].Value = NewPurchaseTransactionTable.FieldI1;
                aSqlCommand.Parameters["@StockAdjustedDate"].Value = NewPurchaseTransactionTable.FieldDate1;
                aSqlCommand.Parameters["@AdjustmentType"].Value = NewPurchaseTransactionTable.FieldS1;
                aSqlCommand.Parameters["@AdjustmentID"].Value = NewPurchaseTransactionTable.FieldI2;
                aSqlCommand.Parameters["@Debit"].Value = NewPurchaseTransactionTable.FieldD1;
                aSqlCommand.Parameters["@Credit"].Value = NewPurchaseTransactionTable.FieldD2;
                aSqlCommand.Parameters["@Details1"].Value = NewPurchaseTransactionTable.FieldS1;
                aSqlCommand.Parameters["@Details2"].Value = NewPurchaseTransactionTable.FieldS2;

                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "A New Pastry Stock Transaction Added to the Database";
                return (true);
            }
            catch (Exception aException)
            {
                _DataBase_Result_Message = "Database Error : " + aException.Message;
                return (false);
            }
            finally
            {
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }
        }//End of AddAPurchaseTransactionTable(PurchaseTransactionTable NewPurchaseTransactionTable, string DataSource) Method 

        public bool ZXTerminalWiseSalesTableInsert(CeylonMiniAdaptor NewCashBook)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(DataSource);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("ZXTerminalWiseSalesTableInsert", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                aSqlCommand.Parameters.Add("@IPAddress", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@UserID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@DateX", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@DateTimeR", SqlDbType.DateTime);
                aSqlCommand.Parameters.Add("@SaleID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@BusinessType", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@PaymentType", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Amount", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@Status", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@ShiftID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@Details1", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Details2", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Details3", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Details4", SqlDbType.VarChar);


                aSqlCommand.Parameters["@IPAddress"].Value = NewCashBook.FieldS1;
                aSqlCommand.Parameters["@UserID"].Value = NewCashBook.FieldI1;
                aSqlCommand.Parameters["@DateX"].Value = NewCashBook.FieldS2;
                aSqlCommand.Parameters["@DateTimeR"].Value = NewCashBook.FieldDate1;
                aSqlCommand.Parameters["@SaleID"].Value = NewCashBook.FieldI2;
                aSqlCommand.Parameters["@BusinessType"].Value = NewCashBook.FieldS3;
                aSqlCommand.Parameters["@PaymentType"].Value = NewCashBook.FieldS4;
                aSqlCommand.Parameters["@Amount"].Value = NewCashBook.FieldD1;
                aSqlCommand.Parameters["@Status"].Value = NewCashBook.FieldS5;
                aSqlCommand.Parameters["@ShiftID"].Value = NewCashBook.FieldI3;
                aSqlCommand.Parameters["@Details1"].Value = NewCashBook.FieldS6;
                aSqlCommand.Parameters["@Details2"].Value = NewCashBook.FieldS7;
                aSqlCommand.Parameters["@Details3"].Value = NewCashBook.FieldS8;
                aSqlCommand.Parameters["@Details4"].Value = NewCashBook.FieldS9;


                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "A New Terminal Transaction Record Added to the Database";
                return (true);
            }
            catch (Exception aException)
            {
                _DataBase_Result_Message = "Database Error : " + aException.Message;
                return (false);
            }
            finally
            {
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }
        }//End of  Method 
        public bool ZXTerminalWiseSalesTableSHiftEndUpdate(CeylonMiniAdaptor NewCashBook)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(DataSource);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("ZXTerminalWiseSalesTableSHiftEndUpdate", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                aSqlCommand.Parameters.Add("@IPAddress", SqlDbType.VarChar);
                //aSqlCommand.Parameters.Add("@UserID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@DateX", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Status", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@ShiftID", SqlDbType.Int);

                aSqlCommand.Parameters["@IPAddress"].Value = NewCashBook.FieldS1;
                //aSqlCommand.Parameters["@UserID"].Value = NewCashBook.FieldI1;
                aSqlCommand.Parameters["@DateX"].Value = NewCashBook.FieldS2;
                aSqlCommand.Parameters["@Status"].Value = NewCashBook.FieldS3;
                aSqlCommand.Parameters["@ShiftID"].Value = NewCashBook.FieldI2;

                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "Selected Terminal Transactions Updated";
                return (true);
            }
            catch (Exception aException)
            {
                _DataBase_Result_Message = "Database Error : " + aException.Message;
                return (false);
            }
            finally
            {
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }
        }//End of  Method 

        public bool AddACashierTransaction(CeylonMiniAdaptor NewCashBook)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(DataSource);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("InsertACashierTransaction", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                aSqlCommand.Parameters.Add("@FK_StaffID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@BusinessType", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@FK_PSaleID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@PayingAmount", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@Status", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@TransDate", SqlDbType.DateTime);


                aSqlCommand.Parameters["@FK_StaffID"].Value = NewCashBook.FieldI1;
                aSqlCommand.Parameters["@BusinessType"].Value = NewCashBook.FieldS1;
                aSqlCommand.Parameters["@FK_PSaleID"].Value = NewCashBook.FieldI2;
                aSqlCommand.Parameters["@PayingAmount"].Value = NewCashBook.FieldD1;
                aSqlCommand.Parameters["@Status"].Value = NewCashBook.FieldS2;
                aSqlCommand.Parameters["@TransDate"].Value = NewCashBook.FieldDate1;


                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "A New Cashier Transaction Added to the Database";
                return (true);
            }
            catch (Exception aException)
            {
                _DataBase_Result_Message = "Database Error : " + aException.Message;
                return (false);
            }
            finally
            {
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }
        }//End of AddACustomer(CustomerTable NewCustomer, string DataSource) Method 

        public bool AddACashTransaction(CeylonMiniAdaptor NewCashBook)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(DataSource);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("InsertACashTransaction", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                aSqlCommand.Parameters.Add("@RegisterDate", SqlDbType.DateTime);
                aSqlCommand.Parameters.Add("@Description", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Credit", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@Debit", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@Balance", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@Details1", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Details2", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Details3", SqlDbType.VarChar);

                aSqlCommand.Parameters["@RegisterDate"].Value = NewCashBook.FieldDate1;
                aSqlCommand.Parameters["@Description"].Value = NewCashBook.FieldS1;
                aSqlCommand.Parameters["@Credit"].Value = NewCashBook.FieldD1;
                aSqlCommand.Parameters["@Debit"].Value = NewCashBook.FieldD2;
                aSqlCommand.Parameters["@Balance"].Value = NewCashBook.FieldD3;
                aSqlCommand.Parameters["@Details1"].Value = NewCashBook.FieldS1;
                aSqlCommand.Parameters["@Details2"].Value = NewCashBook.FieldS2;
                aSqlCommand.Parameters["@Details3"].Value = NewCashBook.FieldS3;

                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "A New Cash Record Added to the Database";
                return (true);
            }
            catch (Exception aException)
            {
                _DataBase_Result_Message = "Database Error : " + aException.Message;
                return (false);
            }
            finally
            {
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }
        }//End of AddACustomer(CustomerTable NewCustomer, string DataSource) Method 

        public int ZXGetNextOrderID(string SearchParameter)
        {
            // Just read the details of due payments
            try
            {
                int PRID = 0;

                // Create new Databse Connection
                aSqlConnection = new SqlConnection(DataSource);

                // Open the Database Connection
                aSqlConnection.Open();

                // Create command to get the all centers into the arraylist
                aSqlCommand = new SqlCommand("ZXGetNextOrderID", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter aSqlParameter = new SqlParameter();
                aSqlParameter.ParameterName = "@CheckDate";
                aSqlParameter.SqlDbType = SqlDbType.VarChar;
                aSqlParameter.Direction = ParameterDirection.Input;
                aSqlParameter.Value = SearchParameter;
                aSqlCommand.Parameters.Add(aSqlParameter);
                aSqlDataReader = aSqlCommand.ExecuteReader();

                while (aSqlDataReader.Read())
                {

                    PRID = aSqlDataReader.GetInt32(0);
                }

                _DataBase_Result_Message = "Database Read Successfully";
                return (PRID);

            }
            catch (Exception Ex)
            {
                _DataBase_Result_Message = "Database Error:-" + Ex.Message;
                return (0);
            }
            finally
            {
                // If connection is unclosed, connection will be closed in here
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }

        }//End of  Method


        public int GetAPastrySaleID(int CheckingSaleId)
        {
            // Just read the details of due payments
            try
            {
                // StockTable aItemTable = new StockTable();
                int GeneratedAQ = 0;

                // Create new Databse Connection
                aSqlConnection = new SqlConnection(DataSource);

                // Open the Database Connection
                aSqlConnection.Open();

                // Create command to get the all centers into the arraylist
                aSqlCommand = new SqlCommand("CheckPastrySaleIDExsitance", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter aSqlParameter = new SqlParameter();
                aSqlParameter.ParameterName = "@SearchParameter";
                aSqlParameter.SqlDbType = SqlDbType.Int;
                aSqlParameter.Direction = ParameterDirection.Input;
                aSqlParameter.Value = CheckingSaleId;
                aSqlCommand.Parameters.Add(aSqlParameter);

                aSqlDataReader = aSqlCommand.ExecuteReader();

                while (aSqlDataReader.Read())
                {

                    GeneratedAQ = aSqlDataReader.GetInt32(0);
                }

                _DataBase_Result_Message = "Database Read Successfully";
                return (GeneratedAQ);

            }
            catch (Exception Ex)
            {
                _DataBase_Result_Message = "Database Error:-" + Ex.Message;
                return (0);
            }
            finally
            {
                // If connection is unclosed, connection will be closed in here
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }

        }//End of int GetASaleID(int CheckingSaleId, string DataSource) Method

        public int AddAPastrySaleOrder(CeylonAdaptor NewSaleTable)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(DataSource);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("InsertAPastrySaleOrder", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;
                int new_MEM_BASIC_ID = 0;
                SqlParameter aParameter = new SqlParameter("@NewBankID", SqlDbType.Int);
                aParameter.Direction = ParameterDirection.Output;
                aSqlCommand.Parameters.Add(aParameter);

                aSqlCommand.Parameters.Add("@FK_PRandBSaleID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@POrderID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@OrderDepID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@OrderDateTime", SqlDbType.DateTime);
                aSqlCommand.Parameters.Add("@Item", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@Quantity", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@Details1", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Details2", SqlDbType.VarChar);

                aSqlCommand.Parameters["@FK_PRandBSaleID"].Value = NewSaleTable.FieldI1;//Ok
                aSqlCommand.Parameters["@POrderID"].Value = NewSaleTable.FieldI2;//Wrong
                aSqlCommand.Parameters["@OrderDepID"].Value = NewSaleTable.FieldI3;//Wrong
                aSqlCommand.Parameters["@OrderDateTime"].Value = NewSaleTable.FieldDate1;
                aSqlCommand.Parameters["@Item"].Value = NewSaleTable.FieldI4;
                aSqlCommand.Parameters["@Quantity"].Value = NewSaleTable.FieldD1;
                aSqlCommand.Parameters["@Details1"].Value = NewSaleTable.FieldS1;
                aSqlCommand.Parameters["@Details2"].Value = NewSaleTable.FieldS2;

                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "A New R and B Sale Order Added to the Database";
                if (aParameter.Value != DBNull.Value) new_MEM_BASIC_ID = Convert.ToInt32(aParameter.Value);
                return new_MEM_BASIC_ID;
            }
            catch (Exception aException)
            {
                _DataBase_Result_Message = "Database Error : " + aException.Message;
                return (0);
            }
            finally
            {
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }
        }//End of method
        public int GetPastryStockIDByPara(int SearchParameter)
        {
            // Just read the details of due payments
            try
            {
                int StockID = 0;

                // Create new Databse Connection
                aSqlConnection = new SqlConnection(DataSource);

                // Open the Database Connection
                aSqlConnection.Open();

                // Create command to get the all centers into the arraylist
                aSqlCommand = new SqlCommand("GetPastryStocIDByPara", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter aSqlParameter = new SqlParameter();
                aSqlParameter.ParameterName = "@SearchParameter";

                aSqlParameter.SqlDbType = SqlDbType.Int;
                aSqlParameter.Direction = ParameterDirection.Input;
                aSqlParameter.Value = SearchParameter;
                aSqlCommand.Parameters.Add(aSqlParameter);

                aSqlDataReader = aSqlCommand.ExecuteReader();

                while (aSqlDataReader.Read())
                {

                    StockID = aSqlDataReader.GetInt32(0);
                }

                _DataBase_Result_Message = "Database Read Successfully";
                return (StockID);

            }
            catch (Exception Ex)
            {
                _DataBase_Result_Message = "Database Error:-" + Ex.Message;
                return (0);
            }
            finally
            {
                // If connection is unclosed, connection will be closed in here
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }

        }//End of int GetStockIDByPara(string SearchParameter, string DataSource) Method

        public int AddAPastrySaleCustomizationTable(CeylonAdaptor NewSaleTable)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(DataSource);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("InsertAPastrySaleCustomizationTable", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;
                int new_MEM_BASIC_ID = 0;
                SqlParameter aParameter = new SqlParameter("@NewBankID", SqlDbType.Int);
                aParameter.Direction = ParameterDirection.Output;
                aSqlCommand.Parameters.Add(aParameter);

                aSqlCommand.Parameters.Add("@FK_SaleID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@FK_TransID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@FK_ItemID", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@Price", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@QTY", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@Total", SqlDbType.Decimal);
                aSqlCommand.Parameters.Add("@Details1", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Details2", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Details3", SqlDbType.VarChar);

                aSqlCommand.Parameters["@FK_SaleID"].Value = NewSaleTable.FieldI1;//SaleID
                aSqlCommand.Parameters["@FK_TransID"].Value = NewSaleTable.FieldI2;//TransID
                aSqlCommand.Parameters["@FK_ItemID"].Value = NewSaleTable.FieldI3;//ItemID
                aSqlCommand.Parameters["@Price"].Value = NewSaleTable.FieldD1;
                aSqlCommand.Parameters["@QTY"].Value = NewSaleTable.FieldD2;
                aSqlCommand.Parameters["@Total"].Value = NewSaleTable.FieldD3;
                aSqlCommand.Parameters["@Details1"].Value = NewSaleTable.FieldS1;
                aSqlCommand.Parameters["@Details2"].Value = NewSaleTable.FieldS2;
                aSqlCommand.Parameters["@Details3"].Value = NewSaleTable.FieldS3;
                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "A New Sale Customisation Added to the Database";
                if (aParameter.Value != DBNull.Value) new_MEM_BASIC_ID = Convert.ToInt32(aParameter.Value);
                return new_MEM_BASIC_ID;
            }
            catch (Exception aException)
            {
                _DataBase_Result_Message = "Database Error : " + aException.Message;
                return (0);
            }
            finally
            {
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }
        }//End of method

    }//End of Manager_DAO
}//End of namespace 
