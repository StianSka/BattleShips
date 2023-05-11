namespace BattleShips
{
    internal class Player
    {
        public List<string> CordsShoot { get; set; }
        public bool IsPlayer { get; set; }
        public GameBoard ShipBoard { get; set; }
        public List<string> HasShoot { get; set; }
        public bool HasLost => ShipBoard.Ships.All(s => s.IsSunk);
        public Player(bool isplayer)
        {
            IsPlayer = isplayer;
            ShipBoard = new GameBoard();
            HasShoot = new List<string>();
            CordsShoot = new List<string>();
        }

        public void ShowBoard()
        {
            ShipBoard.PrintBoard();
        }

        public void PlaceShips()
        {
            ShipBoard.PlaceShip(this);
        }

        public void PlayerShoot(GameBoard board)
        {
            bool isValid = false;
            string shoot = "";
            while (!isValid)
            {
                string current = ValidShootInput();
                isValid = CheckShoot(current);
                if (isValid) { CordsShoot.Add(current); }
                shoot = current;
            }
            board.MarkHitt(shoot);
        }
        public string ValidShootInput()
        {
            bool isValid = false;
            string input = "";
            int[] indexes;
            while (!isValid)
            {
                Console.Clear();
                ShipBoard.PrintBoard();
                Console.WriteLine("Skriv en kordinatene til ruten du vil skyte");
                input = Console.ReadLine();
                indexes = ShipBoard.TranslatCords(input);
                isValid = CheckCords(indexes);
            }
            return input;
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
            string shoot = "";
            while (!isValid) 
            {
                string  current = GetShootPossision();
                isValid = CheckShoot(current);
                if (isValid) { CordsShoot.Add(current); }
                shoot = current;
            }
            board.MarkHitt(shoot);
        }
        public bool CheckShoot(string current)
        {
            for (int i = 0; i < CordsShoot.Count; i++)
            {
                if (CordsShoot[i] == current)
                {
                    return false;
                }
            }
            return true;
        }
        private string GetShootPossision()
        {
            var letters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i" };
            var verticalPosition = Game.RandomNummber(0, 9);
            var horizontalPosition = Game.RandomNummber(1, 10);
            var letter = letters[verticalPosition];
            var position = letter + horizontalPosition;
            return position;
        }



    }
}
