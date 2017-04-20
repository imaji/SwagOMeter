using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Swagometer.Lib.Interfaces;
using Swagometer.Lib.Objects;
using System.Collections.Generic;

namespace Swagometer.Tests.Objects
{
    [TestClass]
    public class SwagOMeterAwardEngineTests
    {
        [TestMethod]
        public void SwagOMeterAwardEngineShouldLoadAttendeesWhenRequestedButHaveNoWinnerOrSwagAssigned()
        {
            // Arrange
            var stubAttendees = new List<AttendeeBase> { new Mock<AttendeeBase>().Object };
            var stubSwag = new List<SwagBase> { new Mock<SwagBase>().Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = BuildAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);

            // Assert
            Assert.IsTrue(awardEngine.CanSwag);
            Assert.IsNull(awardEngine.WinningAttendee);
            Assert.IsNull(awardEngine.AwardedSwag);
        }

        private static SwagOMeterAwardEngine BuildAwardEngine(IAttendeeSource x, ISwagSource y)
        {
            var awardEngine = new SwagOMeterAwardEngine("", x, y, "", "");
            return awardEngine;
        }

        [TestMethod]
        public void SwagOMeterAwardEngineShouldLoadAttendeesWhenRequestedButShouldNotBeSwagableWhenNoAttendeesArePresent()
        {
            // Arrange
            var stubAttendees = new List<AttendeeBase>();
            var stubSwag = new List<SwagBase> { new Mock<SwagBase>().Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = BuildAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);

            // Assert
            Assert.IsFalse(awardEngine.CanSwag);
            Assert.IsNull(awardEngine.WinningAttendee);
            Assert.IsNull(awardEngine.AwardedSwag);
        }

        [TestMethod]
        public void SwagOMeterAwardEngineShouldLoadAttendeesWhenRequestedButShouldNotBeSwagableWhenNoSwagIsPresent()
        {
            // Arrange
            var stubAttendees = new List<AttendeeBase> { new Mock<AttendeeBase>().Object };
            var stubSwag = new List<SwagBase>();

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = BuildAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);

            // Assert
            Assert.IsFalse(awardEngine.CanSwag);
            Assert.IsNull(awardEngine.WinningAttendee);
            Assert.IsNull(awardEngine.AwardedSwag);
        }

        [TestMethod]
        public void SwagOMeterAwardEngineShouldAwardSwagAndCanSwagSetToFalseWhenSwagAndAttendeesAreAllUsedUpAfterAward()
        {
            // Arrange
            var mockAttendee = new Mock<AttendeeBase>();
            var mockSwag = new Mock<SwagBase>();

            var stubAttendees = new List<AttendeeBase> { mockAttendee.Object };
            var stubSwag = new List<SwagBase> { mockSwag.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = BuildAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
            awardEngine.AwardSwag();

            // Assert
            Assert.AreEqual(mockAttendee.Object, awardEngine.WinningAttendee);
            Assert.AreEqual(mockSwag.Object, awardEngine.AwardedSwag);
            Assert.IsFalse(awardEngine.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterAwardEngineShouldAwardSwagAndCanSwagSetToTrueWhenAfterWinningMoreSwagAndAttendeesAreAvailable()
        {
            // Arrange
            var mockAttendee1 = new Mock<AttendeeBase>();
            var mockAttendee2 = new Mock<AttendeeBase>();

            var mockSwag1 = new Mock<SwagBase>();
            var mockSwag2 = new Mock<SwagBase>();

            var stubAttendees = new List<AttendeeBase> { mockAttendee1.Object, mockAttendee2.Object };
            var stubSwag = new List<SwagBase> { mockSwag1.Object, mockSwag2.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = BuildAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
            awardEngine.AwardSwag();

            // Assert
            Assert.IsNotNull(awardEngine.WinningAttendee);
            Assert.IsNotNull(awardEngine.AwardedSwag);
            Assert.IsTrue(awardEngine.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterAwardEngineShouldAwardSwagAndCanSwagSetToFalseWhenAfterAwardAllSwagIsGoneButMoreAttendeesAreAvailable()
        {
            // Arrange
            var mockAttendee1 = new Mock<AttendeeBase>();
            var mockAttendee2 = new Mock<AttendeeBase>();

            var mockSwag = new Mock<SwagBase>();

            var stubAttendees = new List<AttendeeBase> { mockAttendee1.Object, mockAttendee2.Object };
            var stubSwag = new List<SwagBase> { mockSwag.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = BuildAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
            awardEngine.AwardSwag();

            // Assert
            Assert.IsNotNull(awardEngine.WinningAttendee);
            Assert.AreEqual(mockSwag.Object, awardEngine.AwardedSwag);
            Assert.IsFalse(awardEngine.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterAwardEngineShouldHaveCanSwagSetToTrueWhenAWinnerIsNotPresentAndTheSwagIsReAssigned()
        {
            // Arrange
            var mockAttendee1 = new Mock<AttendeeBase>();
            var mockAttendee2 = new Mock<AttendeeBase>();
            var mockSwag = new Mock<SwagBase>();

            var stubAttendees = new List<AttendeeBase> { mockAttendee1.Object, mockAttendee2.Object };
            var stubSwag = new List<SwagBase> { mockSwag.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            var mockWinnersSource = new Mock<IWinnersSource>();

            // Act
            var awardEngine = BuildAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
            awardEngine.AwardSwag();
            awardEngine.AttendeeNotPresent();
            awardEngine.SaveWinners(mockWinnersSource.Object);

            // Assert
            Assert.IsNull(awardEngine.WinningAttendee);
            Assert.IsNull(awardEngine.AwardedSwag);
            Assert.IsTrue(awardEngine.CanSwag);
            mockWinnersSource.Verify(ws => ws.Save(It.IsAny<IList<IWinner>>()), Times.Never());
        }

        [TestMethod]
        public void SwagOMeterAwardEngineShouldHaveCanSwagSetToFalseWhenAWinnerIsNotPresentAndTheSwagIsReAssignedButOnlyOneAttendeeAndSwagIsLeft()
        {
            // Arrange
            var stubAttendee = new Mock<AttendeeBase>();
            var stubSwagObject = new Mock<SwagBase>();

            var stubAttendees = new List<AttendeeBase> { stubAttendee.Object };
            var stubSwag = new List<SwagBase> { stubSwagObject.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = BuildAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
            awardEngine.AwardSwag();
            awardEngine.AttendeeNotPresent();

            // Assert
            Assert.IsNull(awardEngine.WinningAttendee);
            Assert.IsNull(awardEngine.AwardedSwag);
            Assert.IsFalse(awardEngine.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterAwardEngineShouldHaveCanSwagSetToFalseWhenAWinnerIsNotPresentAndTheSwagIsReAssignedAndReAwarded()
        {
            // Arrange
            var mockAttendee1 = new Mock<AttendeeBase>();
            var mockAttendee2 = new Mock<AttendeeBase>();
            var mockSwag = new Mock<SwagBase>();

            var stubAttendees = new List<AttendeeBase> { mockAttendee1.Object, mockAttendee2.Object };
            var stubSwag = new List<SwagBase> { mockSwag.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = BuildAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
            awardEngine.AwardSwag();
            awardEngine.AttendeeNotPresent();
            awardEngine.AwardSwag();

            // Assert
            Assert.IsNotNull(awardEngine.WinningAttendee);
            Assert.AreEqual(mockSwag.Object, awardEngine.AwardedSwag);
            Assert.IsFalse(awardEngine.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterAwardEngineShouldBeAbleToSwagWhenAnAttendeeDoesntWantTheSwagButAnotherAttendeeIsAvailableToWinIt()
        {
            // Arrange
            var mockAttendee1 = new Mock<AttendeeBase>();
            var mockAttendee2 = new Mock<AttendeeBase>();
            var mockSwag = new Mock<SwagBase>();

            var stubAttendees = new List<AttendeeBase> { mockAttendee1.Object, mockAttendee2.Object };
            var stubSwag = new List<SwagBase> { mockSwag.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = BuildAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
            awardEngine.AwardSwag();
            awardEngine.AttendeeDoesNotWantSwag();

            // Assert
            Assert.IsNull(awardEngine.WinningAttendee);
            Assert.IsNull(awardEngine.AwardedSwag);
            Assert.IsTrue(awardEngine.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterAwardEngineShouldNotBeAbleToSwagWhenAnAttendeeDoesntWantTheSwagButNoOtherAttendeeOrSwagIsLeft()
        {
            // Arrange
            var stubAttendee = new Mock<AttendeeBase>();
            var stubSwagObject = new Mock<SwagBase>();

            var stubAttendees = new List<AttendeeBase> { stubAttendee.Object };
            var stubSwag = new List<SwagBase> { stubSwagObject.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = BuildAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
            awardEngine.AwardSwag();
            awardEngine.AttendeeDoesNotWantSwag();

            // Assert
            Assert.IsNull(awardEngine.WinningAttendee);
            Assert.IsNull(awardEngine.AwardedSwag);
            Assert.IsFalse(awardEngine.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterAwardEngineShouldNotBeAbleToSwagWhenAnAttendeeDoesntWantTheSwagThenTheLastAttendeeWinsIt()
        {
            // Arrange
            var stubAttendee1 = new Mock<AttendeeBase>();
            stubAttendee1.SetupGet(a => a.Name).Returns("Bob");

            var stubAttendee2 = new Mock<AttendeeBase>();
            stubAttendee2.SetupGet(a => a.Name).Returns("Fred");

            var mockSwag = new Mock<SwagBase>();
            mockSwag.SetupGet(a => a.Thing).Returns("Pants");

            var stubAttendees = new List<AttendeeBase> { stubAttendee1.Object, stubAttendee2.Object };
            var stubSwag = new List<SwagBase> { mockSwag.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = BuildAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
            awardEngine.AwardSwag();
            awardEngine.AttendeeDoesNotWantSwag();
            awardEngine.AwardSwag();

            // Assert
            Assert.IsNotNull(awardEngine.WinningAttendee);
            Assert.AreEqual(mockSwag.Object, awardEngine.AwardedSwag);
            Assert.IsFalse(awardEngine.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterAwardEngineShouldNotBeAbleToSwagWhenAnAttendeeDoesntWantTheSwagAndTheOtherAttendeeHasLeft()
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

            // Act
            var awardEngine = BuildAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
            awardEngine.AwardSwag();
            awardEngine.AttendeeDoesNotWantSwag();
            awardEngine.AwardSwag();
            awardEngine.AttendeeNotPresent();

            // Assert
            Assert.IsNull(awardEngine.WinningAttendee);
            Assert.IsNull(awardEngine.AwardedSwag);
            Assert.IsFalse(awardEngine.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterAwardEngineShouldNotBeAbleToSwagWhenTheLastAttendeeDoesntWantTheSwagAndTheOnlySwagLeftIsAnotherItemOfTheSame()
        {
            // Arrange
            var mockAttendee1 = new Mock<AttendeeBase>();
            var stubOriginalSwag = new Mock<SwagBase>();
            stubOriginalSwag.SetupGet(s => s.Thing).Returns("SOCKS");
            stubOriginalSwag.SetupGet(s => s.Company).Returns("Company");
            var stubDuplicateSwag = new Mock<SwagBase>();
            stubDuplicateSwag.SetupGet(s => s.Thing).Returns("SoCks");
            stubDuplicateSwag.SetupGet(s => s.Company).Returns("Company");

            var stubAttendees = new List<AttendeeBase> { mockAttendee1.Object };
            var stubSwag = new List<SwagBase> { stubOriginalSwag.Object, stubDuplicateSwag.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = BuildAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
            awardEngine.AwardSwag();
            awardEngine.AttendeeDoesNotWantSwag();

            // Assert
            Assert.IsFalse(awardEngine.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterAwardEngineShouldBeAbleToSwagWhenTheSecondToLastAttendeeDoesntWantTheSwagAndTheOnlySwagLeftIsAnotherItemOfTheSameButTheLastAttendeeDoesWantIt()
        {
            // Arrange
            var mockAttendee1 = new Mock<AttendeeBase>();
            var mockAttendee2 = new Mock<AttendeeBase>();
            var stubOriginalSwag = new Mock<SwagBase>();
            stubOriginalSwag.SetupGet(s => s.Thing).Returns("SOCKS");
            stubOriginalSwag.SetupGet(s => s.Company).Returns("Company");
            var stubDuplicateSwag = new Mock<SwagBase>();
            stubDuplicateSwag.SetupGet(s => s.Thing).Returns("SoCks");
            stubDuplicateSwag.SetupGet(s => s.Company).Returns("Company");

            var stubAttendees = new List<AttendeeBase> { mockAttendee1.Object, mockAttendee2.Object };
            var stubSwag = new List<SwagBase> { stubOriginalSwag.Object, stubDuplicateSwag.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = BuildAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
            awardEngine.AwardSwag();
            awardEngine.AttendeeDoesNotWantSwag();

            // Assert
            Assert.IsTrue(awardEngine.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterAwardEngineShouldNotIncludeMultipleEntriesInTheWinners()
        {
            // Arrange
            var mockAttendee1 = new Mock<AttendeeBase>();
            var mockAttendee2 = new Mock<AttendeeBase>();
            var stubOriginalSwag = new Mock<SwagBase>();
            stubOriginalSwag.SetupGet(s => s.Thing).Returns("SOCKS");
            stubOriginalSwag.SetupGet(s => s.Company).Returns("Company");
            var stubDuplicateSwag = new Mock<SwagBase>();
            stubDuplicateSwag.SetupGet(s => s.Thing).Returns("SoCks");
            stubDuplicateSwag.SetupGet(s => s.Company).Returns("Company");

            var stubAttendees = new List<AttendeeBase> { mockAttendee1.Object, mockAttendee2.Object };
            var stubSwag = new List<SwagBase> { stubOriginalSwag.Object, stubDuplicateSwag.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = BuildAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
            awardEngine.AwardSwag();
            awardEngine.AttendeeDoesNotWantSwag();

            // Assert
            Assert.IsTrue(awardEngine.CanSwag);
        }
    }
}
