CREATE DATABASE AttendanceTracker
USE AttendanceTracker;
DROP DATABASE AttendanceTracker;

CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100),
    Position VARCHAR(100),
    Department VARCHAR(50),
    Salary DECIMAL(10, 2),
    JoinDate DATE
);

SELECT * FROM Employees;

-- CreateEmployee 
CREATE PROCEDURE CreateEmployee   
    @Name VARCHAR(100),
    @Position VARCHAR(100),
    @Department VARCHAR(50),
    @Salary DECIMAL(10, 2),
    @JoinDate DATE
AS
BEGIN
    INSERT INTO Employees (Name, Position, Department, Salary, JoinDate)
    VALUES (@Name, @Position, @Department, @Salary, @JoinDate);
END;
GO

--GetEmployeeById 
CREATE PROCEDURE GetEmployeeById
    @EmployeeID INT
AS
BEGIN
    SELECT * FROM Employees WHERE EmployeeID = @EmployeeID;
END;
GO

--Update Employee 
CREATE PROCEDURE UpdateEmployee
    @EmployeeID INT,
    @Name VARCHAR(100),
    @Position VARCHAR(100),
    @Department VARCHAR(50),
    @Salary DECIMAL(10, 2),
    @JoinDate DATE
AS
BEGIN
    UPDATE Employees
    SET Name = @Name,
        Position = @Position,
        Department = @Department,
        Salary = @Salary,
        JoinDate = @JoinDate
    WHERE EmployeeID = @EmployeeID;
END;
GO

--Delete Employee
CREATE PROCEDURE DeleteEmployee
    @EmployeeID INT
AS
BEGIN
    DELETE FROM Employees WHERE EmployeeID = @EmployeeID;
END;
GO

-- Search Employee
CREATE PROCEDURE SearchEmployeeByName
    @Name VARCHAR(100)
AS
BEGIN
    SELECT * FROM Employees WHERE Name LIKE '%' + @Name + '%';
END;
GO

-- Get count of employees
CREATE PROCEDURE CountEmployees
AS
BEGIN
    SELECT COUNT(*) AS TotalEmployees FROM Employees;
END;
GO





