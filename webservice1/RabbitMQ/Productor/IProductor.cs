namespace webservice1.RabbitMQ
{
    public interface IProductor
    {
        public Task<bool> MandarMensaje(IFormFile Mensaje);
    }
}
