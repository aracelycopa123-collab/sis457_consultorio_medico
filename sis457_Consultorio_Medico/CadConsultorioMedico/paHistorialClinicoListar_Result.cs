namespace CadConsultorioMedico
{
    using System;
    
    public partial class paHistorialClinicoListar_Result
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public string diagnostico { get; set; }
        public string tratamiento { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string Paciente { get; set; }
    }
}
