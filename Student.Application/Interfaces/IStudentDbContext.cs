using Microsoft.EntityFrameworkCore;
using Student.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Student.Application.Interfaces
{
    public interface IStudentDbContext
    {
        DbSet<Domain.Student> Students { get; set; }

        DbSet<Group> Groups { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        int SaveChanges();
    }
}
