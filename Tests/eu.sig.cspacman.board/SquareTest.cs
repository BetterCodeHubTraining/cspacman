using System;
using System.Linq;
using NUnit.Framework;
using Moq;

namespace eu.sig.cspacman.board
{
	[TestFixture]
	public class SquareTest {

		private Square square;

		[SetUp]
		public void SetUp() {
			square = new BasicSquare();
		}

		[Test]
		public void testOccupy() {
			Unit occupant = new Mock<Unit>().Object;
			square.put(occupant);

			Assert.IsTrue(square.Occupants.Contains(occupant));
		}

		[Test]
		public void testLeave() {
			Unit occupant = new Mock<Unit>().Object;
			square.put(occupant);
			square.remove(occupant);

			Assert.IsFalse(square.Occupants.Contains(occupant));
		}

		[Test]
		public void testOrder() {
			Unit o1 = new Mock<Unit>().Object;
			Unit o2 = new Mock<Unit>().Object;
			square.put(o1);
			square.put(o2);

			Object[] occupantsAsArray = square.Occupants.ToArray();
			Assert.AreEqual(new Object[] { o1, o2 }, occupantsAsArray);
		}
	}
}
