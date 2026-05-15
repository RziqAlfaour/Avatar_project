using Avatar_project.Models;

namespace Avatar_project.Services
{
    public class AvatarService : IAvatarService
    {
        private readonly AppState _state;

        public AvatarService()
        {
            _state = new AppState
            {
                IsAdmin = false,
                CurrentPage = 1,
                ItemsPerPage = 6,
                MovieRatings = new List<MovieRating>
                {
                    new MovieRating { Id = 1, User = "Lina", Rating = 5 },
                    new MovieRating { Id = 2, User = "Omar", Rating = 4 },
                    new MovieRating { Id = 3, User = "Sara", Rating = 5 }
                },
                Comments = new List<Comment>
                {
                    new Comment { Id = 1, User = "Ali", Text = "Amazing world building and stunning visuals." },
                    new Comment { Id = 2, User = "Noor", Text = "Neytiri is one of my favorite characters." }
                },
                Favorites = new List<int>(),
                Characters = new List<Character>
                {
                    new Character
                    {
                        Id = 1,
                        Name = "Jake Sully",
                        Role = "hero",
                        Image = "jake.jpg",
                        Description = "A former Marine who joins the Avatar Program and becomes central to Pandora's fate.",
                        Ratings = new List<int> { 5, 4, 5 }
                    },
                    new Character
                    {
                        Id = 2,
                        Name = "Neytiri",
                        Role = "hero",
                        Image = "neytiri.jpg",
                        Description = "A fierce Na'vi warrior who teaches Jake the culture and spirit of Pandora.",
                        Ratings = new List<int> { 5, 5, 4 }
                    }
                }
            };
        }

        public HomeViewModel GetHomeData()
        {
            return new HomeViewModel
            {
                Characters = _state.Characters,
                Comments = _state.Comments.OrderByDescending(x => x.Id).ToList(),
                MovieRatings = _state.MovieRatings,
                Favorites = _state.Favorites,
                IsAdmin = _state.IsAdmin
            };
        }

        public void AddMovieRating(string user, int rating)
        {
            if (string.IsNullOrWhiteSpace(user) || rating < 1 || rating > 5)
                return;

            _state.MovieRatings.Add(new MovieRating
            {
                Id = _state.MovieRatings.Any() ? _state.MovieRatings.Max(x => x.Id) + 1 : 1,
                User = user,
                Rating = rating
            });
        }

        public void AddComment(string user, string text)
        {
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(text))
                return;

            _state.Comments.Add(new Comment
            {
                Id = _state.Comments.Any() ? _state.Comments.Max(x => x.Id) + 1 : 1,
                User = user,
                Text = text
            });
        }

        public void DeleteComment(int id)
        {
            var comment = _state.Comments.FirstOrDefault(x => x.Id == id);
            if (comment != null)
                _state.Comments.Remove(comment);
        }

        public void RateCharacter(int characterId, int value)
        {
            var character = _state.Characters.FirstOrDefault(x => x.Id == characterId);
            if (character != null && value >= 1 && value <= 5)
                character.Ratings.Add(value);
        }

        public void ToggleFavorite(int characterId)
        {
            if (_state.Favorites.Contains(characterId))
                _state.Favorites.Remove(characterId);
            else
                _state.Favorites.Add(characterId);
        }

        public void ToggleAdmin(string password)
        {
            if (_state.IsAdmin)
                _state.IsAdmin = false;
            else if (password == "admin123")
                _state.IsAdmin = true;
        }

        public void SaveCharacter(Character model)
        {
            if (!_state.IsAdmin)
                return;

            if (model.Id == 0)
            {
                model.Id = _state.Characters.Any() ? _state.Characters.Max(x => x.Id) + 1 : 1;
                model.Ratings = new List<int>();
                _state.Characters.Add(model);
            }
            else
            {
                var existing = _state.Characters.FirstOrDefault(x => x.Id == model.Id);
                if (existing != null)
                {
                    existing.Name = model.Name;
                    existing.Role = model.Role;
                    existing.Description = model.Description;
                    existing.Image = model.Image;
                }
            }
        }

        public void DeleteCharacter(int id)
        {
            if (!_state.IsAdmin)
                return;

            var character = _state.Characters.FirstOrDefault(x => x.Id == id);
            if (character != null)
            {
                _state.Characters.Remove(character);
                _state.Favorites.Remove(id);
            }
        }
    }
}