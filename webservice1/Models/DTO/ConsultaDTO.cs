namespace webservice1.Models.DTO
{
    public class ConsultaDTO
    {
        public int IdConsulta { get; set; }
        public DateOnly? Fecha { get; set; }
        public string Medico { get; set; }
        public int IdTipoConsulta { get; set; }
        public int IdExpediente { get; set; }
        public string? Diagnostico{ get; set; }
    }
}
