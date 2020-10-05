using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniversityLectures.Services;
using UniversityLectures.Models;
using UniversityLectures.Models.ViewModels;
using UniversityLectures.Services.Exceptions;
using System.Diagnostics;
using UniversityLectures.Models.ViemModels;
using Microsoft.EntityFrameworkCore;

namespace UniversityLectures.Controllers
{
    public class ProfessorsController : Controller
    {
        private readonly ProfessorService _professorService;
        private readonly DepartmentService _departmentService;

        public ProfessorsController(ProfessorService professorService, DepartmentService departmentService) 
        {
            _professorService = professorService;
            _departmentService = departmentService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _professorService.FindAllAsync();
            foreach (Professor prof in list) 
            {
                var department = await _departmentService.FindByIdAsync(prof.DepartmentId);
                prof.Department = new Department(department.Id, department.Name);
            }
            return View(list);
        }
        public async Task<IActionResult> Create() 
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new ProfessorFormViewModel {Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //To prevent cross-site request forgery
        public async Task<IActionResult> Create(Professor professor) 
        {
            if (!ModelState.IsValid) 
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new ProfessorFormViewModel { professor = professor, Departments = departments };
                return View(viewModel);
            }
            await _professorService.InsertAsync(professor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id) 
        {
            if (id == null) 
            {
                return RedirectToAction(nameof(Error), new { message = "Id is null" });
            }

            var obj = await _professorService.FindByIdAsync(id.Value);
            if (obj == null) 
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" }); 
            }

            return View(obj);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) 
        {
            try
            {
                await _professorService.RemoveProfessorAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException e) 
            {
                throw new IntegrityException(e.Message);
            }
            

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id is null" }); 
            }

            var obj = await _professorService.FindByIdAsync(id.Value);
            
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" }); 
            }
            var department = await _departmentService.FindByIdAsync(obj.DepartmentId);
            obj.Department = new Department(department.Id, department.Name);

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id) 
        {
            if (id == null) 
            {
                return RedirectToAction(nameof(Error), new { message = "Id is null" }); 
            }
            var obj = await _professorService.FindByIdAsync(id.Value);

            if (obj == null) 
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            ProfessorFormViewModel view = new ProfessorFormViewModel { professor = obj, Departments = departments };

            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Professor professor) 
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new ProfessorFormViewModel { professor = professor, Departments = departments };
                return View(viewModel);
            }
            if (id != professor.Id) 
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _professorService.UpdateAsync(professor);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e) 
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };

            return View(viewModel);
        }
    }
}
