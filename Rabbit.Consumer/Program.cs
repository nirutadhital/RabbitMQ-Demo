// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
//Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
var factory = new ConnectionFactory {
    HostName = "localhost",
    UserName = "guest",
    Password = "guest",
    VirtualHost = "/"
};
Console.WriteLine("testing");
//Create the RabbitMQ connection using connection factory details as i mentioned above
var connection = factory.CreateConnection();
//Here we create channel with session and model
using
var channel = connection.CreateModel();



Console.WriteLine("testing2");
//declare the queue after mentioning name and a few property related to that
channel.QueueDeclare("product", exclusive: false);



//Set Event object which listen message from chanel which is sent by producer
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) => {
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Product message received: {message}");
    
};
//read the message
channel.BasicConsume(queue: "product", autoAck: true, consumer: consumer);
Console.ReadKey();



