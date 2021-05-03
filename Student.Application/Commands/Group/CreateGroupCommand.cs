using MediatR;
using Student.Application.Models;
using Student.Domain;

namespace Student.Application.Commands
{
    public class CreateGroupCommand : IRequest<CreateGroupResult>
    {
        public Group Group { get; set; }
    }
}
