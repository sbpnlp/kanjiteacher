using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Kanji.DesktopApp.LogicLayer.Helpers;

namespace Kanji.DesktopApp.LogicLayer
{
    public static class Converter
    {
        public static void ConvertInputToFinalFormat(Stream inputstream)
        {
            XmlDocument output = null; 
            
            XmlTextReader xmlr = new XmlTextReader(inputstream);
            List<Stroke> strokeList = new List<Stroke>();
            List<Character> characterList = new List<Character>();

            string filename = string.Empty;

            while (xmlr.Read())
            {
                if ((xmlr.NodeType == XmlNodeType.Element) &&
                    (xmlr.Name == "character"))
                {
                    characterList.Add(xmlr.ReadElementContentAsCharacter());
                }
            }

            foreach (Character c in characterList)
            {
                output = new XmlDocument();
                output.LoadXml("<ink></ink>");

                c.ToXmlNode(output, output.DocumentElement);

                //for (int i = 0; i < c.StrokeList.Count; i++)
                //{
                //    c.StrokeList[i].ToXmlNode(output, output.DocumentElement);
                //}

                DirectoryInfo di = Directory.CreateDirectory("C:\\Diplom\\kanjiteacher\\data");
                filename = "char" + StringTools.AddZeros(Int32.Parse(c.SHKK), 5) + ".inkml";
                StreamWriter sw = new StreamWriter(di.FullName + Path.DirectorySeparatorChar + filename);
                sw.Write(output.OuterXml);
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// Reads the element content as a character from intermediate format.
        /// </summary>
        /// <param name="xmlr">The XMLR.</param>
        /// <returns></returns>
        private static Character ReadElementContentAsCharacter(this XmlTextReader xmlr)
        {
            if (xmlr.NodeType == XmlNodeType.Element)
            {
                if (xmlr.Name == "character")
                {
                    Character c = new Character();

                    xmlr.ReadStartElement("character"); //now moving to generalinfo

                    //now we should be at the first stroke

                    while (xmlr.NodeType != XmlNodeType.EndElement)
                    {
                        if (xmlr.NodeType == XmlNodeType.Element)
                        {
                            switch (xmlr.Name)
                            {
                                case "GeneralInfo":
                                    c.SHKK = xmlr.ReadElementContentAsString();// +".inkml";
                                    break;

                                case "stroke":
                                    c.StrokeList.Add(xmlr.ReadElementContentAsStroke());
                                    c.StrokeList[c.StrokeList.Count - 1].ID = StringTools.AddZeros(c.StrokeList.Count, 4);
                                    break;
                            }
                        }
                        else xmlr.Read(); //move on
                    }
                    xmlr.ReadEndElement();
                    return c;
                }
                else throw new Exception(string.Format("Not the correct element. This was a {0}-tag", xmlr.Name));
            }
            else throw new Exception("Not even an element type");
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
    }
}
