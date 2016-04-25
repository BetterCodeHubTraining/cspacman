using NUnit.Framework;
using System.Reflection;

using eu.sig.cspacman.board;
using eu.sig.cspacman.level;
using eu.sig.cspacman.game;

[assembly: AssemblyVersionAttribute("1.0.0.0")]
namespace eu.sig.cspacman
{
	[TestFixture]
	public class LauncherSmokeTest {

		private Launcher launcher;

		[SetUp]
		public void SetUpPacman() {
			launcher = new Launcher();
			launcher.Launch();
		}

		[TearDown]
		public void tearDown() {
			launcher.Dispose();
		}

		[Test]
		public void smokeTest() {
			Game game = launcher.Game;
			Player player = game.Players[0];

			// start cleanly.
			Assert.IsFalse(game.IsInProgress());
			game.Start();
			Assert.IsTrue(game.IsInProgress());
			Assert.AreEqual(0, player.Score);

			// get points
			game.Move(player, Direction.EAST);
			Assert.AreEqual(10, player.Score);

			// now moving back does not change the score
			game.Move(player, Direction.WEST);
			Assert.AreEqual(10, player.Score);

			// try to move as far as we can
			move(game, Direction.EAST, 7);
			Assert.AreEqual(60, player.Score);

			// move towards the monsters
			move(game, Direction.NORTH, 6);
			Assert.AreEqual(120, player.Score);

			// no more points to earn here.
			move(game, Direction.WEST, 2);
			Assert.AreEqual(120, player.Score);

			move(game, Direction.NORTH, 2);

			// Sleeping in tests is generally a bad idea.
			// Here we do it just to let the monsters move.
			System.Threading.Thread.Sleep(750);

			// we're close to monsters, this will get us killed.
			move(game, Direction.WEST, 10);
			move(game, Direction.EAST, 10);
			Assert.IsFalse(player.IsAlive);

			game.Stop();
			Assert.IsFalse(game.IsInProgress());
		}

		public static void move(Game game, Direction dir, int numSteps) {
			Player player = game.Players[0];
			for (int i = 0; i < numSteps; i++) {
				game.Move(player, dir);
			}
		}
	}
}
