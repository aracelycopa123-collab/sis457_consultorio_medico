using System;

namespace CpConsultorioMedico
{
    public class Pago
    {
        public int Id { get; set; }
        public int IdCita { get; set; }
        public int IdConcepto { get; set; }
        public DateTime Fecha { get; set; }

        public string UsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public short Estado { get; set; }
    }
}

