using NUnit.Framework;

namespace PinballSwagOMeter.Test
{
    [TestFixture]
    public class BitMatrixTests
    {
        [Test]
        public void GetTheRightBitMaskBack()
        {
            var matrix2 = BitMatrixFactory.Create(new byte[] { 1 }, new byte[] { 0, 255, 15 }, new byte[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 15 });
            var bits0 = matrix2.GetBitsForRow(0);
            var bits1 = matrix2.GetBitsForRow(1);
            var bits2 = matrix2.GetBitsForRow(2);

            for (var bit = 0; bit < 139; ++bit)
            {
                Assert.That(bits0[bit], Is.False);
            }
            Assert.That(bits0[139], Is.True);

            for (var bit = 0; bit < 120; ++bit)
            {
                Assert.That(bits1[bit], Is.False);
            }
            for (var bit = 120; bit < 131; ++bit)
            {
                Assert.That(bits1[bit], Is.True);
            }
            for (var bit = 132; bit < 140; ++bit)
            {
                Assert.That(bits1[bit], Is.False);
            }

            for (var bit = 0; bit < 140; ++bit)
            {
                Assert.That(bits2[bit], Is.True);
            }
        }
    }
}
