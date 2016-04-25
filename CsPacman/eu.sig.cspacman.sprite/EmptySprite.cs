using System.Drawing;

namespace eu.sig.cspacman.sprite
{
	public class EmptySprite : ISprite {

		public void draw(Graphics g, int x, int y, int width, int height) {
			// nothing to draw.
		}

		public ISprite split(int x, int y, int width, int height) {
			return new EmptySprite();
		}

		public int getWidth() {
			return 0;
		}

		public int getHeight() {
			return 0;
		}
	}
}
