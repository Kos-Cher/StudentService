using MediatR;
using Student.Application.Interfaces;
using Student.Application.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Student.Application.Commands
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand,CreateStudentResult>
    {
        private readonly IStudentDbContext _context;

        public CreateStudentCommandHandler(IStudentDbContext context)
        {
            _context = context;
        }
        public async Task<CreateStudentResult> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {      
            var result = _context.Students.Add(request.Student).Entity;
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(new CreateStudentResult { Student = result });
        }
    }
}
