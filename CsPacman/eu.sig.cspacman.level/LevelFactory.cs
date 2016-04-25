using System.Collections.Generic;

using eu.sig.cspacman.board;
using eu.sig.cspacman.npc;
using eu.sig.cspacman.npc.ghost;
using eu.sig.cspacman.sprite;

namespace eu.sig.cspacman.level
{
    public class LevelFactory
    {

        private const int GHOSTS = 4;
        private const int BLINKY = 0;
        private const int INKY = 1;
        private const int PINKY = 2;
        private const int CLYDE = 3;

        private readonly int PELLET_VALUE = 10;

        private readonly PacManSprites sprites;

        private int ghostIndex;

        private readonly GhostFactory ghostFact;

        public LevelFactory(PacManSprites spriteStore, GhostFactory ghostFactory)
        {
            this.sprites = spriteStore;
            this.ghostIndex = -1;
            this.ghostFact = ghostFactory;
        }

        public Level CreateLevel(Board board, IList<NPC> ghosts,
                IList<Square> startPositions)
        {

            // We'll adopt the simple collision map for now.
            ICollisionMap collisionMap = new PlayerCollisions();

            return new Level(board, ghosts, startPositions, collisionMap);
        }

        public NPC CreateGhost()
        {
            ghostIndex++;
            ghostIndex %= GHOSTS;
            switch (ghostIndex)
            {
                case BLINKY:
                    return ghostFact.createBlinky();
                case INKY:
                    return ghostFact.createInky();
                case PINKY:
                    return ghostFact.createPinky();
                case CLYDE:
                    return ghostFact.createClyde();
                default:
                    return new RandomGhost(sprites.getGhostSprite(GhostColor.RED));
            }
        }

        public Pellet CreatePellet()
        {
            return new Pellet(PELLET_VALUE, sprites.getPelletSprite());
        }

        internal sealed class RandomGhost : Ghost
        {

            private const int DELAY = 175;

            internal RandomGhost(IDictionary<Direction, ISprite> ghostSprite) : base(ghostSprite)
            {
            }

            public override int getInterval()
            {
                return DELAY;
            }

            public override Direction NextMove()
            {
                return randomMove();
            }
        }
    }
}
