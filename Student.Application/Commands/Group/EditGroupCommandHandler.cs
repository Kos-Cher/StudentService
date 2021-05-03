using MediatR;
using Student.Application.Interfaces;
using Student.Application.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Student.Application.Commands
{
    public class EditGroupCommandHandler : IRequestHandler<EditGroupCommand, EditGroupResult>
    {
        private readonly IStudentDbContext _context;

        public EditGroupCommandHandler(IStudentDbContext context)
        {
            _context = context;
        }
        public async Task<EditGroupResult> Handle(EditGroupCommand request, CancellationToken cancellationToken)
        {           
            if (!_context.Groups.Any(g => g.Id == request.Group.Id))
                return new EditGroupResult { ObjectNotFound = true };
            var result = _context.Groups.Update(request.Group).Entity;
            await _context.SaveChangesAsync(cancellationToken);
            return new EditGroupResult { Group = result };
        }
    }
}
