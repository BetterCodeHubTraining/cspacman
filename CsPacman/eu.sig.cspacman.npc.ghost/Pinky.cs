using System.Collections.Generic;

using eu.sig.cspacman.board;
using eu.sig.cspacman.level;
using eu.sig.cspacman.sprite;

namespace eu.sig.cspacman.npc.ghost
{
    public class Pinky : Ghost
    {

        private readonly int SQUARES_AHEAD = 4;

        private readonly int INTERVAL_VARIATION = 50;

        private readonly int MOVE_INTERVAL = 200;

        public Pinky(IDictionary<Direction, ISprite> spriteMap) : base(spriteMap)
        {
        }

        public override int getInterval()
        {
            return MOVE_INTERVAL + new System.Random().Next(INTERVAL_VARIATION);
        }

        public override Direction NextMove()
        {
            Unit player = Navigation.findNearest(typeof(Player), Square);
            if (player == null)
            {
                Direction d = randomMove();
                return d;
            }

            Direction targetDirection = player.Direction;
            Square destination = player.Square;
            for (int i = 0; i < SQUARES_AHEAD; i++)
            {
                destination = destination.GetSquareAt(targetDirection);
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
