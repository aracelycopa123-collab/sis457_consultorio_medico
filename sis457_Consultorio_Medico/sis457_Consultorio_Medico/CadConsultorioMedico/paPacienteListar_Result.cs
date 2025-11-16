namespace CadConsultorioMedico
{
    using System;
    
    public partial class paPacienteListar_Result
    {
        public int id { get; set; }
        public string cedulaIdentidad { get; set; }
        public string nombreCompletoPaciente { get; set; }
        public string direccion { get; set; }
        public long celular { get; set; }
    }
}
