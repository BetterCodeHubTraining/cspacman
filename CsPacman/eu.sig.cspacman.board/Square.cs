using System.Collections.Generic;
using System.Collections.Immutable;
using eu.sig.cspacman.sprite;

namespace eu.sig.cspacman.board
{
    public abstract class Square
    {
		private IList<Unit> occupants;

		public IList<Unit> Occupants
        {
            get
            {
                return occupants.ToImmutableList();
            }
            private set { this.occupants = value; }
        }

        private readonly IDictionary<Direction, Square> neighbours;

        protected Square()
        {
            occupants = new List<Unit>();
            neighbours = new Dictionary<Direction, Square>();
        }

        public Square GetSquareAt(Direction direction)
        {
            return neighbours[direction];
        }

        public void Link(Square neighbour, Direction direction)
        {
            neighbours[direction] = neighbour;
        }

        public bool put(Unit occupant)
        {
            System.Diagnostics.Debug.Assert(occupant != null);
            if (!occupants.Contains(occupant))
            {
                occupants.Add(occupant);
                return true;
            }
            return false;
        }

        public void remove(Unit occupant)
        {
            System.Diagnostics.Debug.Assert(occupant != null);
            occupants.Remove(occupant);
        }

        protected bool invariant()
        {
            foreach (Unit occupant in occupants)
            {
                if (occupant.Square != this)
                {
                    return false;
                }
            }
            return true;
        }

        public abstract bool IsAccessibleTo(Unit unit);

        public abstract ISprite Sprite { get; }

    }
}
