<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Root.master" CodeBehind="Training.aspx.cs" Inherits="CPMv2.Training" Title="GridView" %>

<%@ Register Src="~/controls/sidebar.ascx" TagPrefix="uc1" TagName="sidebar" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI.HtmlControls" Assembly="System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" %>


<asp:Content runat="server" ContentPlaceHolderID="Head">
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Content/GridView.css") %>' />
    <script type="text/javascript" src='<%# ResolveUrl("~/Content/GridView.js") %>'></script>
        <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Inter:300,400,500,600,700,900" />
    <!-- Nucleo Icons -->
    <link href="Content/assets/css/nucleo-icons.css" rel="stylesheet" />
    <link href="Content/assets/css/nucleo-svg.css" rel="stylesheet" />
    <!-- Font Awesome Icons -->
    <script src="https://kit.fontawesome.com/42d5adcbca.js" crossorigin="anonymous"></script>
    <!-- Material Icons -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Rounded:opsz,wght,FILL,GRAD@24,400,0,0" />
    <!-- CSS Files -->
    <link id="pagestyle" href="Content/assets/css/material-dashboard.css?v=3.2.0" rel="stylesheet" />
    
    <script src="Content/assets/js/core/popper.min.js"></script>
    <script src="Content/assets/js/core/bootstrap.min.js"></script>
    <script src="Content/assets/js/plugins/perfect-scrollbar.min.js"></script>
    <script src="Content/assets/js/plugins/smooth-scrollbar.min.js"></script>
    <script>
      var win = navigator.platform.indexOf('Win') > -1;
      if (win && document.querySelector('#sidenav-scrollbar')) {
          var options = {
              damping: '0.5'
          }
          Scrollbar.init(document.querySelector('#sidenav-scrollbar'), options);
      }
    </script>
    <!-- Github buttons -->
    <script async defer src="https://buttons.github.io/buttons.js"></script>
    <!-- Control Center for Material Dashboard: parallax effects, scripts for the example pages etc -->
    <script src="Content/assets/js/material-dashboard.min.js?v=3.2.0"></script>
    
    <script type="text/javascript">
          function OnContextMenuItemClick(sender, e) {

              if (e.item.name == "Edit") {
                  e.processOnServer = true;
                  e.usePostBack = true;
              }
              if (e.item.name == "Delete") {
                  e.processOnServer = true;
                  e.usePostBack = true;
              }

        }

        function OnContextMenuItemClickx(sender, e) {

            if (e.item.name == "Edit") {
                e.processOnServer = true;
                e.usePostBack = true;
            }
            if (e.item.name == "Delete") {
                e.processOnServer = true;
                e.usePostBack = true;
            }

        }
    </script>
</asp:Content>



<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="server">
           <div class="container-fluid py-2">

             <div class="card z-index-0 fadeIn3 fadeInBottom">
             <div class="card-body p-3">
                 <div class="card-header pb-0 p-3">
                     <div class="row">
                         <div class="col-6 d-flex align-items-center">
                             <h6 class="mb-0">Tutorial Video </h6>
                         </div>
                     </div>
                 </div>
                 <div class="row">
                     <asp:Repeater ID="Repeater1" runat="server">
                         <ItemTemplate>
                             <div class="col-lg-6 col-7">
                                 <iframe
                                     width="540"
                                     height="310"
                                     src='<%# Eval("vidNameUrl") %>'
                                     frameborder="0"
                                     allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
                                     allowfullscreen>
                                 </iframe>
                             </div>
                         </ItemTemplate>
                     </asp:Repeater>
                    
                     </div>
              
             </div>
           </div>

   </div>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="LeftPanelContent">
    <uc1:sidebar runat="server" ID="sidebar" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="RightPanelContent">
    <div class="settings-content">
        <h2>Settings</h2>
        <p>Place your content here</p>
    </div>
</asp:Content>