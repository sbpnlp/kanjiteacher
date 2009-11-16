using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.KanjiService
{
    public enum KanjiMessage
    {
        Nothing = 0x00000001,
        ClearData = 0x00000002,
        FinishedStroke = 0x00000004,
        InputCharacter = 0x00000008
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class KanjiService : IKanjiService
    {
        private int InputAreaMessages { get; set; }

        private int DesktopMessages { get; set; }
        
        public IObserver Observer { get; set; }

        #region IKanjiService Members

        public bool ReceivePoints(List<int> xcoords, List<int> ycoords, List<DateTime> times)
        {
            Console.WriteLine("Received mouse event: x:{0},y:{1} received datetime: {2}", xcoords[0], ycoords[0], times[0].ToShortDateString());
            Observer.ReveivePoints(xcoords, ycoords, times);
            return true;
        }

        /// <summary>
        /// Receives the messages for desktop sent by the input area.
        /// </summary>
        /// <param name="messages">The messages.</param>
        /// <returns>
        /// True if the transmission of the message to web service was successful, false otherwise.
        /// </returns>
        public bool MessageForDesktop(int messages)
        {
            if (MessageContains(DesktopMessages, (int)KanjiMessage.Nothing))
            {
                //replace DesktopMessages with messages
                DesktopMessages = messages;
            }
            else
            {
                //add messages to DesktopMessages
                DesktopMessages |= messages;
            }
            return true;
        }

        /// <summary>
        /// Receives the messages for input area, sent by the desktop app.
        /// </summary>
        /// <param name="messages">The messages.</param>
        /// <returns>
        /// True if the transmission of the message to web service was successful, false otherwise.
        /// </returns>
        public bool MessageForInputArea(int messages)
        {
            if (MessageContains(InputAreaMessages, (int)KanjiMessage.Nothing))
            {
                //replace InputAreaMessages with messages
                InputAreaMessages = messages;
            }
            else
            {
                //add messages to InputAreaMessages 
                InputAreaMessages |= messages;
            }
            return true;
        }

        /// <summary>
        /// Gets the new messages for the mobile input area.
        /// </summary>
        /// <returns>Messages for mobile input area.</returns>
        public int GetNewMessagesForMobile()
        {
            int retval = InputAreaMessages;
            InputAreaMessages = (int)KanjiMessage.Nothing;
            return retval;
        }

        /// <summary>
        /// Gets the new messages for the desktop application.
        /// </summary>
        /// <returns>Messags for desktop app.</returns>
        public int GetNewMessagesForDesktop()
        {
            int retval = DesktopMessages;
            DesktopMessages = (int)KanjiMessage.Nothing;
            return retval;
        }

        public List<int> GetPointsX() { return null;  }

        public List<int> GetPointsY() { return null; }

        public List<DateTime> GetPointsTime() { return null; }
        #endregion

        #region Private messages
        /// <summary>
        /// Tests if the messages in 'messages' contain 'contained'.
        /// </summary>
        /// <param name="messages">The messages.</param>
        /// <param name="contained">The contained.</param>
        /// <returns>True, if contained is contained in messages.</returns>
        private bool MessageContains(int messages, int contained)
        {
            return ((messages & contained) != 0x0);
        }
        #endregion
    }
}
