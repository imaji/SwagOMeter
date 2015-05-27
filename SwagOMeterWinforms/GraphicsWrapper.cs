using System;
using System.Drawing;

namespace PinballSwagOMeter
{
    public class GraphicsWrapper : IGraphics, IDisposable
    {
        private Graphics _graphics;
        public GraphicsWrapper(Graphics graphics)
        {
            _graphics = graphics;
        }

        public void DrawImage(Image image, int x, int y, int width, int height)
        {
            _graphics.DrawImage(image, x, y, width, height);
        }

        public void Dispose()
        {
            _graphics.Dispose();
        }
    }
}
