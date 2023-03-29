namespace ProjectTicTacToe
{
    public class TicTacToe
    {
        public const string playerIcons = "XOSHABCDEFGIJKLMNPQRSTUVWYZ";
        private readonly string StartingPosition = "XO:         ";
        private string playerOrder;
        private Dictionary<char, IPlayer> PlayersOnMove;
        public BoardState CurrentState { get; private set; }
        public bool KeepPlaying { get; set; } = true;
        private int[] Dimens { get; set; }
        public IPlayer[] Players { get; private set; }


        public event GameEvent OnGameStart;
        public event GameEvent OnGameEnd;
        public event GameEvent OnRoundStart;
        public event GameEvent OnRoundEnd;
        public event GameEvent OnTurnStart;
        public event GameEvent OnTurnEnd;


        public TicTacToe(IPlayer[] players, int[] dimens)
        {
            Dimens = dimens;
            Players = players;
            playerOrder = playerIcons.Substring(0, players.Length);

            PlayersOnMove = new Dictionary<char, IPlayer>();
            for (int i = 0; i < players.Length; i++)
            {
                players[i].Icon = playerIcons[i];
                PlayersOnMove[playerOrder[i]] = players[i];
            }
        }
        public TicTacToe(IPlayer[] players) : this(players, new int[] { 3, 3 }) { }
        public void StartGame()
        {
            KeepPlaying = true;

            OnGameStart?.Invoke(this);

            while (KeepPlaying)
            {
                InitRound();
                OnRoundStart?.Invoke(this);

                GameLoop();

                KeepPlaying = false;
                OnRoundEnd?.Invoke(this);
            }

            OnGameEnd?.Invoke(this);
        }


        private void InitRound()
        {
            CurrentState = BoardState.FromCode(StartingPosition);
        }
        private void GameLoop()
        {
            while (CurrentState.Winner == ' ')
            {
                OnTurnStart?.Invoke(this);

                var move = PlayersOnMove[CurrentState.PlayerOnMove].GetMove(CurrentState);
                CurrentState = CurrentState.AfterMove(move);

                OnTurnEnd?.Invoke(this);
            }
        }
    }
}
