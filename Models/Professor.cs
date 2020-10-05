using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace UniversityLectures.Models
{
    public class Professor
    {
      
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage ="Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name ="Birth date")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        public Department Department  { get; set; }
        public int DepartmentId { get; set; }

        public ICollection<Lecture> Lectures = new List<Lecture>();

        public Professor() { }

        public Professor(int id,string name,string email,DateTime birthdate,Department department) 
        {
            Id = id;
            Name = name;
            Email = email;
            Birthdate = birthdate;
            Department = department;
        }

        public void AddLecture(Lecture lecture) 
        {
            Lectures.Add(lecture);
        }

        public void RemoveLecture(Lecture lecture) 
        {
            Lectures.Remove(lecture);
        }

        public double TotalPaymentProfessor(DateTime initial, DateTime final) 
        {
            return Lectures.Where(lecture => lecture.Date >= initial && lecture.Date <= final).Sum(lecture => lecture.Price);
        }
    }
}
