using firstproject.Config;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using firstproject.Models.DTOs;
using System.Text.Json;
using System.Text;

public class EmailService (IOptions<QueueConfig> queueConfig)
{
    private readonly string _hostName = queueConfig.Value.HostName;
    private readonly string _queue = queueConfig.Value.Queue;


    public void SendEmail(SendEmailDto dto){
        ConnectionFactory factory = new ()
        {
            HostName = _hostName
        };

        IConnection connection = factory.CreateConnection();
        using IModel channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: _queue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        string json = JsonSerializer.Serialize(dto);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(
            exchange: string.Empty,
            routingKey : _queue,
            basicProperties: null,
            body: body
        );
    }
}