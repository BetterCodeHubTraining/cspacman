using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

using eu.sig.cspacman.board;
using eu.sig.cspacman.npc;

namespace eu.sig.cspacman.level
{
    public class Level
    {

        public IBoard Board { get; }

        private readonly Object moveLock = new Object();

        private readonly Object startStopLock = new Object();

        private IDictionary<NPC, CancellationTokenSource> npcs;

        private bool inProgress;

        private readonly IList<Square> startSquares;

        private int startSquareIndex;

        private readonly IList<Player> players;

        private readonly ICollisionMap collisions;

        private readonly IList<ILevelObserver> observers;

        public Level(IBoard b, IList<NPC> ghosts, IList<Square> startPositions,
            ICollisionMap collisionMap)
        {
            Debug.Assert(b != null);
            Debug.Assert(ghosts != null);
            Debug.Assert(startPositions != null);

            this.Board = b;
            this.inProgress = false;
            this.npcs = new Dictionary<NPC, CancellationTokenSource>();
            foreach (NPC g in ghosts)
            {
                npcs[g] = null;
            }
            this.startSquares = startPositions;
            this.startSquareIndex = 0;
            this.players = new List<Player>();
            this.collisions = collisionMap;
            this.observers = new List<ILevelObserver>();
        }

        public void AddObserver(ILevelObserver observer)
        {
            if (observers.Contains(observer))
            {
                return;
            }
            observers.Add(observer);
        }

        public void RemoveObserver(ILevelObserver observer)
        {
            observers.Remove(observer);
        }

        public void RegisterPlayer(Player p)
        {
            Debug.Assert(p != null);
            Debug.Assert(startSquares.Count != 0);

            if (players.Contains(p))
            {
                return;
            }
            players.Add(p);
            Square square = startSquares[startSquareIndex];
            p.Occupy(square);
            startSquareIndex++;
            startSquareIndex %= startSquares.Count;
        }

        public void Move(Unit unit, Direction direction)
        {
            Debug.Assert(unit != null);
            Debug.Assert(direction != null);

            if (!IsInProgress())
            {
                return;
            }

            lock (moveLock)
            {
                unit.Direction = direction;
                Square location = unit.Square;
                Square destination = location.GetSquareAt(direction);

                if (destination.IsAccessibleTo(unit))
                {
                    IList<Unit> occupants = destination.Occupants;
                    unit.Occupy(destination);
                    foreach (Unit occupant in occupants)
                    {
                        collisions.Collide(unit, occupant);
                    }
                }
                UpdateObservers();
            }
        }

        public void Start()
        {
            lock (startStopLock)
            {
                if (IsInProgress())
                {
                    return;
                }
                StartNPCs();
                inProgress = true;
                UpdateObservers();
            }
        }

        public void Stop()
        {
            lock (startStopLock)
            {
                if (!IsInProgress())
                {
                    return;
                }
                StopNPCs();
                inProgress = false;
            }
        }

        private void StartNPCs()
        {
            IDictionary<NPC, CancellationTokenSource> npcsTmp = new Dictionary<NPC, CancellationTokenSource>();
            foreach (NPC npc in npcs.Keys)
            {
                CancellationTokenSource ts = new CancellationTokenSource();
                NpcMoveTask npcMoveTask = new NpcMoveTask(this, npc, ts.Token);
                Task.Delay(npc.getInterval() / 2, ts.Token)
                    .ContinueWith((t) => npcMoveTask.Run());
                npcsTmp[npc] = ts;
            }
            npcs = npcsTmp;
        }

        private void StopNPCs()
        {
            foreach (KeyValuePair<NPC, CancellationTokenSource> e in npcs)
            {
                e.Value.Cancel();
            }
        }

        public bool IsInProgress()
        {
            return inProgress;
        }

        private void UpdateObservers()
        {
            if (!IsAnyPlayerAlive())
            {
                foreach (ILevelObserver o in observers)
                {
                    o.LevelLost();
                }
            }
            if (RemainingPellets() == 0)
            {
                foreach (ILevelObserver o in observers)
                {
                    o.LevelWon();
                }
            }
        }

        public bool IsAnyPlayerAlive()
        {
            foreach (Player p in players)
            {
                if (p.IsAlive)
                {
                    return true;
                }
            }
            return false;
        }

        public int RemainingPellets()
        {
            IBoard b = Board;
            int pellets = 0;
            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    foreach (Unit u in b.SquareAt(x, y).Occupants)
                    {
                        if (u is Pellet)
                        {
                            pellets++;
                        }
                    }
                }
            }
            return pellets;
        }

        private sealed class NpcMoveTask
        {

            private Level me;

            private CancellationToken ct;

            private NPC npc;

            internal NpcMoveTask(Level me, NPC n, CancellationToken ct)
            {
                this.me = me;
                this.npc = n;
                this.ct = ct;
            }

            internal void Run()
            {
                Direction nextMove = npc.NextMove();
                if (nextMove != null)
                {
                    me.Move(npc, nextMove);
                }
                int interval = npc.getInterval();
                Task.Delay(interval, ct).ContinueWith((t) => this.Run());
            }
        }

        public interface ILevelObserver
        {

            void LevelWon();

            void LevelLost();
        }
    }
}
