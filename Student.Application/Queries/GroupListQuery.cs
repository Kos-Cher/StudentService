using MediatR;
using Student.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Application.Queries
{
    public class GroupListQuery : IRequest<GroupListQueryResult>
    {
        public GroupFilter Filter { get; set; }      
    }

    public class GroupFilter
    {
        public string GroupName { get; set; }
    }
}
