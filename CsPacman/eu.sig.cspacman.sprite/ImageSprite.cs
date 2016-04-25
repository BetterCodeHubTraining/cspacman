using System.Drawing;

namespace eu.sig.cspacman.sprite
{
    public class ImageSprite : ISprite
    {

        private readonly Image image;

        public ImageSprite(Image img)
        {
            this.image = img;
        }

        public void draw(Graphics g, int x, int y, int width, int height)
        {
            g.DrawImage(image, x, y);
        }

        public ISprite split(int x, int y, int width, int height)
        {
            // TODO: maybe factor out Image (replace it by Bitmap everywhere)?
            System.Diagnostics.Debug.Assert(image is Bitmap);
            if (withinImage(x, y) && withinImage(x + width - 1, y + height - 1))
            {
                Bitmap newImage = ((Bitmap)image).Clone(new Rectangle(x, y, width, height), image.PixelFormat);
                return new ImageSprite(newImage);
            }
            return new EmptySprite();
        }

        private bool withinImage(int x, int y)
        {
            return x < image.Width && x >= 0 && y < image.Height && y >= 0;
        }

        public int getWidth()
        {
            return image.Width;
        }

        public int getHeight()
        {
            return image.Height;
        }
    }
}
