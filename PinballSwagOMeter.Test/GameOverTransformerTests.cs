using NUnit.Framework;
using PinballSwagOMeter.Transformers;

namespace PinballSwagOMeter.Test
{
    [TestFixture]
    public class GameOverTransformerTests
    {
        [Test]
        public void ThreeScreens()
        {
            var transformer = new GameOverTransformer();
            transformer.GetNextScreen();
            Assert.That(transformer.KeepTimerRunning, Is.True);
            Assert.That(transformer.SubsequentDelayMs, Is.EqualTo(2000));
            transformer.GetNextScreen();
            transformer.GetNextScreen();
            Assert.That(transformer.KeepTimerRunning, Is.False);
        }
    }
}
