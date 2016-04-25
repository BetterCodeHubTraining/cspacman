using System.IO;
using System.Collections.Generic;

using eu.sig.cspacman.board;
using eu.sig.cspacman.npc.ghost;

namespace eu.sig.cspacman.sprite
{
    public class PacManSprites : SpriteStore
    {

        private readonly Direction[] DIRECTIONS = { Direction.NORTH,
            Direction.EAST, Direction.SOUTH, Direction.WEST };

        private const int SPRITE_SIZE = 16;

        private const int PACMAN_ANIMATION_FRAMES = 4;

        private const int PACMAN_DEATH_FRAMES = 11;

        private const int GHOST_ANIMATION_FRAMES = 2;

        private const int ANIMATION_DELAY = 200;

        public IDictionary<Direction, ISprite> getPacmanSprites()
        {
            return DirectionSprite("Sprites.pacman.png", PACMAN_ANIMATION_FRAMES);
        }

        public AnimatedSprite getPacManDeathAnimation()
        {
            string resource = "Sprites.dead.png";

            ISprite baseImage = loadSprite(resource);
            AnimatedSprite animation = createAnimatedSprite(baseImage, PACMAN_DEATH_FRAMES,
                    ANIMATION_DELAY, false);
            animation.setAnimating(false);

            return animation;
        }

        private IDictionary<Direction, ISprite> DirectionSprite(string resource, int frames)
        {
            IDictionary<Direction, ISprite> sprite = new Dictionary<Direction, ISprite>();

            ISprite baseImage = loadSprite(resource);
            for (int i = 0; i < DIRECTIONS.Length; i++)
            {
                ISprite directionSprite = baseImage.split(0, i * SPRITE_SIZE, frames
                        * SPRITE_SIZE, SPRITE_SIZE);
                AnimatedSprite animation = createAnimatedSprite(directionSprite,
                        frames, ANIMATION_DELAY, true);
                animation.setAnimating(true);
                sprite[DIRECTIONS[i]] = animation;
            }

            return sprite;
        }

        public IDictionary<Direction, ISprite> getGhostSprite(GhostColor color)
        {

            string resource = $"Sprites.ghost_{color.ToString().ToLower()}.png";
            return DirectionSprite(resource, GHOST_ANIMATION_FRAMES);
        }

        public ISprite getWallSprite()
        {
            return loadSprite("Sprites.wall.png");
        }

        public ISprite getGroundSprite()
        {
            return loadSprite("Sprites.floor.png");
        }

        public ISprite getPelletSprite()
        {
            return loadSprite("Sprites.pellet.png");
        }

        override public ISprite loadSprite(string resource)
        {
            try
            {
                return base.loadSprite(resource);
            }
            catch (IOException e)
            {
                throw new eu.sig.cspacman.PacmanConfigurationException($"Unable to load sprite: {resource}", e);
            }
        }
    }
}
