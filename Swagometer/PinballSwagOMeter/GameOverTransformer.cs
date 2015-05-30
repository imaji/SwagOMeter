using System.Numerics;

namespace PinballSwagOMeter
{
    public class GameOverTransformer : MatrixTransformer
    {
        private int _screenCounts;

        public override BigInteger[] Transform()
        {
            if (_screenCounts++ == 0)
            {
                SubsequentDelayMs = 2000;
                return BuildJohnForCredits();
            }
            KeepTimerRunning = false;
            return BuildMarkForCredits();
        }

        public BigInteger[] BuildJohnForCredits()
        {
            var bitPatterns = new BigInteger[35];
            bitPatterns[0] = new BigInteger(new byte[] { 0, 0, 0, 224, 243, 99, 248, 126, 190, 15 });
            bitPatterns[1] = new BigInteger(new byte[] { 0, 0, 0, 224, 247, 99, 252, 126, 191, 31, 0, 0, 0, 248, 1 });
            bitPatterns[2] = new BigInteger(new byte[] { 0, 0, 0, 0, 198, 96, 204, 96, 51, 24, 0, 0, 128, 255, 31 });
            bitPatterns[3] = new BigInteger(new byte[] { 0, 0, 0, 192, 199, 96, 204, 126, 63, 24, 0, 0, 224, 255, 255, 0 });
            bitPatterns[4] = new BigInteger(new byte[] { 0, 0, 0, 224, 195, 96, 204, 126, 62, 24, 0, 0, 240, 255, 255, 1 });
            bitPatterns[5] = new BigInteger(new byte[] { 0, 0, 0, 96, 192, 96, 204, 96, 51, 24, 0, 0, 252, 255, 255, 7 });
            bitPatterns[6] = new BigInteger(new byte[] { 0, 0, 0, 224, 199, 96, 252, 126, 179, 31, 0, 0, 255, 255, 255, 15 });
            bitPatterns[7] = new BigInteger(new byte[] { 0, 0, 0, 192, 199, 96, 248, 126, 179, 15, 0, 0, 255, 255, 255, 31 });
            bitPatterns[8] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 255, 255, 255, 31 });
            bitPatterns[9] = new BigInteger(new byte[] { 0, 0, 192, 204, 7, 248, 48, 60, 30, 24, 0, 128, 255, 255, 255, 63 });
            bitPatterns[10] = new BigInteger(new byte[] { 0, 0, 192, 236, 7, 248, 49, 126, 63, 24, 0, 192, 255, 255, 255, 63 });
            bitPatterns[11] = new BigInteger(new byte[] { 0, 0, 192, 108, 6, 128, 49, 102, 51, 24, 0, 192, 255, 255, 255, 127 });
            bitPatterns[12] = new BigInteger(new byte[] { 0, 0, 192, 207, 7, 128, 49, 96, 51, 24, 0, 192, 255, 192, 255, 127 });
            bitPatterns[13] = new BigInteger(new byte[] { 0, 0, 128, 199, 7, 128, 49, 110, 51, 24, 0, 192, 31, 192, 231, 127 });
            bitPatterns[14] = new BigInteger(new byte[] { 0, 0, 0, 99, 6, 128, 49, 102, 51, 24, 0, 192, 15, 192, 143, 127 });
            bitPatterns[15] = new BigInteger(new byte[] { 0, 0, 0, 227, 7, 248, 49, 126, 191, 31, 0, 192, 15, 192, 63, 126 });
            bitPatterns[16] = new BigInteger(new byte[] { 0, 0, 0, 195, 7, 248, 48, 60, 158, 31, 0, 192, 15, 131, 63, 126 });
            bitPatterns[17] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 192, 7, 14, 15, 126 });
            bitPatterns[18] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 204, 102, 158, 1, 0, 192, 7, 28, 6, 124 });
            bitPatterns[19] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 204, 102, 191, 1, 0, 192, 7, 28, 7, 124 });
            bitPatterns[20] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 236, 102, 179, 1, 0, 192, 3, 12, 6, 124 });
            bitPatterns[21] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 252, 126, 179, 1, 0, 192, 3, 0, 0, 120 });
            bitPatterns[22] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 220, 126, 179, 1, 0, 192, 3, 0, 0, 56 });
            bitPatterns[23] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 204, 102, 179, 25, 0, 192, 3, 0, 8, 56 });
            bitPatterns[24] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 204, 102, 191, 31, 0, 192, 1, 0, 14, 48 });
            bitPatterns[25] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 204, 102, 30, 15, 0, 128, 1, 254, 5, 16 });
            bitPatterns[26] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 1, 4, 4, 16 });
            bitPatterns[27] = new BigInteger(new byte[] { 102, 12, 216, 204, 51, 243, 192, 60, 159, 16, 0, 0, 1, 12, 6, 16 });
            bitPatterns[28] = new BigInteger(new byte[] { 102, 12, 216, 236, 55, 251, 193, 126, 191, 25, 0, 0, 2, 240, 1 });
            bitPatterns[29] = new BigInteger(new byte[] { 118, 12, 216, 108, 54, 155, 193, 102, 176, 31, 0, 0, 14, 0, 0, 14 });
            bitPatterns[30] = new BigInteger(new byte[] { 126, 12, 216, 15, 54, 155, 193, 96, 176, 31, 0, 0, 252, 3, 248, 31 });
            bitPatterns[31] = new BigInteger(new byte[] { 110, 12, 216, 239, 54, 155, 193, 110, 176, 25, 0, 224, 255, 0, 192, 127 });
            bitPatterns[32] = new BigInteger(new byte[] { 102, 12, 216, 108, 54, 155, 193, 102, 176, 25, 0, 0, 252, 0, 192, 7 });
            bitPatterns[33] = new BigInteger(new byte[] { 102, 140, 223, 236, 247, 251, 253, 126, 191, 25, 0, 0, 254, 0, 240, 15 });
            bitPatterns[34] = new BigInteger(new byte[] { 102, 140, 223, 204, 227, 241, 252, 60, 159, 25, 0, 128, 255, 3, 252, 63 });
            return bitPatterns;
        }

        public BigInteger[] BuildMarkForCredits()
        {
            var bitPatterns = new BigInteger[35];
            bitPatterns[0] = new BigInteger(new byte[] { 0, 0, 0, 224, 243, 99, 248, 126, 190, 15 });
            bitPatterns[1] = new BigInteger(new byte[] { 0, 0, 0, 227, 247, 99, 252, 126, 191, 31, 0, 0, 0, 224, 255, 0 });
            bitPatterns[2] = new BigInteger(new byte[] { 0, 0, 0, 3, 198, 96, 204, 96, 51, 24, 0, 0, 0, 56, 192, 3 });
            bitPatterns[3] = new BigInteger(new byte[] { 0, 0, 0, 192, 199, 96, 204, 126, 63, 24, 0, 0, 0, 6, 0, 14 });
            bitPatterns[4] = new BigInteger(new byte[] { 0, 0, 0, 224, 195, 96, 204, 126, 62, 24, 0, 0, 0, 3, 0, 28 });
            bitPatterns[5] = new BigInteger(new byte[] { 0, 0, 0, 99, 192, 96, 204, 96, 51, 24, 0, 0, 128, 3, 0, 48 });
            bitPatterns[6] = new BigInteger(new byte[] { 0, 0, 0, 227, 199, 96, 252, 126, 179, 31, 0, 0, 128, 1, 0, 48 });
            bitPatterns[7] = new BigInteger(new byte[] { 0, 0, 0, 192, 199, 96, 248, 126, 179, 15, 0, 0, 128, 0, 0, 48 });
            bitPatterns[8] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 224, 224, 48 });
            bitPatterns[9] = new BigInteger(new byte[] { 102, 62, 192, 231, 195, 152, 249, 60, 62, 15, 0, 0, 128, 243, 241, 49 });
            bitPatterns[10] = new BigInteger(new byte[] { 102, 63, 192, 239, 199, 152, 253, 126, 191, 31, 0, 0, 0, 251, 224, 59 });
            bitPatterns[11] = new BigInteger(new byte[] { 102, 51, 0, 12, 198, 152, 205, 102, 179, 25, 0, 0, 0, 247, 228, 25 });
            bitPatterns[12] = new BigInteger(new byte[] { 126, 62, 128, 15, 198, 248, 253, 102, 63, 24, 0, 0, 0, 52, 14, 12 });
            bitPatterns[13] = new BigInteger(new byte[] { 60, 62, 192, 7, 198, 248, 249, 126, 190, 27, 0, 0, 0, 4, 15, 12 });
            bitPatterns[14] = new BigInteger(new byte[] { 24, 51, 192, 0, 198, 152, 193, 126, 179, 25, 0, 0, 0, 12, 8, 6 });
            bitPatterns[15] = new BigInteger(new byte[] { 24, 63, 192, 239, 199, 152, 193, 102, 179, 31, 0, 0, 0, 12, 0, 3 });
            bitPatterns[16] = new BigInteger(new byte[] { 24, 62, 128, 239, 195, 152, 193, 102, 51, 15, 0, 0, 0, 200, 127, 1 });
            bitPatterns[17] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 1 });
            bitPatterns[18] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 204, 124, 158, 16, 0, 0, 0, 56, 12, 1 });
            bitPatterns[19] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 204, 126, 191, 25, 0, 0, 0, 224, 143, 193, 0 });
            bitPatterns[20] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 204, 102, 179, 31, 0, 0, 240, 193, 255, 224, 1 });
            bitPatterns[21] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 248, 126, 179, 31, 0, 0, 224, 7, 0, 248, 0 });
            bitPatterns[22] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 248, 124, 191, 25, 0, 0, 224, 63, 0, 255, 1 });
            bitPatterns[23] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 204, 102, 191, 25, 0, 0, 240, 255, 225, 255, 3 });
            bitPatterns[24] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 204, 102, 179, 25, 0, 0, 240, 255, 251, 239, 3 });
            bitPatterns[25] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 204, 102, 179, 25, 0, 0, 96, 224, 255, 1 });
            bitPatterns[26] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 255, 0 });
            bitPatterns[27] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 248, 252, 102, 158, 1, 0, 0, 0, 248, 251, 119 });
            bitPatterns[28] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 248, 253, 102, 191, 1, 0, 0, 224, 255, 225, 255, 1 });
            bitPatterns[29] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 128, 193, 118, 179, 1, 0, 0, 224, 127, 192, 255, 3 });
            bitPatterns[30] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 240, 253, 126, 179, 1, 0, 0, 224, 31, 128, 255, 3 });
            bitPatterns[31] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 248, 252, 110, 179, 1, 0, 0, 96, 15, 0, 252, 0 });
            bitPatterns[32] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 24, 192, 102, 179, 25, 0, 0, 0, 7, 0, 104 });
            bitPatterns[33] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 248, 253, 102, 191, 31, 0, 0, 0, 0, 0, 224, 0 });
            bitPatterns[34] = new BigInteger(new byte[] { 0, 0, 0, 0, 0, 240, 253, 102, 30, 15 });
            return bitPatterns;
        }

    }
}
