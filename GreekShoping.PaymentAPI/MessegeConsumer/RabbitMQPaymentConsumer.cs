using GreekShoping.PaymentAPI.Messege;
using GreekShoping.PaymentAPI.RabbitMQSender;
using GreekShoping_PaymentProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GreekShoping.PaymentAPI.MessegeConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private IRabbitMQMessageSender _rabbitMQMessageSender;
        private readonly IProcessPayment _processPayment;

        public RabbitMQPaymentConsumer(IProcessPayment processPayment,
            IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _processPayment = processPayment;
            _rabbitMQMessageSender = rabbitMQMessageSender;
            var fecttory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = fecttory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "orderpaymentprocessqueue", false, false, false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                PaymentMessege vO = JsonSerializer.Deserialize<PaymentMessege>(content);
                ProcessPayment(vO).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume("orderpaymentprocessqueue", false, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessPayment(PaymentMessege vO)
        {
            var result = _processPayment.PaymentProcessor();
            UpdatePaymentResultMessege paymentResult = new ()
            {
                Status = result,
                OrderId = vO.OrderId,
                Email = vO.Email
            };
            try
            {
                _rabbitMQMessageSender.SendeMessage(paymentResult);
            }
            catch (Exception)
            {
                //Log
                throw;
            }
        }
    }
}
