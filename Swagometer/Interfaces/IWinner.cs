namespace Swagometer.Interfaces
{
    public interface IWinner
    {
        ISwag AwardedSwag { get; }
        IAttendee WinningAttendee { get; }
    }
}
