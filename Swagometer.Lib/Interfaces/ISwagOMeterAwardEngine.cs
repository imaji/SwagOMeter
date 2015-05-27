using System.ComponentModel;

namespace Swagometer.Lib.Interfaces
{
    public interface ISwagOMeterAwardEngine : INotifyPropertyChanged
    {
        bool CanSwag { get; set; }
        IAttendee WinningAttendee { get; set; }
        ISwag AwardedSwag { get; set; }

        IWinner AwardSwag();
        void AttendeeNotPresent();
        void AttendeeDoesNotWantSwag();
        bool CheckCanSwag();
        void SaveWinners(IWinnersSource winnersSource);
        void RefreshData(string attendeesFile, string swagFile, IAttendeeSource attendeeSource, ISwagSource swagSource);
    }
}
