namespace CadConsultorioMedico
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuario
    {
        public int id { get; set; }
        public int idDoctor { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public string usuarioRegistro { get; set; }
        public System.DateTime fechaRegistro { get; set; }
        public short estado { get; set; }
    
        public virtual Doctor Doctor { get; set; }
    }
}
