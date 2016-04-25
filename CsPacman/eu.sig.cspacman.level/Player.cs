using System.Collections.Generic;

using eu.sig.cspacman.board;
using eu.sig.cspacman.sprite;

namespace eu.sig.cspacman.level
{

    public class Player : Unit
    {

        public int Score { get; private set; }

        private readonly IDictionary<Direction, ISprite> sprites;

        private readonly AnimatedSprite deathSprite;

        private bool alive = true;

        public Player(IDictionary<Direction, ISprite> spriteMap, AnimatedSprite deathAnimation)
        {
            this.Score = 0;
            this.alive = true;
            this.sprites = spriteMap;
            this.deathSprite = deathAnimation;
            deathSprite.setAnimating(false);
        }

        public bool IsAlive
        {
            get { return alive; }
            set
            {
                if (alive)
                {
                    deathSprite.setAnimating(false);
                }
                if (!alive)
                {
                    deathSprite.restart();
                }
                this.alive = value;
            }
        }

        public override ISprite Sprite
        {
            get
            {
                if (IsAlive)
                {
                    return sprites[Direction];
                }
                return deathSprite;
            }
        }

        public void AddPoints(int points)
        {
            Score += points;
        }
    }
}
