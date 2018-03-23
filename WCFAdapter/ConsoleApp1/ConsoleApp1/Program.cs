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
            string adaptersCommand = "";

            while (adaptersCommand != "exit")
            {
                Console.WriteLine("Enter your name (for exit type \"exit\"):");

                adaptersCommand = Console.ReadLine();

                if (adaptersCommand != "exit")
                {
                    using (var ServiceBus = new ServiceBus.ServiceBusServiceClient())
                    {
                        // Ввести сообщение.
                        var message = new MessageForESB
                        {
                            ClientID = ConfigurationManager.AppSettings["ServiceID4SB"],
                            MessageTypeID = ConfigurationManager.AppSettings["MessageTypeID"],
                            Body = "Hello from " + adaptersCommand + "!"
                        };

                        // Отправить сообщение через шину.
                        ServiceBus.SendMessageToESB(message);

                        ServiceBus.Close();
                    }
                }
            }
        }
    }
}
