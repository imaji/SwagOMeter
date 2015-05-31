using System.Collections;
using System.Collections.Generic;

namespace PinballSwagOMeter
{
    public class BitMatrix
    {
        private readonly IList<BitArray> _bitArrays;

        public BitMatrix()
            : this(BuildEmptyBitArrays())
        {
        }

        public BitMatrix(IEnumerable<BitArray> bitArrays)
        {
            _bitArrays = new List<BitArray>(bitArrays);
        }

        public BitArray this[int i]
        {
            get
            {
                return _bitArrays[i];
            }
            set
            {
                _bitArrays[i] = value;
            }
        }

        public int Count
        {
            get
            {
                return _bitArrays.Count;
            }
        }

        public BitArray GetBitsForRow(int row)
        {
            var bitArray = new BitArray(140);
            for (var i = 0; i < 140; ++i)
            {
                if (i >= _bitArrays[row].Length)
                {
                    break;
                }
                bitArray[139 - i] = _bitArrays[row][i];
            }
            return bitArray;
        }

        private static IEnumerable<BitArray> BuildEmptyBitArrays()
        {
            var bitArrays = new BitArray[35];
            for (var i = 0; i < 35; ++i)
            {
                bitArrays[i] = new BitArray(140);
            }
            return bitArrays;
        }
    }
}
