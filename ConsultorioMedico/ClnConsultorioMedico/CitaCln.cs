using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadConsultorioMedico;
using System.Data.Entity;

namespace ClnConsultorioMedico
{
    public class CitaCln
    {
        public static int insertar(Cita cita)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                context.Cita.Add(cita);
                context.SaveChanges();
                return cita.id;
            }
        }
        public static int actualizar(Cita cita)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                var existente = context.Cita.Find(cita.id);
                existente.idDoctor = cita.idDoctor;
                existente.idPaciente = cita.idPaciente;
                existente.idEspecialidad = cita.idEspecialidad;
                existente.fecha = cita.fecha;
                existente.hora = cita.hora;
                existente.usuarioRegistro = cita.usuarioRegistro;
                return context.SaveChanges();
            }
        }
        public static int eliminar(int id, string usuario)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                var existente = context.Cita.Find(id);
                existente.estado = -1;
                existente.usuarioRegistro = usuario;
                return context.SaveChanges();
            }
        }
        public static Cita obtenerUno(int id)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                return context.Cita.Find(id);
            }
        }
        public static List<paCitaListar_Result> listarPa(string parametro)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                return context.paCitaListar(parametro).ToList();
            }
        }
        public static List<paCitaporFechaListar_Result> listarFecha(DateTime parametroFecha)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                return context.paCitaporFechaListar(parametroFecha).ToList();
            }
        }
        public static Cita obtenerIdCita(string Paciente)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                return context.Cita.Include(x => x.Paciente).FirstOrDefault(x => x.Paciente.nombreCompletoPaciente == Paciente);
            }
        }
        public static bool existeCita(int idPaciente, int idEspecialidad, DateTime fecha)
        {
            using (var context = new LabConsultorioMedicoEntities())
            {
                return context.Cita.Any(c =>
                    c.idPaciente == idPaciente &&
                    c.idEspecialidad == idEspecialidad &&
                    DbFunctions.TruncateTime(c.fecha) == fecha.Date &&
                    c.estado != -1); // opcional si manejas citas anuladas con estado -1
            }
        }
    }
}
