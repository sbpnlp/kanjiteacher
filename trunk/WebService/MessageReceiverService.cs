using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenNETCF.WCF.Sample
{
    public class MessageReceiverService : IMessageReceiver
    {
        #region IMessageReceiver Members

        //public bool ReceiveMessage(List<System.Windows.Forms.MouseEventArgs> message, List<DateTime> times)
        public int ReceiveMessage(List<int> xcoords, List<int> ycoords, List<DateTime> times)
        {
            //Console.WriteLine("Received a list of Mouse coordinates, first point: <{0}, {1}> at time {2}",
            //    message[0].X, message[0].Y, times[0].ToShortTimeString());
            //return message.Count == times.Count;

            Console.WriteLine("Received mouse event: x:{0},y:{1} received datetime: {2}", xcoords[0], ycoords[0], times[0].ToShortDateString());
            return DateTime.Now.Millisecond - times[0].Millisecond;
        }

        #endregion
    }
}
