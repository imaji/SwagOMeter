using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;

namespace PinballSwagOMeter
{
    public class BitMatrix
    {
        private BigInteger[] _bigIntegers;

        public BitMatrix(BigInteger[] bigIntegers)
        {
            _bigIntegers = bigIntegers;
        }

        public BigInteger this[int i]
        {
            get
            {
                return _bigIntegers[i];
            }
            set
            {
                _bigIntegers[i] = value;
            }
        }

        public BitMatrix Add(BitMatrix bigIntegers)
        {
            for (var i = 0; i < bigIntegers._bigIntegers.Length; ++i)
            {
                _bigIntegers[i] += bigIntegers._bigIntegers[i];
            }
            return this;
        }

        public int Length
        {
            get { return _bigIntegers.Length; }
        }

        public IList<BitArray> ToList()
        {
            return new List<BitArray>(_bigIntegers.Select(i => new BitArray(i.ToByteArray())));
        }

        public BitArray GetBitsForRow(int row)
        {
            var bits = new BitArray(Constants.Columns);
            var bitMask = this[row];
            var bit = new BigInteger(Math.Pow(2, Constants.Columns - 1));
            for (var col = 0; col < Constants.Columns; ++col)
            {
                bits[col] = (bitMask & bit) == bit;
                bit >>= 1;
            }
            return bits;
        }
    }
}
