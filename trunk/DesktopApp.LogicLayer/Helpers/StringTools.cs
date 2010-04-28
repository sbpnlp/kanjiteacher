using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanji.DesktopApp.LogicLayer.Helpers
{
    public static class StringTools
    {
        //public static string AddZeros(int s)
        //{
        //    //besser: mit 10er-potenzen.
        //    string retval = string.Empty;
        //    if (s < 10000)
        //        retval += "0";
        //    if (s < 1000)
        //        retval += "0";
        //    if (s < 100)
        //        retval += "0";
        //    if (s < 10)
        //        retval += "0";
        //    retval += s.ToString();
        //    return retval;
        //}

        public static string AddZeros(int s, int power)
        {
            string retval = string.Empty;
             
            for(int i = power; i>1; i--)
            {
                if (Math.Pow((double)10, (double)i - 1) >= (double)s)
                    retval += "0";
                else break;
            }
            return retval + s.ToString();
        }

        public static string AddZeros(long s, int power)
        {
            string retval = string.Empty;
            while(Math.Pow((double)10, (double)--power) >= (double)s)
                    retval += "0";
            return retval + s.ToString();
        }

        public static string AddZeros(int s, long power)
        {
            string retval = string.Empty;
            long ls = (long)Math.Floor(Math.Log10((double)s));
            power -= ls;
            while (--power >0)
                retval += "0";
            return retval + s.ToString();
        }

        public static string AddZeros(long s, long power)
        {
            string retval = string.Empty;
            int len = s.ToString().Length;
            for (int i = 0; i < power-len; i++)
                retval += "0";
            return retval + s.ToString();
        }


        
    }
}
