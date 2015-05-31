using Swagometer.Lib.Interfaces;
using System;

namespace Swagometer.Lib.UI_Interface
{
    internal class WinnerAvailableEventArgs : EventArgs
    {
        private WinnerAvailableEventArgs()
        {
        }

        internal static WinnerAvailableEventArgs Create(IWinner winner)
        {
            return new WinnerAvailableEventArgs { Winner = winner };
        }

        public IWinner Winner { get; private set; }
    }
}
