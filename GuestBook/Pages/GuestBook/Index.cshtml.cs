using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GuestBookApp.Models;
using GuestBookApp.Repositories;
using Microsoft.AspNetCore.Http;

namespace GuestBookApp.Pages.GuestBook
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Message> _messageRepository;
        private readonly IRepository<User> _userRepository;

        [BindProperty]
        public string NewMessage { get; set; } = string.Empty;

        public List<Message> Messages { get; set; } = new List<Message>();

        public IndexModel(IRepository<Message> messageRepository, IRepository<User> userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        public async Task OnGetAsync()
        {
            Messages = await _messageRepository.GetAllAsync();
        }

        public async Task<IActionResult> OnPostAddMessageAsync()
        {
            var userName = HttpContext.Session.GetString("UserName");

            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToPage("Login");
            }

            var user = await GetUserByNameAsync(userName);
            if (user == null)
            {
                return RedirectToPage("Login");
            }

            var message = new Message
            {
                MessageText = NewMessage,
                MessageDate = DateTime.Now,
                Id_User = user.Id,
                User = user
            };

            await _messageRepository.AddAsync(message);

            return RedirectToPage();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();

            return RedirectToPage("/Index"); 
        }

        private async Task<User?> GetUserByNameAsync(string userName)
        {
            return (await _userRepository.GetAllAsync()).SingleOrDefault(u => u.Name == userName);
        }
    }
}
