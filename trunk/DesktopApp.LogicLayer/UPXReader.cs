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
                if (UPXReader.IsUPXCharacterElement(xmlr))
                {
                    xxx magical place. continue working on this Method:
                    characterList.Add(UPXReader.ReadUPXElementContentAsCharacter(xmlr));
                }
            }
        }


        public static bool IsUPXCharacterElement(this XmlTextReader xmlr)
        {
            if ((xmlr.NodeType == XmlNodeType.Element) &&
                ((xmlr.Name == "hLevel") && 
                (xmlr.HasAttributes)))
                {
                    //read attributes
                    for (int i = 0; i < xmlr.AttributeCount; i++)
                    {
                        xmlr.MoveToNextAttribute();
                        if ("level" == xmlr.Name)
                            if ("character" == xmlr.Value)
                            {
                                xmlr.MoveToElement();
                                return true;
                            }
                    }
                }
            return false;
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
            if (UPXReader.IsUPXCharacterElement(xmlr))
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

                    xmlr.ReadStartElement("hLevel"); //now moving to label
                    ReadUPXElementContentAsLabel(xmlr);

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
                                 //   c.StrokeList.Add(xmlr.ReadElementContentAsStroke());
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

        private static void ReadUPXElementContentAsLabel(XmlTextReader xmlr)
        {
            throw new NotImplementedException();
        }
    }
}
