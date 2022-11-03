namespace HvZ_API.Models.DTOs.Player
{
    public class PlayerPostDto
    {
        public bool IsHuman { get; set; }
        public bool IsPatientZero { get; set; }
        public double HungerTime { get; set; }
        public string BiteCode { get; set; }
        public bool IsMuted { get; set; }
        public int GameId { get; set; }
        public string userId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
