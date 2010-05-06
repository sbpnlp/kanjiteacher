using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Kanji.DesktopApp.LogicLayer.Helpers;
using System.IO;

namespace Kanji.DesktopApp.LogicLayer
{
    public static class UPXReader
    {
        public static void ParseUPXFile(Stream inputstream)
        {
            XmlDocument output = null;
            XmlTextReader xmlr = new XmlTextReader(inputstream);
            List<Character> characterList = new List<Character>();
            string filename = string.Empty;

            //if we're hitting and end element that is a hLevel stop reading 
            //the ones from the strokes should be read within ReadUPXElementContentAsStroke
            while (xmlr.Read() && (! IsEndElementTypeWithName(xmlr, "hwData")))
            {
                if (UPXReader.IsUPXhLevelCharacter(xmlr))
                {
                    characterList.Add(UPXReader.ReadUPXElementContentAsCharacter(xmlr));
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
                filename = "char" + c.SHKK + ".INOUT.inkml";
                StreamWriter sw = new StreamWriter(di.FullName + Path.DirectorySeparatorChar + filename);
                sw.Write(output.OuterXml);
                sw.Flush();
                sw.Close();
            }
        }

        public static bool IsUPXhLevelCharacter(this XmlTextReader xmlr)
        {
            return IsUPXhLevelElement(xmlr, "character");
        }

        public static bool IsUPXhLevelRadical(this XmlTextReader xmlr)
        {
            return IsUPXhLevelElement(xmlr, "radical");
        }

        public static bool IsUPXhLevelStroke(this XmlTextReader xmlr)
        {
            return IsUPXhLevelElement(xmlr, "stroke");
        }

        public static bool IsUPXhLevelElement(this XmlTextReader xmlr, string level)
        {
            if (IsElementTypeWithNameAndAttributes(xmlr, "hLevel"))
            {
                //read attributes
                for (int i = 0; i < xmlr.AttributeCount; i++)
                {
                    xmlr.MoveToNextAttribute();
                    if ("level".ToLowerInvariant() == xmlr.Name.ToLowerInvariant())
                        if (level.ToLowerInvariant() == xmlr.Value.ToLowerInvariant())
                        {
                            xmlr.MoveToElement(); //return to element node
                            return true;
                        }
                }
            }
            return false;
        }

        public static bool IsElementTypeWithNameAndAttributes(this XmlTextReader xmlr, string name)
        {
            return (IsElementTypeWithName(xmlr, name) && (xmlr.HasAttributes));
        }

        public static bool IsElementTypeWithName(this XmlTextReader xmlr, string name)
        {
            return ((xmlr.NodeType == XmlNodeType.Element) &&
                (xmlr.Name.ToLowerInvariant() == name.ToLowerInvariant()));
        }

        public static bool IsEndElementTypeWithName(this XmlTextReader xmlr, string name)
        {
            return ((xmlr.NodeType == XmlNodeType.EndElement) &&
                (xmlr.Name.ToLowerInvariant() == name.ToLowerInvariant()));
        }

        public static bool IsUPXhLevelCharacter(this XmlNode node)
        {
            return IsUPXhLevelElement(node, "character");
        }

        public static bool IsUPXhLevelRadical(this XmlNode node)
        {
            return IsUPXhLevelElement(node, "radical");
        }

        public static bool IsUPXhLevelStroke(this XmlNode node)
        {
            return IsUPXhLevelElement(node, "stroke");
        }

        public static bool IsUPXhLevelElement(this XmlNode node, string level)
        {
            if ((node.NodeType == XmlNodeType.Element))
            {
                XmlElement nodeElement = node as XmlElement;
                if ((nodeElement.Name == "hLevel") && 
                    (nodeElement.HasAttributes))
                {
                    //read attributes
                    foreach (XmlAttribute attribute in nodeElement.Attributes)
                    {
                        if ((attribute.Name.ToLowerInvariant() == "level".ToLowerInvariant())
                            &&
                            (attribute.Value.ToLowerInvariant() == level.ToLowerInvariant()))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        
        /// <summary>
        /// Reads the upx element content as a character.
        /// </summary>
        /// <param name="xmlr">The XMLR.</param>
        /// <returns></returns>
        public static Character ReadUPXElementContentAsCharacter(this XmlTextReader xmlr)
        {
            if (UPXReader.IsUPXhLevelCharacter(xmlr))
            {
                if ((xmlr.Name == "hLevel") && (xmlr.HasAttributes))
                {
                    Character c = new Character();

                    c.SHKK = ReadIDAttribute(xmlr);

                    //now moving to label and reading it
                    //then the radicals
                    //if we're hitting and end element that is an hLevel stop reading 
                    //the ones from the Radicals should be eaten within ReadUPXElementContentAsRadical
                    while (xmlr.Read() && (!IsEndElementTypeWithName(xmlr, "hLevel")))
                    {
                        if (xmlr.NodeType == XmlNodeType.Element)
                        {
                            Radical rTemp = null;

                            switch (xmlr.Name)
                            {
                                case "label":
                                    c.Value = ReadUPXElementContentAsLabel(xmlr, c.SHKK);
                                    break;
                                case "hLevel":
                                    if (IsUPXhLevelRadical(xmlr))
                                    {
                                        rTemp = ReadUPXElementContentAsRadical(xmlr);
                                        c.RadicalList.Add(rTemp);
                                    }
                                    break;
                            }
                        }
                    }
                    
                    return c;
                }
                else throw new Exception(string.Format("Not the correct element. This was a {0}-tag", xmlr.Name));
            }
            else throw new Exception("Not even an element type");
        }

        private static Stroke ReadUPXElementContentAsStroke(XmlTextReader xmlr)
        {
            if (UPXReader.IsUPXhLevelStroke(xmlr) && (xmlr.HasAttributes))
            {
                Stroke s = new Stroke();

                s.ID = ReadIDAttribute(xmlr);

                //now moving to label and reading it
                //then the strokes
                //if we're hitting and end element that is a hLevel stop reading 
                //when hLevel end element is hit, stroke is finished
                while (xmlr.Read() && (!IsEndElementTypeWithName(xmlr, "hLevel")))
                {
                    if (xmlr.NodeType == XmlNodeType.Element)
                    {
                        switch (xmlr.Name)
                        {
                            case "label":
                                s.Value = ReadUPXElementContentAsLabel(xmlr, s.ID);
                                break;
                            case "hwTraces":
                                if (IsElementTypeWithName(xmlr, "hwtraces"))
                                    s.AllPoints = ReadUPXElementContentAsPointList(xmlr);
                                break;
                        }
                    }
                }
                return s;
            }
            else throw new Exception(string.Format("Not the correct element. This was a {0}-tag.", xmlr.Name));
        }

        public static string ReadIDAttribute(this XmlTextReader xmlr)
        {
            string retval = string.Empty;
            
            for (int i = 0; i < xmlr.AttributeCount; i++)
            {
                xmlr.MoveToNextAttribute();
                if (xmlr.Name.ToLowerInvariant() == "id".ToLowerInvariant())
                    retval = xmlr.Value; 
            }
            xmlr.MoveToElement();
            return retval;
        }

        private static List<Point> ReadUPXElementContentAsPointList(XmlTextReader xmlr)
        {
            List<Point> retval = null;

            if (IsElementTypeWithName(xmlr, "hwTraces"))
            {
                //now moving to traceview element and reading it
                //if we're hitting and end element that is a hwTraces stop reading 
                //the ones from the inkml:traceview should be eaten within InkMLReader.ReadInkMLTrace
                while (xmlr.Read() && (!IsEndElementTypeWithName(xmlr, "hwTraces")))
                {
                    if ((xmlr.NodeType == XmlNodeType.Element) && (xmlr.Name == "inkml:traceView"))
                    {
                        if (IsElementTypeWithName(xmlr, "inkml:traceView"))
                        {
                            string traceRef = string.Empty;
                            traceRef = xmlr.GetAttribute("traceRef");
                            if (traceRef != string.Empty)
                                retval = InkMLReader.ReadInkMLTrace(traceRef);
                        }
                    }
                }
            }
            return retval;
        }

        private static Radical ReadUPXElementContentAsRadical(XmlTextReader xmlr)
        {
            if (UPXReader.IsUPXhLevelRadical(xmlr) && (xmlr.HasAttributes))
            {
                Radical r = new Radical();

                r.ID = ReadIDAttribute(xmlr);

                //now moving to label and reading it
                //then the strokes
                //if we're hitting and end element that is a hLevel stop reading 
                //the ones from the strokes should be eaten within ReadUPXElementContentAsStroke
                while (xmlr.Read() && (!IsEndElementTypeWithName(xmlr, "hLevel")))
                {
                    if (xmlr.NodeType == XmlNodeType.Element)
                    {
                        Stroke sTemp = null;
                        switch (xmlr.Name)
                        {
                            case "label":
                                r.Value = ReadUPXElementContentAsLabel(xmlr, r.ID);
                                break;
                            case "hLevel":
                                if (IsUPXhLevelStroke(xmlr))
                                {
                                    sTemp = ReadUPXElementContentAsStroke(xmlr);
                                    r.StrokeList.Add(sTemp);
                                }
                                break;
                        }
                    }
                }
                return r;
            }
            else throw new Exception(string.Format("Not the correct element. This was a {0}-tag.", xmlr.Name));
        }

        private static string ReadUPXElementContentAsLabel(XmlTextReader xmlr, string ID)
        {
            string retval = string.Empty;
            if ((xmlr.Name == "label") && (xmlr.NodeType == XmlNodeType.Element) && (xmlr.GetAttribute("id").ToLowerInvariant() == ID.ToLowerInvariant()))
            {
                //if we're hitting and end element that is a label stop reading
                while (xmlr.Read() && (!IsEndElementTypeWithName(xmlr, "label")))
                {
                    if (xmlr.Name == "alternate")
                    {
                        retval = xmlr.ReadElementContentAsString();
                    }
                }
                xmlr.ReadEndElement(); //read label end element
            }
            return retval;
        }
    }
}
