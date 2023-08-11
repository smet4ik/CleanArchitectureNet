
using System;
using System.Threading.Tasks;
using Email.Interfaces;

namespace Email.Implementation
{
    public class EmailService : IEmailService
    {
        
        public  Task SendAsync(string email, string subject, string body)
        {
            Console.WriteLine($"to {email} - {subject}");
            return Task.CompletedTask;
        }
    }
}