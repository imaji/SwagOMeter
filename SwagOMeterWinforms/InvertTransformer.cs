using System.Numerics;

namespace PinballSwagOMeter
{
    public class InvertTransformer : MatrixTransformer
    {
        private enum CurrentDisplay
        {
            Normal,
            Inverted
        }

        private CurrentDisplay _currentDisplay;

        protected override void Initialise(BigInteger[] originals)
        {
            base.Initialise(originals);
        }

        public override BigInteger[] Transform()
        {
            try
            {
                var bitmaps = CloneOriginals();
                if (_currentDisplay == CurrentDisplay.Normal)
                {
                    for (var i = 0; i < bitmaps.Length; ++i)
                    {
                        bitmaps[i] = ~bitmaps[i];
                    }
                }
                return bitmaps;
            }
            finally
            {
                _currentDisplay = _currentDisplay == CurrentDisplay.Normal ? CurrentDisplay.Inverted : CurrentDisplay.Normal;
            }
        }
    }
}
