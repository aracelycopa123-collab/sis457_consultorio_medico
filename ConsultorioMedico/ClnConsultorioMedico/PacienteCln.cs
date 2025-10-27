using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CadConsultorioMedico;

namespace ClnConsultorioMedico
{
    public class PacienteCln
    {
        public static int insertar(Paciente paciente)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                context.Paciente.Add(paciente);
                context.SaveChanges();
                return paciente.id;
            }
        }

        public static int actualizar(Paciente paciente)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                var existente = context.Paciente.Find(paciente.id);
                existente.cedulaIdentidad = paciente.cedulaIdentidad;
                existente.nombreCompletoPaciente = paciente.nombreCompletoPaciente;
                existente.direccion = paciente.direccion;
                existente.celular = paciente.celular;
                existente.usuarioRegistro = paciente.usuarioRegistro;
                existente.estado = paciente.estado;
                return context.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                var existente = context.Paciente.Find(id);
                existente.estado = -1;
                existente.usuarioRegistro = usuarioRegistro;
                return context.SaveChanges();
            }
        }

        public static Paciente obtenerUno(int id)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                return context.Paciente.Find(id);
            }
        }

        public static List<paPacienteListar_Result> listarPa(string parametro)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                return context.paPacienteListar(parametro).ToList();
            }
        }
        public static Paciente buscar(string nombrePaciente)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                return context.Paciente.FirstOrDefault(x => x.nombreCompletoPaciente == nombrePaciente);
            }
        }
        public static Paciente buscarPorCedula(string cedulaIdentidad)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                return context.Paciente.AsNoTracking().FirstOrDefault(x => x.cedulaIdentidad == cedulaIdentidad);
            }
        }
        public static string obtenerNombrePaciente(string cedulaIdentidad)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                var paciente = context.Paciente.FirstOrDefault(x => x.cedulaIdentidad == cedulaIdentidad && x.estado != -1);
                return paciente != null ? paciente.nombreCompletoPaciente : null;
            }
        }
    }
}
