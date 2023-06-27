using Microsoft.Extensions.Configuration;
using SBService;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WCFRecipient1
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

            Console.WriteLine("WCFRecipient1 Started");

            var binding = new WSHttpBinding();
            var endpointAdress = new EndpointAddress(configuration["SBServiceURL"]);

            binding.Security.Mode = SecurityMode.None;

            using (var ServiceBus = new ServiceBusServiceClient(binding, endpointAdress))
            {
                var clientID = configuration["ClientID"];
                var messageTypeID = configuration["MessageTypeID"];
                var sbServiceAvailable = false;

                GetMessageFromESBResponse message = null;
                Task<GetMessageFromESBResponse> messageTask = null;

                while (!sbServiceAvailable)
                {
                    try
                    {
                        messageTask = ServiceBus.GetMessageFromESBAsync(clientID, messageTypeID);
                        messageTask.Wait();
                        sbServiceAvailable = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("SBService delayed: " + ex.Message);

                        Task.Delay(5000).Wait();
                    }
                }

                message = messageTask.Result;

                while (message?.Body?.GetMessageFromESBResult != null)
                {
                    Console.WriteLine("Message Body: " + message.Body.GetMessageFromESBResult.Body);

                    messageTask = ServiceBus.GetMessageFromESBAsync(clientID, messageTypeID);

                    messageTask.Wait();

                    message = messageTask.Result;
                }
            }

            Console.WriteLine("WCFRecipient1 Stoped");
        }

        private static void LogConfigurationValue(IConfiguration configuration, string paramName)
        {
            Console.WriteLine($"Param: {paramName} = {configuration[paramName]}");
        }
    }
}
