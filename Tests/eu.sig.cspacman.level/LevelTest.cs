using System.Collections.Generic;
using NUnit.Framework;
using Moq;

using eu.sig.cspacman.board;
using eu.sig.cspacman.sprite;
using eu.sig.cspacman.npc;

namespace eu.sig.cspacman.level
{
	[TestFixture]
	public class LevelTest {

		private Level level;

		private Mock<NPC> ghost = new Mock<NPC>();

		private Square square1 = Mock.Of<Square>();

		private Square square2 = Mock.Of<Square>();

		private IBoard board = Mock.Of<IBoard>();

		private AnimatedSprite aniSprite = new Mock<AnimatedSprite>(new ISprite[] { }, 0, true).Object;
		private IDictionary<Direction, ISprite> spriteMap = Mock.Of<IDictionary<Direction, ISprite>>();

		private ICollisionMap collisions = Mock.Of<ICollisionMap>();

		[SetUp]
		public void SetUp() {
			const int defaultInterval = 100;
			level = new Level(board, new List<NPC>(new NPC[] {ghost.Object}),
				new List<Square>(new Square[] {square1, square2}), collisions);
			ghost.Setup(g => g.getInterval()).Returns(defaultInterval);
		}

		[Test]
		public void noStart() {
			Assert.IsFalse(level.IsInProgress());
		}

		[Test]
		public void stop() {
			level.Stop();
			Assert.IsFalse(level.IsInProgress());
		}

		[Test]
		public void start() {
			level.Start();
			Assert.IsTrue(level.IsInProgress());
		}

		[Test]
		public void startStop() {
			level.Start();
			level.Stop();
			Assert.IsFalse(level.IsInProgress());
		}

		[Test]
		public void registerPlayer() {
			Mock<Player> p = new Mock<Player>(spriteMap, aniSprite);
			level.RegisterPlayer(p.Object);
			p.Verify(m => m.Occupy(square1));
		}

		[Test]
		public void registerPlayerTwice() {
			Mock<Player> p = new Mock<Player>(spriteMap, aniSprite);
			level.RegisterPlayer(p.Object);
			level.RegisterPlayer(p.Object);
			p.Verify(m => m.Occupy(square1), Times.Once());
		}

		[Test]
		public void registerSecondPlayer() {
			Mock<Player> p1 = new Mock<Player>(spriteMap, aniSprite);
			Mock<Player> p2 = new Mock<Player>(spriteMap, aniSprite);
			level.RegisterPlayer(p1.Object);
			level.RegisterPlayer(p2.Object);
			p2.Verify(p => p.Occupy(square2));
		}

		[Test]
		public void registerThirdPlayer() {
			Mock<Player> p1 = new Mock<Player>(spriteMap, aniSprite);
			Mock<Player> p2 = new Mock<Player>(spriteMap, aniSprite);
			Mock<Player> p3 = new Mock<Player>(spriteMap, aniSprite);
			level.RegisterPlayer(p1.Object);
			level.RegisterPlayer(p2.Object);
			level.RegisterPlayer(p3.Object);
			p3.Verify(p => p.Occupy(square1));
		}
	}
}
