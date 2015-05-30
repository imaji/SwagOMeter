using NUnit.Framework;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace PinballSwagOMeter.Test
{
    [TestFixture]
    public class CharacterToBitMapConverterTests
    {
        [Test]
        public void TestFourLinesOfHello()
        {
            var characterToBitMapConverter = new CharacterToBitMapConverter(null, null);
            var lines = characterToBitMapConverter.GetBitPattern("hello", "hello", "hello", "hello").ToList();

            Assert.That(lines.Count(), Is.EqualTo(35));

            var expected = new byte[9][];
            expected[0] = new byte[] { 0, 0, 0, 0, 0, 0, 0, 60, 48, 216, 111, 6 };
            expected[1] = new byte[] { 0, 0, 0, 0, 0, 0, 0, 126, 48, 216, 111, 6 };
            expected[2] = new byte[] { 0, 0, 0, 0, 0, 0, 0, 102, 48, 24, 108, 6 };
            expected[3] = new byte[] { 0, 0, 0, 0, 0, 0, 0, 102, 48, 216, 239, 7 };
            expected[4] = new byte[] { 0, 0, 0, 0, 0, 0, 0, 102, 48, 216, 239, 7 };
            expected[5] = new byte[] { 0, 0, 0, 0, 0, 0, 0, 102, 48, 24, 108, 6 };
            expected[6] = new byte[] { 0, 0, 0, 0, 0, 0, 0, 126, 191, 223, 111, 6 };
            expected[7] = new byte[] { 0, 0, 0, 0, 0, 0, 0, 60, 191, 223, 111, 6 };
            expected[8] = new byte[] { 0 };

            for (int block = 0; block < 3; ++block)
            {
                for (int blockLine = 0; blockLine < 9; ++blockLine)
                {
                    Assert.That(lines[block * 9 + blockLine].ToByteArray(), Is.EqualTo(expected[blockLine]));
                }
            }
        }

        [Test]
        public void GetBitPatternWithUnrecognisedCharacterIgnoresCharacter()
        {
            var characterToBitMapConverter = new CharacterToBitMapConverter(null, null);
            var lines = characterToBitMapConverter.GetBitPattern("+hello!", "hello'", ">hello)", "<hello.").ToList();

            Assert.That(lines.Count(), Is.EqualTo(35));
            var expected = new byte[9][];
            expected[0] = new byte[] { 0, 0, 0, 0, 0, 0, 0, 60, 48, 216, 111, 6 };
            expected[1] = new byte[] { 0, 0, 0, 0, 0, 0, 0, 126, 48, 216, 111, 6 };
            expected[2] = new byte[] { 0, 0, 0, 0, 0, 0, 0, 102, 48, 24, 108, 6 };
            expected[3] = new byte[] { 0, 0, 0, 0, 0, 0, 0, 102, 48, 216, 239, 7 };
            expected[4] = new byte[] { 0, 0, 0, 0, 0, 0, 0, 102, 48, 216, 239, 7 };
            expected[5] = new byte[] { 0, 0, 0, 0, 0, 0, 0, 102, 48, 24, 108, 6 };
            expected[6] = new byte[] { 0, 0, 0, 0, 0, 0, 0, 126, 191, 223, 111, 6 };
            expected[7] = new byte[] { 0, 0, 0, 0, 0, 0, 0, 60, 191, 223, 111, 6 };
            expected[8] = new byte[] { 0 };

            for (int block = 0; block < 3; ++block)
            {
                for (int blockLine = 0; blockLine < 9; ++blockLine)
                {
                    Assert.That(lines[block * 9 + blockLine].ToByteArray(), Is.EqualTo(expected[blockLine]));
                }
            }
        }

        [Test]
        public void xxxxx()
        {
            var bitPatterns = new BigInteger[35];
            bitPatterns[0] = new BigInteger(new byte[] { 0 });
            bitPatterns[1] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 224, 255, 0 });
            bitPatterns[2] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 56, 192, 3 });
            bitPatterns[3] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 14 });
            bitPatterns[4] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 28 });
            bitPatterns[5] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 3, 0, 48 });
            bitPatterns[6] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 1, 0, 48 });
            bitPatterns[7] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 0, 0, 48 });
            bitPatterns[8] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 224, 224, 48 });
            bitPatterns[9] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 243, 241, 49 });
            bitPatterns[10] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 251, 224, 59 });
            bitPatterns[11] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 247, 228, 25 });
            bitPatterns[12] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 52, 14, 12 });
            bitPatterns[13] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 15, 12 });
            bitPatterns[14] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 12, 8, 6 });
            bitPatterns[15] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 12, 0, 3 });
            bitPatterns[16] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 200, 127, 1 });
            bitPatterns[17] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 1 });
            bitPatterns[18] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 56, 12, 1 });
            bitPatterns[19] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 224, 143, 193, 0 });
            bitPatterns[20] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 240, 193, 255, 224, 1 });
            bitPatterns[21] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 224, 7, 0, 248, 0 });
            bitPatterns[22] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 224, 63, 0, 255, 1 });
            bitPatterns[23] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 240, 255, 225, 255, 3 });
            bitPatterns[24] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 240, 255, 251, 239, 3 });
            bitPatterns[25] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 96, 224, 255, 1 });
            bitPatterns[26] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 255, 0 });
            bitPatterns[27] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 248, 251, 119 });
            bitPatterns[28] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 224, 255, 225, 255, 1 });
            bitPatterns[29] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 224, 127, 192, 255, 3 });
            bitPatterns[30] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 224, 31, 128, 255, 3 });
            bitPatterns[31] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 96, 15, 0, 252, 0 });
            bitPatterns[32] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 104 });
            bitPatterns[33] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 224, 0 });
            bitPatterns[34] = new BigInteger(new byte[] { 0 });

            var wordBitPatterns = new CharacterToBitMapConverter(null, null).GetBitPattern("         Credits:   ", "         Graphics by", "        Mark     ", "         Jones      ");
            var all = wordBitPatterns.Add(bitPatterns);

            for (var row = 0; row < 35; ++row)
            {
                Debug.WriteLine("bitPatterns[{0}] = new BigInteger(new byte[] {{ {1} }});", row, GetByteValues(all[row]));
            }
        }

        private static string GetByteValues(BigInteger bigInteger)
        {
            return bigInteger.ToByteArray().Aggregate("", (current, b) => current + (b + ","));
        }
    }
}

