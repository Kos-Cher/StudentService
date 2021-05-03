using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Student.Application.Models
{
    public class StudentListQueryResult: BaseResult
    {
        public List<StudentModel> Students { get; set; }
    }

}
