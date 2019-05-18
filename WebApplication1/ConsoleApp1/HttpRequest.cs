using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class HttpRequest
    {
        private static HttpClient client;
        public static HttpClient Singleton
        {
            get
            {
                if (client == null)
                {
                    Interlocked.CompareExchange(ref client, new HttpClient(),null);
                }
                return client;
            }
        }
        private static HttpClient http;
        static HttpRequest()
        {
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
            http = new HttpClient(handler);
        }
        public static int Timeout = 20000;
        public static string HttpPost(string Url, string postDataStr)
        {
            string retString = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.Timeout = Timeout;
                request.ContentType = "application/json";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                var data = Encoding.UTF8.GetBytes(postDataStr);
                request.ContentLength = data.Length;
                using (Stream myRequestStream = request.GetRequestStream())
                    myRequestStream.Write(data, 0, data.Length);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream myResponseStream = response.GetResponseStream())
                {
                    using (StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8")))
                    {
                        retString = myStreamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retString;
        }
        public static string DoPost(string url, string postJson)
        {
            string retStr = string.Empty;
            HttpContent content = new StringContent(postJson);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            try
            {
                var response = http.PostAsync(url, content).Result;
                response.EnsureSuccessStatusCode();
                retStr = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retStr;
        }
        public static async Task<HttpResponseMessage> DoGet(string url)
        {
            try
            {
                //var response = http.GetAsync(url).Result;
                var response = http.GetAsync(url);
                return await response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string DoGet1(string url)
        {
            string retStr = string.Empty;
            try
            {
                var response = HttpRequest.Singleton.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                retStr = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                retStr = ex.Message;
                throw ex;
            }
            return retStr;
        }
        public static string HttpGet(string Url)
        {
            string retString = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "GET";
                request.Timeout = Timeout;
                request.ContentType = "application/json";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream myResponseStream = response.GetResponseStream())
                {
                    using (StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8")))
                    {
                        retString = myStreamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retString;
        }
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
    }
}
