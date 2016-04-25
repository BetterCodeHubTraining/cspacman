using System.Collections.Generic;

using eu.sig.cspacman.board;
using eu.sig.cspacman.level;
using eu.sig.cspacman.sprite;

namespace eu.sig.cspacman.npc.ghost
{
    public class Inky : Ghost
    {

        private readonly int SQUARES_AHEAD = 2;

        private readonly int INTERVAL_VARIATION = 50;

        private readonly int MOVE_INTERVAL = 250;

        public Inky(IDictionary<Direction, ISprite> spriteMap) : base(spriteMap)
        {
        }

        public override int getInterval()
        {
            return MOVE_INTERVAL + new System.Random().Next(INTERVAL_VARIATION);
        }

        public override Direction NextMove()
        {
            Unit blinky = Navigation.findNearest(typeof(Blinky), Square);
            if (blinky == null)
            {
                return randomMove();
            }

            Unit player = Navigation.findNearest(typeof(Player), Square);
            if (player == null)
            {
                return randomMove();
            }

            Direction targetDirection = player.Direction;
            Square playerDestination = player.Square;
            for (int i = 0; i < SQUARES_AHEAD; i++)
            {
                playerDestination = playerDestination.GetSquareAt(targetDirection);
            }

            Square destination = playerDestination;
            IList<Direction> firstHalf = Navigation.shortestPath(blinky.Square,
                    playerDestination, null);
            if (firstHalf == null)
            {
                Direction d = randomMove();
                return d;
            }

            foreach (Direction d in firstHalf)
            {
                destination = playerDestination.GetSquareAt(d);
            }

            IList<Direction> path = Navigation.shortestPath(Square,
                    destination, this);
            if (path != null && path.Count != 0)
            {
                return path[0];
            }
            return randomMove();
        }
    }
}
