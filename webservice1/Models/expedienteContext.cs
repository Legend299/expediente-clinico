using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace webservice1.Models
{
    public partial class expedienteContext : DbContext
    {
        public expedienteContext()
        {
        }

        public expedienteContext(DbContextOptions<expedienteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Consulta> Consultas { get; set; } = null!;
        public virtual DbSet<ConsultaTipo> ConsultaTipos { get; set; } = null!;
        public virtual DbSet<Documento> Documentos { get; set; } = null!;
        public virtual DbSet<Especialidade> Especialidades { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<Expediente> Expedientes { get; set; } = null!;
        public virtual DbSet<Hospitale> Hospitales { get; set; } = null!;
        public virtual DbSet<Medico> Medicos { get; set; } = null!;
        public virtual DbSet<Municipio> Municipios { get; set; } = null!;
        public virtual DbSet<MunicipiosEstado> MunicipiosEstados { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseMySql("server=localhost;database=expediente;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.22-mariadb"));
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_spanish_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Consulta>(entity =>
            {
                entity.HasKey(e => e.IdConsulta)
                    .HasName("PRIMARY");

                entity.ToTable("consultas");

                entity.HasIndex(e => e.IdTipoConsulta, "FK_consultaTipos_consultas");

                entity.HasIndex(e => e.IdExpediente, "FK_expedientes_consultas");

                //entity.HasIndex(e => e.IdUsuario, "FK_usuarios_consultas");

                entity.Property(e => e.IdConsulta).HasColumnType("int(11)");

                entity.Property(e => e.Diagnostico).HasMaxLength(255);

                entity.Property(e => e.IdExpediente).HasColumnType("int(11)");

                entity.Property(e => e.IdTipoConsulta).HasColumnType("int(11)");

                entity.Property(e => e.Medico).HasMaxLength(50);

                entity.HasOne(d => d.IdExpedienteNavigation)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.IdExpediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_expedientes_consultas");

                entity.HasOne(d => d.IdTipoConsultaNavigation)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.IdTipoConsulta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_consultaTipos_consultas");

                /*entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_usuarios_consultas");*/
            });

            modelBuilder.Entity<ConsultaTipo>(entity =>
            {
                entity.HasKey(e => e.IdTipoConsulta)
                    .HasName("PRIMARY");

                entity.ToTable("consulta_tipos");

                entity.Property(e => e.IdTipoConsulta).HasColumnType("int(11)");

                entity.Property(e => e.Descripcion).HasMaxLength(200);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Documento>(entity =>
            {
                entity.HasKey(e => e.IdDocumento)
                    .HasName("PRIMARY");

                entity.ToTable("documentos");

                entity.HasIndex(e => e.IdExpediente, "FK_expedientes_documentos");

                entity.HasIndex(e => e.IdUsuario, "FK_usuarios_documentos");

                entity.Property(e => e.IdDocumento).HasColumnType("int(11)");

                entity.Property(e => e.Extension).HasMaxLength(50);

                entity.Property(e => e.IdExpediente).HasColumnType("int(11)");

                entity.Property(e => e.IdUsuario).HasColumnType("int(11)");

                entity.Property(e => e.Nombre).HasMaxLength(200);

                entity.Property(e => e.Peso).HasColumnType("int(11)");

                entity.Property(e => e.Ruta).HasMaxLength(200);

                entity.HasOne(d => d.IdExpedienteNavigation)
                    .WithMany(p => p.Documentos)
                    .HasForeignKey(d => d.IdExpediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_expedientes_documentos");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Documentos)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_usuarios_documentos");
            });

            modelBuilder.Entity<Especialidade>(entity =>
            {
                entity.HasKey(e => e.IdEspecialidad)
                    .HasName("PRIMARY");

                entity.ToTable("especialidades");

                entity.Property(e => e.IdEspecialidad).HasColumnType("int(11)");

                entity.Property(e => e.Descripcion).HasMaxLength(200);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PRIMARY");

                entity.ToTable("estados");

                entity.Property(e => e.IdEstado).HasColumnType("int(11)");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Expediente>(entity =>
            {
                entity.HasKey(e => e.IdExpediente)
                    .HasName("PRIMARY");

                entity.ToTable("expedientes");

                entity.HasIndex(e => e.IdEstado, "FK_estados_expediente");

                entity.HasIndex(e => e.IdMunicipio, "FK_municipios_expediente");

                entity.Property(e => e.IdExpediente).HasColumnType("int(11)");

                entity.Property(e => e.Apellido).HasMaxLength(50);

                entity.Property(e => e.Curp).HasMaxLength(18);

                entity.Property(e => e.IdEstado).HasColumnType("int(11)");

                entity.Property(e => e.IdMunicipio).HasColumnType("int(11)");

                entity.Property(e => e.Imagen).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Telefono).HasMaxLength(14);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Expedientes)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_estados_expediente");

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Expedientes)
                    .HasForeignKey(d => d.IdMunicipio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_municipios_expediente");
            });

            modelBuilder.Entity<Hospitale>(entity =>
            {
                entity.HasKey(e => e.IdHospital)
                    .HasName("PRIMARY");

                entity.ToTable("hospitales");

                entity.HasIndex(e => e.IdEstado, "FK_estados_hospitales");

                entity.HasIndex(e => e.IdMunicipio, "FK_municipios_hospitales");

                entity.Property(e => e.IdHospital).HasColumnType("int(11)");

                entity.Property(e => e.Correo).HasMaxLength(50);

                entity.Property(e => e.Direccion).HasMaxLength(200);

                entity.Property(e => e.IdEstado).HasColumnType("int(11)");

                entity.Property(e => e.IdMunicipio).HasColumnType("int(11)");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Telefono).HasMaxLength(20);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Hospitales)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_estados_hospitales");

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Hospitales)
                    .HasForeignKey(d => d.IdMunicipio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_municipios_hospitales");
            });

            modelBuilder.Entity<Medico>(entity =>
            {
                entity.HasKey(e => e.IdMedico)
                    .HasName("PRIMARY");

                entity.ToTable("medicos");

                entity.HasIndex(e => e.IdEspecialidad, "FK_especialidades_medicos");

                entity.HasIndex(e => e.IdHospital, "FK_hospitales_medicos");

                entity.HasIndex(e => e.IdUsuario, "FK_usuarios_medicos");

                entity.Property(e => e.IdMedico).HasColumnType("int(11)");

                entity.Property(e => e.Cedula).HasMaxLength(100);

                entity.Property(e => e.IdEspecialidad).HasColumnType("int(11)");

                entity.Property(e => e.IdHospital).HasColumnType("int(11)");

                entity.Property(e => e.IdUsuario).HasColumnType("int(11)");

                entity.HasOne(d => d.IdEspecialidadNavigation)
                    .WithMany(p => p.Medicos)
                    .HasForeignKey(d => d.IdEspecialidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_especialidades_medicos");

                entity.HasOne(d => d.IdHospitalNavigation)
                    .WithMany(p => p.Medicos)
                    .HasForeignKey(d => d.IdHospital)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_hospitales_medicos");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Medicos)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_usuarios_medicos");
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.HasKey(e => e.IdMunicipio)
                    .HasName("PRIMARY");

                entity.ToTable("municipios");

                entity.Property(e => e.IdMunicipio).HasColumnType("int(11)");

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<MunicipiosEstado>(entity =>
            {
                entity.ToTable("municipios_estados");

                entity.HasIndex(e => e.IdEstado, "FK_estados_estado");

                entity.HasIndex(e => e.IdMunicipio, "FK_municipios_municipio");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.IdEstado).HasColumnType("int(11)");

                entity.Property(e => e.IdMunicipio).HasColumnType("int(11)");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.MunicipiosEstados)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_estados_estado");

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.MunicipiosEstados)
                    .HasForeignKey(d => d.IdMunicipio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_municipios_municipio");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PRIMARY");

                entity.ToTable("roles");

                entity.Property(e => e.IdRol).HasColumnType("int(11)");

                entity.Property(e => e.Descripcion).HasMaxLength(200);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PRIMARY");

                entity.ToTable("usuarios");

                entity.HasIndex(e => e.IdExpediente, "FK_expedientes_expediente");

                entity.HasIndex(e => e.IdRol, "FK_roles_rol");

                entity.Property(e => e.IdUsuario).HasColumnType("int(11)");

                entity.Property(e => e.Correo).HasMaxLength(50);

                entity.Property(e => e.IdExpediente).HasColumnType("int(11)");

                entity.Property(e => e.IdRol).HasColumnType("int(11)");

                entity.Property(e => e.Password).HasMaxLength(200);

                entity.HasOne(d => d.IdExpedienteNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdExpediente)
                    .HasConstraintName("FK_expedientes_expediente");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_roles_rol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
