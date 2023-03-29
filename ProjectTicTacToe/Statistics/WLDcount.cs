namespace ProjectTicTacToe
{
    public class WLDcount
    {
        public int wins;
        public int losses;
        public int draws;

        public WLDcount()
        {
            wins = 0;
            losses = 0;
            draws = 0;
        }
        public WLDcount(int wins, int losses, int draws)
        {
            this.wins = wins;
            this.losses = losses;
            this.draws = draws;
        }
        public WLDcount(WLDcount original) : this(original.wins, original.losses, original.draws) { }
    }
}
