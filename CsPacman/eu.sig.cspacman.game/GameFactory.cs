using eu.sig.cspacman.level;

namespace eu.sig.cspacman.game
{
   public class GameFactory
    {

        private readonly PlayerFactory playerFact;

        public GameFactory(PlayerFactory playerFactory)
        {
            this.playerFact = playerFactory;
        }

        public Game CreateSinglePlayerGame(Level level)
        {
            return new SinglePlayerGame(playerFact.CreatePacMan(), level);
        }

        protected PlayerFactory GetPlayerFactory()
        {
            return playerFact;
        }
    }
}
