using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GuestBookApp.Models;
using GuestBookApp.Repositories;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using System.Linq;

public class IndexModel : PageModel
{
    private readonly IRepository<Message> _messageRepository;
    private readonly IRepository<User> _userRepository;

    public IndexModel(IRepository<Message> messageRepository, IRepository<User> userRepository)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
    }

    [BindProperty]
    public string NewMessage { get; set; } = string.Empty;

    public List<Message> Messages { get; set; } = new List<Message>();

    public async Task OnGetAsync()
    {
        Messages = await _messageRepository.GetAllAsync();
    }

    public async Task<IActionResult> OnPostAddMessageAsync()
    {
        if (HttpContext.Session.GetString("UserName") != null)
        {
            var user = await GetUserByNameAsync(HttpContext.Session.GetString("UserName"));
            if (user != null)
            {
                var message = new Message
                {
                    MessageText = NewMessage,
                    MessageDate = DateTime.Now,
                    User = user
                };

                await _messageRepository.AddAsync(message);
            }
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostLogoutAsync()
    {
        await HttpContext.SignOutAsync();
        HttpContext.Session.Clear();
        return RedirectToPage("/GuestBook/Login");
    }

    private async Task<User?> GetUserByNameAsync(string? name)
    {
        if (string.IsNullOrEmpty(name))
            return null;

        var users = await _userRepository.GetAllAsync();
        return users.FirstOrDefault(u => u.Name == name);
    }
}
