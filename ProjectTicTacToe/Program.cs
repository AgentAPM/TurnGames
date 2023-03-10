

using System.Security.Cryptography;
using System.Text;

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
        public static Move FromInput(string input)
        {
            return new Move(0, 'X');
        }
        public override bool Equals(object? obj)
        {
            if (!(obj is Move)) return false;

            var other = obj as Move;

            return this.Tile == other.Tile && this.Player == other.Player;
        }
    }
    public class BoardState
    {
        public BoardState()
        {
            Board = new char[] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
            playerQueue = "XO";
        }
        public BoardState(string positionCode)
        {
            var sections = positionCode.Split(':');
            if (!(sections.Length == 2 && sections[1].Length == 9)) throw new Exception(positionCode);

            Board = sections[1].ToCharArray();
            playerQueue = sections[0];
        }
        public BoardState(string fromPosition, Move afterMove) : this(fromPosition)
        {
            Board[afterMove.Tile] = afterMove.Player;
            playerQueue = playerQueue.Substring(1) + playerQueue[0];
        }
        public BoardState(BoardState previousPosition, Move afterMove)
        {
            Board = new char[previousPosition.Board.Length];
            Array.Copy(previousPosition.Board, Board, previousPosition.Board.Length);

            Board[afterMove.Tile] = afterMove.Player;
            playerQueue = previousPosition.playerQueue.Substring(1) + previousPosition.playerQueue[0];
        }
        public readonly char[] Board;
        public char PlayerOnMove
        {
            get
            {
                return playerQueue[0];
            }
        }
        public readonly string playerQueue;
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
            return $"|{Board[0]}{Board[1]}{Board[2]}|\n|{Board[3]}{Board[4]}{Board[5]}|\n|{Board[6]}{Board[7]}{Board[8]}|\n";
        }
    }
    public class BoardTransposition : BoardState
    {
        private static readonly int[][] symmetries;

        static BoardTransposition()
        {
            symmetries = new int[8][];
            symmetries[0] = new int[] {
                0, 1, 2,
                3, 4, 5,
                6, 7, 8
            };
            symmetries[1] = new int[] {
                0, 3, 6,
                1, 4, 7,
                2, 5, 8
            };
            symmetries[2] = new int[] {
                2, 1, 0,
                5, 4, 3,
                8, 7, 6
            };
            symmetries[3] = new int[] {
                6, 3, 0,
                7, 4, 1,
                8, 5, 2
            };
            symmetries[4] = new int[] {
                6, 7, 8,
                3, 4, 5,
                0, 1, 2
            };
            symmetries[5] = new int[] {
                2, 4, 8,
                1, 4, 7,
                0, 3, 5
            };
            symmetries[6] = new int[] {
                8, 7, 6,
                5, 4, 3,
                2, 1, 0
            };
            symmetries[7] = new int[] {
                8, 5, 2,
                7, 4, 1,
                6, 3, 0
            };
        }
        public static string[] FindTranspositions(BoardState from)
        {

            return new string[0];
        }
        public override bool Equals(object? obj)
        {
            if (!(obj is BoardState) ) return false;
            var other = obj as BoardState;

            bool isTransposition = false;
            foreach(var symmetry in symmetries)
            {

            }
            return isTransposition;
        }
    }
    public interface IPlayer
    {
        Move GetMove(BoardState position);
    }
    public class RandomPlayer : IPlayer
    {
        private Random RNG = new Random();
        public Move GetMove(BoardState position)
        {
            var moves = position.PossibleMoves;
            return moves[RNG.Next(moves.Length)];
        }
    }
    public class ConsolePlayer : IPlayer
    {
        public Move GetMove(BoardState position)
        {
            var moves = position.PossibleMoves;

            Console.WriteLine("Podaj pole (YX: 11-33 lub cyfra z klawiatury bocznej)");
            string input;
            Move move = null;
            bool correct = true;
            do
            {
                input = Console.ReadLine();
                correct = true;
                switch (input)
                {
                    case "11":
                    case "7":
                        move = new Move(0,position.PlayerOnMove); break;
                    case "12":
                    case "8":
                        move = new Move(1,position.PlayerOnMove); break;
                    case "13":
                    case "9":
                        move = new Move(2,position.PlayerOnMove); break;

                    case "21":
                    case "4":
                        move = new Move(3,position.PlayerOnMove); break;
                    case "22":
                    case "5":
                        move = new Move(4,position.PlayerOnMove); break;
                    case "23":
                    case "6":
                        move = new Move(5,position.PlayerOnMove); break;

                    case "31":
                    case "1":
                        move = new Move(6,position.PlayerOnMove); break;
                    case "32":
                    case "2":
                        move = new Move(7,position.PlayerOnMove); break;
                    case "33":
                    case "3":
                        move = new Move(8,position.PlayerOnMove); break;
                        
                    default:
                        correct = true; break;
                }

                correct &= moves.Contains(move);

                if (!correct)
                    Console.WriteLine("Podany ruch jest niepoprawny.");

            } while (!correct);

            return move;
        }
    }
    public static class Program
    {
        public static int Main(string[] args)
        {
            var TicTacToePositions = new Dictionary<string, BoardState>();
            var Players = new Dictionary<char, IPlayer>
            {
                { 'X', new ConsolePlayer() },
                { 'O', new RandomPlayer() }
            };

            var CurrentState = new BoardState("XO:         ");
            Console.WriteLine(CurrentState.Draw());

            while (CurrentState.Winner == ' ')
            {
                var move = Players[CurrentState.PlayerOnMove].GetMove(CurrentState);
                CurrentState = new BoardState(CurrentState, move);

                var code = CurrentState.ToString();

                if (TicTacToePositions.ContainsKey(code)) CurrentState = TicTacToePositions[code];
                else TicTacToePositions[code] = CurrentState;

                Console.WriteLine(CurrentState.Draw());
            }
            Console.WriteLine(CurrentState.Winner + " wins");

            return 0;
        }
    }
}