using System;

namespace CpConsultorioMedico
{
    public class Paciente
    {
        public int Id { get; set; }
        public string CedulaIdentidad { get; set; }
        public string NombreCompletoPaciente { get; set; }
        public string Direccion { get; set; }
        public long Celular { get; set; }

        public string UsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public short Estado { get; set; }
    }
}
