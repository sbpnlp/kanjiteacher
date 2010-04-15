﻿//This file should mirror Kanji.DesktopApp.Interfaces.IController in any respect if possible.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanji.InputArea.MobileGUI
{
    /// <summary>
    /// Any Controller in a MVC paradigm needs to implement these communication methods.
    /// </summary>
    public interface IController
    {

        bool SendPointList(object sender,
            List<System.Windows.Forms.MouseEventArgs> activePoints,
            List<DateTime> activeTimes);

        /// <summary>
        /// Sets the view. Regardless of the actual realisation of the view,
        /// the view should implement the IControlled interface
        /// </summary>
        /// <param name="view">The view.</param>
        //void SetView(IControlled view);
        //void ReceivePointList(object sender, IMouseInputEventArgs e);
        //void ReceiveCharacterModels(List<ICharacter> cModels);
        //void ReceiveCharacterList(List<ICharacter> cList);
        //void ReceiveMessage(List<ICharacter> cList);
    }
}
