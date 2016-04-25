using eu.sig.cspacman.sprite;

namespace eu.sig.cspacman.npc.ghost
{
    public class GhostFactory
    {
        private readonly PacManSprites sprites;

        public GhostFactory(PacManSprites spriteStore)
        {
            this.sprites = spriteStore;
        }

        public Ghost createBlinky()
        {
            return new Blinky(sprites.getGhostSprite(GhostColor.RED));
        }

        public Ghost createPinky()
        {
            return new Pinky(sprites.getGhostSprite(GhostColor.PINK));
        }

        public Ghost createInky()
        {
            return new Inky(sprites.getGhostSprite(GhostColor.CYAN));
        }

        public Ghost createClyde()
        {
            return new Clyde(sprites.getGhostSprite(GhostColor.ORANGE));
        }
    }
}
