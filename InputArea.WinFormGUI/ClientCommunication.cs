using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SMC = System.ServiceModel.Channels;
using System.ServiceModel;
using Kanji.DesktopApp.Interfaces;
/* This class should be almost identical to the class
 * with the same name in the namespace:
 * Kanji.WinFormInputArea.MobileGUI
 * Except for tiny bits that must be different.
 * All the methods should be the same.
 * If you change this, change the other one, too.
 */
namespace Kanji.InputArea.WinFormGUI
{
    /// <summary>
    /// Client communicaton class for input coming from a non-mobil device.
    /// </summary>
    public class ClientCommunication : IController
    {
        #region Fields
        public string IP { get { return _ip; } set { _ip = value; } }
        private KanjiServiceClient _client = null;
        public IControlled View { get; set; }
        private string _ip = "localhost";
        #endregion
        
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientCommunication"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public ClientCommunication(IControlled view)
        {
            View = view;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MobileClientCommunication"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="ipaddress">The ipaddress.</param>
        public ClientCommunication(IControlled view, string ipaddress)
        {
            View = view;
            IP = ipaddress;
            _client = CreateClient();
        }
        #endregion

        #region IController Members

        /// <summary>
        /// Sends the point list to the web service.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="activePoints">The active points.</param>
        /// <param name="activeTimes">The active times.</param>
        /// <returns>
        /// True if the transmission of the message to web service was successful, false otherwise.
        /// </returns>
        public bool SendPointList(object sender,
            List<System.Windows.Forms.MouseEventArgs> activePoints,
            List<DateTime> activeTimes)
        {
            try
            {
                if (_client == null) _client = CreateClient();
                //legacy Thread t = new Thread(SendPointListThreadWorker);
                //legacy ThreadParameter threadParam = new ThreadParameter() { ActivePoints = activePoints, ActiveTimes = activeTimes };
                //legacy t.Start(threadParam);
                //legacy //debug SendPointListThreadWorker(threadParam);

                ThreadStart tStart = delegate { SendPointListThreadWorker(activePoints, activeTimes); };
                Thread t2 = new Thread(tStart);
                t2.Start();
                //debug SendPointListThreadWorker(activePoints, activeTimes);                

                return true;
            }
            catch { return false; }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Thread worker for sending the point list.
        /// Awaits as input the list of points and the list of timestamps.
        /// </summary>
        /// <param name="pointListObject">The point list object which should be a type 
        /// that contains the public fields 
        /// List<System.Windows.Forms.MouseEventArgs> ActivePoints and
        /// List<DateTime> ActiveTimes.</param>
        void SendPointListThreadWorker(object pointListObject)
        {
            ThreadParameter tp = null;
            if (!(pointListObject is ThreadParameter))
            {
                System.Windows.Forms.MessageBox.Show("Point list could not be sent");
                return;
            }
            else
            {
                tp = pointListObject as ThreadParameter;
                SendPointListThreadWorker(tp.ActivePoints, tp.ActiveTimes);
            }
        }

        /// <summary>
        /// Thread worker for sending the point list.
        /// Awaits as input the list of points and the list of timestamps.
        /// </summary>
        /// <param name="ActivePoints">The active points.</param>
        /// <param name="ActiveTimes">The active times.</param>
        void SendPointListThreadWorker(List<System.Windows.Forms.MouseEventArgs> ActivePoints, List<DateTime> ActiveTimes)
        {
            try
            {
                List<int> xs = new List<int>();
                List<int> ys = new List<int>();
                foreach (System.Windows.Forms.MouseEventArgs mea in ActivePoints)
                {
                    xs.Add(mea.X);
                    ys.Add(mea.Y);
                }

                _client.ReceivePoints(xs.ToArray(), ys.ToArray(), ActiveTimes.ToArray());
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Creates the client. Establishes a binding to the web service 
        /// at the IP given at this.IP
        /// </summary>
        /// <returns>A handle to the client.</returns>
        private KanjiServiceClient CreateClient()
        {
            SMC.Binding binding = CreateDefaultBinding();
            string remoteAddress = "http://localhost:8000/kanji/KanjiService"; //KanjiServiceClient.EndpointAddress.Uri.ToString();
            remoteAddress = remoteAddress.Replace("localhost", IP);
            EndpointAddress endpoint = new EndpointAddress(remoteAddress);

            try
            {
                return new KanjiServiceClient(binding, endpoint);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(string.Format("File output.config path? probably not in the bin folder. Ex.msg: {0}", ex.Message));
                return null;
            }
        }
        private SMC.Binding CreateDefaultBinding()
        {
            SMC.CustomBinding binding = new SMC.CustomBinding();
            binding.Elements.Add(new SMC.TextMessageEncodingBindingElement(SMC.MessageVersion.Soap11, System.Text.Encoding.UTF8));
            binding.Elements.Add(new SMC.HttpTransportBindingElement());
            return binding;
        }
        #endregion
    }

    /// <summary>
    /// Helper struct that serves as a parameter for the parametrised thread start.
    /// </summary>
    class ThreadParameter
    {
        public List<DateTime> ActiveTimes;
        public List<System.Windows.Forms.MouseEventArgs> ActivePoints;
    }
}
