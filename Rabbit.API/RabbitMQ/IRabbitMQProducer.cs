using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rabbit.API.RabbitMQ
{
    public interface IRabbitMQProducer
    {
        public void SendProductMessageAsync < T > (T message);
    }
}