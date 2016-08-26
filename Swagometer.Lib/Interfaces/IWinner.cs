namespace Swagometer.Lib.Interfaces
{
    public interface IWinner
    {
        SwagBase AwardedSwag { get; }
        AttendeeBase WinningAttendee { get; }
    }
}
