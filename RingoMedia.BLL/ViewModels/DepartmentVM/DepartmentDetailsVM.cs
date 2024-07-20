using System.ComponentModel.DataAnnotations;

namespace RingoMedia.BLL.ViewModels.DepartmentVM;

public class DepartmentDetailsVM
{
    public int DepartmentID { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public byte[] DepartmentLogo { get; set; } = new byte[0];
    [Display(Name = "Parent Department")]
    public int? ParentDepartmentID { get; set; }
    public List<string>? SubDepartments { get; set; } = new List<string>();
    public List<string>? ParentDepartments { get; set; } = new List<string>();
}
