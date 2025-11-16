namespace CadConsultorioMedico
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pago
    {
        public int id { get; set; }
        public int idCita { get; set; }
        public int idConcepto { get; set; }
        public System.DateTime fecha { get; set; }
        public string usuarioRegistro { get; set; }
        public System.DateTime fechaRegistro { get; set; }
        public short estado { get; set; }
    
        public virtual Cita Cita { get; set; }
        public virtual Concepto Concepto { get; set; }
    }
}
