using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

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

        public bool this[int x, int y]
        {
            get
            {
                var bit = 1 << x;
                return (_bigIntegers[y] & bit) == bit;
            }
            set
            {
                BigInteger bit = 1 << x;
                if (value)
                {
                    _bigIntegers[y] |= bit;
                }
                else
                {
                    if ((_bigIntegers[y] & bit) == bit)
                    {
                        _bigIntegers[y] -= bit;
                    }
                }
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

        public IList<BigInteger> ToList()
        {
            return new List<BigInteger>(_bigIntegers);
        }

        public void GetBitsForRow(BitMatrix bitPatterns, BitArray[] bits, int row)
        {
            bits[row] = new BitArray(Constants.Columns);
            var bitMask = bitPatterns[row];
            var bit = new BigInteger(Math.Pow(2, Constants.Columns - 1));
            for (var col = 0; col < Constants.Columns; ++col)
            {
                bits[row][col] = (bitMask & bit) == bit;
                bit >>= 1;
            }
        }
    }
}
