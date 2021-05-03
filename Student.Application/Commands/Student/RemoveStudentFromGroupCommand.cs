using MediatR;
using Student.Application.Models;
using Student.Domain;
using System;

namespace Student.Application.Commands
{
    public class RemoveStudentFromGroupCommand : IRequest<RemoveStudentFromGroupResult>
    {
        public RemoveStudentFromGroupCommand(Guid studentId, Guid groupId)
        {
            StudentId = studentId;
            GroupId = groupId;
        }

        public Guid StudentId { get; set; }
        public Guid GroupId { get; set; }
    }

}
