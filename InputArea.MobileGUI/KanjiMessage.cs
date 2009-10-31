using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Kanji.InputArea.MobileGUI
{
    //keep this updated with enum KanjiMessage in namespace
    //Kanji.DesktopApp.LogicLayer
    public enum KanjiMessage
    {
        Nothing = 0x00000001,
        ClearData = 0x00000002,
        FinishedStroke = 0x00000004,
        InputCharacter = 0x00000008
    }
}
