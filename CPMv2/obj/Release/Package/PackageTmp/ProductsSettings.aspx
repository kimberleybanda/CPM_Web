<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/Root.master" CodeBehind="ProductsSettings.aspx.cs" Inherits="CPMv2.ProductsSettings" Title="GridView" %>

<%@ Register Src="~/controls/sidebar.ascx" TagPrefix="uc1" TagName="sidebar" %>


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
                             <h6 class="mb-0">Product Price Settings </h6>
                         </div>
                     </div>
                 </div>
                 <div class="col-lg-6 col-7">

                     <div class="row">
                         <asp:TextBox Visible="False" id="txtID" class="form-control" runat="server"/>
                     </div>
                     
                     <div class="row">
                         <div class="col-md-6 mb-md-0 mb-4">
                             <label class="form-label">Select Product </label>
                             <div class="input-group input-group-outline my-2">
                                 <asp:DropDownList OnSelectedIndexChanged="drpClients_SelectedIndexChanged" class="form-control"  ID="drpProduct" runat="server" Width="300px" AutoPostBack="false">

                                 </asp:DropDownList>
                             </div>
                             <asp:TextBox ID="txtStoreCliID" Visible="False" Enabled="False"   class="form-control" runat="server"/>
                         </div>
                         <div class="col-md-6 mb-md-0 mb-4">
                             <label class="form-label">Price</label>
                             <div class="input-group input-group-outline my-2">
                                 <asp:TextBox Enabled="True" id="txtPrice"  class="form-control" runat="server"/>
                             </div>

                         </div> 
                         </div>


                     <div class="row">
                         <div class="col-md-6 mb-md-0 mb-4">
                             <label class="form-label">Attach Product Image</label>
                             <div class="input-group input-group-outline my-2">
                                 <asp:FileUpload ID="fileUpload" runat="server"/>
                             </div>
                         </div>
                     </div>
                     <asp:Button OnClick="CreateProductsPrice_Click" ID="btnCreateProductPrice"  class="btn bg-gradient-dark w-100 my-4 mb-2" Text="Create" runat="server"/>
                 </div>
             </div>
           </div>

   </div>
    
            <div class="container-fluid py-2">

          <div class="card z-index-0 fadeIn3 fadeInBottom">
          <div class="card-body p-3">
              <div class="card-header pb-0 p-3">
                  <div class="row">
                      <div class="col-6 d-flex align-items-center">
                          <h6 class="mb-0">Product Price Settings </h6>
                      </div>
                  </div>
              </div>
              <div class="col-lg-6 col-7">

                  <dx:ASPxGridView  KeyFieldName="id" OnContextMenuItemClick="Grid_ContextMenuItemClick"  OnContextMenuInitialize="Grid_ContextMenuInitialize" ID="GridView1" runat="server" Width="100%">
                      <Columns>
                          <dx:GridViewDataTextColumn FieldName="id"/>
                          <dx:GridViewDataTextColumn FieldName="price"/>
                          <dx:GridViewDataTextColumn FieldName="about"/>
                      </Columns>
                      <ClientSideEvents ContextMenuItemClick="OnContextMenuItemClick"/>
                      <SettingsSearchPanel Visible="true"/>
                      <SettingsBehavior AllowSelectByRowClick="true" />
                      <SettingsContextMenu Enabled="True" EnableRowMenu="True">
                          <RowMenuItemVisibility>
                              <ExportMenu Visible="False"/>
                          
                          </RowMenuItemVisibility>
                      </SettingsContextMenu>
                  </dx:ASPxGridView>
              </div>
          </div>
        </div>

</div>    <asp:ObjectDataSource ID="ProductsDataSource" runat="server" DataObjectTypeName="CPMv2.Code.ProductsModel"
                                TypeName="CPMv2.Code.ProductsContextProvider"
                                SelectMethod="GetProducts"></asp:ObjectDataSource>
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