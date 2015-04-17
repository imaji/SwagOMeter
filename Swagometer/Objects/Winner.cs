using Swagometer.Interfaces;

namespace Swagometer.Objects
{
    class Winner : IWinner
    {
        private Winner() {}

        public static IWinner Create (ISwag swag, IAttendee attendee)
        {
            var winner = new Winner {AwardedSwag = swag, WinningAttendee = attendee};

            return winner;
        }

        public ISwag AwardedSwag { get; private set; }

        public IAttendee WinningAttendee { get; private set; }
    }
}
