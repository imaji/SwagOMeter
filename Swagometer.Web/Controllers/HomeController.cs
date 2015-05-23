using Swagometer.Lib.Data;
using Swagometer.Lib.Interfaces;
using System.Configuration;
using System.IO;
using System.Web.Mvc;

namespace Swagometer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAttendeeSource _attendeeSource;
        private readonly ISwagSource _swagSource;
        private readonly IDisplayErrorMessages _errorHandler;

        public HomeController()
            : this(new ErrorMessageCollection())
        {
        }

        private HomeController(IDisplayErrorMessages errorHandler)
            : this(new AttendeeSource(errorHandler), new SwagSource(errorHandler), errorHandler)
        {
        }

        public HomeController(IAttendeeSource attendeeSource, ISwagSource swagSource, IDisplayErrorMessages errorHandler)
        {
            _attendeeSource = attendeeSource;
            _swagSource = swagSource;
            _errorHandler = errorHandler;
        }

        public ViewResult Index()
        {
            var attendeeFileName = ConfigurationManager.AppSettings["AttendeeFileName"];
            var swagFileName = ConfigurationManager.AppSettings["SwagFileName"];
            var fileLocation = ConfigurationManager.AppSettings["FileLocation"];

            var attendees = _attendeeSource.Load(Path.Combine(fileLocation, attendeeFileName));
            var swag = _swagSource.Load(Path.Combine(fileLocation, swagFileName));
            var viewModel = new HomeViewModel
            {
                Attendees = attendees,
                SwagItems = swag,
                Errors = _errorHandler.GetErrors()
            };

            return View(viewModel);
        }
    }
}