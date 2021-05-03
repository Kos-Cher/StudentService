using MediatR;
using Student.Application.Models;

namespace Student.Application.Commands
{
    public class CreateStudentCommand : IRequest<CreateStudentResult>
    {
        public Domain.Student Student { get; set; }
    }
}
