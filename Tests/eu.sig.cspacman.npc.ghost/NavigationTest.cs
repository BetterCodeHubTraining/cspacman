using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;

using eu.sig.cspacman.board;
using eu.sig.cspacman.level;
using eu.sig.cspacman.sprite;

namespace eu.sig.cspacman.npc.ghost
{
	[TestFixture]
	public class NavigationTest {

		private MapParser parser;

		[SetUp]
		public void SetUp() {
			PacManSprites sprites = new PacManSprites();
			parser = new MapParser(new LevelFactory(sprites, new GhostFactory(
				sprites)), new BoardFactory(sprites));
		}

		[Test]
		public void testShortestPathEmpty() {
			IBoard b = parser.ParseMap(new List<string>(new string[] {" "})).Board;
			Square s1 = b.SquareAt(0, 0);
			Square s2 = b.SquareAt(0, 0);
			IList<Direction> path = Navigation
				.shortestPath(s1, s2, new Mock<Unit>().Object);
			Assert.AreEqual(0, path.Count);
		}

		[Test]
		public void testNoShortestPath() {
			IBoard b = parser
				.ParseMap(new List<string>(new string[] {"#####", "# # #", "#####"}))
				.Board;
			Square s1 = b.SquareAt(1, 1);
			Square s2 = b.SquareAt(3, 1);
			IList<Direction> path = Navigation
				.shortestPath(s1, s2, new Mock<Unit>().Object);
			Assert.IsNull(path);
		}

		[Test]
		public void testNoTraveller() {
			IBoard b = parser
				.ParseMap(new List<string>(new string[] {"#####", "# # #", "#####"}))
				.Board;
			Square s1 = b.SquareAt(1, 1);
			Square s2 = b.SquareAt(3, 1);
			IList<Direction> path = Navigation.shortestPath(s1, s2, null);
			Assert.AreEqual(new Direction[] { Direction.EAST, Direction.EAST },
				((List<Direction>)path).ToArray());
		}

		[Test]
		public void testSimplePath() {
			IBoard b = parser.ParseMap(new List<string>(new string[] {"####", "#  #", "####"}))
				.Board;
			Square s1 = b.SquareAt(1, 1);
			Square s2 = b.SquareAt(2, 1);
			IList<Direction> path = Navigation
				.shortestPath(s1, s2, new Mock<Unit>().Object);
			Assert.AreEqual(new Direction[] { Direction.EAST },
				((List<Direction>)path).ToArray());
		}

		[Test]
		public void testCornerPath() {
			IBoard b = parser.ParseMap(
				new List<string>(new string[] {"####", "#  #", "## #", "####"})).Board;
			Square s1 = b.SquareAt(1, 1);
			Square s2 = b.SquareAt(2, 2);
			IList<Direction> path = Navigation
				.shortestPath(s1, s2, new Mock<Unit>().Object);
			Assert.AreEqual(new Direction[] { Direction.EAST, Direction.SOUTH },
				((List<Direction>)path).ToArray());
		}

		[Test]
		public void testNearestUnit() {
			IBoard b = parser
				.ParseMap(new List<string>(new string[] {"#####", "# ..#", "#####"}))
				.Board;
			Square s1 = b.SquareAt(1, 1);
			Square s2 = b.SquareAt(2, 1);
			Square result = Navigation.findNearest(typeof(Pellet), s1).Square;
			Assert.AreEqual(s2, result);
		}

		[Test]
		public void testNoNearestUnit() {
			IBoard b = parser.ParseMap(new List<string>(new string[] {" "})).Board;
			Square s1 = b.SquareAt(0, 0);
			Unit unit = Navigation.findNearest(typeof(Pellet), s1);
			Assert.IsNull(unit);
		}

		[Test]
		public void testFullSizedLevel() {
			System.Reflection.Assembly myAssembly =
				System.Reflection.Assembly.GetExecutingAssembly();
			Stream input = myAssembly.GetManifestResourceStream("Tests.board.txt");
			IBoard b = parser.ParseMap(input).Board;
			Square s1 = b.SquareAt(1, 1);
			Unit unit = Navigation.findNearest(typeof(Ghost), s1);
			Assert.IsNotNull(unit);
		}
	}
}
