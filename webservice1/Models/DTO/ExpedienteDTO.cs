namespace webservice1.Models.DTO
{
    public class ExpedienteDTO
    {
        public int IdExpediente { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Imagen { get; set; }
        public string? Curp { get; set; }
        public EstadoDTO? Estado { get; set; }
        public MunicipioDTO? Municipio { get; set; }
        public bool Sexo { get; set; }
        public string? Telefono { get; set; }
        public DateOnly FechaDeNacimiento { get; set; }
        public List<ConsultaDTO>? Consulta { get; set; }
        public List<DocumentoDTO>? Documento { get; set; }
    }
}
