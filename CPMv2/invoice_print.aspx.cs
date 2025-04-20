using CPMv2.Helpers;
using DevExpress.XtraRichEdit.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CPMv2
{
    public partial class invoice_print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            Uri myUri = new Uri(url);
            string param1 = HttpUtility.ParseQueryString(myUri.Query).Get("zx");


            using (var client = new HttpClient())
            {
                var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/print_invoice");
                var newPost = new Register2.SingleItemPost()
                {
                   document = param1
                };

                var newPostJson = JsonConvert.SerializeObject(newPost);
                var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                var response = client.PostAsync(endpoint, payload).Result;
                response.EnsureSuccessStatusCode();
                var resultJson = response.Content.ReadAsByteArrayAsync().Result;

                Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=" + "portfolio.pdf");
                Response.BinaryWrite(resultJson);


            }

        }
    }
}