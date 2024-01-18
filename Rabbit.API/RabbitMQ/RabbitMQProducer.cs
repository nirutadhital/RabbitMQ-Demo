using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Rabbit.API.RabbitMQ
{
    public class RabbitMQProducer :IRabbitMQProducer
    {
         public void SendProductMessageAsync < T > (T message) {
            //Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
            var factory = new ConnectionFactory {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };
           //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();
            //Here we create channel with session and model
            using
            var channel = connection.CreateModel();
            // var args = new Dictionary<string, object>();
            //args.Add("x-message-ttl", 60000);
            // channel.QueueDeclare("myqueue", false, false, false, args);





           


            //declare the queue after mentioning name and a few property related to that
            channel.QueueDeclare("product", exclusive: false);
            //Serialize the message
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);
            //put the data on to the product queue
            channel.BasicPublish(exchange: "", routingKey: "product", body: body); 


           

            
        }
    }
}