using System;
using System.Collections.Generic;

using eu.sig.cspacman.board;
using eu.sig.cspacman.level;
using eu.sig.cspacman.sprite;

namespace eu.sig.cspacman.npc.ghost
{
	public class Blinky : Ghost {

		private const int INTERVAL_VARIATION = 50;

		private const int MOVE_INTERVAL = 250;

		public Blinky(IDictionary<Direction, ISprite> spriteMap) : base(spriteMap) {
		}

		public override int getInterval() {
			// TODO Blinky should speed up when there are a few pellets left, but he
			// has no way to find out how many there are.
			return MOVE_INTERVAL + new Random().Next(INTERVAL_VARIATION);
		}

		public override Direction NextMove() {
			// TODO Blinky should patrol his corner every once in a while
			// TODO Implement his actual behaviour instead of simply chasing.
			Square target = Navigation.findNearest(typeof(Player), Square).Square;

			if (target == null) {
				return randomMove();
			}

			IList<Direction> path = Navigation.shortestPath(Square, target,
					this);
			if (path != null && path.Count != 0) {
				return path[0];
			}
			return randomMove();
		}
	}
}
