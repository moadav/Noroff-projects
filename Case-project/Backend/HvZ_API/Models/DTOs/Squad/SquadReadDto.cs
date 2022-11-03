using HvZ_API.Models.DTOs.Player;

namespace HvZ_API.Models.DTOs.Squad
{
    public class SquadReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int MemberCount { get; set; }
        public int GameId { get; set; }

    }
}
