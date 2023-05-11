namespace BattleShips
{
    internal class Game
    {
        public Player Player { get; set; }
        public Player CPU { get; set; }
        public bool GameOver => Player.HasLost || CPU.HasLost;
        public Game(Player player, Player cpu)
        {
            Player = player;
            CPU = cpu;
        }

        public void Run()
        {
            CPU.PlaceShips();
            //Player.PlaceShips();
            //while (!GameOver)
            //{
            while (true)
            {
                PrintShootBoard();
                CPU.ShowBoard();
                Player.PlayerShoot();
            }
            //check if victor maybe needs to happen after every shoot
            //}
        }

        public static int RandomNummber(int min, int max)
        {
            Random random = new Random();
            int result = random.Next(min, max);
            return result;
        }
        public void PrintShootBoard()
        {
            Console.Clear();
            Console.WriteLine(
                "    1   2   3   4   5   6   7   8   9\n  ╔═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╗");
            for (var i = 0; i < Player.CordsShoot.GetLength(0); i++)
            {
                var letter = Convert.ToChar(65 + i);
                Console.Write(letter + " ║");
                for (var j = 0; j < Player.CordsShoot.GetLength(1); j++)
                {
                    var value = GetHitValue(i, j);

                    if (j < 8)
                    {
                        Console.Write($" {value} │");
                    }
                    else

                        Console.WriteLine($" {value} ║");

                }

                if (i < 8)
                {
                    Console.WriteLine("  ╟───┼───┼───┼───┼───┼───┼───┼───┼───╢");
                }

            }
            Console.WriteLine("  ╚═══╧═══╧═══╧═══╧═══╧═══╧═══╧═══╧═══╝");
        }

        private string GetHitValue(int i, int j)
        {
            if (Player.CordsShoot[i, j] != null && CPU.ShipBoard.Board[i, j] != null) return "X";
            return Player.CordsShoot[i, j] == null ? " " : "m";
        }
    }
}