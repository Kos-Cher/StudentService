using MediatR;
using Microsoft.EntityFrameworkCore;
using Student.Application.Interfaces;
using Student.Application.Models;
using Student.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Student.Application.Commands
{
    public class RemoveStudentFromGroupCommandHandler : IRequestHandler<RemoveStudentFromGroupCommand, RemoveStudentFromGroupResult>
    {
        private readonly IStudentDbContext _context;

        public RemoveStudentFromGroupCommandHandler(IStudentDbContext context)
        {
            _context = context;
        }

        public async Task<RemoveStudentFromGroupResult> Handle(RemoveStudentFromGroupCommand request, CancellationToken cancellationToken)
        {
            var result = new RemoveStudentFromGroupResult() { Errors = new List<string>()};
            if (!_context.Groups.Any(g => g.Id == request.GroupId))
            {
                result.ObjectNotFound = true;
                result.Errors.Add($"Group with Id = {request.GroupId} was not found");
            }
            if (!_context.Students.Any(g => g.Id == request.StudentId))
            {
                result.ObjectNotFound = true;
                result.Errors.Add($"Student with Id = {request.StudentId} was not found");
            }
            if (result.ObjectNotFound)
                return result;
            var existingStudent = await _context.Students.Where(g => g.Id == request.StudentId).Include(s => s.Groups).FirstAsync();
            var existingGroup = await _context.Groups.Where(g => g.Id == request.GroupId).FirstAsync();
            if (!existingStudent.Groups.Any(g=>g.Id == request.GroupId))
                //is not a part of a group - do nothing
                return result;
            existingStudent.Groups.Remove(existingGroup);
            await _context.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
