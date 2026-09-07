USE Gymnasium2;
GO

-- Selects all Grading table rows newer than '2025-12-01'.
SELECT * FROM Grading WHERE DateSet > '2025-12-01';

-- Calculates the average value of all Grades from the Grading table.
SELECT AVG(Grade) AS 'Average grades' FROM Grading;

-- Selects certain columns from Employee and Role tables.
-- Additionally calculates how long an employee has worked at the Gymnasium.
SELECT 
	e.Name + ' ' + e.Surname AS "Employee Name",
	r.RoleTitle AS "Employee Role",
	e.Tasks,
	e.DateHired AS "Date Hired",
	e.DateQuit AS "Date Quit",
	CASE WHEN DateQuit IS NULL
		THEN CAST(DATEDIFF(YEAR, e.DateHired, GETDATE()) AS INT)
		ELSE CAST(DATEDIFF(YEAR, e.DateHired, e.DateQuit) AS INT)
	END AS "Years working"
FROM Employee AS e JOIN ERole AS r
ON e.RoleId = r.RoleId;

-- Selects name and class of Student table.  
SELECT 
	s.Surname + ' ' + s.Name AS "Student Name",
	s.ClassId
FROM Student AS s JOIN Grading AS g
ON s.StudentId = g.StudentId;

-- Selects all Gradings (its grading, course, the student it belongs to,
-- the teacher who set it) based on Student ID.
SELECT 
	g.DateSet, 
	g.Grade,
	c.Title AS "Course",
	e.Surname + ' ' + e.Name AS "Teacher",
	s.Surname + ' ' + s.Name AS "Student"
FROM Grading AS g 
	JOIN Course AS c
		ON g.CourseId = c.CourseId
	JOIN Employee AS e
		ON g.TeacherId = e.EmployeeId
	JOIN Student AS s
		ON g.StudentId = s.StudentId
WHERE g.StudentId = 5;

-- Calculates the total salary sum and average salary of each Role table row.
SELECT 
	r.RoleTitle AS "Employee Role",
	SUM(e.Salary) AS "Average Salary of Every Employee Role",
	AVG(e.Salary) AS "Average Salary of Every Employee Role"
FROM ERole as r JOIN Employee AS e
ON r.RoleId = e.RoleId
GROUP BY r.RoleTitle;
GO

-- Creates a stored procedure
CREATE PROCEDURE GetStudentById
	@Id INT = NULL
AS
SET NOCOUNT ON
SELECT 
	s.SSN,
	s.Surname + ' ' + s.Name AS "Student",
	s.ClassId AS "Class",
	s.DateEnrolled AS "Enrollment Date",
	s.DateQuit AS "Quit Date"
FROM Student AS s
WHERE s.StudentId = @Id;
GO

-- Executes stored procedure
EXEC GetStudentById @Id = 2;
GO