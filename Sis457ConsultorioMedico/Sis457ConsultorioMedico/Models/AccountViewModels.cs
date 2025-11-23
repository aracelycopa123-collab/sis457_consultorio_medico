using System.ComponentModel.DataAnnotations;

namespace Sis457ConsultorioMedico.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Debe introducir un Usuario")]
        [Display(Name = "Usuario")]
        public string usuario { get; set; }
        [Required(ErrorMessage = "Debe introducir una Contraseña")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string clave { get; set; }
        [Display(Name = "Recordarme")]
        public bool recordarme { get; set; }
    }
}
