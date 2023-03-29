namespace ProjectTicTacToe
{
    public interface IPlayer
    {
        event GameEvent OnMakeMove;
        char Icon { get; set; }
        Move GetMove(BoardState position);
    }
}