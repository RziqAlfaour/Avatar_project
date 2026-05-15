using Avatar_project.Models;

namespace Avatar_project.Services
{
    public interface IAvatarService
    {
        HomeViewModel GetHomeData();
        void AddMovieRating(string user, int rating);
        void AddComment(string user, string text);
        void DeleteComment(int id);
        void RateCharacter(int characterId, int value);
        void ToggleFavorite(int characterId);
        void ToggleAdmin(string password);
        void SaveCharacter(Character model);
        void DeleteCharacter(int id);
    }
}