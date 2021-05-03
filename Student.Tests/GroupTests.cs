using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Student.Application.Commands;
using Student.Application.Interfaces;
using Student.Domain;
using Student.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Student.Tests
{
    [TestClass]
    public class GroupTests
    {
        [TestMethod]
        public async Task create_group_test()
        {                  
            var contextStub = DbContextHelper.GetDbContext();
            var newGroupId = Guid.NewGuid();
            var createGroupCommand = new CreateGroupCommand()
            {
                Group = new Group()
                {
                    Id = newGroupId,
                    Name = newGroupId.ToString()
                }
            };
            var sut = new CreateGroupCommandHandler(contextStub);
            await sut.Handle(createGroupCommand, CancellationToken.None);
            var createdGroup = contextStub.Groups.First(g=>g.Id == newGroupId);
            Assert.AreEqual(createGroupCommand.Group.Id, createdGroup.Id);
            Assert.AreEqual(createGroupCommand.Group.Name, createdGroup.Name);
        }

        [TestMethod]
        public async Task edit_group_test()
        {
            var contextStub = DbContextHelper.GetDbContext();
            var newGroupId = Guid.NewGuid();

            contextStub.Groups.Add(new Group
            {
                Id = newGroupId,
                Name = newGroupId.ToString()             
            });
            contextStub.SaveChanges();
            (contextStub as StudentDbContext).ChangeTracker.Clear();
            var editGroupCommand = new EditGroupCommand()
            {
                Group = new Group()
                {
                    Id = newGroupId,
                    Name = "NewName"
                }
            };

            var sut = new EditGroupCommandHandler(contextStub);
            await sut.Handle(editGroupCommand, CancellationToken.None);
            var editedStudent = contextStub.Groups.First(s=>s.Id == newGroupId);
            Assert.AreEqual(editGroupCommand.Group.Name, editedStudent.Name);
        }

        [TestMethod]
        public async Task delete_group_test()
        {
            var contextStub = DbContextHelper.GetDbContext();
            var newGroupId = Guid.NewGuid();

            contextStub.Groups.Add(new Group
            {
                Id = newGroupId,
                Name = newGroupId.ToString()
            });
            contextStub.SaveChanges();
            (contextStub as StudentDbContext).ChangeTracker.Clear();
         
            var deleteGroupCommand = new DeleteGroupCommand() { GroupId = newGroupId };
            var sut = new DeleteGroupCommandHandler(contextStub);
            await sut.Handle(deleteGroupCommand, CancellationToken.None);           
            Assert.IsFalse(contextStub.Groups.Any(s => s.Id == newGroupId));
        }

        [TestMethod]
        public async Task get_groups_test()
        {
            var contextStub = DbContextHelper.GetDbContext();
            var newGroupId = Guid.NewGuid();

            contextStub.Groups.AddRange(new Group
                {
                    Id = Guid.NewGuid(),
                    Name = "First group"
                }, new Group
                {
                    Id = Guid.NewGuid(),
                    Name = "Second group"
                },
                new Group
                {
                    Id = Guid.NewGuid(),
                    Name = "Third group"
                });
            contextStub.SaveChanges();
            (contextStub as StudentDbContext).ChangeTracker.Clear();

            var deleteGroupCommand = new DeleteGroupCommand() { GroupId = newGroupId };
            var sut = new DeleteGroupCommandHandler(contextStub);
            await sut.Handle(deleteGroupCommand, CancellationToken.None);
            Assert.IsFalse(contextStub.Groups.Any(s => s.Id == newGroupId));
        }
    }
}
