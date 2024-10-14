using HelpyWeb.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using HelpyWeb.ViewModels;
using HelpyWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HelpyWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly HelpyDbContext _context;
        private readonly SignInManager<User> signInManager;
        public AccountController(HelpyDbContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Authenticate the user with ASP.NET Identity
                var result = await signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false
                );

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Account is locked out. Please try again later.");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
            }
            return View(model);
        }
        public async Task GoogleLogin()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponce", "Account")
            });
        }

        public async Task<IActionResult> GoogleResponce()
        {
           var result = await  HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
           var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
           {
           claim.Issuer,
           claim.OriginalIssuer,
           claim.Type,
           claim.Value
           });

           return RedirectToAction("Index", "Home", new { area = "" });
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return View("Login");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Map RegisterViewModel to User entity
                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,  // Make sure to hash the password in a real application
                    CreatedDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    IsActive = true,
                    // Set any other required fields, use default values or nulls as appropriate
                    BirthDate = DateTime.UtcNow,  // Example, replace with actual value if needed
                    Gender = "NotSpecified",      // Example, replace with actual value if needed
                    Sexuality = "NotSpecified",   // Example, replace with actual value if needed
                    Ethnicity = "NotSpecified",   // Example, replace with actual value if needed
                    Occupation = "NotSpecified",  // Example, replace with actual value if needed
                    Description = "",             // Example, replace with actual value if needed
                    Address = "",                 // Example, replace with actual value if needed
                    Town = "",                    // Example, replace with actual value if needed
                    Country = "",                 // Example, replace with actual value if needed
                    Phone = "000-000-0000",       // Example, replace with actual value if needed
                    SubscriptionId = 0,           // Set default value or actual value if needed
                    ExpireDate = DateTime.UtcNow, // Example, replace with actual value if needed
                    PackageRenewalDate = DateTime.UtcNow, // Example, replace with actual value if needed
                    UserLastLogin = DateTime.UtcNow,      // Example, replace with actual value if needed
                };

                // Add the user to the database
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                // Redirect to Login page after successful registration
                return RedirectToAction("Login", "Account");
            }

            // If ModelState is not valid, return the view with the model to show validation errors
            return View(model);
        }
    }
}
