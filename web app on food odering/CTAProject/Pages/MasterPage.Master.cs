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
    public partial class MasterPage : System.Web.UI.MasterPage
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
        private List<CeylonAdaptor> ORList2;
        private CeylonAdaptor[] OrderArray2;
        private List<CeylonAdaptor> CartList;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                getComputerName();
                aManager_DAO = new Manager_DAO();
                SessionID = Convert.ToInt32(Request.QueryString["ssid"]);
                OrderDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<CeylonAdaptor>(Session["" + SessionID + ""].ToString());


                FilltheOrderTable();
                ORList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList" + SessionID + ""].ToString());
                Session.Remove("ORList" + SessionID + "");
                ORList.Clear();
                Session["ORList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList);


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


        public void FilltheOrderTable()
        {
            try
            {
                decimal Total = 0;
                CartList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["CartList" + SessionID + ""].ToString());

              
                CeylonAdaptor[] OrderArray = CartList.ToArray();
             
                decimal GrandTotal = 0;

                if (OrderArray == null || OrderArray.Length == 0) {


                    TotalPriceLBL.Text = "Rs " + "0.00/ ";
                    ItemCountLBL.Text = "0" + " Items";

                }
                else
                {              
                for (int i = 0; i < OrderArray.Length; i++)
                {
                                
                    GrandTotal = GrandTotal + OrderArray[i].FieldD3;
                }

                TotalPriceLBL.Text = "Rs " + GrandTotal.ToString()+"/ ";
                ItemCountLBL.Text = OrderArray.Length.ToString() + " Items";
                OrderDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<CeylonAdaptor>(Session["" + SessionID + ""].ToString());

               

                OrderDetails.FieldD2 = 0;
                OrderDetails.FieldD3 = 0;
                OrderDetails.FieldI4 = 0;// button clicked false

                Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);
                  
                }

            }
            catch (Exception Ex)
            {

                // GridView1.Visible = false;
                TotalPriceLBL.Text = "Rs " + "0.00";
                ItemCountLBL.Text = "0" + " Items";
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Add to Cart Error", "alert('No Results')", true);


            }
        }

        
        protected void CartClick(object sender, EventArgs e)
        {
            Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);
            Response.Redirect("~/Pages/Checkout.aspx?ssid=" + SessionID.ToString());
        }

    }
}