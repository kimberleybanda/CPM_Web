using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CPMv2.Code;
using CPMv2.Model;
using DevExpress.Web;

namespace CPMv2 {
    public partial class ProductSettings : System.Web.UI.Page {
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

            if (IsPostBack)
            {
                List<ProductModel> productList = ProductsContextProvider.GetProduct();
                GridView1.DataSource = productList;
                GridView1.DataBind();
            }
            else
            {
                List<ProductModel>productList= ProductsContextProvider.GetProduct();
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

        protected void Grid_ContextMenuItemClick(object sender, ASPxGridViewContextMenuItemClickEventArgs e)
        {
            var trxnID = GridView1.GetSelectedFieldValues("id");
            int Id = (int)trxnID.First();

           
                if (e.Item.Name == "Edit")
                {

                   ProductModel productModel = ProductsContextProvider.loadProduct(Id);
                   txtID.Text= productModel.id.ToString();
                   txtName.Text = productModel.name;

                btnCreateProduct.Text= "Update";
                }
                
        }



        protected void Grid_ContextMenuInitialize(object sender, ASPxGridViewContextMenuInitializeEventArgs e)
        {

            // var rejectq = "";
            if (e.MenuType == GridViewContextMenuType.Rows)
            {
                e.ContextMenu.Items.Clear();
                e.ContextMenu.Items.Add("Edit", "Edit").Image.IconID = "pdfviewer_zoom_svg_dark_16x16";

            }
        }

        protected void CreateProduct_Click(object sender, EventArgs e)
        {
           
                ProductModel productModel = new ProductModel();
                productModel.id = txtID.Text==""?0:Convert.ToInt32(txtID.Text);
                productModel.name = txtName.Text;
                ProductsContextProvider.createProduct(productModel);

                List<ProductModel> productList = ProductsContextProvider.GetProduct();

                txtID.Text = "";
                txtName.Text = "";
                GridView1.DataSource = productList;
                GridView1.DataBind();
                btnCreateProduct.Text = "Create";
                
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