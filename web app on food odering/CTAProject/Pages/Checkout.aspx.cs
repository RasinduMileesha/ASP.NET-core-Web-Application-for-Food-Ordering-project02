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
using System.Data;

namespace CTAProject.Pages
{
    public partial class Checkout : System.Web.UI.Page
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
        private CeylonAdaptor[] ImageArray;
        private List<CeylonAdaptor> CartList;
        private List<CeylonAdaptor> ActualCartList;
        public String MainName;
        public int OrderID = 0;
        public String imgString;
        public int MagicSaleID;
        public int GeneratedSaleID;
        private List<CeylonAdaptor> PaymentList;
        public decimal DueAmount;
        public decimal RecivedCash = 0;
        public decimal BalanceDisplay = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
                getComputerName();
                aManager_DAO = new Manager_DAO();
                SessionID = Convert.ToInt32(Request.QueryString["ssid"]);
                OrderDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<CeylonAdaptor>(Session["" + SessionID + ""].ToString());

            if (!IsPostBack)
            {
                FilltheOrderTable();
            }
            ORList = new List<CeylonAdaptor>();
            Session["ORList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList);

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

        public void FilltheOrderTable()
        {
            try
            {
                decimal Total = 0;
               decimal ExtraCost = 0;
            CartList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["CartList" + SessionID + ""].ToString());

                CeylonAdaptor[] OrderArray = CartList.ToArray();


                if (OrderArray.Length == 0 || OrderArray == null)
                {

                    Panel3.Visible = false;
                    CheckoutBtn.Enabled = false;
                   
                }
                else
                {
                    DataTable aCustomerData = new DataTable();
                  
                    aCustomerData.Columns.Add(new DataColumn("ImageUrl", typeof(string)));
                    aCustomerData.Columns.Add(new DataColumn("Cat", typeof(string)));
                    aCustomerData.Columns.Add(new DataColumn("ItemID", typeof(string)));
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
                   



                   




                    aCustomerData.Rows.Add(
                            imgString,
                            OrderArray[i].FieldS3,
                             OrderArray[i].FieldI2,
                             OrderArray[i].FieldS1,
                             OrderArray[i].FieldD1,
                            OrderArray[i].FieldD2,
                            OrderArray[i].FieldD3
                            );


                        GrandTotal = GrandTotal + OrderArray[i].FieldD3;
                    }

                    TotalPriceLBL.Text = GrandTotal.ToString();
                    DueAmountTB.Text = GrandTotal.ToString();
                    PayAmountTB.Text = GrandTotal.ToString();

                    DueAmount = GrandTotal;

                    OrderDetails.FieldD2 = 0;
                    OrderDetails.FieldD3 = 0;
                    OrderDetails.FieldI4 = 0;// button clicked false

                    Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);
                    GridView1.DataSource = aCustomerData;
                    GridView1.DataBind();
                    Panel3.Visible = true;

                ORList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList" + SessionID + ""].ToString());
                ORList.Clear();
                Session["ORList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList);
            }
        }
            catch (Exception Ex)
            {

              GridView1.Visible = false;
              CheckoutBtn.Enabled = false;
              ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Add to Cart Error", "alert('Error')", true);


        }

    }

    

       

        protected void CloseBtn_Click(object sender, EventArgs e)
        {

            ORList = new List<CeylonAdaptor>();
            Session["ORList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList);

            Response.Redirect("~/Pages/MainMenu.aspx?ssid=" + SessionID.ToString());
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void CheckoutBtn_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = true;
            BackToOrderBtn.Visible = false;
            CloseBtn.Visible = false;


        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            ORList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList" + SessionID + ""].ToString());
            Session.Remove("ORList" + SessionID + "");
            ORList.Clear();
            Session["ORList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList);

            ORList2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList2" + SessionID + ""].ToString());
            Session.Remove("ORList2" + SessionID + "");
            ORList2.Clear();
            Session["ORList2" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList2);

            CartList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["CartList" + SessionID + ""].ToString());
            Session.Remove("CartList" + SessionID + "");
            CartList.Clear();
            Session["CartList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(CartList);

            OrderDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<CeylonAdaptor>(Session["" + SessionID + ""].ToString());
            OrderDetails.FieldI6 = 0; //reset Secret Key
            Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);
            Response.Redirect("~/Pages/MainMenu.aspx?ssid=" + SessionID.ToString());
        }

        protected void AllCloseBtn_Click(object sender, EventArgs e)
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

        protected void BackToOrderBtn_Click(object sender, EventArgs e)
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
        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "RemoveRow" && !string.IsNullOrEmpty(e.CommandArgument.ToString()))
        //    {
        //        int rowIndex = Convert.ToInt32(e.CommandArgument);

               

        //        CartList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["CartList" + SessionID + ""].ToString());
        //        CartList.RemoveAt(rowIndex);
        //        Session.Remove("CartList" + SessionID + "");
        //        Session["CartList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(CartList);
        //       // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Removed Successfully')", true);
        //        FilltheOrderTable();

        //    }
        //}

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RemoveRow" && !string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Deserialize CartList from the session
                List<CeylonAdaptor> CartList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["CartList" + SessionID]?.ToString());
                if (CartList != null && CartList.Count > rowIndex)
                {
                    // Get the FieldI1 value of the item to be removed from CartList
                    int removedItemFieldI1 = CartList[rowIndex].FieldI1;

                    // Remove the item from CartList
                    CartList.RemoveAt(rowIndex);

                    // Update the CartList in the session
                    Session["CartList" + SessionID] = Newtonsoft.Json.JsonConvert.SerializeObject(CartList);

                    // Deserialize ORList2 from the session
                    List<CeylonAdaptor> ORList2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["ORList2" + SessionID]?.ToString());
                    if (ORList2 != null)
                    {
                        // Remove items from ORList2 that have the same FieldI1 as the removed item from CartList
                        ORList2.RemoveAll(item => item.FieldI1 == removedItemFieldI1);

                        // Update the ORList2 in the session
                        Session["ORList2" + SessionID] = Newtonsoft.Json.JsonConvert.SerializeObject(ORList2);
                    }

                    // Call FilltheOrderTable() to update the user interface with the updated cart data and ORList2
                    FilltheOrderTable();
                }
            }
        }
        public void SaleIDByDate()
        {
            DateTime Today = DateTime.Today;
            string zeroBased = Today.ToString("yy-MM-dd");
            zeroBased = Regex.Replace(zeroBased, @"[^0-9]", "") + "001";
            int CheckSaleIDExsitance = Convert.ToInt32(zeroBased);

            int NewSaleID = 0;
            NewSaleID = aManager_DAO.GetAPastrySaleID(CheckSaleIDExsitance);
            if (NewSaleID == 0)
            {
                MagicSaleID = CheckSaleIDExsitance;
            }
            else
            {
                MagicSaleID = NewSaleID;
            }


        }//End of SaleIDByDate() method
        private void GeneratedSale()
        {
            
                try
                {
                   
                    SaleIDByDate();

                int SelectedCustomerID = 1;
                int TableID = 1;
             
                string Ordertype = "Takeaway";
                OrderID = aManager_DAO.ZXGetNextOrderID(DateTime.Now.ToShortDateString());

                    CeylonMiniAdaptor aTouchSale = new CeylonMiniAdaptor();
                    aTouchSale.FieldI1 = MagicSaleID;
                    aTouchSale.FieldI2 = SelectedCustomerID;
                    aTouchSale.FieldI3 = TableID;
                    aTouchSale.FieldD1 = 0;
                    aTouchSale.FieldDate1 = DateTime.Now;
                    aTouchSale.FieldDate2 = DateTime.Now;
                    aTouchSale.FieldS1 = "Cash Recieved " + RecivedCash;
                    aTouchSale.FieldS2 = "Cash Balance " + BalanceDisplay;
                    aTouchSale.FieldS3 = "Not Printed";
                    aTouchSale.FieldS4 = Ordertype;
                    aTouchSale.FieldS5 = OrderID.ToString();//OrderID
                    GeneratedSaleID = aManager_DAO.AddAPastrySaleTable(aTouchSale);

                CartList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["CartList" + SessionID + ""].ToString());
                CeylonAdaptor[] OrderArray = CartList.ToArray();
                for (int i = 0; i < OrderArray.Length; i++)
                {
                        /*************** Sale Order Details ************/
                        CeylonAdaptor SaleOrderDetails = new CeylonAdaptor();
                        SaleOrderDetails.FieldI1 = MagicSaleID;
                        SaleOrderDetails.FieldI2 = OrderID;
                        SaleOrderDetails.FieldI3 = 4;//Order Department Pastry PendingOrderFullDetails[i].FieldI3;
                        SaleOrderDetails.FieldDate1 = DateTime.Now;
                        SaleOrderDetails.FieldI4 = OrderArray[i].FieldI2;//ItemID
                        SaleOrderDetails.FieldD1 = OrderArray[i].FieldD2;//QTY
                        SaleOrderDetails.FieldS1 = "myIP";// myIP;//TerminalIP
                        SaleOrderDetails.FieldS2 = "SystemUser";//SystemUser;//CashierID
                        //aManager_DAO.AddAPastrySaleOrder(SaleOrderDetails);


                        int CurrentStockID = 0;
                       // CurrentStockID = aManager_DAO.GetPastryStockIDByPara(SaleOrderDetails.FieldI4);
                        CeylonMiniAdaptor aStockTransaction = new CeylonMiniAdaptor();
                        aStockTransaction.FieldI1 = CurrentStockID;
                        aStockTransaction.FieldDate1 = DateTime.Now;
                        aStockTransaction.FieldS1 = "Sale";
                        aStockTransaction.FieldI2 = MagicSaleID;
                        aStockTransaction.FieldD1 = SaleOrderDetails.FieldD1;//Debit QTY
                        aStockTransaction.FieldD2 = 0;
                        aStockTransaction.FieldS2 = MagicSaleID.ToString();
                        if (CurrentStockID != 0)
                        {
                            aManager_DAO.AddAPastryStockTransaction(aStockTransaction);
                        }

                        CeylonAdaptor aTouchSaleTrans = new CeylonAdaptor();
                        aTouchSaleTrans.FieldDate1 = DateTime.Now;
                        aTouchSaleTrans.FieldI1 = MagicSaleID;
                        aTouchSaleTrans.FieldI2 = OrderArray[i].FieldI2;//QTY
                        aTouchSaleTrans.FieldD1 = OrderArray[i].FieldD1;//Price
                        aTouchSaleTrans.FieldD2 = 0;//UnitPrice
                        aTouchSaleTrans.FieldD3 = OrderArray[i].FieldD2;//QTY
                        aTouchSaleTrans.FieldD4 = 0;
                        aTouchSaleTrans.FieldD5 = 0;//Discount
                        aTouchSaleTrans.FieldD6 = (aTouchSaleTrans.FieldD1*aTouchSaleTrans.FieldD3);//aTouchSaleTrans.FieldD1 * aTouchSaleTrans.FieldD3; //SubTotal
                        aTouchSaleTrans.FieldD7 = (aTouchSaleTrans.FieldD6);//TotalwithoutTax
                        aTouchSaleTrans.FieldD8 = 1;//ProftWIthoutTax
                        aTouchSaleTrans.FieldD9 = 0;
                        aTouchSaleTrans.FieldD10 = 0;
                        aTouchSaleTrans.FieldD11 = 0;
                        aTouchSaleTrans.FieldD12 = 1;//Index
                        aTouchSaleTrans.FieldD13 = 0;
                        aTouchSaleTrans.FieldD14 = aTouchSaleTrans.FieldD6;//DiscountedPrice
                        aTouchSaleTrans.FieldD15 = aTouchSaleTrans.FieldD14;//ProftWIthoutTax
                        aTouchSaleTrans.FieldS1 = OrderID.ToString();
                        aTouchSaleTrans.FieldS2 = "NA";//Special Note
                        int FKTransID = aManager_DAO.AddAPastrySaleTransaction(aTouchSaleTrans);

                  
                }
                /*** Deleting Pending Order ************/
                CeylonMiniAdaptor WaiterDetails = new CeylonMiniAdaptor();
                WaiterDetails.FieldI1 = 1;//currentUser;//aManager_DAO.GetWaiterIDByTableIDPastry(TableID.ToString(), DataSource);
                WaiterDetails.FieldI2 = MagicSaleID;
                WaiterDetails.FieldS1 = "Attended";
                aManager_DAO.AddStaffDailyTransactionFinalPastry(WaiterDetails);


                for (int i = 0; i < OrderArray.Length; i++)
                {
                    /*************** Order Customermsation Details ************/
                  
                    int CurrentStockID = 0;
                    CurrentStockID = aManager_DAO.GetPastryStockIDByPara(OrderArray[i].FieldI2);
                    CeylonMiniAdaptor aStockTransaction = new CeylonMiniAdaptor();
                    aStockTransaction.FieldI1 = CurrentStockID;
                    aStockTransaction.FieldDate1 = DateTime.Now;
                    aStockTransaction.FieldS1 = "Sale-Cust Item";
                    aStockTransaction.FieldI2 = MagicSaleID;
                    aStockTransaction.FieldD1 = OrderArray[i].FieldD2;//Debit QTY
                    aStockTransaction.FieldD2 = 0;
                    aStockTransaction.FieldS2 = MagicSaleID.ToString();
                    if (CurrentStockID != 0)
                    {
                        aManager_DAO.AddAPastryStockTransaction(aStockTransaction);
                    }
                }
              
                PaymentList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["PaymentList" + SessionID + ""].ToString());
                CeylonAdaptor[] PaymentArray = PaymentList.ToArray();
                for (int p = 0; p < PaymentArray.Length; p++)
                {
                    if (PaymentArray[p].FieldS1 == "Cash")
                    {
                        CeylonAdaptor aSalePayment = new CeylonAdaptor();
                        aSalePayment.FieldDate1 = DateTime.Now;
                        aSalePayment.FieldS1 = "Cash";
                        aSalePayment.FieldI1 = OrderID;
                        aSalePayment.FieldI2 = SelectedCustomerID;
                        aSalePayment.FieldI3 = GeneratedSaleID;
                        aSalePayment.FieldD1 = PaymentArray[p].FieldD2;
                        aSalePayment.FieldS2 = "";
                        aSalePayment.FieldDate2 = DateTime.Now;
                        aSalePayment.FieldD2 = 0;
                        aSalePayment.FieldDate3 = DateTime.Now;
                        aSalePayment.FieldS3 = "Paid";
                        aSalePayment.FieldS4 = "";
                        aSalePayment.FieldS5 = "Order ID " + OrderID; ;
                        aManager_DAO.AddAPastrySalePaymentsTable(aSalePayment);

                        /********** Terminal Transaction ********/
                        CeylonMiniAdaptor aCeyX = new CeylonMiniAdaptor();
                        aCeyX.FieldS1 = "myIP";//myIP;//IPAddress
                        aCeyX.FieldI1 = 1;//currentUser;//UserID
                        aCeyX.FieldS2 = DateTime.Now.ToShortDateString();//DateX
                        aCeyX.FieldDate1 = DateTime.Now;
                        aCeyX.FieldI2 = MagicSaleID;
                        aCeyX.FieldS3 = "Sale";
                        aCeyX.FieldS4 = "Cash";//Payment Type;
                        aCeyX.FieldD1 = PaymentArray[p].FieldD2;//Amount
                        aCeyX.FieldS5 = "Active";
                        aCeyX.FieldI3 = 0;
                        aCeyX.FieldS6 = "Direct Sale";//Order Type
                        aCeyX.FieldS7 = "Order ID " + OrderID;
                        aCeyX.FieldS8 = "";
                        aCeyX.FieldS9 = "";
                        aManager_DAO.ZXTerminalWiseSalesTableInsert(aCeyX);

                        CeylonMiniAdaptor aCashierTransaction = new CeylonMiniAdaptor();
                        aCashierTransaction.FieldI2 = MagicSaleID;
                        aCashierTransaction.FieldDate1 = DateTime.Now;
                        aCashierTransaction.FieldI1 = 1;//currentUser = AccessLevel.FieldI22;
                        aCashierTransaction.FieldS1 = "Sale";
                        aCashierTransaction.FieldS2 = "Cash";
                        aCashierTransaction.FieldD1 = PaymentArray[p].FieldD2;
                        aManager_DAO.AddACashierTransaction(aCashierTransaction);
                    }
                    else
                    {
                        CeylonAdaptor aSalePayment = new CeylonAdaptor();
                        aSalePayment.FieldDate1 = DateTime.Now;
                        aSalePayment.FieldS1 = "Card";
                        aSalePayment.FieldI1 = OrderID;
                        aSalePayment.FieldI2 = SelectedCustomerID;
                        aSalePayment.FieldI3 = GeneratedSaleID;
                        aSalePayment.FieldD1 = PaymentArray[p].FieldD2;
                        aSalePayment.FieldS2 = "";
                        aSalePayment.FieldDate2 = DateTime.Now;
                        aSalePayment.FieldD2 = 0;
                        aSalePayment.FieldDate3 = DateTime.Now;
                        aSalePayment.FieldS3 = "Paid";
                        aSalePayment.FieldS4 = "";
                        aSalePayment.FieldS5 = "Order ID " + OrderID;
                        aManager_DAO.AddAPastrySalePaymentsTable(aSalePayment);

                        /********** Terminal Transaction ********/
                        CeylonMiniAdaptor aCeyX = new CeylonMiniAdaptor();
                        aCeyX.FieldS1 = "myip";//myIP;//IPAddress
                        aCeyX.FieldI1 = 1111;//currentUser;//UserID
                        aCeyX.FieldS2 = DateTime.Now.ToShortDateString();//DateX
                        aCeyX.FieldDate1 = DateTime.Now;
                        aCeyX.FieldI2 = MagicSaleID;
                        aCeyX.FieldS3 = "Sale";
                        aCeyX.FieldS4 = "Card";//Payment Type;
                        aCeyX.FieldD1 = PaymentArray[p].FieldD2;//Amount
                        aCeyX.FieldS5 = "Active";
                        aCeyX.FieldI3 = 0;
                        aCeyX.FieldS6 = "Direct Sale";//Order Type
                        aCeyX.FieldS7 = "Order ID " + OrderID;
                        aCeyX.FieldS8 = "";
                        aCeyX.FieldS9 = "";
                        aManager_DAO.ZXTerminalWiseSalesTableInsert(aCeyX);

                        CeylonMiniAdaptor aCashierTransaction = new CeylonMiniAdaptor();
                        aCashierTransaction.FieldI2 = MagicSaleID;
                        aCashierTransaction.FieldDate1 = DateTime.Now;
                        aCashierTransaction.FieldI1 = 1111;//currentUser = AccessLevel.FieldI22;
                        aCashierTransaction.FieldS1 = "Sale";
                        aCashierTransaction.FieldS2 = "Card";
                        aCashierTransaction.FieldD1 = PaymentArray[p].FieldD2;
                        aManager_DAO.AddACashierTransaction(aCashierTransaction);
                    }


                }

              

               
            }
          
              catch (Exception Ex)
               {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Add to Cart Error", "alert('Sale Generate Error')", true);
            }
        }//End of GetGeneratedSaleID()

    protected void CardBtn_Click(object sender, EventArgs e)
    {
            decimal dueAmount;
            decimal payAmount;

            // Validate input
            if (decimal.TryParse(DueAmountTB.Text, out dueAmount) && decimal.TryParse(PayAmountTB.Text, out payAmount))
            {
                if (dueAmount >= payAmount)
                {
                    CeylonAdaptor aBook = new CeylonAdaptor
                    {
                        FieldI1 = 0, // ID
                        FieldD2 = dueAmount, // Due Amount
                        FieldD3 = payAmount, // Pay Amount, using a different field
                        FieldS1 = "Card" // CatName
                    };

                     PaymentList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["PaymentList" + SessionID + ""].ToString());
                     PaymentList.Add(aBook);
                     ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Add to Cart Error", "alert('" + PaymentList.Count + "')", true);





                    Session.Remove("PaymentList" + SessionID+ "");
                    Session["PaymentList" + SessionID+ ""] = Newtonsoft.Json.JsonConvert.SerializeObject(PaymentList);
                    Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);

                    DueAmountTB.Text = (dueAmount - payAmount).ToString();
                    PayAmountTB.Text = DueAmountTB.Text;

                    if (DueAmountTB.Text == PayAmountTB.Text)
                    {
                        GeneratedSale();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Add to Cart Error", "alert('Please Enter Exact Amount !!')", true);
                }

                PayPanel.Visible = true;
                BTNPanel.Visible = true;
                BalancePanel.Visible = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Add to Cart Error", "alert('Please enter valid amounts in both fields.')", true);
            }
    }


        protected void Cash_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(DueAmountTB.Text) >= Convert.ToDecimal(PayAmountTB.Text))
            {
                CeylonAdaptor aBook = new CeylonAdaptor
                {
                    FieldI1 = 0, // ID
                    FieldD1 = Convert.ToDecimal(DueAmountTB.Text), // Due Amount
                    FieldD2 = Convert.ToDecimal(PayAmountTB.Text), // Pay Amount
                    FieldS1 = "Cash" // CatName
                };

                List<CeylonAdaptor> PaymentList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["PaymentList" + SessionID].ToString());
                PaymentList.Add(aBook);
                Session["PaymentList" + SessionID] = Newtonsoft.Json.JsonConvert.SerializeObject(PaymentList);
                Session[SessionID.ToString()] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);

                DueAmountTB.Text = (Convert.ToDecimal(DueAmountTB.Text) - Convert.ToDecimal(PayAmountTB.Text)).ToString();
                PayAmountTB.Text = DueAmountTB.Text;

                PayPanel.Visible = true;
                BTNPanel.Visible = true;
                BalancePanel.Visible = false;

                RecivedCash = Convert.ToDecimal(PayAmountTB.Text);
                BalanceDisplay = 0;

                if (Convert.ToDecimal(DueAmountTB.Text) == 0)
                {
                    GeneratedSale();
                }
            }
            else
            {
                BalanceTB.Text = (Convert.ToDecimal(PayAmountTB.Text) - Convert.ToDecimal(DueAmountTB.Text)).ToString();
                RecivedCash = Convert.ToDecimal(PayAmountTB.Text);
                BalanceDisplay = Convert.ToDecimal(BalanceTB.Text);

                CeylonAdaptor aBook = new CeylonAdaptor
                {
                    FieldI1 = 0, // ID
                    FieldD1 = Convert.ToDecimal(DueAmountTB.Text), // Due Amount
                    FieldD2 = Convert.ToDecimal(PayAmountTB.Text) - Convert.ToDecimal(BalanceTB.Text), // Pay Amount
                    FieldS1 = "Cash" // CatName
                };

                List<CeylonAdaptor> PaymentList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["PaymentList" + SessionID + ""].ToString());
                PaymentList.Add(aBook);
                Session["PaymentList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(PaymentList);
                Session[SessionID.ToString()] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);

                DueAmountTB.Text = "0.00";
                PayAmountTB.Text = "0.00";

                PayPanel.Visible = false;
                BTNPanel.Visible = false;
                BalancePanel.Visible = true;

                GeneratedSale(); 
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Add to Cart Error", "alert('" + PaymentList.Count + "')", true);
        }


        protected void BTN500_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(DueAmountTB.Text) >= 500)
            {
                CeylonAdaptor aBook = new CeylonAdaptor();
                aBook.FieldI1 = 0;//ID     
                aBook.FieldD2 = Convert.ToDecimal(DueAmountTB.Text); // Due Amount
                aBook.FieldD2 = 500; // PayAmount
                aBook.FieldS1 = "Cash";// CatName


                PaymentList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["PaymentList" + SessionID + ""].ToString());
                PaymentList.Add(aBook);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Add to Cart Error", "alert('" + PaymentList.Count + "')", true);

                Session.Remove("PaymentList" + SessionID + "");
                Session["PaymentList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(PaymentList);
                Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);

                DueAmountTB.Text = (Convert.ToDecimal(DueAmountTB.Text) - Convert.ToDecimal("500")).ToString();
                PayAmountTB.Text = DueAmountTB.Text;

                PayPanel.Visible = true;
                BTNPanel.Visible = true;
                BalancePanel.Visible = false;

                RecivedCash = 500;
                BalanceDisplay = 0;

                if (Convert.ToDecimal(DueAmountTB.Text) == 500)
                {
                    BalanceDisplay = (Convert.ToDecimal(PayAmountTB.Text) - Convert.ToDecimal(DueAmountTB.Text));
                    RecivedCash = Convert.ToDecimal(PayAmountTB.Text);
                    BalanceDisplay = 0;
                }

            }
            else
            {
                BalanceTB.Text = (500 - Convert.ToDecimal(DueAmountTB.Text)).ToString();
                RecivedCash = 500;
                BalanceDisplay = 500 - Convert.ToDecimal(DueAmountTB.Text);
                CeylonAdaptor aBook = new CeylonAdaptor();
                aBook.FieldI1 = 0;//ID     
                aBook.FieldD2 = Convert.ToDecimal(DueAmountTB.Text); // Due Amount
                aBook.FieldD2 = 500 - Convert.ToDecimal(BalanceTB.Text); // PayAmount
                aBook.FieldS1 = "Cash";// CatName


               


                PaymentList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["PaymentList" + SessionID + ""].ToString());
                PaymentList.Add(aBook);
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Add to Cart Error", "alert('" + PaymentList.Count + "')", true);

                Session.Remove("PaymentList" + SessionID + "");
                Session["PaymentList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(PaymentList);
                Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);

                DueAmountTB.Text = (Convert.ToDecimal(DueAmountTB.Text) - Convert.ToDecimal(PayAmountTB.Text)).ToString();
                PayAmountTB.Text = "0.00";

                PayPanel.Visible = false;
                BTNPanel.Visible = false;
                BalancePanel.Visible = true;

                if (PayAmountTB.Text == "0.00")
                {
                    GeneratedSale();
                }

            }

           
        }

        protected void BTN1000_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(DueAmountTB.Text) >= 1000)
            {
                CeylonAdaptor aBook = new CeylonAdaptor();
                aBook.FieldI1 = 0;//ID     
                aBook.FieldD2 = Convert.ToDecimal(DueAmountTB.Text); // Due Amount
                aBook.FieldD2 = 1000; // PayAmount
                aBook.FieldS1 = "Cash";// CatName


                PaymentList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["PaymentList" + SessionID + ""].ToString());
                PaymentList.Add(aBook);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Add to Cart Error", "alert('" + PaymentList.Count + "')", true);

                Session.Remove("PaymentList" + SessionID + "");
                Session["PaymentList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(PaymentList);
                Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);

                DueAmountTB.Text = (Convert.ToDecimal(DueAmountTB.Text) - Convert.ToDecimal("1000")).ToString();
                PayAmountTB.Text = DueAmountTB.Text;

                PayPanel.Visible = true;
                BTNPanel.Visible = true;
                BalancePanel.Visible = false;


                RecivedCash = 1000;
                BalanceDisplay = 0;

                if (Convert.ToDecimal(DueAmountTB.Text) == 1000)
                {
                    BalanceDisplay = (Convert.ToDecimal(PayAmountTB.Text) - Convert.ToDecimal(DueAmountTB.Text));
                    RecivedCash = Convert.ToDecimal(PayAmountTB.Text);
                    BalanceDisplay = 0;
                }

            }
            else
            {
                BalanceTB.Text = (1000 - Convert.ToDecimal(DueAmountTB.Text)).ToString();
                RecivedCash = 1000;
                BalanceDisplay = 1000 - Convert.ToDecimal(DueAmountTB.Text);
                CeylonAdaptor aBook = new CeylonAdaptor();
                aBook.FieldI1 = 0;//ID     
                aBook.FieldD2 = Convert.ToDecimal(DueAmountTB.Text); // Due Amount
                aBook.FieldD2 = 1000 - Convert.ToDecimal(BalanceTB.Text); // PayAmount
                aBook.FieldS1 = "Cash";// CatName


                PaymentList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["PaymentList" + SessionID + ""].ToString());
                PaymentList.Add(aBook);
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Add to Cart Error", "alert('" + PaymentList.Count + "')", true);

                Session.Remove("PaymentList" + SessionID + "");
                Session["PaymentList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(PaymentList);
                Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);

                DueAmountTB.Text = (Convert.ToDecimal(DueAmountTB.Text) - Convert.ToDecimal(PayAmountTB.Text)).ToString();
                PayAmountTB.Text = "0.00";

                PayPanel.Visible = false;
                BTNPanel.Visible = false;
                BalancePanel.Visible = true;

                if (PayAmountTB.Text == "0.00")
                {
                    GeneratedSale();
                }

            }
        }

        protected void BTN2000_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(DueAmountTB.Text) >= 2000)
            {
                CeylonAdaptor aBook = new CeylonAdaptor();
                aBook.FieldI1 = 0;//ID     
                aBook.FieldD2 = Convert.ToDecimal(DueAmountTB.Text); // Due Amount
                aBook.FieldD2 = 2000; // PayAmount
                aBook.FieldS1 = "Cash";// CatName


                PaymentList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["PaymentList" + SessionID + ""].ToString());
                PaymentList.Add(aBook);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Add to Cart Error", "alert('" + PaymentList.Count + "')", true);

                Session.Remove("PaymentList" + SessionID + "");
                Session["PaymentList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(PaymentList);
                Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);

                DueAmountTB.Text = (Convert.ToDecimal(DueAmountTB.Text) - Convert.ToDecimal("2000")).ToString();
                PayAmountTB.Text = DueAmountTB.Text;

                PayPanel.Visible = true;
                BTNPanel.Visible = true;
                BalancePanel.Visible = false;

                RecivedCash = 2000;
                BalanceDisplay = 0;

                if (Convert.ToDecimal(DueAmountTB.Text) == 2000)
                {
                    BalanceDisplay = (Convert.ToDecimal(PayAmountTB.Text) - Convert.ToDecimal(DueAmountTB.Text));
                    RecivedCash = Convert.ToDecimal(PayAmountTB.Text);
                    BalanceDisplay = 0;
                }

            }
            else
            {
                BalanceTB.Text = (2000 - Convert.ToDecimal(DueAmountTB.Text)).ToString();
                RecivedCash = 2000;
                BalanceDisplay = 2000 - Convert.ToDecimal(DueAmountTB.Text);
                CeylonAdaptor aBook = new CeylonAdaptor();
                aBook.FieldI1 = 0;//ID     
                aBook.FieldD2 = Convert.ToDecimal(DueAmountTB.Text); // Due Amount
                aBook.FieldD2 = 2000 - Convert.ToDecimal(BalanceTB.Text); // PayAmount
                aBook.FieldS1 = "Cash";// CatName


                PaymentList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["PaymentList" + SessionID + ""].ToString());
                PaymentList.Add(aBook);
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Add to Cart Error", "alert('" + PaymentList.Count + "')", true);

                Session.Remove("PaymentList" + SessionID + "");
                Session["PaymentList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(PaymentList);
                Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);

                DueAmountTB.Text = (Convert.ToDecimal(DueAmountTB.Text) - Convert.ToDecimal(PayAmountTB.Text)).ToString();
                PayAmountTB.Text = "0.00";

                PayPanel.Visible = false;
                BTNPanel.Visible = false;
                BalancePanel.Visible = true;

                if (PayAmountTB.Text == "0.00")
                {
                    GeneratedSale();
                }

            }
        }

        protected void BTN5000_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(DueAmountTB.Text) >= 5000)
            {
                CeylonAdaptor aBook = new CeylonAdaptor();
                aBook.FieldI1 = 0;//ID     
                aBook.FieldD2 = Convert.ToDecimal(DueAmountTB.Text); // Due Amount
                aBook.FieldD2 = 5000; // PayAmount
                aBook.FieldS1 = "Cash";// CatName


                PaymentList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["PaymentList" + SessionID + ""].ToString());
                PaymentList.Add(aBook);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Add to Cart Error", "alert('" + PaymentList.Count + "')", true);

                Session.Remove("PaymentList" + SessionID + "");
                Session["PaymentList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(PaymentList);
                Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);

                DueAmountTB.Text = (Convert.ToDecimal(DueAmountTB.Text) - Convert.ToDecimal("5000")).ToString();
                PayAmountTB.Text = DueAmountTB.Text;

                PayPanel.Visible = true;
                BTNPanel.Visible = true;
                BalancePanel.Visible = false;

                RecivedCash = 5000;
                BalanceDisplay = 0;

                if (Convert.ToDecimal(DueAmountTB.Text) == 5000)
                {
                    BalanceDisplay = (Convert.ToDecimal(PayAmountTB.Text) - Convert.ToDecimal(DueAmountTB.Text));
                    RecivedCash = Convert.ToDecimal(PayAmountTB.Text);
                    BalanceDisplay = 0;
                }

            }
            else
            {
                BalanceTB.Text = (5000 - Convert.ToDecimal(DueAmountTB.Text)).ToString();
                RecivedCash = 5000;
                BalanceDisplay = 5000 - Convert.ToDecimal(DueAmountTB.Text);
                CeylonAdaptor aBook = new CeylonAdaptor();
                aBook.FieldI1 = 0;//ID     
                aBook.FieldD2 = Convert.ToDecimal(DueAmountTB.Text); // Due Amount
                aBook.FieldD2 = 5000 - Convert.ToDecimal(BalanceTB.Text); // PayAmount
                aBook.FieldS1 = "Cash";// CatName


                PaymentList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CeylonAdaptor>>(Session["PaymentList" + SessionID + ""].ToString());
                PaymentList.Add(aBook);
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Add to Cart Error", "alert('" + PaymentList.Count + "')", true);

                Session.Remove("PaymentList" + SessionID + "");
                Session["PaymentList" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(PaymentList);
                Session["" + SessionID + ""] = Newtonsoft.Json.JsonConvert.SerializeObject(OrderDetails);

                DueAmountTB.Text = (Convert.ToDecimal(DueAmountTB.Text) - Convert.ToDecimal(PayAmountTB.Text)).ToString();
                PayAmountTB.Text = "0.00";

                PayPanel.Visible = false;
                BTNPanel.Visible = false;
                BalancePanel.Visible = true;

                if (PayAmountTB.Text == "0.00")
                {
                    GeneratedSale();
                }

            }
        }

        protected void PayCloseBtn_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            Panel2.Visible = true;
            Panel3.Visible = true;
            Panel4.Visible = false;
            BackToOrderBtn.Visible = true;
            CloseBtn.Visible = true;
        }
    }
}