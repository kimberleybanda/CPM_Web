using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using CPMv2.Helpers;
using System.Text;

namespace CPMv2.Code
{
    public class PdfTutorials
    {

        public long id { get; set; }
        public String pdfUrl { get; set; }
        public String tutorialName { get; set; }
    }
    public class VideoTutorials
    {

        public long id { get; set; }
        public String vidNameUrl { get; set; }
        public String tutorialName { get; set; }
    }

    public class RootPdf
    {
        public object status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public List<PdfTutorials> data { get; set; }
    }

    public class RootPdf2
    {
        public object status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public PdfTutorials data { get; set; }
    }

    public class RootVideos
    {
        public object status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public List<VideoTutorials> data { get; set; }
    }

    public class RootVideos2
    {
        public object status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public VideoTutorials data { get; set; }
    }
    public static class TutorialsContextProvider
    {
        public static List<VideoTutorials> GetTutorialVid()
        {

            List<VideoTutorials> videoList = new List<VideoTutorials>();
            var client3 = new HttpClient();
            {
                String url = Helper.GetBaseUrl() + "v1/api/videos";
                var endpoint3 = new Uri(url);

                try
                {
                    var result3 = client3.GetAsync(endpoint3).Result.Content.ReadAsStringAsync().Result;
                    var cp3 = JsonConvert.DeserializeObject<RootVideos>(result3);

                    if (cp3.code == 200)
                    {
                        videoList = cp3.data;

                    }
                    else
                    {

                    }

                }
                catch (Exception eecc)
                {
                }

                return videoList;

            }

        }
        

        public static VideoTutorials loadVideo(long id)
        {

            VideoTutorials cp = new VideoTutorials();
            var client = new HttpClient();
            {
                var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/videos_id");

                var newPost = new PostId()
                {
                    id = id

                };
                try
                {
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;
                    var x = JsonConvert.DeserializeObject<RootVideos2>(result);
                    cp = x.data;

                }
                catch (System.Exception e)
                {
                    // Logging.WriteLogFile(e.ToString());
                }
            }


            return cp;

        }

        public static List<PdfTutorials> GetTutorialPdf()
        {

            List<PdfTutorials> videoList = new List<PdfTutorials>();
            var client3 = new HttpClient();
            {
                String url = Helper.GetBaseUrl() + "v1/api/pdf";
                var endpoint3 = new Uri(url);

                try
                {
                    var result3 = client3.GetAsync(endpoint3).Result.Content.ReadAsStringAsync().Result;
                    var cp3 = JsonConvert.DeserializeObject<RootPdf>(result3);

                    if (cp3.code == 200)
                    {
                        videoList = cp3.data;

                    }
                    else
                    {

                    }

                }
                catch (Exception eecc)
                {
                }

                return videoList;

            }

        }
        public static PdfTutorials loadPdf(long id)
        {

            PdfTutorials cp = new PdfTutorials();
            var client = new HttpClient();
            {
                var endpoint = new Uri(Helper.GetBaseUrl() + "v1/api/pdf_id");

                var newPost = new PostId()
                {
                    id = id

                };
                try
                {
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;
                    var x = JsonConvert.DeserializeObject<RootPdf2>(result);
                    cp = x.data;

                }
                catch (System.Exception e)
                {
                    // Logging.WriteLogFile(e.ToString());
                }
            }


            return cp;

        }
    }
}