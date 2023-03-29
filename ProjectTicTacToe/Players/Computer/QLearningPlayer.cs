namespace ProjectTicTacToe
{
    public class QLearningPlayer : IPlayer
    {
        private Random RNG = new Random();
        public char Icon { get; set; }

        public event GameEvent OnMakeMove;
        private Dictionary<string, double> Qtable = new Dictionary<string, double>();
        public double Alpha { get; set; } = 0.5;
        public double Epsillon { get; set; } = 0.9;
        public double LearningRate { get; set; } = 1.0;
        private static Dictionary<char, double> Rewards;
        static QLearningPlayer()
        {
            Rewards = new Dictionary<char, double>
            {
                { ' ', -1.0 },
                { 'W', 50.0 },
                { 'L',-50.0 },
                { '-', -5.0 },
            };
        }
        public Move GetMove(BoardState position)
        {

            var moves = position.PossibleMoves;
            Move nextMove = moves[0];
            if (RNG.NextDouble() < Epsillon)
            {
                double bestQ = double.MinValue;
                foreach (var move in moves)
                {
                    var action = move.ToAction(position);
                    if (!Qtable.ContainsKey(action))
                        Qtable[action] = 0;
                    var moveQ = Qtable[action];

                    if (moveQ > bestQ)
                    {
                        nextMove = move;
                        bestQ = moveQ;
                    }
                }
            }
            else
            {
                nextMove = moves[RNG.Next(moves.Length)];
            }

            var nextPosition = position.AfterMove(nextMove);

            double reward;
            if (nextPosition.Winner == ' ')
                reward = Rewards[' '];
            else if (nextPosition.Winner == Icon)
                reward = Rewards['W'];
            else if (nextPosition.Winner == '-')
                reward = Rewards['D'];
            else
                reward = Rewards['L'];

            var nextAction = nextMove.ToAction(position);
            if (!Qtable.ContainsKey(nextAction))
                Qtable[nextAction] = 0;
            var prevQ = Qtable[nextAction];

            double bestExpectedQ = double.MinValue;
            foreach (var move in nextPosition.PossibleMoves)
            {
                var action = move.ToAction(position);
                if (!Qtable.ContainsKey(action))
                    Qtable[action] = 0;
                var moveQ = Qtable[action];
                if (moveQ > bestExpectedQ)
                {
                    bestExpectedQ = moveQ;
                }
            }
            var temporalDifference = reward + (Alpha * bestExpectedQ - prevQ);
            Qtable[nextAction] = prevQ + LearningRate * temporalDifference;

            OnMakeMove?.Invoke(this);

            return nextMove;
        }
        public static void SayOnMakeMove(object sender, EventArgs e)
        {
            Console.WriteLine("Komputer wykonuje ruch");
        }
    }
}