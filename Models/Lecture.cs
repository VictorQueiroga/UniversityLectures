using System;
using System.ComponentModel.DataAnnotations;
using UniversityLectures.Models.Enums;



namespace UniversityLectures.Models
{
    public class Lecture
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="{0} required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        [Range(500.0,100000.0,ErrorMessage ="{0} must be from {1} and {2}")]
        public double Price { get; set; }

        [Required(ErrorMessage = "{0} required")]
        public Status Status { get; set; }


        public Professor Professor { get; set; }

        public int ProfessorId { get; set; }

        public Lecture() { }

        public Lecture(int id,string title,DateTime date,double price,Status status,Professor professor) 
        {
            Id = id;
            Title = title;
            Date = date;
            Price = price;
            Status = status;
            Professor = professor;
        }

    }
}
