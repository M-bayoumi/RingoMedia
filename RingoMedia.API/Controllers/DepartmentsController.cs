using Microsoft.AspNetCore.Mvc;
using RingoMedia.BLL.Managers.Departments;
using RingoMedia.BLL.ViewModels.DepartmentVM;
using RingoMedia.DAL.Data.Models;

namespace RingoMedia.API.Controllers;

public class DepartmentsController : Controller
{
    IDepartmentManager _departmentManager;

    public DepartmentsController(IDepartmentManager departmentManager)
    {
        _departmentManager = departmentManager;
    }

    public async Task<IActionResult> Index()
    {
        Response response = await _departmentManager.GetDepartmentListAsync();
        if (response.Success)
        {
            return View("Index", response.Data);

        }
        return View("NotFound");
    }
    public async Task<IActionResult> DisplayLogo(int departmentID)
    {
        Response response = await _departmentManager.GetDepartmentByIDAsync(departmentID);
        if (response.Success)
        {
            DepartmentDetailsVM? departmentDetailsVM = response.Data as DepartmentDetailsVM;
            return File(departmentDetailsVM?.DepartmentLogo, "image/jpg");
        }
        return View("NotFound");
    }
    public async Task<IActionResult> Details(int id)
    {
        Response response = await _departmentManager.GetDepartmentByIDAsync(id);
        if (response.Success)
        {
            return View(response.Data);
        }
        return View("NotFound");
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        Response response = await _departmentManager.GetDepartmentListAsync();

        ViewBag.ParentDepartments = response.Data;
        return View("Add");
    }


    [HttpPost]
    public async Task<IActionResult> Add(DepartmentAddVM departmentAddVM, IFormFile departmentLogo)
    {
        if (ModelState.IsValid)
        {
            if (departmentLogo != null && departmentLogo.Length > 0)
            {

                using (var stream = new MemoryStream())
                {
                    await departmentLogo.CopyToAsync(stream);
                    byte[] image = stream.ToArray();
                    Response response = await _departmentManager.AddAsync(departmentAddVM, image);

                    if (response.Success)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
        }
        return View("Add", departmentAddVM);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        Response response = await _departmentManager.GetDepartmentByIDAsync(id);
        if (response.Success)
        {
            Response parentResponse = await _departmentManager.GetDepartmentListAsync();

            ViewBag.ParentDepartments = parentResponse.Data;
            return View("Edit", response.Data);
        }
        return View("NotFound");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(DepartmentEditVM departmentEditVM, IFormFile departmentLogo)
    {
        if (ModelState.IsValid)
        {
            if (departmentLogo != null && departmentLogo.Length > 0)
            {

                using (var stream = new MemoryStream())
                {
                    await departmentLogo.CopyToAsync(stream);
                    byte[] image = stream.ToArray();
                    Response response = await _departmentManager.EditAsync(departmentEditVM, image);

                    if (response.Success)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
        }
        return View("Edit", departmentEditVM);
    }


    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        Response response = await _departmentManager.GetDepartmentByIDAsync(id);
        if (response.Success)
        {
            return View("Delete", response.Data);
        }

        return View("NotFound");
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmDelete(int departmentID)
    {
        Response response = await _departmentManager.DeleteAsync(departmentID);
        if (response.Success)
        {
            return RedirectToAction("Index");
        }
        return View("NotFound");
    }
}


