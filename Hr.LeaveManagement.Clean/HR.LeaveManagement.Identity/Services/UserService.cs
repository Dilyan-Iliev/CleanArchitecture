using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace HR.LeaveManagement.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userMan;

        public UserService(UserManager<ApplicationUser> userMan)
        {
            _userMan = userMan;
        }

        public async Task<Employee> GetEmployee(string userId)
        {
            var employee = await _userMan.FindByIdAsync(userId);
            if (employee == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), userId);
            }

            return new Employee
            {
                Id = employee.Id,
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName
            };
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var employees = await _userMan.GetUsersInRoleAsync("Employee");

            return employees.Select(e => new Employee
            {
                Id = e.Id,
                Email = e.Email,
                FirstName = e.FirstName,
                LastName = e.LastName
            }).ToList();
        }
    }
}
