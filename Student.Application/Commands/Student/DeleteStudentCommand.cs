using MediatR;
using Student.Application.Models;
using System;

namespace Student.Application.Commands
{
    public class DeleteStudentCommand : IRequest<DeleteStudentResult>
    {
        public Guid? StudentId { get; set; }
    }
}
