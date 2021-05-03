using MediatR;
using Student.Application.Interfaces;
using Student.Application.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Student.Application.Commands
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand,CreateGroupResult>
    {
        private readonly IStudentDbContext _context;

        public CreateGroupCommandHandler(IStudentDbContext context)
        {
            _context = context;
        }
        public async Task<CreateGroupResult> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {           
            var result = _context.Groups.Add(request.Group).Entity;
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(new CreateGroupResult { Group = result });
        }
    }
}
