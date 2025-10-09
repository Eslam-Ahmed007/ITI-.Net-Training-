using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Repository
{
    /// <summary>
    /// Repository implementation for Student entity
    /// Handles all database operations using Entity Framework
    /// </summary>
    public class StudentRepository : IStudentRepository
    {
        /// <summary>
        /// Retrieves all students from database
        /// </summary>
        public List<Student> GetAllStudents()
        {
            try
            {
                using (var context = new UniversityContext())
                {
                    return context.Students.ToList();
                }
            }
            catch (Exception ex)
            {
                // Log exception (implement logging as needed)
                throw new Exception($"Error retrieving students: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Retrieves a single student by ID
        /// </summary>
       
        public Student GetStudentById(int id)
        {
            try
            {
                using (var context = new UniversityContext())
                {
                    return context.Students.FirstOrDefault(s => s.StudentId == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving student: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Adds a new student to database
        /// </summary>
        public bool AddStudent(Student student)
        {
            try
            {
                using (var context = new UniversityContext())
                {
                    context.Students.Add(student);
                    return context.SaveChanges() > 0;
                }
            }

            catch (Exception ex)
            {
                throw new Exception($"Error adding student: {ex.Message}", ex);
            }

        }

        /// <summary>
        /// Updates an existing student record
        /// </summary>
        /// 
        public bool UpdateStudent(Student student)
        {
            try
            {
                using (var context = new UniversityContext())
                {
                    var existingStudent = context.Students.Find(student.StudentId);
                    if (existingStudent != null)
                    {
                        // Update properties
                        existingStudent.FirstName = student.FirstName;
                        existingStudent.LastName = student.LastName;
                        existingStudent.Email = student.Email;
                        existingStudent.Phone = student.Phone;
                        existingStudent.EnrollmentDate = student.EnrollmentDate;

                        return context.SaveChanges() > 0;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating student: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Deletes a student by ID
        /// </summary>
        public bool DeleteStudent(int id)
        {
            try
            {
                using (var context = new UniversityContext())
                {
                    var student = context.Students.Find(id);
                    if (student != null)
                    {
                        context.Students.Remove(student);
                        return context.SaveChanges() > 0;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting student: {ex.Message}", ex);
            }
        }
    }
}