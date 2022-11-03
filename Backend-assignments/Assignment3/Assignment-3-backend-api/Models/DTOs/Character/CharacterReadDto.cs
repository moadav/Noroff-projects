namespace Assignment_3_backend_api.Models.DTOs.Character
{
    public class CharacterReadDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Alias { get; set; } = null!;
        public string Picture { get; set; } = null!;

        //navigation
        public List<int> Movies { get; set; } = null!;
    }
}
