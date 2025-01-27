﻿using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;
using FreelanceDB.RabbitMQ.Contracts;

namespace FreelanceDB.RabbitMQ
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IConfiguration _config;
        public RabbitMqService(IConfiguration configuration)
        {
            _config = configuration;
        }
        public void SendMessage(object obj)
        {
            var message = System.Text.Json.JsonSerializer.Serialize(obj);
            SendCreateUserMessage(message);
        }

        public async void SendCreateUserMessage(string message)
        {
            var factory = new ConnectionFactory() { Uri = new Uri(_config["RabbitMQ: ConnectionString"]) };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "CreateUserQueue", durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "CreateUserQueue", body: body);
            Console.WriteLine($" [x] Sent {message}");

            Console.ReadLine();

        }

        public async void SendCreateTaskMessage(long authorId, int price)
        {
            var factory = new ConnectionFactory() { Uri = new Uri(_config["RabbitMQ: ConnectionString"]) };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "CreateTaskQueue", durable: false, exclusive: false, autoDelete: false,
                arguments: null);
            var message = new CreateTaskRabbitMqModel(authorId, (long)price);
            var body = JsonConvert.SerializeObject(message);
            var bytes = Encoding.UTF8.GetBytes(body);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "CreateTaskQueue", body: bytes);
            Console.WriteLine($" [x] Sent {message}");

            Console.ReadLine();

        }

        public async void SendFinishTaskMessage(long authorId, decimal price, long workerId)
        {
            var factory = new ConnectionFactory() { Uri = new Uri(_config["RabbitMQ: ConnectionString"]) };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "FinishTaskQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var message = new FinishTaskMessage(authorId, price, workerId);
            var body = JsonConvert.SerializeObject(message);
            var bytes = Encoding.UTF8.GetBytes(body);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "FinishTaskQueue", body: bytes);
            Console.WriteLine($" [x] Sent {message}");

            Console.ReadLine();

        }



    }
}
