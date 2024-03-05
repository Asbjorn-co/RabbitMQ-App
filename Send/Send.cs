using System.Text;
using System.Text.Json;
using RabbitMQ.Client;


var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "delivery",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

var delivery = new Delivery
{
    pakkeID = 1,
    medlemsNavn = "John Doe",
    pickupAdresse = "123 Main St",
    afleveringsAdresse = "456 Elm St"
};

byte[] message = JsonSerializer.SerializeToUtf8Bytes(delivery);

    channel.BasicPublish(exchange: string.Empty,
                         routingKey: "delivery",
                         basicProperties: null,
                         body: message);
Console.WriteLine($" [x] Sent {delivery}");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();


public class Delivery
{
    public int pakkeID { get; set; }
    public string medlemsNavn { get; set; }
    public string pickupAdresse { get; set; }
    public string afleveringsAdresse { get; set; }
}