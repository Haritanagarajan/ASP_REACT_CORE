using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VehicleManagement.Models;

public partial class VehicleManagementContext : DbContext
{
    public VehicleManagementContext()
    {
    }

    public VehicleManagementContext(DbContextOptions<VehicleManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BrandCar> BrandCars { get; set; }

    public virtual DbSet<CarBrand> CarBrands { get; set; }

    public virtual DbSet<CarDetail> CarDetails { get; set; }

    public virtual DbSet<CarFuel> CarFuels { get; set; }

    public virtual DbSet<CarService> CarServices { get; set; }

    public virtual DbSet<Vrole> Vroles { get; set; }

    public virtual DbSet<Vuser> Vusers { get; set; }

    public DbSet<ValidateUserscs> ValidateUserscs { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-54BLE96\\SQLEXPRESS2019;Database=VehicleManagement;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BrandCar>(entity =>
        {
            entity.HasKey(e => e.Carid).HasName("PK__BrandCar__68A1300627D5A34C");

            entity.Property(e => e.AddAmount).HasColumnType("smallmoney");
            entity.Property(e => e.CarName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Brand).WithMany(p => p.BrandCars)
                .HasForeignKey(d => d.Brandid)
                .HasConstraintName("FK__BrandCars__Brand__3F466844");
        });

        modelBuilder.Entity<CarBrand>(entity =>
        {
            entity.HasKey(e => e.Brandid).HasName("PK__CarBrand__DADBF476D0E258F9");

            entity.ToTable("CarBrand");

            entity.Property(e => e.BrandName)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CarDetail>(entity =>
        {
            entity.HasKey(e => e.DetailsId).HasName("PK__CarDetai__BAC8628CCAB405E5");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.Vuserid).HasColumnName("VUserid");

            entity.HasOne(d => d.Brand).WithMany(p => p.CarDetails)
                .HasForeignKey(d => d.Brandid)
                .HasConstraintName("FK__CarDetail__Brand__47DBAE45");

            entity.HasOne(d => d.Car).WithMany(p => p.CarDetails)
                .HasForeignKey(d => d.Carid)
                .HasConstraintName("FK__CarDetail__Carid__48CFD27E");

            entity.HasOne(d => d.Fuel).WithMany(p => p.CarDetails)
                .HasForeignKey(d => d.Fuelid)
                .HasConstraintName("FK__CarDetail__Fueli__49C3F6B7");

            entity.HasOne(d => d.Service).WithMany(p => p.CarDetails)
                .HasForeignKey(d => d.Serviceid)
                .HasConstraintName("FK__CarDetail__Servi__4AB81AF0");

            entity.HasOne(d => d.Vuser).WithMany(p => p.CarDetails)
                .HasForeignKey(d => d.Vuserid)
                .HasConstraintName("FK__CarDetail__VUser__46E78A0C");
        });

        modelBuilder.Entity<CarFuel>(entity =>
        {
            entity.HasKey(e => e.Fuelid).HasName("PK__CarFuel__706BF78FA04BCE09");

            entity.ToTable("CarFuel");

            entity.Property(e => e.FuelName)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CarService>(entity =>
        {
            entity.HasKey(e => e.Serviceid).HasName("PK__CarServi__C514B422624DBA10");

            entity.Property(e => e.ServiceName).IsUnicode(false);
            entity.Property(e => e.Servicecost).HasColumnType("smallmoney");
            entity.Property(e => e.Subservice1).IsUnicode(false);
            entity.Property(e => e.Subservice2).IsUnicode(false);
            entity.Property(e => e.Subservice3).IsUnicode(false);
            entity.Property(e => e.Subservice4).IsUnicode(false);
            entity.Property(e => e.Warranty).IsUnicode(false);

            entity.HasOne(d => d.Car).WithMany(p => p.CarServices)
                .HasForeignKey(d => d.Carid)
                .HasConstraintName("FK__CarServic__Carid__440B1D61");
        });

        modelBuilder.Entity<Vrole>(entity =>
        {
            entity.HasKey(e => e.Vroleid).HasName("PK__VRoles__73AC3840525CE5FD");

            entity.ToTable("VRoles");

            entity.Property(e => e.Vroleid)
                .ValueGeneratedNever()
                .HasColumnName("VRoleid");
            entity.Property(e => e.Vrolename)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("VRolename");
        });

        modelBuilder.Entity<Vuser>(entity =>
        {
            entity.HasKey(e => e.Vuserid).HasName("PK__VUsers__EB70C7AE5F85A53D");

            entity.ToTable("VUsers", tb => tb.HasTrigger("EncryptionPassword"));

            entity.Property(e => e.Vuserid).HasColumnName("VUserid");
            entity.Property(e => e.VconfirmPassword)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("VConfirmPassword");
            entity.Property(e => e.Vcreated)
                .HasColumnType("date")
                .HasColumnName("VCreated");
            entity.Property(e => e.Vemail)
                .HasMaxLength(90)
                .IsUnicode(false)
                .HasColumnName("VEmail");
            entity.Property(e => e.VlastLoginDate)
                .HasColumnType("datetime")
                .HasColumnName("VLastLoginDate");
            entity.Property(e => e.Vmobile).HasColumnName("VMobile");
            entity.Property(e => e.Vpassword)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("VPassword");
            entity.Property(e => e.Vroleid).HasColumnName("VRoleid");
            entity.Property(e => e.Vusername)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("VUsername");

            entity.HasOne(d => d.Vrole).WithMany(p => p.Vusers)
                .HasForeignKey(d => d.Vroleid)
                .HasConstraintName("FK__VUsers__VRoleid__38996AB5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
