using System.Numerics;

namespace PinballSwagOMeter
{
    public static class Extensions
    {
        public static BigInteger[] Add(this BigInteger[] x, BigInteger[] y)
        {
            for (var i = 0; i < x.Length; ++i)
            {
                x[i] += y[i];
            }
            return x;
        }
    }
}
