namespace HvZ_API.Models.DTOs.Chat
{
    public class ChatReadDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool? IsHumanGlobal { get; set; }
        public bool? IsZombieGlobal { get; set; }
        public DateTime ChatTime { get; set; }
        public int? GameId { get; set; }
        public int PlayerId { get; set; }
        public int? SquadId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
