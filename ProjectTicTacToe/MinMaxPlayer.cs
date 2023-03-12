namespace ProjectTicTacToe
{
    public class MinMaxPlayer : IPlayer
    {
        public char Icon { get; set; }

        public event GameEvent OnMakeMove;

        public Move GetMove(BoardState position)
        {
            throw new NotImplementedException();
        }
    }
}