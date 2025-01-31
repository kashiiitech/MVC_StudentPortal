using Microsoft.AspNetCore.Mvc;
using StudentPortal.Models;

namespace StudentPortal.Controllers
{
    public class StudentsController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddStudentViewModel viewModel)
        {
            return View();
        }
    }
}
