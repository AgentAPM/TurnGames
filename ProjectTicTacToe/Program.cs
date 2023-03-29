namespace ProjectTicTacToe
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            //var playerX = new ConsolePlayer();
            //var playerX = new ExplorerPlayer();
            var playerX = new BruteForcePlayer();
            //playerX.OnMakeMove += ExplorerPlayer.SayOnMakeMove;
            //var playerX = new RandomPlayer();
            //playerX.OnMakeMove += RandomPlayer.SayOnMakeMove;
            playerX.OnMakeMove += BruteForcePlayer.SayOnMakeMove;

            //var playerO = new ConsolePlayer();
            //var playerO = new RandomPlayer();
            //var playerO = new QLearningPlayer();
            //playerO.OnMakeMove += QLearningPlayer.SayOnMakeMove;
            var playerO = new ExplorerPlayer();
            playerO.OnMakeMove += ExplorerPlayer.SayOnMakeMove;
            //var playerO = new BruteForcePlayer();
            //playerO.OnMakeMove += BruteForcePlayer.SayOnMakeMove;

            /*
            playerO.LearningRate = 0;
            playerO.Epsillon = 1;
            playerO.OnMakeMove += QLearningPlayer.SayOnMakeMove;
            */
            
            CasualPlay(new IPlayer[] {
                    playerX,
                    playerO,
                });
            
            /*
            SimulateGame(new IPlayer[] {
                    playerX,
                    playerO,
                },10000);
            */
            return 0;
        }

        public static void CasualPlay(IPlayer[] players)
        {

            var game = new TicTacToe(players);

            GameManager.Play(game);
        }
        public static void SimulateGame(IPlayer[] players,int epochs)
        {
            var game = new TicTacToe(players);

            SimulationManager.Play(game,epochs);
        }
    }
}