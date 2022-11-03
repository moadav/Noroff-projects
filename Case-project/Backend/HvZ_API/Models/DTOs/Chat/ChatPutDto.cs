namespace HvZ_API.Models.DTOs.Chat
{
    public class ChatPutDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsHumanGlobal { get; set; }
        public bool IsZombieGlobal { get; set; }
        public DateTime ChatTime { get; set; }

    }
}
