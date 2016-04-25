using eu.sig.cspacman.board;
using eu.sig.cspacman.npc.ghost;

namespace eu.sig.cspacman.level
{
    public class PlayerCollisions : ICollisionMap
    {

        public void Collide(Unit mover, Unit collidedOn)
        {
            if (mover is Player)
            {
                PlayerColliding((Player)mover, collidedOn);
            }
            else if (mover is Ghost)
            {
                GhostColliding((Ghost)mover, collidedOn);
            }
        }

        private void PlayerColliding(Player player, Unit collidedOn)
        {
            if (collidedOn is Ghost)
            {
                PlayerVersusGhost(player, (Ghost)collidedOn);
            }

            if (collidedOn is Pellet)
            {
                PlayerVersusPellet(player, (Pellet)collidedOn);
            }
        }

        private void GhostColliding(Ghost ghost, Unit collidedOn)
        {
            if (collidedOn is Player)
            {
                PlayerVersusGhost((Player)collidedOn, ghost);
            }
        }

        public void PlayerVersusGhost(Player player, Ghost ghost)
        {
            player.IsAlive = false;
        }

        public void PlayerVersusPellet(Player player, Pellet pellet)
        {
            pellet.LeaveSquare();
            player.AddPoints(pellet.Value);
        }

    }
}
