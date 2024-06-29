using GreekShoping.OrderAPI.Models;

namespace GreekShoping.OrderAPI.Repository._OrderRepository;

public interface IOrderRepository
{
    Task<bool> AddOrder(OrderHeader header);
    Task UpdateOderPaymentStatus(long orderHeaderId, bool paid);
}
