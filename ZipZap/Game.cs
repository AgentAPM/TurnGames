

using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ZipZap
{
    struct Tile
    {
        public Tile()
        {

        }
        public int Player { get; set; }
        public int Facing { get; set; }
        public override string ToString()
        {
            return Player.ToString();
        }
    }
    class Game
    {
        public Tile[,] Board { get; private set; }
        private int BoardWidth;
        private int BoardHeight;
        public Game(int width, int height)
        {
            InitBoard(width, height);
        }

        private void InitBoard(int width, int height)
        {
            BoardWidth = width;
            BoardHeight = height;
            Board = new Tile[width,height];
            for (int x=0; x<width; x++)
            {
                for(int y=0; y<height; y++)
                {
                    Board[x, y] = new Tile();
                }
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            for (int y = 0; y < BoardHeight; y++)
            {
                for (int x = 0; x < BoardWidth; x++)
                {
                    builder.Append($"{Board[x,y]} ");
                }
                builder.Append('\n');
            }

            return builder.ToString();
        }
    }
}