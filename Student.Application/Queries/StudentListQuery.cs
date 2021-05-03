using MediatR;
using Student.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Application.Queries
{
    /// <summary>
    /// Class contains student list query parameters
    /// </summary>
    public class StudentListQuery : IRequest<StudentListQueryResult>
    {
        public StudentFilter Filter { get; set; }
       
        /// <summary>
        /// Offset to allow pagination
        /// </summary>
        public int RowsToSkip { get; set; }

        /// <summary>
        /// Max = 50, Min = 1
        /// </summary>
        public int RowsToTake { get; set; }
    }

    public class StudentFilter
    {
        public string Gender { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string StudentId { get; set; }
        public string GroupName { get; set; }
    }
}
