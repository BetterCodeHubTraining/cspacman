using eu.sig.cspacman.board;

namespace eu.sig.cspacman.level
{
     public interface ICollisionMap
    {
        void Collide(Unit collider, Unit collidee);
    }
}
