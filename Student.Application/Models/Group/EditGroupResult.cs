using Student.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Application.Models
{
    public class EditGroupResult : BaseResult 
    {
        public Group Group { get; set; }
    }
}
