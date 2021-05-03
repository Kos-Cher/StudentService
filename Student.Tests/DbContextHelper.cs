using Microsoft.EntityFrameworkCore;
using Student.Application.Interfaces;
using Student.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Tests
{
    public static class DbContextHelper
    {
        public static IStudentDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<StudentDbContext>()
                               .UseInMemoryDatabase(Guid.NewGuid().ToString())
                               .Options;
            var context = new StudentDbContext(options);

            context.SaveChanges();

            return context;
        }
    }
}
