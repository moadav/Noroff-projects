namespace HvZ_API.Models.DTOs.GameConfig
{
    public class GameConfigPutDto
    {
        public int Id { get; set; }
        public int PlayerCount { get; set; }
        public int InitZombies { get; set; }
        public double Duration { get; set; }
        public double HungerDuration { get; set; }
        public double ChatCooldown { get; set; }
    }
}
