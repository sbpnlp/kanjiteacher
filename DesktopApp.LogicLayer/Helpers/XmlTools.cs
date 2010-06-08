using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Kanji.DesktopApp.LogicLayer.Helpers
{
    public static class XmlTools
    {
        /// <summary>
        /// Reads the ID attribute of a tag.
        /// </summary>
        /// <param name="xmlr">The XMLR.</param>
        /// <returns>A string with the ID</returns>
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
    }
}
