namespace HvZ_API.Models.DTOs.Player
{
    public class PlayerCheckinPutDto
    {
        public int Id { get; set; }
        public double? CheckinLon { get; set; }
        public double? CheckinLat { get; set; }
    }
}
