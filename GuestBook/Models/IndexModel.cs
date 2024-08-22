using System.Collections.Generic;

namespace GuestBookApp.Models
{
    public class IndexModel
    {
        public List<Message> Messages { get; set; } = new List<Message>();
        public string NewMessage { get; set; } = string.Empty;
    }
}
