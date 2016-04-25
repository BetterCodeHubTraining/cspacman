using System.Collections.Generic;

using eu.sig.cspacman.board;
using eu.sig.cspacman.sprite;

namespace eu.sig.cspacman.npc.ghost
{
	public abstract class Ghost : NPC {

		private IDictionary<Direction, ISprite> sprites;

		protected Ghost(IDictionary<Direction, ISprite> spriteMap) {
			this.sprites = spriteMap;
		}

		public override ISprite Sprite {
			get { return sprites [Direction]; }
		}

		protected Direction randomMove() {
			Square square = Square;
			IList<Direction> directions = new List<Direction>();
			foreach (Direction d in Direction.Values) {
				if (square.GetSquareAt(d).IsAccessibleTo(this)) {
					directions.Add(d);
				}
			}
			if (directions.Count == 0) {
				return null;
			}
			int i = new System.Random().Next(directions.Count);
			return directions[i];
		}
	}
}
