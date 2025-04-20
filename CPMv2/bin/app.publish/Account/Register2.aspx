<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/Root.master" CodeBehind="Register3.aspx.cs" Inherits="CPMv3.Register3" Title="Sign In" %>

<asp:Content runat="server" ContentPlaceHolderID="Head">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/SignInRegister.css") %>' />
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/SignInRegister.js") %>'></script>
    
    <link href="../Content/assets/css/nucleo-icons.css" rel="stylesheet" />
    <link href="../Content/assets/css/nucleo-svg.css" rel="stylesheet" />
    <!-- Font Awesome Icons -->
    <script src="https://kit.fontawesome.com/42d5adcbca.js" crossorigin="anonymous"></script>
    <!-- Material Icons -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Rounded:opsz,wght,FILL,GRAD@24,400,0,0" />
    <!-- CSS Files -->
    <link id="pagestyle" href="../Content/assets/css/material-dashboard.css?v=3.2.0" rel="stylesheet" />
    
    <script src="../Content/assets/js/core/popper.min.js"></script>
    <script src="../Content/assets/js/core/bootstrap.min.js"></script>
    <script src="../Content/assets/js/plugins/perfect-scrollbar.min.js"></script>
    <script src="../Content/assets/js/plugins/smooth-scrollbar.min.js"></script>
    
    <!-- JavaScript for Webcam -->
    <script>
            window.addEventListener("DOMContentLoaded", () => {
                // Get the photo parameter from the query string
                const urlParams = new URLSearchParams(window.location.search);
                const base64String = urlParams.get("photo");

                if (base64String) {
                    // Construct data URL
                    const dataUrl = `data:image/jpeg;base64,${base64String}`;
                    const img = document.getElementById("capturedPhoto");
                    img.src = dataUrl;
                    img.style.display = "block"; // Show the image
                    console.log("Image loaded from base64 string.");
                } else {
                    console.error("No photo parameter found in query string.");
                    alert("No photo data available.");
                }
            });
        </script>

</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="server">
     <main class="main-content  mt-0">
    <div class="page-header align-items-start min-vh-100" style="background-image: url('https://images.unsplash.com/photo-1497294815431-9365093b7331?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1950&q=80');">
      <span class="mask bg-gradient-dark opacity-6"></span>
      <div class="container my-auto">
        <div class="row">
          <div class="col-lg-4 col-md-8 col-12 mx-auto">
            <div class="card z-index-0 fadeIn3 fadeInBottom">
              <div class="card-header p-0 position-relative mt-n4 mx-3 z-index-2">
                <div class="bg-gradient-dark shadow-dark border-radius-lg py-3 pe-1">
                  <h4 class="text-white font-weight-bolder text-center mt-2 mb-0">Sign Up</h4>
                  <div class="row mt-3">
                    <div class="col-2 text-center ms-auto">
                      <a class="btn btn-link px-3" href="javascript:;">
                        <i class="fa fa-facebook text-white text-lg"></i>
                      </a>
                    </div>
                    <div class="col-2 text-center px-1">
                      <a class="btn btn-link px-3" href="javascript:;">
                        <i class="fa fa-github text-white text-lg"></i>
                      </a>
                    </div>
                    <div class="col-2 text-center me-auto">
                      <a class="btn btn-link px-3" href="javascript:;">
                        <i class="fa fa-google text-white text-lg"></i>
                      </a>
                    </div>
                  </div>
                </div>
              </div>
              <div class="card-body">
                <form role="form" class="text-start">

                    <div class=" mb-3">
                        <img id="capturedPhoto" alt="Captured Photo" style="max-width: 200px; display: none;" />
                    </div>

                    <label class="form-label">Full Name</label>
                    <div class="input-group input-group-outline my-3">
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" />
                    </div>

                    <label class="form-label">Phone</label>
                  <div class="input-group input-group-outline my-3">
                      <asp:TextBox ValidationExpression="^2637[0-9]{8}$" ID="UserNameTextBox" runat="server" CssClass="form-control" />
                  </div>
                    
                    <label class="form-label">Select Country</label>
                    <div class="input-group input-group-outline mb-3">
                        <asp:DropDownList OnSelectedIndexChanged="drpCountry_OnChanged" class="form-control"  ID="drpCountry" runat="server" Width="300px" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    
                    <label class="form-label">Select City</label>
                    <div class="input-group input-group-outline mb-3">
                        <asp:DropDownList OnSelectedIndexChanged="drpCity_OnChanged" class="form-control"  ID="drpCity" runat="server" Width="300px" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                    

                 
                    <label class="form-label">Select User Type</label>
                    <div class="input-group input-group-outline mb-3">
                        <asp:DropDownList OnSelectedIndexChanged="drpUserType_OnChanged" class="form-control"  ID="drpUserType" runat="server" Width="300px" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>

                    
                    <label class="form-label">Upload National ID</label>
                    <div class="input-group input-group-outline mb-3">
                        <asp:FileUpload ID="fileUpload1" runat="server" />
                    </div>
                    
                    <label class="form-label">3 Months Bank Statement</label>
                    <div class="input-group input-group-outline mb-3">
                        <asp:FileUpload ID="fileUpload2" runat="server" />
                    </div>
                    
                    <label class="form-label">Proof Of Residency</label>
                    <div class="input-group input-group-outline mb-3">
                        <asp:FileUpload ID="fileUpload3" runat="server" />
                    </div>
                    
                    

                    <label class="form-label">Password</label>
                  <div class="input-group input-group-outline mb-3">
                   
                      <asp:TextBox ID="PasswordButtonEdit" runat="server" CssClass="form-control" />
                  </div>
                
                  <div class="text-center">
                      <asp:Button OnClick="SignInButton_Click" ID="btnLogin"  class="btn bg-gradient-dark w-100 my-4 mb-2" Text="Create" runat="server"/>
<%--                    <button type="button" class="btn bg-gradient-dark w-100 my-4 mb-2">Sign in</button>--%>
                  </div>
                  <p class="mt-4 text-sm text-center">
                    Have an account?
                    <a href="../Account/SignIn.aspx" class="text-primary text-gradient font-weight-bold">Sign In</a>
                  </p>
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
      <footer class="footer position-absolute bottom-2 py-2 w-100">
        <div class="container">
          <div class="row align-items-center justify-content-lg-between">
            <div class="col-12 col-md-6 my-auto">
              <div class="copyright text-center text-sm text-white text-lg-start">
                © <script>
                  document.write(new Date().getFullYear())
                </script>,
                made with <i class="fa fa-heart" aria-hidden="true"></i> by
                <a href="https://www.creative-tim.com" class="font-weight-bold text-white" target="_blank">Creative Tim</a>
                for a better web.
              </div>
            </div>
            <div class="col-12 col-md-6">
              <ul class="nav nav-footer justify-content-center justify-content-lg-end">
                <li class="nav-item">
                  <a href="https://www.creative-tim.com" class="nav-link text-white" target="_blank">Creative Tim</a>
                </li>
                <li class="nav-item">
                  <a href="https://www.creative-tim.com/presentation" class="nav-link text-white" target="_blank">About Us</a>
                </li>
                <li class="nav-item">
                  <a href="https://www.creative-tim.com/blog" class="nav-link text-white" target="_blank">Blog</a>
                </li>
                <li class="nav-item">
                  <a href="https://www.creative-tim.com/license" class="nav-link pe-0 text-white" target="_blank">License</a>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </footer>
    </div>
  </main>

</asp:Content>