using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadConsultorioMedico;

namespace ClnConsultorioMedico
{
    public class ConceptoCln
    {
        public static int insertar(Concepto concepto)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                context.Concepto.Add(concepto);
                context.SaveChanges();
                return concepto.id;
            }
        }
        public static Concepto obtenerUno(int id)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                return context.Concepto.Find(id);
            }
        }
        public static List<Concepto> listarPorEspecialidad(int idEspecialidad)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                if (idEspecialidad <= 0)
                {
                    // Si no hay especialidad válida, devolver conceptos activos para que el combobox no quede vacío.
                    return context.Concepto.Where(x => x.estado == 1).ToList();
                }
                return context.Concepto.Where(x => x.idEspecialidad == idEspecialidad && x.estado == 1).ToList();
            }
        }
    }
}
