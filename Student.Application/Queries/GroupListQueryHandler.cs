using MediatR;
using Microsoft.EntityFrameworkCore;
using Student.Application.Interfaces;
using Student.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Student.Application.Queries
{
    public class GroupListQueryHandler : IRequestHandler<GroupListQuery, GroupListQueryResult>
    {
        private readonly IStudentDbContext _context;

        public GroupListQueryHandler(IStudentDbContext context)
        {
            _context = context;
        }

        public async Task<GroupListQueryResult> Handle(GroupListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Groups.Include(s=>s.Students).AsQueryable();
            if (request.Filter!=null)
            {
                if (!string.IsNullOrWhiteSpace(request.Filter.GroupName))
                {
                    query = query.Where(s => s.Name.Contains(request.Filter.GroupName));
                }
            }
            var filteredGroups = await query.ToListAsync();

            return new GroupListQueryResult() { result = filteredGroups.Select(g => new GroupModel(g)).ToList() };
        }
    }   
}
