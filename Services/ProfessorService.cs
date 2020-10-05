using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityLectures.Data;
using UniversityLectures.Models;
using Microsoft.EntityFrameworkCore;
using UniversityLectures.Services.Exceptions;
using UniversityLectures.Models.ViemModels;
using System.Diagnostics;


namespace UniversityLectures.Services
{
    public class ProfessorService
    {
        private readonly UniversityLecturesContext _context;

        public ProfessorService(UniversityLecturesContext context) 
        {
            _context = context;
        }

        public async Task<List<Professor>> FindAllAsync() 
        {
            return await _context.Professor.ToListAsync();
        }

        public async Task InsertAsync(Professor obj) 
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        public async Task<Professor> FindByIdAsync(int id) 
        {
            return await _context.Professor.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveProfessorAsync(int id) 
        {
            try 
            {
                Professor professor = await FindByIdAsync(id);
                _context.Professor.Remove(professor);
                await _context.SaveChangesAsync();
            }    
            catch (DbUpdateException)
            {
                throw new IntegrityException("Can't delete professor because he/she has lectures");
            }
        }

        public async Task UpdateAsync(Professor obj) 
        {
            bool hasAny = await _context.Professor.AnyAsync(x => x.Id == obj.Id);
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
