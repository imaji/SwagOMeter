namespace Swagometer
{
    public interface IWinner
    {
        ISwag AwardedSwag { get; }
        IAttendee WinningAttendee { get; }
    }
}
