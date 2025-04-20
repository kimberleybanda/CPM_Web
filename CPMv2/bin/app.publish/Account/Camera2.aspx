<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/Root.master" CodeBehind="Camera2.aspx.cs" Inherits="CPMv3.Camera2" Title="Sign In" %>

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
          const cameraView = document.getElementById("camera");
          const canvas = document.getElementById("capture-canvas");
          const photoImg = document.getElementById("photo-img");
          const captureBtn = document.getElementById("capture_btn");

          if (!cameraView || !canvas || !photoImg || !captureBtn) {
              console.error("One or more elements not found. Check IDs.");
              return;
          }

          let stream = null;
          let photoData = null;

          async function startCamera() {
              try {
                  stream = await navigator.mediaDevices.getUserMedia({
                      video: { width: { ideal: 100 }, height: { ideal: 100 } } // Reduced resolution
                  });
                  cameraView.srcObject = stream;
              } catch (err) {
                  console.error("Error accessing camera:", err);
                  alert("Could not access the camera. Please allow permissions.");
              }
          }

          function stopCamera() {
              if (stream) {
                  stream.getTracks().forEach(track => track.stop());
                  stream = null;
                  cameraView.srcObject = null;
                  console.log("Camera stream stopped.");
              }
          }

          captureBtn.addEventListener("click", () => {
              if (!stream) {
                  alert("No camera stream available.");
                  return;
              }

              // Draw video to canvas with reduced resolution
              canvas.width = 100; // Reduced from 250
              canvas.height = 100; // Reduced from 250
              canvas.getContext("2d").drawImage(cameraView, 0, 0, canvas.width, canvas.height);

              // Convert to blob with lower quality
              canvas.toBlob((blob) => {
                  photoData = blob;
                  photoImg.src = URL.createObjectURL(blob);
                  photoImg.style.display = "block";

                  // Convert blob to base64
                  const reader = new FileReader();
                  reader.onload = function () {
                      const base64data = reader.result; // Full data URL (e.g., "data:image/jpeg;base64,...")
                      const base64String = base64data.split(",")[1]; // Extract just the base64 part
                      const encodedPhoto = encodeURIComponent(base64String);

                      console.log("Base64 string length:", encodedPhoto.length); // Log length for debugging

                      // Stop the camera before redirecting
                      stopCamera();

                      // Redirect with base64 string
                      alert("Photo captured! Redirecting to Register2.aspx...");
                      window.location.href = `Register2.aspx?photo=${encodedPhoto}`;
                  };
                  reader.readAsDataURL(blob);
              }, "image/jpeg", 0.3); // Reduced quality from 0.7 to 0.3
          });

          // Cleanup stream on page unload (optional, as stopCamera handles it)
          window.addEventListener("beforeunload", () => {
              stopCamera();
          });

          startCamera();
      });
  </script>

</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="server">
     <main class="main-content  mt-0">
    <div class="page-header align-items-start min-vh-100" style="background-image: url('https://images.unsplash.com/photo-1497294815431-9365093b7331?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1950&q=80');">
      <span class="mask bg-gradient-dark opacity-6"></span>
      <div class="container my-auto">
        <div class="row">
          <div class="col-lg-4 col-md-8 col-12 mx-auto" style="height:550px !important">
            <div class="card z-index-0 fadeIn3 fadeInBottom">
              <div class="card-body">
                <form role="form" class="text-start">

                 <%--   ============================--%>
                    
                    <label class="form-label">Full Name</label>
                    <div class="input-group input-group-outline my-3">
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" />
                    </div>
                    
                    <div class="input-group input-group-outline my-3">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" />
                    </div>

                    <!-- Webcam Section -->
                    <label class="form-label">Profile Photo</label>
                    <div class="mb-3 text-center">
                        <video id="camera" width="350" height="350" autoplay="autoplay"  style="max-width: 200px; max-height: 200px; object-fit: cover;"></video>
                        <canvas id="capture-canvas" width="250" height="250" style="display: none;"></canvas>
                        <div id="photo" style="margin-top: 10px;">
                            <img id="photo-img" style="display: none; max-width: 250px; height: auto; border: 1px solid #ccc;" alt="Captured Photo" />
                        </div>
                       <%-- <div class="mt-2">
                            <asp:Button ID="capture_btn" runat="server" Text="Capture Photo" CssClass="btn btn-outline-dark" ClientIDMode="Static" />
                        </div>--%>
                    </div>
               <%--     ===========================--%>
                    
                  
                  <div class="text-center">
                      <asp:Button OnClick="SignInButton_Click" runat="server" ID="capture_btn" ClientIDMode="Static"  class="btn bg-gradient-dark w-100 my-4 mb-2" Text="Take Photo" runat="server"/>
<%--                    <button type="button" class="btn bg-gradient-dark w-100 my-4 mb-2">Sign in</button>--%>
                  </div>
                
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