namespace BattleShips
{
    internal abstract class Ship
    {
        public int Size { get; set; }
        public string Name { get; set; }
        public string DisplayValue { get; set; }
        public int HitCounter { get; set; }
        public bool IsSunk => Size + 1 == HitCounter;
        public bool IsPlaced { get; set; } = false;

        protected Ship(int size, string name, string displayValue)
        {
            Size =  size;
            Name = name;
            HitCounter = 0;
            DisplayValue = displayValue;
        }
    }
}
