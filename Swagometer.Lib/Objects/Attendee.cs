using System;
using System.Xml;
using Swagometer.Lib.Interfaces;
using System.Xml.Serialization;

namespace Swagometer.Lib.Objects
{
    [XmlRoot("Attendee")]
    public class Attendee : AttendeeBase
    {
        private const string DEFAULT = "Set me";

        public Attendee()
        {
            Name = DEFAULT;
        }

        public static AttendeeBase Create(XmlNode attendeeElement)
        {
            var id = attendeeElement.Attributes["id"];

            var convertedId = Guid.Empty;

            if (id != null)
                convertedId = new Guid(id.Value);

            var newAttendee = new Attendee { Name = attendeeElement.InnerText, Id = convertedId };
            return newAttendee;
        }

        [XmlIgnore]
        public override Guid Id { get; set; }

        public override string Name { get; set; }

        public override bool IsValid()
        {
            return !(string.IsNullOrEmpty(Name) || Name.Equals(DEFAULT));
        }

        public override string ToString()
        {
            return Name;
        }

        public override AttendeeBase Duplicate()
        {
            return new Attendee { Id = Id, Name = Name };
        }
    }
}
