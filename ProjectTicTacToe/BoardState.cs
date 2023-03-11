using System.Text;

namespace ProjectTicTacToe
{
    public class BoardState
    {
        public int[] Dimens { get; private set; }
        public readonly char[] Board;
        public readonly string playerQueue;
        public char PlayerOnMove
        {
            get { return playerQueue[0]; }
        }

        private static Dictionary<string, BoardState> AllBoardStates = new Dictionary<string, BoardState>();
        public BoardState(int[] dimens, string playerQueue)
        {
            Dimens = dimens;
            int volume = dimens.Aggregate((int value, int accumulator) => accumulator * value);
            Board = new string(' ',volume).ToCharArray();
            playerQueue = playerQueue;
        }
        private BoardState(string positionCode)
        {
            Dimens = new int[] { 3, 3 };
            var sections = positionCode.Split(':');
            if (!(sections.Length == 2 && sections[1].Length == 9)) throw new Exception(positionCode);

            Board = sections[1].ToCharArray();
            playerQueue = sections[0];
        }
        private BoardState(BoardState previousPosition, Move afterMove)
        {
            Dimens = previousPosition.Dimens;
            playerQueue = previousPosition.playerQueue;
            Board = new char[previousPosition.Board.Length];
            Array.Copy(previousPosition.Board, Board, previousPosition.Board.Length);

            Board[afterMove.Tile] = afterMove.Player;
            playerQueue = previousPosition.playerQueue.Substring(1) + previousPosition.playerQueue[0];
        }
        public static BoardState FromCode(string positionCode)
        {
            if (AllBoardStates.ContainsKey(positionCode))
            {
                return AllBoardStates[positionCode];
            }
            else
            {
                var boardState = new BoardState(positionCode);
                AllBoardStates[positionCode] = boardState;
                return boardState;
            }
        }
        public BoardState AfterMove(Move move)
        {
            var nextState = new BoardState(this, move);

            var newPositionCode = nextState.ToString();
            if (AllBoardStates.ContainsKey(newPositionCode))
            {
                nextState = AllBoardStates[newPositionCode];
            }
            else
            {
                AllBoardStates[newPositionCode] = nextState;
            }

            return nextState;
        }

        private Move[] moves = null;
        public Move[] PossibleMoves
        {
            get
            {
                if (moves == null)
                {
                    var possibleMoves = new List<Move>();
                    for (int i = 0; i < Board.Length; i++)
                    {
                        if (Board[i] == ' ')
                        {
                            possibleMoves.Add(new Move(i, PlayerOnMove));
                        }
                    }
                    moves = possibleMoves.ToArray();
                }
                return moves;
            }
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(playerQueue);
            sb.Append(':');
            sb.Append(Board);
            return sb.ToString();
        }
        private char winner = '_';
        public char Winner
        {
            get
            {
                if (winner == '_')
                {
                    winner = ' ';
                    for (int i = 0; i < 3; i++)
                    {
                        if (Board[i * 3] != ' ' && Board[i * 3] == Board[i * 3 + 1] && Board[i * 3] == Board[i * 3 + 2]) winner = Board[i * 3];
                        else if (Board[i] != ' ' && Board[i] == Board[i + 3] && Board[i] == Board[i + 6]) winner = Board[i];
                    }
                    if (winner == ' ')
                        if (Board[0] != ' ' && Board[0] == Board[4] && Board[0] == Board[8]) winner = Board[0];
                        else if (Board[2] != ' ' && Board[2] == Board[4] && Board[2] == Board[6]) winner = Board[2];
                        else
                        {
                            bool emptyTileExists = false;
                            for (int i = 0; i < 9; i++)
                            {
                                if (Board[i] == ' ')
                                {
                                    emptyTileExists = true;
                                    break;
                                }
                            }
                            if (emptyTileExists)
                            {
                                winner = ' ';
                            }
                            else
                            {
                                winner = '-';
                            }
                        }
                }
                return winner;
            }
        }
        public override bool Equals(object? obj)
        {
            if (!(obj is BoardState)) return false;
            return ToString() == obj.ToString();
        }
        public string Draw()
        {
            var sb = new StringBuilder();
            for (int y = 0; y < Dimens[1]; y++)
            {
                sb.Append("|");
                for (int x = 0; x < Dimens[0]; x++)
                {
                    sb.Append(Board[y * Dimens[0] + x]);
                }
                sb.Append("|\n");
            }
            return sb.ToString();
            //return $"|{Board[0]}{Board[1]}{Board[2]}|\n|{Board[3]}{Board[4]}{Board[5]}|\n|{Board[6]}{Board[7]}{Board[8]}|\n";
        }
    }
}