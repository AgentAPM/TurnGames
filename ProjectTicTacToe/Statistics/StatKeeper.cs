namespace ProjectTicTacToe
{
    public class StatKeeper
    {
        public Dictionary<char, WLDcount> Records;
        public StatKeeper(IPlayer[] players)
        {
            Records = new Dictionary<char, WLDcount>();
            for (int i = 0; i < players.Length; i++)
            {
                char icon = TicTacToe.playerIcons[i];
                Records[icon] = new WLDcount();
            }
        }
        public void RecordRoundResult(TicTacToe game)
        {
            var winner = game.CurrentState.Winner;

            if (winner != ' ')
            {
                foreach (var pair in Records)
                {
                    var icon = pair.Key;
                    var record = pair.Value;

                    if (winner == '-')
                        record.draws++;
                    else if (winner == icon)
                        record.wins++;
                    else
                        record.losses++;
                }
            }
        }
        public void PrintGameResults()
        {
            foreach (var pair in Records)
            {
                var icon = pair.Key;
                var record = pair.Value;

                Console.WriteLine($"Gracz {icon} wygrał {record.wins} {Biernik.rund(record.wins)}, przegrał {record.losses} {Biernik.rund(record.losses)} i zremisował {record.draws} {Biernik.rund(record.draws)}.");
            }
        }
    }
}
