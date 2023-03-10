/*
namespace ProjectTicTacToe
{
    interface Game
    {
        BoardState State { get; }
    }
    public struct BoardState
    {
        public char[,] Board { get; private set; }
        public bool IsTerminal { get; private set; }
        public BoardState(char[,] board, bool isTerminal)
        {
            Board = board;
            IsTerminal = isTerminal;
        }
    }
    public class TicTacToe
    {
        private char[,] Board;
        public TicTacToe()
        {
            Board = new char[3, 3] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
        }
        public bool IsMoveValid(int x, int y, char p)
        {
            return Board[x, y] == ' ';
        }
        public void MakeMove(int x, int y, char p)
        {
            Board[x, y] = p;
        }
    }
}*/
