namespace BattleShips
{
    internal class Program
    {
        static void Main()
        {
            Console.SetWindowSize(100, 60);
            var playerOne = new Player(true);
            var cpu = new Player(false);
            var game = new Game(playerOne, cpu);
            game.Run();
        }
    }
}