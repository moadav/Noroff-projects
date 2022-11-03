namespace HvZ_API.Models
{
    public class GameConfig
    { 
        public int Id { get; set; }
        public int PlayerCount { get; set; } = 32;
        public int InitZombies { get; set; } = 1;
        public double Duration { get; set; } = 24.0;
        public double HungerDuration { get; set; } = 12.0;
        public double ChatCooldown { get; set; } = 0;

    }
}
