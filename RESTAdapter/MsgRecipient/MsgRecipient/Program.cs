using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

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
                    List<MessageListItem> items = JsonConvert.DeserializeObject<List<MessageListItem>>(response);
                    foreach (var item in items)
                    {
                        string url2 = ConfigurationManager.AppSettings["Address"] + "/Message/" + item.Id;
                        response = webClient.DownloadString(url2);
                        Message msg = JsonConvert.DeserializeObject<Message>(response);
                        Console.WriteLine(response);
                    }
                }

                Thread.Sleep(5000);
            }
        }
    }

    public class MessageListItem
    {
        public string Id { get; set; }
        public string MessageTypeID { get; set; }
        public int Priority { get; set; }
        public string MessageFormingTime { get; set; }
    }

    public class Message
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public string MessageFormingTime { get; set; }
        public string MessageTypeID { get; set; }
        public string SenderName { get; set; }
    }

}
