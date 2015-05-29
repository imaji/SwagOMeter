using System;
using System.Numerics;

namespace PinballSwagOMeter
{
    public abstract class MatrixTransformer
    {
        private BigInteger[] _originals;

        public MatrixTransformer()
        {
            KeepTimerRunning = true;
            SubsequentDelayMs = 500;
        }

        public int SubsequentDelayMs { get; protected set; }
        public bool KeepTimerRunning { get; protected set; }

        public static MatrixTransformer Create<T>() where T : MatrixTransformer
        {
            var transformer = (T)Activator.CreateInstance(typeof(T));
            return transformer;
        }

        public static MatrixTransformer Create<T>(BigInteger[] bitmaps) where T : MatrixTransformer
        {
            var transformer = (T)Activator.CreateInstance(typeof(T));
            transformer.Initialise(bitmaps);
            return transformer;
        }

        public abstract BigInteger[] Transform();

        protected virtual void Initialise(BigInteger[] bitmaps)
        {
            _originals = new BigInteger[bitmaps.Length];
            for (var i = 0; i < bitmaps.Length; ++i)
            {
                _originals[i] = new BigInteger(bitmaps[i].ToByteArray());
            }
        }

        protected BigInteger[] CloneOriginals()
        {
            var cloned = new BigInteger[_originals.Length];
            for (var i = 0; i < _originals.Length; ++i)
            {
                cloned[i] = new BigInteger(_originals[i].ToByteArray());
            }
            return cloned;
        }
    }
}
