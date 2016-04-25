using eu.sig.cspacman.board;

namespace eu.sig.cspacman.npc
{
	public abstract class NPC : Unit {

		public abstract int getInterval();

		public abstract Direction NextMove();

	}
}
