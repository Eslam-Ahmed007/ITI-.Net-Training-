using Microsoft.EntityFrameworkCore;
using System;

namespace UniversityManagementApp_CodeFirst.Models
{
    /// <summary>
    /// Database Context for University Management System
    /// Code-First Approach - Defines database structure through code
    /// </summary>
    public class UniversityContext : DbContext
    {
        /// <summary>
        /// Students DbSet - Represents Students table
        /// </summary>
        public DbSet<Student> Students { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public UniversityContext()
        {
        }

        /// <summary>
        /// Constructor with options (for dependency injection)
        /// </summary>
        public UniversityContext(DbContextOptions<UniversityContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Configure database connection
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Connection string for Code-First approach
                // Will create database if it doesn't exist
                optionsBuilder.UseSqlServer(
                    @"Server=Eslam-Laptop;Database=UniversityCodeFirst;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True",
                    options => options.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null
                    )
                );

                // Enable sensitive data logging in development (optional)
#if DEBUG
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.LogTo(Console.WriteLine);
#endif
            }
        }

        /// <summary>
        /// Configure entity relationships and constraints using Fluent API
        /// This method is called when the model is being created
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Student entity
            modelBuilder.Entity<Student>(entity =>
            {
                // Table name
                entity.ToTable("Students");

                // Primary key
                entity.HasKey(e => e.StudentId);

                // Configure StudentId as Identity column
                entity.Property(e => e.StudentId)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(1, 1); // Start at 1, increment by 1

                // Configure FirstName
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                // Configure LastName
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                // Configure Email with unique constraint
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                entity.HasIndex(e => e.Email)
                    .IsUnique()
                    .HasDatabaseName("IX_Students_Email");

                // Configure Phone (optional)
                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnType("nvarchar(20)")
                    .IsRequired(false);

                // Configure EnrollmentDate
                entity.Property(e => e.EnrollmentDate)
                    .IsRequired()
                    .HasColumnType("date")
                    .HasDefaultValueSql("GETDATE()"); // Default to current date

                // Configure RowVersion for concurrency
                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                // Ignore computed properties (not in database)
                entity.Ignore(e => e.FullName);
            });

            // Seed initial data (optional)
            SeedData(modelBuilder);
        }

        /// <summary>
        /// Seed initial data into database
        /// </summary>
        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    StudentId = 1,
                    FirstName = "Ahmed",
                    LastName = "Hassan",
                    Email = "ahmed.hassan@university.edu",
                    Phone = "0123456789",
                    EnrollmentDate = new DateTime(2024, 1, 15)
                },
                new Student
                {
                    StudentId = 2,
                    FirstName = "Fatima",
                    LastName = "Ali",
                    Email = "fatima.ali@university.edu",
                    Phone = "0123456788",
                    EnrollmentDate = new DateTime(2024, 1, 20)
                },
                new Student
                {
                    StudentId = 3,
                    FirstName = "Omar",
                    LastName = "Mohamed",
                    Email = "omar.mohamed@university.edu",
                    Phone = "0123456787",
                    EnrollmentDate = new DateTime(2024, 2, 1)
                },
                new Student
                {
                    StudentId = 4,
                    FirstName = "Yasmin",
                    LastName = "Ibrahim",
                    Email = "yasmin.ibrahim@university.edu",
                    Phone = "0123456786",
                    EnrollmentDate = new DateTime(2024, 2, 10)
                },
                new Student
                {
                    StudentId = 5,
                    FirstName = "Khalid",
                    LastName = "Mahmoud",
                    Email = "khalid.mahmoud@university.edu",
                    Phone = "0123456785",
                    EnrollmentDate = new DateTime(2024, 2, 15)
                }
            );
        }

        /// <summary>
        /// Override SaveChanges to add automatic timestamp tracking (optional)
        /// </summary>
        public override int SaveChanges()
        {
            // You can add automatic CreatedDate/ModifiedDate tracking here
            return base.SaveChanges();
        }
    }
}