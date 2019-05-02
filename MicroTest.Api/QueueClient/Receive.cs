using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MicroTest.Api.QueueClient
{
    public class Receive
    {
        public ConnectionFactory factory { get; set; }
        public Receive(string hostname)
        {
            factory = new ConnectionFactory() { HostName = hostname };
        }

        public string ReceiveNextMessage()
        {
            string result = "ERROR";
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        result = message;
                    };

                    channel.BasicConsume(
                            queue: "hello",
                            autoAck: true,
                            consumer: consumer
                    );
                    
                }
            }
            return result;
        }
    }
}