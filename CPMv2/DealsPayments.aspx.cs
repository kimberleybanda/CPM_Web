using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using CPMv2.Code;
using CPMv2.DealsCode;
using CPMv2.Helpers;
using CPMv2.Model;
using DevExpress.Web;
using Newtonsoft.Json;

namespace CPMv2 {

    public class PaynowModel
    {

        public String phone { get; set; }
        public double amount { get; set; }
    }
    public partial class DealsPayments : System.Web.UI.Page {
        protected void GridView_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e) {
            e.NewValues["Kind"] = 1;
            e.NewValues["Priority"] = 2;
            e.NewValues["Status"] = 1;
            e.NewValues["IsDraft"] = true;
            e.NewValues["IsArchived"] = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (HttpContext.Current.Session["loggeIn"] == null)
                Response.Redirect("~/Account/SignIn.aspx");
            if ((int)HttpContext.Current.Session["loggeIn"] == 0)
            {
                Response.Redirect("~/Account/SignIn.aspx");
            }
            int c = 6;
           int loggerId = (int)HttpContext.Current.Session["loggerId"];
            if (IsPostBack)
            {
                
                List<DealsCustom> productList = DealsContextProvider.GetDealsUsers(true, loggerId);
                GridView1.DataSource = productList;
                GridView1.DataBind();
            }
            else
            {
                List<DealsCustom> productList = DealsContextProvider.GetDealsUsers(true, loggerId);
                GridView1.DataSource = productList;
                GridView1.DataBind();

            }


        }



        protected void GridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
            /*if(e.Parameters == "delete") {
                List<long> selectedIds = GridView.GetSelectedFieldValues("Id").ConvertAll(id => (long)id);
                DataProvider.DeleteIssues(selectedIds);
                GridView.DataBind();
            }*/
        }

        

        protected void PayButton_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();
            {
                var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/paynow");

                var newPost = new PaynowModel();
                // {
               Object xcx= txtEcoPhone.Text;
               Object amount = amount3;
                newPost.phone = txtEcoPhone.Text.ToString();
                newPost.amount = double.Parse(amount3);
                //};
                try
                {
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;
                    var x = JsonConvert.DeserializeObject<Rootx>(result);
                   
                }
                catch (System.Exception es)
                {
                    // Logging.WriteLogFile(e.ToString());
                }
            }
            pcSearch.ShowOnPageLoad = false;
        }

        public static String amount3;
        protected void Grid_ContextMenuItemClick(object sender, ASPxGridViewContextMenuItemClickEventArgs e)
        {
            var trxnID = GridView1.GetSelectedFieldValues("id");
            int Id = (int)trxnID.First();

            var amount = GridView1.GetSelectedFieldValues("amount");
            Object amount2 = amount.First();
            amount3= amount2.ToString();

            if (e.Item.Name == "Pay")
                {
                pcSearch.ShowOnPageLoad = true;
                txtAmount.Text= amount2.ToString();
                 }

                if (e.Item.Name == "Reject")
                {

              
                }

        }



        protected void Grid_ContextMenuInitialize(object sender, ASPxGridViewContextMenuInitializeEventArgs e)
        {

            // var rejectq = "";
            if (e.MenuType == GridViewContextMenuType.Rows)
            {
                e.ContextMenu.Items.Clear();
                e.ContextMenu.Items.Add("Pay", "Pay").Image.IconID = "actions_apply_16x16";

               // e.ContextMenu.Items.Add("Reject", "Reject").Image.IconID = "actions_cancel_16x16office2013";

            }
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