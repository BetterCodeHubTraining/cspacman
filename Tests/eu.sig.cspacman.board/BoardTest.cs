using NUnit.Framework;
using Moq;

namespace eu.sig.cspacman.board
{
	[TestFixture]
	public class BoardTest {

		private Board board;

		private Square x0y0 = new Mock<Square>().Object;
		private Square x0y1 = new Mock<Square>().Object;
		private Square x0y2 = new Mock<Square>().Object;
		private Square x1y0 = new Mock<Square>().Object;
		private Square x1y1 = new Mock<Square>().Object;
		private Square x1y2 = new Mock<Square>().Object;

		private readonly int maxWidth = 2;
		private readonly int maxHeight = 3;

		[SetUp]
		public void SetUp() {
			Square[,] grid = new Square[maxWidth,maxHeight];
			grid[0,0] = x0y0;
			grid[0,1] = x0y1;
			grid[0,2] = x0y2;
			grid[1,0] = x1y0;
			grid[1,1] = x1y1;
			grid[1,2] = x1y2;
			board = new Board(grid);
		}

		[Test]
		public void verifyWidth() {
			Assert.AreEqual(maxWidth, board.Width);
		}

		[Test]
		public void verifyHeight() {
			Assert.AreEqual(maxHeight, board.Height);
		}

		[Test]
		public void verifyX0Y0() {
			Assert.AreEqual(x0y0, board.SquareAt(0, 0));
		}

		[Test]
		public void verifyX1Y2() {
			Assert.AreEqual(x1y2, board.SquareAt(1, 2));
		}

		[Test]
		public void verifyX0Y1() {
			Assert.AreEqual(x0y1, board.SquareAt(0, 1));
		}
	}
}
