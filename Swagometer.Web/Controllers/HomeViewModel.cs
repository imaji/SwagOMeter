
using System.Collections.Generic;
using System.Linq;
using Swagometer.Lib.Interfaces;

namespace Swagometer.Web.Controllers
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Errors = Enumerable.Empty<string>();
            Attendees = Enumerable.Empty<IAttendee>();
            SwagItems = Enumerable.Empty<ISwag>();
        }

        public IEnumerable<IAttendee> Attendees { get; set; }
        public IEnumerable<ISwag> SwagItems { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
