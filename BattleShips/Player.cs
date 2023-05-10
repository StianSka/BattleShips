namespace BattleShips
{
    internal class Player
    {
        public List<string> CordsShoot { get; set; }
        public bool IsPlayer { get; set; }
        public GameBoard Board { get; set; }
        public List<string> HasShoot { get; set; }
        public bool HasLost => Board.Ships.All(s => s.IsSunk);
        public Player(bool isplayer)
        {
            IsPlayer = isplayer;
            Board = new GameBoard();
            HasShoot = new List<string>();
            CordsShoot = new List<string>();
        }

        public void ShowBoard()
        {
            Board.PrintBoard();
        }

        public void PlaceShips()
        {
            Board.PlaceShip(this);
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
