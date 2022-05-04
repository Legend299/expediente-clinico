namespace webservice2.RabbitMQ.Consumidor
{
    public interface IConsumidor
    {
        public Task<bool> RecibirMensaje(string ruta);
    }
}
