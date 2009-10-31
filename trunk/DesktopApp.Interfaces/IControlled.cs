using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanji.DesktopApp.Interfaces
{
    public interface IControlled
    {
        void ReceiveCharacters(List<ICharacter> cModels);

        void ReceivePointList(IStroke stroke, IBoundingBox bb);
    }
}
