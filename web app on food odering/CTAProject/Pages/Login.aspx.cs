using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using CTAProject_ClassLibrary.DAOs;
using CTAProject_ClassLibrary.BusinessObjects;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net;
using RestSharp;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using EO.WebBrowser.DOM;
using System.Net.Mail;

namespace CTAProject.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        private int SessionID;
        private Manager_DAO aManager_DAO;
        private string DataSource;
        private string LocalDataSource;
        private CeylonAdaptor OrderDetails;
        private CeylonMiniAdaptor[] CheckAmount;
        CeylonMiniAdaptor[] UserArray;
        private CeylonAdaptor[] SaleDetailsArray;
        private List<CeylonAdaptor> ORList;
        private CeylonAdaptor[] SchoolArray;
        private string TokenID;
        private CeylonAdaptor[] LocationArray;
        private int UserID = 0;
        private List<CeylonAdaptor> ORList2;
        private List<CeylonAdaptor> CartList;
        private List<CeylonAdaptor> ActualCartList;
        private List<CeylonAdaptor> PaymentList;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                getComputerName();
                aManager_DAO = new Manager_DAO();

                /***** Creating Unqiue SessionID And Details ****/
                DateTime Today = DateTime.Today;
                string zeroBased = Today.ToString("yy-MM-dd");
                zeroBased = Regex.Replace(zeroBased, @"[^0-9]", "") + "001";
                int CheckSessionID = Convert.ToInt32(zeroBased);

                CheckAmount = aManager_DAO.GetSessionInformation();
                string DateCheck = DateTime.Now.ToShortDateString();
                DateTime NewDate = Convert.ToDateTime(DateCheck);
                DateCheck = NewDate.ToString("MM/dd/yyyy");

                if (DateCheck == CheckAmount[0].FieldS1)
                {
                    CeylonMiniAdaptor aUpdate = new CeylonMiniAdaptor();
                    aUpdate.FieldD1 = CheckAmount[0].FieldD1 + 1;
                    aUpdate.FieldS1 = DateCheck;
                    SessionID = Convert.ToInt32(aUpdate.FieldD1);
                    aManager_DAO.UpdateSessionID(aUpdate);
                }
                else
                {
                    CeylonMiniAdaptor aUpdate = new CeylonMiniAdaptor();
                    aUpdate.FieldD1 = CheckSessionID;
                    aUpdate.FieldS1 = DateCheck;
                    SessionID = Convert.ToInt32(aUpdate.FieldD1);
                    aManager_DAO.UpdateSessionID(aUpdate);

                }

                OrderDetails = new CeylonAdaptor();
                OrderDetails.FieldI1 = SessionID;
                OrderDetails.FieldI11 = 0;
                OrderDetails.FieldDate1 = DateTime.Now;
                OrderDetails.FieldD10 = 0;
                OrderDetails.FieldD5 = 0;
                OrderDetails.FieldD6 = 0;
                OrderDetails.FieldD7 = 0;
                OrderDetails.FieldD8 = 0;
                OrderDetails.FieldD9 = 0;

                Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);

                ORList = new List<CeylonAdaptor>();
                Session["ORList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList);

                ORList2 = new List<CeylonAdaptor>();
                Session["ORList2" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList2);


                CartList = new List<CeylonAdaptor>();
                Session["CartList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(CartList);

                ActualCartList = new List<CeylonAdaptor>();
                Session["ActualCartList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ActualCartList);

                PaymentList = new List<CeylonAdaptor>();
                Session["PaymentList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(PaymentList);



                // GetLastSchoolDetals();

            }
            catch (Exception Ex)
            {
                Response.Write("Server Not Found");
            }
        }
        public void getComputerName()
        {
            LocalDataSource = System.Environment.MachineName;
        }
      
        public void GetLastSchoolDetals()
        {
            try
            {
                SchoolArray = aManager_DAO.GetLastSchoolDetals();
                DataTable aAgent = new DataTable();

                aAgent.Columns.Add(new DataColumn("Image", typeof(string)));


                for (int i = 0; i < SchoolArray.Length; i++)
                {
                    aAgent.Rows.Add(
                    SchoolArray[i].FieldByte1
                     );
                    byte[] bytes = SchoolArray[i].FieldByte1;
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                   // Image3.ImageUrl = "data:image/png;base64," + base64String;

                }
            }
            catch (Exception Ex)
            {


            }

        }

       
      

        protected void StaffClick(object sender, EventArgs e)
        {
            StartPanel.Visible = false;
            LoginPanel.Visible = true;
        }
        protected void BackClick(object sender, EventArgs e)
        {
            StartPanel.Visible = true;
            LoginPanel.Visible = false;
        }

        protected void StartBtn_Click1(object sender, EventArgs e)
        {
            Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);
            Response.Redirect("~/Pages/MainMenu.aspx?ssid=" + SessionID.ToString());
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);
            Response.Redirect("~/Pages/ItemImages.aspx?ssid=" + SessionID.ToString());
        }
    }
}