using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;
using System.Xml;
using System.Security.Cryptography;

namespace Kanji.DesktopApp.LogicLayer
{
    /// <summary>
    /// Representation of a character
    /// </summary>
    public class Character : ICharacter
    {
        #region Fields / Properties
        public List<Radical> RadicalList 
        {
            get { return _radicallist; }

            set
            {
                _radicallist = value;
                //change strokelist according to combination
                //of stroke lists of the radicals
                List<Stroke> temp = new List<Stroke>();
                foreach (Radical r in RadicalList)
                {
                    temp.AddRange(r.StrokeList);
                }
                _strokelist = temp;
            }
        }
        private List<Radical> _radicallist;
        public List<List<Point>> ActivePoints { get; set; }
        public IController AppController { get; set; }
        public List<Stroke> StrokeList 
        {
            get
            {
                if ((_strokelist == null) || (_strokelist.Count == 0))
                {
                    _strokelist = new List<Stroke>();
                    foreach (Radical r in RadicalList)
                    {
                        _strokelist.AddRange(r.StrokeList);
                    }
                }
                return _strokelist;
            }

            set { _strokelist = value; }
        }
        private List<Stroke> _strokelist;
        public string SHKK { get; set; }
        public string ID { get { return SHKK; } set { SHKK = value; } }
        public string Value { get; set; }

        #endregion

        #region Constructors
        public Character()
        {
            RadicalList = new List<Radical>();
            ActivePoints = new List<List<Point>>();
            AppController = null;
            _strokelist = new List<Stroke>();
        }

        //public Character(List<List<Point>> activePoints, IController controller)
        //{//xxx this one needs work. what's it's use, anyway?
        //    ActivePoints = activePoints;
        //    AppController = controller;
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        /// <param name="activePoints">The active points.</param>
        public Character(List<List<Point>> activePoints)
        {
            ActivePoints = activePoints;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        /// <param name="strokelist">The strokelist.</param>
        public Character(List<Stroke> strokelist)
        {
            StrokeList = strokelist;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        /// <param name="radicallist">The radicallist.</param>
        public Character(List<Radical> radicallist)
        {
            RadicalList = radicallist;
        }

        #endregion

        #region Public methods
        /// <summary>
        /// Fills the character class from a point list.
        /// </summary>
        /// <param name="ActivePoints">The active points.</param>
        public static void CreateFromPointList(object o)
        {
            if (! (o is Character))
            {
                throw new Exception("Incorrect type");
            }
            else
            {
                Character c = o as Character;
                for (int i = 0; i < c.ActivePoints.Count-1; i++)
                {
                    // find something smarter to split 
                    // how do you know what point belongs to what character?
                    // if say there's a two stroke radical, how does that
                    // claim those to strokes for itself and prevents others from taking it?
                    // maybe split the list at any range and make it a list of lists of radicals?
                    // how to deal with multiple splits?
                    // the split should be based on some assumption, like for instance:
                    // I already found a radical with a high confidence
                    int from, to;
                    from = 0;
                    to = i + 1;
                    Radical r = new Radical(c.ActivePoints.GetRange(from, to));
                    c.RadicalList.Add(r);
                }

//xxx                 c.AppController.ReceiveCharacterModels(new List<ICharacter>() { c });
            }
            
        }

        public double InputMatching(List<Stroke> strokelist) { return 0; }

        /// <summary>
        /// Creates an XML node from the current character.
        /// </summary>
        /// <param name="doc">The XML document.</param>
        /// <param name="attachTo">The element that the strokes should be attached to.</param>
        public void ToXmlNode(XmlDocument doc, XmlElement attachTo)
        {
            StrokeList[0].ToXmlNode(doc, attachTo);
            
            for (int i = 1; i < StrokeList.Count; i++)
            {
                StrokeList[i].ToXmlNode(doc, attachTo, StrokeList[0].BeginPoint.Time);
            }
        }
        #endregion

        #region ICharacter Members

        /// <summary>
        /// Calculates the matching score between two a sequence of IStrokes
        /// and a Radical
        /// </summary>
        /// <param name="radicalSequence"></param>
        /// <param name="charactermatcher"></param>
        /// <returns>double: matching value</returns>
        public double MatchingScore(List<IRadical> radicalSequence, ICharacterMatcher charactermatcher)
        {
            return charactermatcher.Match(this, radicalSequence);
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

            for (int i = 0; (i < byteList.Count) && (startposition < r.Length); i++)
            {
                byteList[i].CopyTo(r, startposition);
                startposition += byteList[i].Length;
            }
            return r;
        }

        #endregion
    }
}
