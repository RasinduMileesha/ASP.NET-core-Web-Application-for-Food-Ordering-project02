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
    public partial class WebForm1 : System.Web.UI.Page
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
        private CeylonAdaptor[] GradeArray;
        private CeylonAdaptor[] ImageArray;
        public String imgString;
        private List<CeylonAdaptor> ORList2;
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
                getComputerName();
                aManager_DAO = new Manager_DAO();
                SessionID = Convert.ToInt32(Request.QueryString["ssid"]);
                OrderDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<CeylonAdaptor>(Session["" + SessionID + ""].ToString());

                ZXGetAllPastryCategoryForMainMenu();
                ((MasterPage)Master).FilltheOrderTable();


           
            //Session.Remove("ORList" + SessionID + "");
            //ORList.Clear();
            //Session["ORList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList);

            //}
            //catch (Exception Ex)
            //{
            //    Response.Redirect("~/Pages/Login.aspx");
            //}
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
                    //Image1.ImageUrl = "data:image/png;base64," + base64String;

                }
            }
            catch (Exception Ex)
            {


            }

        }
        public void ZXGetAllPastryCategoryForMainMenu()
        {
            try
            {
                OrderDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<CeylonAdaptor>(Session["" + SessionID + ""].ToString());
                string str = "";

                GradeArray = aManager_DAO.ZXGetAllPastryCategoryForMainMenu();

                if (GradeArray == null || GradeArray.Length == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "0 Quantity Selected", "alert('Menu Not Found')", true);

                }
                else
                {

                    for (int i = 0; i < GradeArray.Length; i++)
                    {

                
                        if (File.Exists(Server.MapPath("/MenuImages/" + GradeArray[i].FieldI1 + ".jpg")))
                        {
                            imgString = "/MenuImages/" + GradeArray[i].FieldI1 + ".jpg";
                        }
                        else
                        {
                            imgString = "/images/NOimage.png";
                        }
                        //  imgString = "/images/download.png";

                        str += "<div class='col'>";
                        str += "<div class='card mb-5 box-shadow third' style = 'width:245px;border-radius:8px;'>";
                        str += "<a href='/Pages/SubMenu.aspx?ssid=" + SessionID.ToString() + "&MenuID=" + GradeArray[i].FieldI1+ "&MenuName=" + HttpUtility.UrlEncode(GradeArray[i].FieldS1) + "' ' onclick='ShowLoading()' > ";
                        str += "<img src='" + imgString + "' alt='' width='240px' height='220px'/>";
                        str += "<p style='color: black; font-size:18px; padding:8px;text-decoration: none'> <b>" + GradeArray[i].FieldS1 + "</b> </p> ";
                        str += "</a>";

                        str += "</div>";
                        str += "</div>";
                    }
                    div_test.InnerHtml = str;
                   
                }



            }
            catch (Exception Ex)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "0 Quantity Selected", "alert('Menu Error')", true);
            }

        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Login.aspx?ssid=" + SessionID.ToString());
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