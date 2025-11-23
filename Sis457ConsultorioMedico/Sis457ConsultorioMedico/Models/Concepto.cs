using System;
using System.Collections.Generic;

namespace Sis457ConsultorioMedico.Models;

public partial class Concepto
{
    public int Id { get; set; }

    public int IdEspecialidad { get; set; }

    public string Descripcion { get; set; } = null!;

    public decimal Costo { get; set; }

  
    public string? UsuarioRegistro { get; set; }

    public DateTime FechaRegistro { get; set; }

    public short Estado { get; set; }


    public virtual Especialidad? IdEspecialidadNavigation { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
