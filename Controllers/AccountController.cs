using Microsoft.AspNetCore.Mvc;
using Avatar_project.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http; // Required for Session

namespace Avatar_project.Controllers
{
    public class AccountController : Controller
    {
        // Static list of users for educational purposes (No Database used)
        // This acts as our temporary server-side storage.
        private static List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "admin", Password = "1234" }
        };

        // ==========================================
        // LOGIN logic
        // ==========================================

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(User model)
        {
            // Simple check: Find if any user matches the provided username and password
            var user = _users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

            if (user != null)
            {
                // Store Username in Session
                HttpContext.Session.SetString("Username", user.Username);

                // Login successful, redirect to Home page
                TempData["WelcomeMessage"] = $"Welcome back, {user.Username}!";
                return RedirectToAction("Index", "Home");
            }

            // Login failed, set an error message using ViewBag and return the same view
            ViewBag.ErrorMessage = "Invalid username or password. Please try again.";
            return View(model);
        }

        // ==========================================
        // REGISTER logic
        // ==========================================

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(User model)
        {
            // Check if username already exists
            bool userExists = _users.Any(u => u.Username == model.Username);

            if (userExists)
            {
                ViewBag.ErrorMessage = "Username already exists. Please choose a different one.";
                return View(model);
            }

            // Assign a new ID (just max id + 1)
            model.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
            
            // Add the new user to our static list
            _users.Add(model);

            // Pass a flag to the View to trigger LocalStorage saving via JS
            ViewBag.RegistrationSuccess = true;
            ViewBag.RegisteredUsername = model.Username;
            ViewBag.RegisteredPassword = model.Password;

            // We can return the view so JS runs, then JS will redirect to Login.
            // Or we just redirect to Login directly. Let's return the view to run JS.
            return View(model);
        }

        // ==========================================
        // LOGOUT logic
        // ==========================================

        // GET: /Account/Logout
        [HttpGet]
        public IActionResult Logout()
        {
            // Clear the session
            HttpContext.Session.Clear(); 
            
            // Redirect to Home/Index
            return RedirectToAction("Index", "Home");
        }
    }
}
