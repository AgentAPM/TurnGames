namespace ProjectTicTacToe
{
    public class RandomPlayer : IPlayer
    {
        public char Icon { get; set; }

        public event GameEvent OnMakeMove;
        public RandomPlayer()
        {
            Icon = '?';
        }
        private Random RNG = new Random();
        public Move GetMove(BoardState position)
        {
            OnMakeMove?.Invoke(this);

            var moves = position.PossibleMoves;
            return moves[RNG.Next(moves.Length)];
        }
        public static void SayOnMakeMove(object sender, EventArgs e)
        {
            Console.WriteLine("Komputer wykonuje ruch");
        }
    }
}