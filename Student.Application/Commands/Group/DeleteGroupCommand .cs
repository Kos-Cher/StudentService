using MediatR;
using Student.Application.Models;
using Student.Domain;
using System;

namespace Student.Application.Commands
{
    public class DeleteGroupCommand : IRequest<DeleteGroupResult>
    {
        public Guid? GroupId { get; set; }
    }
}
