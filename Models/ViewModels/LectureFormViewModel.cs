using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityLectures.Models.ViewModels
{
    public class LectureFormViewModel
    {
        public Lecture lecture { get; set; }
        public ICollection<Professor> Professors { get; set; }
    }
}
