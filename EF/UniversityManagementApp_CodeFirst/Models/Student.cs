using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityManagementApp_CodeFirst.Models
{
    /// <summary>
    /// Student Entity - Code First Approach
    /// This class defines the structure of the Students table
    /// </summary>
    [Table("Students")] // Specify table name in database
    public class Student
    {
        /// <summary>
        /// Primary Key - Auto-generated identity column
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        /// <summary>
        /// First Name - Required field, max 50 characters
        /// </summary>
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 50 characters")]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name - Required field, max 50 characters
        /// </summary>
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 50 characters")]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        /// <summary>
        /// Email Address - Required, unique, with email validation
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        /// <summary>
        /// Phone Number - Optional field
        /// </summary>
        [Phone(ErrorMessage = "Invalid phone number format")]
        [StringLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string Phone { get; set; }

        /// <summary>
        /// Enrollment Date - Required, defaults to current date
        /// </summary>
        [Required(ErrorMessage = "Enrollment Date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        /// <summary>
        /// Computed property for full name (not stored in database)
        /// </summary>
        [NotMapped] // This property won't be created as a column
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Row version for concurrency handling (optional)
        /// </summary>
        [Timestamp]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Constructor with default values
        /// </summary>
        public Student()
        {
            EnrollmentDate = DateTime.Now;
        }
    }
}