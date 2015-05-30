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

            Assert.That(lines.Count(), Is.EqualTo(Constants.Rows));

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

            Assert.That(lines.Count(), Is.EqualTo(Constants.Rows));
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
            var bitPatterns = BigIntegerArrayFactory.Create(
                new byte[] { 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 248, 1 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 255, 31 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 224, 255, 255, 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 240, 255, 255, 1 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 252, 255, 255, 7 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 255, 255, 255, 15 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 255, 255, 255, 31 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 255, 255, 255, 31 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 255, 255, 255, 63 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 255, 255, 255, 63 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 255, 255, 255, 127 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 255, 192, 255, 127 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 31, 192, 231, 127 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 15, 192, 143, 127 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 15, 192, 63, 126 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 15, 131, 63, 126 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 7, 14, 15, 126 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 7, 28, 6, 124 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 7, 28, 7, 124 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 3, 12, 6, 124 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 3, 0, 0, 120 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 3, 0, 0, 56 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 3, 0, 8, 56 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 1, 0, 14, 48 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 1, 254, 5, 16 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 1, 4, 4, 16 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 12, 6, 16 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 240, 1 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 14, 0, 0, 14 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 252, 3, 248, 31 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 224, 255, 0, 192, 127 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 252, 0, 192, 7 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 254, 0, 240, 15 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 255, 3, 252, 63 }
            );

            var wordBitPatterns = new CharacterToBitMapConverter(null, null).GetBitPattern("         Credits:   ", "         Logic by   ", "         John       ", "         Mcgloughlin");
            var all = wordBitPatterns.Add(bitPatterns);

            for (var row = 0; row < Constants.Rows; ++row)
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
