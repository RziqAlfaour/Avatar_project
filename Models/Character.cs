namespace Avatar_project.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Role { get; set; } = "";
        public string Image { get; set; } = "";
        public string Description { get; set; } = "";
        public List<int> Ratings { get; set; } = new();
    }
}