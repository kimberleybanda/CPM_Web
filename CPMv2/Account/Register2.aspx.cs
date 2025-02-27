using System;
using System.Collections.Generic;
using DevExpress.Web;
using CPMv2.Model;
using DevExpress.Web.Internal.Dialogs;
using CPMv2.Code;
using System.IO;
using System.Net.Http;
using System.Web.UI.WebControls;
using CPMv2.Helpers;
using static DevExpress.Utils.MVVM.Internal.ILReader;
using System.Text.RegularExpressions;

namespace CPMv2 {
    public partial class Register2 : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e)
        {
            int x = 9;
            List<UserTypes> userTypes = AuthHelper.getUserTypes();
            drpUserType.DataSource = userTypes;
            drpUserType.DataTextField = "name";
            drpUserType.DataValueField = "id";
            drpUserType.DataBind();

            List<Countries> countries = AuthHelper.getCountry();
            drpCountry.DataSource = countries;
            drpCountry.DataTextField = "name";
            drpCountry.DataValueField = "id";
            drpCountry.DataBind();

            List<City> cities = AuthHelper.getCity();
            drpCity.DataSource = cities;
            drpCity.DataTextField = "name";
            drpCity.DataValueField = "id";
            drpCity.DataBind();

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
        }

        protected void drpUserType_OnChanged(object sender, EventArgs e)
        {

        }

        protected void drpCountry_OnChanged(object sender, EventArgs e)
        {

        }

        protected void drpCity_OnChanged(object sender, EventArgs e)
        {

        }
        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            // Define the pattern to match exactly "2637" followed by 8 digits
            string pattern = @"^2637\d{8}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(phoneNumber);
        }
        protected async void SignInButton_Click(object sender, EventArgs e) {

            if (!ValidatePhoneNumber(UserNameTextBox.Text))
            {
                Response.Write("<script>alert('Correct Phone Format: 263783065525')</script>");

                return;
            }
            if (!fileUpload1.HasFile)
            {
                Response.Write("<script>alert('Upload National ID')</script>");

                return;
            }
            if (!fileUpload2.HasFile)
            {
                Response.Write("<script>alert('Upload Bank Statement')</script>");
                return;
            }
            if (!fileUpload3.HasFile)
            {
                Response.Write("<script>alert('Upload Proof Of Residency')</script>");

                return;
            }

            Registration login = new Registration();
            login.name = txtFullName.Text;
            login.phone = UserNameTextBox.Text;
            login.password = PasswordButtonEdit.Text;
            login.userType = new UserType2();
            login.userType.id = Convert.ToInt32(drpUserType.SelectedValue);
            login.userType.name = drpUserType.Text;


            try
            {
                //*===========================================
                var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/register");
                var postedFile = fileUpload1.PostedFile;
                string uploadsFolder = Server.MapPath("~/Uploads");
                string fileName = Path.GetFileName(postedFile.FileName);
                string absolutePath = Path.Combine(uploadsFolder, fileName);

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                postedFile.SaveAs(absolutePath);

                var postedFile2 = fileUpload2.PostedFile;
                string uploadsFolder2 = Server.MapPath("~/Uploads");
                string fileName2 = Path.GetFileName(postedFile2.FileName);
                string absolutePath2 = Path.Combine(uploadsFolder2, fileName);
                if (!Directory.Exists(uploadsFolder2))
                {
                    Directory.CreateDirectory(uploadsFolder2);
                }
                postedFile2.SaveAs(absolutePath2);


                var postedFile3 = fileUpload3.PostedFile;
                string uploadsFolder3 = Server.MapPath("~/Uploads");
                string fileName3 = Path.GetFileName(postedFile3.FileName);
                string absolutePath3 = Path.Combine(uploadsFolder3, fileName);

                if (!Directory.Exists(uploadsFolder3))
                {
                    Directory.CreateDirectory(uploadsFolder3);
                }
                postedFile3.SaveAs(absolutePath3);




                //  var endpoint = new Uri(Helper.GetBaseUrl() + "crm/groups_generation");
                String jsonMo = Newtonsoft.Json.JsonConvert.SerializeObject(login);

                var client = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(File.OpenRead(absolutePath)), "nationalIdFile", fileName);
                content.Add(new StreamContent(File.OpenRead(absolutePath2)), "bankStatementFile", fileName2);
                content.Add(new StreamContent(File.OpenRead(absolutePath3)), "proofOfResidencyFile", fileName3);
                content.Add(new StringContent(jsonMo, System.Text.Encoding.UTF8, "application/json"), "users");

                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode)
                {
                    Response.Write("<script>alert('Registration Success, Pending Approval')</script>");
                }
                Response.Redirect("~/Account/SignIn.aspx");
                //===========================================

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            /* bool rootLogin = AuthHelper.register(login);
             if (rootLogin)
             {
                 Response.Redirect("~/Account/SignIn.aspx");
             }
             else
             {

             }*/

        }
    }
}