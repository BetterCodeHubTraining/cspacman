using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;

namespace eu.sig.cspacman.sprite
{
    public class SpriteStore
    {

        private IDictionary<string, ISprite> spriteMap;

        public SpriteStore()
        {
            spriteMap = new Dictionary<string, ISprite>();
        }

        public virtual ISprite loadSprite(string resource)
        {
            if (spriteMap.ContainsKey(resource))
            {
                return spriteMap[resource];
            }
            else
            {
                return loadSpriteFromResource(resource);
            }
        }

        private ISprite loadSpriteFromResource(string resource)
        {
            System.Reflection.Assembly myAssembly =
                System.Reflection.Assembly.GetExecutingAssembly();
            using (Stream input = myAssembly.GetManifestResourceStream($"CsPacman.{resource}"))
            {
                if (input == null)
                {
                    throw new IOException($"Unable to load {resource}, resource does not exist.");
                }
                Bitmap image = new Bitmap(input);
                return new ImageSprite(image);
            }
        }

        public AnimatedSprite createAnimatedSprite(ISprite baseImage, int frames,
                int delay, bool loop)
        {
            Debug.Assert(baseImage != null);
            Debug.Assert(frames > 0);

            int frameWidth = baseImage.getWidth() / frames;

            ISprite[] animation = new ISprite[frames];
            for (int i = 0; i < frames; i++)
            {
                animation[i] = baseImage.split(i * frameWidth, 0, frameWidth,
                        baseImage.getHeight());
            }

            return new AnimatedSprite(animation, delay, loop);
        }
    }
}
