﻿namespace webservice1.Models.DTO
{
    public class DocumentoDTO
    {
        public int IdDocumento { get; set; }
        public string? Nombre { get; set; }
        public string? Extension { get; set; }
        public string? Ruta { get; set; }
        public int IdUsuario { get; set; }
        public int Peso { get; set; }
        public int IdExpediente { get; set; }
    }
}
