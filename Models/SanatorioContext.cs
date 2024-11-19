using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sanatorio.Models;

public partial class SanatorioContext : DbContext
{
    public SanatorioContext()
    {
    }

    public SanatorioContext(DbContextOptions<SanatorioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cita> Citas { get; set; }

    public virtual DbSet<Facturacion> Facturacions { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            //=> optionsBuilder.UseSqlServer("server=localhost; database=Sanatorio; integrated security=true; TrustServerCertificate=True");

        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.IdCita).HasName("PK__Citas__A95AFC0753F6A728");

            entity.Property(e => e.IdCita).HasColumnName("Id_Cita");
            entity.Property(e => e.MedicosId).HasColumnName("Medicos_Id");
            entity.Property(e => e.PacientesId).HasColumnName("Pacientes_Id");
            entity.Property(e => e.Tratamiento).HasMaxLength(100);

            entity.HasOne(d => d.Medicos).WithMany(p => p.Cita)
                .HasForeignKey(d => d.MedicosId)
                .HasConstraintName("FK__Citas__Medicos_I__3C69FB99");

            entity.HasOne(d => d.Pacientes).WithMany(p => p.Cita)
                .HasForeignKey(d => d.PacientesId)
                .HasConstraintName("FK__Citas__Pacientes__3B75D760");
        });

        modelBuilder.Entity<Facturacion>(entity =>
        {
            entity.HasKey(e => e.IdFacturacion).HasName("PK__Facturac__818195A7BAC3FE60");

            entity.ToTable("Facturacion");

            entity.Property(e => e.IdFacturacion).HasColumnName("Id_Facturacion");
            entity.Property(e => e.CitasId).HasColumnName("Citas_Id");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Tratamiento).HasMaxLength(100);

            entity.HasOne(d => d.Citas).WithMany(p => p.Facturacions)
                .HasForeignKey(d => d.CitasId)
                .HasConstraintName("FK__Facturaci__Citas__3F466844");
        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.HasKey(e => e.IdMedico).HasName("PK__Medicos__7BA5D81062F22E20");

            entity.Property(e => e.IdMedico).HasColumnName("Id_Medico");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Dni).HasColumnName("DNI");
            entity.Property(e => e.Especialidad).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(15);
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PK__Paciente__032CD4A69572D6CC");

            entity.Property(e => e.IdPaciente).HasColumnName("Id_Paciente");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Dni).HasColumnName("DNI");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
