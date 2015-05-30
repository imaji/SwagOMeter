using System.Numerics;
using System.Linq;
using System.Collections;

namespace PinballSwagOMeter
{
    public static class BitMatrixFactory
    {
        public static BitMatrix Create2(params byte[][] byteLumps)
        {
            return new BitMatrix(byteLumps.Select(ba => new BitArray(ba)).ToArray());
        }
    }
}
