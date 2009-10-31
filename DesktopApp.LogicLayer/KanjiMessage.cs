using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanji.DesktopApp.LogicLayer
{
    //keep this updated with enum KanjiMessage in namespace
    //Kanji.InputArea.MobileGUI

    public enum KanjiMessage
    {
        Nothing = 0x00000001,
        ClearData = 0x00000002,
        FinishedStroke = 0x00000004,
        InputCharacter = 0x00000008
    }
}
