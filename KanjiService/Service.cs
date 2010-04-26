using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Net;
using System.Threading;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.KanjiService
{
    public class Service
    {
        /// <summary>
        /// Gets or sets a value indicating whether to [show meta data] or not.
        /// </summary>
        /// <value><c>true</c> if [show meta data]; otherwise, <c>false</c>.</value>
        public bool ShowMetaData { get { return showmdata; } set { showmdata = value; } }
        private bool showmdata = false;

        public void Run(IObserver observer)
        {
            IPAddress hostIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
            foreach (IPAddress ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ip.ToString().Substring(0, 3) == "192") //local
                ////if (ip.ToString().Substring(0, 3) == "134") //exclusively for saarland university
                {
                    hostIP = ip;
                    break;
                }
            }

            observer.setIP(hostIP);
            Uri address;

            if (showmdata)
            {
                address = new Uri(string.Format("http://localhost:8000/kanji", hostIP.ToString()));
                Console.WriteLine("Discovery mode");
            }

            else
            {
                address = new Uri(string.Format("http://{0}:8000/kanji", hostIP.ToString()));
                Console.WriteLine("NOT Discovery mode");
            }

            KanjiService knsvc = new KanjiService();
            knsvc.RegisterObserver(observer);

            //ServiceHost serviceHost = new ServiceHost(typeof(KanjiService), address);

            ServiceHost serviceHost = new ServiceHost((object) knsvc, address);

            //if (serviceHost.SingletonInstance is KanjiService)
            //    Console.WriteLine("is a kanjiservice");

            try
            {
                serviceHost.AddServiceEndpoint(
                    typeof(IKanjiService),
                    new BasicHttpBinding(),
                    "KanjiService");

                if (showmdata)
                {
                    // Enable metadata exchange - this is needed for NetCfSvcUtil to discover us
                    ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                    smb.HttpGetEnabled = true;
                    serviceHost.Description.Behaviors.Add(smb);
                    Console.WriteLine("Metadata published");
                }
                else
                {
                    Console.WriteLine("Metadata NOT published");
                }
                serviceHost.Open();

                Console.WriteLine("KanjiService is running at " + address.ToString());
                //Console.WriteLine("KanjiService is running at " + Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString());
                //Console.WriteLine("Press <ENTER> to terminate");
                int i = Int32.MaxValue /2; 
                while (true)
                {
                    if (i-- == 0)
                    {
                        Thread.Sleep(1000);
                        
                        i = Int32.MaxValue /2;
                    }
                }
                //Console.ReadLine();

                // Close the ServiceHostBase to shutdown the service.
                //serviceHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occured: {0}", ce.Message);
                serviceHost.Abort();
            }
        }
    }
}
