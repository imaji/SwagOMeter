using Swagometer.Lib.Interfaces;
using Swagometer.Lib.Objects;

namespace Swagometer.ViewModels
{
    public class CreateAttendeeViewModel : CreateThingViewModel<IAttendee>
    {
        public string Name { get; set; }

        protected override void ExecuteCreate()
        {
            var newAttendee = new Attendee { Name = Name };

            if (newAttendee.IsValid())
                NewThing = newAttendee;

            FireThingGood(newAttendee.IsValid());
        }
    }
}
