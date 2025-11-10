using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadConsultorioMedico;

namespace ClnConsultorioMedico
{
    public class EspecialidadCln
    {
        public static int insertar(Especialidad especialidad)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                context.Especialidad.Add(especialidad);
                context.SaveChanges();
                return especialidad.id;
            }
        }

        public static int actualizar(Especialidad especialidad)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                var existente = context.Especialidad.Find(especialidad.id);
                existente.nombre = especialidad.nombre;
                existente.usuarioRegistro = especialidad.usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuario)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                var especialidad = context.Especialidad.Find(id);
                especialidad.estado = -1;
                especialidad.usuarioRegistro = usuario;
                return context.SaveChanges();
            }
        }

        public static Especialidad obtenerUno(int id)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                return context.Especialidad.Find(id);
            }
        }

        public static List<paEspecialidadListar_Result> listarPa(string parametro)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                // Llamada al SP sin parámetros (modelo generado sin sobrecarga)
                var lista = context.paEspecialidadListar().ToList();

                if (string.IsNullOrWhiteSpace(parametro))
                {
                    return lista;
                }

                // Filtrado en memoria: búsqueda por nombre (case-insensitive)
                return lista
                    .Where(x => !string.IsNullOrEmpty(x.nombre) &&
                                x.nombre.IndexOf(parametro, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    .ToList();
            }
        }

        public static List<Especialidad> listar()
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                return context.Especialidad.Where(x => x.estado != -1).ToList();
            }
        }
    }
}