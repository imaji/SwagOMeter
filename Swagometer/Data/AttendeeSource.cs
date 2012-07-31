using System;
using System.Collections.Generic;
using System.Linq;
using Swagometer.Collections;
using Swagometer.Views;

namespace Swagometer.Data
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
