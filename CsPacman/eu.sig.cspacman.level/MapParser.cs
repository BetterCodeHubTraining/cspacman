using System.IO;
using System.Collections.Generic;

using eu.sig.cspacman.board;
using eu.sig.cspacman.npc;

namespace eu.sig.cspacman.level
{
    public class MapParser
    {

        private readonly LevelFactory levelCreator;

        private readonly BoardFactory boardCreator;

        public MapParser(LevelFactory levelFactory, BoardFactory boardFactory)
        {
            this.levelCreator = levelFactory;
            this.boardCreator = boardFactory;
        }

        public Level ParseMap(char[,] map)
        {
            int width = map.GetLength(0);
            int height = map.GetLength(1);

            Square[,] grid = new Square[width, height];

            IList<NPC> ghosts = new List<NPC>();
            IList<Square> startPositions = new List<Square>();

            MakeGrid(map, width, height, grid, ghosts, startPositions);

            Board board = boardCreator.CreateBoard(grid);
            return levelCreator.CreateLevel(board, ghosts, startPositions);
        }

        private void MakeGrid(char[,] map, int width, int height,
                Square[,] grid, IList<NPC> ghosts, IList<Square> startPositions)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    char c = map[x, y];
                    AddSquare(grid, ghosts, startPositions, x, y, c);
                }
            }
        }

        private void AddSquare(Square[,] grid, IList<NPC> ghosts,
                IList<Square> startPositions, int x, int y, char c)
        {
            switch (c)
            {
                case ' ':
                    grid[x, y] = boardCreator.createGround();
                    break;
                case '#':
                    grid[x, y] = boardCreator.createWall();
                    break;
                case '.':
                    Square pelletSquare = boardCreator.createGround();
                    grid[x, y] = pelletSquare;
                    levelCreator.CreatePellet().Occupy(pelletSquare);
                    break;
                case 'G':
                    Square ghostSquare = MakeGhostSquare(ghosts);
                    grid[x, y] = ghostSquare;
                    break;
                case 'P':
                    Square playerSquare = boardCreator.createGround();
                    grid[x, y] = playerSquare;
                    startPositions.Add(playerSquare);
                    break;
                default:
                    throw new PacmanConfigurationException("Invalid character at "
                            + x + "," + y + ": " + c);
            }
        }

        private Square MakeGhostSquare(IList<NPC> ghosts)
        {
            Square ghostSquare = boardCreator.createGround();
            NPC ghost = levelCreator.CreateGhost();
            ghosts.Add(ghost);
            ghost.Occupy(ghostSquare);
            return ghostSquare;
        }

        public Level ParseMap(IList<string> text)
        {

            CheckMapFormat(text);

            int height = text.Count;
            int width = text[0].Length;

            char[,] map = new char[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    map[x, y] = text[y][x];
                }
            }
            return ParseMap(map);
        }

        private void CheckMapFormat(IList<string> text)
        {
            if (text == null)
            {
                throw new PacmanConfigurationException(
                        "Input text cannot be null.");
            }

            if (text.Count == 0)
            {
                throw new PacmanConfigurationException(
                        "Input text must consist of at least 1 row.");
            }

            int width = text[0].Length;

            if (width == 0)
            {
                throw new PacmanConfigurationException(
                    "Input text lines cannot be empty.");
            }

            foreach (string line in text)
            {
                if (line.Length != width)
                {
                    throw new PacmanConfigurationException(
                        "Input text lines are not of equal width.");
                }
            }
        }

        public Level ParseMap(Stream source)
        {
            using (StreamReader reader = new StreamReader(new BufferedStream(
                    source), System.Text.Encoding.UTF8))
            {
                IList<string> lines = new List<string>();
                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());
                }
                return ParseMap(lines);
            }
        }
    }
}
