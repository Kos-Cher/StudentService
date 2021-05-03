using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student.Application.Commands;
using Student.Application.Models;
using Student.Application.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Service.Controllers
{
    [Route("api/students")]
    [ApiController]   
    public class StudentController : ControllerBase
    {
        readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get a list of students
        /// </summary>
        /// <remarks>
        /// Get a list of students, possibly filtered and paged by parameters 
        /// </remarks>
        /// <param name="query">The query object - contains filter and pagnation parameters</param>
        /// <response code="200">A page of filtered list of students</response>      
        /// <returns>List of students</returns>
        [Route("list")]
        [HttpGet]
        public async Task<IActionResult> List([FromBody] StudentListQuery query)
        {          
            if (query == null)
            {
                query = new StudentListQuery();
            }          
            var result = await _mediator.Send(query);         
            return Ok(result);
        }

        /// <summary>
        /// Create a new student
        /// </summary>
        /// <remarks>
        /// Method to create a new student from the passed json model
        /// </remarks>
        /// <param name="command">Json payload of a student</param>
        /// <response code="200">A newly created student</response>
        /// <response code="400">Invalid request if no body provided</response>
        /// <returns>New Student</returns>
        [HttpPost]
        [Route("new")]
        public async Task<IActionResult> Create([FromBody] CreateStudentCommand command)
        {
            if (command == null || command.Student == null)
                return Problem(statusCode: 400, title: "Request body doesn't contain the student object");
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Edit an existing student
        /// </summary>
        /// <remarks>
        /// Method to edit an existing student from the passed json model
        /// </remarks>
        /// <param name="command">Json payload of a student</param>
        /// <response code="200">Edited student</response>
        /// <response code="400">Invalid request if no body provided</response>
        /// <response code="404">Invalid request if student with given id is not found</response>
        /// <returns>Student with applied changes</returns>
        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> Edit([FromBody] EditStudentCommand command)
        {

            if (command == null || command.Student == null)
                return Problem(statusCode: 400, title: "Request body doesn't contain the student object");
            var result = await _mediator.Send(command);
            if (result.ObjectNotFound)
                return Problem(statusCode: 404, title: "Requested student was not found");
            return Ok(result);
        }

        /// <summary>
        /// Adds student to a group
        /// </summary>
        /// <remarks>
        /// Method to add a student to a group
        /// </remarks>
        /// <param name="studentId">Student Id</param>
        /// <param name="groupId">Group Id</param>
        /// <response code="200">Success</response>
        /// <response code="404">Invalid request if student or group with given id were not found</response>
        [HttpPost]
        [Route("{studentId}/group/{groupId}")]
        public async Task<IActionResult> AddToGroup(Guid studentId, Guid groupId)
        {
            var result = await _mediator.Send(new AddStudentToGroupCommand(studentId, groupId));
            if (result.ObjectNotFound)
                return Problem(statusCode: 404, title: "Requested object was not found", detail: result?.Errors.Aggregate((i, j) => i + ',' + j));
            return Ok(result);
        }

        /// <summary>
        /// Removes student from a group
        /// </summary>
        /// <remarks>
        /// Method to remove a student from a group
        /// </remarks>
        /// <param name="studentId">Student Id</param>
        /// <param name="groupId">Group Id</param>
        /// <response code="200">Success</response>
        /// <response code="404">Invalid request if student or group with given id were not found</response>

        [HttpDelete]
        [Route("{studentId}/group/{groupId}")]
        public async Task<IActionResult> RemoveFromGroup(Guid studentId, Guid groupId)
        {
            var result = await _mediator.Send(new RemoveStudentFromGroupCommand(studentId, groupId));
            if (result.ObjectNotFound)
                return Problem(statusCode: 404, title: "Requested object was not found", detail: result?.Errors.Aggregate((i, j) => i + ',' + j));
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteStudentCommand command)
        {
            if (command == null || command.StudentId == null)
                return Problem(statusCode: 400, title: "Request body doesn't contain the student Id");

            var result = await _mediator.Send(command);
            if (result.ObjectNotFound)
                return Problem(statusCode: 404, title: "Requested student was not found");
            return Ok(result);
        }
    }
}
