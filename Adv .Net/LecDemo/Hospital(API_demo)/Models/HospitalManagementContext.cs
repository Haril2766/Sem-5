using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Models;

public partial class HospitalManagementContext : DbContext
{
    public HospitalManagementContext()
    {
    }

    public HospitalManagementContext(DbContextOptions<HospitalManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<HospitalMaster> HospitalMasters { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Doctor");

            entity.Property(e => e.Degree).HasMaxLength(100);
            entity.Property(e => e.DoctorId).ValueGeneratedOnAdd();
            entity.Property(e => e.DoctorName).HasMaxLength(100);
            entity.Property(e => e.Expirience).HasMaxLength(50);
            entity.Property(e => e.Specialization).HasMaxLength(100);
        });

        modelBuilder.Entity<HospitalMaster>(entity =>
        {
            entity.HasKey(e => e.HospitalId).HasName("PK__Hospital__38C2E58F5FAE43D8");

            entity.ToTable("HospitalMaster");

            entity.Property(e => e.HospitalId).HasColumnName("HospitalID");
            entity.Property(e => e.ContactNumber).HasMaxLength(10);
            entity.Property(e => e.EmailAddress).HasMaxLength(250);
            entity.Property(e => e.HospitalAddress).HasMaxLength(250);
            entity.Property(e => e.HospitalName).HasMaxLength(150);
            entity.Property(e => e.OpeningDate).HasColumnType("datetime");
            entity.Property(e => e.OwnerName).HasMaxLength(250);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Patient");

            entity.Property(e => e.BloodGroup).HasMaxLength(3);
            entity.Property(e => e.ContactNo).HasMaxLength(10);
            entity.Property(e => e.PatientId).ValueGeneratedOnAdd();
            entity.Property(e => e.PatientName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
