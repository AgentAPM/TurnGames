namespace ProjectTicTacToe
{
    public class GameManager
    {
        public static void Play(TicTacToe game)
        {
            var keeper = new StatKeeper(game.Players);

            game.OnRoundEnd += (object sender, EventArgs e) =>
            {
                if (!(sender is TicTacToe)) throw new Exception("Sender error");
                var game = sender as TicTacToe;

                Console.Write(game.CurrentState.Draw());
                if (game.CurrentState.Winner != '-')
                    Console.WriteLine($"Wygrywa {game.CurrentState.Winner}!!!");
                else
                    Console.WriteLine("Remis.");
            };

            game.OnRoundEnd += (object sender, EventArgs e) =>
            {
                if (!(sender is TicTacToe)) throw new Exception("Sender error");
                var game = sender as TicTacToe;

                keeper.RecordRoundResult(game);

            };

            game.OnTurnStart += (object sender, EventArgs e) =>
            {
                if (!(sender is TicTacToe)) throw new Exception("Sender error");
                var game = sender as TicTacToe;

                Console.Write(game.CurrentState.Draw());
                Console.WriteLine($"{game.CurrentState.PlayerOnMove} na ruchu");
            };
            game.OnRoundEnd += PromptRestartRound;
            game.OnGameEnd += (object sender, EventArgs e) =>
            {
                keeper.PrintGameResults();
            };
            game.OnGameEnd += PromptGameEnd;

            game.StartGame();
        }
        public static void PromptRestartRound(object sender, EventArgs args)
        {
            if (!(sender is TicTacToe)) throw new Exception("Sender error");
            var game = sender as TicTacToe;

            Console.WriteLine("Czy chcesz zagrać jeszcze raz?");

            string input;
            bool wrongInput;
            do
            {
                wrongInput = false;
                input = Console.ReadLine();
                switch (input)
                {
                    case "t":
                    case "T":
                    case "tak":
                    case "":
                        game.KeepPlaying = true;
                        break;
                    case "n":
                    case "N":
                    case "nie":
                        game.KeepPlaying = false;
                        break;
                    default:
                        Console.WriteLine("Błędna komenda");
                        wrongInput = true;
                        break;
                }

            } while (wrongInput);
        }
        public static void PromptGameEnd(object sender, EventArgs args)
        {
            Console.WriteLine("Dziękuję za grę!");
        }
    }
}
