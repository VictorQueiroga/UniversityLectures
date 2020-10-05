using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityLectures.Models.ViewModels
{
    public class ProfessorFormViewModel
    {
        public Professor professor { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
