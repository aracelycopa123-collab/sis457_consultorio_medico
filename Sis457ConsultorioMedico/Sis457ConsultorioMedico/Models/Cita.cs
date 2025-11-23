using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sis457ConsultorioMedico.Models;

public partial class Cita
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Debe seleccionar un Doctor")]
    public int IdDoctor { get; set; }
    [Required(ErrorMessage = "El campo Nombre del Paciente es obligatorio.")]
    public int IdPaciente { get; set; }
    [Required(ErrorMessage = "Debe seleccionar una Especialidad")]
    public int IdEspecialidad { get; set; }
    [Required(ErrorMessage = "Debe seleccionar una Fecha")]
    public DateOnly Fecha { get; set; }
    [Required(ErrorMessage = "Debe seleccionar una Hora")]
    public TimeOnly Hora { get; set; }

    public string? UsuarioRegistro { get; set; }

    public DateTime FechaRegistro { get; set; }

    public short Estado { get; set; }

    public virtual Doctor? IdDoctorNavigation { get; set; }

    public virtual Especialidad? IdEspecialidadNavigation { get; set; }

    public virtual Paciente? IdPacienteNavigation { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
