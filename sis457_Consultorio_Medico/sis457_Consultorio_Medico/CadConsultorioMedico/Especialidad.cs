namespace CadConsultorioMedico
{
    using System;
    using System.Collections.Generic;
    
    public partial class Especialidad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Especialidad()
        {
            this.Cita = new HashSet<Cita>();
            this.Concepto = new HashSet<Concepto>();
            this.Doctor = new HashSet<Doctor>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public string usuarioRegistro { get; set; }
        public System.DateTime fechaRegistro { get; set; }
        public short estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cita> Cita { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Concepto> Concepto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Doctor> Doctor { get; set; }
    }
}
