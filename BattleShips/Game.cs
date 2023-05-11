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
            Player.ShowBoard();
            CPU.PlaceShips();
            Player.PlaceShips();
            //while (!GameOver)
            //{
            //CPU.CPUShoot(Player.Board);
            //Player.ShowBoard();
            //Player.PlayerShoot(CPU.Board);
            //Player.ShowBoard();
            // check if victor maybe needs to happen after every shoot 
            //}
        }

        public static int RandomNummber(int min, int max)
        {
            Random random = new Random();
            int result = random.Next(min, max);
            return result;
        }
    }
}