using Avatar_project.Models;
using Avatar_project.Services;
using Microsoft.AspNetCore.Mvc;

namespace Avatar_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAvatarService _avatarService;

        public HomeController(IAvatarService avatarService)
        {
            _avatarService = avatarService;
        }

        public IActionResult Index()
        {
            var vm = _avatarService.GetHomeData();
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddMovieRating(string user, int rating)
        {
            _avatarService.AddMovieRating(user, rating);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddComment(string user, string text)
        {
            _avatarService.AddComment(user, text);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteComment(int id)
        {
            _avatarService.DeleteComment(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RateCharacter(int characterId, int value)
        {
            _avatarService.RateCharacter(characterId, value);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ToggleFavorite(int characterId)
        {
            _avatarService.ToggleFavorite(characterId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ToggleAdmin(string password)
        {
            _avatarService.ToggleAdmin(password);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SaveCharacter(Character model)
        {
            _avatarService.SaveCharacter(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteCharacter(int id)
        {
            _avatarService.DeleteCharacter(id);
            return RedirectToAction("Index");
        }
    }
}