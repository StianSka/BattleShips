namespace BattleShips
{
    internal class Player
    {
        public string[,] CordsShoot { get; set; }
        public bool IsPlayer { get; set; }
        public GameBoard ShipBoard { get; set; }
        public List<string> HasShoot { get; set; }
        public bool HasLost => ShipBoard.Ships.All(s => s.HitCounter == 0);
        public Player(bool isplayer)
        {
            IsPlayer = isplayer;
            ShipBoard = new GameBoard();
            CordsShoot = new string[9, 9];
        }


        public void PlaceShips()
        {
            ShipBoard.PlaceShip(this);
        }

        public void PlayerShoot(GameBoard board)
        {
            bool isValid = false;
            while (!isValid)
            {
                Ship? hitShip = null;
                int[] current = ValidShootInput();
                isValid = CheckShoot(current);
                
                if (isValid)
                {
                    hitShip = board.Board[current[0], current[1]];
                    CordsShoot[current[0], current[1]] = "X";

                }
                if (hitShip != null)
                {
                    hitShip.HitCounter--;
                    if (hitShip.HitCounter <= 0)
                    {
                        ErrorMessage($"DU SENKET FIENDENS {hitShip.Name}!");
                        
                    }
                }

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
                if (!isValid)
                {
                    ErrorMessage("UGYLDIGE KOORDINATER!");
                }
            }
            return indexes;
        }

        public bool CheckCords(int[] indexes)
        {
            bool isValid = indexes[0] >= 0 && indexes[1] >= 0 && indexes[0] <= 8 && indexes[1] <= 8;
            return isValid;
        }

        public void CPUShoot(GameBoard board)
        {
            Ship? hitShip = null;
            bool isValid = false;
            while (!isValid)
            {
                int[] current = GetShootPossision();
                isValid = CheckShoot(current);
                if (isValid)
                {
                    CordsShoot[current[0], current[1]] = "X";
                    hitShip = board.Board[current[0], current[1]];
                    if (hitShip != null)
                    {
                        hitShip.HitCounter--;
                    }

                }
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
            return new[] { verticalPosition, horizontalPosition };
        }
        public void ErrorMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Trykke en tast for å fortsette");
            Console.ReadKey();
        }
    }
}
