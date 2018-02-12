namespace MsgSender
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Configuration;
    using ICSSoft.STORMNET.Tools;
    using System.Net;
    

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
                    using (var ServiceBus = new ServiceBusServiceClient("SBService"))
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
                            Body = ToolZIP.Compress("Hello from " + s + "!")
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
