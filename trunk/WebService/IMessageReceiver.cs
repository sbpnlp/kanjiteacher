using System;
using System.ServiceModel;
using System.Collections.Generic;

namespace OpenNETCF.WCF.Sample
{
    [ServiceContract(Namespace = "http://opennetcf.wcf.sample")]
    public interface IMessageReceiver
    {
        [OperationContract]
        //bool ReceiveMessage(List<System.Windows.Forms.MouseEventArgs> message, List<DateTime> times);
        int ReceiveMessage(List<int> xcoords, List<int> ycoords, List<DateTime> times);
    }
}
