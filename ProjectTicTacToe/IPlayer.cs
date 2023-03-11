namespace ProjectTicTacToe
{
    public interface IPlayer
    {
        char Icon { get; set; }
        Move GetMove(BoardState position);
    }
}