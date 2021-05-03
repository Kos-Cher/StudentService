using Student.Domain;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Student.Application.Models
{
    public class GroupListQueryResult : BaseResult
    {
        public List<GroupModel> result;
    }

    public class GroupModel
    {
        public GroupModel(Group group)
        {
            this.Id = group.Id;
            this.GroupName = group.Name;
            this.StudentNumber = (group.Students != null && group.Students.Any()) ? group.Students.Count() : 0;
        }
        public Guid Id { get; private set; }

        public string GroupName { get; private set; }

        public int StudentNumber { get; private set; }
    }

}
