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

        public void Run(object iObserver)
        {
            IObserver observer = (IObserver)iObserver;

            string hostIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            //Console.WriteLine(Dns.GetHostName());
            //Console.WriteLine(Dns.GetHostEntry(Dns.GetHostName()).ToString());

            //Console.WriteLine("BEGIN IP-List");
            foreach (IPAddress ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                Console.WriteLine("IP: " + ip.ToString());
                if (ip.ToString().Substring(0, 3) == "192")
                {
                    hostIP = ip.ToString();
                    //Console.WriteLine("^\n|\nDie nehmen wir!");
                    break;
                }
            }
            //Console.WriteLine("END IP-List");

            Uri address;

            if (showmdata)
            {
                address = new Uri(string.Format("http://localhost:8000/kanji", hostIP));
                Console.WriteLine("Discovery mode");
            }

            else
            {
                address = new Uri(string.Format("http://{0}:8000/kanji", hostIP));
                Console.WriteLine("NOT Discovery mode");
            }

            KanjiService knsvc = new KanjiService();
            knsvc.Observer = observer;

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
