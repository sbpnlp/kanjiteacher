using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logics = Kanji.DesktopApp.LogicLayer;
using System.Threading;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.InputArea.WinFormGUI
{
/// <summary>
    /// Input controller for input coming from a non-mobil device.
    /// </summary>
    public class WinClientCommunication : IController
    {
        public IControlled View { get; set; }

        #region IController Members

        /// <summary>
        /// Receives the point list from the input device.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Kanji.DesktopApp.LogicLayer.MouseInputEventArgs"/> instance containing the event data.</param>
        public void ReceivePointList(object sender, IMouseInputEventArgs e)
        {
            // Create stroke from ActivePoints
            // Store Stroke in stroke list
            // take current stroke list and try to build radicals in different variants
            // store radicals
            // take current radicals and try to build character from them
            // store characters

//            Logics.Character c = new Logics.Character(((Logics.MouseInputEventArgs)e).ActivePoints);
//            c.AppController = this;
            // Thread t = new Thread(Logics.Character.CreateFromPointList);
            // t.Start(c);
//            Logics.Stroke s = new Logics.Stroke(((Logics.MouseInputEventArgs)e).ActivePoints);
//            Logics.Character.CreateFromPointList(c);

            //why giving anything back to the inputarea??? View.ReceivePointList((IStroke)s, (IBoundingBox)bb);
            //giving it to the "View" is correct, but the inputarea is not the "View"!
        }

        /// <summary>
        /// Receives the character models. When the characters have been built up from
        /// the point lists, this method can be used to transmit those models to
        /// the controller.
        /// </summary>
        /// <param name="cModels">The character models.</param>
        public void ReceiveCharacterModels(List<ICharacter> cModels)
        {
            //again: transmitting the character models to the view is correct,
            //but the inputarea is not the view, rather send it to the desktopapp.winformgui
            //View.ReceiveCharacters(cModels);
        }

        /// <summary>
        /// Receives the character list. When the characters models have been compared with the stored
        /// characters in the DB, this method can be used to transmit those models to
        /// the controller.
        /// </summary>
        /// <param name="cList">The character list.</param>
        public void ReceiveCharacterList(List<ICharacter> cList)
        {
            throw new NotImplementedException();
        }

        public void SetView(IControlled view)
        {
            View = view;
        }

        #endregion

        #region IController Members

        public void ReceiveMessage(List<ICharacter> cList)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
