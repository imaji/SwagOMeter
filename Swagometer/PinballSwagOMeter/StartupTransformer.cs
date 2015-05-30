using System;
using System.Numerics;

namespace PinballSwagOMeter
{
    public class StartupTransformer : MatrixTransformer
    {
        static readonly Random Random = new Random();

        private int _invokeCount;

        public StartupTransformer()
        {
            SubsequentDelayMs = 100;
        }

        public override BigInteger[] Transform()
        {
            _invokeCount++;

            if (_invokeCount <= 3)
            {
                return BuildRandomFlicker();
            }

            if (_invokeCount == 4)
            {
                return BuildAllGridOn();
            }

            if (_invokeCount == 5)
            {
                return BuildDevScSplashScreen();
            }

            if (_invokeCount == 6)
            {
                return BuildSwagometerScreen();
            }

            SubsequentDelayMs = SubsequentDelayMs > 250 ? 250 : Math.Max(SubsequentDelayMs - 35, 80);
            if (_invokeCount % 2 == 1 && _invokeCount < 25)
            {
                return BuildInverseSwagometerScreen();
            }

            if (_invokeCount % 2 == 0 && _invokeCount < 25)
            {
                return BuildSwagometerScreen();
            }

            KeepTimerRunning = false;
            return BuildSwagometerScreen();
        }

        private BigInteger[] BuildAllGridOn()
        {
            var allOn = new BigInteger[35];
            allOn[0] = new BigInteger(new byte[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 15 });
            for (var i = 1; i < 35; ++i)
            {
                allOn[i] = allOn[0];
            }
            SubsequentDelayMs = 1500;
            return allOn;
        }

        private BigInteger[] BuildRandomFlicker()
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

        private BigInteger[] BuildInverseSwagometerScreen()
        {
            var orig = InvertAll(BuildSwagometerScreen());
            return orig;
        }

        private BigInteger[] InvertAll(BigInteger[] bigIntegers)
        {
            for (var i = 0; i < bigIntegers.Length; ++i)
            {
                bigIntegers[i] = ~bigIntegers[i];
            }
            return bigIntegers;
        }

        private BigInteger[] BuildSwagometerScreen()
        {
            var swagometerScreenBits = new BigInteger[35];
            swagometerScreenBits[0] = new BigInteger(new byte[] { 0 });
            swagometerScreenBits[1] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 0 });
            swagometerScreenBits[2] = new BigInteger(new byte[] { 192, 225, 240, 199, 1, 198, 1, 60, 192, 1, 7, 1, 194, 0, 56, 8, 224, 3 });
            swagometerScreenBits[3] = new BigInteger(new byte[] { 240, 243, 241, 239, 3, 239, 3, 126, 224, 131, 143, 3, 231, 1, 124, 24, 243, 7 });
            swagometerScreenBits[4] = new BigInteger(new byte[] { 240, 243, 251, 239, 135, 255, 7, 255, 224, 135, 159, 3, 231, 3, 252, 28, 243, 15 });
            swagometerScreenBits[5] = new BigInteger(new byte[] { 248, 251, 251, 239, 135, 255, 135, 255, 240, 199, 159, 51, 247, 3, 254, 156, 255, 15 });
            swagometerScreenBits[6] = new BigInteger(new byte[] { 248, 251, 251, 239, 207, 255, 135, 255, 241, 207, 159, 51, 247, 7, 254, 156, 255, 15 });
            swagometerScreenBits[7] = new BigInteger(new byte[] { 248, 243, 255, 239, 207, 255, 143, 255, 241, 207, 191, 51, 231, 7, 252, 157, 255, 15 });
            swagometerScreenBits[8] = new BigInteger(new byte[] { 252, 243, 247, 239, 207, 255, 207, 255, 241, 207, 191, 51, 231, 7, 252, 157, 255, 15 });
            swagometerScreenBits[9] = new BigInteger(new byte[] { 252, 227, 247, 199, 207, 121, 207, 231, 115, 239, 189, 59, 231, 7, 248, 157, 255, 15 });
            swagometerScreenBits[10] = new BigInteger(new byte[] { 188, 3, 199, 1, 206, 121, 206, 195, 115, 238, 185, 123, 7, 7, 192, 157, 247, 5 });
            swagometerScreenBits[11] = new BigInteger(new byte[] { 156, 3, 199, 1, 206, 57, 206, 193, 115, 238, 184, 123, 7, 7, 192, 157, 199, 1 });
            swagometerScreenBits[12] = new BigInteger(new byte[] { 156, 3, 199, 1, 206, 57, 238, 129, 51, 238, 184, 123, 7, 7, 192, 157, 199, 1 });
            swagometerScreenBits[13] = new BigInteger(new byte[] { 156, 227, 199, 193, 207, 57, 238, 129, 3, 238, 184, 123, 199, 7, 248, 253, 199, 1 });
            swagometerScreenBits[14] = new BigInteger(new byte[] { 188, 243, 199, 193, 207, 57, 238, 128, 243, 238, 184, 123, 231, 7, 252, 253, 199, 1 });
            swagometerScreenBits[15] = new BigInteger(new byte[] { 188, 243, 199, 225, 207, 57, 238, 128, 243, 238, 184, 123, 227, 7, 252, 253, 199, 1 });
            swagometerScreenBits[16] = new BigInteger(new byte[] { 252, 243, 199, 225, 207, 57, 238, 128, 251, 238, 191, 251, 243, 3, 252, 253, 199, 1 });
            swagometerScreenBits[17] = new BigInteger(new byte[] { 248, 243, 199, 225, 207, 57, 238, 128, 251, 238, 63, 255, 243, 3, 252, 253, 199, 1 });
            swagometerScreenBits[18] = new BigInteger(new byte[] { 248, 243, 199, 193, 207, 57, 238, 128, 251, 238, 63, 255, 243, 3, 252, 253, 199, 1 });
            swagometerScreenBits[19] = new BigInteger(new byte[] { 248, 227, 199, 193, 207, 57, 238, 128, 251, 238, 63, 239, 251, 1, 248, 253, 199, 1 });
            swagometerScreenBits[20] = new BigInteger(new byte[] { 252, 3, 199, 1, 206, 57, 238, 128, 251, 238, 63, 239, 123, 0, 192, 157, 199, 1 });
            swagometerScreenBits[21] = new BigInteger(new byte[] { 252, 3, 199, 1, 206, 57, 238, 129, 123, 238, 63, 207, 59, 0, 192, 157, 199, 1 });
            swagometerScreenBits[22] = new BigInteger(new byte[] { 188, 3, 199, 1, 206, 57, 238, 129, 123, 238, 63, 207, 59, 0, 192, 157, 199, 1 });
            swagometerScreenBits[23] = new BigInteger(new byte[] { 156, 3, 199, 1, 206, 57, 206, 193, 123, 238, 57, 207, 59, 0, 192, 157, 199, 1 });
            swagometerScreenBits[24] = new BigInteger(new byte[] { 156, 3, 199, 1, 206, 57, 206, 195, 115, 238, 56, 207, 123, 0, 192, 157, 199, 1 });
            swagometerScreenBits[25] = new BigInteger(new byte[] { 156, 243, 199, 193, 207, 57, 206, 231, 115, 239, 56, 207, 251, 3, 252, 157, 199, 1 });
            swagometerScreenBits[26] = new BigInteger(new byte[] { 156, 243, 199, 225, 207, 57, 206, 255, 241, 239, 56, 207, 243, 7, 252, 157, 199, 1 });
            swagometerScreenBits[27] = new BigInteger(new byte[] { 156, 251, 199, 225, 207, 57, 142, 255, 241, 239, 56, 207, 243, 7, 254, 157, 199, 1 });
            swagometerScreenBits[28] = new BigInteger(new byte[] { 156, 251, 195, 225, 207, 57, 142, 255, 241, 231, 56, 207, 243, 7, 254, 156, 199, 1 });
            swagometerScreenBits[29] = new BigInteger(new byte[] { 156, 251, 195, 225, 199, 57, 14, 255, 224, 231, 56, 199, 243, 7, 254, 156, 199, 1 });
            swagometerScreenBits[30] = new BigInteger(new byte[] { 156, 251, 195, 225, 199, 57, 14, 255, 224, 231, 56, 135, 225, 7, 254, 28, 195, 1 });
            swagometerScreenBits[31] = new BigInteger(new byte[] { 12, 243, 129, 225, 195, 48, 12, 126, 192, 195, 48, 134, 193, 7, 124, 24, 131, 1 });
            swagometerScreenBits[32] = new BigInteger(new byte[] { 8, 224, 0, 192, 1, 0, 0, 60, 128, 1, 0, 0, 128, 3, 56 });
            swagometerScreenBits[33] = new BigInteger(new byte[] { 0 });
            swagometerScreenBits[34] = new BigInteger(new byte[] { 0 });
            return swagometerScreenBits;
        }

        private static BigInteger[] BuildDevScSplashScreen()
        {
            var splashScreenBits = new BigInteger[35];
            splashScreenBits[0] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 179, 159, 15, 0, 0, 0, 0, 0, 2 });
            splashScreenBits[1] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 179, 223, 15, 0, 0, 0, 0, 192, 3 });
            splashScreenBits[2] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 51, 216, 12, 0, 0, 0, 0, 240, 3 });
            splashScreenBits[3] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 179, 223, 12, 0, 0, 0, 0, 252, 1 });
            splashScreenBits[4] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 179, 223, 12, 0, 0, 0, 0, 255, 1 });
            splashScreenBits[5] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 63, 216, 12, 0, 0, 0, 192, 255, 0 });
            splashScreenBits[6] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 158, 223, 15, 0, 0, 0, 224, 255, 0 });
            splashScreenBits[7] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 140, 159, 15, 0, 0, 0, 240, 127 });
            splashScreenBits[8] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 248, 63 });
            splashScreenBits[9] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 204, 126, 51, 207, 7, 0, 0, 0, 252, 63 });
            splashScreenBits[10] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 204, 126, 179, 223, 15, 0, 0, 0, 254, 63 });
            splashScreenBits[11] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 204, 24, 179, 25, 12, 0, 0, 0, 255, 63 });
            splashScreenBits[12] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 252, 24, 179, 153, 15, 0, 0, 128, 255, 63 });
            splashScreenBits[13] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 252, 24, 179, 217, 7, 0, 0, 192, 255, 63 });
            splashScreenBits[14] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 204, 24, 179, 217, 0, 0, 0, 224, 255, 63 });
            splashScreenBits[15] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 204, 24, 191, 223, 15, 0, 0, 224, 255, 63 });
            splashScreenBits[16] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 204, 24, 30, 143, 15, 0, 0, 224, 255, 63 });
            splashScreenBits[17] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 240, 255, 63 });
            splashScreenBits[18] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 252, 62, 30, 207, 7, 0, 0, 240, 255, 63 });
            splashScreenBits[19] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 252, 126, 191, 223, 15, 0, 0, 248, 255, 63 });
            splashScreenBits[20] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 48, 96, 179, 25, 12, 0, 0, 248, 255, 63 });
            splashScreenBits[21] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 48, 124, 179, 25, 12, 0, 0, 252, 255, 127 });
            splashScreenBits[22] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 48, 62, 191, 25, 12, 0, 0, 252, 255, 255, 0 });
            splashScreenBits[23] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 48, 6, 191, 25, 12, 0, 0, 254, 255, 255, 0 });
            splashScreenBits[24] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 48, 126, 179, 223, 15, 0, 0, 254, 255, 255, 1 });
            splashScreenBits[25] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 48, 124, 51, 207, 7, 0, 0, 254, 255, 255, 3 });
            splashScreenBits[26] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 254, 255, 255, 7 });
            splashScreenBits[27] = new BigInteger(new byte[] { 0, 0, 0, 224, 243, 155, 253, 62, 63, 159, 15, 0, 0, 254, 255, 255, 15 });
            splashScreenBits[28] = new BigInteger(new byte[] { 0, 0, 0, 224, 247, 155, 253, 126, 191, 223, 15, 0, 0, 254, 255, 255, 31 });
            splashScreenBits[29] = new BigInteger(new byte[] { 0, 0, 0, 0, 198, 216, 193, 96, 176, 217, 12, 0, 0, 254, 255, 255, 127 });
            splashScreenBits[30] = new BigInteger(new byte[] { 0, 0, 0, 192, 199, 248, 253, 124, 191, 223, 15, 0, 0, 254, 255, 255, 255, 0 });
            splashScreenBits[31] = new BigInteger(new byte[] { 0, 0, 0, 224, 195, 184, 253, 62, 63, 159, 15, 0, 0, 254, 7, 0, 192, 7 });
            splashScreenBits[32] = new BigInteger(new byte[] { 0, 0, 0, 96, 192, 152, 193, 6, 176, 25, 12, 0, 0, 126 });
            splashScreenBits[33] = new BigInteger(new byte[] { 0, 0, 0, 224, 199, 152, 253, 126, 191, 25, 12, 0, 0, 30 });
            splashScreenBits[34] = new BigInteger(new byte[] { 0, 0, 0, 192, 199, 152, 253, 124, 191, 25, 12, 0, 0, 3 });
            return splashScreenBits;
        }
    }
}
