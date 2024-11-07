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

namespace CTAProject.Pages
{
    public partial class WebForm2 : System.Web.UI.Page
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
        private int MenuID;
        private string MenuName;
        private CeylonAdaptor[] ImageArray;
        private CeylonMiniAdaptor[] GradeArray;
        private CeylonAdaptor[] MenuArray;
        public String imgString;
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
                MenuID = Convert.ToInt32(Request.QueryString["MenuID"]);
                MenuName= Convert.ToString(Request.QueryString["MenuName"]);
                OrderDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<CeylonAdaptor>(Session["" + SessionID + ""].ToString());

                OrderDetails.FieldI2 = MenuID;
                OrderDetails.FieldS1 = MenuName;
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "0 Quantity Selected", "alert('"+ HttpUtility.HtmlEncode(MenuName)  + "')", true);
                Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);
                ZXGetAllPastryCategoryForMainMenu();
                GetAllPastryForSalesByParaCategory();


                ((MasterPage)Master).FilltheOrderTable();




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





         public void ZXGetAllPastryCategoryForMainMenu()
    {
        try
        {
            OrderDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<CeylonAdaptor>(Session["" + SessionID + ""].ToString());
            string str = "";

                MenuArray = aManager_DAO.ZXGetAllPastryCategoryForMainMenu();

            if (MenuArray == null || MenuArray.Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "0 Quantity Selected", "alert('Menu Not Found')", true);

            }
            else
            {

                for (int i = 0; i < MenuArray.Length; i++)
                {                    

                        str += "<a href='/Pages/SubMenu.aspx?ssid=" + SessionID.ToString() + "&MenuID=" + MenuArray[i].FieldI1 + "&MenuName=" + HttpUtility.UrlEncode(MenuArray[i].FieldS1) + "' ' onclick='ShowLoading()'>";
                        str += HttpUtility.HtmlEncode(MenuArray[i].FieldS1);
                        str += "</a>";

                    }
                    MenuPanel.InnerHtml = str;

                  

                }



        }
        catch (Exception Ex)
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "0 Quantity Selected", "alert('Menu Error')", true);
        }

    }
    public void GetAllPastryForSalesByParaCategory()
        {
            try
            {
                OrderDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<CeylonAdaptor>(Session["" + SessionID + ""].ToString());
                string str2 = "";

               

                GradeArray = aManager_DAO.GetAllPastryForSalesByParaCategory(MenuName);

                if (GradeArray == null || GradeArray.Length == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "0 Quantity Selected", "alert('Items Not Found')", true);

                }
                else
                {
                    for (int i = 0; i < GradeArray.Length; i++)
                    {

                       
                        if (File.Exists(Server.MapPath("/ItemImages/" + GradeArray[i].FieldI1 + ".jpg")))
                        {
                            imgString = "/ItemImages/" + GradeArray[i].FieldI1 + ".jpg";
                        }
                        else
                        {
                            imgString = "/images/NOimage.png";
                        }
                            

                        str2 += "<div class='col'>";
                        str2 += "<div class='card mb-5 box-shadow third' style = 'width:327px;border-radius: 8px;'>";

                        str2 += "<a href='/Pages/JustItem.aspx?ssid=" + SessionID.ToString() + "&ItemID=" + GradeArray[i].FieldI1 + "&ItemName=" + GradeArray[i].FieldS1 + "&ItemPrice=" + GradeArray[i].FieldD1 + "&SubCat=" + GradeArray[i].FieldS3+ "&MenuName="+ OrderDetails.FieldS1 + "&CATID=3' ' ' onclick='ShowLoading()'>";


                        str2 += "<img src='" + imgString + "' alt='' width='325px' height='325px'/>";
                        str2 += "<p style='color: black; font-size:20px; padding:3px;'> "+ GradeArray[i].FieldS1 + " </p> ";
                        //str2 += "<br>";
                        str2 += "<p style='color: black; font-size:15px; padding:8px;'> From Rs "+ GradeArray[i].FieldD1 + " </p> ";
                        str2 += "</a>";

                        str2 += "</div>";
                        str2 += "</div>";
                    }
                    div_test.InnerHtml = str2;

                }



            }
            catch (Exception Ex)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "0 Quantity Selected", "alert('Items Error')", true);
            }

        }
        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/MainMenu.aspx?ssid=" + SessionID.ToString());
        }

        protected void StartBtn_Click(object sender, EventArgs e)
        {
            ORList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList" + SessionID + ""].ToString());
            Session.Remove("ORList" + SessionID + "");
            ORList.Clear();
            Session["ORList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList);

            ORList2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList2" + SessionID + ""].ToString());
            Session.Remove("ORList2" + SessionID + "");
            ORList2.Clear();
            Session["ORList2" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList2);

            OrderDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<CeylonAdaptor>(Session["" + SessionID + ""].ToString());
            OrderDetails.FieldI6 = 0; //reset Secret Key
            Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);
            Response.Redirect("~/Pages/Login.aspx?ssid=" + SessionID.ToString());
        }
    }
}