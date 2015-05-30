using NUnit.Framework;

namespace PinballSwagOMeter.Test
{
    [TestFixture]
    public class StartupTransformerTests
    {
        [Test]
        public void SwagometerFlashes()
        {
            var x = MatrixTransformer.Create<StartupTransformer>();
            BitMatrix bitmaps;
            for (var i = 0; i < 7; ++i)
            {
                bitmaps = x.GetNextScreen();
            }
        }
    }
}
