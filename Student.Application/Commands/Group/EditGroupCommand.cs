using MediatR;
using Student.Application.Models;
using Student.Domain;

namespace Student.Application.Commands
{
    public class EditGroupCommand : IRequest<EditGroupResult>
    {
        public Group Group { get; set; }
    }
}
