using Company.Data.Models;
using Company.Repository.interfaces;
using Company.Service.Interfaces;
using Company.Service.Interfaces.Department.Dto;
using Company.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmetService;

        public DepartmentController(IDepartmentService departmetService)
        {
            _departmetService = departmetService;
        }
        public IActionResult Index()
        {
            var dept = _departmetService.GetAll();
            return View(dept);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]  
        public IActionResult Create(DepartmentDto department )        
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _departmetService.Add(department);
                    return RedirectToAction(nameof(Index));

                }
                ModelState.AddModelError("DepartmentError", "ValidationErrors");
                return View(department);


            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("DepartmentError", ex.Message);
                return View(department);
            }

        }
        [HttpGet]
        public IActionResult Details(int? id,string viewname= "Details")
        {
            var dept = _departmetService.GetById(id);
            if (dept == null)
            { 
            return NotFound();  
            }
            return View(viewname,dept);   
        }
      /*  [HttpGet]
        public IActionResult Update(int? id)
        {
            return Details(id, "Update");

        }
        [HttpPost]
        public IActionResult Update(int? id,Department department)
        {
            if(department.Id!=id.Value)
            {
                return RedirectToAction("NOtFoundPage",null,"Home");
            }
            _departmetService.Update(department);
            return RedirectToAction(nameof(Index));
        }*/

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var dept = _departmetService.GetById(id);
                if (dept == null)
                {
                    return NotFound();
                }
                //softDelete
                /*dept.IsDeleted = true;
                _departmetService.Update(dept);*/

                _departmetService.Delete(dept);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("DepartmentError", ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
