using System;
using Microsoft.VisualBasic;
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
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using System.IO;
using System.Data;

namespace CTAProject.Pages
{
    public partial class AdminDashboard : System.Web.UI.MasterPage
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
        private CeylonAdaptor[] ExpenseTypeArray;
        private CeylonAdaptor[] ExpenseResultArray;
        private CeylonAdaptor[] SchoolArray;
        private CeylonAdaptor[] LocationArray;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                getComputerName();
                aManager_DAO = new Manager_DAO();
                SessionID = Convert.ToInt32(Request.QueryString["ssid"]);
                OrderDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<CeylonAdaptor>(Session["" + SessionID + ""].ToString());




            }
            catch (Exception Ex)
            {
                Response.Redirect("~/Pages/Login.aspx");
            }
        }

        public void getComputerName()
        {
            LocalDataSource = System.Environment.MachineName;
        }
    }
}