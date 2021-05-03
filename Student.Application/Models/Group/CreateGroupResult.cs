using Student.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Application.Models
{
    public class CreateGroupResult : BaseResult
    {
        public Group Group { get; set; }
    }
}
