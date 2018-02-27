using ConsoleApp2.ServiceBus;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var messageFromESB = new MessageFromESB();
            MyServiceHost.StartService();

            Console.WriteLine("MsgListener started.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            MyServiceHost.StopService();
        }

        internal class MyServiceHost
        {
            private static Thread ScanThread;

            internal static ServiceHost myServiceHost = null;

            internal static void StartService()
            {
                myServiceHost = new ServiceHost(new MsgListenerClass());

                myServiceHost.Open();

                //ScanThread = MsgListenerClass.GetScanningThread();
                //ScanThread.Start();
            }

            internal static void StopService()
            {
                //ScanThread.Abort();

                if (myServiceHost.State != CommunicationState.Closed)
                    myServiceHost.Close();
            }
        }

    }

    [ServiceContract(ConfigurationName = "MsgListener.ICallbackSubscriber")]
    public interface ICallbackSubscriber
    {
        [OperationContract]
        void AcceptMessage(MessageFromESB msg);

        [OperationContract]
        void RiseEvent(string ИдТипаСобытия);

        string GetSourceId();
    }

    [ServiceBehavior(InstanceContextMode =InstanceContextMode.Single)]
    public class MsgListenerClass : ICallbackSubscriber
    {
        public string GetSourceId()
        {
            return ConfigurationManager.AppSettings["ExternalKey"];
        }

        public void AcceptMessage(MessageFromESB msg)
        {
            Console.WriteLine(msg.Body);
        }

        public void RiseEvent(string ИдТипаСобытия)
        {
            Console.WriteLine(ИдТипаСобытия);
        }

        internal static void SubscribeMe4Messages(string ИдТипаСообщения)
        {
            using (ServiceBus.ServiceBusServiceClient ServiceBus = new
            ServiceBus.ServiceBusServiceClient())
            {
                ServiceBus.SubscribeClientForMessageCallback(
                ConfigurationManager.AppSettings["ServiceID4SB"],
                ИдТипаСообщения);

                ServiceBus.Close();
            }
        }

        private static void NewSubscribeOrUpdate()
        {
            while (true)
            {
                SubscribeMe4Messages(
                ConfigurationManager.AppSettings["MessageTypeID"]);

                Thread.Sleep(Convert.ToInt32(
                ConfigurationManager.AppSettings["ScanPeriod"]));
            }
        }

        public static Thread GetScanningThread()
        {
            return new Thread(NewSubscribeOrUpdate);
        }
    }
}
