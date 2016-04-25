using eu.sig.cspacman.sprite;

namespace eu.sig.cspacman.board
{
    class BasicSquare : Square
    {

        internal BasicSquare() : base()
        {
        }

        public override bool IsAccessibleTo(Unit unit)
        {
            return true;
        }

        public override ISprite Sprite
        {
            get
            {
                return null;
            }
        }
    }
}
