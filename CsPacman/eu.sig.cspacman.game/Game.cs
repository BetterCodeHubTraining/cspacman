using System;
using System.Collections.Generic;

using eu.sig.cspacman.board;
using eu.sig.cspacman.level;

namespace eu.sig.cspacman.game
{
    public abstract class Game : Level.ILevelObserver
    {

        private bool inProgress;

        private readonly Object progressLock = new Object();

        public abstract IList<Player> Players { get; }

        public abstract Level Level { get; }

        protected Game()
        {
            inProgress = false;
        }

        public void Start()
        {
            lock (progressLock)
            {
                if (IsInProgress())
                {
                    return;
                }
                if (Level.IsAnyPlayerAlive()
                    && Level.RemainingPellets() > 0)
                {
                    inProgress = true;
                    Level.AddObserver(this);
                    Level.Start();
                }
            }
        }

        public void Stop()
        {
            lock (progressLock)
            {
                if (!IsInProgress())
                {
                    return;
                }
                inProgress = false;
                Level.Stop();
            }
        }

        public bool IsInProgress()
        {
            return inProgress;
        }

        public void Move(Player player, Direction direction)
        {
            if (IsInProgress())
            {
                // execute player move.
                Level.Move(player, direction);
            }
        }

        public void LevelWon()
        {
            Stop();
        }

        public void LevelLost()
        {
            Stop();
        }
    }
}
