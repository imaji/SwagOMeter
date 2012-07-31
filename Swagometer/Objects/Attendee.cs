using System;
using System.Xml;

namespace Swagometer
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
            XmlAttribute id = attendeeElement.Attributes["id"];

            Guid convertedId = Guid.Empty;

            if (id != null)
                convertedId = new Guid(id.Value);

            var newAttendee = new Attendee { Name = attendeeElement.InnerText, Id = convertedId };
            return newAttendee;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsValid()
        {
            bool isValid = true;

            if ((string.IsNullOrEmpty(Name)) ||
                (Name.Equals(DEFAULT)))
                isValid = false;

            return isValid;
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
