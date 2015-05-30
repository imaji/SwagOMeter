using NUnit.Framework;
using System.Numerics;

namespace PinballSwagOMeter.Test
{
    [TestFixture]
    public class FlashNameTransformerTests
    {
        [Test]
        public void WhenTransformExpectOscillating8RowsAfterFruitMachine()
        {
            var originals = new BitMatrix(new BigInteger[10]);
            for (int i = 0; i < 8; ++i)
            {
                originals[i] = i + 1;
            }
            originals[9] = new BigInteger(255);
            var transformer = MatrixTransformer.Create<WinnerTransformer>(originals);

            for (var i = 0; i <= 8; ++i)
            {
                transformer.GetNextScreen();
            }

            var transformed = transformer.GetNextScreen();
            AssertValuesTransformed(transformed);

            transformed = transformer.GetNextScreen();
            AssertValuesBackToOriginal(transformed);

            transformed = transformer.GetNextScreen();
            AssertValuesTransformed(transformed);
        }

        private static void AssertValuesBackToOriginal(BitMatrix transformed)
        {
            for (var i = 0; i < 8; ++i)
            {
                Assert.That(transformed[i].IsZero, Is.False);
            }
            Assert.That(transformed[9].IsZero, Is.False);
        }

        private static void AssertValuesTransformed(BitMatrix transformed)
        {
            for (var i = 0; i < 8; ++i)
            {
                Assert.That(transformed[i].IsZero, Is.True);
            }
            Assert.That(transformed[9].IsZero, Is.False);
        }
    }
}
