using HvZ_API.Models.DTOs.GameConfig;

namespace HvZ_API.Models.DTOs.Game
{
    public class GameReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public bool Is_Started { get; set; }
        public double Nw_Lat { get; set; }
        public double Nw_Lng { get; set; }
        public double Se_Lat { get; set; }
        public double Se_Lng { get; set; }
        public string? Image { get; set; }
        public int GameConfigId { get; set; }

    }
}
