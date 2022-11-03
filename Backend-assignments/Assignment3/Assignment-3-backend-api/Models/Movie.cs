namespace Assignment_3_backend_api.Models
{
    public class Movie
    {
        public Movie()
        {
            Characters = new HashSet<Character>();
        }

        public int Id { get; set; }
        public string MovieTitle { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public string Director { get; set; } = null!;
        public string Picture { get; set; } = null!;
        public string Trailer { get; set; } = null!;

        public int? FranchiseId { get; set; }

        //navigation
        public virtual ICollection<Character>? Characters { get; set; }


        public virtual Franchise? Franchise { get; set; } = null!;



    }
}
