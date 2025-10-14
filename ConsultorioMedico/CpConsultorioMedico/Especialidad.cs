using System;

namespace CpConsultorioMedico
{
    public class Especialidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string UsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public short Estado { get; set; }
    }
}

