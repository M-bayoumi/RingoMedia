using System.ComponentModel.DataAnnotations;

namespace RingoMedia.BLL.ViewModels.DepartmentVM;

public class DepartmentEditVM
{

    [Required(ErrorMessage = "Department ID is required.")]
    public int DepartmentID { get; set; }

    [Required(ErrorMessage = "Department name is required.")]
    public string DepartmentName { get; set; } = string.Empty;

    [Display(Name = "Parent Department")]
    public int? ParentDepartmentID { get; set; }
}