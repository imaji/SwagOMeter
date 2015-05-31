using System;
using Swagometer.Lib.Interfaces;

namespace Swagometer.Lib
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
