using Microsoft.EntityFrameworkCore;
using UniversityLectures.Models;

namespace UniversityLectures.Data
{
    public class UniversityLecturesContext : DbContext
    {
        public UniversityLecturesContext (DbContextOptions<UniversityLecturesContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; }

        public DbSet<Professor> Professor { get; set; }

        public DbSet<Lecture> Lecture { get; set; }

    }
}
