using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expediente_clinico.RabbitMQ
{
    interface IProductorMensaje
    {
        void EnviarMensaje<T>(T mensaje);
    }
}
