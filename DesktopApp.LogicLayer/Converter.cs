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
            output.LoadXml("<ink></ink>");
            
            XmlTextReader xmlr = new XmlTextReader(inputstream);
            List<Stroke> strokeList = new List<Stroke>();

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
                            strokeList.Add(xmlr.ReadElementContentAsStroke());
                            break;
                    }
                }
            }

//            output.DocumentElement = output.CreateElement("Character");

            for (int i = 0; i < strokeList.Count; i++)
            {
                strokeList[i].ToXmlNode(output, output.DocumentElement);
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

            outputstream.Write(
                Encoding.UTF8.GetBytes(output.OuterXml), 
                0, 
                Encoding.UTF8.GetByteCount(output.OuterXml));
        }

        private static Stroke ReadElementContentAsStroke(this XmlTextReader xmlr)
        {
            if (xmlr.NodeType == XmlNodeType.Element)
            {
                if (xmlr.Name == "stroke")
                {
                    Stroke s = new Stroke();
                    xmlr.ReadStartElement("stroke"); //now moving to point
                    while (xmlr.NodeType != XmlNodeType.EndElement)
                        s.AllPoints.Add(xmlr.ReadElementContentAsPoint());
                    xmlr.ReadEndElement();
                    return s;
                }
                else throw new Exception(string.Format("Not the correct element. This was a {0}-tag", xmlr.Name));
            }
            else throw new Exception("Not even an element type");
        }

        private static Point ReadElementContentAsPoint(this XmlTextReader xmlr) 
        {
            if (xmlr.NodeType == XmlNodeType.Element)
            {
                if (xmlr.Name == "point")
                {
                    xmlr.ReadStartElement("point"); //now moving to time
                    DateTime initialTs = new DateTime(xmlr.ReadElementContentAsLong()); //reading time
                    int x = xmlr.ReadElementContentAsInt();
                    int y = xmlr.ReadElementContentAsInt();
                    xmlr.ReadEndElement();
                    return new Point(x, y, initialTs);
                }else throw new Exception(string.Format("Not the correct element. This was a {0}-tag", xmlr.Name));
            }
            else throw new Exception("Not even an element type");

            
        }

        private static void HandleGeneralInfo(XmlDocument output, XmlTextReader xmlr)
        {
            //do nothing or create file with correct name
        }
    }
}
