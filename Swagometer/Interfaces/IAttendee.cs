using System;
using Swagometer.Interfaces;

namespace Swagometer
{
    public interface IAttendee : IThing<IAttendee>
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}
