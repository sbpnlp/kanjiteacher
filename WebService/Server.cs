using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Net;
using KSvc = Kanji.KanjiService;
using System.Threading;

namespace Kanji.Webservice
{
    class Server
    {
        static void Main(string[] args)
        {
            KSvc.Service serv = new KSvc.Service();
            bool b = true;
            serv.Run((object)b);
        }
    }
}
