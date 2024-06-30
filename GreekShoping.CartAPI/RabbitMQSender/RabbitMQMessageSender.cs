using GreekShoping.CartAPI.Message;
using GreekShoping.MessageBus;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GreekShoping.CartAPI.RabbitMQSender;

public class RabbitMQMessageSender : IRabbitMQMessageSender
{
    private readonly string _hostName;
    private readonly string _userName;
    private readonly string _password;
    private IConnection _connection;

    public RabbitMQMessageSender()
    {//Here we have de access date of RabbiitMQ
        _hostName = "localhost";
        _userName = "guest";
        _password = "guest";
    }

    public void SendeMessage(BaseMessage message, string queueName)
    {
        if (ConnectionExists())
        {
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);
            byte[] body = GetMessageAsBytArray(message);
            channel.BasicPublish(exchange: string.Empty, routingKey: queueName, basicProperties: null, body: body);
        }
    }

    private byte[] GetMessageAsBytArray(BaseMessage message)
    {
        var opts = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        var json = JsonSerializer.Serialize<CheckouHeaderVO>((CheckouHeaderVO)message, opts);
        var body = Encoding.UTF8.GetBytes(json);
        return body;
    }

    private void CreateConnecton()
    {
        try
        {
            var fecttory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password
            };
            _connection = fecttory.CreateConnection();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private bool ConnectionExists()
    {
        if (_connection != null) return true;
        CreateConnecton();

        return _connection != null;
    }
}
