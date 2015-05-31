using Swagometer.Lib.Data;
using Swagometer.Lib.Interfaces;
using Swagometer.Lib.Objects;
using System;

namespace Swagometer.Lib.UI_Interface
{
    internal class Model
    {
        private SwagOMeterAwardEngine _swagOMeterAwardEngine;

        public event EventHandler<WinnerAvailableEventArgs> WinnerAvailable;
        private IWinnersSource _winnersSource;

        public Model(AttendeeSource attendeeSource, SwagSource swagSource, IWinnersSource winnersSource)
        {
            _winnersSource = winnersSource;
            _swagOMeterAwardEngine = new SwagOMeterAwardEngine(@"C:\Users\M_J_O_N_E_S\Documents\GitHub\SwagOMeter\Example XML Files", attendeeSource, swagSource, "attendees.xml", "swag.xml");
        }

        public void ReturnSwag()
        {
            _swagOMeterAwardEngine.AttendeeDoesNotWantSwag();
        }

        public void GetWinner()
        {
            OnWinnerAvailable(_swagOMeterAwardEngine.AwardSwag());
        }

        private void OnWinnerAvailable(IWinner winner)
        {
            if (WinnerAvailable != null)
            {
                WinnerAvailable(this, WinnerAvailableEventArgs.Create(winner));
            }
        }

        public void SaveWinners()
        {
            _swagOMeterAwardEngine.SaveWinners(_winnersSource);
        }

        internal void MarkAttendeeAsLeft()
        {
            _swagOMeterAwardEngine.AttendeeNotPresent();
        }
    }
}
