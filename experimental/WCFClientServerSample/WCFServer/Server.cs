using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Net;

namespace OpenNETCF.WCF.Sample
{
    class Server
    {
        static void Main(string[] args)
        {

            string hostIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();

#if CLIENT_DISCOVERY_BUILD
            Uri address = new Uri(string.Format("http://localhost:8000/msgreceiver", hostIP));
            //Uri address = new Uri(string.Format("http://localhost:8000/calculator", hostIP));
            Console.WriteLine("Discovery mode");
#else
            Uri address = new Uri(string.Format("http://{0}:8000/msgreceiver", hostIP));
            Console.WriteLine("NOT Discovery mode");
#endif
            //ServiceHost serviceHost = new ServiceHost(typeof(CalculatorService), address);
            ServiceHost serviceHost = new ServiceHost(typeof(MessageReceiverService), address);

            try
            {
                // Add a service endpoint
                //serviceHost.AddServiceEndpoint(
                //    typeof(ICalculator),
                //    new BasicHttpBinding(),
                //    "Calculator");

                serviceHost.AddServiceEndpoint(
                    typeof(IMessageReceiver),
                    new BasicHttpBinding(),
                    "MessageReceiver"); 

#if CLIENT_DISCOVERY_BUILD
                // Enable metadata exchange - this is needed for NetCfSvcUtil to discover us
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                serviceHost.Description.Behaviors.Add(smb);
                Console.WriteLine("Metadata published");
#else
                Console.WriteLine("Metadata NOT published");
#endif

                serviceHost.Open();

                //Console.WriteLine("CalculatorService is running at " + address.ToString());
                Console.WriteLine("MessageReceiverService is running at " + address.ToString());
                Console.WriteLine("MessageReceiverService is running at " + Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString());
                Console.WriteLine("Press <ENTER> to terminate");
                Console.ReadLine();

                // Close the ServiceHostBase to shutdown the service.
                serviceHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occured: {0}", ce.Message);
                serviceHost.Abort();
            }
        }
    }
}
