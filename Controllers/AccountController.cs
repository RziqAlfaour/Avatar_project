using Microsoft.AspNetCore.Mvc;
using Avatar_project.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Avatar_project.Controllers
{
    public class AccountController : Controller
    {
        // Temporary server-side storage for the session
        private static List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "admin", Password = "1234" }
        };

        // ==========================================
        // LOGIN
        // ==========================================

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                return RedirectToAction("Index", "Home");
            }

            // Authentication failed
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(model);
        }

        // ==========================================
        // REGISTER
        // ==========================================

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check if user already exists
            if (_users.Any(u => u.Username.ToLower() == model.Username.ToLower()))
            {
                ModelState.AddModelError("Username", "This username is already taken. Please choose another.");
                return View(model);
            }

            // Create new User entity
            var newUser = new User
            {
                Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1,
                Username = model.Username,
                Password = model.Password
            };

            _users.Add(newUser);

            // Pass success flag to view to trigger localStorage save
            ViewBag.RegistrationSuccess = true;
            ViewBag.RegisteredUsername = newUser.Username;
            ViewBag.RegisteredPassword = newUser.Password;

            TempData["RegisterSuccess"] = "Account created successfully! Please login.";
            
            // We return View so the JS can run and then redirect, or we could redirect directly.
            // Returning the View allows the JS script to execute.
            return View(model);
        }

        // ==========================================
        // LOGOUT
        // ==========================================

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Index", "Home");
        }
    }
}
