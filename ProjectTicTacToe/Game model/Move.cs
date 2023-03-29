namespace ProjectTicTacToe
{
    public class Move
    {
        public int Tile { get; private set; }
        public char Player { get; private set; }
        public Move(int tile, char player)
        {
            Tile = tile;
            Player = player;
        }
        public override bool Equals(object? obj)
        {
            if (!(obj is Move)) return false;

            var other = obj as Move;

            return this.Tile == other.Tile && this.Player == other.Player;
        }
        public static string ToAction(BoardState fromPosition, Move move)
        {
            return $"{fromPosition}>{move.Tile}";
        }
        public string ToAction(BoardState fromPosition)
        {
            return ToAction(fromPosition, this);
        }
    }
}