<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Root.master" CodeBehind="Products.aspx.cs" Inherits="CPMv2.Products" Title="GridView" %>

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
    
    <!--Start of Tawk.to Script-->
    <script type="text/javascript">
        var Tawk_API = Tawk_API || {}, Tawk_LoadStart = new Date();
        (function () {
            var s1 = document.createElement("script"), s0 = document.getElementsByTagName("script")[0];
            s1.async = true;
            s1.src = 'https://embed.tawk.to/67e3c0d7fdf8c219086c0831/1in8qijcm';
            s1.charset = 'UTF-8';
            s1.setAttribute('crossorigin', '*');
            s0.parentNode.insertBefore(s1, s0);
        })();
    </script>
    <!--End of Tawk.to Script-->
</asp:Content>



<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="server">
  
      <div class="container-fluid py-2">
      <div class="col-12">
          <div class="card my-4">
              <div class="card-header p-0 position-relative mt-n4 mx-3 z-index-2">
                  <div class="bg-gradient-success shadow-dark border-radius-lg pt-4 pb-3">
                     
                      <div class="row">
                          <h6 class="text-white text-capitalize ps-3">Product Deals</h6>
                      </div>
                  </div>
                  
                 
              </div>
<%--              <dx:ASPxVerticalGrid ID="ASPxVerticalGrid1" ClientInstanceName="grid" runat="server" DataSourceID="ProductsDataSource" Width="100%" KeyFieldName="ID">
                  <ClientSideEvents SelectionChanged="OnRecordSelectionChanged" />
                  <Rows>
                      <dx:VerticalGridCommandRow ShowSelectCheckbox="true" />
                      <dx:VerticalGridImageRow FieldName="PhotoUrl" Caption="Photo">
                          <PropertiesImage ImageHeight="180" />
                      </dx:VerticalGridImageRow>
                      <dx:VerticalGridDataRow FieldName="Brand" RecordStyle-HorizontalAlign="Center" />
                      <dx:VerticalGridDataRow FieldName="Model" RecordStyle-HorizontalAlign="Center" />
                      <dx:VerticalGridDataRow FieldName="Rating" RecordStyle-HorizontalAlign="Center">
                          <DataItemTemplate>
                              <dx:ASPxRatingControl runat="server" Value="<%# Convert.ToDecimal(Container.Text) %>" ReadOnly="true" />
                          </DataItemTemplate>
                      </dx:VerticalGridDataRow>
                      <dx:VerticalGridTextRow FieldName="Price">
                          <PropertiesTextEdit DisplayFormatString="c" />
                      </dx:VerticalGridTextRow>
                  </Rows>
                  <Settings HeaderAreaWidth="100" RecordWidth="200" HorizontalScrollBarMode="Auto" />
                  <SettingsPager PageSize="10" EnableAdaptivity="true" />
              </dx:ASPxVerticalGrid>--%>

              <dx:ASPxVerticalGrid ID="VerticalGrid" runat="server" DataSourceID="ProductsDataSource" OnHeaderFilterFillItems="VerticalGrid_HeaderFilterFillItems"
                                   OnSelectionChanged="VerticalGrid_SelectionChanged"        Width="100%" EnableRecordsCache="False" AutoGenerateRows="False">
                  <Rows>
                      <dx:VerticalGridImageRow FieldName="imageUrl" Caption="Photo" Settings-AllowHeaderFilter="False">
                          <PropertiesImage ImageWidth="200" ImageHeight="132" />
                      </dx:VerticalGridImageRow>
                      <dx:VerticalGridTextRow FieldName="price">
                          <PropertiesTextEdit DisplayFormatString="c" />
                          <RecordStyle Font-Bold="true" />
                      </dx:VerticalGridTextRow>
                      <dx:VerticalGridCategoryRow Caption="Information">
                          <Rows>
                              <dx:VerticalGridDataRow FieldName="about" Settings-AllowSort="False" Settings-AllowHeaderFilter="False" />
                          </Rows>
                      </dx:VerticalGridCategoryRow>
                      <dx:VerticalGridTextRow FieldName="id">
                      </dx:VerticalGridTextRow>

                      <dx:VerticalGridCommandRow ShowNewButtonInHeader="true" ShowSelectButton="True">
                          <RecordStyle HorizontalAlign="Center" />
                      </dx:VerticalGridCommandRow>
                    
                  </Rows>
                
                  <Settings HeaderAreaWidth="150" RecordWidth="220" ShowCategoryIndents="false" ShowHeaderFilterButton="true" HorizontalScrollBarMode="Auto" />
                  <SettingsPager EnableAdaptivity="true" />
                  <SettingsPopup>
                      <HeaderFilter MinWidth="250">
                          <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="768" MinHeight="300" />
                      </HeaderFilter>
                  </SettingsPopup>
              </dx:ASPxVerticalGrid>
              

          </div>
          
          
          
          <div class="col-lg-6 col-md-6 mt-4 mb-4">
              <div class="card">
                  <div class="card-body">
                      <h6 class="mb-0">Create Deals</h6>
                      <div class="pe-2">
                          <div class="chart">
                              <div class="row">
                                  <div class="col-md-6 mb-md-0 mb-4">
                                      <label class="form-label">Select Product</label>
                                      <div class="input-group input-group-outline my-2">
                                          <asp:DropDownList OnSelectedIndexChanged="drpClients_SelectedIndexChanged" class="form-control"  ID="drpProduct" runat="server" Width="300px" AutoPostBack="true">
                                          </asp:DropDownList>
                                      </div>
                                      <asp:TextBox ID="txtStoreCliID" Visible="False" Enabled="False"   class="form-control" runat="server"/>
                                  </div>
                                  <div class="col-md-6 mb-md-0 mb-4">
                                      <label class="form-label">Qty</label>
                                      <div class="input-group input-group-outline my-2">
                                          <asp:TextBox AutoPostBack="True" OnTextChanged="qty_changed" Enabled="True" id="txtQty"  class="form-control" runat="server"/>
                                      </div>
                                  </div> 
                                  
                                  <div class="col-md-6 mb-md-0 mb-4">
                                      <label class="form-label">Total Amount</label>
                                      <div class="input-group input-group-outline my-2">
                                          <asp:TextBox Enabled="False" id="txtTotalAmount"  class="form-control" runat="server"/>
                                      </div>
                                  </div> 
                                  
                                  <div class="col-md-6 mb-md-0 mb-4">
                                      <label class="form-label">Incentive Type</label>
                                      <div class="input-group input-group-outline my-2">
                                          <asp:TextBox Enabled="True" id="TextBox1"  class="form-control" runat="server"/>
                                      </div>
                                  </div> 
                                  
                                  <asp:Button OnClick="btnCreateDeal_Click" ID="btnCreateDeal"  class="btn bg-gradient-dark w-100 my-4 mb-2" Text="Create" runat="server"/>

                              </div>
                          </div>
                      </div>
                      <hr class="dark horizontal">
                      <div class="d-flex">
                          <i class="material-symbols-rounded text-sm my-auto me-1">schedule</i>
                          <p class="mb-0 text-sm">1 jan 2024</p>
                      </div>
                  </div>
              </div>
          </div>
      </div>
  </div>
    <asp:ObjectDataSource ID="ProductsDataSource" runat="server" DataObjectTypeName="CPMv2.Code.ProductsModel"
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