using System;

namespace Swagometer.Lib.Interfaces
{
    public interface IAttendee : IThing<IAttendee>
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}
