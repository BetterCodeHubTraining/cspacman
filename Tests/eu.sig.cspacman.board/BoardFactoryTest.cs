using NUnit.Framework;
using Moq;

using eu.sig.cspacman.sprite;

namespace eu.sig.cspacman.board
{
	[TestFixture]
	public class BoardFactoryTest {

		private BoardFactory factory;

		[SetUp]
		public void SetUp() {
			PacManSprites sprites = new Mock<PacManSprites>().Object;
			factory = new BoardFactory(sprites);
		}

		[Test]
		public void worldIsRound() {
			Square s = new BasicSquare();
			Square[,] grid = new Square[,]{{s}};
			factory.CreateBoard(grid);
			Assert.AreEqual(s, s.GetSquareAt(Direction.NORTH));
			Assert.AreEqual(s, s.GetSquareAt(Direction.SOUTH));
			Assert.AreEqual(s, s.GetSquareAt(Direction.WEST));
			Assert.AreEqual(s, s.GetSquareAt(Direction.EAST));
		}

		[Test]
		public void connectedEast() {
			Square s1 = new BasicSquare();
			Square s2 = new BasicSquare();
			Square[,] grid = new Square[,]{{s1}, {s2}};
			factory.CreateBoard(grid);
			Assert.AreEqual(s2, s1.GetSquareAt(Direction.EAST));
			Assert.AreEqual(s1, s2.GetSquareAt(Direction.EAST));
		}

		[Test]
		public void connectedWest() {
			Square s1 = new BasicSquare();
			Square s2 = new BasicSquare();
			Square[,] grid = new Square[,]{{s1}, {s2}};
			factory.CreateBoard(grid);
			Assert.AreEqual(s2, s1.GetSquareAt(Direction.WEST));
			Assert.AreEqual(s1, s2.GetSquareAt(Direction.WEST));
		}

		[Test]
		public void connectedNorth() {
			Square s1 = new BasicSquare();
			Square s2 = new BasicSquare();
			Square[,] grid = new Square[,]{{s1, s2}};
			factory.CreateBoard(grid);
			Assert.AreEqual(s2, s1.GetSquareAt(Direction.NORTH));
			Assert.AreEqual(s1, s2.GetSquareAt(Direction.NORTH));
		}

		[Test]		
		public void connectedSouth() {
			Square s1 = new BasicSquare();
			Square s2 = new BasicSquare();
			Square[,] grid = new Square[,]{{s1, s2}};
			factory.CreateBoard(grid);
			Assert.AreEqual(s2, s1.GetSquareAt(Direction.SOUTH));
			Assert.AreEqual(s1, s2.GetSquareAt(Direction.SOUTH));
		}
	}
}
