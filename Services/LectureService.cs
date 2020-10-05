using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityLectures.Data;
using UniversityLectures.Models;
using Microsoft.EntityFrameworkCore;
using UniversityLectures.Services.Exceptions;

namespace UniversityLectures.Services
{
    public class LectureService
    {
        private readonly UniversityLecturesContext _context;

        public LectureService(UniversityLecturesContext context) 
        {
            _context = context;
        }

        public async Task<List<Lecture>> FindAllAsync()
        {
            return await _context.Lecture.ToListAsync();
        }

        public async Task<Lecture> FindByIdAsync(int id)
        {
            return await _context.Lecture.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task<List<Lecture>> FindByDateAsync(DateTime? minDate, DateTime? maxDate) 
        {
            var lectures = from obj in _context.Lecture select obj;

            if (minDate.HasValue) 
            {
                lectures = lectures.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                lectures = lectures.Where(x => x.Date <= maxDate.Value);
            }

            return await lectures.Include(x => x.Professor).Include(x => x.Professor.Department).OrderByDescending(x => x.Date).ToListAsync();
        }

        public async Task<List<IGrouping<Department,Lecture>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var lectures = from obj in _context.Lecture select obj;

            if (minDate.HasValue)
            {
                lectures = lectures.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                lectures = lectures.Where(x => x.Date <= maxDate.Value);
            }

            return await lectures.Include(x => x.Professor).Include(x => x.Professor.Department).OrderByDescending(x => x.Date).GroupBy(x => x.Professor.Department).ToListAsync();
        }

        public async Task InsertAsync(Lecture obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Lecture obj)
        {
            bool hasAny = await _context.Lecture.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

        }
    }
}
