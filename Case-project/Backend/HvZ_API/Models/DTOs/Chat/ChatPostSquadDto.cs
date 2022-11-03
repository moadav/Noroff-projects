namespace HvZ_API.Models.DTOs.Chat
{
    public class ChatPostSquadDto
    {
        public string Message { get; set; }
        public DateTime ChatTime { get; set; }
        public int? GameId { get; set; }
        public int? SquadId { get; set; }
        public int playerId { get; set; }


    }
}
