-- 1 
SELECT 
    Dno, 
    COUNT(*) AS NumberOfEmployees
FROM Employee
GROUP BY Dno;



--2 

SELECT 
    Dno, 
    MIN(Salary) AS MinSalary
FROM Employee
GROUP BY Dno;



--3 
SELECT 
    Dno, 
    AVG(Salary) AS AvgSalary
FROM Employee
GROUP BY Dno;

--4
SELECT 
    Dno, 
    COUNT(*) AS NumberOfEmployees
FROM Employee
GROUP BY Dno
HAVING COUNT(*) > 3;

--5
SELECT 
    Pno, 
    COUNT(Essn) AS NumberOfEmployees
FROM Works_for
GROUP BY Pno
HAVING COUNT(Essn) > 2;


--6 
SELECT 
    Fname, 
    Lname, 
    Salary
FROM Employee
WHERE Salary = (SELECT MAX(Salary) FROM Employee);


 
--7 
SELECT 
    Fname, 
    Lname, 
    Salary
FROM Employee
WHERE Salary > (SELECT AVG(Salary) FROM Employee);


--8 
SELECT DISTINCT e.Fname, e.Lname
FROM Employee e
JOIN Works_for w ON e.SSN = w.Essn
WHERE w.Pno IN (
    SELECT w2.Pno
    FROM Employee e2
    JOIN Works_for w2 ON e2.SSN = w2.Essn
    WHERE e2.Fname = 'John' AND e2.Lname = 'Smith'
)
AND NOT (e.Fname = 'John' AND e.Lname = 'Smith');


--9
SELECT DISTINCT d.Dname
FROM Departments d
JOIN Project p ON d.Dnum = p.Dnum
JOIN Works_for w ON p.Pnumber = w.Pno
JOIN Employee e ON w.Essn = e.SSN
WHERE e.Fname = 'Alice';


--10 

CREATE VIEW Employee_Department_Salary AS
SELECT 
    e.Fname,
    e.Lname,
    d.Dname,
    e.Salary
FROM Employee e
JOIN Departments d 
    ON e.Dno = d.Dnum;


--11 
SELECT * FROM Employee_Department_Salary;


--12 
CREATE VIEW Project_Department AS
SELECT 
    p.Pname,
    p.Pnumber,
    d.Dname
FROM Project p
JOIN Departments d 
    ON p.Dnum = d.Dnum;


SELECT * FROM Project_Department;

--13
SELECT 
    Fname, 
    Lname, 
    Salary
FROM Employee
ORDER BY Salary DESC;


--14 
SELECT 
    Pname, 
    Pnumber, 
    Dnum
FROM Project
ORDER BY Pname ASC;


--15 
SELECT TOP 3 WITH TIES 
    Fname, 
    Lname, 
    Salary
FROM Employee
ORDER BY Salary DESC;


--16 
SELECT TOP 2 
    Dno, 
    COUNT(*) AS NumberOfEmployees
FROM Employee
GROUP BY Dno
ORDER BY COUNT(*) DESC;


--17 
SELECT 
    Pno, 
    COUNT(Essn) AS NumberOfEmployees
FROM Works_for 
GROUP BY Pno;


--18 
CREATE VIEW Courses AS
SELECT 
    Pname, 
    Pnumber, 
    Plocation, 
    Dnum
FROM Project;


INSERT INTO Project (Pname, Pnumber, Plocation, Dnum)
VALUES ('omar', 1000, 'cairo', 10);

DELETE FROM Courses
WHERE Pnumber = 1000;


--19
CREATE VIEW Employee_View AS
SELECT 
    SSN,
    Fname,
    Lname,
    Dno,
    Salary
FROM Employee;

UPDATE Employee_View
SET Salary = Salary * 1.10
WHERE Fname = 'John' AND Lname = 'Smith';



--20 
SELECT 
    e.Fname, 
    e.Lname, 
    e.Salary, 
    e.Dno
FROM Employee e
WHERE e.Salary > (
    SELECT AVG(e2.Salary)
    FROM Employee e2
    WHERE e2.Dno = e.Dno
);

