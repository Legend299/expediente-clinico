using webservice2.Models;

namespace webservice1.RabbitMQ
{
    public interface IProductor
    {
        public Task<bool> MandarMensaje(DocumentoInfo Mensaje);
    }
}
