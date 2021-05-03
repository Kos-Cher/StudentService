using MediatR;
using Student.Application.Interfaces;
using Student.Application.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Student.Application.Commands
{
    public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, DeleteGroupResult>
    {
        private readonly IStudentDbContext _context;

        public DeleteGroupCommandHandler(IStudentDbContext context)
        {
            _context = context;
        }
        public async Task<DeleteGroupResult> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var groupToDelete = _context.Groups.FirstOrDefault(g => g.Id == request.GroupId);
            if (groupToDelete == null)
                return new DeleteGroupResult { ObjectNotFound = true }; 
            var result = _context.Groups.Remove(groupToDelete).Entity;
            await _context.SaveChangesAsync(cancellationToken);
            return new DeleteGroupResult { Group = result };
        }
    }
}
