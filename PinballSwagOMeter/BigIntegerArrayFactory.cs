using System.Numerics;
using System.Linq;

namespace PinballSwagOMeter
{
    public static class BigIntegerArrayFactory
    {
        public static BitMatrix Create(params byte[][] byteLumps)
        {
            return new BitMatrix(byteLumps.Select(ba => new BigInteger(ba)).ToArray());
        }
    }
}
