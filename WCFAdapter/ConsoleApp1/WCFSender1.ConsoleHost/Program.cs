using Microsoft.Extensions.Configuration;
using SBService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace WCFSender1.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", false, false)
               .AddEnvironmentVariables()
               .Build();

            LogConfigurationValue(configuration, "SBServiceURL");
            LogConfigurationValue(configuration, "ClientID");
            LogConfigurationValue(configuration, "MessageTypeID");

            var lstMessages = new List<string>() {
                "Test from sender 1",
                "Second test from sender 1",
                "Test from sender 1. Last message" };

            Console.WriteLine("WCFSender1 Started");

            var binding = new WSHttpBinding();
            var endpointAdress = new EndpointAddress(configuration["SBServiceURL"]);

            binding.Security.Mode = SecurityMode.None;

            using (var ServiceBus = new ServiceBusServiceClient(binding, endpointAdress))
            {
                //ServiceBus.Endpoint.Address.Uri = configuration["SBServiceURL"];

                var messages = lstMessages.Select(x => new MessageForESB
                {
                    ClientID = configuration["ClientID"],
                    MessageTypeID = configuration["MessageTypeID"],
                    Body = "Hello from WCFSender1! Message: " + x
                });

                foreach (var message in messages)
                {
                    // Отправить сообщение через шину.
                    var operationCopmpleted = false;

                    while (!operationCopmpleted)
                    {
                        try
                        {
                            ServiceBus.SendMessageToESBAsync(message).Wait();

                            operationCopmpleted = true;
                        } 
                        catch (Exception ex)
                        {
                            Console.WriteLine("Sending delayed: " + ex.Message);

                            System.Threading.Tasks.Task.Delay(2000).Wait();
                        }
                    }
                    Console.WriteLine("Send message: " + message.Body);
                }

                ServiceBus.Close();
            }

            Console.WriteLine("WCFSender1 Stoped");
        }

        private static void LogConfigurationValue(IConfiguration configuration, string paramName)
        {
            Console.WriteLine($"Param: {paramName} = {configuration[paramName]}");
        }
    }
}
