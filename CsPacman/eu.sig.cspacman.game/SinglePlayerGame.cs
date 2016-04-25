using System.Collections.Generic;
using System.Collections.Immutable;

using eu.sig.cspacman.board;
using eu.sig.cspacman.level;

namespace eu.sig.cspacman.game
{
    public class SinglePlayerGame : Game
    {

        private readonly Player player;

        public override IList<Player> Players
        {
            get
            {
                return (new List<Player>(new Player[] { player })).ToImmutableList();
            }
        }

        // Property accessor is called in the constructor, so sealed to avoid problems when overridden.
        sealed override public Level Level { get; }

        internal SinglePlayerGame(Player p, Level l)
        {
            System.Diagnostics.Debug.Assert(p != null);
            System.Diagnostics.Debug.Assert(l != null);

            player = p;
            Level = l;
            Level.RegisterPlayer(p);
        }

        public void MoveUp()
        {
            Move(player, Direction.NORTH);
        }

        public void MoveDown()
        {
            Move(player, Direction.SOUTH);
        }

        public void MoveLeft()
        {
            Move(player, Direction.WEST);
        }

        public void MoveRight()
        {
            Move(player, Direction.EAST);
        }
    }
}
