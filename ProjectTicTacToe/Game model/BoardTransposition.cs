namespace ProjectTicTacToe
{
    /*
        Error: nie podano argumentu dimens
    */
    /*
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
            var transpositionCodes = new List<string>();
            var sections = from.ToString().Split(":");
            var playerQueue = sections[0];
            var positionCode = sections[1];

            foreach (var symmetry in symmetries)
            {
                var transposition = new char[positionCode.Length];
                for (int t = 0; t < positionCode.Length; t++)
                {
                    transposition[t] = positionCode[symmetry[t]];
                }
                var transpositionAsString = $"{playerQueue}:{new string(transposition)}";

                if (transpositionAsString != positionCode)
                    transpositionCodes.Add(transpositionAsString);
            }
            return transpositionCodes.ToArray();
        }
        public override bool Equals(object? obj)
        {
            if (!(obj is BoardState)) return false;
            var other = obj as BoardState;

            bool isTransposition = false;
            foreach (var symmetry in symmetries)
            {
                bool allMatch = true;
                for (int i = 0; i < Board.Length; i++)
                {
                    if (Board[i] != other.Board[symmetry[i]])
                    {
                        allMatch = false;
                        break;
                    }
                }
                if (allMatch)
                {
                    isTransposition = true;
                    break;
                }
            }
            return isTransposition;
        }
    }
    //*/
}