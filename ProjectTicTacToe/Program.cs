

using System.Net.Http.Headers;
using System.Security.Cryptography;

namespace ProjectTicTacToe
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            CasualPlay();
            //SimulateGame();

            return 0;
        }

        public static void CasualPlay()
        {
            var playerX = new ConsolePlayer();
            //playerX.OnMakeMove += ExplorerPlayer.SayOnMakeMove;

            var playerO = new ExplorerPlayer();
            playerO.OnMakeMove += ExplorerPlayer.SayOnMakeMove;

            var game = new TicTacToe(new IPlayer[] {
                    playerX,
                    playerO,
                });

            GameManager.Play(game);
        }
        public static void SimulateGame()
        {
            var playerX = new ExplorerPlayer();

            var playerO = new ExplorerPlayer();

            var game = new TicTacToe(new IPlayer[] {
                    playerX,
                    playerO,
                });

            SimulationManager.Play(game,9*9*9*9*9);
        }
    }
}