namespace HvZ_API.Models
{
    public class Mission
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool Is_Human_Visible { get; set; }
        public bool Is_Zombie_Visible { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set;  }

        public double Lat { get; set; }
        public double Lng { get; set; }

        //Navigation
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
