using CTAProject_ClassLibrary.BusinessObjects;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CTAProject_ClassLibrary.DAOs
{
    public class Login_DAO
    {
        // Creates variables for Connect to the database,return messages and ect
        private readonly byte _DataBase_Error_Code;
        private string _DataBase_Result_Message;
        private ConnectionStringSettings _aConnectionString;
        private SqlConnection _aSqlConnection;
        private SqlCommand _aSqlCommand;
        private SqlDataReader _aSqlReader;
        private string LocalDataBaseAccess;
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
        public Login_DAO()
        {
            _DataBase_Error_Code = 255;
            _DataBase_Result_Message = "";
            getComputerName();

            DataSource = "Data Source=yourserver,1433;Initial Catalog=db;Persist Security Info=True;User ID=username;Password=pass;";

        }//End of Defult Constructor 
        public void getComputerName()
        {
            LocalDataSource = System.Environment.MachineName;
        }//End of getComputerName() method 
        public CeylonAdaptor[] GetLoginDetails(string username, string password)
        {
            try
            {

                ArrayList StaffDetails = new ArrayList();

                // Create new Databse Connection
                aSqlConnection = new SqlConnection(LocalDataBaseAccess);

                // Open the Database Connection
                aSqlConnection.Open();

                // Create command to get the all centers into the arraylist
                aSqlCommand = new SqlCommand("GetStaffDetailsViaUandP", aSqlConnection);

                aSqlCommand.CommandType = CommandType.StoredProcedure;
                aSqlCommand.Parameters.Add("@Username", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Password", SqlDbType.VarChar);
                aSqlCommand.Parameters["@Username"].Value = username;
                aSqlCommand.Parameters["@Password"].Value = password;

                aSqlCommand.CommandType = CommandType.StoredProcedure;
                aSqlDataReader = aSqlCommand.ExecuteReader();

                while (aSqlDataReader.Read())
                {
                    CeylonAdaptor aLoginUser = new CeylonAdaptor();

                    aLoginUser.FieldI1 = aSqlDataReader.GetInt32(0);
                    aLoginUser.FieldS1 = aSqlDataReader.GetString(1);
                    aLoginUser.FieldS2 = aSqlDataReader.GetString(2);
                    aLoginUser.FieldI2 = aSqlDataReader.GetInt32(3);
                    aLoginUser.FieldI3 = aSqlDataReader.GetInt32(4);
                    aLoginUser.FieldI4 = aSqlDataReader.GetInt32(5);
                    aLoginUser.FieldI5 = aSqlDataReader.GetInt32(6);
                    aLoginUser.FieldI6 = aSqlDataReader.GetInt32(7);
                    aLoginUser.FieldI7 = aSqlDataReader.GetInt32(8);
                    aLoginUser.FieldI8 = aSqlDataReader.GetInt32(9);
                    aLoginUser.FieldI9 = aSqlDataReader.GetInt32(10);
                    aLoginUser.FieldI10 = aSqlDataReader.GetInt32(11);
                    aLoginUser.FieldI11 = aSqlDataReader.GetInt32(12);
                    aLoginUser.FieldI12 = aSqlDataReader.GetInt32(13);
                    aLoginUser.FieldI13 = aSqlDataReader.GetInt32(14);
                    aLoginUser.FieldI14 = aSqlDataReader.GetInt32(15);
                    aLoginUser.FieldI15 = aSqlDataReader.GetInt32(16);
                    aLoginUser.FieldI16 = aSqlDataReader.GetInt32(17);
                    aLoginUser.FieldI17 = aSqlDataReader.GetInt32(18);
                    aLoginUser.FieldI18 = aSqlDataReader.GetInt32(19);
                    aLoginUser.FieldI19 = aSqlDataReader.GetInt32(20);
                    aLoginUser.FieldI20 = aSqlDataReader.GetInt32(21);
                    aLoginUser.FieldI21 = aSqlDataReader.GetInt32(22);
                    aLoginUser.FieldI22 = aSqlDataReader.GetInt32(23);


                    StaffDetails.Add(aLoginUser);
                }

                _DataBase_Result_Message = "Database Read Successfully";
                return ((CeylonAdaptor[])StaffDetails.ToArray(typeof(CeylonAdaptor)));

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

        }//End of GetLoginDetails() method
        public bool AddLog(CeylonMiniAdaptor NewLog)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(LocalDataBaseAccess);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("InsertALog", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                aSqlCommand.Parameters.Add("@StaffID", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@LogDate", SqlDbType.DateTime);
                aSqlCommand.Parameters.Add("@LogFunction", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@LogCommand", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Details", SqlDbType.VarChar);

                aSqlCommand.Parameters["@StaffID"].Value = NewLog.FieldI1;
                aSqlCommand.Parameters["@LogDate"].Value = NewLog.FieldDate1;
                aSqlCommand.Parameters["@LogFunction"].Value = NewLog.FieldS1;
                aSqlCommand.Parameters["@LogCommand"].Value = NewLog.FieldS2;
                aSqlCommand.Parameters["@Details"].Value = NewLog.FieldS3;

                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "New Log Added to the Database";
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
        }//End of AddLog(LogClass NewLogClass) Method 
        public bool AddAttendance(CeylonMiniAdaptor NewAttendance)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(LocalDataBaseAccess);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("InsertAAttendance", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                aSqlCommand.Parameters.Add("@InOutDateTime", SqlDbType.DateTime);
                aSqlCommand.Parameters.Add("@FK_StaffId", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@Details1", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Details2", SqlDbType.VarChar);

                aSqlCommand.Parameters["@InOutDateTime"].Value = NewAttendance.FieldDate1;
                aSqlCommand.Parameters["@FK_StaffId"].Value = NewAttendance.FieldI1;
                aSqlCommand.Parameters["@Details1"].Value = NewAttendance.FieldS1;
                aSqlCommand.Parameters["@Details2"].Value = NewAttendance.FieldS2;

                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "New Attendance Added to the Database";
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
        }//End of AddAttendance(CeylonMiniAdaptor NewAttendance) Method 
        public CeylonMiniAdaptor AddPCList(CeylonMiniAdaptor NewLog)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(LocalDataBaseAccess);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("SearchIPAddressAndInsertAPCList", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;
                CeylonMiniAdaptor aPCDetails = new CeylonMiniAdaptor();
                SqlParameter aParameter = new SqlParameter("@N1", SqlDbType.Int);
                SqlParameter aParameter1 = new SqlParameter("@N2", SqlDbType.DateTime);
                SqlParameter aParameter2 = new SqlParameter("@N3", SqlDbType.VarChar, 300);
                SqlParameter aParameter3 = new SqlParameter("@N4", SqlDbType.VarChar, 300);
                SqlParameter aParameter4 = new SqlParameter("@N5", SqlDbType.VarChar, 300);
                SqlParameter aParameter5 = new SqlParameter("@N6", SqlDbType.VarChar, 300);

                aParameter.Direction = ParameterDirection.Output;
                aSqlCommand.Parameters.Add(aParameter);
                aParameter1.Direction = ParameterDirection.Output;
                aSqlCommand.Parameters.Add(aParameter1);
                aParameter2.Direction = ParameterDirection.Output;
                aSqlCommand.Parameters.Add(aParameter2);
                aParameter3.Direction = ParameterDirection.Output;
                aSqlCommand.Parameters.Add(aParameter3);
                aParameter4.Direction = ParameterDirection.Output;
                aSqlCommand.Parameters.Add(aParameter4);
                aParameter5.Direction = ParameterDirection.Output;
                aSqlCommand.Parameters.Add(aParameter5);

                aSqlCommand.Parameters.Add("@SearchParameter", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@RegisterDate", SqlDbType.DateTime);
                aSqlCommand.Parameters.Add("@IPAddress", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Functionality", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Status", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Details", SqlDbType.VarChar);

                aSqlCommand.Parameters["@SearchParameter"].Value = NewLog.FieldS1;
                aSqlCommand.Parameters["@RegisterDate"].Value = NewLog.FieldDate1;
                aSqlCommand.Parameters["@IPAddress"].Value = NewLog.FieldS2;
                aSqlCommand.Parameters["@Functionality"].Value = NewLog.FieldS3;
                aSqlCommand.Parameters["@Status"].Value = NewLog.FieldS4;
                aSqlCommand.Parameters["@Details"].Value = NewLog.FieldS5;

                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "A New Bank Transaction to the Database";

                if (aParameter.Value != DBNull.Value) aPCDetails.FieldI1 = Convert.ToInt32(aParameter.Value);
                if (aParameter1.Value != DBNull.Value) aPCDetails.FieldDate1 = Convert.ToDateTime(aParameter1.Value);
                if (aParameter2.Value != DBNull.Value) aPCDetails.FieldS1 = Convert.ToString(aParameter2.Value);
                if (aParameter3.Value != DBNull.Value) aPCDetails.FieldS2 = Convert.ToString(aParameter3.Value);
                if (aParameter4.Value != DBNull.Value) aPCDetails.FieldS3 = Convert.ToString(aParameter4.Value);
                if (aParameter5.Value != DBNull.Value) aPCDetails.FieldS4 = Convert.ToString(aParameter5.Value);
                return aPCDetails;


            }
            catch (Exception aException)
            {
                _DataBase_Result_Message = "Database Error:-" + aException.Message;
                return (null);
            }
            finally
            {
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }
        }//End of AddLog(LogClass NewLogClass) Method 
        public bool UpdatePCList(CeylonMiniAdaptor NewLog)
        {
            try
            {
                //Create new Database Connection
                aSqlConnection = new SqlConnection(LocalDataBaseAccess);

                //Open the Database Connection
                aSqlConnection.Open();

                //Create COmmand to Add a Council into the Council Table
                aSqlCommand = new SqlCommand("SearchIPAddressAndUpdateAPCList", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                aSqlCommand.Parameters.Add("@SearchParameter", SqlDbType.Int);
                aSqlCommand.Parameters.Add("@IPAddress", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Functionality", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Status", SqlDbType.VarChar);
                aSqlCommand.Parameters.Add("@Details", SqlDbType.VarChar);

                aSqlCommand.Parameters["@SearchParameter"].Value = NewLog.FieldI1;
                aSqlCommand.Parameters["@IPAddress"].Value = NewLog.FieldS1;
                aSqlCommand.Parameters["@Functionality"].Value = NewLog.FieldS2;
                aSqlCommand.Parameters["@Status"].Value = NewLog.FieldS3;
                aSqlCommand.Parameters["@Details"].Value = NewLog.FieldS4;

                aSqlCommand.ExecuteNonQuery();
                _DataBase_Result_Message = "New PC Update to the Database";
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
        }//End of AddLog(LogClass NewLogClass) Method 
        public string CheckAttendanceRecordExisitance(string DataSource, int StaffID, DateTime INOUT)
        {
            // Just read the details of due payments
            try
            {
                string Reply = "";

                // Create new Databse Connection
                aSqlConnection = new SqlConnection(DataSource);

                // Open the Database Connection
                aSqlConnection.Open();

                // Create command to get the all centers into the arraylist
                aSqlCommand = new SqlCommand("CheckAttendanceRecordExisitance", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter aSqlParameter = new SqlParameter();
                aSqlParameter.ParameterName = "@FK_StaffID";
                aSqlParameter.SqlDbType = SqlDbType.Int;
                aSqlParameter.Direction = ParameterDirection.Input;
                aSqlParameter.Value = StaffID;
                aSqlCommand.Parameters.Add(aSqlParameter);
                SqlParameter aSqlParameter3 = new SqlParameter();
                aSqlParameter3.ParameterName = "@INOUT";
                aSqlParameter3.SqlDbType = SqlDbType.DateTime;
                aSqlParameter3.Direction = ParameterDirection.Input;
                aSqlParameter3.Value = INOUT;
                aSqlCommand.Parameters.Add(aSqlParameter3);
                aSqlDataReader = aSqlCommand.ExecuteReader();

                while (aSqlDataReader.Read())
                {

                    Reply = aSqlDataReader.GetString(0);
                }

                _DataBase_Result_Message = "Database Read Successfully";
                return (Reply);

            }
            catch (Exception Ex)
            {
                _DataBase_Result_Message = "Database Error:-" + Ex.Message;
                return ("");
            }
            finally
            {
                // If connection is unclosed, connection will be closed in here
                if (aSqlConnection != null)
                {
                    aSqlConnection.Close();
                }
            }

        }//End of CheckPastryItemIDExsistanceItemID(string DataSource, int ExpectedID) Method
        public CeylonMiniAdaptor CheckLastATTCount(string DataSource)
        {
            // Just read the details of due payments
            try
            {
                CeylonMiniAdaptor Reply = new CeylonMiniAdaptor();

                // Create new Databse Connection
                aSqlConnection = new SqlConnection(DataSource);

                // Open the Database Connection
                aSqlConnection.Open();

                // Create command to get the all centers into the arraylist
                aSqlCommand = new SqlCommand("CheckLastATTCount", aSqlConnection);
                aSqlCommand.CommandType = CommandType.StoredProcedure;
                aSqlDataReader = aSqlCommand.ExecuteReader();

                while (aSqlDataReader.Read())
                {

                    Reply.FieldI1 = aSqlDataReader.GetInt32(0);
                    Reply.FieldDate1 = aSqlDataReader.GetDateTime(1);

                }

                _DataBase_Result_Message = "Database Read Successfully";
                return (Reply);

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

        }//End of CheckPastryItemIDExsistanceItemID(string DataSource, int ExpectedID) Method

    }//End of Login_DAO
}//End of AvenraDistributedERP_ClassLibrary.DAOs
