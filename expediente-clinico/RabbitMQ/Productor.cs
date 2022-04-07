using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expediente_clinico.RabbitMQ
{
    public class Productor : IProductorMensaje
    {
        public void EnviarMensaje<T>(T mensaje)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("COLA_ENVIAR_ARCHIVOS");

            var json = JsonConvert.SerializeObject(mensaje);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "COLA_ENVIAR_ARCHIVOS", body: body);
        }
    }
}
