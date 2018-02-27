using System;
using System.Net;
using System.Text;
using System.Configuration;
using Newtonsoft.Json;

namespace MsgSender
{
    class Program
    {
        static void Main(string[] args)
        {

            string msgBody = "";

            while (msgBody != "exit")
            {
                Console.Write("Enter your msg (for exit type \"exit\"): ");

                msgBody = Console.ReadLine();

                // Создадим сообщение.
                string jsonData = JsonConvert.SerializeObject(new
                {
                    Body = msgBody,
                    ClientID = ConfigurationManager.AppSettings["SenderID"],
                    MessageTypeID = ConfigurationManager.AppSettings["MessageTypeID"]
                });

                string url = ConfigurationManager.AppSettings["Address"] + "/Message";

                using (var webClient = new WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    webClient.UploadString(url, "POST", jsonData);
                    Console.WriteLine("Message sent");    
                }
            }
        }
    }
}
