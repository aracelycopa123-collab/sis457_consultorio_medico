namespace CadConsultorioMedico
{
    using System;
    
    public partial class paPagoListar_Result
    {
        public int id { get; set; }
        public int CitaID { get; set; }
        public string Concepto { get; set; }
        public System.DateTime FechaPago { get; set; }
    }
}
