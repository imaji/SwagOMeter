using NUnit.Framework;
using System.Numerics;

namespace PinballSwagOMeter.Test
{
    [TestFixture]
    public class InvertTransformerTest
    {
        [Test]
        public void WhenTransformExpectAllRowsOscillating()
        {
            var originals = new BigInteger[10];
            for (int i = 0; i < 8; ++i)
            {
                originals[i] = i + 1;
            }
            originals[9] = new BigInteger(255);
            var transformer = MatrixTransformer.Create<InvertTransformer>(originals);

            var transformed = transformer.Transform();
            AssertValuesTransformed(transformed);

            transformed = transformer.Transform();
            AssertValuesBackToOriginal(transformed);

            transformed = transformer.Transform();
            AssertValuesTransformed(transformed);
        }

        private static void AssertValuesTransformed(BigInteger[] transformed)
        {
            for (var i = 0; i < 8; ++i)
            {
                Assert.That((int)transformed[i], Is.EqualTo(-i - 2));
            }
        }

        private static void AssertValuesBackToOriginal(BigInteger[] transformed)
        {
            for (var i = 0; i < 8; ++i)
            {
                Assert.That((int)transformed[i], Is.EqualTo(i + 1));
            }
        }
    }
}
