﻿using GreekShoping.MessageBus;
using GreekShoping.PaymentAPI.Messege;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GreekShoping.PaymentAPI.RabbitMQSender;

public class RabbitMQMessageSender : IRabbitMQMessageSender
{
    private readonly string _hostName;
    private readonly string _userName;
    private readonly string _password;
    private IConnection _connection;
    private const string ExchangeName = "FanoutPaymentUpdateExchange";

    public RabbitMQMessageSender()
    {//Here we have de access date of RabbiitMQ
        _hostName = "localhost";
        _userName = "guest";
        _password = "guest";
    }

    public void SendeMessage(BaseMessage message)
    {
        if (ConnectionExists())
        {
            using var channel = _connection.CreateModel();
            channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout, durable: false);
            byte[] body = GetMessageAsBytArray(message);
            channel.BasicPublish(
                exchange: ExchangeName, string.Empty, basicProperties: null, body: body);
        }
    }

    private byte[] GetMessageAsBytArray(BaseMessage message)
    {
        var opts = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        var json = JsonSerializer.Serialize<UpdatePaymentResultMessege>((UpdatePaymentResultMessege)message, opts);
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
