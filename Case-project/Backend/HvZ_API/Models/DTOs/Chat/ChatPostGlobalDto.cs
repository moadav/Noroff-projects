namespace HvZ_API.Models.DTOs.Chat
{
    public class ChatPostGlobalDto
    {
        public string Message { get; set; }
        public DateTime ChatTime { get; set; }
        public int? GameId { get; set; }
        public int playerId { get; set; }
    }
}
