using RingoMedia.BLL.ViewModels.DepartmentVM;
using RingoMedia.DAL.Data.Models;
using RingoMedia.DAL.UnitOfWork;

namespace RingoMedia.BLL.Managers.Departments;

public class DepartmentManager : IDepartmentManager
{
    private readonly IUnitOfWork _unitOfWork;

    public DepartmentManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> GetDepartmentListAsync()
    {
        IEnumerable<Department>? departments = await _unitOfWork.DepartmentRepository.GetDepartmentListAsync();
        if (departments is not null)
        {
            var data = departments.Select(x => new DepartmentReadVM
            {
                DepartmentID = x.DepartmentID,
                DepartmentName = x.DepartmentName,
                DepartmentLogo = x.DepartmentLogo,
            });
            return _unitOfWork.Response(true, data, null);

        }
        return _unitOfWork.Response(false, null, "There is no Departments");
    }
    public async Task<Response> GetDepartmentByIDAsync(int departmentID)
    {
        Department? department = await _unitOfWork.DepartmentRepository.GetDepartmentByIDAsync(departmentID);
        if (department is not null)
        {
            List<string>? subDepartments = await _unitOfWork.DepartmentRepository.GetSubDepartmentListAsync(departmentID);
            List<string>? parentDepartments = await _unitOfWork.DepartmentRepository.GetDepartmentParentListAsync(departmentID);

            var data = new DepartmentDetailsVM
            {
                DepartmentID = department.DepartmentID,
                DepartmentName = department.DepartmentName,
                DepartmentLogo = department.DepartmentLogo,
                SubDepartments = subDepartments ?? new List<string>(),
                ParentDepartments = parentDepartments ?? new List<string>(),
            };
            return _unitOfWork.Response(true, data, null);

        }
        return _unitOfWork.Response(false, null, $"Department is not found");
    }
    public async Task<Response> AddAsync(DepartmentAddVM departmentAddVM, byte[] image)
    {
        Department department = new Department
        {
            DepartmentName = departmentAddVM.DepartmentName,
            DepartmentLogo = image,
            ParentDepartmentID = departmentAddVM.ParentDepartmentID,
        };
        var result = await _unitOfWork.DepartmentRepository.AddDepartmentAsync(department);

        if (result > 0)
        {
            return _unitOfWork.Response(true, null, "Department has been added successfully");
        }
        return _unitOfWork.Response(false, null, "Failed to add Department");
    }

    public async Task<Response> EditAsync(DepartmentEditVM departmentEditVM, byte[] image)
    {
        Department? department = await _unitOfWork.DepartmentRepository.GetDepartmentByIDAsync(departmentEditVM.DepartmentID);
        if (department is not null)
        {
            department.DepartmentName = departmentEditVM.DepartmentName;
            department.DepartmentLogo = image;
            department.ParentDepartmentID = departmentEditVM.ParentDepartmentID;
        }
        bool result = await _unitOfWork.SaveChangesAsync() > 0;

        if (result)
        {
            return _unitOfWork.Response(true, null, "Department has been updated successfully");
        }
        return _unitOfWork.Response(false, null, "Failed to update Department");
    }
    public async Task<Response> DeleteAsync(int departmentID)
    {
        Department? department = await _unitOfWork.DepartmentRepository.GetDepartmentByIDAsync(departmentID);

        if (department is not null)
        {
            var result = await _unitOfWork.DepartmentRepository.DeleteDepartmentAsync(department);
            if (result > 0)
            {
                return _unitOfWork.Response(true, null, "Department has been deleted successfully");
            }
            return _unitOfWork.Response(false, null, "Failed to delete Department");
        }
        return _unitOfWork.Response(false, null, $"Department is not found");
    }
}
