using System.Collections.Generic;
using Swagometer.Lib.Collections;
using Swagometer.Lib.Interfaces;

namespace Swagometer.Lib.Data
{
    public class AttendeeSource : ThingSource<AttendeeBase>, IAttendeeSource
    {
        public AttendeeSource(IDisplayErrorMessages displayErrorMessage) : base(displayErrorMessage, "Attendee") {}

        protected override IList<AttendeeBase> LoadThings(string thingLocation)
        {
            return AttendeeCollection.Load(thingLocation);
        }

        protected override IThingCollection<AttendeeBase> GetCollection()
        {
            return AttendeeCollection.Create();
        }
    }
}
