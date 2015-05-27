using NUnit.Framework;
using System.Linq;

namespace PinballSwagOMeter.Test
{
    [TestFixture]
    public class CharacterToBitMapConverterTests
    {
        [Test]
        public void TestFourLinesOfHello()
        {
            var characterToBitMapConverter = new CharacterToBitMapConverter(null, null, 0, 0);
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
            var characterToBitMapConverter = new CharacterToBitMapConverter(null, null, 0, 0);
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
    }
}

