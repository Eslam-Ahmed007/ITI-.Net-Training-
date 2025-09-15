using System;
using System.Collections.Generic;

namespace Day_4_task
{
    internal class Program
    {
        #region Task 1 (Company, Department, Employee, Course, Car/Engine)

        // =============== Employee & Course (Association) ==================
        class Employee
        {
            public string Name { get; set; }
            public List<Course> Courses { get; set; } = new List<Course>();
        }

        class Course
        {
            public string Title { get; set; }
            public Instructor Instructor { get; set; }
            public List<Student> Students { get; set; } = new List<Student>();
            public CourseLevel Level { get; set; }
        }

        // =============== Department (Aggregation of Employees) ==================
        class Department
        {
            public string Name { get; set; }
            public List<Employee> Employees { get; set; } = new List<Employee>();
            public List<Student> Students { get; set; } = new List<Student>();
            public List<Instructor> Instructors { get; set; } = new List<Instructor>();
        }

        // =============== Company (Aggregation of Departments) ==================
        class Company
        {
            public string Name { get; set; }
            public List<Department> Departments { get; set; } = new List<Department>();
        }

        // =============== Car & Engine (Composition) ==================
        class Engine
        {
            public string Model { get; set; }
            public int HorsePower { get; set; }
            public void Start() => Console.WriteLine($"Engine {Model} with {HorsePower} HP started.");
        }

        class Car
        {
            public string Make { get; set; }
            public string Model { get; set; }
            private Engine Engine { get; }

            public Car(string make, string model, string engineModel, int hp)
            {
                Make = make;
                Model = model;
                Engine = new Engine { Model = engineModel, HorsePower = hp }; // Composition
            }

            public void Start() => Engine.Start();
        }

        #endregion

        #region Task 2 (Person, Student, Instructor, Override Introduce)

        abstract class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public virtual void Introduce()
            {
                Console.WriteLine($"Hi, I'm {Name}, {Age} years old.");
            }
        }

        class Instructor : Person
        {
            public Instructor(string name, int age) : base(name, age) { }

            public void TeachCourse(Course course)
            {
                course.Instructor = this;
                Console.WriteLine($"{Name} is now teaching {course.Title}.");
            }

            public override void Introduce()
            {
                Console.WriteLine($"Hello, I'm {Name}, {Age} years old, and I teach courses.");
            }
        }

        class Student : Person
        {
            public List<Course> Courses { get; set; } = new List<Course>();
            public List<Grade> Grades { get; set; } = new List<Grade>();

            public Student(string name, int age) : base(name, age) { }

            public void RegisterCourse(Course course)
            {
                Courses.Add(course);
                course.Students.Add(this);

                Console.WriteLine($"{Name} registered for {course.Title}.");

                switch (course.Level)
                {
                    case CourseLevel.Beginner:
                        Console.WriteLine("Good luck starting out!");
                        break;
                    case CourseLevel.Intermediate:
                        Console.WriteLine("Keep it up, you're progressing well!");
                        break;
                    case CourseLevel.Advanced:
                        Console.WriteLine("This will be challenging!");
                        break;
                }
            }

            public override void Introduce()
            {
                Console.WriteLine($"Hi, I'm {Name}, {Age} years old, and I'm a learner.");
            }
        }

        #endregion

        #region Task 3 (Shape, Circle, Rectangle, Polymorphism + Interface)

        abstract class Shape
        {
            public abstract double Area();
        }

        interface IDrawable
        {
            void Draw();
        }

        class Circle : Shape, IDrawable
        {
            public double Radius { get; set; }
            public Circle(double radius) { Radius = radius; }

            public override double Area() => Math.PI * Radius * Radius;

            public void Draw()
            {
                Console.WriteLine("Drawing Circle:");
                Console.WriteLine("   ***   ");
                Console.WriteLine(" *     * ");
                Console.WriteLine("   ***   ");
            }
        }

        class Rectangle : Shape, IDrawable
        {
            public double Width { get; set; }
            public double Height { get; set; }
            public Rectangle(double width, double height) { Width = width; Height = height; }

            public override double Area() => Width * Height;

            public void Draw()
            {
                Console.WriteLine("Drawing Rectangle:");
                for (int i = 0; i < (int)Height; i++)
                    Console.WriteLine(new string('*', (int)Width));
            }
        }

        #endregion

        #region Task 4 (IdGenerator, Auto IDs, Grade with Operator Overloading)

        static class IdGenerator
        {
            private static int currentId = 0;
            public static int GenerateId() => ++currentId;
        }

        class Grade
        {
            public int Value { get; set; }
            public Grade(int value) { Value = value; }

            public static Grade operator +(Grade g1, Grade g2) => new Grade(g1.Value + g2.Value);
            // == operator
            public static bool operator ==(Grade g1, Grade g2)
            {
                if (ReferenceEquals(g1, null) && ReferenceEquals(g2, null))
                    return true;

                if (ReferenceEquals(g1, null) || ReferenceEquals(g2, null))
                    return false;

                return g1.Value == g2.Value;
            }
            public static bool operator !=(Grade g1, Grade g2) => !(g1 == g2);

            public override bool Equals(object obj)
            {
                if (obj is Grade g)
                    return Value == g.Value;

                return false;
            }

            public override int GetHashCode() => Value.GetHashCode();
        }

        #endregion

        #region Task 5 (CourseLevel + System Simulation)

        enum CourseLevel { Beginner, Intermediate, Advanced }

        #endregion

        static void Main(string[] args)
        {
            // ================= Task 5 Simulation =================
            Console.WriteLine("\n--- Task 5: System Simulation ---");

            Company myCompany = new Company { Name = "Tech Corp" };

            Department depIT = new Department { Name = "IT" };
            Department depHR = new Department { Name = "HR" };
            myCompany.Departments.Add(depIT);
            myCompany.Departments.Add(depHR);

            // Instructors
            Instructor inst1 = new Instructor("Ali", 35);
            Instructor inst2 = new Instructor("Sara", 40);
            depIT.Instructors.Add(inst1);
            depHR.Instructors.Add(inst2);

            // Courses
            Course csharp = new Course { Title = "C#", Level = CourseLevel.Beginner };
            Course sql = new Course { Title = "SQL", Level = CourseLevel.Intermediate };
            Course oop = new Course { Title = "OOP", Level = CourseLevel.Advanced };

            inst1.TeachCourse(csharp);
            inst1.TeachCourse(oop);
            inst2.TeachCourse(sql);

            // Students
            Student s1 = new Student("Omar", 22);
            Student s2 = new Student("Laila", 23);
            Student s3 = new Student("Hossam", 24);

            depIT.Students.Add(s1);
            depIT.Students.Add(s2);
            depHR.Students.Add(s3);

            // Register Courses
            s1.RegisterCourse(csharp);
            s1.RegisterCourse(oop);
            s2.RegisterCourse(sql);
            s3.RegisterCourse(csharp);

            // Add Grades
            s1.Grades.Add(new Grade(85));
            s1.Grades.Add(new Grade(90));
            s2.Grades.Add(new Grade(78));
            s3.Grades.Add(new Grade(88));

            // ===== Report =====
            Console.WriteLine($"\nCompany Report: {myCompany.Name}");

            // Students Report
            foreach (var dept in myCompany.Departments)
            {
                foreach (var student in dept.Students)
                {
                    Grade total = new Grade(0);
                    foreach (var g in student.Grades) total += g;

                    Console.WriteLine($"{student.Name} ({dept.Name}) enrolled in:");
                    foreach (var course in student.Courses)
                    {
                        Console.WriteLine($" - {course.Title} ({course.Level}), Instructor: {course.Instructor?.Name}");
                    }
                    Console.WriteLine($" Total Grades = {total.Value}\n");
                }
            }

            // Instructors Report
            foreach (var dept in myCompany.Departments)
            {
                foreach (var inst in dept.Instructors)
                {
                    Console.WriteLine($"Instructor {inst.Name} teaches:");
                    foreach (var course in new List<Course> { csharp, sql, oop })
                    {
                        if (course.Instructor == inst)
                            Console.WriteLine($" - {course.Title}");
                    }
                }
            }

            // Department Report
            foreach (var dept in myCompany.Departments)
            {
                int employeesCount = dept.Employees.Count + dept.Students.Count + dept.Instructors.Count;
                Console.WriteLine($"\nDepartment {dept.Name} has {employeesCount} people (Employees+Students+Instructors).");
            }
        }
    }
}
