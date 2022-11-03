namespace HvZ_API.Models.DTOs.Gravestone
{
    public class GravestonePostDto
    {
        public DateTime TimeOfDeath { get; set; }
        public string? Description { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int? KillerId { get; set; }
        public int GameId { get; set; }

    }
}
