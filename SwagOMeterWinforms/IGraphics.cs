using System.Drawing;

namespace PinballSwagOMeter
{
    public interface IGraphics
    {
        void DrawImage(Image image, int x, int y, int width, int height);
    }
}
