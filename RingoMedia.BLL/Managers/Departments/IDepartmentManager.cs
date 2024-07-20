using RingoMedia.BLL.ViewModels.DepartmentVM;
using RingoMedia.DAL.Data.Models;

namespace RingoMedia.BLL.Managers.Departments;

public interface IDepartmentManager
{
    Task<Response> GetDepartmentListAsync();
    Task<Response> GetDepartmentByIDAsync(int departmentID);
    Task<Response> AddAsync(DepartmentAddVM departmentAddVM, byte[] image);
    Task<Response> EditAsync(DepartmentEditVM departmentEditVM, byte[] image);
    Task<Response> DeleteAsync(int departmentID);
}