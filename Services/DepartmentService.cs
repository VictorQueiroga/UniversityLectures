using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityLectures.Data;
using UniversityLectures.Models;
using Microsoft.EntityFrameworkCore; 

namespace UniversityLectures.Services
{
    public class DepartmentService
    {
        private readonly UniversityLecturesContext _context;

        public DepartmentService(UniversityLecturesContext context) 
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync() 
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Department> FindByIdAsync(int id) 
        {
            return await _context.Department.FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
