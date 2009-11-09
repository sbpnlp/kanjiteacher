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
            bool b = true;
            serv.Run((object)b);
        }
    }
}
