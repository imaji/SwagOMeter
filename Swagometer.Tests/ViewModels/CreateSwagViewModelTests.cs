using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swagometer.ViewModels;

namespace Swagometer.Tests.ViewModels
{
    [TestClass]
    public class CreateSwagViewModelTests
    {
        [TestMethod]
        public void CreateSwagViewModelShouldConstructValidSwagWhenGivenGoodInput()
        {
            // Arrange
            var SwagValid = false;

            // Act
            var viewModel = new CreateSwagViewModel();
            viewModel.ThingGood += (o, e) => SwagValid = e.IsGood;
            viewModel.Thing = "Thing";
            viewModel.Company = "Company";
            viewModel.CreateCommand.Execute(null);

            // Assert
            Assert.IsTrue(SwagValid);
            Assert.AreEqual(viewModel.NewThing.Thing, "Thing");
            Assert.AreEqual(viewModel.NewThing.Company, "Company");
        }

        [TestMethod]
        public void CreateSwagViewModelShouldConstructAnInvalidSwagWhenGivenMissingInput()
        {
            // Arrange
            var SwagValid = false;

            // Act
            var viewModel = new CreateSwagViewModel();
            viewModel.ThingGood += (o, e) => SwagValid = e.IsGood;
            viewModel.Thing = "Thing";
            viewModel.CreateCommand.Execute(null);

            // Assert
            Assert.IsFalse(SwagValid);
            Assert.IsNull(viewModel.NewThing);
        }
    }
}
