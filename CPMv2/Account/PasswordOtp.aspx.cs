using System;
using DevExpress.Web;
using CPMv2.Model;
using DevExpress.Web.Internal.Dialogs;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Web;
using CPMv2.Helpers;

namespace CPMv2 {
    public partial class PasswordOtp : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
        }

        protected void SignInButton_Click(object sender, EventArgs e) 
        {
            /* FormLayout.FindItemOrGroupByName("GeneralError").Visible = false;
             if(ASPxEdit.ValidateEditorsInContainer(this)) {
                 // DXCOMMENT: You Authentication logic
                 if(!AuthHelper.SignIn(UserNameTextBox.Text, PasswordButtonEdit.Text)) {
                     GeneralErrorDiv.InnerText = "Invalid login attempt.";
                     FormLayout.FindItemOrGroupByName("GeneralError").Visible = true;
                 }
                 else
                     Response.Redirect("~/");
             }*/

            //if (!AuthHelper.SignIn(UserNameTextBox.Text, PasswordButtonEdit.Text))
           // if (!AuthHelper.SignIn(UserNameTextBox.Text, PasswordButtonEdit.Text))
           // {
                RootLogin cp = new RootLogin();
                Datax login2 = new Datax();
                bool c = false;
                var client = new HttpClient();
                {
                    var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/login");

                    var newPost = new Login()
                    {
                       // phone = UserNameTextBox.Text,
                     
                    };
                    try
                    {
                        var newPostJson = JsonConvert.SerializeObject(newPost);
                        var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                        var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;
                        var x = JsonConvert.DeserializeObject<Rootx>(result);
                        login2 = x.data;

                        if (login2.id == 0)
                        {
                            c = false;
                            String message = x.message;
                            Response.Write("<script>alert('"+message+"')</script>");
                    }
                        else
                        {

                            c = true;
                            HttpContext.Current.Session["loggeIn"] = 1;
                            HttpContext.Current.Session["loggerId"] = login2.id;
                            HttpContext.Current.Session["loggerPhone"] = login2.phone;
                            HttpContext.Current.Session["userType"] = login2.userType;

                           

                            Object xx = HttpContext.Current.Session["userType"];
                            var cx = JsonConvert.DeserializeObject<UserTypes>(xx.ToString());
                            if (cx.name.Equals("agent"))
                            {
                                Response.Redirect("~/Products.aspx");
                        }

                            if (cx.name.Equals("admin"))
                            {
                            Response.Redirect("~/Index.aspx");
                        }
                    }
                    }
                    catch (System.Exception es)
                    {
                        // Logging.WriteLogFile(e.ToString());
                    }
                }
           
   
        }
    }
}