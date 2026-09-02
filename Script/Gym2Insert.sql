Use Gymnasium2;
GO

INSERT INTO Class(ClassId, DateStart, DateEnd) VALUES
	('2024EST', '2024-09-01', '2026-07-01'),
	('2024NAT', '2024-09-01', '2026-07-01'),
	('2025EST', '2025-09-01', '2027-07-01'),
	('2025NAT', '2025-09-01', '2027-07-01')
;
GO

INSERT INTO Course(ClassId, Title, DateStart, DateEnd) VALUES
	('2024EST', 'Bild', '2024-09-01', '2024-12-21'),
	('2024EST', 'Estetik', '2025-01-11', '2025-07-01'),
	('2024EST', 'Bild och form', '2025-09-01', '2025-12-21'),
	('2024EST', 'Examensarbete', '2026-01-10', '2026-07-01'),
	('2024NAT', 'Biologi', '2024-09-01', '2024-12-21'),
	('2024NAT', 'Kemi', '2025-01-11', '2025-07-01'),
	('2024NAT', 'Fysik', '2025-09-01', '2025-12-21'),
	('2024NAT', 'Examensarbete', '2026-01-10', '2026-07-01'),
	('2025EST', 'Bild', '2025-09-01', '2025-12-21'),
	('2025EST', 'Estetik', '2026-01-11', '2026-07-01'),
	('2025EST', 'Bild och form', '2026-09-01', '2026-12-21'),
	('2025EST', 'Examensarbete', '2027-01-10', '2027-07-01'),
	('2025NAT', 'Biologi', '2025-09-01', '2025-12-21'),
	('2025NAT', 'Kemi', '2026-01-11', '2026-07-01'),
	('2025NAT', 'Fysik', '2026-09-01', '2026-12-21'),
	('2025NAT', 'Examensarbete', '2027-01-10', '2027-07-01')
;
GO

INSERT INTO Student(ClassId, SSN, Surname, Name, DateEnrolled, IsActive, DateQuit) VALUES
	('2024EST', '200911198800', 'Larsson', 'Wilma', '2024-09-01', 'TRUE', NULL),
	('2024EST', '201004225555', 'De Andalucia',	'Antonio Acosta', '2024-09-01', 'TRUE', NULL),
	('2024EST', '201008228880', 'Sun', 'Hannah Xiamao', '2024-09-01', 'FALSE', '2025-12-03'),
	('2024EST', '201002172230', 'Novak', 'Drago', '2024-09-01', 'TRUE', NULL),
	('2024NAT', '200907263333', 'Algren', 'Barbro', '2024-09-01', 'TRUE', NULL),
	('2024NAT', '200909982220', 'Toresson', 'My Ljung', '2024-09-01', 'TRUE', NULL),
	('2024NAT', '200909091110', 'Toresson', 'Rosa Maja', '2024-09-01', 'TRUE', NULL),
	('2025EST', '201009306666', 'Bergström', 'Viktoria', '2025-09-01', 'TRUE', NULL),
	('2025EST', '201010319990', 'Stefanyk', 'Petro', '2025-03-21', 'TRUE', NULL),
	('2025EST', '201012219900', 'Ogden', 'Markus', '2025-09-01', 'TRUE', NULL),
	('2025NAT', '201006131111', 'Mariesdottir', 'Yvonne', '2025-09-01', 'TRUE', NULL),
	('2025NAT', '201010319990', 'Åberg', 'Alfons', '2025-09-01', 'TRUE', NULL),
	('2025NAT', '201104137770', 'Sadeghi', 'Leila', '2025-09-01', 'TRUE', NULL)
;
GO

INSERT INTO ERole(RoleTitle) VALUES
	('Teacher'),
	('Administrator'),
	('Principal')
;
GO

INSERT INTO Employee(SSN, RoleId, Surname, Name, Salary, DateHired, IsEmployed, DateQuit) VALUES
	('198411029999', 3, 'Eriksberg', 'Joakim', 40000.00, '2022-10-01', 'FALSE', '2024-07-01'),
	('199907168888', 2, 'Selimovic', 'Mikaela', 32000.00, '2022-10-01', 'TRUE', NULL),
	('198502017777', 1, 'Spike', 'Lee', 30000.00, '2022-10-01', 'TRUE', NULL),
	('199105174444', 1, 'Linneus', 'Ann-Charlotte', 30000.00, '2022-10-08', 'TRUE', NULL),
	('199605166660', 2, 'Balinska', 'Andreas', 28000.00, '2023-03-19', 'TRUE', NULL),
	('200202214440', 1, 'Hallwyl', 'Konny', 29000.00, '2023-03-20', 'TRUE', NULL),
	('199905013330', 3, 'Joseph', 'Fares', 35000.00, '2024-06-02', 'TRUE', NULL)
;
GO

INSERT INTO ClassEmployee(ClassId, EmployeeId) VALUES
	('2024EST', 3),
	('2024NAT', 4),
	('2025EST', 6),
	('2025EST', 3),
	('2025NAT', 4)
;
GO

INSERT INTO Grading(Grade, DateSet, StudentId, TeacherId, CourseId) VALUES
	(0, '2024-10-02', 3, 3, 1),
	(5, '2024-08-30', 6, 4, 5)
;
GO