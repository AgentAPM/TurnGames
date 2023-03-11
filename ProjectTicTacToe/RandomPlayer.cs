namespace ProjectTicTacToe
{
    public class RandomPlayer : IPlayer
    {
        public char Icon { get; set; }
        public RandomPlayer()
        { }
        private Random RNG = new Random();
        public Move GetMove(BoardState position)
        {
            Console.WriteLine("Komputer wykonuje ruch");
            var moves = position.PossibleMoves;
            return moves[RNG.Next(moves.Length)];
        }
    }
}