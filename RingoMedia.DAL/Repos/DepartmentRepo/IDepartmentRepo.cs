using RingoMedia.DAL.Data.Models;

namespace RingoMedia.DAL.Repos.DepartmentRepo;
public interface IDepartmentRepository
{
    Task<List<Department>?> GetDepartmentListAsync();
    Task<Department?> GetDepartmentByIDAsync(int departmentID);
    Task<List<string>?> GetSubDepartmentListAsync(int departmentID);
    Task<List<string>?> GetDepartmentParentListAsync(int departmentID);
    Task<int> AddDepartmentAsync(Department department);
    Task<int> EditDepartmentAsync(Department department);
    Task<int> DeleteDepartmentAsync(Department department);
}
