using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GuestBookApp.Models;
using GuestBookApp.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBookApp.Pages.GuestBook
{
    public class LoginModel : PageModel
    {
        private readonly IRepository<User> _userRepository;

        [BindProperty]
        public string LoginName { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string ErrorMessage { get; set; }

        public LoginModel(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = (await _userRepository.GetAllAsync()).SingleOrDefault(u => u.Name == LoginName && u.Pwd == Password);

            if (user == null)
            {
                ErrorMessage = "Invalid login attempt.";
                return Page();
            }

            HttpContext.Session.SetString("UserName", user.Name);
            return RedirectToPage("Index");
        }
    }
}
