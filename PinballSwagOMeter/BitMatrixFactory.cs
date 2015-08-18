using System.Linq;
using System.Collections;

namespace PinballSwagOMeter
{
    public static class BitMatrixFactory
    {
        public static BitMatrix Create(params byte[][] byteLumps)
        {
            return new BitMatrix(byteLumps.Select(ba => new BitArray(ba)).ToArray());
        }
    }
}
