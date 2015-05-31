using NUnit.Framework;
using System.Collections;
using System.Linq;
using System.Text;

namespace PinballSwagOMeter.Test
{
    [TestFixture]
    public class CharacterToBitMapConverterTests
    {
        [Test]
        public void GetBitPatternWithUnrecognisedCharacterIgnoresCharacter()
        {
            var characterToBitMapConverter = new CharacterToBitMapConverter(null, null);

            var dodgyCharacterLines = characterToBitMapConverter.GetBitPattern("+hello!", "hello'", ">hello)", "<hello.");
            var niceLines = characterToBitMapConverter.GetBitPattern("hello", "hello", "hello", "hello");

            for (int block = 0; block < 3; ++block)
            {
                for (int blockLine = 0; blockLine < 9; ++blockLine)
                {
                    Assert.That(dodgyCharacterLines[block * 9 + blockLine], Is.EqualTo(niceLines[block * 9 + blockLine]));
                }
            }
        }

        [Test]
        public void CentreAndGetBitPattern()
        {
            var x = CharacterToBitMapConverter.CentreAndGetBitPattern("Hello");
            Assert.That(x.Count(), Is.EqualTo(8));

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
            var bitMask = new StringBuilder(140);
            for (var i = 0; i < 140; ++i)
            {
                bitMask.Append(x[i] ? "*" : " ");
            }
            return bitMask.ToString();
        }
    }
}