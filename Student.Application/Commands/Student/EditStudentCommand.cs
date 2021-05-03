using MediatR;
using Student.Application.Models;

namespace Student.Application.Commands
{
    public class EditStudentCommand : IRequest<EditStudentResult>
    {
        public Domain.Student Student { get; set; }
    }
}
