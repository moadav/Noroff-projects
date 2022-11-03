namespace HvZ_API.Models.DTOs.Player
{
    public class PlayerPutDto
    {
        public int Id { get; set; }
        public bool IsHuman { get; set; }
        public bool IsPatientZero { get; set; }
        public double HungerTime { get; set; }
        public string BiteCode { get; set; }
        public bool IsMuted { get; set; }
        public int GameId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
