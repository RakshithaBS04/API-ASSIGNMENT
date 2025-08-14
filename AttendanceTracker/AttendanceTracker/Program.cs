using Microsoft.Data.SqlClient;
using System;
using System.Data;

class AttendanceTracker
{
    static string connectionString = "Server=DESKTOP-00GRLI4;Database=AttendanceTracker;Integrated Security=True;TrustServerCertificate=True";

    // Create a new employee
    public static void CreateEmployee(string name, string position, string department, decimal salary, DateTime joinDate)
    {
        string query = "CreateEmployee"; // Stored procedure name

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Position", position);
            command.Parameters.AddWithValue("@Department", department);
            command.Parameters.AddWithValue("@Salary", salary);
            command.Parameters.AddWithValue("@JoinDate", joinDate);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine("Employee added successfully!");
        }
    }

    // Get employee by ID
    public static void GetEmployeeById(int employeeId)
    {
        string query = "GetEmployeeById"; // Stored procedure name

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@EmployeeID", employeeId);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Employee ID: {reader["EmployeeID"]}");
                    Console.WriteLine($"Name: {reader["Name"]}");
                    Console.WriteLine($"Position: {reader["Position"]}");
                    Console.WriteLine($"Department: {reader["Department"]}");
                    Console.WriteLine($"Salary: {reader["Salary"]}");
                    Console.WriteLine($"Join Date: {reader["JoinDate"]}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }

            connection.Close();
        }
    }

    // Update employee details
    public static void UpdateEmployee(int employeeId, string name, string position, string department, decimal salary, DateTime joinDate)
    {
        string query = "UpdateEmployee"; // Stored procedure name

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@EmployeeID", employeeId);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Position", position);
            command.Parameters.AddWithValue("@Department", department);
            command.Parameters.AddWithValue("@Salary", salary);
            command.Parameters.AddWithValue("@JoinDate", joinDate);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine("Employee updated successfully!");
        }
    }

    // Delete employee
    public static void DeleteEmployee(int employeeId)
    {
        string query = "DeleteEmployee"; // Stored procedure name

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@EmployeeID", employeeId);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine("Employee deleted successfully!");
        }
    }

    // Search employee by name
    public static void SearchEmployeeByName(string name)
    {
        string query = "SearchEmployeeByName"; // Stored procedure name

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Name", name);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Employee ID: {reader["EmployeeID"]}");
                    Console.WriteLine($"Name: {reader["Name"]}");
                    Console.WriteLine($"Position: {reader["Position"]}");
                    Console.WriteLine($"Department: {reader["Department"]}");
                    Console.WriteLine($"Salary: {reader["Salary"]}");
                    Console.WriteLine($"Join Date: {reader["JoinDate"]}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No employees found with that name.");
            }

            connection.Close();
        }
    }

    // Get total employee count
    public static void CountEmployees()
    {
        string query = "CountEmployees"; // Stored procedure name

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                Console.WriteLine($"Total Employees: {reader["TotalEmployees"]}");
            }

            connection.Close();
        }
    }

    // Main menu for user interaction
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nAttendance Tracker");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. Get Employee by ID");
            Console.WriteLine("3. Update Employee");
            Console.WriteLine("4. Delete Employee");
            Console.WriteLine("5. Search Employee by Name");
            Console.WriteLine("6. Count Employees");
            Console.WriteLine("7. Exit");
            Console.Write("Choose an option: ");

            try
            {
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter Employee Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter Position: ");
                        string position = Console.ReadLine();
                        Console.Write("Enter Department: ");
                        string department = Console.ReadLine();
                        Console.Write("Enter Salary: ");
                        decimal salary = decimal.Parse(Console.ReadLine());
                        Console.Write("Enter Join Date (yyyy-mm-dd): ");
                        DateTime joinDate = DateTime.Parse(Console.ReadLine());
                        CreateEmployee(name, position, department, salary, joinDate);
                        break;

                    case 2:
                        Console.Write("Enter Employee ID: ");
                        int empId = int.Parse(Console.ReadLine());
                        GetEmployeeById(empId);
                        break;

                    case 3:
                        Console.Write("Enter Employee ID to Update: ");
                        int updateId = int.Parse(Console.ReadLine());
                        Console.Write("Enter New Name: ");
                        string newName = Console.ReadLine();
                        Console.Write("Enter New Position: ");
                        string newPosition = Console.ReadLine();
                        Console.Write("Enter New Department: ");
                        string newDepartment = Console.ReadLine();
                        Console.Write("Enter New Salary: ");
                        decimal newSalary = decimal.Parse(Console.ReadLine());
                        Console.Write("Enter New Join Date (yyyy-mm-dd): ");
                        DateTime newJoinDate = DateTime.Parse(Console.ReadLine());
                        UpdateEmployee(updateId, newName, newPosition, newDepartment, newSalary, newJoinDate);
                        break;

                    case 4:
                        Console.Write("Enter Employee ID to Delete: ");
                        int deleteId = int.Parse(Console.ReadLine());
                        DeleteEmployee(deleteId);
                        break;

                    case 5:
                        Console.Write("Enter Employee Name to Search: ");
                        string searchName = Console.ReadLine();
                        SearchEmployeeByName(searchName);
                        break;

                    case 6:
                        CountEmployees();
                        break;

                    case 7:
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
    }
}
