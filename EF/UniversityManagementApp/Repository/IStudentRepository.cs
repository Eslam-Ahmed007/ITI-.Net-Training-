using System.Collections.Generic;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Repository
{
    /// <summary>
    /// Interface for Student Repository
    /// Defines contract for all CRUD operations
    /// </summary>
    public interface IStudentRepository
    {
        /// <summary>
        /// Retrieves all students from database
        /// </summary>
        List<Student> GetAllStudents();

        /// <summary>
        /// Retrieves a single student by ID
        /// </summary>
        Student GetStudentById(int id);

        /// <summary>
        /// Adds a new student to database
        /// </summary>
        bool AddStudent(Student student);

        /// <summary>
        /// Updates an existing student record
        /// </summary>
        bool UpdateStudent(Student student);

        /// <summary>
        /// Deletes a student by ID
        /// </summary>
        bool DeleteStudent(int id);
    }
}