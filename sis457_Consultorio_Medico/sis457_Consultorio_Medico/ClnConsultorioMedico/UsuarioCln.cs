using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadConsultorioMedico;

namespace ClnConsultorioMedico
{
    public class UsuarioCln
    {
        public static int insertar(Usuario usuarioRegistro)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                context.Usuario.Add(usuarioRegistro);
                try
                {
                    context.SaveChanges();
                    return usuarioRegistro.id;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    var sb = new System.Text.StringBuilder();
                    sb.AppendLine("Errores de validación al guardar Usuario:");
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        sb.AppendLine($"Entidad: {eve.Entry.Entity.GetType().Name}");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            sb.AppendLine($" - Propiedad: {ve.PropertyName}, Error: {ve.ErrorMessage}");
                        }
                    }
                    // Lanzar excepción con detalle para que la UI o el log la capture.
                    throw new InvalidOperationException(sb.ToString(), ex);
                }
            }
        }
        public static int actualizar(Usuario usuarioRegistro)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                var existente = context.Usuario.Find(usuarioRegistro.id);
                existente.usuario = usuarioRegistro.usuario;
                existente.clave = usuarioRegistro.clave;
                existente.usuarioRegistro = usuarioRegistro.usuarioRegistro;
                return context.SaveChanges();
            }
        }
        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                var existente = context.Usuario.Find(id);
                existente.estado = -1;
                existente.usuarioRegistro = usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static Usuario obtenerUnoPorDoctor(int idDoctor)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                return context.Usuario.Where(x => x.idDoctor == idDoctor).FirstOrDefault();
            }
        }
        public static Usuario obtenerUno(int id)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                return context.Usuario.Find(id);
            }
        }
        public static Usuario validar(string usuario, string clave)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                return context.Usuario
            .Where(u => u.usuario == usuario && u.clave == clave && u.estado == 1)
            .FirstOrDefault();

            }
        }
    }
}

