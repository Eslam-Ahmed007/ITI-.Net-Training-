using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UniversityManagementApp_CodeFirst.Models;

namespace UniversityManagementApp_CodeFirst.Repository
{
    /// <summary>
    /// Repository implementation for Student entity - Code First Approach
    /// Handles all database operations using Entity Framework Core
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
                    // Ensure database is created (Code-First specific)
                    context.Database.EnsureCreated();

                    return context.Students
                        .OrderBy(s => s.StudentId)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
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
                    return context.Students
                        .FirstOrDefault(s => s.StudentId == id);
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
                    // Check for duplicate email
                    if (context.Students.Any(s => s.Email == student.Email))
                    {
                        throw new Exception("A student with this email already exists.");
                    }

                    context.Students.Add(student);
                    return context.SaveChanges() > 0;
                }
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database error adding student: {dbEx.InnerException?.Message ?? dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding student: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Updates an existing student record
        /// </summary>
        public bool UpdateStudent(Student student)
        {
            try
            {
                using (var context = new UniversityContext())
                {
                    var existingStudent = context.Students.Find(student.StudentId);

                    if (existingStudent == null)
                    {
                        throw new Exception("Student not found.");
                    }

                    // Check for duplicate email (excluding current student)
                    if (context.Students.Any(s => s.Email == student.Email && s.StudentId != student.StudentId))
                    {
                        throw new Exception("Another student with this email already exists.");
                    }

                    // Update properties
                    existingStudent.FirstName = student.FirstName;
                    existingStudent.LastName = student.LastName;
                    existingStudent.Email = student.Email;
                    existingStudent.Phone = student.Phone;
                    existingStudent.EnrollmentDate = student.EnrollmentDate;

                    return context.SaveChanges() > 0;
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("The student record was modified by another user. Please refresh and try again.");
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database error updating student: {dbEx.InnerException?.Message ?? dbEx.Message}", dbEx);
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

                    if (student == null)
                    {
                        throw new Exception("Student not found.");
                    }

                    context.Students.Remove(student);
                    return context.SaveChanges() > 0;
                }
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database error deleting student: {dbEx.InnerException?.Message ?? dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting student: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Check if database exists and create if needed
        /// </summary>
        public bool EnsureDatabaseCreated()
        {
            try
            {
                using (var context = new UniversityContext())
                {
                    return context.Database.EnsureCreated();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating database: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Get total student count
        /// </summary>
        public int GetStudentCount()
        {
            try
            {
                using (var context = new UniversityContext())
                {
                    return context.Students.Count();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting student count: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Search students by name or email
        /// </summary>
        public List<Student> SearchStudents(string searchTerm)
        {
            try
            {
                using (var context = new UniversityContext())
                {
                    return context.Students
                        .Where(s => s.FirstName.Contains(searchTerm) ||
                                   s.LastName.Contains(searchTerm) ||
                                   s.Email.Contains(searchTerm))
                        .OrderBy(s => s.LastName)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching students: {ex.Message}", ex);
            }
        }
    }
}