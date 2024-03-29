﻿namespace ProjectTicTacToe
{
    public class ExplorerPlayer : IPlayer
    {
        public char Icon { get; set; }
        private Random RNG = new Random();
        private Dictionary<string, uint> memories = new Dictionary<string, uint>();

        public event GameEvent OnMakeMove;

        public Move GetMove(BoardState position)
        {
            var positionCode = position.PositionCode;
            if (!memories.ContainsKey(positionCode))
                memories[positionCode] = 0;

            OnMakeMove?.Invoke(this, new ExplorerMoveArgs(memories[positionCode]));

            memories[positionCode]++;

            var moves = position.PossibleMoves;

            var moveCandidates = new List<Move>(moves);
            uint bestFamiliarity = uint.MaxValue;

            foreach (var move in moves)
            {
                var actionCode = move.ToAction(position);
                uint actionFamiliarity = 0;

                if (memories.ContainsKey(actionCode))
                    actionFamiliarity = memories[actionCode];
                else
                    memories[actionCode] = actionFamiliarity;

                if (actionFamiliarity <= bestFamiliarity)
                {
                    if (actionFamiliarity < bestFamiliarity)
                        moveCandidates = new List<Move>();

                    moveCandidates.Add(move);
                    bestFamiliarity = actionFamiliarity;
                }
            }

            int pick = RNG.Next(moveCandidates.Count);
            var pickedMove = moveCandidates[pick];

            ++memories[pickedMove.ToAction(position)];


            return pickedMove;
        }

        internal class ExplorerMoveArgs : EventArgs
        {
            public uint Familiarity { get; set; }
            public ExplorerMoveArgs(uint familiarity)
            {
                Familiarity = familiarity;
            }
        }
        public static void SayOnMakeMove(object sender, EventArgs e)
        {
            if (!(sender is ExplorerPlayer)) throw new Exception("Sender error");
            if (!(e is ExplorerMoveArgs)) throw new Exception("Args error");

            var args = e as ExplorerMoveArgs;

            if (args.Familiarity == 0)
                Console.WriteLine($"Komputer jeszcze nie widział tej pozycji.");
            else if (args.Familiarity == 1)
                Console.WriteLine($"Komputer widział tę pozycję {args.Familiarity} raz.");
            else
                Console.WriteLine($"Komputer widział tę pozycję już {args.Familiarity} razy.");
        }

    }
}