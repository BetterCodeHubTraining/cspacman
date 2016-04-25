using System.Collections.Generic;

namespace eu.sig.cspacman.board
{
    public class Direction
    {
        public static readonly Direction NORTH = new Direction(0, -1);

        public static readonly Direction SOUTH = new Direction(0, 1);

        public static readonly Direction WEST = new Direction(-1, 0);

        public static readonly Direction EAST = new Direction(1, 0);

        public int DeltaX { get; }

        public int DeltaY { get; }

        public static IEnumerable<Direction> Values
        {
            get
            {
                yield return NORTH;
                yield return SOUTH;
                yield return WEST;
                yield return EAST;
            }
        }

        private Direction(int deltaX, int deltaY)
        {
            this.DeltaX = deltaX;
            this.DeltaY = deltaY;
        }

    }
}
