namespace MsgListener
{
    using System;
    using System.Configuration;
    using System.ServiceModel;
    using System.Threading;
    using ICSSoft.STORMNET.Tools;
    using ServiceBusServiceClient;

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
                var service = new MsgListenerClass();
                myServiceHost = new ServiceHost(service);

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

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class MsgListenerClass : ICallbackSubscriber
    {
        public string GetSourceId()
        {
            return ConfigurationManager.AppSettings["ExternalKey"];
        }

        public void AcceptMessage(MessageFromESB msg)
        {
            Console.WriteLine(ToolZIP.Decompress(msg.Body));
        }

        public void RiseEvent(string ИдТипаСобытия)
        {
            Console.WriteLine(ИдТипаСобытия);
        }

        internal static void SubscribeMe4Messages(string ИдТипаСообщения)
        {
            using (ServiceBusServiceClient.ServiceBusServiceClient ServiceBus = new
            ServiceBusServiceClient.ServiceBusServiceClient())
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