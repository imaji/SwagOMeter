using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Swagometer.Data;
using Swagometer.Interfaces;
using Swagometer.Objects;

namespace Swagometer.Tests.Objects
{
    [TestClass]
    public class SwagOMeterAwardEngineTests
    {
        [TestMethod]
        public void SwagOMeterAwardEngineShouldLoadAttendeesWhenRequestedButHaveNoWinnerOrSwagAssigned()
        {
            // Arrange
            var stubAttendees = new List<IAttendee> { new Mock<IAttendee>().Object };
            var stubSwag = new List<ISwag> { new Mock<ISwag>().Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = new SwagOMeterAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
            
            // Assert
            Assert.IsTrue(awardEngine.CanSwag);
            Assert.IsNull(awardEngine.WinningAttendee);
            Assert.IsNull(awardEngine.AwardedSwag);
        }

        [TestMethod]
        public void SwagOMeterAwardEngineShouldLoadAttendeesWhenRequestedButShouldNotBeSwagableWhenNoAttendeesArePresent()
        {
            // Arrange
            var stubAttendees = new List<IAttendee>();
            var stubSwag = new List<ISwag> { new Mock<ISwag>().Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = new SwagOMeterAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);

            // Assert
            Assert.IsFalse(awardEngine.CanSwag);
            Assert.IsNull(awardEngine.WinningAttendee);
            Assert.IsNull(awardEngine.AwardedSwag);
        }

        [TestMethod]
        public void SwagOMeterAwardEngineShouldLoadAttendeesWhenRequestedButShouldNotBeSwagableWhenNoSwagIsPresent()
        {
            // Arrange
            var stubAttendees = new List<IAttendee> { new Mock<IAttendee>().Object };
            var stubSwag = new List<ISwag>();

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = new SwagOMeterAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);

            // Assert
            Assert.IsFalse(awardEngine.CanSwag);
            Assert.IsNull(awardEngine.WinningAttendee);
            Assert.IsNull(awardEngine.AwardedSwag);
        }

        [TestMethod]
        public void SwagOMeterAwardEngineShouldAwardSwagAndCanSwagSetToFalseWhenSwagAndAttendeesAreAllUsedUpAfterAward()
        {
            // Arrange
            var mockAttendee = new Mock<IAttendee>();
            var mockSwag = new Mock<ISwag>();

            var stubAttendees = new List<IAttendee> { mockAttendee.Object };
            var stubSwag = new List<ISwag> { mockSwag.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = new SwagOMeterAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
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
            var mockAttendee1 = new Mock<IAttendee>();
            var mockAttendee2 = new Mock<IAttendee>();

            var mockSwag1 = new Mock<ISwag>();
            var mockSwag2 = new Mock<ISwag>();

            var stubAttendees = new List<IAttendee> { mockAttendee1.Object, mockAttendee2.Object };
            var stubSwag = new List<ISwag> { mockSwag1.Object, mockSwag2.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = new SwagOMeterAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
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
            var mockAttendee1 = new Mock<IAttendee>();
            var mockAttendee2 = new Mock<IAttendee>();

            var mockSwag = new Mock<ISwag>();

            var stubAttendees = new List<IAttendee> { mockAttendee1.Object, mockAttendee2.Object };
            var stubSwag = new List<ISwag> { mockSwag.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = new SwagOMeterAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
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
            var mockAttendee1 = new Mock<IAttendee>();
            var mockAttendee2 = new Mock<IAttendee>();
            var mockSwag = new Mock<ISwag>();

            var stubAttendees = new List<IAttendee> { mockAttendee1.Object, mockAttendee2.Object };
            var stubSwag = new List<ISwag> { mockSwag.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            var mockWinnersSource = new Mock<IWinnersSource>();

            // Act
            var awardEngine = new SwagOMeterAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
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
            var stubAttendee = new Mock<IAttendee>();
            var stubSwagObject = new Mock<ISwag>();

            var stubAttendees = new List<IAttendee> { stubAttendee.Object };
            var stubSwag = new List<ISwag> { stubSwagObject.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = new SwagOMeterAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
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
            var mockAttendee1 = new Mock<IAttendee>();
            var mockAttendee2 = new Mock<IAttendee>();
            var mockSwag = new Mock<ISwag>();

            var stubAttendees = new List<IAttendee> { mockAttendee1.Object, mockAttendee2.Object };
            var stubSwag = new List<ISwag> { mockSwag.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = new SwagOMeterAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
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
            var mockAttendee1 = new Mock<IAttendee>();
            var mockAttendee2 = new Mock<IAttendee>();
            var mockSwag = new Mock<ISwag>();

            var stubAttendees = new List<IAttendee> { mockAttendee1.Object, mockAttendee2.Object };
            var stubSwag = new List<ISwag> { mockSwag.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = new SwagOMeterAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
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
            var stubAttendee = new Mock<IAttendee>();
            var stubSwagObject = new Mock<ISwag>();

            var stubAttendees = new List<IAttendee> { stubAttendee.Object };
            var stubSwag = new List<ISwag> { stubSwagObject.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = new SwagOMeterAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
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
            var stubAttendee1 = new Mock<IAttendee>();
            stubAttendee1.SetupGet(a => a.Name).Returns("Bob");

            var stubAttendee2 = new Mock<IAttendee>();
            stubAttendee2.SetupGet(a => a.Name).Returns("Fred");

            var mockSwag = new Mock<ISwag>();
            mockSwag.SetupGet(a => a.Thing).Returns("Pants");

            var stubAttendees = new List<IAttendee> { stubAttendee1.Object, stubAttendee2.Object };
            var stubSwag = new List<ISwag> { mockSwag.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = new SwagOMeterAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
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
            var stubAttendee1 = new Mock<IAttendee>();
            stubAttendee1.SetupGet(a => a.Name).Returns("Bob");

            var stubAttendee2 = new Mock<IAttendee>();
            stubAttendee2.SetupGet(a => a.Name).Returns("Fred");

            var stubSwagObject = new Mock<ISwag>();
            stubSwagObject.SetupGet(a => a.Thing).Returns("Pants");

            var stubAttendees = new List<IAttendee> { stubAttendee1.Object, stubAttendee2.Object };
            var stubSwag = new List<ISwag> { stubSwagObject.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = new SwagOMeterAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
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
            var mockAttendee1 = new Mock<IAttendee>();
            var stubOriginalSwag = new Mock<ISwag>();
            stubOriginalSwag.SetupGet(s => s.Thing).Returns("SOCKS");
            stubOriginalSwag.SetupGet(s => s.Company).Returns("Company");
            var stubDuplicateSwag = new Mock<ISwag>();
            stubDuplicateSwag.SetupGet(s => s.Thing).Returns("SoCks");
            stubDuplicateSwag.SetupGet(s => s.Company).Returns("Company");

            var stubAttendees = new List<IAttendee> { mockAttendee1.Object };
            var stubSwag = new List<ISwag> { stubOriginalSwag.Object, stubDuplicateSwag.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = new SwagOMeterAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
            awardEngine.AwardSwag();
            awardEngine.AttendeeDoesNotWantSwag();

            // Assert
            Assert.IsFalse(awardEngine.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterAwardEngineShouldBeAbleToSwagWhenTheSecondToLastAttendeeDoesntWantTheSwagAndTheOnlySwagLeftIsAnotherItemOfTheSameButTheLastAttendeeDoesWantIt()
        {
            // Arrange
            var mockAttendee1 = new Mock<IAttendee>();
            var mockAttendee2 = new Mock<IAttendee>();
            var stubOriginalSwag = new Mock<ISwag>();
            stubOriginalSwag.SetupGet(s => s.Thing).Returns("SOCKS");
            stubOriginalSwag.SetupGet(s => s.Company).Returns("Company");
            var stubDuplicateSwag = new Mock<ISwag>();
            stubDuplicateSwag.SetupGet(s => s.Thing).Returns("SoCks");
            stubDuplicateSwag.SetupGet(s => s.Company).Returns("Company");

            var stubAttendees = new List<IAttendee> { mockAttendee1.Object, mockAttendee2.Object };
            var stubSwag = new List<ISwag> { stubOriginalSwag.Object, stubDuplicateSwag.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = new SwagOMeterAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
            awardEngine.AwardSwag();
            awardEngine.AttendeeDoesNotWantSwag();

            // Assert
            Assert.IsTrue(awardEngine.CanSwag);
        }

        [TestMethod]
        public void SwagOMeterAwardEngineShouldNotIncludeMultipleEntriesInTheWinners()
        {
            // Arrange
            var mockAttendee1 = new Mock<IAttendee>();
            var mockAttendee2 = new Mock<IAttendee>();
            var stubOriginalSwag = new Mock<ISwag>();
            stubOriginalSwag.SetupGet(s => s.Thing).Returns("SOCKS");
            stubOriginalSwag.SetupGet(s => s.Company).Returns("Company");
            var stubDuplicateSwag = new Mock<ISwag>();
            stubDuplicateSwag.SetupGet(s => s.Thing).Returns("SoCks");
            stubDuplicateSwag.SetupGet(s => s.Company).Returns("Company");

            var stubAttendees = new List<IAttendee> { mockAttendee1.Object, mockAttendee2.Object };
            var stubSwag = new List<ISwag> { stubOriginalSwag.Object, stubDuplicateSwag.Object };

            var stubAttendeeSource = new Mock<IAttendeeSource>();
            stubAttendeeSource.Setup(sa => sa.Load(It.IsAny<string>())).Returns(stubAttendees);

            var stubSwagSource = new Mock<ISwagSource>();
            stubSwagSource.Setup(ss => ss.Load(It.IsAny<string>())).Returns(stubSwag);

            // Act
            var awardEngine = new SwagOMeterAwardEngine(stubAttendeeSource.Object, stubSwagSource.Object);
            awardEngine.AwardSwag();
            awardEngine.AttendeeDoesNotWantSwag();

            // Assert
            Assert.IsTrue(awardEngine.CanSwag);
        }
    }
}
