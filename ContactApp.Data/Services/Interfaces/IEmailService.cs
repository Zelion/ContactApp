namespace ContactApp.Data.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string userEmail, string confirmationLink, string subject);
    }
}
