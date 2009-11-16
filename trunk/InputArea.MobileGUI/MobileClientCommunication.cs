using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using LL = Kanji.DesktopApp.LogicLayer;
using Draw = System.Drawing;
using SMC = System.ServiceModel.Channels;
using System.ServiceModel;
using System.Threading;

namespace Kanji.InputArea.MobileGUI
{
    internal class MobileClientCommunication
    {
        #region Fields
        public string IP { get; set; }
        private KanjiServiceClient _client = null;
        private IMobileView _view { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MobileClientCommunication"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public MobileClientCommunication(IMobileView view)
        {
            _view = view;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MobileClientCommunication"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="ipaddress">The ipaddress.</param>
        public MobileClientCommunication(IMobileView view, string ipaddress)
        {
            _view = view;
            IP = ipaddress;
        }

        #endregion

        ///// <summary>
        ///// Sends the message for the desktop app to the web service.
        ///// </summary>
        ///// <param name="message">The message.</param>
        ///// <returns>
        ///// True if the transmission of the message to web service was successful, false otherwise.
        ///// </returns>
        //public bool SendMessageToDesktop(KanjiMessage message)
        //{
        //    return _client.MessageForDesktop((int)message);
        //}

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
            if (_client == null) _client = CreateClient();

            try
            {
                List<int> xs = new List<int>();
                List<int> ys = new List<int>();
                foreach (System.Windows.Forms.MouseEventArgs mea in activePoints)
                {
                    xs.Add(mea.X);
                    ys.Add(mea.Y);
                }

                //MessageForDesktopWrapper((object)KanjiMessage.FinishedStroke);
                ////why doesn't this method want to be called in a thread?
                ////Thread myThread = new Thread(this.MessageForDesktopWrapper);
                ////myThread.Start((object)KanjiMessage.FinishedStroke);

                return _client.ReceivePoints(xs.ToArray(), ys.ToArray(), activeTimes.ToArray());
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
        }

        ///// <summary>
        ///// Wraps the web service method for threading.
        ///// </summary>
        ///// <param name="message">The message.</param>
        ///// <returns></returns>
        //public void MessageForDesktopWrapper(object message)
        //{
        //    _client.MessageForDesktop((int) message);
        //}

        ///// <summary>
        ///// Handles an incoming message from the server appropriately.
        ///// </summary>
        ///// <param name="kanjiMessage">The kanji message.</param>
        //private void HandleMessage(KanjiMessage kanjiMessage)
        //{
        //    switch (kanjiMessage)
        //    {
        //        case KanjiMessage.Nothing:
        //            //System.Windows.Forms.MessageBox.Show(KanjiMessage.Nothing.ToString());
                    
        //            break;
        //        case KanjiMessage.InputCharacter:
        //            //System.Windows.Forms.MessageBox.Show(KanjiMessage.InputCharacter.ToString());
        //            break;
        //        case KanjiMessage.FinishedStroke:
        //            //System.Windows.Forms.MessageBox.Show(KanjiMessage.FinishedStroke.ToString());
        //            break;
        //        case KanjiMessage.ClearData:
        //            //System.Windows.Forms.MessageBox.Show(KanjiMessage.ClearData.ToString());
        //            break;
        //    }
        //}

        /// <summary>
        /// Creates the client. Establishes a binding to the web service 
        /// at the IP given at this.IP
        /// </summary>
        /// <returns>A handle to the client.</returns>
        private KanjiServiceClient CreateClient()
        {
            SMC.Binding binding = KanjiServiceClient.CreateDefaultBinding();
            string remoteAddress = KanjiServiceClient.EndpointAddress.Uri.ToString();
            remoteAddress = remoteAddress.Replace("localhost", IP);
            EndpointAddress endpoint = new EndpointAddress(remoteAddress);

            return new KanjiServiceClient(binding, endpoint);
        }
    }
}
