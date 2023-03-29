namespace ProjectTicTacToe
{
    public class SimulationManager
    {
        public static void Play(TicTacToe game, int epochs)
        {
            var keeper = new StatKeeper(game.Players);

            game.OnRoundEnd += (sender, e) =>
            {
                if (!(sender is TicTacToe)) throw new Exception("Sender error");
                var game = sender as TicTacToe;

                keeper.RecordRoundResult(game);
            };

            int epoch = 0;
            game.OnRoundEnd += (sender, e) =>
            {
                if (!(sender is TicTacToe)) throw new Exception("Sender error");
                var game = sender as TicTacToe;

                epoch++;
                if (epoch < epochs)
                    game.KeepPlaying = true;
                else
                    game.KeepPlaying = false;
            };

            game.OnGameEnd += (sender, e) =>
            {
                keeper.PrintGameResults();
            };

            game.StartGame();
        }
    }
}
