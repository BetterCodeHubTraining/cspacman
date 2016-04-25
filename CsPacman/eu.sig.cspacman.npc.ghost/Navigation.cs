using System;
using System.Collections.Generic;

using eu.sig.cspacman.board;

namespace eu.sig.cspacman.npc.ghost
{
    public sealed class Navigation
    {

        private Navigation()
        {
        }

        public static IList<Direction> shortestPath(Square from, Square to,
                Unit traveller)
        {
            if (from == to)
            {
                return new List<Direction>();
            }

            IList<Node> targets = new List<Node>();
            ISet<Square> visited = new HashSet<Square>();
            targets.Add(new Node(null, from, null));
            while (targets.Count != 0)
            {
                Node n = targets.RemoveAt<Node>(0);
                Square s = n.getSquare();
                if (s == to)
                {
                    List<Direction> path = n.getPath();
                    return path;
                }
                visited.Add(s);
                addNewTargets(traveller, targets, visited, n, s);
            }
            return null;
        }

        private static void addNewTargets(Unit traveller, IList<Node> targets,
                ISet<Square> visited, Node n, Square s)
        {
            foreach (Direction d in Direction.Values)
            {
                Square target = s.GetSquareAt(d);
                if (!visited.Contains(target)
                        && (traveller == null || target
                                .IsAccessibleTo(traveller)))
                {
                    targets.Add(new Node(d, target, n));
                }
            }
        }

        public static Unit findNearest(Type type,
                Square currentLocation)
        {
            IList<Square> toDo = new List<Square>();
            ISet<Square> visited = new HashSet<Square>();

            toDo.Add(currentLocation);

            while (toDo.Count != 0)
            {
                Square square = toDo.RemoveAt<Square>(0);
                Unit unit = findUnit(type, square);
                if (unit != null)
                {
                    return unit;
                }
                visited.Add(square);
                foreach (Direction d in Direction.Values)
                {
                    Square newTarget = square.GetSquareAt(d);
                    if (!visited.Contains(newTarget) && !toDo.Contains(newTarget))
                    {
                        toDo.Add(newTarget);
                    }
                }
            }
            return null;
        }

        public static Unit findUnit(Type type, Square square)
        {
            foreach (Unit u in square.Occupants)
            {
                if (type.IsInstanceOfType(u))
                {
                    return u;
                }
            }
            return null;
        }

        private sealed class Node
        {
            private readonly Direction direction;

            private readonly Node parent;

            private readonly Square square;

            internal Node(Direction d, Square s, Node p)
            {
                this.direction = d;
                this.square = s;
                this.parent = p;
            }

            private Direction getDirection()
            {
                return direction;
            }

            internal Square getSquare()
            {
                return square;
            }

            private Node getParent()
            {
                return parent;
            }

            internal List<Direction> getPath()
            {
                if (getParent() == null)
                {
                    return new List<Direction>();
                }
                List<Direction> path = parent.getPath();
                path.Add(getDirection());
                return path;
            }
        }
    }

    public static class MyExtensionMethods
    {
        public static T RemoveAt<T>(this IList<T> list, int index)
        {
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }
    }
}
