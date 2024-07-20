namespace RingoMedia.DAL.Data.Models;
public class Department
{
    public int DepartmentID { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public byte[] DepartmentLogo { get; set; } = new byte[0];
    public int? ParentDepartmentID { get; set; }
    public Department? ParentDepartment { get; set; }
    public List<Department> SubDepartments { get; set; } = new List<Department>();
}
