using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using webservice2.Models;

namespace webservice2.RabbitMQ.Consumidor
{
    public class Consumidor : IConsumidor
    {
        public void RecibirMensaje()
        {
            try
            {
                //Debug
                var factory = new ConnectionFactory { HostName = "localhost" };

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {

                    channel.QueueDeclare(queue: "TRANSFERENCIA_ARCHIVO",
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    //consumer.Received += Consumer_Received;

                    consumer.Received += (sender, args) =>
                    {
                        var body = Encoding.UTF8.GetString(args.Body.ToArray());

                        DocumentoInfo documentoInfo = JsonSerializer.Deserialize<DocumentoInfo>(body);
                        Console.WriteLine("[API 2] DATA COMPLETA");
                        Console.WriteLine("NOMBRE DOCUMENTO: " + documentoInfo.Nombre);
                        Console.WriteLine("EXTENSION: " + documentoInfo.Extension);
                        //using var stream = File.Create("C:/Users/acer/Desktop/Archivos_Expediente");
                        //stream.Write(body, 0, body.Length);

                        //File.WriteAllBytes("C:/Users/acer/Desktop/Archivos_Expediente/Imagen_TEST.pdf", body);
                    };

                    channel.BasicConsume(queue: "TRANSFERENCIA_ARCHIVO", autoAck: true, consumer: consumer);
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine("ERROR ---> "+e.Message);
            }
        }

        //private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event) 
        //{
        //    var body = @event.Body.ToArray();
            
        //    //using var stream = File.Create("C:/Users/acer/Desktop/Archivos_Expediente");
        //    //stream.Write(body, 0, body.Length);

        //    File.WriteAllBytes("C:/Users/acer/Desktop/Archivos_Expediente/Imagen_TEST.PNG", body);

        //    Console.WriteLine("[API 2] DATA COMPLETA");

        //    await Task.Delay(250);
        //}
    }
}
