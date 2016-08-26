using Swagometer.Lib.Interfaces;

namespace Swagometer.Lib.Objects
{
    public class Winner : IWinner
    {
        private Winner() {}

        public static IWinner Create (SwagBase swag, AttendeeBase attendee)
        {
            var winner = new Winner {AwardedSwag = swag, WinningAttendee = attendee};

            return winner;
        }

        public SwagBase AwardedSwag { get; private set; }

        public AttendeeBase WinningAttendee { get; private set; }
    }
}
