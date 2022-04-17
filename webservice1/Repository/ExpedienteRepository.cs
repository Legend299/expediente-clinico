using webservice1.Models.DTO;

namespace webservice1.Repository
{
    public class ExpedienteRepository : IExpedienteRepository
    {
        private readonly expedienteContext _context;
        public ExpedienteRepository(expedienteContext context)
        {
            _context = context;
        }

        public async Task<ExpedienteDTO?> ListarExpedienteUsuario(int id)
        {
            var expediente = await _context.Expedientes
                .Where(idu => idu.IdExpediente == id)
                .Select(x => new ExpedienteDTO
                {
                    IdExpediente = x.IdExpediente,
                    Nombre = x.Nombre,
                    Apellido = x.Apellido,
                    Imagen = x.Imagen,
                    Curp = x.Curp,

                    Estado = _context.Estados.Where(ide => ide.IdEstado == x.IdEstado).Select(es => new EstadoDTO
                    {
                        IdEstado = es.IdEstado,
                        Nombre = es.Nombre
                    }).FirstOrDefault(),

                    Municipio = _context.Municipios.Where(idm => idm.IdMunicipio == x.IdMunicipio).Select(em => new MunicipioDTO
                    {
                        IdMunicipio = em.IdMunicipio,
                        Nombre = em.Nombre
                    }).FirstOrDefault(),

                    Sexo = x.Sexo,
                    Telefono = x.Telefono,
                    FechaDeNacimiento = x.FechaDeNacimiento,

                    Consulta = x.Consulta.Select(c => new ConsultaDTO
                    {
                        IdConsulta = c.IdConsulta,
                        Fecha = c.Fecha,
                        Medico = c.Medico,
                        IdTipoConsulta = c.IdTipoConsulta,
                        Diagnostico = c.Diagnostico,
                        IdExpediente = c.IdExpediente
                    }).ToList(),

                    Documento = x.Documentos.Select(d => new DocumentoDTO {
                        IdDocumento = d.IdDocumento,
                        Nombre = d.Nombre,
                        Extension = d.Extension,
                        Ruta = d.Ruta,
                        IdUsuario = d.IdUsuario,
                        Peso = d.Peso,
                        IdExpediente = d.IdExpediente
                    }).ToList()
                }).FirstOrDefaultAsync();

            return expediente;

            //Retorna expediente normal
            //return await _context.Expedientes.Where(c =>
            //    c.IdExpediente == id)
            //        .FirstOrDefaultAsync();
        }
        public async Task<List<Expediente>?> ListarExpediente(int id) 
        {
            var expediente = await _context.Expedientes
                .Where(ide => ide.IdExpediente == id).ToListAsync();

            return expediente;
        }

        public bool AgregarExpediente(Expediente expediente)
        {
            try 
            {
                _context.Expedientes.Add(expediente);
                _context.SaveChanges();
                return true;
            } catch (Exception ex) 
            {
                Console.WriteLine("No se ha podido agregar el expediente: "+ex.Message);
                return false;
            }
        }

        public Expediente ModificarExpediente(Expediente expediente)
        {
            try
            {
                _context.Expedientes.Update(expediente);
                _context.SaveChanges();

                return expediente;
            } catch (Exception ex) 
            {
                Console.WriteLine("No se ha podido modificar el expediente: "+ex.Message);
                return null;  
            }
        }
    }
}
