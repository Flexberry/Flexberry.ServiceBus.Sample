using ConsoleApp3.ServiceBus;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            string adaptersCommand = "";

            while (adaptersCommand != "exit")
            {
                using (var ServiceBus = new ServiceBus.ServiceBusServiceClient())
                {
                    string serviceID = ConfigurationManager.AppSettings["ServiceID4SB"];
                    string messageTypeID = ConfigurationManager.AppSettings["MessageTypeID"];

                    //Получить все адресованные сообщения (при запуске).
                    MessageFromESB message = ServiceBus.GetMessageFromESB(serviceID, messageTypeID);
                    Console.WriteLine(message.Body);

                    // Получить новые сообщения/выйти из приложения.
                    Console.WriteLine("Enter \"get\" for get a message (for exit type \"exit\"):");
                    adaptersCommand = Console.ReadLine();

                    while (message != null)
                    {
                        // Получить новый список сообщений через команду "get".
                        message = ServiceBus.GetMessageFromESB(serviceID, messageTypeID);
                    }
                }
            }
        }
    }
}
