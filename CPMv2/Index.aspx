<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Root.master" CodeBehind="Index.aspx.cs" Inherits="CPMv2.Index" Title="GridView" %>

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
    
    <script src="Content/assets/js/loader.js" crossorigin="anonymous"></script>
    
    
    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);
        function drawChart() {
            var data = google.visualization.arrayToDataTable([
                ['Year', 'Sales', 'Expenses'],
                ['2004', 1000, 400],
                ['2005', 1170, 460],
                ['2006', 660, 1120],
                ['2007', 1030, 540]
            ]);
            var options = {
                title: 'Company Performance',
                curveType: 'function',
                legend: { position: 'bottom' }
            };

            var chart = new google.visualization.LineChart(document.getElementById('curve_chart'));

            chart.draw(data, options);
        }
    </script>
    
    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);
        function drawChart() {
            // Fetch data from the API
            fetch('http://localhost:8500/v1/api/sales_stats')
                .then(response => response.json())
                .then(data => {
                    // Transform the data into the format required by Google Charts
                    var chartData = [['Product', 'Count']];
                    data.forEach(item => {
                        chartData.push([item.productName, item.productCount]);
                    });

                    // Create the data table
                    var dataTable = google.visualization.arrayToDataTable(chartData);
                    // Set chart options
                    var options = {
                        title: 'Location Based Sales'
                    };
                    // Instantiate and draw the chart
                    var chart = new google.visualization.PieChart(document.getElementById('piechart'));
                    chart.draw(dataTable, options);
                })
                .catch(error => console.error('Error fetching data:', error));
        }
    </script>
    
    
    <script type="text/javascript">
          google.charts.load('current', { 'packages': ['corechart'] });
          google.charts.setOnLoadCallback(drawChart);
          function drawChart() {
              // Fetch data from the API
              fetch('http://localhost:8500/v1/api/sales_stats2')
                  .then(response => response.json())
                  .then(data => {
                      // Transform the data into the format required by Google Charts
                      var chartData = [['Product', 'Count']];
                      data.forEach(item => {
                          chartData.push([item.productName, item.productCount]);
                      });

                      // Create the data table
                      var dataTable = google.visualization.arrayToDataTable(chartData);
                      // Set chart options
                      var options = {
                          title: 'Product Sales Statistics'
                      };
                      // Instantiate and draw the chart
                      var chart = new google.visualization.BarChart(document.getElementById('histchart'));
                      chart.draw(dataTable, options);
                  })
                  .catch(error => console.error('Error fetching data:', error));
          }
    </script>
    

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
    <main class="main-content position-relative max-height-vh-100 h-100 border-radius-lg ">
    <!-- Navbar -->
    <!-- End Navbar -->
    <div class="container-fluid py-2">
      <div class="row">
        <div class="col-xl-3 col-sm-6 mb-xl-0 mb-4">
          <div class="card">
            <div class="card-header p-2 ps-3">
              <div class="d-flex justify-content-between">
                <div>
                  <p class="text-sm mb-0 text-capitalize">Pending Approvals</p>
                    <asp:Label class="mb-0" ID="lblClosedDeals" runat="server"></asp:Label>
                </div>
                <div class="icon icon-md icon-shape bg-gradient-dark shadow-dark shadow text-center border-radius-lg">
                  <i class="material-symbols-rounded opacity-10">weekend</i>
                </div>
              </div>
            </div>
            <hr class="dark horizontal my-0">
          </div>
        </div>
        <div class="col-xl-3 col-sm-6 mb-xl-0 mb-4">
          <div class="card">
            <div class="card-header p-2 ps-3">
              <div class="d-flex justify-content-between">
                <div>
                  <p class="text-sm mb-0 text-capitalize">Approved Users</p>
                    <asp:Label class="mb-0" ID="lblOpenDeals" runat="server"></asp:Label>
                </div>
                <div class="icon icon-md icon-shape bg-gradient-dark shadow-dark shadow text-center border-radius-lg">
                  <i class="material-symbols-rounded opacity-10">person</i>
                </div>
              </div>
            </div>
            <hr class="dark horizontal my-0">
          </div>
        </div>
        <div class="col-xl-3 col-sm-6 mb-xl-0 mb-4">
          <div class="card">
            <div class="card-header p-2 ps-3">
              <div class="d-flex justify-content-between">
                <div>
                  <p class="text-sm mb-0 text-capitalize">Sales</p>
                    <asp:Label class="mb-0" ID="lblSales" runat="server"></asp:Label>

                </div>
                <div class="icon icon-md icon-shape bg-gradient-dark shadow-dark shadow text-center border-radius-lg">
                  <i class="material-symbols-rounded opacity-10">leaderboard</i>
                </div>
              </div>
            </div>
            <hr class="dark horizontal my-0">
          </div>
        </div>
        <div class="col-xl-3 col-sm-6">
          <div class="card">
            <div class="card-header p-2 ps-3">
              <div class="d-flex justify-content-between">
                <div>
                  <p class="text-sm mb-0 text-capitalize">Sales</p>
                  <h4 class="mb-0">$103,430</h4>
                </div>
                <div class="icon icon-md icon-shape bg-gradient-dark shadow-dark shadow text-center border-radius-lg">
                  <i class="material-symbols-rounded opacity-10">weekend</i>
                </div>
              </div>
            </div>
            <hr class="dark horizontal my-0">
          </div>
        </div>
      </div>
      <div class="row">
       
          <div class="col-lg-6 col-md-6 mt-4 mb-4">
              <div class="card">
                  <div class="card-body">
                      <h6 class="mb-0">Sales</h6>
                      <div class="pe-2">
                          <div class="chart">
                              <!-- Pie Chart Container -->
                              <div id="piechart" style="width: 100%; height: 300px;"></div>
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
          <div class="col-lg-6 col-md-6 mt-4 mb-4">
              <div class="card">
                  <div class="card-body">
                      <h6 class="mb-0">High Selling Product</h6>
                      <div class="pe-2">
                          <div class="chart">
                              <!-- Pie Chart Container -->
                              <div id="histchart" style="width: 100%; height: 300px"></div>
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
      <footer class="footer py-4  ">
        <div class="container-fluid">
          <div class="row align-items-center justify-content-lg-between">
            <div class="col-lg-6 mb-lg-0 mb-4">
              <div class="copyright text-center text-sm text-muted text-lg-start">
                © <script>
                      document.write(new Date().getFullYear())
                </script>,
                made with <i class="fa fa-heart"></i> by
                <a href="https://www.creative-tim.com" class="font-weight-bold" target="_blank">Creative Tim</a>
                for a better web.
              </div>
            </div>
            <div class="col-lg-6">
              <ul class="nav nav-footer justify-content-center justify-content-lg-end">
                <li class="nav-item">
                  <a href="https://www.creative-tim.com" class="nav-link text-muted" target="_blank">Creative Tim</a>
                </li>
                <li class="nav-item">
                  <a href="https://www.creative-tim.com/presentation" class="nav-link text-muted" target="_blank">About Us</a>
                </li>
                <li class="nav-item">
                  <a href="https://www.creative-tim.com/blog" class="nav-link text-muted" target="_blank">Blog</a>
                </li>
                <li class="nav-item">
                  <a href="https://www.creative-tim.com/license" class="nav-link pe-0 text-muted" target="_blank">License</a>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </footer>
    </div>
  </main>

    <asp:ObjectDataSource ID="GridViewDataSource" runat="server" DataObjectTypeName=" CPMv2.Model.Issue"
        TypeName=" CPMv2.Model.DataProvider"
        SelectMethod="GetIssues" InsertMethod="AddNewIssue" UpdateMethod="UpdateIssue"></asp:ObjectDataSource>

    <asp:ObjectDataSource ID="ContactsDataSource" runat="server" DataObjectTypeName=" CPMv2.Model.Contact"
        TypeName=" CPMv2.Model.DataProvider"
        SelectMethod="GetContacts"></asp:ObjectDataSource>

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