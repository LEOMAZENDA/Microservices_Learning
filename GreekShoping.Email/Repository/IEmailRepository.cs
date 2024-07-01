using GreekShoping.Email.Messege;

namespace GreekShoping.Email.Repository;

public interface IEmailRepository
{
    Task LogEmail(UpdatePaymentResultMessage message);
}
