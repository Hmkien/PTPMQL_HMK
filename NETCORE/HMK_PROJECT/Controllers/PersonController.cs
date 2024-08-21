using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HMK_PROJECT.Data;
using HMK_PROJECT.Models;
using HMK_PROJECT.Models.Process;
using Humanizer;
using OfficeOpenXml;

namespace HMK_PROJECT.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Person
        public async Task<IActionResult> Index()
        {
            return View(await _context.Persons.ToListAsync());
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FullName,Address")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonId,FullName,Address")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(string id)
        {
            return _context.Persons.Any(e => e.PersonId == id);
        }
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("File is required");

            }
            var fileExtention = Path.GetExtension(file.FileName);
            if (fileExtention.ToLower() != ".xlsx" && fileExtention.ToLower() != ".xls")
            {
                return BadRequest("file upload is not match ");
            }
            var fileName = file.FileName;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload/Excels" + fileName);
            var fileLocation = new FileInfo(filePath).ToString();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                var dt = _excelProcess.ExcelToDataTable(fileLocation);
                var ExistingPerson = _context.Persons.Select(e => e.PersonId).ToList();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    var ps = new Person();
                    ps.PersonId = dt.Rows[i][0].ToString();
                    ps.FullName = dt.Rows[i][1].ToString();
                    ps.Address = dt.Rows[i][2].ToString();
                    if (!ExistingPerson.Contains(ps.PersonId))
                    {
                        _context.Persons.Add(ps);
                    }

                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
        }
        public IActionResult Download()
        {
            var fileName = "Person" + ".xlsx";
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet workbook = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                workbook.Cells["A1"].Value = "PersonId";
                workbook.Cells["B1"].Value = "Fullname";
                workbook.Cells["C1"].Value = "Address";
                var PersonList = _context.Persons.ToList();
                workbook.Cells["A2"].LoadFromCollection(PersonList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream, "application/vnd-ms-excel", fileName);

            }


        }

        public async Task<IActionResult> DeleteAll()
        {
            var EmployeeList = await _context.Persons.ToListAsync();
            _context.RemoveRange(EmployeeList);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
