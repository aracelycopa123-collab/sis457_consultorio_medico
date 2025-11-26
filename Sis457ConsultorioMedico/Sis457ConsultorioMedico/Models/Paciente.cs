using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sis457ConsultorioMedico.Models
{
    [Table("Paciente")]
    public partial class Paciente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Cédula de Identidad es obligatorio.")]
        public string CedulaIdentidad { get; set; } = null!;

        [Required(ErrorMessage = "El campo Nombres es obligatorio.")]
        public string Nombres { get; set; } = null!;

        public string? PrimerApellido { get; set; }

        public string? SegundoApellido { get; set; }

        // Ya no es obligatorio
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El campo Celular es obligatorio.")]
        public long Celular { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaNacimiento { get; set; }

        public string? UsuarioRegistro { get; set; }

        public DateTime FechaRegistro { get; set; }

        public short Estado { get; set; }

        public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
    }
}
