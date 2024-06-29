
using GreekShoping.OrderAPI.Messeges;
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
        throw new NotImplementedException();
    }
}
