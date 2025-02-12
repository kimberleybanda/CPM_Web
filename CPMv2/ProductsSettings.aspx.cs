using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Xml.Linq;
using CPMv2.Code;
using CPMv2.Helpers;
using CPMv2.Model;
using DevExpress.Web;

namespace CPMv2 {
    public partial class ProductsSettings : System.Web.UI.Page {
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
                // GridView1.DataSource = accSearchList;
                // GridView1.DataBind();
            }
            else
            {

                List<ProductsCustom> productList2 = ProductsContextProvider.GetProducts();
                GridView1.DataSource = productList2;
                GridView1.DataBind();

                List<ProductModel>productList = ProductsContextProvider.GetProduct();
            
            drpProduct.DataSource = productList;
            drpProduct.DataTextField = "name";
            drpProduct.DataValueField = "id";
            drpProduct.DataBind();
            }


        }
        public static String productId;
        protected void drpClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            String input = drpProduct.SelectedItem.Text;
            productId = drpProduct.SelectedItem.Value;

            txtStoreCliID.Text = productId;

            string[] parts = input.Split(new string[] { "--" }, StringSplitOptions.None);

         //   txtClientFullName2.Text = parts[1];
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
                    btnCreateProductPrice.Text= "Update";
                   ProductsCustom productsCustomsList = ProductsContextProvider.loadProducts(Id);
                   txtID.Text= productsCustomsList.id.ToString();
                   txtPrice.Text= productsCustomsList.price.ToString();
                }

                if (e.Item.Name == "Delete")
                {
                   
                    ProductsCustom productsCustomsList = ProductsContextProvider.deleteProducts(Id);
                    List<ProductsCustom> productList2 = ProductsContextProvider.GetProducts();
                    GridView1.DataSource = productList2;
                    GridView1.DataBind();

            }
        }

        protected void Grid_ContextMenuInitialize(object sender, ASPxGridViewContextMenuInitializeEventArgs e)
        {

            // var rejectq = "";
            if (e.MenuType == GridViewContextMenuType.Rows)
            {
                e.ContextMenu.Items.Clear();
                e.ContextMenu.Items.Add("Edit", "Edit").Image.IconID = "pdfviewer_zoom_svg_dark_16x16";
                e.ContextMenu.Items.Add("Delete", "Delete").Image.IconID = "pdfviewer_zoom_svg_dark_16x16";

            }
        }

        protected async void CreateProductsPrice_Click(object sender, EventArgs e)
        {

            ProductsCustom productModel = new ProductsCustom();
            productModel.id = txtID.Text==""?0:Convert.ToInt32(txtID.Text);
            productModel.price = txtPrice.Text;
            productModel.about = "";


            //*===========================================
            ProductsModel cp = new ProductsModel();

            var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/products");

            var newPost = new ProductsModel()
            {
                id = productModel.id,
                imageUrl = productModel.imageUrl,
                price = productModel.price,
                about = productModel.about,
                product = new Product2()
                {
                    id = Convert.ToInt32(productId)
                }

                

            };


            var postedFile = fileUpload.PostedFile;

            string uploadsFolder = Server.MapPath("~/Uploads");
            string fileName = Path.GetFileName(postedFile.FileName);
            string absolutePath = Path.Combine(uploadsFolder, fileName);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            postedFile.SaveAs(absolutePath);

            //  var endpoint = new Uri(Helper.GetBaseUrl() + "crm/groups_generation");
            String jsonMo = Newtonsoft.Json.JsonConvert.SerializeObject(newPost);

            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(File.OpenRead(absolutePath)), "file", fileName);
            content.Add(new StringContent(jsonMo, System.Text.Encoding.UTF8, "application/json"), "products");

            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            if (response.IsSuccessStatusCode)
            {
                Response.Write("<script>alert('Products Settings Created/Updated Successfully')</script>");
            }


            //===========================================

            List<ProductsCustom> productList = ProductsContextProvider.GetProducts();
            GridView1.DataSource = productList;
            GridView1.DataBind();

            txtID.Text = "";
            txtPrice.Text = "";
            btnCreateProductPrice.Text = "Create";

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