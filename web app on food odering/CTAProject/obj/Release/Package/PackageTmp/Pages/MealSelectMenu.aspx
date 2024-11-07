<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MealSelectMenu.aspx.cs" Inherits="CTAProject.Pages.MealSelectMenu"  %>

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
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css">
  <script src="https://cdn.jsdelivr.net/npm/jquery@3.6.4/dist/jquery.slim.min.js"></script>
  <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"></script>
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
  
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <script src="Scripts/jquery.signalR-2.4.3.min.js"></script>

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
      <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11.0.18/dist/sweetalert2.all.min.js"></script>
      <script>
          function showConfirmation() {
              Swal.fire({
                  title: 'Are you sure?',
                  text: 'This action will start again. Do you want to proceed?',
                  icon: 'question',
                  showCancelButton: true,
                  confirmButtonText: 'Yes',
                  cancelButtonText: 'No',
                  customClass: {
                      container: 'my-sweetalert-container',
                      popup: 'my-sweetalert-popup', // New class for positioning
                      title: 'my-sweetalert-title',
                      text: 'my-sweetalert-text',
                      confirmButton: 'my-sweetalert-confirm-button',
                      cancelButton: 'my-sweetalert-cancel-button'
                  }
              }).then((result) => {
                  // The result.value will be true if 'Yes' is clicked, and false if 'No' is clicked or the dialog is closed.
                  if (result.value) {
                      // User clicked 'Yes', proceed with the server-side click event.
                      __doPostBack('<%= StartBtn.UniqueID %>', ''); // Triggers the server-side click event.
                      } else {
                          // User clicked 'No' or closed the dialog, do nothing.
                      }
                  });
              // Return false here to prevent the server-side click event from firing immediately.
              return false;
          }
</script>

  
     
</head>
    
    
<body style=" text-decoration: none !important;overflow-x: hidden;">
    <form id="form1" runat="server">
        
       <asp:GridView ID="GridView1" runat="server"  CssClass="table table-small-font table-bordered table-striped" Visible="false" >
                                                <SelectedRowStyle BackColor="#B4D9FF" Font-Bold="True" />
                                            </asp:GridView>
    

        <asp:GridView ID="GridView2" runat="server"  CssClass="table table-small-font table-bordered table-striped" Visible="false" >
                                                <SelectedRowStyle BackColor="#B4D9FF" Font-Bold="True" />
                                            </asp:GridView>

        
      <br />
      <div class="row" align="left"  >
          <br />
          <div class="col" align="left"  >
        &nbsp;&nbsp;&nbsp;&nbsp;  <asp:Button ID="BackBtn" runat="server" Text="Back" OnClick="BackBtn_Click" Width="90px"  BackColor="#C93127" ForeColor="White"  Style="border-radius: 11px;" Height="40px" Font-Bold="true"/>
              </div>
             <div class="col" align="right"  >
             <asp:Button ID="StartBtn" runat="server" Text="Start Again"  Width="90px" OnClick="StartBtn_Click" OnClientClick="return showConfirmation();" BackColor="#C93127" ForeColor="White"  Style="border-radius: 11px;" Height="40px" Font-Bold="true" />&nbsp;&nbsp;&nbsp;&nbsp;
                    </div>
          </div>

        <asp:Panel ID="Panel1" runat="server">
            <br />
            <div class="row">

                 <div class="col" align="left">
                    
                     <div style="padding-left:10px;">
                     <asp:Label ID="NameLBL" CssClass="HeadFont" runat="server" Text=""  Font-Bold="True"></asp:Label>

                         <asp:Label ID="PriceLBL" CssClass="HeadFont" runat="server" Text=""  Font-Bold="True"></asp:Label>
                     </div>
            </div>
                 <div class="col" align="right">

                     <asp:Button ID="CloseBtn" CssClass="HeadFont" runat="server" Text="X" BackColor="White" BorderStyle="None" Font-Bold="True"  Height="38px" OnClick="CloseBtn_Click" Width="38px" />
                     &nbsp;
            </div>
            </div>
             <br />
            <br />

            <%--<asp:Button ID="ClickBtn" runat="server" Text="Select" OnClick="ClickBtn_Click" />--%>
           
        </asp:Panel>
        <asp:Panel ID="Panel3" runat="server" Visible="false">

         <div class="row" align="center" >
                <div class="col" align="center">

        <div class="table-responsive-lg"  style="max-width:1000px; max-height:570px;min-height:500px;min-width:350px;left:50%; box-shadow: 1px 1px 1px 1px gray;overflow-x: hidden; border-radius: 8px;">
            <div class="row" align="left" >
                <div class="col" align="left"  style="padding:30px" >


                    <asp:Label ID="MealIncludesLBL" runat="server" Text="3 wings, fries, chicken fillet burger, 1 classic side, regular fries and a soft drink"></asp:Label>
                   
                    <br /> <br />
                    <asp:Label ID="Label1" runat="server" Text="CHOOSE YOUR SIDES" Font-Bold="true" Font-Size="Medium" ForeColor="#DB1517"></asp:Label>
                    
                    <br />
                     </div>
            </div>
                    
            <div class="row"  >

                 <div class="col" id="cartGRid" runat="server">
                     
                     </div>

                <div class="col" >
                    <a href="#"  style=" text-decoration: none !important;overflow-x: hidden;">                       
                        <asp:Panel ID="SidesPanel" runat="server"  CssClass="table-responsive-lg" style="text-decoration: none;border-style: dashed; border-color:#C93127;max-width:930px; max-height:80px;min-height:80px;overflow-x: hidden;" ScrollBars="None">
                          
                             <div class="row"  >
                <div class="col" align="left" >
                    <br />
                  &nbsp;   <asp:Label ID="Label2" runat="server" Text="choose your sides" Font-Size="Medium" ForeColor="#C93127" ></asp:Label>
                   
                    </div>
                <div class="col" align="right" >
                    
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Icons/Plus.png" height="50px" style="padding-top:14px;" /> &nbsp;  
                    </div>

                                 </div>
                        </asp:Panel>
                    </a>
                    
            </div>
            </div>


             <br />  <div class="row" align="left" >
                <div class="col" align="left" style="padding:30px">
                    <asp:Label ID="Label3" runat="server" Text="CHOOSE YOUR DRINK" Font-Bold="true" Font-Size="Medium" ForeColor="#C93127"></asp:Label>
                    </div>
                 </div>
                    <br />

             <div class="row"  >
                <div class="col" >
                    <a href="#"  style=" text-decoration: none !important;overflow-x: hidden;">                       
                        <asp:Panel ID="DrinkPanel" runat="server"  CssClass="table-responsive-lg" style="text-decoration: none;border-style: dashed; border-color:#C93127;max-width:930px; max-height:80px;min-height:80px;overflow-x: hidden;" ScrollBars="None">
                          
                             <div class="row"  >
                <div class="col" align="left" >
                    <br />
                  &nbsp;   <asp:Label ID="Label4" runat="server" Text="choose your drink" Font-Size="Medium" ForeColor="#C93127" ></asp:Label>
                   
                    </div>
                <div class="col" align="right" >
                    
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Icons/Plus.png" height="50px" style="padding-top:14px;" /> &nbsp;  
                    </div>

                                 </div>
                        </asp:Panel>
                    </a>
                    
            </div>
            </div>

        </div>


</div>
             </div>
            </asp:Panel>
        <asp:Panel ID="CanBeMealPanel" runat="server" Visible="false">
             <div class="container" align="center" >
         <br />
                <div class="row" align="center">
                   
                      <div class="col-sm-3" align="center" id="CartCol" runat="server"  visible="false">

                         <asp:GridView ID="GridView3" runat="server"  CssClass="table table-small-font table-bordered table-striped" Font-Size="15px"  >
                                                <SelectedRowStyle BackColor="#B4D9FF" Font-Bold="True" />
                                            </asp:GridView>
                              
                         </div>

                     <div class="col" align="center" id="div_test" runat="server" >


                         </div>
                    </div>
                   
         </div>



        </asp:Panel>


         <asp:Panel ID="ItemPanel" runat="server" Visible="false">
             <div class="container" align="center" >
         <br />
                <div class="row" align="center">
                     <div class="col"  >
                      
                         <asp:Image ID="Image3" runat="server"  />
                         </div>

                    <div class="col" align="left" >
                      
                        <asp:Label ID="ItemNameLBL" runat="server" Text="" Font-Size="XX-Large" Font-Bold="true"></asp:Label>
                        <br />
                        <asp:Label ID="ItemPriceLBL" runat="server" Text="" Font-Size="Large" Font-Bold="false"></asp:Label>

                         </div>
                    </div>
         </div>



        </asp:Panel>

         <asp:Panel ID="Panel2" runat="server" style="position: fixed;left: 0;bottom: 0;width: 100%;box-shadow: 1px 1px 1px 1px gray;">

             <div class="row">
                 <div class="col" style="padding:20px">
                     <asp:Panel ID="CartButtonsPanel" runat="server" Visible="false">
                     <asp:Button ID="RemoveBtn" runat="server" Text="-" CssClass="btn" ForeColor="#2B3234" OnClick="RemoveBtn_Click"  />
                     <asp:Label ID="CountLBL" runat="server"  Font-Size="Medium" ForeColor="#585E5F"></asp:Label>
                      <asp:Button ID="AddBtn" runat="server" Text="+" CssClass="btn"  ForeColor="#2B3234" OnClick="AddBtn_Click" />
                     </asp:Panel>
                 </div>

                 <div class="col" style="padding:20px">
                     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="updatePanel" runat="server">
    <ContentTemplate>
       <%-- <asp:Label ID="lblItemDetails" runat="server" Text="fk"></asp:Label>--%>




                    
    </ContentTemplate>
</asp:UpdatePanel>
                     <asp:Button ID="AddToOrderBTN" runat="server" Text="Add to Order" Width="100%" BackColor="#C93127" ForeColor="White" Font-Bold="true" Font-Size="Medium" CssClass="table-responsive-lg" Height="50px" BorderStyle="None" OnClick="AddToOrderBTN_Click"  />
                 </div>
                 
             </div>
             </asp:Panel>


    </form>
</body>
</html>
