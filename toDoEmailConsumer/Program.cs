using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Http.Json;
using System.Net;

/** RabbitMQ **/
string queueName = "emails";
ConnectionFactory factory = new()
{
  HostName = "localhost",
  UserName = "guest",
  Password = "guest"
};
IConnection connection = factory.CreateConnection();
IModel channel = connection.CreateModel();

channel.QueueDeclare(
  queue: queueName,
  durable: true,
  exclusive: false,
  autoDelete: false,
  arguments: null
);

Console.WriteLine("[*] Waiting for messages...");
HttpClient client = new();

EventingBasicConsumer consumer = new(channel);
consumer.Received += async (model, ea) =>
{
  var basicProperties = channel.CreateBasicProperties();
  basicProperties.Persistent = true;

  var body = ea.Body.ToArray();
  var message = Encoding.UTF8.GetString(body);
  Console.WriteLine("[x] Received {0}", message);

  EmailRequestDto? emailRequest = JsonSerializer.Deserialize<EmailRequestDto>(message);
  if(emailRequest == null) channel.BasicReject(ea.DeliveryTag, false);

  var content = JsonContent.Create(emailRequest);

  Console.WriteLine($"Recebi pedido de envio de email para: {emailRequest.TargetEmail}");

  //Enviar request HTTP para o serviço externo de email. 
  var response = await client.PostAsync("http://localhost:5122/mail", content);//URI do service externo de email.

  //Lidar com a resposta do serviço externo.
  if (response.StatusCode != HttpStatusCode.OK) channel.BasicReject(ea.DeliveryTag, true);
  else 
  {
    var notifyDbResponse = await client.PatchAsync($"http://localhost:5144/ToDo/notify/{emailRequest.ToDoId}", null);//URI da API de toDos. 
    Console.WriteLine($"Sending notification back to database: => Response => {notifyDbResponse}");

    channel.BasicAck(ea.DeliveryTag, false);
  }

  Console.WriteLine($"Email sent sucessfuly response => {response}");
};

channel.BasicConsume(
  queue: queueName,
  autoAck: false,
  consumer: consumer
);

Console.WriteLine("Press [enter] to exit");
Console.ReadLine();