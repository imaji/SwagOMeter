using System;
using System.Collections;

namespace PinballSwagOMeter
{
    public abstract class MatrixTransformer
    {
        private BitMatrix _originals;
        protected int InvocationCount;

        protected MatrixTransformer()
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

        public static MatrixTransformer Create<T>(BitMatrix bitmaps) where T : MatrixTransformer
        {
            var transformer = (T)Activator.CreateInstance(typeof(T));
            transformer.Initialise(bitmaps);
            return transformer;
        }

        public BitMatrix GetNextScreen()
        {
            return Transform();
        }

        protected abstract BitMatrix Transform();

        protected virtual void Initialise(BitMatrix bitmaps)
        {
            _originals = new BitMatrix(new BitArray[bitmaps.Count]);
            for (var i = 0; i < bitmaps.Count; ++i)
            {
                _originals[i] = new BitArray(bitmaps[i]);
            }
        }

        protected BitMatrix CloneOriginals()
        {
            var cloned = new BitMatrix(new BitArray[_originals.Count]);
            for (var i = 0; i < _originals.Count; ++i)
            {
                cloned[i] = new BitArray(_originals[i]);
            }
            return cloned;
        }
    }
}
