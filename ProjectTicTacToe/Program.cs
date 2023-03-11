

using System.Net.Http.Headers;
using System.Security.Cryptography;

namespace ProjectTicTacToe
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var game = new TicTacToe(new IPlayer[] { 
                new ConsolePlayer(),
                new ConsolePlayer(),
            });

            game.EnterGame();

            return 0;
        }
    }
}