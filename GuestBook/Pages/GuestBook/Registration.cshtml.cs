using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GuestBookApp.Models;
using GuestBookApp.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBookApp.Pages.GuestBook
{
    public class RegistrationModel : PageModel
    {
        private readonly IRepository<User> _userRepository;

        [BindProperty]
        public string LoginName { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string ErrorMessage { get; set; }

        public RegistrationModel(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return Page();
            }

            var existingUser = (await _userRepository.GetAllAsync()).SingleOrDefault(u => u.Name == LoginName);
            if (existingUser != null)
            {
                ErrorMessage = "User already exists.";
                return Page();
            }

            var user = new User
            {
                Name = LoginName,
                Pwd = Password
            };
            await _userRepository.AddAsync(user);

            return RedirectToPage("Login");
        }
    }
}
