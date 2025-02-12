using CPMv2.DealsCode;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System;
using System.Web;
using CPMv2.Helpers;
using CPMv2.Code;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace CPMv2.Model {
    public class ApplicationUser {
        public string UserName { get; set; }
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
    }


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Datax
    {
        public int id { get; set; }
        public string phone { get; set; }
        public object password { get; set; }
        public object userType { get; set; }
    }

    public class Rootx
    {
        public object status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public Datax data { get; set; }
    }

    public class Login
    {
        public string phone { get; set; }
        public string password { get; set; }
    }

    public class UserTypes
    {
        public long id { get; set; }
        public String name { get; set; }
    }

    public class RootUserTypes
    {
        public object status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public List<UserTypes> data { get; set; }
    }


    public class Login2
    {
        public int id { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public UserType2 userType { get; set; }
    }

    public class UserType2
    {
        public int id { get; set; }
        public string name { get; set; }
    }


    public class RootLogin
    {
        public object status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public bool data { get; set; }
    }

    public class RootRegister
    {
        public object status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public Login2 data { get; set; }
    }

    public static class AuthHelper {

        public static bool register(Login2 login2)
        {

            RootLogin cp = new RootLogin();
            bool c = false;
            var client = new HttpClient();
            {
                var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/register");

                var newPost = new Login2()
                {
                    phone = login2.phone,
                    password = login2.password,
                    userType = new UserType2()
                    {
                        id = login2.userType.id
                    }


                };
                try
                {
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;
                    var x = JsonConvert.DeserializeObject<RootRegister>(result);
                    

                    if (x.status.Equals("200"))
                    {
                        c= true;
                    }
                    else
                    {
                        c = false;
                    }
                }
                catch (System.Exception e)
                {
                    // Logging.WriteLogFile(e.ToString());
                }
            }
            

            return c;
        }
        public static List<UserTypes> getUserTypes()
        {

            List<UserTypes> userTypesList = new List<UserTypes>();
            var client3 = new HttpClient();
            {
                String url = Helper.GetBaseUrl() + "v1/api/usertypes";
                var endpoint3 = new Uri(url);

                try
                {
                    var result3 = client3.GetAsync(endpoint3).Result.Content.ReadAsStringAsync().Result;
                    var cp3 = JsonConvert.DeserializeObject<RootUserTypes>(result3);

                    if (cp3.code == 200)
                    {
                        userTypesList = cp3.data;

                    }
                    else
                    {

                    }

                }
                catch (Exception eecc)
                {
                }

                return userTypesList;

            }

        }
        public static bool SignIn(string userName, string password) {
            //  HttpContext.Current.Session["User"] = CreateDefualtUser();
            // Mock user data
            //Login cp = new Login();
            RootLogin cp = new RootLogin();
            Datax login2 = new Datax();
            bool c = false;
            var client = new HttpClient();
            {
                var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/login");

                var newPost = new Login()
                {
                    phone = userName,
                    password = password
                };
                try
                {
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;
                    var x = JsonConvert.DeserializeObject<Rootx>(result);
                     login2 = x.data;

                     if (login2.id==0)
                     {
                         c=false;
                     }
                     else
                     {

                        c =true;
                        HttpContext.Current.Session["loggeIn"] = 1;
                        HttpContext.Current.Session["loggerId"] = login2.id;
                        HttpContext.Current.Session["loggerPhone"] = login2.phone;
                        HttpContext.Current.Session["userType"] = login2.userType;
                    }
                }
                catch (System.Exception e)
                {
                    // Logging.WriteLogFile(e.ToString());
                }
            }
           

            return c;
        }
        public static void SignOut() {
            HttpContext.Current.Session["User"] = null;
        }
        public static bool IsAuthenticated() {
            return GetLoggedInUserInfo() != null;
        }

        public static ApplicationUser GetLoggedInUserInfo() {
            return HttpContext.Current.Session["User"] as ApplicationUser;
        }
        private static ApplicationUser CreateDefualtUser() {
            return new ApplicationUser {
                UserName = "JBell",
                FirstName = "Julia",
                LastName = "Bell",
                Email = "julia.bell@example.com",
                AvatarUrl = "~/Content/Photo/Julia_Bell.jpg"
            };
        }
    }
}