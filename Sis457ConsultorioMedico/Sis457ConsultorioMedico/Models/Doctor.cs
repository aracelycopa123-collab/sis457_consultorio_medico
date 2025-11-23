using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sis457ConsultorioMedico.Models;

public partial class Doctor
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Debe seleccionar una Especialidad")]
    public int IdEspecialidad { get; set; }
    [Required(ErrorMessage = "El campo Cédula de Identidad es obligatorio.")]
    public string CedulaIdentidad { get; set; } = null!;
    [Required(ErrorMessage = "El campo Nombres es obligatorio.")]
    public string Nombres { get; set; } = null!;
    public string? PrimerApellido { get; set; }
    public string? SegundoApellido { get; set; }
    [Required(ErrorMessage = "El campo Dirección es obligatorio.")]
    public string Direccion { get; set; } = null!;
    [Required(ErrorMessage = "El campo Celular es obligatorio.")]
    public long Celular { get; set; }
    public string? UsuarioRegistro { get; set; }
    public DateTime FechaRegistro { get; set; }
    public short Estado { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Especialidad? IdEspecialidadNavigation { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
