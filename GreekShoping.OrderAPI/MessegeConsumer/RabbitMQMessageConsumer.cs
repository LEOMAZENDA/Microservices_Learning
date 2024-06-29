
using GreekShoping.OrderAPI.Messeges;
using GreekShoping.OrderAPI.Models;
using GreekShoping.OrderAPI.Repository._OrderRepository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GreekShoping.OrderAPI.MessegeConsumer;

public class RabbitMQMessageConsumer : BackgroundService
{
    private readonly OrderRepository _repository;
    private IConnection _connection;
    private IModel _channel;

    public RabbitMQMessageConsumer(OrderRepository repository)
    {
        _repository = repository;
        var fecttory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };
        _connection = fecttory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "checkoutqueue", false, false, false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (chanel, evt) =>
        {
            var content = Encoding.UTF8.GetString(evt.Body.ToArray());
            CheckoutHeaderVO vO = JsonSerializer.Deserialize<CheckoutHeaderVO>(content);
            ProcessOder(vO).GetAwaiter().GetResult();
            _channel.BasicAck(evt.DeliveryTag, false);
        };
        _channel.BasicConsume("checkoutqueue", false, consumer);
        return Task.CompletedTask;
    }

    private async Task ProcessOder(CheckoutHeaderVO vO)
    {
        OrderHeader order = new ()
        {
            UserId = vO.UserId,
            FirstName = vO.FirstName,
            LastName = vO.LastName,
            OrderDetails = new List<OrderDetail>(),
            CardNumber = vO.CardNumber,
            CouponCode = vO.CouponCode,
            CVV = vO.CVV,
            DiscountAmount = vO.DiscountAmount,
            Email = vO.Email,
            ExpiryMonthYear = vO.ExpiryMothYear,
            Phone = vO.Phone,
            PaymentStatus = false,
            OrderTime = DateTime.Now
        };

        foreach (var details in vO.CartDetails)
        {
            OrderDetail model = new() {
                ProductId = details.ProductId,
                ProductName = details.Product.Name,
                Price = details.Product.Price,
                Count = details.count,
            };
            order.CartTotalItens += details.count;
            order.OrderDetails.Add(model);
        }
        await _repository.AddOrder(order);
    }
}
