using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Configuration;

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
                    Console.WriteLine(response);
                }

                Thread.Sleep(5000);
            }
        }
    }
}
