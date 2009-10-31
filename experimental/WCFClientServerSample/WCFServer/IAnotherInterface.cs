using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace OpenNETCF.WCF.Sample
{
    [ServiceContract(Namespace = "http://opennetcf.wcf.sample")]
    public interface IAnotherInterface
    {
        [OperationContract]
        string ReceiveMessage(string message, string message2);
    }
}
