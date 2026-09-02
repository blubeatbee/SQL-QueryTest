/* 
This SQL script can be run by pressing Execute once
It will delete any existing database with the same name and recreate
all tables and their records.
*/
USE master;
GO

/*
If database 'TestDataBase' exists:
	- Disconnect other connections in case others are using it
	- Remove the database..
Otherwise skip this.
*/
IF DB_ID('TestDataBase') IS NOT NULL
BEGIN
	ALTER DATABASE TestDataBase SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

	DROP DATABASE TestDataBase;
END

GO

-- Creates new database and switches to it.
CREATE DATABASE Gymnasium2;
GO

USE Gymnasium2;
GO


-- Creates database tables.
CREATE TABLE Class (
	ClassId			NCHAR(7) UNIQUE NOT NULL,
	DateStart		DATE NOT NULL,
	DateEnd			DATE NOT NULL,
	CONSTRAINT PK_Class PRIMARY KEY(ClassId)
);
CREATE TABLE Course (
	CourseId		INT IDENTITY(1,1) NOT NULL,
	ClassId			NCHAR(7) NOT NULL,
	Title			NVARCHAR(50) NOT NULL,
	DateStart		DATE NOT NULL,
	DateEnd			DATE NOT NULL,
	CONSTRAINT PK_Course PRIMARY KEY(CourseId),
	CONSTRAINT FK_Course_Class FOREIGN KEY(ClassId) REFERENCES Class(ClassId),
);
GO

CREATE TABLE Student (
	StudentId		INT IDENTITY(1,1) NOT NULL,
	ClassId			NCHAR(7) NOT NULL,
	SSN				NCHAR(12) NOT NULL,
	Surname			NVARCHAR(50) NOT NULL,
	Name			NVARCHAR(50) NOT NULL,
	DateEnrolled	DATE NOT NULL,
	DateQuit		DATE,
	IsActive		BIT NOT NULL,
	CONSTRAINT PK_Student PRIMARY KEY(StudentId),
	CONSTRAINT FK_Student_Class FOREIGN KEY(ClassId) REFERENCES Class(ClassId),
);

CREATE TABLE ERole (
	RoleId			INT IDENTITY(1,1) NOT NULL,
	RoleTitle		NVARCHAR(50) UNIQUE NOT NULL,
	CONSTRAINT PK_ERole PRIMARY KEY(RoleId),
);
GO

CREATE TABLE Employee (
	EmployeeId		INT IDENTITY(1,1) NOT NULL,
	SSN				NCHAR(12) NOT NULL,
	Surname			NVARCHAR(50) NOT NULL,
	Name			NVARCHAR(50) NOT NULL,
	RoleId			INT NOT NULL,
	Tasks			NVARCHAR(100),
	Salary			DECIMAL NOT NULL,
	DateHired		DATE NOT NULL,
	DateQuit		DATE,
	IsEmployed		BIT NOT NULL,
	CONSTRAINT PK_Employee PRIMARY KEY(EmployeeId),
	CONSTRAINT FK_Employee_ERole FOREIGN KEY(RoleId) REFERENCES ERole(RoleId),
);
GO

CREATE TABLE Grading (
	GradingId		INT IDENTITY(1,1) NOT NULL,
	Grade			INT NOT NULL,
	DateSet			DATE NOT NULL,
	CourseId		INT NOT NULL,
	StudentId		INT NOT NULL,
	TeacherId		INT NOT NULL,
	CONSTRAINT PK_Grading PRIMARY KEY(GradingId),
	CONSTRAINT FK_Grading_Course FOREIGN KEY(CourseId) REFERENCES Course(CourseId),
	CONSTRAINT FK_Grading_Student FOREIGN KEY(StudentId) REFERENCES Student(StudentId),
	CONSTRAINT FK_Grading_Teacher FOREIGN KEY(StudentId) REFERENCES Employee(EmployeeId),
);
GO

CREATE TABLE ClassEmployee (
	ClassId			NCHAR(7) NOT NULL,
	EmployeeId		INT NOT NULL,
	CONSTRAINT FK_ClassEmployee_Class FOREIGN KEY(ClassId) REFERENCES Class(ClassId),
	CONSTRAINT FK_ClassEmployee_Employee FOREIGN KEY(EmployeeId) REFERENCES Employee(EmployeeId),
);
GO