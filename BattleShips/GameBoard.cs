
namespace BattleShips
{
    internal class GameBoard
    {
        public Ship[,] Board;

        public Carrier Carrier { get; set; }
        public Battleship Battleship { get; set; }
        public Cruiser Cruiser { get; set; }
        public Destroyer Destroyer1 { get; set; }
        public Destroyer Destroyer2 { get; set; }
        public List<Ship> Ships => new List<Ship> { Carrier, Battleship, Cruiser, Destroyer1, Destroyer2 };
        
        public GameBoard()
        {
            Board = new Ship[9, 9];
            Carrier = new Carrier();
            Battleship = new Battleship();
            Cruiser = new Cruiser();
            Destroyer1 = new Destroyer();
            Destroyer2 = new Destroyer();
        }

        

        public void PrintBoard()
        {
            Console.Clear();
            Console.WriteLine(
                "    1   2   3   4   5   6   7   8   9\n  ╔═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╗");
            for (var i = 0; i < Board.GetLength(0); i++)
            {
                var letter = Convert.ToChar(65 + i);
                Console.Write(letter + " ║");
                for (var j = 0; j < Board.GetLength(1); j++)
                {
                    var value = GetDisplayValue(i, j);
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
            Console.WriteLine("  ╚═══╧═══╧═══╧═══╧═══╧═══╧═══╧═══╧═══╝\n H = Hangarskip (5 ruter)\n S = Slagskip (4 ruter)\n K = Krysser (3 ruter)\n J = Jager (2 ruter)\n");
        }

        public string GetDisplayValue(int y, int x)
        {
            switch (Board[y, x]?.Name)
            {
                case "Slagskip":
                    return ("S");

                case "Hangarskip":
                    return ("H");

                case "Krysser":
                    return ("K");

                case "Jager":
                    return ("J");

                default:
                    return (" ");
            }
        }

        public void AddShip(Ship ship, string startPosition, string direction)
        {
            var startPositionIndex = TranslatCords(startPosition);
            var endPosition = GetEndPosition(ship, startPosition, direction);

            if (arePositionsFree(startPositionIndex, endPosition, direction))
            {
                MarkPositions(startPositionIndex, endPosition, direction, ship);
            }
        }

        public int[] GetEndPosition(Ship ship, string startPosition, string direction)
        {
            var startCoordinates = TranslatCords(startPosition);
            var startRow = startCoordinates[0];
            var startColumn = startCoordinates[1];
            var endRow = direction == "h" ? startRow : startRow + ship.Size;
            var endColumn = direction == "h" ? startColumn + ship.Size : startColumn;
            return new int[] { endRow, endColumn };
        }

        public int[] TranslatCords(string cordinate)
        {
            char firstCord = cordinate[0];
            char secondCord = cordinate[1];
            int rowIndex = Convert.ToInt32(firstCord) - 97;
            int lineIndex = Convert.ToInt32(secondCord) - 49;
            int[] Value = new int[] { rowIndex, lineIndex };
            return Value;
        }
        public void MarkHitt(string shoot)
        {
            int[] indexes = TranslatCords(shoot);

        }

        private bool arePositionsFree(int[] startPositionIndex, int[] endPosition, string direction)
        {
            if (direction == "h")
            {
                for (int i = startPositionIndex[1]; i <= endPosition[1]; i++)
                {
                    if (Board[startPositionIndex[0], i] != null) { return false; }
                }
            }
            else if (direction == "v") { }
            {
                for (int i = startPositionIndex[0]; i <= endPosition[0]; i++)
                {
                    if (Board[i, endPosition[1]] != null) { return false; }
                }
            }
            return true;
        }

        private void MarkPositions(int[] startPositionIndex, int[] endPosition, string direction, Ship ship)
        {
            if (direction == "h")
            {
                for (int i = startPositionIndex[1]; i <= endPosition[1]; i++)
                {
                    Board[startPositionIndex[0], i] = ship;
                }
            }
            else if (direction == "v")
            {
                for (int i = startPositionIndex[0]; i <= endPosition[0]; i++)
                {
                    Board[i, endPosition[1]] = ship;
                }
            }
            ship.IsPlaced = true;
        }

        public void PlaceShip(Player player)
        {
            foreach (Ship ship in Ships)
            {
                if (player.IsPlayer) playerPlaceLoop(ship);
                else { CPUPlaceLoop(ship); }
            }
        }

        public void playerPlaceLoop(Ship ship)
        {
            while (ship.IsPlaced == false)
            {
                var placement = GetPlacement(ship);
                AddShip(ship, placement[0], placement[1]);
                if (!ship.IsPlaced) ErrorMessage("DET ER ALLEREDE ET SKIP DER!");
                PrintBoard();
            }
        }

        public void CPUPlaceLoop(Ship ship)
        {
            while (ship.IsPlaced == false)
            {
                var placement = GetCPUPlacement(ship);
                AddShip(ship, placement[0], placement[1]);
            }
        }

        public string[] GetPlacement(Ship ship)
        {
            int size = ship.Size;
            string pos = "";
            string direction = "";
            int[] inputIndex;
            bool isValid = false;
            while (isValid == false)
            {
                PrintBoard();
                pos = GetInput($"Hvor vil du at {ship.Name} skal starte?");
                direction = GetInput($"Hvilken retning vil du legge skipet ditt? (h = horisontalt \\ v = vertikalt");
                inputIndex = TranslatCords(pos);
                if (BoatIsFittingOnBoard(inputIndex, size, direction))
                {
                    isValid = true;
                }
                else
                {
                    ErrorMessage("UGYLDIG POSISJON!");
                }
            }
            return new string[] { pos, direction };
        }

        public string[] GetCPUPlacement(Ship ship)
        {
            int size = ship.Size;
            string pos = "";
            string direction = "";
            bool isValid = false;
            while (!isValid)
            {
                pos = GetRandomPosition();
                direction = Game.RandomNummber(0, 2) == 0 ? "h" : "v";
                var inputIndex = TranslatCords(pos);

                if (BoatIsFittingOnBoard(inputIndex, size, direction))
                {
                    isValid = true;
                }
                
            }
            return new string[] { pos, direction };
        }

        private string GetRandomPosition()
        {
            var letters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i" };
            var verticalPosition = Game.RandomNummber(0, 9);
            var horizontalPosition = Game.RandomNummber(1, 10);
            var letter = letters[verticalPosition];
            var position = letter + horizontalPosition;
            return position;
        }

        private bool BoatIsFittingOnBoard(int[] inputIndex, int size, string direction)
        {
            return inputIndex[0] + size < 9 && direction == "v" ||
                   inputIndex[1] + size < 9 && direction == "h";
        }

        public void ErrorMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Trykke en tast for å fortsette");
            Console.ReadKey();
        }

        private string GetInput(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

    }
}
