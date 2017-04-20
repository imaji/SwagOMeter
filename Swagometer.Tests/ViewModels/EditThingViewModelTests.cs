using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Swagometer.Dialogs;
using Swagometer.Lib.Interfaces;
using Swagometer.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Swagometer.Tests.ViewModels
{
    [TestClass]
    public class EditThingViewModelTests
    {
        [TestMethod]
        public void EditThingViewModelShouldLoadAvailableSwagFromTheSwagSourceWhenConstructed()
        {
            // Arrange
            var stubSwag1 = new Mock<SwagBase>();
            var stubSwag2 = new Mock<SwagBase>();
            var stubSwag3 = new Mock<SwagBase>();

            var stubSwag = new List<SwagBase> { stubSwag1.Object, stubSwag2.Object, stubSwag3.Object };

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            const string stubPath = "A Path";

            var stubDiaglogFactory = new Mock<IDialogFactory<SwagBase>>();

            // Act
            var viewModel = new EditThingsViewModel<SwagBase, ISwagSource>(stubSwagSource.Object, stubPath, stubDiaglogFactory.Object);
            viewModel.ViewReady();

            // Assert
            Assert.AreEqual(3, viewModel.Things.Count());
        }

        [TestMethod]
        public void EditThingViewModelShouldNotSaveAvailableSwagToTheSwagSourceWhenChangesHaveNotBeenMade()
        {
            // Arrange
            var stubSwag1 = new Mock<SwagBase>();
            var stubSwag2 = new Mock<SwagBase>();
            var stubSwag3 = new Mock<SwagBase>();

            var stubSwag = new List<SwagBase> { stubSwag1.Object, stubSwag2.Object, stubSwag3.Object };

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            const string stubPath = "A Path";

            var stubDiaglogFactory = new Mock<IDialogFactory<SwagBase>>();

            var stubView = new Mock<ICanClose>();

            // Act
            var viewModel = new EditThingsViewModel<SwagBase, ISwagSource>(stubSwagSource.Object, stubPath, stubDiaglogFactory.Object);
            viewModel.RegisterView(stubView.Object);
            viewModel.ViewReady();
            viewModel.SaveCommand.Execute(null);

            // Assert
            Assert.AreEqual(3, viewModel.Things.Count());
            stubSwagSource.Verify(ss => ss.Save(It.IsAny<IList<SwagBase>>(), It.Is<string>(s => s.StartsWith(stubPath))), Times.Never());
        }

        [TestMethod]
        public void EditThingViewModelSaveAvailableSwagToTheSwagSourceWhenChangesHaveBeenMade()
        {
            // Arrange
            var stubSwag1 = new Mock<SwagBase>();
            var stubSwag2 = new Mock<SwagBase>();
            var stubSwag3 = new Mock<SwagBase>();

            var stubNewSwag = new Mock<SwagBase>();

            var stubSwag = new List<SwagBase> { stubSwag1.Object, stubSwag2.Object, stubSwag3.Object };

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            const string stubPath = "A Path";

            var stubCreateSwag = new Mock<ICreateNewThings<SwagBase>>();
            stubCreateSwag.Setup(cs => cs.ShowDialog());
            stubCreateSwag.SetupGet(cs => cs.NewThing).Returns(stubNewSwag.Object);

            var stubDiaglogFactory = new Mock<IDialogFactory<SwagBase>>();
            stubDiaglogFactory.Setup(f => f.CreateDialog()).Returns(stubCreateSwag.Object);

            var stubView = new Mock<ICanClose>();

            // Act
            var viewModel = new EditThingsViewModel<SwagBase, ISwagSource>(stubSwagSource.Object, stubPath, stubDiaglogFactory.Object);
            viewModel.RegisterView(stubView.Object);
            viewModel.ViewReady();
            viewModel.CreateCommand.Execute(null);
            viewModel.SaveCommand.Execute(null);

            // Assert
            stubSwagSource.Verify(ss => ss.Save(It.IsAny<IList<SwagBase>>(), It.Is<string>(s => s.StartsWith(stubPath))), Times.Once());
        }

        [TestMethod]
        public void EditThingViewModelShoulAddNewSwagToTheSwagSourceWhenChangesHaveBeenMade()
        {
            // Arrange
            var stubSwag1 = new Mock<SwagBase>();
            var stubSwag2 = new Mock<SwagBase>();
            var stubSwag3 = new Mock<SwagBase>();

            var stubNewSwag = new Mock<SwagBase>();

            var stubSwag = new List<SwagBase> { stubSwag1.Object, stubSwag2.Object, stubSwag3.Object };

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            const string stubPath = "A Path";

            var stubCreateSwag = new Mock<ICreateNewThings<SwagBase>>();
            stubCreateSwag.Setup(cs => cs.ShowDialog());
            stubCreateSwag.SetupGet(cs => cs.NewThing).Returns(stubNewSwag.Object);

            var stubDiaglogFactory = new Mock<IDialogFactory<SwagBase>>();
            stubDiaglogFactory.Setup(f => f.CreateDialog()).Returns(stubCreateSwag.Object);

            var stubView = new Mock<ICanClose>();

            // Act
            var viewModel = new EditThingsViewModel<SwagBase, ISwagSource>(stubSwagSource.Object, stubPath, stubDiaglogFactory.Object);
            viewModel.RegisterView(stubView.Object);
            viewModel.ViewReady();
            viewModel.CreateCommand.Execute(null);
            viewModel.SaveCommand.Execute(null);

            // Assert
            Assert.AreEqual(4, viewModel.Things.Count());
            Assert.IsTrue(viewModel.Things.Contains(stubNewSwag.Object));
        }

        [TestMethod]
        public void EditThingViewModelShouldNotAddNewSwagToTheSwagSourceWhenChangesAreStartedButThenCancelled()
        {
            // Arrange
            var stubSwag1 = new Mock<SwagBase>();
            var stubSwag2 = new Mock<SwagBase>();
            var stubSwag3 = new Mock<SwagBase>();

            var stubSwag = new List<SwagBase> { stubSwag1.Object, stubSwag2.Object, stubSwag3.Object };

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            const string stubPath = "A Path";

            var stubCreateSwag = new Mock<ICreateNewThings<SwagBase>>();
            stubCreateSwag.Setup(cs => cs.ShowDialog());
            stubCreateSwag.SetupGet(cs => cs.NewThing).Returns((SwagBase)null);

            var stubDiaglogFactory = new Mock<IDialogFactory<SwagBase>>();
            stubDiaglogFactory.Setup(f => f.CreateDialog()).Returns(stubCreateSwag.Object);

            var stubView = new Mock<ICanClose>();

            // Act
            var viewModel = new EditThingsViewModel<SwagBase, ISwagSource>(stubSwagSource.Object, stubPath, stubDiaglogFactory.Object);
            viewModel.RegisterView(stubView.Object);
            viewModel.ViewReady();
            viewModel.CreateCommand.Execute(null);
            viewModel.SaveCommand.Execute(null);

            // Assert
            Assert.AreEqual(3, viewModel.Things.Count());
        }

        [TestMethod]
        public void EditThingViewModelShouldNotRemoveSelectedSwagWhenNoChangesHaveBeenMade()
        {
            // Arrange
            var stubSwag1 = new Mock<SwagBase>();
            var stubSwag2 = new Mock<SwagBase>();
            var stubSwag3 = new Mock<SwagBase>();

            var stubSwag = new List<SwagBase> { stubSwag1.Object, stubSwag2.Object, stubSwag3.Object };

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            const string stubPath = "A Path";

            var stubDiaglogFactory = new Mock<IDialogFactory<SwagBase>>();

            // Act
            var viewModel = new EditThingsViewModel<SwagBase, ISwagSource>(stubSwagSource.Object, stubPath, stubDiaglogFactory.Object);
            viewModel.ViewReady();
            viewModel.DeleteCommand.Execute(null);

            // Assert
            Assert.AreEqual(3, viewModel.Things.Count());
        }

        [TestMethod]
        public void EditThingViewModelShouldRemoveSelectedSwagWhenOneHasBeenSelected()
        {
            // Arrange
            var stubSwag1 = new Mock<SwagBase>();
            var stubSwag2 = new Mock<SwagBase>();
            var stubSwag3 = new Mock<SwagBase>();

            var stubSwag = new List<SwagBase> { stubSwag1.Object, stubSwag2.Object, stubSwag3.Object };

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            const string stubPath = "A Path";

            var stubDiaglogFactory = new Mock<IDialogFactory<SwagBase>>();

            // Act
            var viewModel = new EditThingsViewModel<SwagBase, ISwagSource>(stubSwagSource.Object, stubPath, stubDiaglogFactory.Object);
            viewModel.ViewReady();
            viewModel.SelectedThing = stubSwag2.Object;
            viewModel.DeleteCommand.Execute(null);

            // Assert
            Assert.AreEqual(2, viewModel.Things.Count());
            Assert.IsFalse(viewModel.Things.Contains(stubSwag2.Object));
        }

        [TestMethod]
        public void EditThingViewModelShouldDuplicateSelectedSwagWhenOneHasBeenSelected()
        {
            // Arrange
            var mockDuplicateSwag = new Mock<SwagBase>();

            var stubSwag1 = new Mock<SwagBase>();
            var stubSwag2 = new Mock<SwagBase>();
            stubSwag2.Setup(s => s.Duplicate()).Returns(mockDuplicateSwag.Object);
            var stubSwag3 = new Mock<SwagBase>();

            var stubSwag = new List<SwagBase> { stubSwag1.Object, stubSwag2.Object, stubSwag3.Object };

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            const string stubPath = "A Path";

            var stubDiaglogFactory = new Mock<IDialogFactory<SwagBase>>();

            // Act
            var viewModel = new EditThingsViewModel<SwagBase, ISwagSource>(stubSwagSource.Object, stubPath, stubDiaglogFactory.Object);
            viewModel.ViewReady();
            viewModel.SelectedThing = stubSwag2.Object;
            viewModel.DuplicateCommand.Execute(null);

            // Assert
            Assert.AreEqual(4, viewModel.Things.Count());
            Assert.IsTrue(viewModel.Things.Contains(mockDuplicateSwag.Object));
        }

        [TestMethod]
        public void EditThingViewModelShouldNotDuplicateSwagWhenOneHasNotBeenSelected()
        {
            // Arrange
            var stubSwag1 = new Mock<SwagBase>();
            var stubSwag2 = new Mock<SwagBase>();
            var stubSwag3 = new Mock<SwagBase>();

            var stubSwag = new List<SwagBase> { stubSwag1.Object, stubSwag2.Object, stubSwag3.Object };

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            const string stubPath = "A Path";

            var stubDiaglogFactory = new Mock<IDialogFactory<SwagBase>>();

            // Act
            var viewModel = new EditThingsViewModel<SwagBase, ISwagSource>(stubSwagSource.Object, stubPath, stubDiaglogFactory.Object);
            viewModel.ViewReady();
            viewModel.DuplicateCommand.Execute(null);

            // Assert
            Assert.AreEqual(3, viewModel.Things.Count());
        }
    }
}
