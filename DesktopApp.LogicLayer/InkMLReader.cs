using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Kanji.DesktopApp.LogicLayer.Helpers;

namespace Kanji.DesktopApp.LogicLayer
{
    /// <summary>
    /// A class that provides methods to parse an inkML file
    /// </summary>
    public static class InkMLReader
    {
        //xxx get path from some config file...
        static string path = "C:\\Diplom\\kanjiteacher\\data";

        /// <summary>
        /// Reads an InkML file and creates a number list strokes from the traces.
        /// </summary>
        /// <param name="pathToInkMLFile">The path to ink ML file.</param>
        /// <returns></returns>
        public static List<Stroke> ReadInkMLFile(string pathToInkMLFile)
        {
            List<Stroke> r = new List<Stroke>(10);
            FileStream inputstream = 
                new FileStream(
                    path + Path.DirectorySeparatorChar + pathToInkMLFile, 
                    FileMode.Open);

            XmlTextReader xmlr = new XmlTextReader(inputstream);
            
            
            while ((!UPXReader.IsEndElementTypeWithName(xmlr, "ink")))
            {
                if (UPXReader.IsElementTypeWithName(xmlr, "trace"))
                {
                    Console.WriteLine(XmlTools.ReadIDAttribute(xmlr));
                    r.Add(new Stroke(
                        Stroke.PointListFromInkMLTrace(
                            xmlr.ReadElementContentAsString())));
                }
                else xmlr.Read(); //overread element
            }

            xmlr.Close();
            inputstream.Close();

            return r;
        }

        /// <summary>
        /// Reads an InkML trace.
        /// </summary>
        /// <param name="traceRef">The trace reference to read.</param>
        /// <returns></returns>
        public static List<Point> ReadInkMLTrace(string traceRef)
        {
            List<Point> retval = null;
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
                    (ID == XmlTools.ReadIDAttribute(xmlr)))
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
    }
}
