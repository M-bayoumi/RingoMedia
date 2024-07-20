using Microsoft.EntityFrameworkCore;
using RingoMedia.DAL.Data.Context;
using RingoMedia.DAL.Data.Models;

namespace RingoMedia.DAL.Repos.DepartmentRepo;
public class DepartmentRepository : IDepartmentRepository
{
    private readonly AppDbContext _context;

    public DepartmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Department>?> GetDepartmentListAsync()
    {
        return await _context.Departments.ToListAsync();
    }


    public async Task<Department?> GetDepartmentByIDAsync(int departmentID)
    {
        return await _context.Departments.FindAsync(departmentID);
    }

    public async Task<List<string>?> GetDepartmentParentListAsync(int departmentID)
    {
        var parentDepartments = new List<string>();
        var currentDepartment = await _context.Departments.FindAsync(departmentID);

        while (currentDepartment?.ParentDepartmentID != null)
        {
            currentDepartment = await _context.Departments.FindAsync(currentDepartment.ParentDepartmentID);
            if (currentDepartment != null)
            {
                parentDepartments.Add(currentDepartment.DepartmentName);
            }
        }

        return parentDepartments;
    }

    public async Task<List<string>?> GetSubDepartmentListAsync(int departmentID)
    {
        return await _context.Departments
            .Where(d => d.ParentDepartmentID == departmentID)
            .Select(d => d.DepartmentName)
            .ToListAsync();
    }

    public async Task<int> AddDepartmentAsync(Department newDepartment)
    {
        await _context.Departments.AddAsync(newDepartment);
        return _context.SaveChanges();
    }

    public async Task<int> EditDepartmentAsync(Department updatedDepartment)
    {
        _context.Departments.Update(updatedDepartment);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteDepartmentAsync(Department department)
    {
        _context.Departments.Remove(department);
        return await _context.SaveChangesAsync();
    }

}
