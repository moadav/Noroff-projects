
using System.Collections.ObjectModel;

namespace HvZ_API.Models
{
    public class Squad
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int MemberCount { get; set; } = 1;
        
        //Navigation
        public int GameId { get; set; }
        public Game Game { get; set; }


    }
}
