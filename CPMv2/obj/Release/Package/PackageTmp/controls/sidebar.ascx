<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="sidebar.ascx.cs" Inherits="CPMv2.controls.sidebar" %>
  <aside class="sidenav navbar navbar-vertical navbar-expand-xs border-radius-lg fixed-start ms-2  bg-white my-2" id="sidenav-main">
    <div class="sidenav-header">
      <i class="fas fa-times p-3 cursor-pointer text-dark opacity-5 position-absolute end-0 top-0 d-none d-xl-none" aria-hidden="true" id="iconSidenav"></i>
      <a class="navbar-brand px-4 py-3 m-0" href=" https://demos.creative-tim.com/material-dashboard/pages/dashboard " target="_blank">
        <img src="../assets/img/logo-ct-dark.png" class="navbar-brand-img" width="26" height="26" alt="main_logo">
        <span class="ms-1 text-sm text-dark">Creative Tim</span>
      </a>
    </div>
    <hr class="horizontal dark mt-0 mb-2">
    <div class="collapse navbar-collapse  w-auto " id="sidenav-collapse-main">
      <ul class="navbar-nav">
        <li class="nav-item">
          <a class="nav-link active bg-gradient-dark text-white" href="Index.aspx">
            <i class="material-symbols-rounded opacity-5">dashboard</i>
            <span class="nav-link-text ms-1">Dashboard</span>
          </a>
        </li>
          
          <li class="nav-item">
              <a class="nav-link text-dark" href="Products.aspx">
                  <i class="material-symbols-rounded opacity-5">view_in_ar</i>
                  <span class="nav-link-text ms-1">Products</span>
              </a>
          </li>
          
          <li class="nav-item">
              <a class="nav-link text-dark" href="Training.aspx">
                  <i class="material-symbols-rounded opacity-5">format_textdirection_r_to_l</i>
                  <span class="nav-link-text ms-1">Training</span>
              </a>
          </li>
          
          <li class="nav-item">
              <a class="nav-link text-dark" href="DiaryCreation.aspx">
                  <i class="material-symbols-rounded opacity-5">table_view</i>
                  <span class="nav-link-text ms-1">Logistics</span>
              </a>
          </li>
          <li class="nav-item">
              <a class="nav-link text-dark" href="ActivityGeneration.aspx">
                  <i class="material-symbols-rounded opacity-5">receipt_long</i>
                  <span class="nav-link-text ms-1">Integrations</span>
              </a>
          </li>
    
       
        <li class="nav-item mt-3">
          <h6 class="ps-4 ms-2 text-uppercase text-xs text-dark font-weight-bolder opacity-5">Settings</h6>
        </li>
        
          <li id="divProductsSettings" runat="server" class="nav-item">
              <a class="nav-link text-dark" href="ProductsSettings.aspx">
                  <i class="material-symbols-rounded opacity-5">person</i>
                  <span class="nav-link-text ms-1">Products Settings</span>
              </a>
          </li>
          
          <li id="divProductSettings" runat="server" class="nav-item">
              <a class="nav-link text-dark" href="ProductSettings.aspx">
                  <i class="material-symbols-rounded opacity-5">person</i>
                  <span class="nav-link-text ms-1">Product Settings</span>
              </a>
          </li>
          <li id="divDealsApprovals" runat="server" class="nav-item">
              <a class="nav-link text-dark" href="DealsApprovals.aspx">
                  <i class="material-symbols-rounded opacity-5">person</i>
                  <span class="nav-link-text ms-1">Deals Approvals</span>
              </a>
          </li>
          <li id="divTrainingSettings" runat="server" class="nav-item">
              <a class="nav-link text-dark" href="TrainingSettings.aspx">
                  <i class="material-symbols-rounded opacity-5">person</i>
                  <span class="nav-link-text ms-1">Training Settings</span>
              </a>
          </li>
          
          <li id="divVerificationSettings" runat="server" class="nav-item">
              <a class="nav-link text-dark" href="VerificationSettings.aspx">
                  <i class="material-symbols-rounded opacity-5">person</i>
                  <span class="nav-link-text ms-1">Verification Settings</span>
              </a>
          </li>

        <li class="nav-item">
          <a class="nav-link text-dark" href="Account/SignIn.aspx">
            <i class="material-symbols-rounded opacity-5">login</i>
            <span class="nav-link-text ms-1">Sign Out</span>
          </a>
        </li>
       
      </ul>
    </div>
 
  </aside>
