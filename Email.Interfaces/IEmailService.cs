namespace Infrastructure.Interfaces.Integrations;

public interface IEmailService
{
    Task SendAsync(string address, string title, string body);
}