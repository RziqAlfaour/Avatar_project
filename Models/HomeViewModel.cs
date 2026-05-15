namespace Avatar_project.Models
{
    public class HomeViewModel
    {
        public List<Character> Characters { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
        public List<MovieRating> MovieRatings { get; set; } = new();
        public List<int> Favorites { get; set; } = new();
        public bool IsAdmin { get; set; }
    }
}