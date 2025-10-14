using System;

namespace CpConsultorioMedico
{
    public class Usuario
    {
        public int Id { get; set; }
        public int IdDoctor { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }

        public string UsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public short Estado { get; set; }
    }
}

