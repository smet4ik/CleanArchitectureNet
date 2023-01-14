using Infrastructure.Interfaces.Integrations;

namespace Email;

public class EmailService : IEmailService
{
    public Task SendAsync(string address, string title, string body)
    {
        return Task.CompletedTask;
    }
}