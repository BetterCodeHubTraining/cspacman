using System.Drawing;
using System.Windows.Forms;

using eu.sig.cspacman.game;
using eu.sig.cspacman.board;

namespace eu.sig.cspacman.ui
{
	internal class BoardPanel : Panel {

		private readonly Color BACKGROUND_COLOR = Color.Black;

		private const int SQUARE_SIZE = 16;

		private readonly Game game;

		internal BoardPanel(Game game) : base() {
			System.Diagnostics.Debug.Assert(game != null);
			this.game = game;

			IBoard board = game.Level.Board;

			int w = board.Width * SQUARE_SIZE;
			int h = board.Height * SQUARE_SIZE;

			Size = new Size(w, h);
		}

		protected override void OnPaint(PaintEventArgs e) {
			System.Diagnostics.Debug.Assert(e != null);
            base.OnPaint(e);
            using (BufferedGraphics buffer = BufferedGraphicsManager.Current
                .Allocate(e.Graphics, DisplayRectangle))
            {
                render(game.Level.Board, buffer.Graphics, Size);
                buffer.Render();
            }
		}

		private void render(IBoard board, Graphics g, Size window) {
			int cellW = window.Width / board.Width;
			int cellH = window.Height / board.Height;

			g.Clear(BACKGROUND_COLOR);

			for (int y = 0; y < board.Height; y++) {
				for (int x = 0; x < board.Width; x++) {
					int cellX = x * cellW;
					int cellY = y * cellH;
					Square square = board.SquareAt(x, y);
					render(square, g, cellX, cellY, cellW, cellH);
				}
			}
		}

		private void render(Square square, Graphics g, int x, int y, int w, int h) {
			square.Sprite.draw(g, x, y, w, h);
			foreach (Unit unit in square.Occupants) {
				unit.Sprite.draw(g, x, y, w, h);
			}
		}
	}
}
