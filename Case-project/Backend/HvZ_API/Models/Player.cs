namespace HvZ_API.Models
{
    public class Player
    {
        public int Id { get; set; }
        public bool IsHuman { get; set; }
        public bool IsPatientZero { get; set; }
        public double HungerTime { get; set; }
        public string BiteCode { get; set; }
        public bool IsMuted { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public virtual ICollection<Chat>? Chats { get; set; }
        public int? SquadId { get; set; }
        public Squad? Squad { get; set; }
        public ICollection<Gravestone>? KillerStones { get; set; }
        public int? VictimId { get; set; }
        public Gravestone? Victim { get; set; }
        public double? CheckinLon { get; set; }
        public double? CheckinLat { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


    }
}
