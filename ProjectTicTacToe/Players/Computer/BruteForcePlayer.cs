using static ProjectTicTacToe.ExplorerPlayer;

namespace ProjectTicTacToe
{
    public class BruteForcePlayer : IPlayer
    {
        public char Icon { get; set; }
        private Random RNG = new Random();
        private Dictionary<string, WLDcount>? StateStats;
        private Dictionary<string, char>? StateEvaluations;

        public event GameEvent? OnMakeMove;

        public Move GetMove(BoardState position)
        {
            if (StateEvaluations == null)
                StateEvaluations = new Dictionary<string, char>();

            if (!StateEvaluations.ContainsKey(position.PositionCode))
                SearchBest(position, Icon);

            var moves = position.PossibleMoves;
            var moveCandidates = new List<Move>();
            var bestEvaluation = int.MinValue;

            foreach (var move in moves)
            {
                var nextPosition = position.AfterMove(move);
                char nextWinner = StateEvaluations[nextPosition.PositionCode];
                int nextEvaluation = 0;
                if (nextWinner == Icon)
                    nextEvaluation = 1;
                else if (nextWinner == '-')
                    nextEvaluation = 0;
                else
                    nextEvaluation = -1;

                if (nextEvaluation >= bestEvaluation)
                {
                    if (nextEvaluation > bestEvaluation)
                    {
                        moveCandidates.RemoveAll((Move m) => true);
                        bestEvaluation = nextEvaluation;
                    }

                    moveCandidates.Add(move);
                }
            }

            int pick = RNG.Next(moveCandidates.Count);

            OnMakeMove?.Invoke(this,new BruteForceMoveArgs(StateEvaluations[position.PositionCode]));
            /*
            var sb = new StringBuilder();
            for (int y = 0; y < 3; y++)
            {
                sb.Append("|");
                for (int x = 0; x < 3; x++)
                {
                    sb.Append(debug_pos[y * 3 + x]);
                }
                sb.Append("|\n");
            }
            Console.WriteLine(sb.ToString());
            */
            return moveCandidates[pick];

        }
        private int SearchBest(BoardState position, char perspective)
        {
            int positionEvaluation = 0;
            if (StateEvaluations.ContainsKey(position.PositionCode))
            {
                var positionWinner = StateEvaluations[position.PositionCode];
                if (positionWinner == perspective)
                    positionEvaluation = 1;
                else if (positionWinner == '-')
                    positionEvaluation = 0;
                else
                    positionEvaluation = -1;
            }
            else
            {
                if (position.Winner != ' ')
                {
                    StateEvaluations[position.PositionCode] = position.Winner;
                    if (position.Winner == perspective)
                        positionEvaluation = 1;
                    else if (position.Winner == '-')
                        positionEvaluation = 0;
                    else
                        positionEvaluation = -1;
                }
                else
                {
                    int evaluation = int.MinValue;
                    char positionWinner = ' ';
                    var moves = position.PossibleMoves;
                    foreach (var move in moves)
                    {
                        var nextPosition = position.AfterMove(move);

                        int moveEvaluation = SearchBest(nextPosition, position.PlayerOnMove);

                        if (moveEvaluation > evaluation)
                        {
                            evaluation = moveEvaluation;
                            positionWinner = StateEvaluations[nextPosition.PositionCode];
                        }
                    }
                    StateEvaluations[position.PositionCode] = positionWinner;
                }
            }
            return positionEvaluation;
        }
        private void SearchTree(BoardState position)
        {
            var evaluation = new WLDcount();
            SearchTree(position, 9, ref evaluation);
            StateStats[position.PositionCode] = evaluation;
        }
        private void SearchTree(BoardState position, int maxDepth, ref WLDcount evaluation)
        {
            if (position.Winner == ' ')
            {
                if (maxDepth > 0)
                {
                    var moves = position.PossibleMoves;
                    foreach (var move in moves)
                    {
                        var nextPosition = position.AfterMove(move);
                        SearchTree(nextPosition, maxDepth - 1, ref evaluation);
                    }
                }
            }
            else if (position.Winner == '-')
                evaluation.draws++;
            else if (position.Winner == Icon)
                evaluation.wins++;
            else
                evaluation.losses++;
        }
        internal class BruteForceMoveArgs : EventArgs
        {
            public char Evauation { get; set; }
            public BruteForceMoveArgs(char evaluation)
            {
                Evauation = evaluation;
            }
        }
        public static void SayOnMakeMove(object sender, EventArgs e)
        {
            if (!(sender is BruteForcePlayer)) throw new Exception("Sender error");
            if (!(e is BruteForceMoveArgs)) throw new Exception("Args error");

            var player = sender as BruteForcePlayer;
            var args = e as BruteForceMoveArgs;

            if(args.Evauation==player.Icon)
                Console.WriteLine("Pozycja wygląda na wygraną");
            else if(args.Evauation=='-')
                Console.WriteLine("Pozycja wygląda na remis");
            else
                Console.WriteLine("Pozycja wygląda na przegraną");
        }
    }
}