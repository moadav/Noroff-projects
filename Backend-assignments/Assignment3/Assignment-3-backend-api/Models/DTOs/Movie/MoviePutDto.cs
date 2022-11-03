namespace Assignment_3_backend_api.Models.DTOs.Movie
{
    public class MoviePutDto
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public string Director { get; set; } = null!;
        public string Picture { get; set; } = null!;
        public string Trailer { get; set; } = null!;



    }
}
