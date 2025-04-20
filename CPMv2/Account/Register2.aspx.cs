using System;
using System.Collections.Generic;
using CPMv2.Model;
using System.IO;
using System.Net.Http;
using CPMv2.Helpers;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Text;

using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


using System.Drawing;
using System.Web.UI;
using Emgu.CV; // For Screen class
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.ComponentModel.Design;
using System.Web.Services;
using System.Web;
using StackExchange.Redis;

namespace CPMv3 {
    public partial class Register3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int x = 9;


            if (IsPostBack)
            {

            }
            else
            {
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

        static int cityNum;

        protected void drpCity_OnChanged(object sender, EventArgs e)
        {
            int x = 9;
            String b = "";
            cityNum = Convert.ToInt32(drpCity.SelectedValue);

        }

        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            // Define the pattern to match exactly "2637" followed by 8 digits
            string pattern = @"^2637\d{8}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(phoneNumber);
        }


        /* protected  void TakePicButton_Click(object sender, EventArgs e)
         {

           //  Response.Write("<script>alert('Pic Fired')</script>");
         }*/
        protected void TakePicButton_Click(object sender, ImageClickEventArgs e)
        {
          
        }

        protected async void SignInButton_Click(object sender, EventArgs e)
        {
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

            Registration login = new Registration
            {
                name = txtFullName.Text,
                phone = UserNameTextBox.Text,
                password = PasswordButtonEdit.Text,
                countries = new Countries { id = int.Parse(drpCountry.SelectedValue) },
                userType = new UserType2
                {
                    id = Convert.ToInt32(drpUserType.SelectedValue),
                    name = drpUserType.Text
                },
                cities = new City { id = cityNum == 0 ? Convert.ToInt32(drpCity.SelectedValue) : cityNum }
            };

            try
            {
                // Define uploads folder
                string uploadsFolder = Server.MapPath("~/Uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Save National ID
                var postedFile = fileUpload1.PostedFile;
                string fileName = Path.GetFileName(postedFile.FileName);
                string absolutePath = Path.Combine(uploadsFolder, fileName);
                postedFile.SaveAs(absolutePath);

                // Save Bank Statement
                var postedFile2 = fileUpload2.PostedFile;
                string fileName2 = Path.GetFileName(postedFile2.FileName);
                string absolutePath2 = Path.Combine(uploadsFolder, fileName2); // Use fileName2
                postedFile2.SaveAs(absolutePath2);

                // Save Proof of Residency
                var postedFile3 = fileUpload3.PostedFile;
                string fileName3 = Path.GetFileName(postedFile3.FileName);
                string absolutePath3 = Path.Combine(uploadsFolder, fileName3); // Use fileName3
                postedFile3.SaveAs(absolutePath3);

                // Save Photo from base64
                string fileNamePhoto = "captured_photo.jpg";
                string absolutePathPhoto = Path.Combine(uploadsFolder, fileNamePhoto);
                string base64String = Request.QueryString["photo"];
                if (!string.IsNullOrEmpty(base64String))
                {
                    try
                    {
                        byte[] imageBytes = Convert.FromBase64String(base64String);
                        System.IO.File.WriteAllBytes(absolutePathPhoto, imageBytes);
                    }
                    catch (FormatException ex)
                    {
                        Response.Write("<script>alert('Invalid photo base64 data')</script>");
                        return;
                    }
                }
                else
                {
                    Response.Write("<script>alert('No photo data received')</script>");
                    return;
                }

                // Verify National ID (if required)
                using (var memoryStream = new MemoryStream())
                {
                    postedFile.InputStream.Position = 0; // Reset stream position
                    postedFile.InputStream.CopyTo(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    string base64StringNationalId = Convert.ToBase64String(fileBytes);

                    bool isValid =  Verify(base64StringNationalId).Result;
                    if (!isValid)
                    {
                        Response.Write("<script>alert('Document Verification Failed: Invalid National ID')</script>");
                        return;
                    }
                }

                // Send to API
                var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/register");
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
                    var content = new MultipartFormDataContent
            {
                { new StreamContent(File.OpenRead(absolutePath)), "nationalIdFile", fileName },
                { new StreamContent(File.OpenRead(absolutePath2)), "bankStatementFile", fileName2 },
                { new StreamContent(File.OpenRead(absolutePath3)), "proofOfResidencyFile", fileName3 },
                { new StreamContent(File.OpenRead(absolutePathPhoto)), "photoFile", fileNamePhoto },
                { new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(login), System.Text.Encoding.UTF8, "application/json"), "users" }
            };

                    request.Content = content;
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        Response.Write("<script>alert('Registration Success, Pending Approval')</script>");
                        Response.Redirect("~/Account/SignIn.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('Registration Failed: Server Error')</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "')</script>");
                Console.WriteLine(ex.Message);
            }
        }
        /* protected async void SignInButton_Click(object sender, EventArgs e)
         {

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

             login.countries = new Countries();
             login.countries.id = int.Parse(drpCountry.SelectedValue);

             login.userType = new UserType2();
             login.userType.id = Convert.ToInt32(drpUserType.SelectedValue);
             login.userType.name = drpUserType.Text;

             login.cities = new City();
             login.cities.id = cityNum == 0 ? Convert.ToInt32(drpCity.SelectedValue) : cityNum;


             try
             {
                 //*===========================================
                 var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/register");
                 var postedFile = fileUpload1.PostedFile;


                 string base64String;
                 using (var memoryStream = new MemoryStream())
                 {
                     postedFile.InputStream.CopyTo(memoryStream);
                     byte[] fileBytes = memoryStream.ToArray();
                     base64String = Convert.ToBase64String(fileBytes);


                     bool x = true;//Verify(base64String).Result;

                   if (!x)
                   {
                       Response.Write("<script>alert('Document Verification Failed ID Wrong')</script>");
                       return;
                   }




                 }



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

                 //===================================
                 string fileNamePhoto = "";
                 string absolutePathPhoto = "";
                 string base64String2 = Request.QueryString["photo"];
                 if (!string.IsNullOrEmpty(base64String2))
                 {
                     // Decode base64 string
                     byte[] imageBytes = Convert.FromBase64String(base64String2);

                     // Save to file (optional)
                     string uploadsFolder4 = Server.MapPath("~/Uploads");
                      fileNamePhoto = "captured_photo.jpg";
                      absolutePathPhoto = Path.Combine(uploadsFolder4, fileNamePhoto);
                     System.IO.File.WriteAllBytes(absolutePathPhoto, imageBytes);

                     Response.Write("Photo received and saved successfully!");
                 }
                 else
                 {
                     Response.StatusCode = 400;
                     Response.Write("No photo data received.");
                 }


                 //====================================





                 //  var endpoint = new Uri(Helper.GetBaseUrl() + "crm/groups_generation");
                 String jsonMo = Newtonsoft.Json.JsonConvert.SerializeObject(login);

                 var client = new HttpClient();

                 var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
                 var content = new MultipartFormDataContent();
                 content.Add(new StreamContent(File.OpenRead(absolutePath)), "nationalIdFile", fileName);
                 content.Add(new StreamContent(File.OpenRead(absolutePath2)), "bankStatementFile", fileName2);
                 content.Add(new StreamContent(File.OpenRead(absolutePath3)), "proofOfResidencyFile", fileName3);
                 content.Add(new StreamContent(File.OpenRead(absolutePathPhoto)), "photoFile", fileNamePhoto);
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



         }*/

        public class DocumentUploadRequest
        {
            public string profile { get; set; }
            public string document { get; set; }
        }


        public class ApiResponse
        {
            public bool success { get; set; }
            public JObject data { get; set; }

            public string decision { get; set; }
            // Add other fields as needed
        }

        public class SingleItemPost
        {
            public String document { get; set; }
        }

        public async Task<bool> Verify(string documentBase64)
        {
            bool x= false;
            var client = new HttpClient();
            {
                var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/doc_verification");

                SingleItemPost newPost = new SingleItemPost();
                newPost.document = documentBase64;

                try
                {
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;
                    //  var x = JsonConvert.DeserializeObject<Rootx>(result);
                     x = ContainsSecondDataTag(result);
                }
                catch (System.Exception es)
                {
                    // Logging.WriteLogFile(e.ToString());
                }
            }
            return x;
        }

        public bool ContainsSecondDataTag(string jsonResponse)
        {
            try
            {
                // 1. Parse the outer JSON
                JObject outerObj = JObject.Parse(jsonResponse);

                // 2. Check if the first "data" field exists and is a string
                if (outerObj["data"] == null || outerObj["data"].Type != JTokenType.String)
                    return false;

                // 3. Parse the inner JSON (nested inside the "data" string)
                string nestedJson = outerObj["data"].ToString();
                JObject innerObj = JObject.Parse(nestedJson);

                // 4. Check if the nested JSON has a "data" field
                return innerObj["data"] != null;
            }
            catch (JsonReaderException)
            {
                // Handle JSON parsing errors
                Console.WriteLine("Invalid JSON format");
                return false;
            }
        }





        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Age
        {
            public string value { get; set; }
            public double confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
            public List<List<int>> inputBox { get; set; }
            public List<List<int>> outputBox { get; set; }
        }

        public class CountryFull
        {
            public string value { get; set; }
            public int confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
        }

        public class CountryIso2
        {
            public string value { get; set; }
            public int confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
        }

        public class CountryIso3
        {
            public string value { get; set; }
            public int confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
        }

        public class MyDataList
        {
            public List<Age> age { get; set; }
            public List<CountryFull> countryFull { get; set; }
            public List<CountryIso2> countryIso2 { get; set; }
            public List<CountryIso3> countryIso3 { get; set; }
            public List<DaysFromIssue> daysFromIssue { get; set; }
            public List<Dob> dob { get; set; }
            public List<DobDay> dobDay { get; set; }
            public List<DobMonth> dobMonth { get; set; }
            public List<DobYear> dobYear { get; set; }
            public List<DocumentName> documentName { get; set; }
            public List<DocumentNumber> documentNumber { get; set; }
            public List<DocumentSide> documentSide { get; set; }
            public List<DocumentType> documentType { get; set; }
            public List<Face> face { get; set; }
            public List<FirstName> firstName { get; set; }
            public List<FullName> fullName { get; set; }
            public List<InternalId> internalId { get; set; }
            public List<Issued> issued { get; set; }
            public List<IssuedDay> issuedDay { get; set; }
            public List<IssuedMonth> issuedMonth { get; set; }
            public List<IssuedYear> issuedYear { get; set; }
            public List<LastName> lastName { get; set; }
            public List<NationalityFull> nationalityFull { get; set; }
            public List<NationalityIso2> nationalityIso2 { get; set; }
            public List<NationalityIso3> nationalityIso3 { get; set; }
            public List<OptionalDatum> optionalData { get; set; }
            public List<PlaceOfBirth> placeOfBirth { get; set; }
        }

        public class DaysFromIssue
        {
            public string value { get; set; }
            public double confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
            public List<List<int>> inputBox { get; set; }
            public List<List<int>> outputBox { get; set; }
        }

        public class Dob
        {
            public string value { get; set; }
            public double confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
            public List<List<int>> inputBox { get; set; }
            public List<List<int>> outputBox { get; set; }
        }

        public class DobDay
        {
            public string value { get; set; }
            public double confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
            public List<List<int>> inputBox { get; set; }
        }

        public class DobMonth
        {
            public string value { get; set; }
            public double confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
            public List<List<int>> inputBox { get; set; }
        }

        public class DobYear
        {
            public string value { get; set; }
            public double confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
            public List<List<int>> inputBox { get; set; }
        }

        public class DocumentName
        {
            public string value { get; set; }
            public int confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
        }

        public class DocumentNumber
        {
            public string value { get; set; }
            public double confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
            public List<List<int>> inputBox { get; set; }
        }

        public class DocumentSide
        {
            public string value { get; set; }
            public int confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
        }

        public class DocumentType
        {
            public string value { get; set; }
            public int confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
        }

        public class Face
        {
            public string value { get; set; }
            public int confidence { get; set; }
            public int index { get; set; }
            public List<List<int>> inputBox { get; set; }
        }

        public class FirstName
        {
            public string value { get; set; }
            public double confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
            public List<List<int>> inputBox { get; set; }
        }

        public class FullName
        {
            public string value { get; set; }
            public double confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
            public List<List<int>> inputBox { get; set; }
        }

        public class InternalId
        {
            public string value { get; set; }
            public int confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
        }

        public class Issued
        {
            public string value { get; set; }
            public double confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
            public List<List<int>> inputBox { get; set; }
            public List<List<int>> outputBox { get; set; }
        }

        public class IssuedDay
        {
            public string value { get; set; }
            public double confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
            public List<List<int>> inputBox { get; set; }
        }

        public class IssuedMonth
        {
            public string value { get; set; }
            public double confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
            public List<List<int>> inputBox { get; set; }
        }

        public class IssuedYear
        {
            public string value { get; set; }
            public double confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
            public List<List<int>> inputBox { get; set; }
        }

        public class LastName
        {
            public string value { get; set; }
            public double confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
            public List<List<int>> inputBox { get; set; }
        }

        public class NationalityFull
        {
            public string value { get; set; }
            public int confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
        }

        public class NationalityIso2
        {
            public string value { get; set; }
            public int confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
        }

        public class NationalityIso3
        {
            public string value { get; set; }
            public int confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
        }

        public class OptionalDatum
        {
            public string value { get; set; }
            public double confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
            public List<List<int>> inputBox { get; set; }
        }

        public class PlaceOfBirth
        {
            public string value { get; set; }
            public double confidence { get; set; }
            public string source { get; set; }
            public int index { get; set; }
            public List<List<int>> inputBox { get; set; }
        }

        public class SuccessRoot
        {
            public bool success { get; set; }
            public MyDataList data { get; set; }
            public int reviewScore { get; set; }
            public int rejectScore { get; set; }
            public string decision { get; set; }
            public int quota { get; set; }
            public int credit { get; set; }
            public double executionTime { get; set; }
        }

        public class ErrorRoot
        {
            public bool success { get; set; }
            public int reviewScore { get; set; }
            public int rejectScore { get; set; }
            public String decision { get; set; }
            public int quota { get; set; }
            public int credit { get; set; }
            public double executionTime { get; set; }
        }


    }
}