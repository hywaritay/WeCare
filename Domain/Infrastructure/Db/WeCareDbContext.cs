using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Infrastructure.Entity;

namespace WeCare.Domain.Infrastructure.Db;

public class WeCareDbContext : DbContext
{
    public WeCareDbContext(DbContextOptions<WeCareDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Child> Children { get; set; }
    public DbSet<Vaccine> Vaccines { get; set; }
    public DbSet<VaccinationRecord> VaccinationRecords { get; set; }
    public DbSet<HealthRecord> HealthRecords { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<HealthcareFacility> HealthcareFacilities { get; set; }
    public DbSet<TwoFactorAuth> TwoFactorAuths { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.PhoneNumber).IsUnique();
            entity.Property(e => e.Role).HasConversion<int>();
        });

        // Child configuration
        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasOne(d => d.Mother)
                .WithMany(p => p.Children)
                .HasForeignKey(d => d.MotherId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.Property(e => e.Gender).HasConversion<int>();
        });

        // VaccinationRecord configuration
        modelBuilder.Entity<VaccinationRecord>(entity =>
        {
            entity.HasOne(d => d.Child)
                .WithMany(p => p.VaccinationRecords)
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(d => d.Vaccine)
                .WithMany(p => p.VaccinationRecords)
                .HasForeignKey(d => d.VaccineId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.Property(e => e.Status).HasConversion<int>();
        });

        // HealthRecord configuration
        modelBuilder.Entity<HealthRecord>(entity =>
        {
            entity.HasOne(d => d.Child)
                .WithMany(p => p.HealthRecords)
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Appointment configuration
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasOne(d => d.Child)
                .WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(d => d.Doctor)
                .WithMany()
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.Property(e => e.Status).HasConversion<int>();
        });

        // Notification configuration
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasOne(d => d.User)
                .WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.Property(e => e.Type).HasConversion<int>();
            entity.Property(e => e.Channel).HasConversion<int>();
            entity.Property(e => e.Status).HasConversion<int>();
        });

        // TwoFactorAuth configuration
        modelBuilder.Entity<TwoFactorAuth>(entity =>
        {
            entity.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Seed data for vaccines
        SeedVaccines(modelBuilder);
    }

    private void SeedVaccines(ModelBuilder modelBuilder)
    {
        var vaccines = new List<Vaccine>
        {
            new Vaccine
            {
                Id = Guid.NewGuid(),
                Name = "BCG",
                Description = "Bacillus Calmette-Guérin vaccine for tuberculosis",
                AgeInWeeks = 0,
                AgeInMonths = 0,
                Manufacturer = "Various",
                IsRequired = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Vaccine
            {
                Id = Guid.NewGuid(),
                Name = "Hepatitis B",
                Description = "Hepatitis B vaccine",
                AgeInWeeks = 0,
                AgeInMonths = 0,
                Manufacturer = "Various",
                IsRequired = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Vaccine
            {
                Id = Guid.NewGuid(),
                Name = "OPV",
                Description = "Oral Polio Vaccine",
                AgeInWeeks = 6,
                AgeInMonths = 1,
                Manufacturer = "Various",
                IsRequired = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Vaccine
            {
                Id = Guid.NewGuid(),
                Name = "DPT",
                Description = "Diphtheria, Pertussis, Tetanus vaccine",
                AgeInWeeks = 6,
                AgeInMonths = 1,
                Manufacturer = "Various",
                IsRequired = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Vaccine
            {
                Id = Guid.NewGuid(),
                Name = "Hib",
                Description = "Haemophilus influenzae type b vaccine",
                AgeInWeeks = 6,
                AgeInMonths = 1,
                Manufacturer = "Various",
                IsRequired = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Vaccine
            {
                Id = Guid.NewGuid(),
                Name = "PCV",
                Description = "Pneumococcal Conjugate Vaccine",
                AgeInWeeks = 6,
                AgeInMonths = 1,
                Manufacturer = "Various",
                IsRequired = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Vaccine
            {
                Id = Guid.NewGuid(),
                Name = "Rotavirus",
                Description = "Rotavirus vaccine",
                AgeInWeeks = 6,
                AgeInMonths = 1,
                Manufacturer = "Various",
                IsRequired = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Vaccine
            {
                Id = Guid.NewGuid(),
                Name = "IPV",
                Description = "Inactivated Polio Vaccine",
                AgeInWeeks = 14,
                AgeInMonths = 3,
                Manufacturer = "Various",
                IsRequired = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Vaccine
            {
                Id = Guid.NewGuid(),
                Name = "MMR",
                Description = "Measles, Mumps, Rubella vaccine",
                AgeInWeeks = 52,
                AgeInMonths = 12,
                Manufacturer = "Various",
                IsRequired = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        };

        modelBuilder.Entity<Vaccine>().HasData(vaccines);
    }
} 