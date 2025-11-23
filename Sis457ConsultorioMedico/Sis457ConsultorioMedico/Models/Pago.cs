using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sis457ConsultorioMedico.Models;
public partial class Pago
{
    public int Id { get; set; }

    public int IdCita { get; set; }
    [Required (ErrorMessage = "Debe seleccionar un Concepto de Pago.")]
    public int IdConcepto { get; set; }

    public DateOnly Fecha { get; set; }

    public string? UsuarioRegistro { get; set; }

    public DateTime FechaRegistro { get; set; }

    public short Estado { get; set; }

    public virtual Cita? IdCitaNavigation { get; set; }

    public virtual Concepto? IdConceptoNavigation { get; set; }

    [NotMapped]
    [Display(Name = "Monto Pagado")]
    [Range(0, double.MaxValue, ErrorMessage = "Ingrese un monto válido.")]
    public decimal Efectivo { get; set; }

    [NotMapped]
    public decimal Cambio => Efectivo - (IdConceptoNavigation?.Costo ?? 0);
}
