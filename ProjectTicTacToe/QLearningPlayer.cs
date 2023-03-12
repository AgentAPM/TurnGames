namespace ProjectTicTacToe
{
    public class QLearningPlayer : IPlayer
    {
        private Random RNG = new Random();
        public char Icon { get; set; }

        public event GameEvent OnMakeMove;
        private string ToAction(BoardState state, Move move)
        {
            return $"{state}>{move.Tile}";
        }
        private Dictionary<string, double> Qtable = new Dictionary<string, double>();
        public double Alpha { get; set; } = 0.5;
        public double Epsillon { get; set; } = 0.9;
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
            var moves = position.PossibleMoves;

            foreach(var move in moves)
            {
                var actionKey = ToAction(position,move);
                double actionValue=0;

                if (Qtable.ContainsKey(actionKey))
                    actionValue = Qtable[actionKey];
                else
                    Qtable[actionKey] = actionValue;


            }

            throw new NotImplementedException();

            OnMakeMove?.Invoke(this);
        }
    }
}