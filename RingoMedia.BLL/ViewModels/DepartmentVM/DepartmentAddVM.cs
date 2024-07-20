using System.ComponentModel.DataAnnotations;

namespace RingoMedia.BLL.ViewModels.DepartmentVM;

public class DepartmentAddVM
{
    [Required(ErrorMessage = "Department name is required.")]
    public string DepartmentName { get; set; } = string.Empty;

    [Display(Name = "Parent Department")]
    public int? ParentDepartmentID { get; set; }
}