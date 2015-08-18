namespace Swagometer.Lib.Interfaces
{
    public interface IWinner
    {
        ISwag AwardedSwag { get; }
        IAttendee WinningAttendee { get; }
    }
}
