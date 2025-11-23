using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sis457ConsultorioMedico.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public int IdDoctor { get; set; }
    [Required(ErrorMessage = "El campo Usuario es obligatorio")]
    public string Usuario1 { get; set; } = null!;
    [Required(ErrorMessage = "El campo Clave es obligatorio")]
    public string Clave { get; set; } = null!;

    public string? UsuarioRegistro { get; set; }

    public DateTime FechaRegistro { get; set; }

    public short Estado { get; set; }

    public virtual Doctor? IdDoctorNavigation { get; set; }
}
