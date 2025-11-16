namespace CadConsultorioMedico
{
    using System;
    
    public partial class paCitaListar_Result
    {
        public int id { get; set; }
        public string Doctor { get; set; }
        public string Paciente { get; set; }
        public string Especialidad { get; set; }
        public System.DateTime fecha { get; set; }
        public System.TimeSpan hora { get; set; }
    }
}
