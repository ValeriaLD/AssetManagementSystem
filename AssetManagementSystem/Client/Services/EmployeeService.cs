using AssetManagementSystem.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace AssetManagementSystem.Client.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public EmployeeService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public List<Employee> Employees { get; set; }

        public async Task CreateEmployee(Employee employee)
        {
            var result = await _httpClient.PostAsJsonAsync("api/employee", employee);
            SetEmployees(result);
        }

        public async Task DeleteEmployee(int employeeId)
        {
            var result = await _httpClient.DeleteAsync($"api/employee/{employeeId}");
            SetEmployees(result);
        }

        public async Task<Employee> GetEmployeeById(int employeeId)
        {
            var result = await _httpClient.GetFromJsonAsync<Employee>($"api/employee/{employeeId}");
            if (result != null) 
                return result;
            throw new Exception("Employee not found!");
        }

        public async Task GetEmployees()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Employee>>("api/employee");
            if (result != null) Employees = result;
        }

        public async Task UpdateEmployee(Employee employee)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/employee/{employee.Id}", employee);
            SetEmployees(result);
        }

        private async Task SetEmployees(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Employee>>();
            Employees = response;
            _navigationManager.NavigateTo("employees");
        }
    }
}
