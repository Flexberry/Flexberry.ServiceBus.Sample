using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestSender1.ConsoleHost
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

            string apiUrl = configuration["SBServiceURL"] + "/Message";

            using (var httpClient = new HttpClient())
            {
                object input = new
                {
                    Body = "Hello from RestSender1!",
                    ClientID = configuration["ClientID"],
                    MessageTypeID = configuration["MessageTypeID"]
                };

                var msg = JsonConvert.SerializeObject(input);
                var buffer = Encoding.UTF8.GetBytes(msg);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var sbServiceAvailable = false;
                Task<HttpResponseMessage> responseTask = null;

                while (!sbServiceAvailable)
                {
                    try
                    {
                        responseTask = httpClient.PostAsync(apiUrl, byteContent);

                        responseTask.Wait();

                        var response = responseTask.Result;

                        if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                        {
                            Console.WriteLine("Message sended...");
                            sbServiceAvailable = true;
                        }
                        else
                        {
                            Console.WriteLine("Message delayed: " + response.StatusCode);

                            Task.Delay(2000).Wait();
                        }
                    } 
                    catch (Exception ex)
                    {
                        Console.WriteLine("SBService delayed: " + ex.Message);

                        Task.Delay(2000).Wait();
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
