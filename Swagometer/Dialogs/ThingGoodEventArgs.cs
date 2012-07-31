using System;
using System.Collections.Generic;
using System.Linq;

namespace Swagometer.Dialogs
{
    public class ThingGoodEventArgs : EventArgs
    {
        public ThingGoodEventArgs(bool isGood)
        {
            IsGood = isGood;            
        }

        public bool IsGood { get; private set; }
    }
}
