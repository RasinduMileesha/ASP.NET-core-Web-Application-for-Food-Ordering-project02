
<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MasterPage.Master" AutoEventWireup="true" CodeBehind="MainMenu.aspx.cs" Inherits="CTAProject.Pages.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
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




      <br />
      <div class="row" align="left"  >
          <br />
          <div class="col" align="left"  >
        &nbsp;&nbsp;&nbsp;&nbsp;  <asp:Button ID="BackBtn" runat="server" Text="Back" OnClick="BackBtn_Click" Width="90px"  BackColor="#212F3C" ForeColor="White"  Style="border-radius: 11px;" Height="40px" Font-Bold="true"/>
              </div>
             <div class="col" align="right"  >
             <asp:Button ID="StartBtn" runat="server" Text="Start Again"  Width="90px" OnClick="StartBtn_Click" OnClientClick="return showConfirmation();" BackColor="#212F3C" ForeColor="White"  Style="border-radius: 11px;" Height="40px" Font-Bold="true" />&nbsp;&nbsp;&nbsp;&nbsp;
                    </div>
          </div>
       
 
     <div class="container" align="center">
       




                <div class="row" align="center"  id="div_test" runat="server" >

                 


                    </div>
         </div>
</asp:Content>

