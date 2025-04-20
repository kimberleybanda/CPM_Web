<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Root.master" CodeBehind="DealsPayments.aspx.cs" Inherits="CPMv2.DealsPayments" Title="GridView" %>

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

        function grid_CustomButtonClick(sender, e) {
            var rowKey = sender.GetRowKey(e.visibleIndex);
            window.location.href = "/invoice_print.aspx?zx="+rowKey;
        }
          function OnContextMenuItemClick(sender, e) {

              if (e.item.name == "Pay") {
                  e.processOnServer = true;
                  e.usePostBack = true;
              }
              if (e.item.name == "Reject") {
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

          <div class="card z-index-0 fadeIn3 fadeInBottom">
          <div class="card-body p-3">
              <div class="card-header pb-0 p-3">
                  <div class="row">
                      <div class="col-6 d-flex align-items-center">
                          <h6 class="mb-0">Deals Payments </h6>
                      </div>
                  </div>
              </div>
              <div class="col-lg-6 col-7">

                  <dx:ASPxGridView  KeyFieldName="id" OnContextMenuItemClick="Grid_ContextMenuItemClick"  OnContextMenuInitialize="Grid_ContextMenuInitialize" ID="GridView1" runat="server" Width="100%">
                      <ClientSideEvents CustomButtonClick="grid_CustomButtonClick" /> 
                      <Columns>
                          <dx:GridViewCommandColumn Width="100" VisibleIndex="0">  
                              <CustomButtons>  
                                  <dx:GridViewCommandColumnCustomButton  Text="View Invoice"
                                                                         ID="ShowNewWindow">  
                                  </dx:GridViewCommandColumnCustomButton>  
                              </CustomButtons>  
                          </dx:GridViewCommandColumn>
                          <dx:GridViewDataTextColumn FieldName="id"/>
                          <dx:GridViewDataTextColumn FieldName="amount"/>
                          <dx:GridViewDataTextColumn FieldName="qty"/>
                          <dx:GridViewDataTextColumn FieldName="productName"/>
                          <dx:GridViewDataTextColumn FieldName="status" Caption="Approved"/>
                      
                      </Columns>
                      <ClientSideEvents ContextMenuItemClick="OnContextMenuItemClick"/>
                      <SettingsSearchPanel Visible="true"/>
                      <SettingsBehavior AllowSelectByRowClick="true" />
                      <SettingsContextMenu Enabled="True" EnableRowMenu="True">
                          <RowMenuItemVisibility>
                              <ExportMenu Visible="False"/>
                          
                          </RowMenuItemVisibility>
                      </SettingsContextMenu>
                      <SettingsDataSecurity AllowReadUnlistedFieldsFromClientApi="True" AllowEdit="False" AllowInsert="False" AllowDelete="False" />

                  </dx:ASPxGridView>
              </div>
          </div>
        </div>
                
                       <dx:ASPxPopupControl ID="pcSearch" runat="server" Width="320" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
       PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcSearch"
       HeaderText="Ecocash Payment" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" AutoUpdatePosition="true">
       <ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('entryGroup'); pcSearch.Focus(); }" />
       <ContentCollection>
           <dx:PopupControlContentControl runat="server">
               <dx:ASPxPanel ID="Panel1" runat="server" DefaultButton="btOK">
                   <PanelCollection>
                       <dx:PanelContent runat="server">
                           <dx:ASPxFormLayout   runat="server" ID="ASPxFormLayout1" Width="100%" Height="100%">
                               <Items>
                                   <dx:LayoutGroup Caption="Pay" Width="300px" ColCount="2" ColumnCount="2" ColSpan="1">
                                       <Items>
                                           <dx:LayoutItem Caption="Amount" ColSpan="1" FieldName="Name">
                                               <LayoutItemNestedControlCollection>
                                                   <dx:LayoutItemNestedControlContainer runat="server">
                                                       <dx:ASPxTextBox ID="txtAmount" runat="server" Enabled="False" ReadOnly="True" Width="200px" ClientInstanceName="txtName">
                                                       </dx:ASPxTextBox>
                                                   </dx:LayoutItemNestedControlContainer>
                                               </LayoutItemNestedControlCollection>
                                           </dx:LayoutItem>
                                           
                                           <dx:LayoutItem Caption="Ecocash Phone" ColSpan="1" FieldName="Name">
                                               <LayoutItemNestedControlCollection>
                                                   <dx:LayoutItemNestedControlContainer runat="server">
                                                       <dx:ASPxTextBox ID="txtEcoPhone" runat="server" ReadOnly="False" Width="200px" ClientInstanceName="txtName">
                                                       </dx:ASPxTextBox>
                                                   </dx:LayoutItemNestedControlContainer>
                                               </LayoutItemNestedControlCollection>
                                           </dx:LayoutItem>
                                       
                                     <dx:LayoutItem Caption="" ColSpan="1">
                      <LayoutItemNestedControlCollection>
                          <dx:LayoutItemNestedControlContainer runat="server">
                              <dx:ASPxButton   ID="ASPxButton1" runat="server" Width="100" Height="10" Text="Pay" OnClick="PayButton_Click"></dx:ASPxButton>
                          </dx:LayoutItemNestedControlContainer>
                      </LayoutItemNestedControlCollection>
                  </dx:LayoutItem>
                                       </Items>
                                   </dx:LayoutGroup>

                               </Items>
                           </dx:ASPxFormLayout>
                       </dx:PanelContent>
                   </PanelCollection>
               </dx:ASPxPanel>

           </dx:PopupControlContentControl>
       </ContentCollection>
       <ContentStyle>
           <Paddings PaddingBottom="5px" />
       </ContentStyle>
   </dx:ASPxPopupControl>

</div>    <asp:ObjectDataSource ID="ProductsDataSource" runat="server" DataObjectTypeName="CPMv2.Code.ProductsModel"
                                TypeName="CPMv2.Code.ProductsContextProvider"
                                SelectMethod="GetProduct"></asp:ObjectDataSource>
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