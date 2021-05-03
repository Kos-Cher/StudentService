using MediatR;
using Microsoft.EntityFrameworkCore;
using Student.Application.Interfaces;
using Student.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Student.Application.Queries
{
    public class StudentListQueryHandler : IRequestHandler<StudentListQuery, StudentListQueryResult>
    {
        private readonly IStudentDbContext _context;

        public StudentListQueryHandler(IStudentDbContext context)
        {
            _context = context;
        }

        public async Task<StudentListQueryResult> Handle(StudentListQuery request, CancellationToken cancellationToken)
        {
            //dafault and max rows = 50
            if (request.RowsToTake > 50 || request.RowsToTake < 1)
            {
                request.RowsToTake = 50;
            }
          
            var query = _context.Students.Include(s=>s.Groups).AsQueryable();
            if (request.Filter!=null)
            {
                if (!string.IsNullOrWhiteSpace(request.Filter.Gender))
                {
                    query = query.Where(s=>s.Gender.Equals(request.Filter.Gender));
                }
                if (!string.IsNullOrWhiteSpace(request.Filter.FirstName))
                {
                    query = query.Where(s => s.FirstName.Contains(request.Filter.FirstName, StringComparison.InvariantCultureIgnoreCase));
                }
                if (!string.IsNullOrWhiteSpace(request.Filter.LastName))
                {
                    query = query.Where(s => s.LastName.Contains(request.Filter.LastName, StringComparison.InvariantCultureIgnoreCase));
                }
                if (!string.IsNullOrWhiteSpace(request.Filter.MiddleName))
                {
                    query = query.Where(s => s.MiddleName.Contains(request.Filter.MiddleName, StringComparison.InvariantCultureIgnoreCase));
                }
                if (!string.IsNullOrWhiteSpace(request.Filter.StudentId))
                {
                    query = query.Where(s => s.StudentId.Equals(request.Filter.StudentId));
                }
                if (!string.IsNullOrWhiteSpace(request.Filter.GroupName))
                {
                    query = query.Include(s => s.Groups).Where(s => s.Groups.Any(g=>g.Name.Contains(request.Filter.GroupName, StringComparison.InvariantCultureIgnoreCase)));
                }
            }
            var filteredStudents = await query.Skip(request.RowsToSkip).Take(request.RowsToTake).ToListAsync();

            return new StudentListQueryResult() { Students = filteredStudents.Select(s => new StudentModel(s)).ToList() };
        }
    }   
}
