using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;

namespace FreelanceDB.RabbitMQ
{
    public class RabbitMqService : IRabbitMqService
    {
        public void SendMessage(object obj)
        {
            var message = System.Text.Json.JsonSerializer.Serialize(obj);
            SendCreateUserMessage(message);
        }

        public async void SendCreateUserMessage(string message)
        {
            var factory = new ConnectionFactory() { Uri = new Uri("amqps://lzpoyxzx:zHSe2yBq-j1eaCjF8S6ztpMg0Y_D2xg_@dog.lmq.cloudamqp.com/lzpoyxzx") };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "CreateUserQueue", durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "CreateUserQueue", body: body);
            Console.WriteLine($" [x] Sent {message}");

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }

        public async void SendCreateTaskMessage(long authorId, int price)
        {
            var factory = new ConnectionFactory() { Uri = new Uri("amqps://lzpoyxzx:zHSe2yBq-j1eaCjF8S6ztpMg0Y_D2xg_@dog.lmq.cloudamqp.com/lzpoyxzx") };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "CreateTaskQueue", durable: false, exclusive: false, autoDelete: false,
                arguments: null);
            var message = new CreateTaskRabbitMqModel(authorId, (long)price);
            var body = JsonConvert.SerializeObject(message);
            var bytes = Encoding.UTF8.GetBytes(body);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "CreateTaskQueue", body: bytes);
            Console.WriteLine($" [x] Sent {message}");

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }

        

    }
}
