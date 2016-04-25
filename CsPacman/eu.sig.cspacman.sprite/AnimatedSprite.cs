using System.Drawing;

namespace eu.sig.cspacman.sprite
{
    public class AnimatedSprite : ISprite
    {
        private static readonly ISprite END_OF_LOOP = new EmptySprite();

        private readonly ISprite[] animationFrames;

        private readonly int animationDelay;

        private readonly bool looping;

        private int current;

        private bool animating;

        private long lastUpdate;

        public AnimatedSprite(ISprite[] frames, int delay, bool loop) : this(frames, delay, loop, false)
        {
        }

        public AnimatedSprite(ISprite[] frames, int delay, bool loop,
                bool isAnimating)
        {
            System.Diagnostics.Debug.Assert(frames.Length > 0);

            this.animationFrames = (ISprite[])frames.Clone();
            this.animationDelay = delay;
            this.looping = loop;
            this.animating = isAnimating;

            this.current = 0;
            this.lastUpdate = System.Environment.TickCount;
        }

        private ISprite currentSprite()
        {
            if (current < animationFrames.Length)
            {
                return animationFrames[current];
            }
            return END_OF_LOOP;
        }

        public void setAnimating(bool isAnimating)
        {
            this.animating = isAnimating;
        }

        public void restart()
        {
            this.current = 0;
            this.lastUpdate = System.Environment.TickCount;
            setAnimating(true);
        }

        public void draw(Graphics g, int x, int y, int width, int height)
        {
            update();
            currentSprite().draw(g, x, y, width, height);
        }

        public ISprite split(int x, int y, int width, int height)
        {
            update();
            return currentSprite().split(x, y, width, height);
        }

        private void update()
        {
            int now = System.Environment.TickCount;
            if (animating)
            {
                while (lastUpdate < now)
                {
                    lastUpdate += animationDelay;
                    current++;
                    if (looping)
                    {
                        current %= animationFrames.Length;
                    }
                    else if (current == animationFrames.Length)
                    {
                        animating = false;
                    }
                }
            }
            else
            {
                lastUpdate = now;
            }
        }

        public int getWidth()
        {
            return currentSprite().getWidth();
        }

        public int getHeight()
        {
            return currentSprite().getHeight();
        }

    }
}