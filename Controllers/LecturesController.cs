using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniversityLectures.Services;
using UniversityLectures.Models.ViewModels;
using UniversityLectures.Models;
using UniversityLectures.Models.ViemModels;
using System.Diagnostics;
using UniversityLectures.Services.Exceptions;

namespace UniversityLectures.Controllers
{
    public class LecturesController : Controller
    {
        private readonly LectureService _serviceLecture;
        private readonly ProfessorService _professorService;
        private readonly DepartmentService _departmentService;

        public LecturesController(LectureService serviceLecture, ProfessorService professorService, DepartmentService departmentService) 
        {
            _serviceLecture = serviceLecture;
            _professorService = professorService;
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            return View();
        }

        

        public async  Task<IActionResult> Create()
        {
            var professors = await _professorService.FindAllAsync();
            var viewModel = new LectureFormViewModel {Professors = professors};
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //To prevent cross-site request forgery
        public async Task<IActionResult> Create(Lecture lecture)
        {
            if (!ModelState.IsValid)
            {
                
                var viewModel = new LectureFormViewModel {  lecture = lecture };
                return View(viewModel);
            }
            await _serviceLecture.InsertAsync(lecture);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail() 
        {
            var list = await _serviceLecture.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id is null" });
            }
            var obj = await _serviceLecture.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            
            LectureFormViewModel view = new LectureFormViewModel { lecture = obj };

            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Lecture lecture)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new LectureFormViewModel { lecture = lecture };
                return View(viewModel);
            }
            if (id != lecture.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _serviceLecture.UpdateAsync(lecture);
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
