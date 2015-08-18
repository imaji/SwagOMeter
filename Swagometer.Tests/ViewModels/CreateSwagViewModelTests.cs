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
            var swagValid = false;

            // Act
            var viewModel = new CreateSwagViewModel();
            viewModel.ThingGood += (o, e) => swagValid = e.IsGood;
            viewModel.Thing = "Thing";
            viewModel.Company = "Company";
            viewModel.CreateCommand.Execute(null);

            // Assert
            Assert.IsTrue(swagValid);
            Assert.AreEqual(viewModel.NewThing.Thing, "Thing");
            Assert.AreEqual(viewModel.NewThing.Company, "Company");
        }

        [TestMethod]
        public void CreateSwagViewModelShouldConstructAnInvalidSwagWhenGivenMissingInput()
        {
            // Arrange
            var swagValid = false;

            // Act
            var viewModel = new CreateSwagViewModel();
            viewModel.ThingGood += (o, e) => swagValid = e.IsGood;
            viewModel.Thing = "Thing";
            viewModel.CreateCommand.Execute(null);

            // Assert
            Assert.IsFalse(swagValid);
            Assert.IsNull(viewModel.NewThing);
        }
    }
}
