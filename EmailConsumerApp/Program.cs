using ModelLayer;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RepositoryLayer.Utilities;
using System.Text;

namespace EmailConsumerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare("RegisterQueue", exclusive: false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) => {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Email received: {message}");
                var result = JsonConvert.DeserializeObject<EmailML>(message);
                EmailService.SendRegisterMail(result);
                
            };
            channel.BasicConsume(queue: "RegisterQueue", autoAck: true, consumer: consumer);
            Console.ReadKey();
        }
    }
}
