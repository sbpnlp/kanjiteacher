using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace Kanji.DesktopApp.LogicLayer
{
    public static class Converter
    {
        public static void ConvertInputToFinalFormat(Stream inputstream, Stream outputstream)
        {
            XmlDocument output = new XmlDocument();
//            XmlNode node = output.CreateNode(
//            output.CreateDocumentType("kanji

            XmlElement root = output.DocumentElement;
            
            XmlTextReader xmlr = new XmlTextReader(inputstream);
            bool firstStrokedone = false;
            long ts = 0;

            while (xmlr.Read())
            {
                if (xmlr.NodeType == XmlNodeType.Element)
                {
                    switch (xmlr.Name)
                    {
                        case "GeneralInfo":
                            HandleGeneralInfo(output, xmlr);
                            break;
                        case "stroke":
                            if (firstStrokedone)
                                HandleStroke(root, xmlr);
                            else HandleFirstStroke(root, xmlr, out ts);
                            break;
                    }
                }
            }

            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml("<book>" +
            //            "  <title>Oberon's Legacy</title>" +
            //            "  <price>5.95</price>" +
            //            "</book>");

            //// Create a new element node.
            //XmlNode newElem = doc.CreateNode("element", "pages", "");
            //newElem.InnerText = "290";
            //XmlNode newElem2 = doc.CreateElement("pages2");
            //newElem2.InnerText = "333";

            //Console.WriteLine("Add the new element to the document...");
            //XmlElement root = doc.DocumentElement;
            //root.AppendChild(newElem);
            //root.AppendChild(newElem2);
            //XmlNode newElem3 = doc.CreateElement("testpages");
            //newElem3.InnerText = "textdrinnen";
            //root.AppendChild(newElem3);

            //Console.WriteLine("Display the modified XML document...");
            //Console.WriteLine(doc.OuterXml);

            outputstream.Write(Encoding.UTF8.GetBytes(output.OuterXml), 0, Encoding.UTF8.GetByteCount(output.OuterXml));
        }

        private static void HandleFirstStroke(XmlElement root, XmlTextReader xmlr, out long ts)
        {
            //continue reading 
            //take first point element and get time information out of it
            //create timestamp node
            //create traceFormat node
            //crate trace from the point nodes
            ts = 0;
        }

        private static void HandleStroke(XmlElement root, XmlTextReader xmlr)
        {

            
        }

        private static void HandleGeneralInfo(XmlDocument output, XmlTextReader xmlr)
        {
            //do nothing or create file with correct name
        }
    }
}
