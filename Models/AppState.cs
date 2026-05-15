namespace Avatar_project.Models
{
    public class AppState
    {
        public bool IsAdmin { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 6;

        public List<MovieRating> MovieRatings { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
        public List<int> Favorites { get; set; } = new();
        public List<Character> Characters { get; set; } = new();
    }
}