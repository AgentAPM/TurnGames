namespace ProjectTicTacToe
{
    public class GameManager
    {
        public static void Play(TicTacToe game)
        {
            var keeper = new StatKeeper(game.Players);

            game.OnRoundEnd += (sender, e) =>
            {
                if (!(sender is TicTacToe)) throw new Exception("Sender error");
                var game = sender as TicTacToe;

                Console.Write(game.CurrentState.Draw());
                if (game.CurrentState.Winner != '-')
                    Console.WriteLine($"Wygrywa {game.CurrentState.Winner}!!!");
                else
                    Console.WriteLine("Remis.");
            };

            game.OnRoundEnd += (sender, e) =>
            {
                if (!(sender is TicTacToe)) throw new Exception("Sender error");
                var game = sender as TicTacToe;

                keeper.RecordRoundResult(game);

            };

            game.OnTurnStart += (sender, e) =>
            {
                if (!(sender is TicTacToe)) throw new Exception("Sender error");
                var game = sender as TicTacToe;

                Console.WriteLine($"{game.CurrentState.PlayerOnMove} na ruchu:");
                Console.Write(game.CurrentState.Draw());
            };
            game.OnTurnEnd += (sender, e) =>
            {
                Console.WriteLine();
            };
            game.OnRoundEnd += PromptRestartRound;
            game.OnGameEnd += (sender, e) =>
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
