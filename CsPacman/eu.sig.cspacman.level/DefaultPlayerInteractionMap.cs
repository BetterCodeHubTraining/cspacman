using eu.sig.cspacman.board;
using eu.sig.cspacman.npc.ghost;

namespace eu.sig.cspacman.level
{
   public class DefaultPlayerInteractionMap : ICollisionMap
    {

        private ICollisionMap collisions = DefaultCollisions();

        public void Collide(Unit mover, Unit movedInto)
        {
            collisions.Collide(mover, movedInto);
        }

        private static CollisionInteractionMap DefaultCollisions()
        {
            CollisionInteractionMap collisionMap = new CollisionInteractionMap();

            collisionMap.OnCollision(typeof(Player), typeof(Ghost),
                (player, ghost) => { ((Player)player).IsAlive = false; });

            collisionMap.OnCollision(typeof(Player), typeof(Pellet),
                (player, pellet) =>
                {
                    pellet.LeaveSquare();
                    ((Player)player).AddPoints(((Pellet)pellet).Value);
                });
            return collisionMap;
        }
    }
}
