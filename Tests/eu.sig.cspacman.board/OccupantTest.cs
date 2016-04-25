using NUnit.Framework;

namespace eu.sig.cspacman.board
{
	[TestFixture]
	public class OccupantTest {

		private Unit unit;

		[SetUp]
		public void SetUp() {
			unit = new BasicUnit();
		}

		[Test]
		public void noStartSquare() {
			Assert.IsNull(unit.Square);
		}

		[Test]
		public void testOccupy() {
			Square target = new BasicSquare();
			unit.Occupy(target);
			Assert.That(unit.Square, Is.EqualTo(target));
		}
	}
}
