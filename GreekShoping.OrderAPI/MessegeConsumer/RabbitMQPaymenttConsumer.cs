using GreekShoping.OrderAPI.Messege;
using GreekShoping.OrderAPI.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GreekShoping.OrderAPI.MessegeConsumer
{
    public class RabbitMQPaymenttConsumer : BackgroundService
    {
        private readonly OrderRepository _repository;
        private IConnection _connection;
        private IModel _channel;
        private const string ExchangeName = "DirectPaymentUpdateExchange";
        private const string PaymentOrderUpdateQueueName = "PaymentOrderUpdateQueueName";
        public RabbitMQPaymenttConsumer(OrderRepository repository)
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

            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);
            _channel.QueueDeclare(PaymentOrderUpdateQueueName, false, false, false, null);
            _channel.QueueBind(PaymentOrderUpdateQueueName, ExchangeName, "PaymentOrder");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                UpdatePaymentResultVO vO = JsonSerializer.Deserialize<UpdatePaymentResultVO>(content);
                UpdadePaymentStatus(vO).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume(PaymentOrderUpdateQueueName, false, consumer);
            return Task.CompletedTask;
        }

        private async Task UpdadePaymentStatus(UpdatePaymentResultVO vO)
        {            
            try
            {
                await _repository.UpdateOderPaymentStatus(vO.OrderId, vO.Status);
            }
            catch (Exception)
            {
                //Log
                throw;
            }
        }
    }
}
