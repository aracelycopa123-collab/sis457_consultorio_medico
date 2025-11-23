using System;
using System.Collections.Generic;

namespace Sis457ConsultorioMedico.Models;

public partial class Especialidad
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public short Estado { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<Concepto> Conceptos { get; set; } = new List<Concepto>();

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
