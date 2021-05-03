using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Application.Models
{
    public class DeleteStudentResult : BaseResult
    {
        public Domain.Student Student { get; set; }
    }
}
