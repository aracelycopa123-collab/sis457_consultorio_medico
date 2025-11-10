using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadConsultorioMedico;

namespace ClnConsultorioMedico
{
    public class PagoCln
    {
        public static int insertar(Pago pago)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                // Validar que el Concepto referenciado exista para evitar violación FK y dar mensaje claro.
                bool conceptoExiste = context.Concepto.Any(c => c.id == pago.idConcepto);
                if (!conceptoExiste)
                {
                    throw new InvalidOperationException($"El concepto con id {pago.idConcepto} no existe. Seleccione un concepto válido.");
                }

                context.Pago.Add(pago);
                context.SaveChanges();
                return pago.id;
            }
        }

        public static int actualizar(Pago pago)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                var existente = context.Pago.Find(pago.id);
                existente.idConcepto = pago.idConcepto;
                existente.fechaRegistro = pago.fechaRegistro;
                existente.usuarioRegistro = pago.usuarioRegistro;
                existente.fecha = pago.fecha;
                existente.estado = pago.estado;
                return context.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                var existente = context.Pago.Find(id);
                existente.estado = -1;
                existente.usuarioRegistro = usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static Pago obtenerUno(int id)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                return context.Pago.Find(id);
            }
        }

        public static List<paPagoListar_Result> listarPa(string parametro)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                // Contexto expone paPagoListar() sin parámetros en el modelo generado.
                return context.paPagoListar().ToList();
            }
        }

        public static Pago obtenerCosto(int idConcepto)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                return context.Pago.FirstOrDefault(x => x.idConcepto == idConcepto);
            }
        }
    }
}