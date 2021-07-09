using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniApp.Data;
using UniApp.Models;

namespace UniApp.Controllers
{
    public class StudentsController : Controller
    {

        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        // Index Method
        public async Task<IActionResult> Index(
            string sortOrder, 
            string currentFilter, 
            string searchString, 
            int? pageNUmber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortPram"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortPram"] = sortOrder == "Date" ? "date_desc" : "Date";


            if (searchString != null)
            {
                pageNUmber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
           
            var students = from s in _context.Students select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.FirstName.Contains(searchString)
                           || s.LastName.Contains(searchString));
            }

       
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 4;

            return View(await PaginatedList<Student>.CreateAsync(students.AsNoTracking(), pageNUmber ?? 1, pageSize));
        }

        // Details Method
       public async Task<IActionResult> Details(int? id)
        {
            if( id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                       .Include(s => s.Enrollments)
                       .ThenInclude( e => e.Course)
                       .AsNoTracking().FirstOrDefaultAsync( m => m.ID == id);

            if(student == null)
            {
                return NotFound();
            }

            return View(student);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentDate,FirstName,LastName")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException /* ex */)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Please try again if problem persist" +
                    " see your system administrator");
            }


            return View(student);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
                     
            if (student == null)
            {
                return NotFound();
            }

            return View(student);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EnrollmentDate,FirstName,LastName")] Student student)
        {
            if(id != student.ID)
            {
                return NotFound();              
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + 
                        "Please try again if problem persist" +
                        " see your system administrator");
                }
            }

            return View(student);
            
        }
    
    
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if(id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if(student == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete faild. Try again, and if the problem persist " +
                        " see your system administrator. ";
            }


            return View(student);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if(student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
            
        }
    
    }
}
