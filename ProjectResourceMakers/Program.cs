namespace ResourceMakers
{
    public class Program
    {
        int turn;
        
        int Ore;
        int Clay;
        int Obsidian;
        int Geodes;

        int OreRobots;
        int ClayRobots;
        int ObsidianRobots;
        int GeodeRobots;

        bool gameOn;
        public static void Main(String[] Args)
        {
            var game=new Program();
            game.Game();
        }

        public void Game()
        {
            StartGame();

            while (gameOn)
            {
                Console.WriteLine($"It is day {turn}");
                Console.WriteLine($"{OreRobots} ore robots, {ClayRobots} clay robots, {ObsidianRobots} obsidian robots, {GeodeRobots} geode robots");
                Console.WriteLine($"({Ore} ore, {Clay} clay, {Obsidian} obsidian, {Geodes} geodes)");

                Console.WriteLine("0. Don't build robots");
                Console.WriteLine("1. Build ore robot (4 ore)");
                Console.WriteLine("2. Build clay robot (2 ore)");
                Console.WriteLine("3. Build obsidian robot (3 ore, 14 clay)");
                Console.WriteLine("4. Build geode robot (2 ore 7 obsidian)");
                bool invalidInput;
                do
                {
                    invalidInput = false;
                    Console.Write("Choose an option: ");
                    string option = Console.ReadLine();
                    switch (option)
                    {
                        case "0":break;
                        case "1":
                            if (Ore >= 4)
                            {
                                Ore -= 4;
                                OreRobots += 1;
                            } else {
                                Console.WriteLine("Not enough resources.");
                                invalidInput = true;
                            }
                            break;
                        case "2":
                            if (Ore >= 2)
                            {
                                Ore -= 2;
                                ClayRobots += 1;
                            }
                            else
                            {
                                Console.WriteLine("Not enough resources.");
                                invalidInput = true;
                            }
                            break;
                        case "3":
                            if (Ore >= 3 && Clay>=14)
                            {
                                Ore -= 3;
                                Clay -= 14;
                                ObsidianRobots += 1;
                            }
                            else
                            {
                                Console.WriteLine("Not enough resources.");
                                invalidInput = true;
                            }
                            break;
                        case "4":
                            if (Ore >= 2 && Obsidian>=7)
                            {
                                Ore -= 2;
                                Obsidian -= 7;
                                GeodeRobots += 1;
                            }
                            else
                            {
                                Console.WriteLine("Not enough resources.");
                                invalidInput = true;
                            }
                            break;

                        default:
                            Console.WriteLine("Wrong input.");
                            invalidInput = true;
                            break;
                    }
                } while (invalidInput);

                EndTurn();
            }
        }

        void StartGame()
        {
            turn = 1;

            Ore = 0;
            Clay = 0;
            Obsidian = 0;
            Geodes = 0;

            OreRobots = 1;
            ClayRobots = 0;
            ObsidianRobots = 0;
            GeodeRobots = 0;

            gameOn = true;
        }

        void EndTurn()
        {
            turn += 1;

            Ore += OreRobots;
            Clay += ClayRobots;
            Obsidian += ObsidianRobots;
            Geodes += GeodeRobots;

            if (Geodes >= 50)
            {
                Console.WriteLine($"You produced enough geodes in {turn} days!!!");
                gameOn = false;
            }
        }
    }
}