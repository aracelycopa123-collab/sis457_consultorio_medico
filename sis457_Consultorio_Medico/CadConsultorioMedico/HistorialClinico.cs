namespace CadConsultorioMedico
{
    using System;
    using System.Collections.Generic;
    
    public partial class HistorialClinico
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public string diagnostico { get; set; }
        public string tratamiento { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<int> idPaciente { get; set; }
        public string usuarioRegistro { get; set; }
        public System.DateTime fechaRegistro { get; set; }
        public short estado { get; set; }
    }
}
