using eu.sig.cspacman.sprite;

namespace eu.sig.cspacman.board
{
	public class BoardFactory {

		private readonly PacManSprites sprites;

		public BoardFactory(PacManSprites spriteStore) {
			this.sprites = spriteStore;
		}

		public Board CreateBoard(Square[,] grid) {
			System.Diagnostics.Debug.Assert(grid != null);

			Board board = new Board(grid);

			int width = board.Width;
			int height = board.Height;
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					Square square = grid[x, y];
					foreach (Direction dir in Direction.Values) {
						int dirX = (width + x + dir.DeltaX) % width;
						int dirY = (height + y + dir.DeltaY) % height;
						Square neighbour = grid[dirX,dirY];
						square.Link(neighbour, dir);
					}
				}
			}

			return board;
		}

		public Square createGround() {
			return new Ground(sprites.getGroundSprite());
		}

		public Square createWall() {
			return new Wall(sprites.getWallSprite());
		}

		private sealed class Wall : Square {

			private readonly ISprite background;

			internal Wall(ISprite sprite) {
				this.background = sprite;
			}

			public override bool IsAccessibleTo(Unit unit) {
				return false;
			}

			public override ISprite Sprite {
                get
                {
                    return background;
                }
            }
        }

		private sealed class Ground : Square {

			private readonly ISprite background;

			internal Ground(ISprite sprite) {
				this.background = sprite;
			}

			public override bool IsAccessibleTo(Unit unit) {
				return true;
			}

			public override ISprite Sprite {
                get
                {
                    return background;
                }
            }
        }
	}
}
