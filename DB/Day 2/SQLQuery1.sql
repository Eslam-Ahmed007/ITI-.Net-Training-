CREATE DATABASE CompanyTask;


USE CompanyTask;

CREATE TABLE Departments (
    DNumber INT PRIMARY KEY, --emp
    DName NVARCHAR(100) NOT NULL,
    ManagerId INT NULL, 
    HiringDate DATE NOT NULL
);



CREATE TABLE Employees (
    Id INT PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Addresss NVARCHAR(200),
    Gender CHAR(1) ,
    BirthDate DATE NOT NULL,
    SupervisorId INT NULL,
    DepartmentNumber INT NOT NULL,
    CONSTRAINT FK_Employees_Department FOREIGN KEY (DepartmentNumber)
        REFERENCES Departments(DNumber),
    CONSTRAINT FK_Employees_Supervisor FOREIGN KEY (SupervisorId)
        REFERENCES Employees(Id)
);


CREATE TABLE Projects (
    PNumber INT PRIMARY KEY,
    PName NVARCHAR(100) NOT NULL,
    Locationn NVARCHAR(100),
    City NVARCHAR(100),
    DeptNum INT NOT NULL,
    CONSTRAINT FK_Projects_Department FOREIGN KEY (DeptNum)
        REFERENCES Departments(DNumber)
);





CREATE TABLE Employee_Projects (
    EId INT NOT NULL,
    PNum INT NOT NULL,
    WorkingHours INT NOT NULL ,
    PRIMARY KEY (EId, PNum),
    CONSTRAINT FK_EmployeeProjects_Employee FOREIGN KEY (EId)
        REFERENCES Employees(Id),
    CONSTRAINT FK_EmployeeProjects_Project FOREIGN KEY (PNum)
        REFERENCES Projects(PNumber)
);

ALTER TABLE Departments
ADD CONSTRAINT FK_Departments_Manager FOREIGN KEY (ManagerId)
    REFERENCES Employees(Id);




INSERT INTO Departments (DNumber, DName, ManagerId, HiringDate)
VALUES 
(1, 'Human Resources', NULL, '2020-01-15'),
(2, 'IT', NULL, '2019-03-10'),
(3, 'Finance', NULL, '2021-06-01');

INSERT INTO Employees (Id, FirstName, LastName, Addresss, Gender, BirthDate, SupervisorId, DepartmentNumber)
VALUES
(101, 'John', 'Smith', 'alex', 'M', '1985-04-20', NULL, 1),
(102, 'Sarah', 'Johnson', 'Cairo', 'F', '1990-07-15', 101, 2),
(103, 'Michael', 'Brown', 'dakahlya', 'M', '1982-02-10', 101, 2),
(104, 'Emily', 'Davis', 'obour', 'F', '1995-12-05', 102, 3),
(105, 'David', 'Wilson', 'shrouk', 'M', '1988-09-22', 103, 3);


INSERT INTO Projects (PNumber, PName, Locationn, City, DeptNum)
VALUES
(201, 'HR System Upgrade', 'Building A', 'New York', 1),
(202, 'Website Redesign', 'Building B', 'Chicago', 2),
(203, 'Financial Audit', 'Building C', 'Boston', 3);

INSERT INTO Employee_Projects (EId, PNum, WorkingHours)
VALUES
(101, 201, 20), 
(102, 202, 35),
(103, 202, 30), 
(104, 203, 25), 
(105, 203, 40); 





SELECT *
FROM Employees
WHERE DepartmentNumber = 1;

SELECT FirstName + ' ' + LastName AS FullName
FROM Employees
WHERE Addresss LIKE '%Cairo%';

SELECT *
FROM Employees
WHERE BirthDate BETWEEN '1999-01-01' AND '2002-12-31';

SELECT P.PName
FROM Projects P
INNER JOIN Employee_Projects EP ON P.PNumber = EP.PNum
WHERE EP.EId = 2;


SELECT *
FROM Employees
ORDER BY LastName DESC;


SELECT *
FROM Employees
WHERE SupervisorId IS NULL;


UPDATE Employees
SET Addresss = 'Alex'
WHERE Id = 103;



DELETE FROM Employee_Projects
WHERE EId = 105;

DELETE FROM Employees
WHERE Id = 105;




SELECT *
FROM Employees
WHERE FirstName LIKE 'M%';

SELECT DISTINCT Addresss
FROM Employees;

SELECT *
FROM Employees
ORDER BY FirstName , LastName ASC;

SELECT *
FROM Employees
ORDER BY FirstName , LastName ASC;


SELECT *
FROM Employees
WHERE FirstName LIKE '____';  

SELECT *
FROM Employees
WHERE FirstName LIKE 'A_m%';


SELECT *
FROM Employees
WHERE FirstName LIKE '[A-M]%';

SELECT *
FROM Employees
WHERE Addresss NOT LIKE 'C%';
