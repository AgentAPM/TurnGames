
namespace ProjectTicTacToe
{
    public class TicTacToe
    {
        const string playerIcons = "XOSHABCDEFGIJKLMNPQRSTUVWYZ";
        private string playerOrder;
        public Dictionary<string, BoardState> TicTacToePositions = new Dictionary<string, BoardState>();
        private BoardState CurrentState;
        private Dictionary<char, IPlayer> Players;
        private bool keepPlaying = true;
        private int[] dimens;
        public TicTacToe(IPlayer[] players, int[] dimens)
        {
            this.dimens = dimens;
            playerOrder = playerIcons.Substring(0, players.Length);

            Players = new Dictionary<char, IPlayer>();
            for (int i = 0; i < players.Length; i++)
            {
                players[i].Icon = playerIcons[i];
                Players[playerOrder[i]] = players[i];
            }
        }
        public TicTacToe(IPlayer[] players) : this(players, new int[] { 3, 3 }) { }
        public void EnterGame()
        {
            while (keepPlaying)
            {
                InitRound();

                GameLoop();

                RoundEnd();
            }

            Console.WriteLine("Dziękuję za grę");
        }


        private void InitRound()
        {
            keepPlaying = true;
            CurrentState = new BoardState("XO:         ");
            
        }
        private void GameLoop()
        {
            while (CurrentState.Winner == ' ')
            {
                Console.WriteLine($"{CurrentState.PlayerOnMove} na ruchu:");
                Console.WriteLine(CurrentState.Draw());

                var move = Players[CurrentState.PlayerOnMove].GetMove(CurrentState);
                CurrentState = new BoardState(CurrentState, move);

                var code = CurrentState.ToString();

                if (TicTacToePositions.ContainsKey(code)) CurrentState = TicTacToePositions[code];
                else TicTacToePositions[code] = CurrentState;
            }
        }
        private void RoundEnd()
        {
            Console.WriteLine(CurrentState.Draw());
            if (CurrentState.Winner != '-')
                Console.WriteLine($"Wygrywa {CurrentState.Winner}!!!");
            else
                Console.WriteLine("Remis.");

            Console.WriteLine("Zagrać jeszcze raz?");

            string input;
            bool wrongInput = false;
            do
            {
                wrongInput = false;
                input = Console.ReadLine();
                switch (input)
                {
                    case "t":
                    case "T":
                    case "tak":
                        keepPlaying = true;
                        break;
                    case "n":
                    case "N":
                    case "nie":
                        keepPlaying = false;
                        break;
                    default:
                        Console.WriteLine("Błędna komenda");
                        wrongInput = true;
                        break;
                }

            } while (wrongInput);
        }
    }
}
