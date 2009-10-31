using System;
using System.ServiceModel;
using System.Collections.Generic;

namespace Kanji.KanjiService
{
    [ServiceContract(Namespace = "http://www.japanese-coach.com")]
    public interface IKanjiService
    {
        /// <summary>
        /// Receives the point lists from the input area.
        /// </summary>
        /// <param name="xcoords">The xcoords.</param>
        /// <param name="ycoords">The ycoords.</param>
        /// <param name="times">The times.</param>
        /// <returns>True if the transmission of points to desktop was successful, false otherwise.</returns>
        [OperationContract]
        bool ReceivePoints(List<int> xcoords, List<int> ycoords, List<DateTime> times);

        [OperationContract]
        List<int> GetPointsX();
        [OperationContract]
        List<int> GetPointsY();
        [OperationContract]
        List<DateTime> GetPointsTime();

        /// <summary>
        /// Receives the messages for desktop sent by the input area.
        /// </summary>
        /// <param name="messages">The messages.</param>
        /// <returns>True if the transmission of the message to web service was successful, false otherwise.</returns>
        [OperationContract]
        bool MessageForDesktop(int messages);

        /// <summary>
        /// Receives the messages for input area, sent by the desktop app.
        /// </summary>
        /// <param name="messages">The messages.</param>
        /// <returns>True if the transmission of the message to web service was successful, false otherwise.</returns>
        [OperationContract]
        bool MessageForInputArea(int messages);

        /// <summary>
        /// Gets the new messages for the mobile input area.
        /// </summary>
        /// <returns>Messages for mobile input area.</returns>
        [OperationContract]
        int GetNewMessagesForMobile();

        /// <summary>
        /// Gets the new messages for the desktop application.
        /// </summary>
        /// <returns>Messags for desktop app.</returns>
        [OperationContract]
        int GetNewMessagesForDesktop();

    }
}
