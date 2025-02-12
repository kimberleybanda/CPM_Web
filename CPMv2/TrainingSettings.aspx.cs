using System;
using System.Collections.Generic;
using CPMv2.Code;
using System.IO;
using System.Net.Http;
using CPMv2.Helpers;
using CPMv2.Model;
using DevExpress.Web;
using System.Linq;
using System.Xml.Linq;
using System.Web;

namespace CPMv2 {
    public partial class TrainingSettings : System.Web.UI.Page {
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

            }
            else
            {
                List<VideoTutorials> productList = TutorialsContextProvider.GetTutorialVid();
                GridView1.DataSource = productList;
                GridView1.DataBind();

                List<PdfTutorials> productList2 = TutorialsContextProvider.GetTutorialPdf();
                GridView2.DataSource = productList2;
                GridView2.DataBind();

            }


        }


        protected void GridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
            /*if(e.Parameters == "delete") {
                List<long> selectedIds = GridView.GetSelectedFieldValues("Id").ConvertAll(id => (long)id);
                DataProvider.DeleteIssues(selectedIds);
                GridView.DataBind();
            }*/
        }

      
        protected async void CreateTutorialVid_Click(object sender, EventArgs e)
        {
            try
            {
                VideoTutorials newPost = new VideoTutorials();
                newPost.id = txtID.Text == "" ? 0 : Convert.ToInt32(txtID.Text);
                newPost.tutorialName = txtTutorialName.Text;



                //*===========================================
                ProductsModel cp = new ProductsModel();

                var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/video");

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
                content.Add(new StringContent(jsonMo, System.Text.Encoding.UTF8, "application/json"), "videoTutorials");

                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    Response.Write("<script>alert('Training Settings Created/Updated Successfully')</script>");
                }


                //===========================================
                List<VideoTutorials> productList = TutorialsContextProvider.GetTutorialVid();
                GridView1.DataSource = productList;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            

        }

        protected async void CreateTutorialPdf_Click(object sender, EventArgs e)
        {
            ///  try
            // {
            PdfTutorials newPost = new PdfTutorials();
            newPost.id = txtID.Text == "" ? 0 : Convert.ToInt32(txtID.Text);
            newPost.tutorialName = txtTutorialName.Text;



            //*===========================================

            var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/pdf");

            var postedFile = fileUpload1.PostedFile;

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
            content.Add(new StringContent(jsonMo, System.Text.Encoding.UTF8, "application/json"), "pdfTutorials");

            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            if (response.IsSuccessStatusCode)
            {
                Response.Write("<script>alert('Group Message Created Successfully')</script>");
            }


            //===========================================
            List<VideoTutorials> productList = TutorialsContextProvider.GetTutorialVid();
            GridView1.DataSource = productList;
            GridView1.DataBind();
            // }
            /* catch (Exception ex)
             {
                 Console.WriteLine(ex.Message);
             }*/



        }

        protected void Grid_ContextMenuItemClick(object sender, ASPxGridViewContextMenuItemClickEventArgs e)
        {
            var trxnID = GridView1.GetSelectedFieldValues("id");
            object Id = trxnID.First();


            if (e.Item.Name == "Edit")
            {

                VideoTutorials videoTutorials = TutorialsContextProvider.loadVideo((long)Id);
                txtID.Text = videoTutorials.id.ToString();
                txtTutorialName.Text = videoTutorials.tutorialName;

                btnCreateTutorialVid.Text = "Update";
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

        protected void Grid_ContextMenuItemClick2(object sender, ASPxGridViewContextMenuItemClickEventArgs e)
        {
            var trxnID = GridView2.GetSelectedFieldValues("id");
            object Id = trxnID.First();


            if (e.Item.Name == "Edit")
            {

                PdfTutorials videoTutorials = TutorialsContextProvider.loadPdf((long)Id);
                txtID.Text = videoTutorials.id.ToString();
                txtTutorialName.Text = videoTutorials.tutorialName;

                btnCreateTutorialPdf.Text = "Update";
            }

        }
        protected void Grid_ContextMenuInitialize2(object sender, ASPxGridViewContextMenuInitializeEventArgs e)
        {

            // var rejectq = "";
            if (e.MenuType == GridViewContextMenuType.Rows)
            {
                e.ContextMenu.Items.Clear();
                e.ContextMenu.Items.Add("Edit", "Edit").Image.IconID = "pdfviewer_zoom_svg_dark_16x16";

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