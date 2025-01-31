using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Data;
using StudentPortal.Models;
using StudentPortal.Models.Entities;

namespace StudentPortal.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            // student entity
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed,
            };

            await dbContext.Students.AddAsync(student);

            await dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListStudents()
        {
            var students = await dbContext.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student studentModel)
        {
            var student = await dbContext.Students.FindAsync(studentModel.Id);
            if(student is not null)
            {
                student.Name = studentModel.Name;
                student.Email = studentModel.Email;
                student.Phone = studentModel.Phone;
                student.Subscribed = studentModel.Subscribed;
            }
            await dbContext.SaveChangesAsync();
            return RedirectToAction("ListStudents");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student studentModel)
        {
            var student = await dbContext.Students.FindAsync(studentModel.Id);
            if (student is not null)
            {
                dbContext.Students.Remove(student);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("ListStudents");
        }
    }
}
