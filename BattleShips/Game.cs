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
            Player.PlaceShips();
            while (!GameOver)
            {
                PrintEnemyShootBoard();
                PrintShootBoard();
                Player.PlayerShoot(CPU.ShipBoard);
                if (!GameOver) CPU.CPUShoot(Player.ShipBoard);
            }

            var winner = Player.HasLost ? "CPU" : "Du";
            Console.WriteLine($"{winner} vant!");
        }

        public static int RandomNummber(int min, int max)
        {
            Random random = new Random();
            int result = random.Next(min, max);
            return result;
        }
        public void PrintShootBoard()
        {
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
            Console.WriteLine();
            Console.WriteLine("Gjenværende skip:");
            Console.WriteLine();
            Console.WriteLine(GetShipStatus());
            Console.WriteLine();
        }


        private string GetHitValue(int i, int j)
        {
            if (Player.CordsShoot[i, j] != null && CPU.ShipBoard.Board[i, j] != null) return "X";
            return Player.CordsShoot[i, j] == null ? " " : "m";
        }

        public void PrintEnemyShootBoard()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(
                "    1   2   3   4   5   6   7   8   9\n  ╔═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╗");
            for (var i = 0; i < Player.ShipBoard.Board.GetLength(0); i++)
            {
                var letter = Convert.ToChar(65 + i);
                Console.Write(letter + " ║");
                for (var j = 0; j < Player.ShipBoard.Board.GetLength(1); j++)
                {

                    var value = CPU.CordsShoot[i, j] == null ? GetEnemyHitValue(i, j) : GetEnemyShotValue(i, j);

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

        private string GetShipStatus()
        {
            var ships = CPU.ShipBoard.Ships.Where(ship => ship.HitCounter > 0).Aggregate("", (current, ship) => current + (ship.Name + "\n"));
            return ships;
        }


        private string GetEnemyShotValue(int i, int j)
        {
            if (CPU.CordsShoot[i, j] == "X" && Player.ShipBoard.Board[i, j] != null)
            {
                return "/";
            }

            return "X";
        }

        private string GetEnemyHitValue(int i, int j)
        {

            switch (Player.ShipBoard.Board[i, j]?.DisplayValue)
            {
                case "S":
                    return ("S");

                case "H":
                    return ("H");

                case "K":
                    return ("K");

                case "J":
                    return ("J");

                case "X":
                    return ("X");
                case "/":
                    return ("/");

                default:
                    return (" ");
            }
        }
    }
}
