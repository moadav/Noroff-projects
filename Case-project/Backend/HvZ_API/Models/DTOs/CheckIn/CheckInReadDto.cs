namespace HvZ_API.Models.DTOs.CheckIn
{
    public class CheckInReadDto
    {
        public int Id { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }

        public int PlayerId { get; set; }
    }
}
