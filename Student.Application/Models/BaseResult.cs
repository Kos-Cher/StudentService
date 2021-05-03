using Student.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Application.Models
{
    public class BaseResult
    {
        public bool ObjectNotFound { get; set; }

        public List<string> Errors { get; set; }
    }
}
