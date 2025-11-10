namespace CadConsultorioMedico
{
    using System;
    
    public partial class paDoctorListar_Result
    {
        public int id { get; set; }
        public string nombreCompletoDoctor { get; set; }
        public string especialidad { get; set; }
        public string cedulaIdentidad { get; set; }
        public string direccion { get; set; }
        public long celular { get; set; }
    }
}
