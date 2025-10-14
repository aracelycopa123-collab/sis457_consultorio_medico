using CpConsultorioMedico; // referencia entidades
using Microsoft.EntityFrameworkCore;
using System;

namespace CadConsultorioMedico.Data
{
    public class ConsultorioContext : DbContext
    {
        public ConsultorioContext(DbContextOptions<ConsultorioContext> options) : base(options)
        {
        }

        // Db tablas
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Concepto> Conceptos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Doctor> Doctores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeo de tablas y claves primarias
            modelBuilder.Entity<Especialidad>().ToTable("Especialidad").HasKey(e => e.Id);
            modelBuilder.Entity<Concepto>().ToTable("Concepto").HasKey(c => c.Id);
            modelBuilder.Entity<Paciente>().ToTable("Paciente").HasKey(p => p.Id);
            modelBuilder.Entity<Doctor>().ToTable("Doctor").HasKey(d => d.Id);
            modelBuilder.Entity<Usuario>().ToTable("Usuario").HasKey(u => u.

