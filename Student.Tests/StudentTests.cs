using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Student.Application.Commands;
using Student.Application.Interfaces;
using Student.Application.Queries;
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
    public class StudentTests
    {
        [TestMethod]
        public async Task create_student_test()
        {                  
            var contextStub = DbContextHelper.GetDbContext();
            var newStudentId = Guid.NewGuid();
            var createStudentCommand = new CreateStudentCommand()
            {
                Student = new Domain.Student()
                {
                    Id = newStudentId,
                    FirstName = newStudentId.ToString(),
                    LastName = newStudentId.ToString(),
                    Gender = "F"
                }
            };
            var sut = new CreateStudentCommandHandler(contextStub);
            await sut.Handle(createStudentCommand, CancellationToken.None);
            var createdStudent = contextStub.Students.First(s => s.Id == newStudentId);
            Assert.AreEqual(newStudentId, createdStudent.Id);
        }

        [TestMethod]
        public async Task edit_student_test()
        {
            var contextStub = DbContextHelper.GetDbContext();
            var newStudentId = Guid.NewGuid();
         

            contextStub.Students.Add(new Domain.Student()
            {
                Id = newStudentId,
                FirstName = newStudentId.ToString(),
                LastName = newStudentId.ToString(),
                Gender = "F"
            });
            contextStub.SaveChanges();
            (contextStub as StudentDbContext).ChangeTracker.Clear();
            var editStudentCommand = new EditStudentCommand()
            {
                Student = new Domain.Student()
                {
                    Id = newStudentId,
                    FirstName = "Johnny",
                    LastName = "Goode",
                    MiddleName = "B",
                    Gender = "M",
                    StudentId = "101010101010"
                }
            };

            var sut = new EditStudentCommandHandler(contextStub);
            await sut.Handle(editStudentCommand, CancellationToken.None);
            var editedStudent = contextStub.Students.First(s=>s.Id == newStudentId);
            Assert.AreEqual(editStudentCommand.Student.Gender, editedStudent.Gender);
            Assert.AreEqual(editStudentCommand.Student.FirstName, editedStudent.FirstName);
            Assert.AreEqual(editStudentCommand.Student.LastName, editedStudent.LastName);
            Assert.AreEqual(editStudentCommand.Student.StudentId, editedStudent.StudentId);
        }

        [TestMethod]
        public async Task delete_student_test()
        {
            var contextStub = DbContextHelper.GetDbContext();
            var newStudentId = Guid.NewGuid();
            contextStub.Students.Add(new Domain.Student()
            {
                Id = newStudentId,
                FirstName = newStudentId.ToString(),
                LastName = newStudentId.ToString(),
                Gender = "F"
            });
            contextStub.SaveChanges();
            (contextStub as StudentDbContext).ChangeTracker.Clear();
            var deleteStudentCommand = new DeleteStudentCommand() { StudentId = newStudentId };
            var sut = new DeleteStudentCommandHandler(contextStub);
            await sut.Handle(deleteStudentCommand, CancellationToken.None);
            var deletedStudent = contextStub.Students.FirstOrDefault(s => s.Id == newStudentId);
            Assert.IsNull(deletedStudent);
        }

        [TestMethod]
        public async Task add_student_to_group_test()
        {
            var contextStub = DbContextHelper.GetDbContext();
            var newStudentId = Guid.NewGuid();
            var newGroupId = Guid.NewGuid();
            contextStub.Students.Add(new Domain.Student()
            {
                Id = newStudentId,
                FirstName = newStudentId.ToString(),
                LastName = newStudentId.ToString(),
                Gender = "F"
            });
            contextStub.Groups.Add(new Group()
            {
                Id = newGroupId,
                Name = newGroupId.ToString()
            });
            contextStub.SaveChanges();
            (contextStub as StudentDbContext).ChangeTracker.Clear();
            var addStudentToGroupCommand = new AddStudentToGroupCommand(newStudentId, newGroupId);
            var sut = new AddStudentToGroupCommandHandler(contextStub);
            await sut.Handle(addStudentToGroupCommand, CancellationToken.None);
            var studentAddedToGroup = contextStub.Students.Include(s => s.Groups).Any(s => s.Groups.Any(g => g.Id == newGroupId));
            Assert.IsTrue(studentAddedToGroup);
        }

        [TestMethod]
        public async Task remove_student_from_group_test()
        {
            var contextStub = DbContextHelper.GetDbContext();
            var newStudentId = Guid.NewGuid();
            var newGroupId = Guid.NewGuid();
            contextStub.Groups.Add(new Group()
            {
                Id = newGroupId,
                Name = newGroupId.ToString()
            });
            contextStub.SaveChanges();
            contextStub.Students.Add(new Domain.Student()
            {
                Id = newStudentId,
                FirstName = newStudentId.ToString(),
                LastName = newStudentId.ToString(),
                Gender = "F",
                Groups = new List<Group> { await contextStub.Groups.FirstAsync()}
            });
         
            contextStub.SaveChanges();
            (contextStub as StudentDbContext).ChangeTracker.Clear();
            var removeStudentToGroupCommand = new RemoveStudentFromGroupCommand(newStudentId, newGroupId);
            var sut = new RemoveStudentFromGroupCommandHandler(contextStub);
            await sut.Handle(removeStudentToGroupCommand, CancellationToken.None);
            var studentRemovedFromGroup = !contextStub.Students.Include(s => s.Groups).First(s=>s.Id == newStudentId).Groups.Any();
            Assert.IsTrue(studentRemovedFromGroup);
        }

        [TestMethod]
        public async Task get_students_test()
        {
            var contextStub = DbContextHelper.GetDbContext();
            contextStub.Students.AddRange(new Domain.Student()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Alex",
                    LastName = "First",
                    Gender = "M"
                },
                new Domain.Student()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Alex",
                    LastName = "Second",
                    Gender = "M"
                },
                 new Domain.Student()
                 {
                     Id = Guid.NewGuid(),
                     FirstName = "Alexa",
                     LastName = "Third",
                     Gender = "F"
                 }
            );
            contextStub.SaveChanges();
            var studentListQuery = new StudentListQuery()
            {
                Filter = new StudentFilter()
                {
                    FirstName = "alex"
                },
                RowsToTake = 1
            };
            var sut = new StudentListQueryHandler(contextStub);
            var result = await sut.Handle(studentListQuery, CancellationToken.None);
            
            Assert.AreEqual(1, result.Students.Count);
            Assert.AreEqual("Alex First", result.Students[0].FIO);

            studentListQuery = new StudentListQuery()
            {
                Filter = new StudentFilter()
                {
                    FirstName = "alex"
                },
                RowsToTake = 1,
                RowsToSkip = 1
            };
            sut = new StudentListQueryHandler(contextStub);
            result = await sut.Handle(studentListQuery, CancellationToken.None);

            Assert.AreEqual(1, result.Students.Count);
            Assert.AreEqual("Alex Second", result.Students[0].FIO);

            studentListQuery = new StudentListQuery()
            {
                Filter = new StudentFilter()
                {
                    FirstName = "alex",
                    Gender = "F"
                },
                RowsToTake = 1
            };
            sut = new StudentListQueryHandler(contextStub);
            result = await sut.Handle(studentListQuery, CancellationToken.None);

            Assert.AreEqual(1, result.Students.Count);
            Assert.AreEqual("Alexa Third", result.Students[0].FIO);

            studentListQuery = new StudentListQuery();
            sut = new StudentListQueryHandler(contextStub);
            result = await sut.Handle(studentListQuery, CancellationToken.None);

            Assert.AreEqual(3, result.Students.Count);           
        }
    }
}
