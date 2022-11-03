namespace HvZ_API.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool? IsHumanGlobal { get; set; }
        public bool? IsZombieGlobal { get; set; }
        public DateTime ChatTime { get; set; }
        //Navigation
        public int? GameId { get; set; }
        public Game? Game { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int? SquadId { get; set; }
        public Squad? Squad { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }



    }
}
