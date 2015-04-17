using System;

namespace Swagometer.Interfaces
{
    public interface IAttendee : IThing<IAttendee>
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}
