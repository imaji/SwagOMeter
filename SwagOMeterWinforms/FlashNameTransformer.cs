using System.Numerics;

namespace PinballSwagOMeter
{
    public class FlashNameTransformer : MatrixTransformer
    {
        private enum CurrentDisplay
        {
            Name,
            Blank
        }

        private CurrentDisplay _currentDisplay;

        public override BigInteger[] Transform()
        {
            try
            {
                var bitmaps = CloneOriginals();
                if (_currentDisplay == CurrentDisplay.Name)
                {
                    for (var i = 0; i < 8; ++i)
                    {
                        bitmaps[i] = new BigInteger(0);
                    }
                }
                return bitmaps;
            }
            finally
            {
                _currentDisplay = _currentDisplay == CurrentDisplay.Blank ? CurrentDisplay.Name : CurrentDisplay.Blank;
            }
        }

        protected override void Initialise(BigInteger[] originals)
        {
            base.Initialise(originals);
            _currentDisplay = CurrentDisplay.Name;
        }
    }
}
