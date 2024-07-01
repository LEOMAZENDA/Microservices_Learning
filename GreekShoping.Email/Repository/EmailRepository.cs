using GreekShoping.Email.Messege;
using GreekShoping.Email.Context;
using Microsoft.EntityFrameworkCore;
using GreekShoping.Email.Models;

namespace GreekShoping.Email.Repository;

public class EmailRepository : IEmailRepository
{
    private readonly DbContextOptions<MySqlContext> _context;

    public EmailRepository(DbContextOptions<MySqlContext> context)
    {
        _context = context;
    }

    public async Task LogEmail(UpdatePaymentResultMessage message)
    {
        EmailLog emailLog = new EmailLog()
        {
            Email = message.Email,
            SentDate = DateTime.Now,
            Log = $"Order - {message.OrderId} has been created successfully!"
        };
        await using var _db = new MySqlContext(_context);
       _db.Emails.Add(emailLog);
        await _db.SaveChangesAsync();
    }
}
