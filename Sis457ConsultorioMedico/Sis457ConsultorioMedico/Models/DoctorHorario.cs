using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sis457ConsultorioMedico.Models
{
    [Table("DoctorHorario")]
    public class DoctorHorario
    {
        public int Id { get; set; }

        public int IdDoctor { get; set; }

        // En la DB la columna se llama "Dia"
        [Column("Dia")]
        public int DiaSemana { get; set; }

        // Mapear explícitamente a time(7) en la BD
        [Column("Hora", TypeName = "time(7)")]
        public TimeOnly Hora { get; set; }

        [Column("Estado")]
        public short Estado { get; set; } = 1;

        [ForeignKey(nameof(IdDoctor))]
        public virtual Doctor? Doctor { get; set; }
    }
}
