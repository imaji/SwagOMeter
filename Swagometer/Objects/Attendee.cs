using System;
using System.Xml;
using Swagometer.Interfaces;

namespace Swagometer.Objects
{
    class Attendee : IAttendee
    {
        private const string DEFAULT = "Set me";

        public Attendee()
        {
            Name = DEFAULT;
        }

        internal static IAttendee Create(XmlNode attendeeElement)
        {
            var id = attendeeElement.Attributes["id"];

            var convertedId = Guid.Empty;

            if (id != null)
                convertedId = new Guid(id.Value);

            var newAttendee = new Attendee { Name = attendeeElement.InnerText, Id = convertedId };
            return newAttendee;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(Name) || Name.Equals(DEFAULT));
        }

        public override string ToString()
        {
            return Name;
        }

        public IAttendee Duplicate()
        {
            return new Attendee { Id = Id, Name = Name };
        }
    }
}
