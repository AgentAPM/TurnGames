using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnGames.MoveManager
{
    internal class Context
    {
        public Context() { }
    }
    internal abstract class Move
    { 
        public abstract bool Do(Context gameState);
        public abstract bool Undo(Context gameState);
    }
    internal class GameLog
    {
        Context gameState;
        List<Move> moveList;
        int currentMove = 0;
        public GameLog(Context gameState) {
            this.gameState = gameState;
            this.moveList = new List<Move>();


        }

        public void SubmitMove(Move move)
        {
            moveList.Add(move);
        }
        public void Execute(int targetIndex)
        {


        }
    }
}
