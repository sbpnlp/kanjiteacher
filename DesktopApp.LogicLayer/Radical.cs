using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;
using System.Security.Cryptography;

namespace Kanji.DesktopApp.LogicLayer
{
    /// <summary>
    /// Represents a radical
    /// </summary>
    public class Radical : IRadical
    {
        #region Fields / Properties
        public List<Stroke> StrokeList = new List<Stroke>();
        public List<List<Point>> ActivePoints;
        public string ID { get; set; }
        public string Value { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Radical"/> class.
        /// </summary>
        public Radical() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Radical"/> class.
        /// </summary>
        /// <param name="activePoints">The active points.</param>
        public Radical(List<List<Point>> activePoints)
        {
            ActivePoints = activePoints;

            for (int i = 0; i < ActivePoints.Count; i++)
            {
                Stroke s = new Stroke(ActivePoints[i]);
                StrokeList.Add(s);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Radical"/> class.
        /// </summary>
        /// <param name="strokelist">The strokelist.</param>
        public Radical(List<Stroke> strokelist)
        {
            StrokeList = strokelist;
        }
        #endregion

        #region IRadical Members

        /// <summary>
        /// Calculates the matching score between two a sequence of IStrokes
        /// and a Radical
        /// </summary>
        /// <param name="strokeSequence">The stroke sequence.</param>
        /// <param name="radicalmatcher">An instance of a radicalmatcher class.</param>
        /// <returns>double: matching value</returns>
        public double MatchingScore(List<IStroke> strokeSequence, IRadicalMatcher radicalmatcher)
        {
            return radicalmatcher.Match(this, strokeSequence);
        }

        /// <summary>
        /// Creates an Hash of the IRadical point sequence.
        /// </summary>
        /// <param name="withTime">if set to <c>true</c> compute
        /// the hash including the time information of the points.</param>
        /// <returns>A byte array with the hash.</returns>
        public byte[] Hash(bool withTime)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(ToByteArray(withTime));
        }

        /// <summary>
        /// Creates a byte array from the points coordinates within the Radical
        /// </summary>
        /// <param name="withTime">if set to <c>true</c> 
        /// include the timestamp information of the points.</param>
        /// <returns>A byte array of the point coordinates.</returns>
        public byte[] ToByteArray(bool withTime)
        {
            int length = 0;
            List<byte[]> byteList = new List<byte[]>(StrokeList.Count);
            foreach (Stroke s in StrokeList)
            {
                byte[] temp = s.ToByteArray(withTime);
                length += temp.Length;
                byteList.Add(temp);
            }
            byte[] r = new byte[length];
            int startposition = 0;

            for (int i = 0; (i < byteList.Count)&&(startposition<r.Length); i++)
            {
                byteList[i].CopyTo(r, startposition);
                startposition += byteList[i].Length;
            }
            return r;
        }

        #endregion
    }
}
