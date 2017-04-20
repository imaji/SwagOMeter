using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Swagometer.Lib.Interfaces;
using Swagometer.ViewModels;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Swagometer.Tests.ViewModels
{
    [TestClass]
    public class SwagOMeterViewModelTests
    {
        [TestMethod]
        public void SwagOMeterViewModelShouldLoadAttendeesWhenRequestedButHaveNoWinnerOrSwagAssigned()
        {
            // Arrange
            var stubAttendeeSource = new Mock<IAttendeeSource>();

            var stubSwagSource = new Mock<ISwagSource>();

            var stubWinnersSource = new Mock<IWinnersSource>();

            var stubSwagOMeterEngine = new Mock<ISwagOMeterAwardEngine>();
            stubSwagOMeterEngine.Setup(e => e.CheckCanSwag()).Returns(true);

            // Act
            var viewModel = new SwagOMeterViewModel(stubAttendeeSource.Object, stubSwagSource.Object, stubWinnersSource.Object, stubSwagOMeterEngine.Object);
            viewModel.ViewReady();

            // Assert
            Assert.IsTrue(viewModel.CanSwag);
            Assert.IsNull(viewModel.WinningAttendee);
            Assert.IsNull(viewModel.WonSwag);
        }

        [TestMethod]
        public void SwagOMeterViewModelShouldLoadAttendeesWhenRequestedButShouldNotBeSwagableWhenNoAttendeesArePresent()
        {
            // Arrange
            var stubAttendeeSource = new Mock<IAttendeeSource>();

            var stubSwagSource = new Mock<ISwagSource>();

            var stubWinnersSource = new Mock<IWinnersSource>();

            var stubSwagOMeterEngine = new Mock<ISwagOMeterAwardEngine>();

            // Act
            var viewModel = new SwagOMeterViewModel(stubAttendeeSource.Object, stubSwagSource.Object, stubWinnersSource.Object, stubSwagOMeterEngine.Object);
            viewModel.ViewReady();

            // Assert
            Assert.IsFalse(viewModel.CanSwag);
            Assert.IsNull(viewModel.WinningAttendee);
            Assert.IsNull(viewModel.WonSwag);
        }

        [TestMethod]
        public void SwagOMeterViewModelShouldLoadAttendeesWhenRequestedButShouldNotBeSwagableWhenNoSwagIsPresent()
        {
            // Arrange
            var stubAttendeeSource = new Mock<IAttendeeSource>();

            var stubSwagSource = new Mock<ISwagSource>();

            var stubWinnersSource = new Mock<IWinnersSource>();

            var stubSwagOMeterEngine = new Mock<ISwagOMeterAwardEngine>();

            // Act
            var viewModel = new SwagOMeterViewModel(stubAttendeeSource.Object, stubSwagSource.Object, stubWinnersSource.Object, stubSwagOMeterEngine.Object);
            viewModel.ViewReady();

            // Assert
            Assert.IsFalse(viewModel.CanSwag);
            Assert.IsNull(viewModel.WinningAttendee);
            Assert.IsNull(viewModel.WonSwag);
        }

        [TestMethod]
        public void SwagOMeterViewModelShouldAwardSwagAndCanSwagSetToFalseWhenSwagAndAttendeesAreAllUsedUpAfterAward()
        {
            // Arrange
            var mockAttendee = new Mock<AttendeeBase>();
            mockAttendee.SetupGet(a => a.Name).Returns("Bob");

            var mockSwag = new Mock<SwagBase>();
            mockSwag.SetupGet(s => s.Company).Returns("Company");
            mockSwag.SetupGet(s => s.Thing).Returns("Thing");

            var stubAttendeeSource = new Mock<IAttendeeSource>();

            var stubSwagSource = new Mock<ISwagSource>();

            var stubWinnersSource = new Mock<IWinnersSource>();

            var stubSwagOMeterEngine = new Mock<ISwagOMeterAwardEngine>();
            stubSwagOMeterEngine.Setup(e => e.CheckCanSwag()).Returns(true);
            stubSwagOMeterEngine.SetupSequence(e => e.CanSwag).Returns(true).Returns(false);
            stubSwagOMeterEngine.SetupGet(e => e.AwardedSwag).Returns(mockSwag.Object);
            stubSwagOMeterEngine.SetupGet(e => e.WinningAttendee).Returns(mockAttendee.Object);

            // Act
            var viewModel = new SwagOMeterViewModel(stubAttendeeSource.Object, stubSwagSource.Object, stubWinnersSource.Object, stubSwagOMeterEngine.Object);
            viewModel.ViewReady();
            viewModel.AwardSwagCommand.Execute(null);

            // Assert
            StringAssert.Matches(viewModel.WinningAttendee, new Regex("Bob"));
            StringAssert.Matches(viewModel.WonSwag, new Regex("Company Thing"));
            Assert.IsFalse(viewModel.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterViewModelShouldAwardSwagAndCanSwagSetToTrueWhenAfterWinningMoreSwagAndAttendeesAreAvailable()
        {
            // Arrange
            var mockAttendee1 = new Mock<AttendeeBase>();
            mockAttendee1.SetupGet(a => a.Name).Returns("1");

            var mockSwag1 = new Mock<SwagBase>();
            mockSwag1.SetupGet(s => s.Company).Returns("Company");
            mockSwag1.SetupGet(s => s.Thing).Returns("Thing");

            var stubAttendeeSource = new Mock<IAttendeeSource>();

            var stubSwagSource = new Mock<ISwagSource>();

            var stubWinnersSource = new Mock<IWinnersSource>();

            var stubSwagOMeterEngine = new Mock<ISwagOMeterAwardEngine>();
            stubSwagOMeterEngine.Setup(e => e.CheckCanSwag()).Returns(true);
            stubSwagOMeterEngine.SetupGet(e => e.CanSwag).Returns(true);
            stubSwagOMeterEngine.SetupGet(e => e.AwardedSwag).Returns(mockSwag1.Object);
            stubSwagOMeterEngine.SetupGet(e => e.WinningAttendee).Returns(mockAttendee1.Object);

            // Act
            var viewModel = new SwagOMeterViewModel(stubAttendeeSource.Object, stubSwagSource.Object, stubWinnersSource.Object, stubSwagOMeterEngine.Object);
            viewModel.ViewReady();
            viewModel.AwardSwagCommand.Execute(null);

            // Assert
            StringAssert.Matches(viewModel.WinningAttendee, new Regex("1"));
            StringAssert.Matches(viewModel.WonSwag, new Regex("Company Thing"));
            Assert.IsTrue(viewModel.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterViewModelShouldAwardSwagAndCanSwagSetToFalseWhenAfterAwardAllSwagIsGoneButMoreAttendeesAreAvailable()
        {
            // Arrange
            var mockAttendee1 = new Mock<AttendeeBase>();
            mockAttendee1.SetupGet(a => a.Name).Returns("1");

            var mockSwag = new Mock<SwagBase>();
            mockSwag.SetupGet(s => s.Company).Returns("Company");
            mockSwag.SetupGet(s => s.Thing).Returns("Thing");

            var stubAttendeeSource = new Mock<IAttendeeSource>();

            var stubSwagSource = new Mock<ISwagSource>();

            var stubWinnersSource = new Mock<IWinnersSource>();

            var stubSwagOMeterEngine = new Mock<ISwagOMeterAwardEngine>();
            stubSwagOMeterEngine.Setup(e => e.CheckCanSwag()).Returns(true);
            stubSwagOMeterEngine.SetupSequence(e => e.CanSwag).Returns(true).Returns(false);
            stubSwagOMeterEngine.SetupGet(e => e.AwardedSwag).Returns(mockSwag.Object);
            stubSwagOMeterEngine.SetupGet(e => e.WinningAttendee).Returns(mockAttendee1.Object);

            // Act
            var viewModel = new SwagOMeterViewModel(stubAttendeeSource.Object, stubSwagSource.Object, stubWinnersSource.Object, stubSwagOMeterEngine.Object);
            viewModel.ViewReady();
            viewModel.AwardSwagCommand.Execute(null);

            // Assert
            StringAssert.Matches(viewModel.WinningAttendee, new Regex("1"));
            StringAssert.Matches(viewModel.WonSwag, new Regex("Company Thing"));
            Assert.IsFalse(viewModel.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterViewModelShouldHaveCanSwagSetToTrueWhenAWinnerIsNotPresentAndTheSwagIsReAssigned()
        {
            // Arrange
            var mockAttendee = new Mock<AttendeeBase>();
            var mockSwag = new Mock<SwagBase>();

            var stubAttendeeSource = new Mock<IAttendeeSource>();

            var stubSwagSource = new Mock<ISwagSource>();

            var stubWinnersSource = new Mock<IWinnersSource>();

            var stubSwagOMeterEngine = new Mock<ISwagOMeterAwardEngine>();
            stubSwagOMeterEngine.Setup(e => e.CheckCanSwag()).Returns(true);
            stubSwagOMeterEngine.SetupSequence(e => e.CanSwag).Returns(true);
            stubSwagOMeterEngine.SetupGet(e => e.AwardedSwag).Returns(mockSwag.Object);
            stubSwagOMeterEngine.SetupGet(e => e.WinningAttendee).Returns(mockAttendee.Object);

            // Act
            var viewModel = new SwagOMeterViewModel(stubAttendeeSource.Object, stubSwagSource.Object, stubWinnersSource.Object, stubSwagOMeterEngine.Object);
            viewModel.ViewReady();
            viewModel.AwardSwagCommand.Execute(null);
            viewModel.AttendeeNotPresentCommand.Execute(null);

            // Assert
            Assert.IsNull(viewModel.WinningAttendee);
            Assert.IsNull(viewModel.WonSwag);
            Assert.IsTrue(viewModel.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterViewModelShouldHaveCanSwagSetToFalseWhenAWinnerIsNotPresentAndTheSwagIsReAssignedButOnlyOneAttendeeAndSwagIsLeft()
        {
            // Arrange
            var mockAttendee = new Mock<AttendeeBase>();
            var mockSwag = new Mock<SwagBase>();

            var stubAttendeeSource = new Mock<IAttendeeSource>();

            var stubSwagSource = new Mock<ISwagSource>();

            var stubWinnersSource = new Mock<IWinnersSource>();

            var stubSwagOMeterEngine = new Mock<ISwagOMeterAwardEngine>();
            stubSwagOMeterEngine.SetupSequence(e => e.CheckCanSwag()).Returns(true).Returns(false);
            stubSwagOMeterEngine.SetupGet(e => e.AwardedSwag).Returns(mockSwag.Object);
            stubSwagOMeterEngine.SetupGet(e => e.WinningAttendee).Returns(mockAttendee.Object);
            stubSwagOMeterEngine.SetupGet(e => e.CanSwag).Returns(true);

            // Act
            var viewModel = new SwagOMeterViewModel(stubAttendeeSource.Object, stubSwagSource.Object, stubWinnersSource.Object, stubSwagOMeterEngine.Object);
            viewModel.ViewReady();
            viewModel.AwardSwagCommand.Execute(null);
            viewModel.AttendeeNotPresentCommand.Execute(null);

            // Assert
            Assert.IsNull(viewModel.WinningAttendee);
            Assert.IsNull(viewModel.WonSwag);
            Assert.IsFalse(viewModel.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterViewModelShouldHaveCanSwagSetToFalseWhenAWinnerIsNotPresentAndTheSwagIsReAssignedAndReAwarded()
        {
            // Arrange
            var mockAttendee = new Mock<AttendeeBase>();
            mockAttendee.SetupGet(a => a.Name).Returns("1");

            var mockSwag = new Mock<SwagBase>();
            mockSwag.SetupGet(s => s.Company).Returns("Company");
            mockSwag.SetupGet(s => s.Thing).Returns("Thing");

            var stubAttendeeSource = new Mock<IAttendeeSource>();

            var stubSwagSource = new Mock<ISwagSource>();

            var stubWinnersSource = new Mock<IWinnersSource>();

            var stubSwagOMeterEngine = new Mock<ISwagOMeterAwardEngine>();
            stubSwagOMeterEngine.Setup(e => e.CheckCanSwag()).Returns(true);
            stubSwagOMeterEngine.SetupGet(e => e.AwardedSwag).Returns(mockSwag.Object);
            stubSwagOMeterEngine.SetupGet(e => e.WinningAttendee).Returns(mockAttendee.Object);
            stubSwagOMeterEngine.SetupSequence(e => e.CanSwag).Returns(true).Returns(true).Returns(true).Returns(false);

            // Act
            var viewModel = new SwagOMeterViewModel(stubAttendeeSource.Object, stubSwagSource.Object, stubWinnersSource.Object, stubSwagOMeterEngine.Object);
            viewModel.ViewReady();
            viewModel.AwardSwagCommand.Execute(null);
            viewModel.AttendeeNotPresentCommand.Execute(null);
            viewModel.AwardSwagCommand.Execute(null);

            // Assert
            StringAssert.Matches(viewModel.WinningAttendee, new Regex("1"));
            StringAssert.Matches(viewModel.WonSwag, new Regex("Company Thing"));
            Assert.IsFalse(viewModel.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterViewModelShouldBeAbleToSwagWhenAnAttendeeDoesntWantTheSwagButAnotherAttendeeIsAvailableToWinIt()
        {
            // Arrange
            var stubAttendee = new Mock<AttendeeBase>();
            var stubSwag = new Mock<SwagBase>();

            var stubAttendeeSource = new Mock<IAttendeeSource>();

            var stubSwagSource = new Mock<ISwagSource>();

            var stubWinnersSource = new Mock<IWinnersSource>();

            var stubSwagOMeterEngine = new Mock<ISwagOMeterAwardEngine>();
            stubSwagOMeterEngine.Setup(e => e.CheckCanSwag()).Returns(true);
            stubSwagOMeterEngine.SetupGet(e => e.AwardedSwag).Returns(stubSwag.Object);
            stubSwagOMeterEngine.SetupGet(e => e.WinningAttendee).Returns(stubAttendee.Object);
            stubSwagOMeterEngine.SetupSequence(e => e.CanSwag).Returns(true).Returns(false).Returns(true);

            // Act
            var viewModel = new SwagOMeterViewModel(stubAttendeeSource.Object, stubSwagSource.Object, stubWinnersSource.Object, stubSwagOMeterEngine.Object);
            viewModel.ViewReady();
            viewModel.AwardSwagCommand.Execute(null);
            viewModel.AlreadyGotSwagCommand.Execute(null);

            // Assert
            Assert.IsNull(viewModel.WinningAttendee);
            Assert.IsNull(viewModel.WonSwag);
            Assert.IsTrue(viewModel.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterViewModelShouldNotBeAbleToSwagWhenAnAttendeeDoesntWantTheSwagButNoOtherAttendeeOrSwagIsLeft()
        {
            // Arrange
            var stubAttendee = new Mock<AttendeeBase>();
            var stubSwag = new Mock<SwagBase>();

            var stubAttendeeSource = new Mock<IAttendeeSource>();

            var stubSwagSource = new Mock<ISwagSource>();

            var stubWinnersSource = new Mock<IWinnersSource>();

            var stubSwagOMeterEngine = new Mock<ISwagOMeterAwardEngine>();
            stubSwagOMeterEngine.SetupSequence(e => e.CheckCanSwag()).Returns(true).Returns(false);
            stubSwagOMeterEngine.SetupGet(e => e.AwardedSwag).Returns(stubSwag.Object);
            stubSwagOMeterEngine.SetupGet(e => e.WinningAttendee).Returns(stubAttendee.Object);
            stubSwagOMeterEngine.SetupSequence(e => e.CanSwag).Returns(true).Returns(false);

            // Act
            var viewModel = new SwagOMeterViewModel(stubAttendeeSource.Object, stubSwagSource.Object, stubWinnersSource.Object, stubSwagOMeterEngine.Object);
            viewModel.ViewReady();
            viewModel.AwardSwagCommand.Execute(null);
            viewModel.AlreadyGotSwagCommand.Execute(null);

            // Assert
            Assert.IsNull(viewModel.WinningAttendee);
            Assert.IsNull(viewModel.WonSwag);
            Assert.IsFalse(viewModel.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterViewModelShouldNotSaveAnyWinnerWhenNoWinnersHaveBeenAssigned()
        {
            // Arrange
            var stubAttendee1 = new Mock<AttendeeBase>();
            stubAttendee1.SetupGet(a => a.Name).Returns("Bob");

            var stubAttendee2 = new Mock<AttendeeBase>();
            stubAttendee2.SetupGet(a => a.Name).Returns("Fred");

            var stubSwagObject = new Mock<SwagBase>();
            stubSwagObject.SetupGet(a => a.Thing).Returns("Pants");

            var stubAttendees = new List<AttendeeBase> { stubAttendee1.Object, stubAttendee2.Object };
            var stubSwag = new List<SwagBase> { stubSwagObject.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            var mockWinnersSource = new Mock<IWinnersSource>();
            mockWinnersSource.Setup(ws => ws.Save(It.IsAny<IList<IWinner>>())).Verifiable();

            var stubSwagOMeterEngine = new Mock<ISwagOMeterAwardEngine>();
            stubSwagOMeterEngine.SetupSequence(e => e.CheckCanSwag()).Returns(false);

            // Act
            var viewModel = new SwagOMeterViewModel(stubAttendeeSource.Object, stubSwagSource.Object, mockWinnersSource.Object, stubSwagOMeterEngine.Object);
            viewModel.ViewReady();
            viewModel.CloseCommand.Execute(null);

            // Assert
            mockWinnersSource.Verify(ws => ws.Save(It.IsAny<IList<IWinner>>()), Times.Never());
        }

        [TestMethod]
        public void SwagOMeterViewModelShouldNotSaveIfSaveOnExitSettingOff()
        {
            // Arrange
            var mockAttendee = new Mock<AttendeeBase>();
            mockAttendee.SetupGet(a => a.Name).Returns("Bob");

            var mockSwagObject = new Mock<SwagBase>();
            mockSwagObject.SetupGet(a => a.Thing).Returns("Pants");

            var stubAttendees = new List<AttendeeBase> { mockAttendee.Object };
            var stubSwag = new List<SwagBase> { mockSwagObject.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            var stubWinnersSource = new Mock<IWinnersSource>();
            stubWinnersSource.Setup(ws => ws.Save(It.IsAny<IList<IWinner>>())).Verifiable();

            var mockSwagOMeterEngine = new Mock<ISwagOMeterAwardEngine>();
            mockSwagOMeterEngine.SetupSequence(e => e.CheckCanSwag()).Returns(true);
            mockSwagOMeterEngine.SetupGet(e => e.CanSwag).Returns(true);

            // Act
            var viewModel = new SwagOMeterViewModel(stubAttendeeSource.Object, stubSwagSource.Object, stubWinnersSource.Object, mockSwagOMeterEngine.Object, false);
            viewModel.ViewReady();
            viewModel.AwardSwagCommand.Execute(null);
            viewModel.CloseCommand.Execute(null);

            // Assert
            mockSwagOMeterEngine.Verify(e => e.SaveWinners(It.Is<IWinnersSource>(ws => ws == stubWinnersSource.Object)), Times.Never());
        }

        [TestMethod]
        public void SwagOMeterViewModelShouldSaveAnyWinnerWhenWinnersHaveBeenAssigned()
        {
            // Arrange
            var mockAttendee = new Mock<AttendeeBase>();
            mockAttendee.SetupGet(a => a.Name).Returns("Bob");

            var mockSwagObject = new Mock<SwagBase>();
            mockSwagObject.SetupGet(a => a.Thing).Returns("Pants");

            var stubAttendees = new List<AttendeeBase> { mockAttendee.Object };
            var stubSwag = new List<SwagBase> { mockSwagObject.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            var stubWinnersSource = new Mock<IWinnersSource>();
            stubWinnersSource.Setup(ws => ws.Save(It.IsAny<IList<IWinner>>())).Verifiable();

            var mockSwagOMeterEngine = new Mock<ISwagOMeterAwardEngine>();
            mockSwagOMeterEngine.SetupSequence(e => e.CheckCanSwag()).Returns(true);
            mockSwagOMeterEngine.SetupGet(e => e.CanSwag).Returns(true);

            // Act
            var viewModel = new SwagOMeterViewModel(stubAttendeeSource.Object, stubSwagSource.Object, stubWinnersSource.Object, mockSwagOMeterEngine.Object);
            viewModel.ViewReady();
            viewModel.AwardSwagCommand.Execute(null);
            viewModel.CloseCommand.Execute(null);

            // Assert
            mockSwagOMeterEngine.Verify(e => e.SaveWinners(It.Is<IWinnersSource>(ws => ws == stubWinnersSource.Object)), Times.Once());
        }
    }
}
