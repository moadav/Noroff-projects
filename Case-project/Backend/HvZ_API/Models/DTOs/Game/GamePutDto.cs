using HvZ_API.Models.DTOs.GameConfig;

namespace HvZ_API.Models.DTOs.Game
{
    public class GamePutDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public bool Is_Started { get; set; }
        public double nw_Lat { get; set; }
        public double nw_Lng { get; set; }
        public double se_Lat { get; set; }
        public double se_Lng { get; set; }
        public string? image { get; set; }
        public int GameConfigId { get; set; }
    }
}
