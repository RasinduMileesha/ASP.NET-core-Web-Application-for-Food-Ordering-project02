<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="CTAProject.Pages.Checkout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width,height=device-height, user-scalable=no">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
  <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.14.7/dist/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>    
    <!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        function showPaymentPanel() {
            var panel = document.getElementById('<%= PaymentPanel.ClientID %>');
            panel.style.display = 'block';
        }
    </script>
    <style type="text/css">
        .HeadFont {
            padding-left: 5px;
            font-size: 22px;
        }
      
        .card {
            box-shadow: 1px 1px 1px 1px gray;
            width:900px;
            height:500px;
            position: fixed;
            top: 51%;
            left: 50%;
            -webkit-transform: translate(-50%, -50%);
             transform: translate(-50%, -50%);
            }
         .SidesPanel {           
            height:70px;
            width:800px;
           
            }

        @media (max-width:600px) {
             .HeadFont {
                padding-left:2px;
                font-size: 8px;
            }
            .card {
                box-shadow: 1px 1px 1px 1px gray;
                width:500px;
                height:500px;
                position: fixed;
                top: 51%;
                left: 50%;
               -webkit-transform: translate(-50%, -50%);
                transform: translate(-50%, -50%);

            }
            .SidesPanel {
                height: 60px;
                width: 550px;
            }
        }        
         @media (max-width:900px) {
             .HeadFont {
                padding-left:3px;
                font-size:13px;
            }
            .card {
                box-shadow: 1px 1px 1px 1px gray;
                width:800px;
                height:500px;
                position: fixed;
                top: 51%;
                left: 50%;
                -webkit-transform: translate(-50%, -50%);
                transform: translate(-50%, -50%);

            }
            .SidesPanel {
                height: 70px;
                width: 800px;
            }
        }
        @media (max-width:1000px) {
             .HeadFont {
                padding-left:4px;
                font-size: 19px;
            }
            .card {
               box-shadow: 1px 1px 1px 1px gray;
               width:800px;
               height:500px;
               position: fixed;
               top: 51%;
               left: 50%;
               -webkit-transform: translate(-50%, -50%);
               transform: translate(-50%, -50%);
            }
            .SidesPanel {
                height: 70px;
                width: 800px;
            }
        }

        
        .btn {
  
  height: 50px;
  width: 50px;
  border-radius: 50%;
  border: 1px solid #C9CCCC;
  
}



        

    </style>

    <script>
        function showPaymentPanel() {
            var panel = document.getElementById('<%= PaymentPanel.ClientID %>');
            panel.style.display = 'flex';
        }

        function closePaymentPanel() {
            var panel = document.getElementById('<%= PaymentPanel.ClientID %>');
            panel.style.display = 'none';
        }
    </script>

    <script type="text/javascript">
        function addToCart(itemId, itemName, itemPrice, itemDescription) {
            // AJAX request to send the data to the server
            $.ajax({
                type: "POST", // or "GET" depending on your needs
                url: "~/Pages/MealSelectMenu.aspx/AddToCart", // Replace "YourPage.aspx" with the actual name of your page
                data: JSON.stringify({
                    itemId: itemId,
                    itemName: itemName,
                    itemPrice: itemPrice,
                    itemDescription: itemDescription
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    // Handle the server response if needed
                    console.log("Item added to cart successfully!");
                },
                error: function (xhr, status, error) {
                    // Handle any errors that occur during the AJAX request
                    console.error(error);
                }
            });
        }


    </script>
      <style>
        /* Apply CSS styles to the loading overlay */
        #loadingOverlay {
            display: flex;
            flex-direction: column; /* To stack image and text vertically */
            justify-content: center;
            align-items: center;
            z-index: 999;
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: white;
            filter: alpha(opacity=97);
            opacity: 0.95;
        }

        /* Style the loading image */
        #loadingImage {
            width: 140px; /* Adjust the width as needed */
            height: 140px; /* Adjust the height as needed */
        }

        /* Style the loading text */
        #loadingText {
            font-style: italic;
            font-weight: 400;
            font-size: 27px; /* Adjust the font size as needed */
            margin-top: 10px; /* Add some space between image and text */
        }
    </style>
    <script type="text/javascript">
        function ShowLoading(e) {
            var loadingOverlay = document.createElement('div');
            loadingOverlay.id = 'loadingOverlay';

            var loadingImage = document.createElement('img');
            loadingImage.id = 'loadingImage';
            loadingImage.src = '../images/Loading.gif';

            var loadingText = document.createElement('p');
            loadingText.id = 'loadingText';
            loadingText.textContent = 'Please Wait...';

            loadingOverlay.appendChild(loadingImage);
            loadingOverlay.appendChild(loadingText);

            document.body.appendChild(loadingOverlay);

            // These 2 lines cancel form submission, so only use if needed.
            window.event.cancelBubble = true;
            e.stopPropagation();
        }
    </script>

     
</head>
    
<body style=" text-decoration: none !important;overflow-x: hidden;">
    <form id="form1" runat="server">
        
     
    

        <asp:Panel ID="Panel1" runat="server">
            <br />
            <div class="row">

                 <div class="col" align="left">
                  
            </div>
                 <div class="col" align="right">

                     <asp:Button ID="CloseBtn" CssClass="HeadFont" runat="server" Text="X" BackColor="White" BorderStyle="None" Font-Bold="True"  Height="38px" OnClick="CloseBtn_Click" Width="38px" />
                     &nbsp;
            </div>
            </div>
             <br />
            <br />
         
        </asp:Panel>


        <asp:Panel ID="Panel3" runat="server" >

         <div class="row" align="center" >
                <div class="col" align="center">

        <div class="table-responsive-lg"  style="max-width:1000px; min-width:800px; max-height:1000px;min-height:550px;left:50%; box-shadow: 1px 1px 1px 1px gray;overflow-x: hidden; border-radius: 8px;">
            <div class="row" align="center" >
                <div class="col" align="center"  style="padding:30px" >

                    <asp:Label ID="MealIncludesLBL" runat="server" Text="Cart" Font-Size="X-Large" Font-Bold="true"></asp:Label>
                                
                   
                     </div>
            </div>
                    
            <div class="row"  >
                <div class="col" >
                                
                        <asp:Panel ID="SidesPanel" runat="server"  CssClass="table-responsive-lg" style="text-decoration: none;max-width:1000px;min-height:500px; min-width:900px;" ScrollBars="Vertical">                        
                             <div class="row"  >
                <div class="col" align="center" >
                    <br />
                  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"  CssClass="table table-small-font table-bordered table-striped" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridView1_RowCommand">
      <SelectedRowStyle BackColor="#B4D9FF" Font-Bold="True" />


                       <Columns>
        <asp:TemplateField ItemStyle-Width="60px">
            <ItemTemplate>
                <asp:Image ID="ItemImage" runat="server" ImageUrl='<%# Eval("ImageUrl") %>'  Height="40px" Width="40px" style="display: block; margin: 0 auto;"/>
            </ItemTemplate>
        </asp:TemplateField>
       <asp:BoundField DataField="Cat" HeaderText="Cat"  />
        <asp:BoundField DataField="ItemID" HeaderText="ItemID" Visible="false" />
        <asp:BoundField DataField="Item" HeaderText="Item"    />
        <asp:BoundField DataField="Price" HeaderText="Price"  ItemStyle-Width="130px"/>
        <asp:BoundField DataField="QTY" HeaderText="QTY" ItemStyle-Width="110px" />
        <asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-Width="140px" />
                            <asp:TemplateField ItemStyle-Width="100px">
            <ItemTemplate>
                <asp:Button ID="BtnRemove" runat="server" CommandName="RemoveRow" CommandArgument='<%# Container.DataItemIndex %>' Text="Remove" CssClass="btn btn-danger" Width="100px" />
            </ItemTemplate>
        </asp:TemplateField>


    </Columns>
</asp:GridView>
                    <br />

                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large">Grand Total Rs.</asp:Label>    <asp:Label ID="TotalPriceLBL" runat="server" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                     <br /> <br />
                   </div>              

                                 </div>
                        </asp:Panel>
                     <br /> <br />




            </div>
            </div>


             

            
        </div>


</div>
             </div>
            </asp:Panel>


        <br />
      
      
        <asp:Panel ID="Panel2" runat="server" style="position: fixed;left: 0;bottom: 0;width: 100%;box-shadow: 1px 1px 1px 1px gray;">
              
             <div class="row">
                 <div class="col" style="padding:20px">
                     <asp:Button ID="ClearBtn" runat="server" Text="Clear All" Width="100%" BackColor="#C93127" ForeColor="White" Font-Bold="true" Font-Size="Medium" CssClass="table-responsive-lg" Height="50px" BorderStyle="None" onclientclick="return confirm('Are you sure to Clear All?');" xmlns:asp="#unknown" OnClick="ClearBtn_Click"  />
                 
                 </div>

                 <div class="col" style="padding:20px">
                   
                     <asp:Button ID="CheckoutBtn" runat="server" Text="PAY" Width="100%" BackColor="#C93127" ForeColor="White" Font-Bold="true" Font-Size="Medium" CssClass="table-responsive-lg" Height="50px" BorderStyle="None" OnClick="CheckoutBtn_Click" OnClientClick="showPaymentPanel(); return false;" />
                 </div>
                 
             </div>
             </asp:Panel>

          <asp:Panel ID="Panel4" runat="server" Visible="false" >

               <div class="row">

                 <div class="col" align="left">
                  
            </div>
                 <div class="col" align="right">

                     <asp:Button ID="AllCloseBtn" CssClass="HeadFont" runat="server" Text="X" BackColor="White" BorderStyle="None" Font-Bold="True"  Height="38px"  Width="38px" OnClick="AllCloseBtn_Click" />
                     &nbsp;
            </div>
            </div>
         <div class="row" align="center" >
         <div class="col" align="center">

        <div class="table-responsive-lg"  style="max-width:1000px; max-height:570px;min-height:500px;min-width:350px;left:50%; box-shadow: 1px 1px 1px 1px gray;overflow-x: hidden; border-radius: 8px;">
            <div class="row" align="center" >
                <div class="col" align="center"  style="padding:30px" >

                          
                    <br />
                     </div>
            </div>
                     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="row"  >
                <div class="col" >
                                
                        <asp:Panel ID="Panel5" runat="server"  CssClass="table-responsive-lg" style="text-decoration: none;border-style: dashed; border-color:#C93127;max-width:930px;min-height:300px;overflow-x: hidden;" ScrollBars="None" BackColor="#003366">                        
                            
                            
                              
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
         <asp:Panel ID="PaymentPanel" runat="server" >
                <div class="centered-panel">
                     <div class="row" align="right">
                          <div class="col" align="right">
                        <asp:Button ID="PayCloseBtn" CssClass="HeadFont" runat="server" Text="X" BackColor="Transparent" BorderStyle="None" Font-Bold="True"  Height="38px"  Width="38px" ForeColor="White" OnClick="PayCloseBtn_Click" />
               </div>
                    </div>


                    <div class="row" id="PayPanel" runat="server">
                        <div class="col" style="padding:20px">
                            <asp:Label ID="Label2" runat="server" Text="TO PAY" Font-Bold="True" Font-Size="X-Large" Width="150px" ForeColor="White"></asp:Label> <asp:TextBox ID="DueAmountTB" runat="server" Enabled="false" Font-Bold="True" Font-Size="X-Large" Width="250px"></asp:TextBox>
                            <br /><br />
                            <asp:Label ID="Label3" runat="server" Text="PAY" Font-Bold="True" Font-Size="X-Large" Width="150px" ForeColor="White"></asp:Label> <asp:TextBox ID="PayAmountTB" runat="server" TextMode="Number" Font-Bold="True" Font-Size="X-Large" Width="250px"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row" id="BalancePanel" runat="server" Visible="false">
                        <div class="col" style="padding:20px">
                             <asp:Label ID="Label4" runat="server" Text="BALANCE" Font-Bold="True" Font-Size="X-Large" Width="150px" ForeColor="White"></asp:Label> <asp:TextBox ID="BalanceTB" runat="server" Enabled="false" Font-Bold="True" Font-Size="X-Large" Width="250px"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row" id="BTNPanel" runat="server">
                        <div class="col" style="padding:20px">
                            <asp:Button ID="CardBtn" runat="server" Text="Card" OnClick="CardBtn_Click" Height="100px" Width="100px" Font-Bold="True" Font-Size="Large" />
                            &nbsp; &nbsp; &nbsp; 
                            <asp:Button ID="Cashbtn" runat="server" Text="Cash" OnClick="Cash_Click" Height="100px" Width="100px" Font-Bold="True" Font-Size="Large" />
                        </div>
                        <div class="col" style="padding:20px" align="left">
                            <asp:Button ID="BTN500" runat="server" Text="500" Height="100px" Width="100px" Font-Bold="True" Font-Size="Large" OnClick="BTN500_Click" /> &nbsp; &nbsp;
                            <asp:Button ID="BTN1000" runat="server" Text="1000" Height="100px" Width="100px" Font-Bold="True" Font-Size="Large" OnClick="BTN1000_Click" /> &nbsp; &nbsp;
                            <asp:Button ID="BTN2000" runat="server" Text="2000" Height="100px" Width="100px" Font-Bold="True" Font-Size="Large" OnClick="BTN2000_Click" /> &nbsp; &nbsp;
                            <asp:Button ID="BTN5000" runat="server" Text="5000" Height="100px" Width="100px" Font-Bold="True" Font-Size="Large" OnClick="BTN5000_Click" />
                        </div>
                    </div>
                </div>
            </asp:Panel>

           
        </div>
                </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="CardBtn" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="Cashbtn" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>
                           
                             
                            
                        </asp:Panel>
                    </div></div>
                   
                      <div class="row" style="background-color:white;">
                                
                 <div class="col" align="center" style="background-color:white;">
                       <br /> <br /> 
                     <asp:Button ID="BackToOrderBtn" runat="server" Text="Back to Order" Width="350px" BackColor="#C93127" ForeColor="White" Font-Bold="true" Font-Size="Medium" CssClass="table-responsive-lg" Height="50px" BorderStyle="None" OnClick="BackToOrderBtn_Click"  />
                   <br /> <br />
                 </div>

         
             </div>
                   
            
           

             

            
        </div>


</div>
            
            </asp:Panel>


         <asp:Panel ID="Panel6" runat="server" style="position: fixed;left: 0;bottom: 0;width: 100%;box-shadow: 1px 1px 1px 1px gray;" Visible="false">
              
             <div class="row">
                 <div class="col" style="padding:20px">
                     <asp:Button ID="Button1" runat="server" Text="Clear All" Width="100%" BackColor="#C93127" ForeColor="White" Font-Bold="true" Font-Size="Medium" CssClass="table-responsive-lg" Height="50px" BorderStyle="None" onclientclick="return confirm('Are you sure to Clear All?');" xmlns:asp="#unknown" OnClick="ClearBtn_Click"  />
                 
                 </div>

                 <div class="col" style="padding:20px">
                   
                     <asp:Button ID="Button2" runat="server" Text="PAY" Width="100%" BackColor="#C93127" ForeColor="White" Font-Bold="true" Font-Size="Medium" CssClass="table-responsive-lg" Height="50px" BorderStyle="None"  OnClientClick="CheckoutBtn_Click" autopostback="true"  />
                 </div>
                 
             </div>
             </asp:Panel>

       

    </form>
</body>
</html>
