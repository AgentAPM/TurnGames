namespace ProjectTicTacToe
{
    public class ConsolePlayer : IPlayer
    {
        public char Icon { get; set; } = '?';
        public ConsolePlayer()
        {

        }
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
                        move = new Move(0, Icon); break;
                    case "12":
                    case "8":
                        move = new Move(1, Icon); break;
                    case "13":
                    case "9":
                        move = new Move(2, Icon); break;

                    case "21":
                    case "4":
                        move = new Move(3, Icon); break;
                    case "22":
                    case "5":
                        move = new Move(4, Icon); break;
                    case "23":
                    case "6":
                        move = new Move(5, Icon); break;

                    case "31":
                    case "1":
                        move = new Move(6, Icon); break;
                    case "32":
                    case "2":
                        move = new Move(7, Icon); break;
                    case "33":
                    case "3":
                        move = new Move(8, Icon); break;

                    default:
                        correct = false; break;
                }

                correct &= moves.Contains(move);

                if (!correct)
                    Console.WriteLine("Podany ruch jest niepoprawny.");

            } while (!correct);

            return move;
        }
    }
}