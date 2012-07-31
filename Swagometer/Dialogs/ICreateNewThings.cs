using System;
using System.Collections.Generic;
using Swagometer.Interfaces;

namespace Swagometer.Dialogs
{
    public interface ICreateNewThings<TThing>
        where TThing : IThing<TThing>
    {
        TThing NewThing { get; }

        bool? ShowDialog();
    }
}
