using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using CPMv2.Code;
using CPMv2.Helpers;
using CPMv2.Model;
using DevExpress.Web;
using Newtonsoft.Json;

namespace CPMv2 {

    class ActivateUser
    {
        public long id { get; set; }
        public string phone { get; set; }
    }

    public partial class VerificationSettings : System.Web.UI.Page {
        protected void GridView_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e) {
            e.NewValues["Kind"] = 1;
            e.NewValues["Priority"] = 2;
            e.NewValues["Status"] = 1;
            e.NewValues["IsDraft"] = true;
            e.NewValues["IsArchived"] = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            /* if (HttpContext.Current.Session["loggeIn"] == null)
                 Response.Redirect("~/Account/SignIn.aspx");
             if ((int)HttpContext.Current.Session["loggeIn"] == 0)
             {
                 Response.Redirect("~/Account/SignIn.aspx");
             }*/
            List<CustomUsers> customersList = AuthHelper.GetCustomUsers();
            if (IsPostBack)
            {
                
                 GridView1.DataSource = customersList;
                 GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = customersList;
                GridView1.DataBind();

            }


        }
        public static String productId;
        protected void drpClients_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }


        protected void GridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
            /*if(e.Parameters == "delete") {
                List<long> selectedIds = GridView.GetSelectedFieldValues("Id").ConvertAll(id => (long)id);
                DataProvider.DeleteIssues(selectedIds);
                GridView.DataBind();
            }*/
        }

        protected async void Grid_ContextMenuItemClick(object sender, ASPxGridViewContextMenuItemClickEventArgs e)
        {
            var trxnID = GridView1.GetSelectedFieldValues("id");
            var phoneID = GridView1.GetSelectedFieldValues("phone");
            Object Id = trxnID.First();
            String phone = phoneID.First().ToString();
            


            if (e.Item.Name == "View")
                {
                    zoomNavigator.ImageSourceFolder = "~/RegistrationsUploads/" + phone + "/";
                // zoomNavigator.ImageSourceFolder= "Uploads";
                }

                if (e.Item.Name == "Approve")
                {
              
                var client = new HttpClient();
                {
                    var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/activate_user");

                    var newPost = new ActivateUser()
                    {
                        phone = phone,
                        id = (long)Id,

                    };
                    try
                    {
                        var newPostJson = JsonConvert.SerializeObject(newPost);
                        var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                        var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;
                        var x = JsonConvert.DeserializeObject<RootActivate>(result);


                        if (x.code==200)
                        {
                            // c = true;
                            Response.Write("<script>alert('User Approved Successfully')</script>");
                            List<CustomUsers> customersList = AuthHelper.GetCustomUsers();
                                GridView1.DataSource = customersList;
                                GridView1.DataBind();

                        
                            }
                        else
                        {
                            // c = false;
                            Response.Write("<script>alert('Something Happened')</script>");
                        }
                    }
                    catch (System.Exception ess)
                    {
                        // Logging.WriteLogFile(e.ToString());
                    }
                }
            }
        }

        protected void Grid_ContextMenuInitialize(object sender, ASPxGridViewContextMenuInitializeEventArgs e)
        {

            // var rejectq = "";
            if (e.MenuType == GridViewContextMenuType.Rows)
            {
                e.ContextMenu.Items.Clear();
                e.ContextMenu.Items.Add("View", "View").Image.IconID = "pdfviewer_zoom_svg_dark_16x16";
                e.ContextMenu.Items.Add("Approve", "Approve").Image.IconID = "actions_apply_16x16";

            }
        }

        protected async void CreateProductsPrice_Click(object sender, EventArgs e)
        {

        

        }


        protected void VerticalGrid_HeaderFilterFillItems(object sender, ASPxVerticalGridHeaderFilterEventArgs e)
        {
            if (e.Row.FieldName == "Price")
                PreparePriceFilterItems(e);
            if (e.Row.FieldName == "HouseSize")
                PrepareHouseSizeFilterItems(e);
        }
        protected void PreparePriceFilterItems(ASPxVerticalGridHeaderFilterEventArgs e)
        {
            e.Values.Clear();
            if (e.Row.SettingsHeaderFilter.Mode == GridHeaderFilterMode.List)
                e.AddShowAll();
            int step = 500000;
            for (int i = 0; i < 4; i++)
            {
                double start = step * i;
                double end = start + step - 1;
                e.AddValue(string.Format("from {0:c0} to {1:c0}", start, end), "", string.Format("[Price] >= {0} and [Price] <= {1}", start, end));
            }
            e.AddValue(string.Format("> {0:c}", 2000000), "", "[Price] > 2000000");
        }
        protected void PrepareHouseSizeFilterItems(ASPxVerticalGridHeaderFilterEventArgs e)
        {
            e.Values.Clear();
            if (e.Row.SettingsHeaderFilter.Mode == GridHeaderFilterMode.List)
                e.AddShowAll();
            int step = 5000;
            for (int i = 0; i < 2; i++)
            {
                double start = step * i;
                double end = start + step - 1;
                e.AddValue(string.Format("from {0} to {1}", start, end), "", string.Format("[HouseSize] >= {0} and [HouseSize] <= {1}", start, end));
            }
            e.AddValue("> 10000", "", "[HouseSize] > 10000");
        }


    }
}