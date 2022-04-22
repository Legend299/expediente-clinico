using RabbitMQ.Client;
using System.Text;

namespace webservice1.RabbitMQ.Productor
{
    public class Productor : IProductor
    {
        public async void MandarMensaje(IFormFile Mensaje)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "TRANSFERENCIA_ARCHIVO", durable: false, exclusive: false, autoDelete: false, arguments: null);

            // Archivo a Bytes
            //await using var memoryStream = new MemoryStream();
            //await Mensaje.CopyToAsync(memoryStream);

            //var body = memoryStream.ToArray();

            // System.ObjectDisposedException: 'Cannot access a closed file.'
            var body = await GetBytes(Mensaje);

            channel.BasicPublish(exchange: "", routingKey: "TRANSFERENCIA_ARCHIVO", basicProperties: null, body: body);
            Console.WriteLine("MENSAJE ENVIADO:");
        }

        public async Task<byte[]> GetBytes(IFormFile archivo) 
        {
            await using var memoryStream = new MemoryStream();
            await archivo.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

    }
}
