using MediatR;
using Student.Application.Interfaces;
using Student.Application.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Student.Application.Commands
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, DeleteStudentResult>
    {
        private readonly IStudentDbContext _context;

        public DeleteStudentCommandHandler(IStudentDbContext context)
        {
            _context = context;
        }
        public async Task<DeleteStudentResult> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var studentToDelete = _context.Students.FirstOrDefault(g => g.Id == request.StudentId);
            if (studentToDelete == null)
                return new DeleteStudentResult { ObjectNotFound = true }; 
            var result = _context.Students.Remove(studentToDelete).Entity;
            await _context.SaveChangesAsync(cancellationToken);
            return new DeleteStudentResult { Student = result };
        }
    }
}
