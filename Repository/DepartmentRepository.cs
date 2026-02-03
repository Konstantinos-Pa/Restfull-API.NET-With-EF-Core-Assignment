using Assignment.Models;
using Assignment.DTOs;
using Assignment.Service;
using Microsoft.EntityFrameworkCore;
using Mapster;
using Microsoft.IdentityModel.Tokens;

namespace Assignment.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetDepartmentAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id) + " must be greater than zero. (Thrown from GetDepartmentByIdAsync)");
            }
            Department? department = await _context.Departments.FirstOrDefaultAsync(a => a.Id == id);
            if (department == null)
            {
                throw new Exception("Address not found (Thrown from GetDepartmentByIdAsync)");
            }
            return department;
        }

        public async Task<int> AddDepartmentAsync(Department department)
        {
            if (department == null)
            {
                throw new ArgumentNullException(nameof(department) + " Is Null (Thrown from AddDepartmentAsync)");
            }
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department.Id;
        }

        public async Task UpdateDepartmentAsync(int id, Department department)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id) + " must be greater than zero. (Thrown from UpdateDepartmentAsyncc)");
            }
            else if (department == null)
            {
                throw new ArgumentNullException(nameof(department) + "Is Null (Thrown from UpdateDepartmentAsync)");
            }
            Department existingDepartment = await GetDepartmentByIdAsync(id);
            existingDepartment.Name = department.Name;
            existingDepartment.Phone = department.Phone;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id) + " must be greater than zero. (Thrown from DeleteDepartmentAsync)");
            }
            Department department = await GetDepartmentByIdAsync(id);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }
    }
}