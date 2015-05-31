using System;

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
