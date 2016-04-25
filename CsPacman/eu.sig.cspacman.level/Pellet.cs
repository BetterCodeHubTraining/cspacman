using eu.sig.cspacman.board;
using eu.sig.cspacman.sprite;

namespace eu.sig.cspacman.level
{
    public class Pellet : Unit
    {

        private readonly ISprite image;

		public override ISprite Sprite
        {
            get { return image; }
        }

        public int Value { get; }

        public Pellet(int points, ISprite sprite)
        {
            this.image = sprite;
            this.Value = points;
        }
    }
}
