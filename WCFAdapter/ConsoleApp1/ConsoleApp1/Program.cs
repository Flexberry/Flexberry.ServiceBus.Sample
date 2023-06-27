using ConsoleApp1.ServiceBus;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lstMessages = new List<string>() { 
                "Test from sender 1", 
                "Second test from sender 1",
                "Test from sender 1. Last message" };

            Console.WriteLine("WCFSender1 Started");

            using (var ServiceBus = new ServiceBusServiceClient())
            {
                var messages = lstMessages.Select(x => new MessageForESB
                {
                    ClientID = ConfigurationManager.AppSettings["ServiceID4SB"],
                    MessageTypeID = ConfigurationManager.AppSettings["MessageTypeID"],
                    Body = "Hello from WCFSender1! Message: " + x
                });

                foreach (var message in messages)
                {
                    // Отправить сообщение через шину.
                    ServiceBus.SendMessageToESB(message);
                    Console.WriteLine("Send message: " + message.Body);
                }

                ServiceBus.Close();
            }

            Console.WriteLine("WCFSender1 Stoped");
        }
    }
}
