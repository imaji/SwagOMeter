using Swagometer.Interfaces;
using Swagometer.Objects;

namespace Swagometer.ViewModels
{
    public class CreateAttendeeViewModel : CreateThingViewModel<IAttendee>
    {
        public string Name { get; set; }

        protected override void ExecuteCreate()
        {
            var newAttendee = new Attendee() { Name = Name };

            if (newAttendee.IsValid())
                NewThing = newAttendee;

            FireThingGood(newAttendee.IsValid());
        }
    }
}
