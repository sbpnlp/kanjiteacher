using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Kanji.DesktopApp.Interfaces
{
    public interface IPoint
    {
        /// <summary>
        /// Creates an XML string representation of the IPoint
        /// </summary>
        /// <returns>An XML string</returns>
        string ToXmlString();

        /// <summary>
        /// Creates a hash from the IPoint coordinates.
        /// </summary>
        /// <param name="withTime">if set to <c>true</c> compute 
        /// the hash including the time information.</param>
        /// <returns>A byte array with the hash.</returns>
        byte[] Hash(bool withTime);  
    }
}
