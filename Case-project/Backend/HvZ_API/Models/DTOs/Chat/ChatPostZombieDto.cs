namespace HvZ_API.Models.DTOs.Chat
{
    public class ChatPostZombieDto
    {
        public string Message { get; set; }
        public bool IsZombieGlobal { get; set; }
        public DateTime ChatTime { get; set; }
        public int? GameId { get; set; }
        public int playerId { get; set; }

    }
}
