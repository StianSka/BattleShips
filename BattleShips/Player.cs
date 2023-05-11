namespace BattleShips
{
    internal class Player
    {
        public string[,] CordsShoot { get; set; }
        public bool IsPlayer { get; set; }
        public GameBoard ShipBoard { get; set; }
        public List<string> HasShoot { get; set; }
        public bool HasLost => ShipBoard.Ships.All(s => s.IsSunk);
        public Player(bool isplayer)
        {
            IsPlayer = isplayer;
            ShipBoard = new GameBoard();
            CordsShoot = new string[9, 9];
        }

        public void ShowBoard()
        {
            ShipBoard.PrintBoard();
        }

        public void PlaceShips()
        {
            ShipBoard.PlaceShip(this);
        }

        public void PlayerShoot()
        {
            bool isValid = false;
            while (!isValid)
            {
                int[] current = ValidShootInput();
                isValid = CheckShoot(current);
                if (isValid) CordsShoot[current[0], current[1]] = "X";
            }

        }

        public int[] ValidShootInput()
        {
            bool isValid = false;
            string input = "";
            int[] indexes = null;
            while (!isValid)
            {
                
                Console.WriteLine("Skriv en kordinatene til ruten du vil skyte");
                input = Console.ReadLine();
                indexes = ShipBoard.TranslatCords(input);
                isValid = CheckCords(indexes);
            }
            return indexes;
        }

        public bool CheckCords(int[] indexes)
        {
            bool isValid = false;
            if (indexes[0] >= 0 && indexes[1] >= 0 && indexes[0] <= 9 && indexes[1] <= 9)
            {
                isValid = true;
            }
            return isValid;
        }

        public void CPUShoot(GameBoard board)
        {
            bool isValid = false;
            while (!isValid)
            {
                int[] current = GetShootPossision();
                isValid = CheckShoot(current);
                if (isValid) CordsShoot[current[0], current[1]] = "X";
            }
        }

        public bool CheckShoot(int[] current)
        {
            return CordsShoot[current[0], current[1]] == null;
        }

        private int[] GetShootPossision()
        {
         var verticalPosition = Game.RandomNummber(0, 9);
         var horizontalPosition = Game.RandomNummber(0, 9);
         return new [] { verticalPosition, horizontalPosition };
        }
    }
}
