using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace MsgRecipient
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string url = ConfigurationManager.AppSettings["Address"] + "/Messages?clientId=" + ConfigurationManager.AppSettings["RecipientID"];

                using (var webClient = new WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    var response = webClient.DownloadString(url);
                    List<Message> items = JsonConvert.DeserializeObject<List<Message>>(response);
                    foreach (var item in items)
                    {
                        string url2 = ConfigurationManager.AppSettings["Address"] + "/Message/" + item.Id;
                        response = webClient.DownloadString(url2);
                        Console.WriteLine(response);
                        Console.Write("Delete message?[y/n]");
                        var key=Console.ReadKey(false);
                        Console.WriteLine();
                        if (key.Key== ConsoleKey.Y)
                        {
                            var res=webClient.UploadValues(url2, "DELETE", new NameValueCollection());
                            Console.WriteLine($"DELETED - {url2}");
                        }
                    }
                }

                Thread.Sleep(5000);
            }
        }
    }

    public class Message
    {
        public string Id { get; set; }
        public string MessageTypeID { get; set; }
        public int Priority { get; set; }
        public string MessageFormingTime { get; set; }
    }
}
