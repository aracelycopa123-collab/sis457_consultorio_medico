using System;

namespace CpConsultorioMedico
{
    public class Concepto
    {
        public int Id { get; set; }
        public int IdEspecialidad { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }

        public string UsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public short Estado { get; set; }
    }
}
