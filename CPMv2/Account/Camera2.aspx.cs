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
    public partial class Camera2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int x = 9;


            if (IsPostBack)
            {

            }
            else
            {
             
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

          

        }

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