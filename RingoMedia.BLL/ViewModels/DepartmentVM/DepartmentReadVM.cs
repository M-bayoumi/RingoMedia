namespace RingoMedia.BLL.ViewModels.DepartmentVM;

public class DepartmentReadVM
{
    public int DepartmentID { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public byte[] DepartmentLogo { get; set; } = new byte[0];
}
