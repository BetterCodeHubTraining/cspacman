using eu.sig.cspacman.sprite;

namespace eu.sig.cspacman.board
{
    public abstract class Unit
    {

        private Square square;

        public Square Square
        {
            get
            {
                System.Diagnostics.Debug.Assert(Invariant());
                return square;
            }
        }


        public Direction Direction { get; set; }


        public abstract ISprite Sprite { get; }

        protected Unit()
        {
            this.Direction = Direction.EAST;
        }

        // Marked virtual because Moq needs to mock this in our unit tests
        public virtual void Occupy(Square target)
        {
            System.Diagnostics.Debug.Assert(target != null);

            if (square != null)
            {
                square.remove(this);
            }
            square = target;
            target.put(this);
            System.Diagnostics.Debug.Assert(Invariant());
        }

        public void LeaveSquare()
        {
            if (square != null)
            {
                square.remove(this);
                square = null;
            }
        }

        protected bool Invariant()
        {
            if (square != null)
            {
                return square.Occupants.Contains(this);
            }
            return true;
        }

    }
}
