namespace GuestBookApp.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; } = string.Empty;  // Инициализация по умолчанию
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
