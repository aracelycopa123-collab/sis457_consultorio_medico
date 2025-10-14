using System;

namespace CpConsultorioMedico
{
    public class Cita
    {
        public int Id { get; set; }
        public int IdDoctor { get; set; }
        public int IdPaciente { get; set; }
        public int IdEspecialidad { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }

        public string UsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public short Estado { get; set; }
    }
}

