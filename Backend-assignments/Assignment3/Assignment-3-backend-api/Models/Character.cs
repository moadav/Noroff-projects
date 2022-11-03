namespace Assignment_3_backend_api.Models
{
    public class Character
    {
        public Character()
        {
            Movies = new HashSet<Movie>();

        }

        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Alias { get; set; } = null!;
        public string Picture { get; set; } = null!;

        //navigation
        public virtual ICollection<Movie> Movies { get; set; }

    }
}
