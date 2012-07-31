using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Swagometer.Data;

namespace Swagometer.ViewModels
{
    public interface ISwagOMeterAwardEngine : INotifyPropertyChanged
    {
        bool CanSwag { get; set; }
        IAttendee WinningAttendee { get; set; }
        ISwag AwardedSwag { get; set; }

        void AwardSwag();
        void AttendeeNotPresent();
        void AttendeeDoesNotWantSwag();
        bool CheckCanSwag();
        void SaveWinners(IWinnersSource winnersSource);
        void RefreshData(IAttendeeSource attendeeSource, ISwagSource swagSource);
    }
}
