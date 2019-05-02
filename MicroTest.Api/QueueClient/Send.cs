using System;
using RabbitMQ.Client;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace MicroTest.Api.QueueClient
{
    public class Send
    {
        public ConnectionFactory factory { get; set;}
        public Send(string hostname)
        {
            factory = new ConnectionFactory() { HostName = hostname };
        }

        public void SendMessage(string qmessage)
        {
            using (var connection = factory.CreateConnection())
            {
                try 
                {
                    using (var channel = connection.CreateModel())
                    {
                        var body = Encoding.UTF8.GetBytes(qmessage);
                        channel.BasicPublish(exchange: "",
                            routingKey: "hello",
                            basicProperties: null,
                            mandatory: false,
                            body: body
                        );
                    }
                }
                catch(Exception ex)
                {

                }
            }
            return;
        }

        public void Dispose()
        {
            factory = null;
            return;
        }
    }
}