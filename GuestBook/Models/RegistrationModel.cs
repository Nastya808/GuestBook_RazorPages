namespace GuestBookApp.Models
{
    public class RegistrationModel
    {
        public string LoginName { get; set; } = string.Empty;  // Инициализация по умолчанию
        public string Password { get; set; } = string.Empty;  // Инициализация по умолчанию
        public string ConfirmPassword { get; set; } = string.Empty;  // Инициализация по умолчанию
    }
}
