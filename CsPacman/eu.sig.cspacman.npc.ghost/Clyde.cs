using System.Collections.Generic;

using eu.sig.cspacman.board;
using eu.sig.cspacman.level;
using eu.sig.cspacman.sprite;

namespace eu.sig.cspacman.npc.ghost
{
    public class Clyde : Ghost
    {

        private readonly int SHYNESS = 8;

        private readonly int INTERVAL_VARIATION = 50;

        private readonly int MOVE_INTERVAL = 250;

        private readonly IDictionary<Direction, Direction> OPPOSITES = new Dictionary<Direction, Direction>
        {
            [Direction.NORTH] = Direction.SOUTH,
            [Direction.SOUTH] = Direction.NORTH,
            [Direction.WEST] = Direction.EAST,
            [Direction.EAST] = Direction.WEST
        };

        public Clyde(IDictionary<Direction, ISprite> spriteMap) : base(spriteMap)
        {
        }

        public override int getInterval()
        {
            // TODO: direct translation from Java, look up API
            return MOVE_INTERVAL + new System.Random().Next(INTERVAL_VARIATION);
        }

        public override Direction NextMove()
        {
            Square target = Navigation.findNearest(typeof(Player), Square).Square;
            if (target == null)
            {
                return randomMove();
            }

            IList<Direction> path = Navigation.shortestPath(Square, target, this);
            if (path != null && path.Count != 0)
            {
                Direction d = path[0];
                if (path.Count <= SHYNESS)
                {
                    Direction oppositeDir = OPPOSITES[d];
                    return oppositeDir;
                }
                return d;
            }
            return randomMove();
        }
    }
}
