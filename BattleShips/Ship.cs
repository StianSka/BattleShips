namespace BattleShips
{
    internal abstract class Ship
    {
        public int Size { get; set; }
        public string Name { get; set; }
        public string DisplayValue { get; set; }
        public int HitCounter { get; set; }
        public bool IsPlaced { get; set; } = false;

        protected Ship(int size, string name, string displayValue)
        {
            Size =  size;
            Name = name;
            HitCounter = Size + 1;
            DisplayValue = displayValue;
        }
    }
}
