using System;
using System.Numerics;

namespace PinballSwagOMeter
{
    public class StartupTransformer : MatrixTransformer
    {
        static readonly Random Random = new Random();

        public StartupTransformer()
        {
            SubsequentDelayMs = 100;
        }

        protected override BitMatrix Transform()
        {
            InvocationCount++;

            if (InvocationCount <= 3)
            {
                return BuildRandomFlicker();
            }

            if (InvocationCount == 4)
            {
                return BuildAllGridOn();
            }

            if (InvocationCount == 5)
            {
                return BuildDevScSplashScreen();
            }

            if (InvocationCount == 6)
            {
                return BuildSwagometerScreen();
            }

            SubsequentDelayMs = SubsequentDelayMs > 250 ? 250 : Math.Max(SubsequentDelayMs - 35, 80);
            if (InvocationCount % 2 == 1 && InvocationCount < 25)
            {
                return BuildInverseSwagometerScreen();
            }

            if (InvocationCount % 2 == 0 && InvocationCount < 25)
            {
                return BuildSwagometerScreen();
            }

            KeepTimerRunning = false;
            return BuildSwagometerScreen();
        }

        private BitMatrix BuildAllGridOn()
        {
            var allOn = new BitMatrix(new BigInteger[Constants.Rows]);
            allOn[0] = new BigInteger(new byte[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 15 });
            for (var i = 1; i < Constants.Rows; ++i)
            {
                allOn[i] = allOn[0];
            }
            SubsequentDelayMs = 1500;
            return allOn;
        }

        private BitMatrix BuildRandomFlicker()
        {
            var cloned = CloneOriginals();
            for (var row = 0; row < cloned.Length; ++row)
            {
                for (var flickers = Random.Next(30); flickers > 0; --flickers)
                {
                    var col = Random.Next(140);
                    var bitPosition = (BigInteger)Math.Pow(2, col);
                    if ((cloned[row] & bitPosition) == 0)
                    {
                        cloned[row] += (BigInteger)Math.Pow(2, col);
                    }
                }
            }
            return cloned;
        }

        private BitMatrix BuildInverseSwagometerScreen()
        {
            var orig = InvertAll(BuildSwagometerScreen());
            return orig;
        }

        private BitMatrix InvertAll(BitMatrix bigIntegers)
        {
            for (var i = 0; i < bigIntegers.Length; ++i)
            {
                bigIntegers[i] = ~bigIntegers[i];
            }
            return bigIntegers;
        }

        private BitMatrix BuildSwagometerScreen()
        {
            return BitMatrixFactory.Create(
                new byte[] { 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 0 },
                new byte[] { 192, 225, 240, 199, 1, 198, 1, 60, 192, 1, 7, 1, 194, 0, 56, 8, 224, 3 },
                new byte[] { 240, 243, 241, 239, 3, 239, 3, 126, 224, 131, 143, 3, 231, 1, 124, 24, 243, 7 },
                new byte[] { 240, 243, 251, 239, 135, 255, 7, 255, 224, 135, 159, 3, 231, 3, 252, 28, 243, 15 },
                new byte[] { 248, 251, 251, 239, 135, 255, 135, 255, 240, 199, 159, 51, 247, 3, 254, 156, 255, 15 },
                new byte[] { 248, 251, 251, 239, 207, 255, 135, 255, 241, 207, 159, 51, 247, 7, 254, 156, 255, 15 },
                new byte[] { 248, 243, 255, 239, 207, 255, 143, 255, 241, 207, 191, 51, 231, 7, 252, 157, 255, 15 },
                new byte[] { 252, 243, 247, 239, 207, 255, 207, 255, 241, 207, 191, 51, 231, 7, 252, 157, 255, 15 },
                new byte[] { 252, 227, 247, 199, 207, 121, 207, 231, 115, 239, 189, 59, 231, 7, 248, 157, 255, 15 },
                new byte[] { 188, 3, 199, 1, 206, 121, 206, 195, 115, 238, 185, 123, 7, 7, 192, 157, 247, 5 },
                new byte[] { 156, 3, 199, 1, 206, 57, 206, 193, 115, 238, 184, 123, 7, 7, 192, 157, 199, 1 },
                new byte[] { 156, 3, 199, 1, 206, 57, 238, 129, 51, 238, 184, 123, 7, 7, 192, 157, 199, 1 },
                new byte[] { 156, 227, 199, 193, 207, 57, 238, 129, 3, 238, 184, 123, 199, 7, 248, 253, 199, 1 },
                new byte[] { 188, 243, 199, 193, 207, 57, 238, 128, 243, 238, 184, 123, 231, 7, 252, 253, 199, 1 },
                new byte[] { 188, 243, 199, 225, 207, 57, 238, 128, 243, 238, 184, 123, 227, 7, 252, 253, 199, 1 },
                new byte[] { 252, 243, 199, 225, 207, 57, 238, 128, 251, 238, 191, 251, 243, 3, 252, 253, 199, 1 },
                new byte[] { 248, 243, 199, 225, 207, 57, 238, 128, 251, 238, 63, 255, 243, 3, 252, 253, 199, 1 },
                new byte[] { 248, 243, 199, 193, 207, 57, 238, 128, 251, 238, 63, 255, 243, 3, 252, 253, 199, 1 },
                new byte[] { 248, 227, 199, 193, 207, 57, 238, 128, 251, 238, 63, 239, 251, 1, 248, 253, 199, 1 },
                new byte[] { 252, 3, 199, 1, 206, 57, 238, 128, 251, 238, 63, 239, 123, 0, 192, 157, 199, 1 },
                new byte[] { 252, 3, 199, 1, 206, 57, 238, 129, 123, 238, 63, 207, 59, 0, 192, 157, 199, 1 },
                new byte[] { 188, 3, 199, 1, 206, 57, 238, 129, 123, 238, 63, 207, 59, 0, 192, 157, 199, 1 },
                new byte[] { 156, 3, 199, 1, 206, 57, 206, 193, 123, 238, 57, 207, 59, 0, 192, 157, 199, 1 },
                new byte[] { 156, 3, 199, 1, 206, 57, 206, 195, 115, 238, 56, 207, 123, 0, 192, 157, 199, 1 },
                new byte[] { 156, 243, 199, 193, 207, 57, 206, 231, 115, 239, 56, 207, 251, 3, 252, 157, 199, 1 },
                new byte[] { 156, 243, 199, 225, 207, 57, 206, 255, 241, 239, 56, 207, 243, 7, 252, 157, 199, 1 },
                new byte[] { 156, 251, 199, 225, 207, 57, 142, 255, 241, 239, 56, 207, 243, 7, 254, 157, 199, 1 },
                new byte[] { 156, 251, 195, 225, 207, 57, 142, 255, 241, 231, 56, 207, 243, 7, 254, 156, 199, 1 },
                new byte[] { 156, 251, 195, 225, 199, 57, 14, 255, 224, 231, 56, 199, 243, 7, 254, 156, 199, 1 },
                new byte[] { 156, 251, 195, 225, 199, 57, 14, 255, 224, 231, 56, 135, 225, 7, 254, 28, 195, 1 },
                new byte[] { 12, 243, 129, 225, 195, 48, 12, 126, 192, 195, 48, 134, 193, 7, 124, 24, 131, 1 },
                new byte[] { 8, 224, 0, 192, 1, 0, 0, 60, 128, 1, 0, 0, 128, 3, 56 },
                new byte[] { 0 },
                new byte[] { 0 }
                );
        }

        private static BitMatrix BuildDevScSplashScreen()
        {
            return BitMatrixFactory.Create(
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 179, 159, 15, 0, 0, 0, 0, 0, 2 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 179, 223, 15, 0, 0, 0, 0, 192, 3 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 51, 216, 12, 0, 0, 0, 0, 240, 3 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 179, 223, 12, 0, 0, 0, 0, 252, 1 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 179, 223, 12, 0, 0, 0, 0, 255, 1 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 63, 216, 12, 0, 0, 0, 192, 255, 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 158, 223, 15, 0, 0, 0, 224, 255, 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 140, 159, 15, 0, 0, 0, 240, 127 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 248, 63 },
                new byte[] { 0, 0, 0, 0, 0, 0, 204, 126, 51, 207, 7, 0, 0, 0, 252, 63 },
                new byte[] { 0, 0, 0, 0, 0, 0, 204, 126, 179, 223, 15, 0, 0, 0, 254, 63 },
                new byte[] { 0, 0, 0, 0, 0, 0, 204, 24, 179, 25, 12, 0, 0, 0, 255, 63 },
                new byte[] { 0, 0, 0, 0, 0, 0, 252, 24, 179, 153, 15, 0, 0, 128, 255, 63 },
                new byte[] { 0, 0, 0, 0, 0, 0, 252, 24, 179, 217, 7, 0, 0, 192, 255, 63 },
                new byte[] { 0, 0, 0, 0, 0, 0, 204, 24, 179, 217, 0, 0, 0, 224, 255, 63 },
                new byte[] { 0, 0, 0, 0, 0, 0, 204, 24, 191, 223, 15, 0, 0, 224, 255, 63 },
                new byte[] { 0, 0, 0, 0, 0, 0, 204, 24, 30, 143, 15, 0, 0, 224, 255, 63 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 240, 255, 63 },
                new byte[] { 0, 0, 0, 0, 0, 0, 252, 62, 30, 207, 7, 0, 0, 240, 255, 63 },
                new byte[] { 0, 0, 0, 0, 0, 0, 252, 126, 191, 223, 15, 0, 0, 248, 255, 63 },
                new byte[] { 0, 0, 0, 0, 0, 0, 48, 96, 179, 25, 12, 0, 0, 248, 255, 63 },
                new byte[] { 0, 0, 0, 0, 0, 0, 48, 124, 179, 25, 12, 0, 0, 252, 255, 127 },
                new byte[] { 0, 0, 0, 0, 0, 0, 48, 62, 191, 25, 12, 0, 0, 252, 255, 255, 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 48, 6, 191, 25, 12, 0, 0, 254, 255, 255, 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 48, 126, 179, 223, 15, 0, 0, 254, 255, 255, 1 },
                new byte[] { 0, 0, 0, 0, 0, 0, 48, 124, 51, 207, 7, 0, 0, 254, 255, 255, 3 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 254, 255, 255, 7 },
                new byte[] { 0, 0, 0, 224, 243, 155, 253, 62, 63, 159, 15, 0, 0, 254, 255, 255, 15 },
                new byte[] { 0, 0, 0, 224, 247, 155, 253, 126, 191, 223, 15, 0, 0, 254, 255, 255, 31 },
                new byte[] { 0, 0, 0, 0, 198, 216, 193, 96, 176, 217, 12, 0, 0, 254, 255, 255, 127 },
                new byte[] { 0, 0, 0, 192, 199, 248, 253, 124, 191, 223, 15, 0, 0, 254, 255, 255, 255, 0 },
                new byte[] { 0, 0, 0, 224, 195, 184, 253, 62, 63, 159, 15, 0, 0, 254, 7, 0, 192, 7 },
                new byte[] { 0, 0, 0, 96, 192, 152, 193, 6, 176, 25, 12, 0, 0, 126 },
                new byte[] { 0, 0, 0, 224, 199, 152, 253, 126, 191, 25, 12, 0, 0, 30 },
                new byte[] { 0, 0, 0, 192, 199, 152, 253, 124, 191, 25, 12, 0, 0, 3 }
                );
        }
    }
}
