using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;

using eu.sig.cspacman.board;
using eu.sig.cspacman.game;
using eu.sig.cspacman.level;
using eu.sig.cspacman.npc.ghost;
using eu.sig.cspacman.sprite;
using eu.sig.cspacman.ui;

[assembly: AssemblyVersionAttribute("1.0.0.0")]
namespace eu.sig.cspacman
{
    public class Launcher
    {

        private static readonly PacManSprites SPRITE_STORE = new PacManSprites();

        private PacManUI pacManUI;

        public Game Game { get; private set; }

        public Game MakeGame()
        {
            GameFactory gf = GetGameFactory();
            Level level = MakeLevel();
            return gf.CreateSinglePlayerGame(level);
        }

        public Level MakeLevel()
        {
            Stream boardStream = null;
            MapParser parser = GetMapParser();
            try
            {
                System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                boardStream =  myAssembly.GetManifestResourceStream("CsPacman.board.txt");
                return parser.ParseMap(boardStream);
            }
            catch (IOException e)
            {
                throw new PacmanConfigurationException("Unable to create level.", e);
            }
            finally
			{
				if (boardStream != null) {
                    boardStream.Dispose();
                }
			}
        }

        protected MapParser GetMapParser()
        {
            return new MapParser(GetLevelFactory(), GetBoardFactory());
        }

        protected BoardFactory GetBoardFactory()
        {
            return new BoardFactory(GetSpriteStore());
        }

        protected PacManSprites GetSpriteStore()
        {
            return SPRITE_STORE;
        }

        protected LevelFactory GetLevelFactory()
        {
            return new LevelFactory(GetSpriteStore(), GetGhostFactory());
        }

        protected GhostFactory GetGhostFactory()
        {
            return new GhostFactory(GetSpriteStore());
        }

        protected GameFactory GetGameFactory()
        {
            return new GameFactory(GetPlayerFactory());
        }

        protected PlayerFactory GetPlayerFactory()
        {
            return new PlayerFactory(GetSpriteStore());
        }

        protected void AddSinglePlayerKeys(PacManUiBuilder builder,
            Game game)
        {
            Player p1 = GetSinglePlayer(game);

            builder.addKey(Keys.Up, () => game.Move(p1, Direction.NORTH))
                .addKey(Keys.Down, () => game.Move(p1, Direction.SOUTH))
                .addKey(Keys.Left, () => game.Move(p1, Direction.WEST))
                .addKey(Keys.Right, () => game.Move(p1, Direction.EAST));
        }

        private Player GetSinglePlayer(Game game)
        {
            IList<Player> players = game.Players;
            if (players.Count == 0)
            {
                throw new ArgumentException("Game has 0 players.");
            }
            Player p1 = players[0];
            return p1;
        }

        public void Launch()
        {
            Game = MakeGame();
            PacManUiBuilder builder = new PacManUiBuilder().withDefaultButtons();
            AddSinglePlayerKeys(builder, Game);
            pacManUI = builder.build(Game);
            pacManUI.start();
        }

        public PacManUI GetPacManUI()
        {
            return pacManUI;
        }

        public void Dispose()
        {
            pacManUI.Dispose();
        }

        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Launcher launcher = new Launcher();
            launcher.Launch();
            Application.Run(launcher.GetPacManUI());
            Application.Exit();
        }

    }
}
