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
        public static void ReadUPXFile(Stream inputstream)
        {
            XmlDocument output = null;
            //XmlTextReader xmlr = new XmlTextReader(inputstream);
            XmlDocument input = new XmlDocument();
            input.Load(inputstream);
            XmlElement root = input.DocumentElement;


            foreach (XmlNode node in input.GetElementsByTagName("hLevel"))
            {
                //if it is a character, read it
                if (IsUPXhLevelCharacter(node))
                {

                }
            }


           
            //List<Stroke> strokeList = new List<Stroke>();
            //List<Character> characterList = new List<Character>();

            //string filename = string.Empty;

            //while (xmlr.Read())
            //{
            //    if (UPXReader.IsUPXCharacterElement(xmlr))
            //    {
            //        characterList.Add(UPXReader.ReadUPXElementContentAsCharacter(xmlr));
            //    }
            //}
        }

        public static void ParseUPXFile(Stream inputstream)
        {
            XmlDocument output = null;
            XmlTextReader xmlr = new XmlTextReader(inputstream);
            List<Character> characterList = new List<Character>();
            string filename = string.Empty;

            while (xmlr.Read())
            {
                if (UPXReader.IsUPXhLevelCharacter(xmlr))
                {
                    characterList.Add(UPXReader.ReadUPXElementContentAsCharacter(xmlr));
                }
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
            if (IsElementTypeWithName(xmlr, "hLevel"))
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

        public static bool IsElementTypeWithName(this XmlTextReader xmlr, string name)
        {
            return ((xmlr.NodeType == XmlNodeType.Element) &&
                ((xmlr.Name.ToLowerInvariant() == name.ToLowerInvariant()) && 
                (xmlr.HasAttributes)));
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

                    //read attributes, especially ID
                    for (int i = 0; i < xmlr.AttributeCount; i++)
                    {
                        xmlr.MoveToNextAttribute();
                        if (xmlr.Name.ToLowerInvariant() == "id")
                            c.SHKK = xmlr.Value; //maybe strip the "CHARACTER_" from the int value
                    }

                    //now moving to label and reading it
                    //then the radicals
                    while (xmlr.Read())
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
                                        rTemp = ReadUPXElementContentAsRadical(xmlr);
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

                //read attributes, especially ID
                for (int i = 0; i < xmlr.AttributeCount; i++)
                {
                    xmlr.MoveToNextAttribute();
                    if (xmlr.Name.ToLowerInvariant() == "id")
                        s.ID = xmlr.Value; //maybe strip the "STROKE_" from the ID
                }

                //now moving to label and reading it
                //then the strokes
                while (xmlr.Read())
                {
                    if (xmlr.NodeType == XmlNodeType.Element)
                    {
                        switch (xmlr.Name)
                        {
                            case "label":
                                s.Value = ReadUPXElementContentAsLabel(xmlr, s.ID);
                                break;
                            case "hwTraces":
                                if (IsUPXhLevelStroke(xmlr))
                                    s.AllPoints = ReadUPXElementContentAsPointList(xmlr);
                                break;
                        }
                    }
                }
                return s;
            }
            else throw new Exception(string.Format("Not the correct element. This was a {0}-tag.", xmlr.Name));
        }

        private static List<Point> ReadUPXElementContentAsPointList(XmlTextReader xmlr)
        {
/*
 * in here, read
 * hwTraces element, i.e. use the inkml:traceview elements to find the
 * appropriate stroke data.
 * use inkml reader to read the actual inkml file
 */
            return InkMLReader.ReadInkMLTrace("findthefilename", 42);
        }

        private static Radical ReadUPXElementContentAsRadical(XmlTextReader xmlr)
        {
            if (UPXReader.IsUPXhLevelRadical(xmlr) && (xmlr.HasAttributes))
            {
                Radical r = new Radical();

                //read attributes, especially ID
                for (int i = 0; i < xmlr.AttributeCount; i++)
                {
                    xmlr.MoveToNextAttribute();
                    if (xmlr.Name.ToLowerInvariant() == "id")
                        r.ID = xmlr.Value; //maybe strip the "RADICAL_" from the ID
                }

                //now moving to label and reading it
                //then the strokes
                while (xmlr.Read())
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
                                    sTemp = ReadUPXElementContentAsStroke(xmlr);
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
                while ((! ((xmlr.NodeType == XmlNodeType.EndElement) && (xmlr.Name == "label"))) &&
                    xmlr.Read())
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
