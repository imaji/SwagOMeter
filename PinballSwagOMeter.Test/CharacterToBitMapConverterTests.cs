using NUnit.Framework;
using System.Collections;
using System.Diagnostics;
using System.Linq;

namespace PinballSwagOMeter.Test
{
    [TestFixture]
    public class CharacterToBitMapConverterTests
    {
        //[Test]
        //public void TestFourLinesOfHello()
        //{
        //    var characterToBitMapConverter = new CharacterToBitMapConverter(null, null);
        //    var lines = characterToBitMapConverter.GetBitPattern("hello", "hello", "hello", "hello").ToList();

        //    Assert.That(lines.Count(), Is.EqualTo(Constants.Rows));

        //    var expected = new BitArray[9];
        //    expected[0] = new BitArray(new byte[] { 0, 0, 0, 0, 0, 0, 0, 60, 48, 216, 111, 6 });
        //    expected[1] = new BitArray(new byte[] { 0, 0, 0, 0, 0, 0, 0, 126, 48, 216, 111, 6 });
        //    expected[2] = new BitArray(new byte[] { 0, 0, 0, 0, 0, 0, 0, 102, 48, 24, 108, 6 });
        //    expected[3] = new BitArray(new byte[] { 0, 0, 0, 0, 0, 0, 0, 102, 48, 216, 239, 7 });
        //    expected[4] = new BitArray(new byte[] { 0, 0, 0, 0, 0, 0, 0, 102, 48, 216, 239, 7 });
        //    expected[5] = new BitArray(new byte[] { 0, 0, 0, 0, 0, 0, 0, 102, 48, 24, 108, 6 });
        //    expected[6] = new BitArray(new byte[] { 0, 0, 0, 0, 0, 0, 0, 126, 191, 223, 111, 6 });
        //    expected[7] = new BitArray(new byte[] { 0, 0, 0, 0, 0, 0, 0, 60, 191, 223, 111, 6 });
        //    expected[8] = new BitArray(new byte[] { 0 });

        //    for (int block = 0; block < 3; ++block)
        //    {
        //        for (int blockLine = 0; blockLine < 9; ++blockLine)
        //        {
        //            Assert.That(lines[block * 9 + blockLine], Is.EqualTo(expected[blockLine]));
        //        }
        //    }
        //}

        //[Test]
        //public void GetBitPatternWithUnrecognisedCharacterIgnoresCharacter()
        //{
        //    var characterToBitMapConverter = new CharacterToBitMapConverter(null, null);
        //    var lines = characterToBitMapConverter.GetBitPattern("+hello!", "hello'", ">hello)", "<hello.").ToList();

        //    Assert.That(lines.Count(), Is.EqualTo(Constants.Rows));
        //    var expected = new BitArray[9];
        //    expected[0] = new BitArray(new byte[] { 0, 0, 0, 0, 0, 0, 0, 60, 48, 216, 111, 6 });
        //    expected[1] = new BitArray(new byte[] { 0, 0, 0, 0, 0, 0, 0, 126, 48, 216, 111, 6 });
        //    expected[2] = new BitArray(new byte[] { 0, 0, 0, 0, 0, 0, 0, 102, 48, 24, 108, 6 });
        //    expected[3] = new BitArray(new byte[] { 0, 0, 0, 0, 0, 0, 0, 102, 48, 216, 239, 7 });
        //    expected[4] = new BitArray(new byte[] { 0, 0, 0, 0, 0, 0, 0, 102, 48, 216, 239, 7 });
        //    expected[5] = new BitArray(new byte[] { 0, 0, 0, 0, 0, 0, 0, 102, 48, 24, 108, 6 });
        //    expected[6] = new BitArray(new byte[] { 0, 0, 0, 0, 0, 0, 0, 126, 191, 223, 111, 6 });
        //    expected[7] = new BitArray(new byte[] { 0, 0, 0, 0, 0, 0, 0, 60, 191, 223, 111, 6 });
        //    expected[8] = new BitArray(new byte[] { 0 });

        //    for (int block = 0; block < 3; ++block)
        //    {
        //        for (int blockLine = 0; blockLine < 9; ++blockLine)
        //        {
        //            Assert.That(lines[block * 9 + blockLine], Is.EqualTo(expected[blockLine]));
        //        }
        //    }
        //}

        [Test]
        public void CentreAndGetBitPattern()
        {
            var x = CharacterToBitMapConverter.CentreAndGetBitPattern("Hello");
            Assert.That(x.Count(), Is.EqualTo(8));


            ToBitMask(x.ElementAt(0));
            ToBitMask(x.ElementAt(1));
            ToBitMask(x.ElementAt(2));
            ToBitMask(x.ElementAt(3));
            ToBitMask(x.ElementAt(4));
            ToBitMask(x.ElementAt(5));
            ToBitMask(x.ElementAt(6));
            ToBitMask(x.ElementAt(7));



            Assert.That(ToBitMask(x.ElementAt(0)), Is.EqualTo("                                                          ****      **     ** ****** **  **                                                 "));
            Assert.That(ToBitMask(x.ElementAt(1)), Is.EqualTo("                                                         ******     **     ** ****** **  **                                                 "));
            Assert.That(ToBitMask(x.ElementAt(2)), Is.EqualTo("                                                         **  **     **     **     ** **  **                                                 "));
            Assert.That(ToBitMask(x.ElementAt(3)), Is.EqualTo("                                                         **  **     **     ** ****** ******                                                 "));
            Assert.That(ToBitMask(x.ElementAt(4)), Is.EqualTo("                                                         **  **     **     ** ****** ******                                                 "));
            Assert.That(ToBitMask(x.ElementAt(5)), Is.EqualTo("                                                         **  **     **     **     ** **  **                                                 "));
            Assert.That(ToBitMask(x.ElementAt(6)), Is.EqualTo("                                                         ****** ****** ****** ****** **  **                                                 "));
            Assert.That(ToBitMask(x.ElementAt(7)), Is.EqualTo("                                                          ****  ****** ****** ****** **  **                                                 "));
        }

        private string ToBitMask(BitArray x)
        {
            var bitMask = "";

            for (var i = 0; i < 140; ++i)
            {
                bitMask += x[i] ? "*" : " ";
            }

            Debug.WriteLine(bitMask);

            return bitMask;
        }
    }
}
