namespace ProjectTicTacToe
{
    public class QLearningPlayer : IPlayer
    {
        private Random RNG = new Random();
        public char Icon { get; set; }
        private string ToAction(BoardState state, Move move)
        {
            return state.ToString() + move.Tile;
        }
        private Dictionary<string, double> Qtable = new Dictionary<string, double>();
        private double epsillon = 0.9;
        private static Dictionary<char, double> rewards;
        static QLearningPlayer()
        {
            rewards = new Dictionary<char, double>
            {
                { ' ',  0.0 },
                { 'X', 50.0 },
                { 'O',-50.0 },
                { '-',-10.0 },
            };
        }
        public Move GetMove(BoardState position)
        {
            throw new NotImplementedException();
        }
    }
}