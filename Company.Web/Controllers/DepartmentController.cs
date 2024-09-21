using Company.Repository.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmetRepository _departmetRepository;

        public DepartmentController(IDepartmetRepository departmetRepository)
        {
           _departmetRepository = departmetRepository;
        }
        public IActionResult Index()
        {
            var dept = _departmetRepository.GetAll();
            return View(dept);
        }
    }
}
