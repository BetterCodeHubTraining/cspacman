using eu.sig.cspacman.sprite;

namespace eu.sig.cspacman.level
{
    public class PlayerFactory
    {
        private readonly PacManSprites sprites;

        public PlayerFactory(PacManSprites spriteStore)
        {
            this.sprites = spriteStore;
        }

        public Player CreatePacMan()
        {
            return new Player(sprites.getPacmanSprites(),
                    sprites.getPacManDeathAnimation());
        }
    }
}
