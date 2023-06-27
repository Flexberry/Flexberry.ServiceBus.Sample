using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestRecipient.ConsoleHost
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

            Console.WriteLine("RestSender1 Started");

            string apiUrl = configuration["SBServiceURL"];

            using (var httpClient = new HttpClient())
            {
                var sbServiceAvailable = false;
                Task<HttpResponseMessage> responseTask = null;

                while (!sbServiceAvailable)
                {
                    try
                    {
                        responseTask = httpClient.GetAsync(apiUrl + "/Messages?clientId=" + configuration["ClientID"]);

                        responseTask.Wait();

                        var readTask = responseTask.Result.Content.ReadAsStringAsync();

                        readTask.Wait();

                        var result = readTask.Result;
                        var items = JsonConvert.DeserializeObject<List<SBMesagesInfo>>(result);

                        foreach (var item in items)
                        {
                            var messageUrl = apiUrl + "/Message/" + item.Id;
                            var messageTask = httpClient.GetAsync(messageUrl);

                            messageTask.Wait();

                            var readMessageTask = messageTask.Result.Content.ReadAsStringAsync();

                            readMessageTask.Wait();

                            Console.WriteLine(readMessageTask.Result);

                            httpClient.DeleteAsync(messageUrl).Wait();
                        }

                        sbServiceAvailable = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("SBService delayed: " + ex.Message);

                        Task.Delay(5000).Wait();
                    }
                }
            }

            Console.WriteLine("RestSender1 Stoped");
        }

        private static void LogConfigurationValue(IConfiguration configuration, string paramName)
        {
            Console.WriteLine($"Param: {paramName} = {configuration[paramName]}");
        }
    }
}