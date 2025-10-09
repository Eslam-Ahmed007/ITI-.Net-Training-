-- 2)
SELECT d.*
FROM Departments d 
JOIN Employee e ON d.Dnum = e.Dno
WHERE e.Dno = (SELECT MIN(Dno) FROM Employee);

-- 4) List the full name of all managers who have no dependents.
SELECT m.Fname, m.Lname
FROM Departments dept
JOIN Employee m ON dept.MGRSSN = m.SSN
LEFT JOIN Dependent dep ON m.SSN = dep.ESSN
WHERE dep.ESSN IS NULL;

-- 5) For each department-- if its average salary is less than the average salary of all employees,
SELECT d.Dnum, d.Dname, COUNT(e.SSN) AS NumEmployees
FROM Departments d
JOIN Employee e ON d.Dnum = e.Dno
CROSS JOIN (SELECT AVG(Salary) AS overall_avg FROM Employee) oa
GROUP BY d.Dnum, d.Dname, oa.overall_avg
HAVING AVG(e.Salary) < oa.overall_avg;


-- 6) Retrieve a list of employee’s names and the projects names they are working on

SELECT e.Fname, e.Lname, p.Pname, e.Dno
FROM Employee e
JOIN Works_for w ON e.SSN = w.ESSN
JOIN Project p ON w.Pno = p.Pnumber
ORDER BY e.Dno, e.Lname, e.Fname;



-- 7) In the department table insert new department called "DEPT IT”, 

INSERT INTO Departments (Dname,Dnum, MGRSSN, [MGRStart Date])
VALUES ('DEPT IT', 100, 112233, '2006-11-01');

-- a) Update Mrs. Noha Mohamed to be the manager of department 100
UPDATE Departments
SET MGRSSN = 968574
WHERE Dnum = 100;

-- b) Update my record (SSN = 102672) to be department 20 manager
UPDATE Departments
SET MGRSSN = 102672
WHERE Dnum = 20;

-- c) Update employee 102660 to be supervised by me (SSN = 102672)
UPDATE Employee
SET SuperSSN = 102672
WHERE SSN = 102660;


-- 9) Display the department number, department name, and the manager’s SSN, first name, and last name.
SELECT d.Dnum, d.Dname, m.SSN AS ManagerSSN, m.Fname, m.Lname
FROM Departments d
JOIN Employee m ON d.MGRSSN = m.SSN;


-- 10) Display each department name with the projects that belong to it.
SELECT d.Dname, p.Pname
FROM Departments d
JOIN Project p ON d.Dnum = p.Dnum;


-- 11) Display all dependent information with the full name of the related employee.
SELECT d.*, e.Fname, e.Lname
FROM Dependent d
JOIN Employee e ON d.ESSN = e.SSN;


-- 12) Display the project number, project name, and location for projects located in Cairo or Alex.
SELECT p.Pnumber, p.Pname, p.Plocation
FROM Project p
WHERE p.Plocation IN ('Cairo', 'Alex');



-- 13) Display all projects whose names start with the letter A.
SELECT *
FROM Project
WHERE Pname LIKE 'A%';


-- 14) Display all employees who work in department 30 and whose salary is between 1000 and 2000.
SELECT *
FROM Employee
WHERE Dno = 30
  AND Salary BETWEEN 1000 AND 2000;



-- 15) Display the first names of employees in department 10 
--     who worked 10 hours or more on the project named 'Al Rabwah'.
SELECT e.Fname
FROM Employee e
JOIN Works_for w ON e.SSN = w.ESSN
JOIN Project p ON w.Pno = p.Pnumber
WHERE e.Dno = 10
  AND p.Pname = 'Al Rabwah'
  AND w.Hours >= 10;


  -- 16) Display the first names of employees who are supervised by an employee named 'Kamel'.
SELECT e.Fname
FROM Employee e
JOIN Employee s ON e.SuperSSN = s.SSN
WHERE s.Fname = 'Kamel';


-- 17) Display employees’ first names along with the project names they work on, ordered by the project name.
SELECT e.Fname, p.Pname
FROM Employee e
JOIN Works_for w ON e.SSN = w.ESSN
JOIN Project p ON w.Pno = p.Pnumber
ORDER BY p.Pname;



-- 18) Display the project number, project name, department name, manager’s last name, and manager’s address.
SELECT p.Pnumber, p.Pname, d.Dname, m.Lname AS ManagerLastName, m.Address AS ManagerAddress
FROM Project p
JOIN Departments d ON p.Dnum = d.Dnum
JOIN Employee m ON d.MGRSSN = m.SSN;
