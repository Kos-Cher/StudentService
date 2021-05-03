using MediatR;
using Microsoft.AspNetCore.Mvc;
using Student.Application.Commands;
using Student.Application.Queries;
using System.Threading.Tasks;

namespace Student.Service.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        readonly IMediator _mediator;

        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get a list of groups
        /// </summary>
        /// <remarks>
        /// Get a list of groups, possibly filtered by Name
        /// </remarks>
        /// <param name="query">The query object</param>
        /// <response code="200">A list of all groups that match the filter</response>      
        /// <returns>List of groups</returns>
        [Route("list")]
        [HttpGet]
        public async Task<IActionResult> List([FromBody] GroupListQuery query)
        {
            if (query == null)
            {
                query = new GroupListQuery();
            }
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Create a new group
        /// </summary>
        /// <remarks>
        /// Method to create a new group from the passed json model
        /// </remarks>
        /// <param name="command">Json payload of a group</param>
        /// <response code="200">A newly created group</response>
        /// <response code="400">Invalid request if no body provided</response>
        /// <returns>New Group</returns>
        [HttpPost]
        [Route("new")]

        public async Task<IActionResult> Create([FromBody] CreateGroupCommand command)
        {
            if (command == null || command.Group == null)
                return Problem(statusCode: 400, title: "Request body doesn't contain the group object");
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Edit an existing group
        /// </summary>
        /// <remarks>
        /// Method to edit an existing group from the passed json model
        /// </remarks>
        /// <param name="command">Json payload of a group</param>
        /// <response code="200">Edited group</response>
        /// <response code="400">Invalid request if no body provided</response>
        /// <response code="404">Invalid request if group with given id is not found</response>
        /// <returns>Group with applied changes</returns>
        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> Edit([FromBody] EditGroupCommand command)
        {
            if (command == null || command.Group == null)
                return Problem(statusCode: 400, title: "Request body doesn't contain the group object");
            var result = await _mediator.Send(command);
            if (result.ObjectNotFound)
                return Problem(statusCode: 404, title: "Requested group was not found");
            return Ok(result);
        }

        /// <summary>
        /// Deletes an existing group
        /// </summary>
        /// <remarks>
        /// Method to delete a group a group by id
        /// </remarks>
        /// <param name="groupId">Id of the group to delete</param>
        /// <response code="200">Success</response>      
        /// <response code="404">Invalid request if group with given id is not found</response>
        [HttpDelete]
        [Route("delete/{groupId}")]
        public async Task<IActionResult> Delete(System.Guid groupId)
        {          
            var result = await _mediator.Send(new DeleteGroupCommand() { GroupId = groupId});
            if (result.ObjectNotFound)
                return Problem(statusCode: 404, title: "Requested group was not found");
            return Ok(result);
        }      
    }
}
