// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using Rabbit.API.Models;
// using Rabbit.API.Services;
// using RabbitMQ.Client;
// using RabbitMQ.Client.Events;

// namespace Rabbit.Consumer
// {
//     public class BackgroundServices :BackgroundService
//     {
//         private IServiceProvider _sp;
//         private ConnectionFactory _factory;
//         private IConnection _connection;
//         private IModel _channel;

//         // initialize the connection, channel and queue 
//         // inside the constructor to persist them 
//         // for until the service (or the application) runs
//         public BackgroundServices(IServiceProvider sp)
//         {
//             _sp = sp;
            
//             _factory = new ConnectionFactory() { HostName = "localhost" };
            
//             _connection = _factory.CreateConnection();
            
//             _channel = _connection.CreateModel();
            
//             _channel.QueueDeclare(
//                 queue: "product", 
//                 durable: false, 
//                 exclusive: false, 
//                 autoDelete: false, 
//                 arguments: null);
//         }

//         protected override Task ExecuteAsync(CancellationToken stoppingToken)
//         {
//             // when the service is stopping
//             // dispose these references
//             // to prevent leaks
//             if (stoppingToken.IsCancellationRequested)
//             {
//                 _channel.Dispose();
//                 _connection.Dispose();
//                 return Task.CompletedTask;
//             }

//             // create a consumer that listens on the channel (queue)
//             var consumer = new EventingBasicConsumer(_channel);

//             // handle the Received event on the consumer
//             // this is triggered whenever a new message
//             // is added to the queue by the producer
//             consumer.Received += (model, ea) =>
//             {
//                 // read the message bytes
//                 var body = ea.Body.ToArray();
                
//                 // convert back to the original string
//                 // {index}|SuperHero{10000+index}|Fly,Eat,Sleep,Manga|1|{DateTime.UtcNow.ToLongDateString()}|0|0
//                 // is received here
//                 var message = Encoding.UTF8.GetString(body);
                
//                 Console.WriteLine(" [x] Received {0}", message);
                

//                 Task.Run(() =>
//                 {
//                     // split the incoming message
//                     // into chunks which are inserted
//                     // into respective columns of the Heroes table
//                     var chunks = message.Split("|");

//                    var product = new Product();
//                     if (chunks.Length == 7)
//                     {
//                         product.ProductId = Convert.ToInt32(chunks[0]); // Assuming the first chunk is the ID
//                         product.ProductName = chunks[1];
//                         product.ProductDescription = chunks[2];
//                         product.ProductPrice = Convert.ToInt32(chunks[4]); // Assuming the fifth chunk is the price
//                         product.ProductStock = Convert.ToInt32(chunks[6]); // Assuming the seventh chunk is the stock
//                     }


//                     // BackgroundService is a Singleton service
//                     // IHeroesRepository is declared a Scoped service
//                     // by definition a Scoped service can't be consumed inside a Singleton
//                     // to solve this, we create a custom scope inside the Singleton and 
//                     // perform the insertion.
//                     using (var scope = _sp.CreateScope())
//                     {
//                         var db = scope.ServiceProvider.GetRequiredService<IProductServices>();
//                         db.AddProduct(product);
//                     }

//                 });
//             };

//             _channel.BasicConsume(queue: "product", autoAck: true, consumer: consumer);

//             return Task.CompletedTask;
//         }
//     }
// }