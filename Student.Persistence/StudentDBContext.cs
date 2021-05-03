using Microsoft.EntityFrameworkCore;
using Student.Application.Interfaces;
using Student.Domain;


namespace Student.Persistence
{
    public class StudentDbContext : DbContext, IStudentDbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options)
          : base(options) { }

        public DbSet<Domain.Student> Students { get; set; }

        public DbSet<Group> Groups { get; set; }

    }
}

