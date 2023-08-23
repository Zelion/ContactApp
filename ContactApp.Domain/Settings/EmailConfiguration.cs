namespace ContactApp.Domain.Settings
{
    public class EmailConfiguration
    {
        public string From { get; set; } = default!;
        public string SmtpServer { get; set; } = default!;
        public int Port { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
