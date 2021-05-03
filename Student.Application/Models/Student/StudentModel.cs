using System;
using System.Linq;

namespace Student.Application.Models
{
    public class StudentModel
    {
        public StudentModel(Domain.Student student)
        {
            this.Id = student.Id;
            this.FIO = student.FirstName + " " + (student.MiddleName != null ? (student.MiddleName + " " + student.LastName) : student.LastName);
            this.StudentId = student.StudentId;
            this.Groups = (student.Groups != null && student.Groups.Any()) ? string.Join(',', student.Groups) : string.Empty;
        }
        public Guid Id { get; private set; }
        public string FIO { get; private set; }

        public string StudentId { get; private set; }

        public string Groups { get; private set; }
    }

}
