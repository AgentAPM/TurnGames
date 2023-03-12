namespace ProjectTicTacToe
{
    public class SimulationManager
    {
        public static void Play(TicTacToe game, int epochs)
        {
            var keeper = new StatKeeper(game.Players);

            game.OnRoundEnd += (object sender, EventArgs e) =>
            {
                if (!(sender is TicTacToe)) throw new Exception("Sender error");
                var game = sender as TicTacToe;

                keeper.RecordRoundResult(game);
            };

            int epoch = 0;
            game.OnRoundEnd += (object sender, EventArgs e) =>
            {
                if (!(sender is TicTacToe)) throw new Exception("Sender error");
                var game = sender as TicTacToe;

                epoch++;
                if (epoch >= epochs)
                {
                    game.KeepPlaying = false;
                }
            };

            game.OnGameEnd += (object sender, EventArgs e) =>
            {
                keeper.PrintGameResults();
            };

            game.StartGame();
        }
    }
}
