using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace Kanji.DesktopApp.LogicLayer
{
    /// <summary>
    /// A class that provides methods to parse an inkML file
    /// </summary>
    public static class InkMLReader
    {
        public static List<Point> ReadInkMLTrace(string traceRef)
        {
            List<Point> retval = null;

            //xxx get path from some config file...
            string path = "C:\\Diplom\\kanjiteacher\\data";
            string[] pathXmlFile = traceRef.Split('#');
            string filename = path + Path.DirectorySeparatorChar + pathXmlFile[0];
            string ID = pathXmlFile[1];

            FileStream inputstream = new FileStream(filename, FileMode.Open);
            XmlTextReader xmlr = new XmlTextReader(inputstream);

            //var elements = XElement.Load(path + Path.DirectorySeparatorChar + pathXmlFile[0]);

            //var myTrace = from a in elements.Elements()
            //              where a.Attribute("id").Value.Equals(pathXmlFile[1])
            //              select a.Element("trace").Value;

            string trace = string.Empty;
            while (xmlr.Read() && (!UPXReader.IsEndElementTypeWithName(xmlr, "ink")))
            {
                if ((UPXReader.IsElementTypeWithName(xmlr, "trace"))
                    &&
                    (ID == UPXReader.ReadIDAttribute(xmlr)))
                {
                    trace = xmlr.ReadElementContentAsString();
                    retval = Stroke.PointListFromInkMLTrace(trace);
                    break; //finished reading, now splitting the trace
                }
            }

            xmlr.Close();
            inputstream.Close();
         
            return retval;
        }

        private static string ReadIDAttribute(this XmlTextReader xmlr)
        {
            string retval = string.Empty;

            for (int i = 0; i < xmlr.AttributeCount; i++)
            {
                xmlr.MoveToNextAttribute();
                if (xmlr.Name.ToLowerInvariant() == "id".ToLowerInvariant())
                    retval = xmlr.Value;
            }
            return retval;
        }
    }
}
