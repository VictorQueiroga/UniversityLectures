using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace UniversityLectures.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        public string Name { get; set; }

        public ICollection<Professor> Professors = new List<Professor>();

        public Department() 
        { 
        }

        public Department(int id, string name) 
        {
            Id = id;
            Name = name;
        }

        public void AddProfessor(Professor professor)
        {
            Professors.Add(professor);
        }

        public void RemoveProfessor(Professor professor)
        {
            Professors.Remove(professor);
        }

        public double TotalExpenses(DateTime initial,DateTime final) 
        {
            return Professors.Sum(professor => professor.TotalPaymentProfessor(initial, final));
        }
    }
}
