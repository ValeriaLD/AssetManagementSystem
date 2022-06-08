using AssetManagementSystem.Shared;

namespace AssetManagementSystem.Client.Services
{
    public interface IEmployeeService
    {
        List<Employee> Employees { get; set; }
        Task GetEmployees();
        Task<Employee> GetEmployeeById(int employeeId);
        Task CreateEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task DeleteEmployee(int employeeId);
    }
}
