using NUnit.Framework;

namespace PinballSwagOMeter.Test
{
    [TestFixture]
    public class GameOverTransformerTests
    {
        [Test]
        public void TwoScreens()
        {
            var transformer = new GameOverTransformer();
            transformer.Transform();
            Assert.That(transformer.KeepTimerRunning, Is.True);
            Assert.That(transformer.SubsequentDelayMs, Is.EqualTo(2000));
            transformer.Transform();
            Assert.That(transformer.KeepTimerRunning, Is.False);
        }
    }
}
