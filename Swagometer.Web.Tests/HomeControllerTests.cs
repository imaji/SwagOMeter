using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swagometer.Web.Controllers;
using System.Linq;

namespace Swagometer.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void RequestingPageLoadsAttendees()
        {
            var controller = new ControllerBuilder()
                .WithAttendees("Derek Badger", "Brian Sausage")
                .Build();

            var viewModel = (HomeViewModel)(controller.Index().Model);
            Assert.IsTrue(viewModel.Attendees.Any(a => a.Name == "Derek Badger"));
            Assert.IsTrue(viewModel.Attendees.Any(a => a.Name == "Brian Sausage"));
        }

        [TestMethod]
        public void RequestingPageLoadsSwagItems()
        {
            var controller = new ControllerBuilder()
                .WithSwag("Stuffed Badger", "Badgers R Us")
                .WithSwag("T Shirt", "Tshirts N Stuff")
                .Build();

            var viewModel = (HomeViewModel)(controller.Index().Model);
            Assert.IsTrue(viewModel.SwagItems.Any(a => a.Company == "Badgers R Us" && a.Thing == "Stuffed Badger"));
            Assert.IsTrue(viewModel.SwagItems.Any(a => a.Company == "Tshirts N Stuff" && a.Thing == "T Shirt"));
        }

        [TestMethod, Ignore]
        public void EmptyLocationCausesErrorNotifications()
        {
            var controller = new HomeController();

            var viewModel = (HomeViewModel)(controller.Index().Model);
            Assert.IsTrue(viewModel.Errors.Contains("Attendee location not specified"));
            Assert.IsTrue(viewModel.Errors.Contains("Swag location not specified"));
        }
    }
}
