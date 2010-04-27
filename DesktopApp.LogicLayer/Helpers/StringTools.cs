using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanji.DesktopApp.LogicLayer.Helpers
{
    public static class StringTools
    {
        public static string AddZeros(int s)
        {
            //besser: mit 10er-potenzen.
            string retval = string.Empty;
            if (s < 10000)
                retval += "0";
            if (s < 1000)
                retval += "0";
            if (s < 100)
                retval += "0";
            if (s < 10)
                retval += "0";
            retval += s.ToString();
            return retval;
        }
    }
}
