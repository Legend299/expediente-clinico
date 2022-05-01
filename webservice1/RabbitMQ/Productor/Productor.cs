using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using webservice2.Models;

namespace webservice1.RabbitMQ.Productor
{
    public class Productor : IProductor
    {
        public async Task<bool> MandarMensaje(DocumentoInfo Mensaje)
        {
            //int peso = 0;
            try
            {
                //Debug
                var factory = new ConnectionFactory { HostName = "localhost" };

                using (var connection = factory.CreateConnection())

                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "TRANSFERENCIA_ARCHIVO", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    // Archivo a Bytes
                    //await using var memoryStream = new MemoryStream();
                    //await Mensaje.CopyToAsync(memoryStream);

                    //var body = memoryStream.ToArray();

                    // System.ObjectDisposedException: 'Cannot access a closed file.'
                    //var body = await GetBytes(Mensaje);
                    string json = JsonConvert.SerializeObject(Mensaje);
                    var body = Encoding.UTF8.GetBytes(json);

                    //peso += body.Length;
                    channel.BasicPublish(exchange: "", routingKey: "TRANSFERENCIA_ARCHIVO", basicProperties: null, body: body);
                    //if(peso >= Mensaje.Length) {
                    Console.WriteLine("MENSAJE ENVIADO:");
                    return true;
                    //}
                    //return false;
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine("ERROR --> "+e.Message);
                Console.WriteLine("TRACE: "+e.StackTrace);
                return false;
            }
        }

        //public async Task<byte[]> GetBytes(IFormFile archivo)
        //{
        //    await using var memoryStream = new MemoryStream();
        //    await archivo.CopyToAsync(memoryStream);
        //    return memoryStream.ToArray();
        //}

    }
}
