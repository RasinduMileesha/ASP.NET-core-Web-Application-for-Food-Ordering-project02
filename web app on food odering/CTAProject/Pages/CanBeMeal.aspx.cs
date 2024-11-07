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
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace CTAProject.Pages
{
    public partial class CanBeMeal : System.Web.UI.Page
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
        int count;
        private int ItemID;
        private string ItemName;
        private string ItemPrice;
        private string SubCat;
        private string CATID;
        private string imgString;
        private CeylonAdaptor[] ImageArray;
        private CeylonMiniAdaptor[] GradeArray;
        private CeylonAdaptor[] MenuArray;
        public String ItemImage;
        private CeylonAdaptor[] OrderArray;
        private decimal selectedTotal = 0;
        // private YourItem[] GradeArray;
        decimal Itemtotal = 0;
        private List<CeylonAdaptor> ORList2;
        private CeylonAdaptor[] OrderArray2;
        private CeylonAdaptor[] CartArray;
        private CeylonAdaptor[] CartArray2;
        public String ItemClicked;
        public String MainItemID;
        public String SKey;
        public String XCustomClick;
        public int SecretKey;
        private List<CeylonAdaptor> CartList;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                getComputerName();
                aManager_DAO = new Manager_DAO();
                SessionID = Convert.ToInt32(Request.QueryString["ssid"]);
                OrderDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<CeylonAdaptor>(Session["" + SessionID + ""].ToString());


                ItemID = Convert.ToInt32(Request.QueryString["ItemID"]);
                ItemPrice = Convert.ToString(Request.QueryString["ItemPrice"]);
                SubCat = Convert.ToString(Request.QueryString["SubCat"]);
                ItemName = Convert.ToString(Request.QueryString["ItemName"]);
                CATID = Convert.ToString(Request.QueryString["CATID"]);
                ItemClicked = Convert.ToString(Request.QueryString["ItemClicked"]);




                OrderDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<CeylonAdaptor>(Session["" + SessionID + ""].ToString());

                OrderDetails.FieldI3 = ItemID;
                OrderDetails.FieldD1 = Convert.ToDecimal(ItemPrice);
                OrderDetails.FieldS2 = SubCat;
                OrderDetails.FieldS3 = ItemName;



                if (Convert.ToInt32(CATID) == 0)
                {
                    CATID = Convert.ToString(OrderDetails.FieldI5);
                }
                else
                {
                    OrderDetails.FieldI5 = Convert.ToInt32(CATID);
                }


                if (!IsPostBack)
                {
                    PriceLBL.Text = ItemPrice;
                    this.CountLBL.Text = "1";
                    OrderDetails.FieldD2 = 0;
                    OrderDetails.FieldD3 = 0;
                    OrderDetails.FieldI4 = 0;// button clicked false

                    NameLBL.Text = ItemName + " £";
                    CanBeMealPanel.Visible = true;
                    ZGetAllSelectrosFromCategoryTable();
                    CartButtonsPanel.Visible = false;

                    OrderDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<CeylonAdaptor>(Session["" + SessionID + ""].ToString());
                    //OrderDetails.FieldI6 = 0;
                    Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);

                    AddtoTempTable();
                  //  FillCart();
                  

                }

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

        protected void CloseBtn_Click(object sender, EventArgs e)
        {
            OrderDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<CeylonAdaptor>(Session["" + SessionID + ""].ToString());

            ORList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList" + SessionID + ""].ToString());
            Session.Remove("ORList" + SessionID + "");
            ORList.Clear();
            Session["ORList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList);


            ORList2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList2" + SessionID + ""].ToString());
            Session.Remove("ORList2" + SessionID + "");
            ORList2.Clear();
            Session["ORList2" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList);


            OrderDetails.FieldD2 = 0;
            OrderDetails.FieldD3 = 0;
            OrderDetails.FieldI4 = 0;// button clicked false

           // OrderDetails.FieldI6 = 0; //reset Secret Key

            Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);
            Response.Redirect("~/Pages/SubMenu.aspx?ssid=" + SessionID.ToString() + "&MenuID=" + OrderDetails.FieldI2 + "&MenuName=" + HttpUtility.UrlEncode(OrderDetails.FieldS1) + "");
        }
       
        public void ZGetAllSelectrosFromCategoryTable()
        {

            CeylonAdaptor[] SelectorArray = aManager_DAO.ZGetAllSelectrosFromCategoryTable();
            if (SelectorArray == null || SelectorArray.Length == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "0 Quantity Selected", "alert('No Selects Found')", true);

            }
            else
            {
                string str = "";

                for (int i = 0; i < SelectorArray.Length; i++)
                {
                    str += "<div class='row' style='width:100%; align='left'>";
                    str += "<div style='width:100%;border: 2px dashed red;'>";
                    str += "<p style='color:red;font-weight:bold; padding:5px;text-align:left;'>  Why Don't you try " + SelectorArray[i].FieldS1 + "";
                    str += "</p><br>";

                    try
                    {

                        GradeArray = aManager_DAO.GetAllPastryForSalesByParaCategory(SelectorArray[i].FieldS1);
                        if (GradeArray == null || GradeArray.Length == 0)
                        {

                        }
                        else
                        {

                            for (int x = 0; x < GradeArray.Length; x++)
                            {

                                //ImageArray = aManager_DAO.GetAllPastryImagesByID(GradeArray[x].FieldI1);
                                //if (ImageArray == null || ImageArray.Length == 0)
                                //{
                                //    imgString = "/images/NOimage.png";
                                //}
                                //else
                                //{
                                //    byte[] bytes = ImageArray[0].FieldByte2;
                                //    OrderDetails.FieldByte1 = ImageArray[0].FieldByte2;
                                //    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                                //    if (base64String == "AAAAAA==")
                                //    {
                                //        imgString = "/images/NOimage.png";
                                //    }
                                //    else
                                //    {
                                //        imgString = "data:image/png;base64," + base64String;
                                //    }

                                //    //   imgString = "/images/NOimage.png";

                                //}
                                
                                if (File.Exists(Server.MapPath("/ItemImages/" + GradeArray[x].FieldI1 + ".jpg")))
                                {
                                    imgString = "/ItemImages/" + GradeArray[x].FieldI1 + ".jpg";
                                }
                                else
                                {
                                    imgString = "/images/NOimage.png";
                                }

                                str += "<div class='col-md-3' style='width:210px;height:320px;'>";
                                str += "<div style='width:198px;height:260px;border-radius:8px;border: 2px solid #DFDFDF;'>";

                                str += "<a href='/Pages/CanBeMeal.aspx?ssid=" + SessionID.ToString() + "&ItemID=" + GradeArray[x].FieldI1 + "&ItemName=" + GradeArray[x].FieldS1 + "&ItemPrice=" + GradeArray[x].FieldD1 + "&SubCat=" + GradeArray[x].FieldS3 + "&CATID=" + CATID + "&MainItemName=" + ItemName + "&ItemClicked=1' ' onclick='ShowLoading()'>";
                                str += "<div style='padding-top: 3px;'><img src='" + imgString + "' alt='' width='185px' height='185px'/></div>";
                                str += "<p style='color: black; font-size:17px; padding:1px;'> " + GradeArray[x].FieldS1 + " </p> ";
                                str += "<p style='color: black; font-size:15px; padding:1px;'> From £" + GradeArray[x].FieldD1 + " </p> ";
                                str += "</a>";
                                str += "</div>";
                                str += "</div>";
                              
                            }
                        }

                    }
                    catch (Exception Ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "0 Quantity Selected", "alert('Items Error')", true);
                    }

                    str += "</div>";
                    str += "</div>";
                    str += "</br>";

                }
                div_test.InnerHtml = str;
            }
        }
        protected void AddBtn_Click(object sender, EventArgs e)
        {

            int val;
            if (int.TryParse(this.CountLBL.Text, out val))
            {

                this.CountLBL.Text = (val + 1).ToString();
                OrderDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<CeylonAdaptor>(Session["" + SessionID + ""].ToString());


                if (Convert.ToInt32(CATID) == 3)//Just a Item
                {
                    ORList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList" + SessionID + ""].ToString());

                    // Find the item in the list by its ItemID
                    CeylonAdaptor itemToUpdate = ORList.Find(item => item.FieldI2 == ItemID);
                    if (itemToUpdate != null)
                    {
                        // Update the quantity
                        itemToUpdate.FieldD2 = Convert.ToDecimal(CountLBL.Text);
                        // Update the total
                        itemToUpdate.FieldD3 = itemToUpdate.FieldD1 * itemToUpdate.FieldD2;

                        // Store the updated list back into the session variable
                        Session.Remove("ORList" + SessionID + "");
                        Session["ORList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList);
                        FilltheOrderTable();
                    }
                    else
                    {
                        // Handle the case when the item with the given ItemID is not found in the list
                        // (e.g., show an error message)
                    }
                }
            }
            else
            {
                this.CountLBL.Text = "1";

            }

        }
        protected void RemoveBtn_Click(object sender, EventArgs e)
        {

            int val;

            if (int.TryParse(this.CountLBL.Text, out val))
            {
                if (val > 1)
                {
                    this.CountLBL.Text = (val - 1).ToString();
                    OrderDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<CeylonAdaptor>(Session["" + SessionID + ""].ToString());
                    if (OrderDetails.FieldI5 == 1)//meal
                    {

                    }
                    if (OrderDetails.FieldI5 == 2)//Can Be meal
                    {
                        ORList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList" + SessionID + ""].ToString());
                        ORList.RemoveAt(1);
                        Session.Remove("ORList" + SessionID + "");
                        Session["ORList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList);
                        FilltheOrderTable();
                        Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);
                    }
                    if (OrderDetails.FieldI5 == 3)//Just a Item
                    {
                        ORList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList" + SessionID + ""].ToString());

                        // Find the item in the list by its ItemID
                        CeylonAdaptor itemToUpdate = ORList.Find(item => item.FieldI2 == ItemID);
                        if (itemToUpdate != null)
                        {
                            // Update the quantity
                            itemToUpdate.FieldD2 = Convert.ToDecimal(CountLBL.Text);
                            // Update the total
                            itemToUpdate.FieldD3 = itemToUpdate.FieldD1 * itemToUpdate.FieldD2;

                            // Store the updated list back into the session variable
                            Session.Remove("ORList" + SessionID + "");
                            Session["ORList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList);
                            FilltheOrderTable();
                        }
                        else
                        {
                            // Handle the case when the item with the given ItemID is not found in the list
                            // (e.g., show an error message)
                        }
                    }
                }
                else
                {
                    this.CountLBL.Text = "1";
                }
            }

        }



        public void AddtoTempTable()
        {
            try
            {
                OrderDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<CeylonAdaptor>(Session["" + SessionID + ""].ToString());
                CeylonAdaptor aBook = new CeylonAdaptor();

                aBook.FieldI1 = 0; // SK
                aBook.FieldI2 = ItemID; // Item ID
                aBook.FieldS1 = ItemName; // Item name
                aBook.FieldD1 = Convert.ToDecimal(ItemPrice); // price
                aBook.FieldD2 = Convert.ToDecimal(1); // qty
                aBook.FieldD3 = Convert.ToDecimal(ItemPrice) * Convert.ToDecimal(1); // total
                aBook.FieldDate1 = DateTime.Now;
                aBook.FieldS2 = CATID;// ItemType


                ORList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList" + SessionID + ""].ToString());
                ORList.Add(aBook);
                Session.Remove("ORList" + SessionID + "");
                Session["ORList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList);
                Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);

                FilltheOrderTable();
            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Reservation Error", "alert('Add to Cart Error')", true);

            }
        }
        public void FilltheOrderTable()
        {
            try
            {

                ORList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList" + SessionID + ""].ToString());
                CeylonAdaptor[] CartArray1 = ORList.ToArray();

                if (CartArray1.Length == 0 || CartArray1 == null)
                {
                    GridView1.Visible = false;

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Add to Cart Error", "alert('No Items Found')", true);
                }
                else
                {

                    // NameLBL.Text = CartArray[0].FieldS1 + " £"; 

                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Add to Cart Error", "alert('" + CartArray.Length + "')", true);
                    DataTable aCustomerData = new DataTable();
                    aCustomerData.Columns.Add(new DataColumn("SK", typeof(string)));
                    aCustomerData.Columns.Add(new DataColumn("Cat", typeof(string)));
                    aCustomerData.Columns.Add(new DataColumn("ItemID", typeof(string)));
                    aCustomerData.Columns.Add(new DataColumn("Item", typeof(string)));
                    aCustomerData.Columns.Add(new DataColumn("Price", typeof(string)));
                    aCustomerData.Columns.Add(new DataColumn("QTY", typeof(string)));
                    aCustomerData.Columns.Add(new DataColumn("Total", typeof(string)));


                    decimal GrandTotal = 0;

                    for (int i = 0; i < CartArray1.Length; i++)
                    {
                        string CatType = "";
                        if (CartArray1[i].FieldS2 == "1")
                        {
                            CatType = "Meal";
                        }
                        if (CartArray1[i].FieldS2 == "2")
                        {
                            CatType = "Can Be meal";
                        }
                        if (CartArray1[i].FieldS2 == "3")
                        {
                            CatType = "Just a Item";
                        }
                        aCustomerData.Rows.Add(
                            CartArray1[i].FieldI1,
                          CatType,
                             CartArray1[i].FieldI2,
                             CartArray1[i].FieldS1,
                             CartArray1[i].FieldD1,
                            CartArray1[i].FieldD2,
                            CartArray1[i].FieldD3
                            );


                        GrandTotal = GrandTotal + CartArray1[i].FieldD3;
                    }


                    PriceLBL.Text = Convert.ToString(GrandTotal);

                    GridView1.DataSource = aCustomerData;
                    GridView1.DataBind();

                    //  Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);
                    GridView1.Visible = false;



                }
            }
            catch (Exception Ex)
            {
                GridView1.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Add to Cart Error", "alert('Item Filling Error')", true);
            }

        }

        //Pending Order transaction new Item
       

        protected void AddToOrderBTN_Click(object sender, EventArgs e)
        {
            OrderDetails.FieldD2 = 0;
            OrderDetails.FieldD3 = 0;
            OrderDetails.FieldI4 = 0;// button clicked false


            ORList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList" + SessionID + ""].ToString());
            CeylonAdaptor[] CartArray2 = ORList.ToArray();



            for (int i = 0; i < CartArray2.Length; i++)

            {
                CeylonAdaptor aBook = new CeylonAdaptor();
                aBook.FieldI1 = CartArray2[i].FieldI1; // SK
                aBook.FieldI2 = CartArray2[i].FieldI2; // Item ID
                aBook.FieldS1 = CartArray2[i].FieldS1; // Item name
                aBook.FieldD1 = CartArray2[i].FieldD1; // price
                aBook.FieldD2 = CartArray2[i].FieldD2; // qty
                aBook.FieldD3 = CartArray2[i].FieldD3; // total
                aBook.FieldDate1 = CartArray2[i].FieldDate1;
                aBook.FieldS2 = CartArray2[i].FieldS2;// ItemType


                CartList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["CartList" + SessionID + ""].ToString());
                CartList.Add(aBook);
                Session.Remove("CartList" + SessionID + "");
                Session["CartList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(CartList);
                Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);

            }


            ORList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList" + SessionID + ""].ToString());
            ORList.Clear();
            Session["ORList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList);

            Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);

            Response.Redirect("~/Pages/Checkout.aspx?ssid=" + SessionID.ToString());
        }
        protected void BackBtn_Click(object sender, EventArgs e)
        {
            OrderDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<CeylonAdaptor>(Session["" + SessionID + ""].ToString());

            ORList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList" + SessionID + ""].ToString());
            Session.Remove("ORList" + SessionID + "");
            ORList.Clear();
            Session["ORList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList);


            ORList2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList2" + SessionID + ""].ToString());
            Session.Remove("ORList2" + SessionID + "");
            ORList2.Clear();
            Session["ORList2" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList);


            OrderDetails.FieldD2 = 0;
            OrderDetails.FieldD3 = 0;
            OrderDetails.FieldI4 = 0;// button clicked false

            // OrderDetails.FieldI6 = 0; //reset Secret Key

            Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);
            Response.Redirect("~/Pages/SubMenu.aspx?ssid=" + SessionID.ToString() + "&MenuID=" + OrderDetails.FieldI2 + "&MenuName=" + HttpUtility.UrlEncode(OrderDetails.FieldS1) + "");

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
        public void FillCart()
        {
            //try
            //{
            decimal Total = 0;
            decimal ExtraCost = 0;
            ORList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList" + SessionID + ""].ToString());
            CeylonAdaptor[] CartArray2 = ORList.ToArray();



            for (int i = 0; i < CartArray2.Length; i++)

            {
                CeylonAdaptor aBook = new CeylonAdaptor();
                aBook.FieldI1 = CartArray2[i].FieldI1; // SK
                aBook.FieldI2 = CartArray2[i].FieldI2; // Item ID
                aBook.FieldS1 = CartArray2[i].FieldS1; // Item name
                aBook.FieldD1 = CartArray2[i].FieldD1; // price
                aBook.FieldD2 = CartArray2[i].FieldD2; // qty
                aBook.FieldD3 = CartArray2[i].FieldD3; // total
                aBook.FieldDate1 = CartArray2[i].FieldDate1;
                aBook.FieldS2 = CartArray2[i].FieldS2;// ItemType


                CartList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["CartList" + SessionID + ""].ToString());
                CartList.Add(aBook);
                Session.Remove("CartList" + SessionID + "");
                Session["CartList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(CartList);
                Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);

            }
            CartList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["CartList" + SessionID + ""].ToString());

            CeylonAdaptor[] OrderArray = CartList.ToArray();


            if (OrderArray.Length == 0 || OrderArray == null)
            {

                // Panel3.Visible = false;

                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Add to Cart Error", "alert('No Items Found in Cart')", true);
            }
            else
            {


                DataTable aCustomerData = new DataTable();


                //aCustomerData.Columns.Add(new DataColumn("ImageUrl", typeof(string)));
                //aCustomerData.Columns.Add(new DataColumn("Cat", typeof(string)));
                //aCustomerData.Columns.Add(new DataColumn("ItemID", typeof(string)));
                aCustomerData.Columns.Add(new DataColumn("Item", typeof(string)));
                aCustomerData.Columns.Add(new DataColumn("Price", typeof(string)));
                aCustomerData.Columns.Add(new DataColumn("QTY", typeof(string)));
                aCustomerData.Columns.Add(new DataColumn("Total", typeof(string)));


                decimal GrandTotal = 0;





                for (int i = 0; i < OrderArray.Length; i++)
                {

                    if (File.Exists(Server.MapPath("/ItemImages/" + OrderArray[i].FieldI2 + ".jpg")))
                    {
                        imgString = "/ItemImages/" + OrderArray[i].FieldI2 + ".jpg";
                    }
                    else
                    {
                        imgString = "/images/NOimage.png";
                    }

                    string CatType = "";

                    string OpenBracket = "";
                    string CloseBracket = "";
                    string combinedS1Values = "";
                    if (OrderArray[i].FieldS2 == "1")
                    {
                        CatType = "Meal";
                        //OpenBracket = "(";
                        //CloseBracket = ")";

                        OrderArray[i].FieldS6 = "(";
                        OrderArray[i].FieldS7 = ")";

                        List<CeylonAdaptor> ORList2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList2" + SessionID + ""].ToString());
                        int searchFieldValue = OrderArray[i].FieldI1; // 
                                                                      // Filter the items in ORList2 based on the search parameter
                        List<CeylonAdaptor> searchedItems = ORList2.Where(item => item.FieldI1 == searchFieldValue).ToList();

                        CeylonAdaptor[] PriceF = searchedItems.ToArray();
                        ExtraCost = 0;
                        for (int j = 0; j < PriceF.Length; j++)
                        {
                            ExtraCost = ExtraCost + PriceF[j].FieldD3;
                        }

                        // Add the "FieldS1" values of the searched items to the DataTable "aCustomerData"
                        combinedS1Values = string.Join(", ", searchedItems.Select(item => item.FieldS1));

                    }
                    if (OrderArray[i].FieldS2 == "2")
                    {
                        OrderArray[i].FieldS6 = "";
                        OrderArray[i].FieldS7 = "";

                        CatType = "Can Be meal";
                    }
                    if (OrderArray[i].FieldS2 == "3")
                    {
                        OrderArray[i].FieldS6 = "";
                        OrderArray[i].FieldS7 = "";
                        CatType = "Just a Item";
                    }




                    aCustomerData.Rows.Add(
                             //imgString,
                             //CatType,
                             // OrderArray[i].FieldI2,
                             OrderArray[i].FieldS1 + OrderArray[i].FieldS6 + combinedS1Values + OrderArray[i].FieldS7,
                             OrderArray[i].FieldD1,
                            OrderArray[i].FieldD2,
                            OrderArray[i].FieldD3 + ExtraCost
                            );


                    GrandTotal = GrandTotal + OrderArray[i].FieldD3;
                }

                //  TotalPriceLBL.Text = "Grand Total  £" + GrandTotal.ToString();

                OrderDetails.FieldD2 = 0;
                OrderDetails.FieldD3 = 0;
                OrderDetails.FieldI4 = 0;// button clicked false

                Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);
                GridView3.DataSource = aCustomerData;
                GridView3.DataBind();

                ORList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList" + SessionID + ""].ToString());
                ORList.Clear();
                Session["ORList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList);
            }
            // }
            //catch (Exception Ex)
            //{

            //  GridView1.Visible = false;
            //  CheckoutBtn.Enabled = false;
            //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Add to Cart Error", "alert('Error')", true);


            //}

        }


    }
}