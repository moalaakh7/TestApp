using Nancy.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TestApp.Utils
{
    public class FCM
    {
        public static string ServerKey { get; set; }
        static string webAddr = "https://fcm.googleapis.com/fcm/send";

        public static string SendNotification(string deviceToken, string title, string content)
        {
            try
            {
                var result = "-1";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", ServerKey));
                httpWebRequest.Method = "POST";

                var payload = new
                {
                    to = deviceToken,
                    priority = "high",
                    content_available = true,
                    notification = new
                    {
                        body = content,
                        title = title
                    },
                };
                var serializer = new JavaScriptSerializer();
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = serializer.Serialize(payload);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                return result;
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }
    }
}
