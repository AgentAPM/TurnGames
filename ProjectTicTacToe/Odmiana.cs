namespace ProjectTicTacToe
{
    public abstract class Mianownik
    {
        public static string rund(int ile)
        {
            if (ile == 1) return "runda";
            else if (ile % 100 - ile % 10 == 1) return "rund";
            else if (ile % 10 >= 2 && ile % 10 <= 4) return "rundy";
            else return "rund";
        }
        public static string Rund(int ile)
        {
            if (ile == 1) return "Runda";
            else if (ile % 100 - ile % 10 == 1) return "Rund";
            else if (ile % 10 >= 2 && ile % 10 <= 4) return "Rundy";
            else return "Rund";
        }
    }
    public abstract class Biernik
    {
        public static string rund(int ile)
        {
            if (ile == 1) return "rundę";
            else if (ile % 100 - ile % 10 == 1) return "rund";
            else if (ile % 10 >= 2 && ile % 10 <= 4) return "rundy";
            else return "rund";
        }
        public static string Rund(int ile)
        {
            if (ile == 1) return "Rundę";
            else if (ile % 100 - ile % 10 == 1) return "Rund";
            else if (ile % 10 >= 2 && ile % 10 <= 4) return "Rundy";
            else return "Rund";
        }
    }
}
