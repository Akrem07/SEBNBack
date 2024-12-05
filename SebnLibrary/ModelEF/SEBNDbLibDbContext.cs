using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SebnLibrary.ModelEF;

public partial class SEBNDbLibDbContext : DbContext
{
    public SEBNDbLibDbContext()
    {
    }

    public SEBNDbLibDbContext(DbContextOptions<SEBNDbLibDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<EmpEva> EmpEvas { get; set; }

    public virtual DbSet<ExcelDatum> ExcelData { get; set; }

    public virtual DbSet<FicheFonction> FicheFonctions { get; set; }

    public virtual DbSet<IntegrationPlan> IntegrationPlans { get; set; }

    public virtual DbSet<RespEva> RespEvas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LACREEM\\SQL2022;Initial Catalog=SEBNDbLibrary;User ID=sa;Password=25736;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.IdDep).HasName("PK__Departme__0E65B7A6B6B80651");

            entity.Property(e => e.IdDep).ValueGeneratedNever();
        });

        modelBuilder.Entity<EmpEva>(entity =>
        {
            entity.Property(e => e.IdEva).ValueGeneratedNever();

            entity.HasOne(d => d.MatNavigation).WithMany(p => p.EmpEvas)
                .HasPrincipalKey(p => p.Mat)
                .HasForeignKey(d => d.Mat)
                .HasConstraintName("FK_EmpEva_User");
        });

        modelBuilder.Entity<FicheFonction>(entity =>
        {
            entity.HasKey(e => e.IdFf).HasName("PK__FicheFon__B77388667354C07C");

            entity.HasOne(d => d.MrespNavigation).WithMany(p => p.FicheFonctions)
                .HasPrincipalKey(p => p.Mat)
                .HasForeignKey(d => d.Mresp)
                .HasConstraintName("FK_FicheFonction_User");
        });

        modelBuilder.Entity<IntegrationPlan>(entity =>
        {
            entity.HasKey(e => e.IdIp).HasName("PK__Integrat__B773E1DEFCB812BE");

            entity.Property(e => e.IdIp).ValueGeneratedNever();
        });

        modelBuilder.Entity<RespEva>(entity =>
        {
            entity.Property(e => e.IdReva).ValueGeneratedNever();

            entity.HasOne(d => d.MatNavigation).WithMany(p => p.RespEvas)
                .HasPrincipalKey(p => p.Mat)
                .HasForeignKey(d => d.Mat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RespEva_User");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdR).HasName("PK__Role__C4960014BD14A236");

            entity.Property(e => e.IdR).ValueGeneratedNever();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC075A5658E5");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdDepNavigation).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__IdDep__4222D4EF");

            entity.HasOne(d => d.IdFfNavigation).WithMany(p => p.Users).HasConstraintName("FK__User__IdFF__403A8C7D");

            entity.HasOne(d => d.IdIpNavigation).WithMany(p => p.Users).HasConstraintName("FK__User__IdIP__4316F928");

            entity.HasOne(d => d.IdRNavigation).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__IdR__412EB0B6");

            entity.HasOne(d => d.MatRespNavigation).WithMany(p => p.InverseMatRespNavigation)
                .HasPrincipalKey(p => p.Mat)
                .HasForeignKey(d => d.MatResp)
                .HasConstraintName("FK_User_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
