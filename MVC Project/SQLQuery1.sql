CREATE DATABASE TestDB;
GO
USE TestDB;
GO



CREATE TABLE Departments (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    ManagerName NVARCHAR(100) NOT NULL
);

CREATE TABLE Courses (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Degree DECIMAL(10,2) NOT NULL,
    MinimumDegree DECIMAL(10,2) NOT NULL,
    Hours INT NOT NULL,
    DepartmentId INT NOT NULL,
    FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
);


CREATE TABLE Students (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Address NVARCHAR(200) NOT NULL,
    Grade DECIMAL(5,2) NOT NULL,
    DepartmentId INT NOT NULL,
    FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
);


CREATE TABLE Instructors (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Address NVARCHAR(200) NOT NULL,
    Salary DECIMAL(10,2) NOT NULL,
    Image NVARCHAR(200) NOT NULL,
    DepartmentId INT NOT NULL,
    CourseId INT NOT NULL,
    FOREIGN KEY (DepartmentId) REFERENCES Departments(Id),
    FOREIGN KEY (CourseId) REFERENCES Courses(Id)
);




CREATE TABLE CourseStudents (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Degree DECIMAL(10,2) NOT NULL,
    CourseId INT NOT NULL,
    StudentId INT NOT NULL,
    FOREIGN KEY (CourseId) REFERENCES Courses(Id),
    FOREIGN KEY (StudentId) REFERENCES Students(Id)
);

INSERT INTO Departments (Name, ManagerName)
VALUES
('Information Systems', 'Dr. Sara Hassan'),
('Software Engineering', 'Dr. Hany Farid');






INSERT INTO Courses (Name, Degree, MinimumDegree, Hours, DepartmentId)
VALUES
('Database Systems', 100, 50, 60, 1),
('Operating Systems', 100, 55, 70, 1),
('Web Development', 100, 60, 80, 2),
('Artificial Intelligence', 100, 65, 75, 3);

INSERT INTO Students (Name, Address, Grade, DepartmentId)
VALUES
('Eslam Ahmed', 'Cairo, Egypt', 90, 1),
('Nada Hassan', 'Alexandria, Egypt', 88, 2),
('Omar Khaled', 'Giza, Egypt', 75, 3),
('Sara Mahmoud', 'Cairo, Egypt', 82, 1);


INSERT INTO Instructors (Name, Address, Salary, Image, DepartmentId, CourseId)
VALUES
('Dr. Ahmed Ali', 'Cairo', 15000.00, 'ahmed.jpg', 1, 1),
('Dr. Sara Hassan', 'Alexandria', 14000.00, 'sara.jpg', 2, 3),
('Dr. Omar Nabil', 'Giza', 13000.00, 'omar.jpg', 3, 4),
('Dr. Hany Farid', 'Cairo', 14500.00, 'hany.jpg', 1, 2);




INSERT INTO CourseStudents (Degree, CourseId, StudentId)
VALUES
(95, 1, 1),
(85, 2, 2),
(70, 3, 3),
(88, 4, 4),
(92, 1, 2),
(80, 3, 1);
