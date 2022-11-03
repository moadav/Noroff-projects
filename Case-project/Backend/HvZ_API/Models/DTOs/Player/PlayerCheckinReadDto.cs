namespace HvZ_API.Models.DTOs.Player
{
    public class PlayerCheckinReadDto
    {
        public int Id { get; set; }
        public double? CheckinLon { get; set; }
        public double? CheckinLat { get; set; }

        public string? FirstName { get; set; }
    }
}
