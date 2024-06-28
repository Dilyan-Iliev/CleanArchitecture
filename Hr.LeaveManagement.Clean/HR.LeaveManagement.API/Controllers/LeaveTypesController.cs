using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class LeaveTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveTypesController>
        [HttpGet]
        public async Task<IEnumerable<LeaveTypeDto>> Get()
        {
            //mediator will send the request (GetLeaveTypesQueryRequest)
            //and automatically invoke the correct handler (GetLeaveTypesQueryRequestHandler)
            var leaveTypes = await _mediator.Send(new GetLeaveTypesQueryRequest());
            return leaveTypes;
        }

        // GET api/<LeaveTypesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LeaveTypesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LeaveTypesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LeaveTypesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
