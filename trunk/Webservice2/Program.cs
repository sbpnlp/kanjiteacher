using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSvc = Kanji.KanjiService;

namespace Kanji.Webservice2
{
    class Program
    {
        static void Main(string[] args)
        {
            KSvc.Service serv = new KSvc.Service();

            //show meta data
            bool showmetadata = true;

            //or don't show metadata
            //showmetadata = false;

            serv.ShowMetaData = showmetadata;

            serv.Run(new DummyObserver());
        }
    }
}
