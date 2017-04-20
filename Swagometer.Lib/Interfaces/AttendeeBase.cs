using Swagometer.Lib.Objects;
using System;
using System.Xml.Serialization;

namespace Swagometer.Lib.Interfaces
{
    [XmlInclude(typeof(Attendee))]
    [XmlRoot("Attendee")]
    public abstract class AttendeeBase : IThing<AttendeeBase>
    {
        [XmlIgnore]
        public abstract Guid Id { get; set; }
        public abstract string Name { get; set; }
        public abstract AttendeeBase Duplicate();
        public abstract bool IsValid();
    }
}
