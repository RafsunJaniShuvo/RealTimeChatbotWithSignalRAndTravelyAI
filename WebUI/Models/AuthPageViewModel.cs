namespace WebUI.Models
{
    public class AuthPageViewModel
    {
        public LoginViewModel Login { get; set; } = new();
        public RegisterViewModel Register { get; set; } = new();
    }
}
