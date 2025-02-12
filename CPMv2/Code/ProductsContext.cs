
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using CPMv2.Helpers;

namespace CPMv2.Code
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ProductsModel
    {
        public int id { get; set; }
        public string imageUrl { get; set; }
        public string price { get; set; }
        public string about { get; set; }
        public Product2 product { get; set; }
    }

    public class ProductsCustom
    {
        public int id { get; set; }
        public string imageUrl { get; set; }
        public string price { get; set; }
        public string about { get; set; }
        public String product { get; set; }

        public ProductsCustom()
        {
            
        }
        public ProductsCustom(int id, string imageUrl, string price, string about, string product)
        {
            this.id = id;
            this.imageUrl = imageUrl;
            this.price = price;
            this.about = about;
            this.product = product;
        }
    }

    public class Product2
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Root
    {
        public object status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public List<ProductsModel> data { get; set; }
    }

    public class Root2
    {
        public object status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public ProductsModel data { get; set; }
    }

    public class RootProduct
    {
        public object status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public List<ProductModel> data { get; set; }
    }

    public class RootProduct2
    {
        public object status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public ProductModel data { get; set; }
    }



    public class ProductModel
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class PostId
    {
        public long id { get; set; }
    }

    public class RootProductSingle
    {
        public object status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public ProductsCustom data { get; set; }
    }



    public static class ProductsContextProvider
    {
        public static List<ProductsCustom> GetProducts()
        {
            int x = 44;

            List<ProductsModel> productsModels = new List<ProductsModel>();
            List<ProductsCustom> productsCustomList = new List<ProductsCustom>();
            var client3 = new HttpClient();
            {
                String url = Helper.GetBaseUrl() + "v1/api/products";
                var endpoint3 = new Uri(url);

                try
                {
                    var result3 = client3.GetAsync(endpoint3).Result.Content.ReadAsStringAsync().Result;
                    var cp3 = JsonConvert.DeserializeObject<Root>(result3);

                    if (cp3.code == 200)
                    {
                        productsModels = cp3.data;

                        foreach (var cx in productsModels)
                        {
                            if (cx.product == null)
                            {
                                cx.product = new Product2();
                                cx.product.name = "";
                            }
                            productsCustomList.Add(new ProductsCustom(
                                cx.id,cx.imageUrl,cx.price,cx.about,cx.product.name));
                        }

                       
                    }
                    else
                    {

                    }

                }
                catch (Exception eecc)
                {
                }

                return productsCustomList;

            }

        }
        public static List<ProductModel> GetProduct()
        {

            List<ProductModel> productModelList = new List<ProductModel>();
            var client3 = new HttpClient();
            {
                String url = Helper.GetBaseUrl() + "v1/api/product";
                var endpoint3 = new Uri(url);

                try
                {
                    var result3 = client3.GetAsync(endpoint3).Result.Content.ReadAsStringAsync().Result;
                    var cp3 = JsonConvert.DeserializeObject<RootProduct>(result3);

                    if (cp3.code == 200)
                    {
                        productModelList = cp3.data;

                    }
                    else
                    {

                    }

                }
                catch (Exception eecc)
                {
                }

                return productModelList;

            }

        }

        public static ProductsModel loadProducts2(long id)
        {

            List<ProductModel> productModelList = new List<ProductModel>();
            ProductsModel cp = new ProductsModel();
            var client = new HttpClient();
            {
                var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/products_id");

                var newPost = new PostId()
                {
                    id = id

                };
                try
                {
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;
                    var x = JsonConvert.DeserializeObject<Root2>(result);
                    cp = x.data;

                }
                catch (System.Exception e)
                {
                    // Logging.WriteLogFile(e.ToString());
                }
            }


            return cp;

        }

        public static ProductsCustom loadProducts(long id)
        {

            List<ProductModel> productModelList = new List<ProductModel>();
            ProductsCustom cp = new ProductsCustom();
            var client = new HttpClient();
            {
                var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/products_id");

                var newPost = new PostId()
                {
                    id = id

                };
                try
                {
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;
                    var x = JsonConvert.DeserializeObject<RootProductSingle>(result);
                    cp = x.data;

                }
                catch (System.Exception e)
                {
                    // Logging.WriteLogFile(e.ToString());
                }
            }


            return cp;

        }

        public static ProductsCustom deleteProducts(long id)
        {

            List<ProductModel> productModelList = new List<ProductModel>();
            ProductsCustom cp = new ProductsCustom();
            var client = new HttpClient();
            {
                var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/products_delete");

                var newPost = new PostId()
                {
                    id = id

                };
                try
                {
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;
                    var x = JsonConvert.DeserializeObject<RootProductSingle>(result);
                    cp = x.data;

                }
                catch (System.Exception e)
                {
                    // Logging.WriteLogFile(e.ToString());
                }
            }


            return cp;

        }

        public static ProductModel loadProduct(long id)
        {

            ProductModel cp = new ProductModel();
            var client = new HttpClient();
            {
                var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/product_id");

                var newPost = new PostId()
                {
                    id = id

                };
                try
                {
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;
                    var x = JsonConvert.DeserializeObject<RootProduct2>(result);
                    cp = x.data;

                }
                catch (System.Exception e)
                {
                    // Logging.WriteLogFile(e.ToString());
                }
            }


            return cp;

        }

        public static void createProducts(ProductsModel productModel)
        {
 



            // return cp;
        }
        public static void createProduct(ProductModel productModel)
        {
            ProductModel cp = new ProductModel();
            var client = new HttpClient();
            {
                var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/product");

                var newPost = new ProductModel()
                {
                    id = productModel.id,
                    name = productModel.name

                };
                try
                {
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;
                    var x = JsonConvert.DeserializeObject<RootProduct2>(result);
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