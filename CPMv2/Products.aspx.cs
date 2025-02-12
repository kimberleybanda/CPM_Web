using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using CPMv2.Code;
using CPMv2.DealsCode;
using CPMv2.Model;
using DevExpress.Web;

namespace CPMv2 {
    public partial class Products : System.Web.UI.Page {
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

            int x = 5;
            List<ProductsCustom> d = ProductsContextProvider.GetProducts();

            if (IsPostBack)
            {
               /* drpProduct.DataSource = d;
                drpProduct.DataTextField = "product";
                drpProduct.DataValueField = "id";
                drpProduct.DataBind(); */
            }
            else
            {
               
                drpProduct.DataSource = d;
                drpProduct.DataTextField = "product";
                drpProduct.DataValueField = "id";
                drpProduct.DataBind();
            }
        }

        protected void GridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
            /*if(e.Parameters == "delete") {
                List<long> selectedIds = GridView.GetSelectedFieldValues("Id").ConvertAll(id => (long)id);
                DataProvider.DeleteIssues(selectedIds);
                GridView.DataBind();
            }*/
        }

        public static double totalAmount;
        public static double price;

        protected void btnCreateDeal_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["loggerId"] == null)
                Response.Redirect("~/Account/SignIn.aspx");
            if ((int)HttpContext.Current.Session["loggerId"] == 0)
            {
                Response.Redirect("~/Account/SignIn.aspx");
            }

            Deals deal = new Deals();
            deal.id = 0;
            deal.amount = Convert.ToDouble(txtTotalAmount.Text);
            deal.status = false;

            deal.products = new CPMv2.DealsCode.Products();
            deal.products.id = Convert.ToInt32(drpProduct.SelectedValue);
            deal.qty = Convert.ToInt32(txtQty.Text);

            deal.users = new Users();
            deal.users.id = (int)HttpContext.Current.Session["loggerId"];


            DealsContextProvider.createDeals(deal);


        }

        protected void qty_changed(object sender, EventArgs e)
        {
    
            double xx = price * Convert.ToDouble(txtQty.Text);
            txtTotalAmount.Text =  xx.ToString();
;
        }
        protected void drpClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int x = 5;
                Object x3 = drpProduct.SelectedValue;
               // double y = Convert.ToDouble(drpProduct.SelectedItem.Text);
               ProductsModel cc= ProductsContextProvider.loadProducts2(long.Parse(drpProduct.SelectedValue));
                price = double.Parse(cc.price);
            }
            catch (Exception ex)
            {

            } // (Exception ex


        }
        protected void VerticalGrid_SelectionChanged(object sender, EventArgs e)
        {
            // Get the selected row's key value
            var selectedRowKey = VerticalGrid.GetSelectedFieldValues("ID"); // Replace "ID" with your key field name

            if (selectedRowKey != null && selectedRowKey.Count > 0)
            {
                // Assuming the key field is an integer
                int selectedId = (int)selectedRowKey[0];

                // Perform your action with the selected ID
                // For example, display a message or redirect to another page
                DevExpress.Web.ASPxWebControl.RedirectOnCallback("YourActionPage.aspx?id=" + selectedId);
            }
            else
            {
                // No row is selected
                // Handle this case if needed
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