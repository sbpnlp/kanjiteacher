using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.DesktopApp.LogicLayer
{
    /// <summary>
    /// Representation of a character
    /// </summary>
    public class Character : ICharacter
    {
        public List<Radical> RadicalList = new List<Radical>();
        public List<List<Point>> ActivePoints;
        public IController AppController;

        public Character(List<List<Point>> activePoints, IController controller)
        {
            ActivePoints = activePoints;
            AppController = controller;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        /// <param name="activePoints">The active points.</param>
        public Character(List<List<Point>> activePoints)
        {
            ActivePoints = activePoints;
        }
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

                c.AppController.ReceiveCharacterModels(new List<ICharacter>() { c });
            }
            
        }
    }
}
