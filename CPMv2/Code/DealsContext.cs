
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using CPMv2.Helpers;
using System.Text;
using DevExpress.Utils.About;

namespace CPMv2.DealsCode
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    

public class Users
{
    public int id { get; set; }
    public string password { get; set; }
    public string phone { get; set; }
    public UserType userType { get; set; }
}

public class UserType
{
    public int id { get; set; }
    public string name { get; set; }
}
    public class Products
    {
        public string about { get; set; }
        public int id { get; set; }
        public string imageUrl { get; set; }
        public string price { get; set; }
        public Product product { get; set; }
    }

    public class Deals
    {
        public double amount { get; set; }
        public int id { get; set; }
        public Products products { get; set; }
        public int qty { get; set; }
        public bool status { get; set; }
        public bool approve { get; set; }
        public Users users { get; set; }
    }

    public class DealsCustom
    {
        public double amount { get; set; }
        public int id { get; set; }
        public String productName { get; set; }
        public int qty { get; set; }
        public bool status { get; set; }
        public int userId { get; set; }

        public DealsCustom(double amount, int id, string productName, int qty, bool status, int userId)
        {
            this.amount = amount;
            this.id = id;
            this.productName = productName;
            this.qty = qty;
            this.status = status;
            this.userId = userId;
        }
    }

    public class Root
    {
        public object status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public Deals data { get; set; }
    }

    public class RootApprovedDeals
    {
        public object status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public int data { get; set; }
    }

    public class RootList
    {
        public object status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public List<Deals> data { get; set; }
    }


    public static class DealsContextProvider
    {

        public static List<DealsCustom> GetDeals()
        {
            List<DealsCustom> customDealsList= new List<DealsCustom>();
            List<Deals> dealsList = new List<Deals>();
            var client3 = new HttpClient();
            {
                String url = Helper.GetBaseUrl() + "v1/api/deals";
                var endpoint3 = new Uri(url);

                try
                {
                    var result3 = client3.GetAsync(endpoint3).Result.Content.ReadAsStringAsync().Result;
                    var cp3 = JsonConvert.DeserializeObject<RootList>(result3);

                    if (cp3.code == 200)
                    {
                        dealsList = cp3.data;

                        foreach (var item in dealsList)
                        {

                            customDealsList.Add(
                                    new DealsCustom(
                                        item.amount,
                                        item.id,
                                        item.products.product.name,
                                        item.qty,
                                        item.status,
                                        item.users.id

                                    )
                                
                                ); 
                        }

                    }
                    else
                    {

                    }

                }
                catch (Exception eecc)
                {
                }

                return customDealsList;

            }
        }


        public static int  GetApprovedDeals(int user_id)
        {
         int approvedDeals= 0;
            var client3 = new HttpClient();
            {
                String url = Helper.GetBaseUrl() + "v1/api/deals_approved?user_id="+ user_id;
                var endpoint3 = new Uri(url);

                try
                {
                    var result3 = client3.GetAsync(endpoint3).Result.Content.ReadAsStringAsync().Result;
                    var cp3 = JsonConvert.DeserializeObject<RootApprovedDeals>(result3);

                    if (cp3.code == 200)
                    {
                        approvedDeals = cp3.data;
                    }
                    else
                    {

                    }

                }
                catch (Exception eecc)
                {
                }

                return approvedDeals;

            }
        }

        public static int GetNonApprovedDeals(int user_id)
        {
            int approvedDeals = 0;
            var client3 = new HttpClient();
            {
                String url = Helper.GetBaseUrl() + "v1/api/deals_nonapproved?user_id=" + user_id;
                var endpoint3 = new Uri(url);

                try
                {
                    var result3 = client3.GetAsync(endpoint3).Result.Content.ReadAsStringAsync().Result;
                    var cp3 = JsonConvert.DeserializeObject<RootApprovedDeals>(result3);

                    if (cp3.code == 200)
                    {
                        approvedDeals = cp3.data;
                    }
                    else
                    {

                    }

                }
                catch (Exception eecc)
                {
                }

                return approvedDeals;

            }
        }

        public static int GetAdminApprovedDeals()
        {
            int approvedDeals = 0;
            var client3 = new HttpClient();
            {
                String url = Helper.GetBaseUrl() + "v1/api/deals_adminapproved";
                var endpoint3 = new Uri(url);

                try
                {
                    var result3 = client3.GetAsync(endpoint3).Result.Content.ReadAsStringAsync().Result;
                    var cp3 = JsonConvert.DeserializeObject<RootApprovedDeals>(result3);

                    if (cp3.code == 200)
                    {
                        approvedDeals = cp3.data;
                    }
                    else
                    {

                    }

                }
                catch (Exception eecc)
                {
                }

                return approvedDeals;

            }
        }

        public static int GetAdminNonApprovedDeals()
        {
            int approvedDeals = 0;
            var client3 = new HttpClient();
            {
                String url = Helper.GetBaseUrl() + "v1/api/deals_adminnonapproved";
                var endpoint3 = new Uri(url);

                try
                {
                    var result3 = client3.GetAsync(endpoint3).Result.Content.ReadAsStringAsync().Result;
                    var cp3 = JsonConvert.DeserializeObject<RootApprovedDeals>(result3);

                    if (cp3.code == 200)
                    {
                        approvedDeals = cp3.data;
                    }
                    else
                    {

                    }

                }
                catch (Exception eecc)
                {
                }

                return approvedDeals;

            }
        }


        public static void createDeals(Deals deals)
        {

            Deals cp = new Deals();
            var client = new HttpClient();
            {
                var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/deals");

                var newPost = new Deals();

                newPost.id = deals.id;
                newPost.amount = deals.amount == null ? 0 : deals.amount;
                newPost.qty = deals.qty == null ? 0 : deals.qty;
                newPost.status = deals.status == null ? false : deals.status;
                newPost.approve = deals.approve == null ? false : deals.approve;
                   var products = new Products();
                if (deals.products != null)
                {
                    products.id =  deals.products.id;
                }
                else
                {
                    products.id = 0;
                }
                
                   var users = new Users();
                if (deals.users != null)
                {
                    users.id = deals.users.id;
                }
                else
                {
                    users.id = 0;
                }

                try
                {
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;
                    var x = JsonConvert.DeserializeObject<Root>(result);
                    // cp = x.data;
                }
                catch (System.Exception e)
                {
                    // Logging.WriteLogFile(e.ToString());
                }
            }


            // return cp;
        }



    }
}