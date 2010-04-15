using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logics = Kanji.DesktopApp.LogicLayer;
using System.Threading;
using Kanji.DesktopApp.Interfaces;
using SMC = System.ServiceModel.Channels;

namespace Kanji.InputArea.WinFormGUI
{
/// <summary>
    /// Input controller for input coming from a non-mobil device.
    /// </summary>
    public class WinClientCommunication : IController
    {
        #region Fields
        public string IP { get { return _ip; } set { _ip = value; } }
        private KanjiServiceClient _client = null;
        public IControlled View { get; set; }
        private string _ip = "localhost";
        #endregion
        
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WinClientCommunication"/> class.
        /// </summary>
        public WinClientCommunication() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="MobileClientCommunication"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public WinClientCommunication(IControlled view)
        {
            View = view;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MobileClientCommunication"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="ipaddress">The ipaddress.</param>
        public WinClientCommunication(IControlled view, string ipaddress)
        {
            View = view;
            IP = ipaddress;
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

        #region Helpers
        /// <summary>
        /// Creates the client. Establishes a binding to the web service 
        /// at the IP given at this.IP
        /// </summary>
        /// <returns>A handle to the client.</returns>
        private KanjiServiceClient CreateClient()
        {
            SMC.Binding binding = CreateDefaultBinding();
            string remoteAddress = KanjiServiceClient.EndpointAddress.Uri.ToString();
            remoteAddress = remoteAddress.Replace("localhost", IP);
            EndpointAddress endpoint = new EndpointAddress(remoteAddress);

            return new KanjiServiceClient(binding, endpoint);
        }
        private SMC.Binding CreateDefaultBinding()
        {
            SMC.CustomBinding binding = new SMC.CustomBinding();
            binding.Elements.Add(new SMC.TextMessageEncodingBindingElement(SMC.MessageVersion.Soap11, System.Text.Encoding.UTF8));
            binding.Elements.Add(new SMC.HttpTransportBindingElement());
            return binding;
        }
        #endregion
#endregion

//        /// <summary>
//        /// Receives the point list from the input device.
//        /// </summary>
//        /// <param name="sender">The sender.</param>
//        /// <param name="e">The <see cref="Kanji.DesktopApp.LogicLayer.MouseInputEventArgs"/> instance containing the event data.</param>
//        public void ReceivePointList(object sender, IMouseInputEventArgs e)
//        {
//            // Create stroke from ActivePoints
//            // Store Stroke in stroke list
//            // take current stroke list and try to build radicals in different variants
//            // store radicals
//            // take current radicals and try to build character from them
//            // store characters

////            Logics.Character c = new Logics.Character(((Logics.MouseInputEventArgs)e).ActivePoints);
////            c.AppController = this;
//            // Thread t = new Thread(Logics.Character.CreateFromPointList);
//            // t.Start(c);
////            Logics.Stroke s = new Logics.Stroke(((Logics.MouseInputEventArgs)e).ActivePoints);
////            Logics.Character.CreateFromPointList(c);

//            //why giving anything back to the inputarea??? View.ReceivePointList((IStroke)s, (IBoundingBox)bb);
//            //giving it to the "View" is correct, but the inputarea is not the "View"!
//        }

//        /// <summary>
//        /// Receives the character models. When the characters have been built up from
//        /// the point lists, this method can be used to transmit those models to
//        /// the controller.
//        /// </summary>
//        /// <param name="cModels">The character models.</param>
//        public void ReceiveCharacterModels(List<ICharacter> cModels)
//        {
//            //again: transmitting the character models to the view is correct,
//            //but the inputarea is not the view, rather send it to the desktopapp.winformgui
//            //View.ReceiveCharacters(cModels);
//        }

//        /// <summary>
//        /// Receives the character list. When the characters models have been compared with the stored
//        /// characters in the DB, this method can be used to transmit those models to
//        /// the controller.
//        /// </summary>
//        /// <param name="cList">The character list.</param>
//        public void ReceiveCharacterList(List<ICharacter> cList)
//        {
//            throw new NotImplementedException();
//        }
    }
}
