using MediatR;
using Microsoft.EntityFrameworkCore;
using Student.Application.Interfaces;
using Student.Application.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Student.Application.Commands
{
    public class EditStudentCommandHandler : IRequestHandler<EditStudentCommand, EditStudentResult>
    {
        private readonly IStudentDbContext _context;

        public EditStudentCommandHandler(IStudentDbContext context)
        {
            _context = context;
        }
        public async Task<EditStudentResult> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            if (!_context.Students.Any(g => g.Id == request.Student.Id))
                return new EditStudentResult { ObjectNotFound = true };
            var result = _context.Students.Update(request.Student).Entity;
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(new EditStudentResult { Student = result });
        }
    }
}
