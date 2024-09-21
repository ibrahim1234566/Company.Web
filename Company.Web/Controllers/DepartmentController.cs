using Company.Data.Models;
using Company.Repository.interfaces;
using Company.Service.Interfaces;
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
        public IActionResult Create(Department department )        
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
        public IActionResult Details(int id)
        {
            var dept = _departmetService.GetById(id);
            if (dept == null)
            { 
            return NotFound();  
            }
            return View(dept);   
        }
    }
}
