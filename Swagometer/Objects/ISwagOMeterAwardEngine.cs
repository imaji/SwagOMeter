using System.ComponentModel;
using Swagometer.Data;
using Swagometer.Interfaces;

namespace Swagometer.Objects
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
