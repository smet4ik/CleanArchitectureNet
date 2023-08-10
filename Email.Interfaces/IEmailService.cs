using System.Threading.Tasks;

namespace Email.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(string email, string subject, string body);
    }
}