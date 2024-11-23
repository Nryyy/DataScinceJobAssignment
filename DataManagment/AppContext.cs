using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataManagment
{
    public class AppContext : DbContext
    {
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<ExperienceLevel> ExperienceLevels { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<CompanySize> CompanySizes { get; set; }
        public DbSet<EmploymentTypes> EmploymentTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<WorkSetting> WorkSettings { get; set; }
        public DbSet<JobAssignment> JobAssignments { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=DataScienceJobAssignment;User=sa;Password=Admin123!;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Визначення зв'язку один до багатьох між JobCategory та Job
            modelBuilder.Entity<JobCategory>()
                .HasMany(jc => jc.Jobs)                  // Одне JobCategory може мати багато Jobs
                .WithOne(j => j.Category)                // Кожен Job має один JobCategory
                .HasForeignKey(j => j.CategoryId)        // Зовнішній ключ у таблиці Jobs
                .OnDelete(DeleteBehavior.Restrict);      // Запобігаємо каскадному видаленню (якщо необхідно)

            // Визначення зв'язку один до багатьох між ExperienceLevel та Employee
            modelBuilder.Entity<ExperienceLevel>()
                .HasMany(el => el.Employees)             // Один ExperienceLevel може мати багато Employees
                .WithOne(e => e.ExperienceLevel)        // Кожен Employee має один ExperienceLevel
                .HasForeignKey(e => e.ExperienceLevelId) // Зовнішній ключ у таблиці Employees
                .OnDelete(DeleteBehavior.Restrict);      // Запобігаємо каскадному видаленню (якщо необхідно)

            // Визначення зв'язку один до багатьох між CompanySize та Company
            modelBuilder.Entity<CompanySize>()
                .HasMany(cs => cs.Companies)            // Один CompanySize може мати багато Companies
                .WithOne(c => c.Size)            // Кожен Company має один CompanySize
                .HasForeignKey(c => c.SizeId)           // Зовнішній ключ у таблиці Companies
                .OnDelete(DeleteBehavior.Restrict);      // Запобігаємо каскадному видаленню (якщо необхідно)

            // Визначення зв'язку один до багатьох між EmploymentType та Company
            modelBuilder.Entity<EmploymentTypes>()
                .HasMany(et => et.Companies)            // Один EmploymentType може мати багато Companies
                .WithOne(c => c.EmploymentType)        // Кожен Company має один EmploymentType
                .HasForeignKey(c => c.EmploymentTypeId) // Зовнішній ключ у таблиці Companies
                .OnDelete(DeleteBehavior.Restrict);      // Запобігаємо каскадному видаленню (якщо необхідно)

            // Визначення зв'язку один до багатьох між WorkSetting та JobAssignment
            modelBuilder.Entity<WorkSetting>()
                .HasMany(ws => ws.JobAssignments)       // Один WorkSetting може мати багато JobAssignments
                .WithOne(ja => ja.WorkSetting)          // Кожен JobAssignment має один WorkSetting
                .HasForeignKey(ja => ja.WorkSettingId)  // Зовнішній ключ у таблиці JobAssignments
                .OnDelete(DeleteBehavior.Restrict);      // Запобігаємо каскадному видаленню (якщо необхідно)

            // Визначення зв'язку один до багатьох між Job та JobAssignment
            modelBuilder.Entity<Job>()
                .HasMany(j => j.JobAssignments)        // Одне Job може мати багато JobAssignments
                .WithOne(ja => ja.Job)                 // Кожен JobAssignment має одне Job
                .HasForeignKey(ja => ja.JobId)         // Зовнішній ключ у таблиці JobAssignments
                .OnDelete(DeleteBehavior.Restrict);     // Запобігаємо каскадному видаленню (якщо необхідно)

            // Визначення зв'язку один до багатьох між Employee та JobAssignment
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.JobAssignments)        // Один Employee може мати багато JobAssignments
                .WithOne(ja => ja.Employee)            // Кожен JobAssignment має одного Employee
                .HasForeignKey(ja => ja.EmployeeId)    // Зовнішній ключ у таблиці JobAssignments
                .OnDelete(DeleteBehavior.Restrict);    // Запобігаємо каскадному видаленню (якщо необхідно)

            // Визначення зв'язку один до багатьох між Company та JobAssignment
            modelBuilder.Entity<Company>()
                .HasMany(c => c.JobAssignments)        // Одна Company може мати багато JobAssignments
                .WithOne(ja => ja.Company)             // Кожен JobAssignment має одну Company
                .HasForeignKey(ja => ja.CompanyId)     // Зовнішній ключ у таблиці JobAssignments
                .OnDelete(DeleteBehavior.Restrict);    // Запобігаємо каскадному видаленню (якщо необхідно)

            // Конфігурація для запобігання каскадному видаленню
            modelBuilder.Entity<JobAssignment>()
                .HasOne(ja => ja.Job)
                .WithMany(j => j.JobAssignments)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobAssignment>()
                .HasOne(ja => ja.Employee)
                .WithMany(e => e.JobAssignments)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobAssignment>()
                .HasOne(ja => ja.Company)
                .WithMany(c => c.JobAssignments)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobAssignment>()
                .HasOne(ja => ja.WorkSetting)
                .WithMany(ws => ws.JobAssignments)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

    }
}
