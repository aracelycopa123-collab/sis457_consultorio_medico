using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sis457ConsultorioMedico.Models
{
    [Table("Doctor")]
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

        // Relaciones existentes
        public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public virtual Especialidad? IdEspecialidadNavigation { get; set; }

        // Relación con DoctorHorario (correcta)
        public virtual ICollection<DoctorHorario> DoctorHorarios { get; set; } = new List<DoctorHorario>();
    }
}
