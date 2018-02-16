using ConsoleApp1.ServiceBus;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "";

            while (s != "exit")
            {
                Console.WriteLine("Enter your name (for exit type \"exit\"):");

                s = Console.ReadLine();

                if (s != "exit")
                {
                    using (var ServiceBus = new ServiceBus.ServiceBusServiceClient())
                    {
                        // Установим прокси, если нужно.
                        var useProxy = ConfigurationManager.AppSettings["UseProxy"];
                        if (!string.IsNullOrEmpty(useProxy) && useProxy.ToLower() == "true")
                        {
                            var proxy = new WebProxy(ConfigurationManager.AppSettings["ProxyServer"], true)
                            {
                                Credentials = new NetworkCredential(ConfigurationManager.AppSettings["ProxyLogin"], ConfigurationManager.AppSettings["ProxyPass"])
                            };
                            WebRequest.DefaultWebProxy = proxy;
                        }

                        // Создадим сообщение.
                        var message = new MessageForESB
                        {
                            ClientID = ConfigurationManager.AppSettings["ServiceID4SB"],
                            MessageTypeID = ConfigurationManager.AppSettings["MessageTypeID"],
                            Body = "Hello from " + s + "!"
                        };

                        // И отправим его через шину.
                        ServiceBus.SendMessageToESB(message);

                        ServiceBus.Close();
                    }
                }
            }
        }
    }
}
