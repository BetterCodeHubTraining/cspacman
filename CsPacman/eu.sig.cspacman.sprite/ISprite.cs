using System.Drawing;

namespace eu.sig.cspacman.sprite
{
	public interface ISprite {

		void draw(Graphics g, int x, int y, int width, int height);

		ISprite split(int x, int y, int width, int height);

		int getWidth();

		int getHeight();
	}
}