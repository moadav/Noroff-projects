namespace Assignment_3_backend_api.Models
{
    public class Franchise
    {
        public Franchise()
        {
            Movies = new HashSet<Movie>();

        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        //navigation
        public virtual ICollection<Movie> Movies { get; set; }

    }
}
