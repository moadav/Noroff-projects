namespace HvZ_API.Models
{
    public class Gravestone
    {
        public int Id { get; set; }
        public DateTime TimeOfDeath { get; set; }
        public string? Description { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }

        //Navigations

        //Game
        public int GameId { get; set; }
        public Game Game { get; set; }

        //Zombie Player
        public int? KillerId { get; set; }
        public Player? Killer { get; set; }

        //Human Player
        public int? VictimId { get; set; }
        public Player? Victim { get; set; }


    }
}
