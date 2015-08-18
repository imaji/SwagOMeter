using System.Collections.Generic;
using Swagometer.Lib.Collections;
using Swagometer.Lib.Interfaces;

namespace Swagometer.Lib.Data
{
    public class AttendeeSource : ThingSource<IAttendee>, IAttendeeSource
    {
        public AttendeeSource(IDisplayErrorMessages displayErrorMessage) : base(displayErrorMessage, "Attendee") {}

        protected override IList<IAttendee> LoadThings(string thingLocation)
        {
            return AttendeeCollection.Load(thingLocation);
        }

        protected override IThingCollection<IAttendee> GetCollection()
        {
            return AttendeeCollection.Create();
        }
    }
}
