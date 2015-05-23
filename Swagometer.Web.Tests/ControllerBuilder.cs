using Moq;
using Swagometer.Lib.Interfaces;
using Swagometer.Lib.Objects;
using Swagometer.Web;
using Swagometer.Web.Controllers;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Swagometer.Tests
{
    internal class ControllerBuilder
    {
        private string _fileLocation;
        private readonly string _attendeeFileName;
        private readonly string _swagFileName;
        private IEnumerable<IAttendee> _attendees;
        private readonly IList<ISwag> _swags;

        internal ControllerBuilder()
        {
            _fileLocation = "anywhere";
            _attendeeFileName = "attendeeFileName";
            _swagFileName = "swagFileName";
            _attendees = Enumerable.Empty<IAttendee>();
            _swags = new List<ISwag>();
        }

        internal ControllerBuilder WithFileLocation(string fileLocation)
        {
            _fileLocation = fileLocation;
            return this;
        }

        internal ControllerBuilder WithAttendees(params string[] attendees)
        {
            _attendees = attendees.Select(attendee => new Attendee { Name = attendee });
            return this;
        }

        internal ControllerBuilder WithSwag(string item, string company)
        {
            _swags.Add(new Swag { Company = company, Thing = item });
            return this;
        }

        internal HomeController Build()
        {
            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource
                .Setup(sa => sa.Load(_fileLocation + "\\" + _attendeeFileName))
                .Returns(() => _attendees.ToList());

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource
                .Setup(sa => sa.Load(_fileLocation + "\\" + _swagFileName))
                .Returns(() => _swags);

            ConfigurationManager.AppSettings["FileLocation"] = _fileLocation;
            ConfigurationManager.AppSettings["AttendeeFileName"] = _attendeeFileName;
            ConfigurationManager.AppSettings["SwagFileName"] = _swagFileName;

            return new HomeController(stubAttendeeSource.Object, stubSwagSource.Object, new ErrorMessageCollection());
        }
    }
}
