namespace CadConsultorioMedico
{
    using System;
    
    public partial class paEspecialidadListar_Result
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string usuarioRegistro { get; set; }
        public System.DateTime fechaRegistro { get; set; }
        public short estado { get; set; }
    }
}
